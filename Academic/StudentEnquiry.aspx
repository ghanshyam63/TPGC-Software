<%@ Page Language="C#" MasterPageFile="~/ERPMaster.master" AutoEventWireup="true" CodeFile="StudentEnquiry.aspx.cs" Inherits="Academic_StudentEnquiry" Title="Student Enquiry" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
   <link href="../CSS/InvStyle.css" rel="stylesheet" type="text/css" />
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <Triggers>
            <asp:PostBackTrigger ControlID="btnList" />
            <asp:PostBackTrigger ControlID="btnNew" />
            <asp:PostBackTrigger ControlID="btnBin" />
            <asp:PostBackTrigger ControlID="gvDepMaster" />
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
                                                <asp:Label ID="lblHeader" runat="server" Text="Student Enquiry Setup"
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
                                                                <asp:ListItem Selected="True" Text="Student Name" Value="StudentName"></asp:ListItem>
                                                                <asp:ListItem Text="Ftaher Name" Value="FatherName"></asp:ListItem>
                                                                <asp:ListItem Text="Mother Name" Value="MotherName"></asp:ListItem>
               
                                                                <asp:ListItem Text="Class" Value="Class"></asp:ListItem>                                           
                                                                                         <asp:ListItem Text="Mobile No" Value="Mobile_No"></asp:ListItem>
                                                                                         <asp:ListItem Text="Student Id" Value="Student_Id"></asp:ListItem>
                                                                                         
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
                                        <asp:GridView ID="gvDepMaster" PageSize="<%# SystemParameter.GetPageSize() %>" runat="server"
                                            AutoGenerateColumns="False" Width="100%" AllowPaging="True" AllowSorting="True"
                                            CssClass="grid" OnPageIndexChanging="gvDepMaster_PageIndexChanging" OnSorting="gvDepMaster_OnSorting">
                                            <Columns>
                                                <asp:TemplateField HeaderText="<%$ Resources:Attendance,Edit %>">
                                                    <ItemTemplate>
                                                        <asp:ImageButton ID="btnEdit" runat="server" CommandArgument='<%# Eval("Student_Id") %>'
                                                            ImageUrl="~/Images/edit.png" CausesValidation="False" OnCommand="btnEdit_Command"
                                                            Visible="false" ToolTip="<%$ Resources:Attendance,Edit %>" /></ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Center" />
                                                 <ItemStyle CssClass="grid" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="<%$ Resources:Attendance,Delete %>">
                                                    <ItemTemplate>
                                                        <asp:ImageButton ID="IbtnDelete" runat="server" CausesValidation="False" CommandArgument='<%# Eval("Student_Id") %>'
                                                            ImageUrl="~/Images/Erase.png" OnCommand="IbtnDelete_Command" Visible="false"
                                                            ToolTip="<%$ Resources:Attendance,Delete %>" />
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Center" />
                                                    <ItemStyle CssClass="grid" />
                                                </asp:TemplateField>
                                                
                                                  <asp:TemplateField HeaderText="Student Id" SortExpression="Student_Id">
                                                    <ItemTemplate>
                                        
                                                        <asp:Label ID="lblDepartmentId1"  runat="server" Text='<%# Eval("Student_Id") %>'></asp:Label>
                                                        
                                                        
                                                    </ItemTemplate>
                                                    <ItemStyle CssClass="grid" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Student Name" SortExpression="StudentName">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lbstrStudentName" runat="server" Text='<%# Eval("StudentName") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle CssClass="grid" />
                                                </asp:TemplateField>
                                             <asp:TemplateField HeaderText="IDate" SortExpression="IDate">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblConversion_Location" runat="server" Text='<%# Eval("IDate") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle CssClass="grid" />
                                                </asp:TemplateField>
                                            
                                                <asp:TemplateField HeaderText="Father Name" SortExpression="FatherName">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblFatherName" runat="server" Text='<%# Eval("FatherName") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle CssClass="grid" />
                                                </asp:TemplateField>
                                            
                                                <asp:TemplateField HeaderText="Mother Name" SortExpression="MotherName">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblLocCode" runat="server" Text='<%# Eval("MotherName") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle CssClass="grid" />
                                                </asp:TemplateField>
                                             <asp:TemplateField HeaderText="Class" SortExpression="Class">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblParentLoc" runat="server" Text='<%# Eval("ClassName") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle CssClass="grid" />
                                                </asp:TemplateField>
                                               
                                                <asp:TemplateField HeaderText="Mobile_No" SortExpression="Mobile_No">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblConversion_Location" runat="server" Text='<%# Eval("Mobile_No") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle CssClass="grid" />
                                                </asp:TemplateField>
                                           <asp:TemplateField HeaderText="Address" SortExpression="Address">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblConversion_Location" runat="server" Text='<%# Eval("Address") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle CssClass="grid" />
                                                </asp:TemplateField>
                                          <asp:TemplateField HeaderText="Detail" SortExpression="Detail">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblConversion_Location" runat="server" Text='<%# Eval("Detail") %>'></asp:Label>
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
                <td width="170px"align='<%= Common.ChangeTDForDefaultLeft()%>' >
                    Student Name</td>
                <td width="1px">
                    :
                </td>
                <td align='<%= Common.ChangeTDForDefaultLeft()%>'>
                    <asp:TextBox ID="txtStudentName" BackColor="#e3e3e3" Width="250px" runat="server" CssClass="textComman" OnTextChanged="txtStudentName_OnTextChanged" AutoPostBack="true" />
                                   <cc1:AutoCompleteExtender ID="AutoCompleteExtender" runat="server" DelimiterCharacters=""
                                                        Enabled="True" ServiceMethod="GetCompletionListStudentName" ServicePath="" CompletionInterval="100"
                                                        MinimumPrefixLength="1" CompletionSetCount="1" TargetControlID="txtStudentName"
                                                        UseContextKey="True" CompletionListCssClass="completionList" CompletionListItemCssClass="listItem"
                                                        CompletionListHighlightedItemCssClass="itemHighlighted">
                                                    </cc1:AutoCompleteExtender>
                </td>
            <td width="170px" align='<%= Common.ChangeTDForDefaultLeft()%>'>
                    Father Name</td>
                <td width="1px">
                    :
                </td>
                <td align='<%= Common.ChangeTDForDefaultLeft()%>'>
                    <asp:TextBox ID="txtFatherName" Width="250px" runat="server" CssClass="textComman"  />
                 
                </td>
            </tr>
           
            <tr>
                <td width="170px" align='<%= Common.ChangeTDForDefaultLeft()%>' >
                    Class</td>
                <td width="1px">
                    :
                </td>
                <td align='<%= Common.ChangeTDForDefaultLeft()%>'>
                     <asp:DropDownList ID="ddlClass" Width="262px" runat="server"  CssClass="textComman" />
                 
                </td>
                 <td width="170px"align='<%= Common.ChangeTDForDefaultLeft()%>' >
                     Mother Name</td>
                <td width="1px">
                    :
                </td>
                <td align='<%= Common.ChangeTDForDefaultLeft()%>'>
                    <asp:TextBox ID="txtMotherName" runat="server" CssClass="textComman" 
                        Width="250px" />
                </td>

            </tr>
                
          
            
             <tr>
                <td width="170px"align='<%= Common.ChangeTDForDefaultLeft()%>' >
                    Address</td>
                <td width="5px">
                    :
                </td>
                <td align='<%= Common.ChangeTDForDefaultLeft()%>'>
                   <asp:TextBox ID="txtAddressName" BackColor="#e3e3e3" Width="292px" 
                        runat="server" CssClass="textComman" Height="71px" 
                        />
                 
                </td>
                   <td width="170px"align='<%= Common.ChangeTDForDefaultLeft()%>' >
                       Mobile No</td>
                <td width="1px">
                    :
                </td>
                <td align='<%= Common.ChangeTDForDefaultLeft()%>'>
                   <asp:TextBox ID="txtMobileNo"  Width="250px" runat="server" CssClass="textComman"  />
                 
                </td>
            </tr>
            
           
            
            <tr>
                <td width="170px"align='<%= Common.ChangeTDForDefaultLeft()%>' >
                    Mobile No 2</td>
                <td width="1px">
                    :
                </td>
                <td align='<%= Common.ChangeTDForDefaultLeft()%>'>
                   <asp:TextBox ID="txtMobileNo2"  Width="250px" runat="server" CssClass="textComman"  />
                 
                </td>
                <td width="170px"align='<%= Common.ChangeTDForDefaultLeft()%>' >
                    Detail</td>
                <td width="1px">
                    :
                </td>
                <td align='<%= Common.ChangeTDForDefaultLeft()%>'>
                   <asp:TextBox ID="txtDetail"  Width="250px" runat="server" CssClass="textComman"  />
                 
                </td>
    
            </tr>
             
 <tr>
                <td width="170px"align='<%= Common.ChangeTDForDefaultLeft()%>' >
                    Amount</td>
                <td width="1px">
                    :
                </td>
                <td align='<%= Common.ChangeTDForDefaultLeft()%>'>
                   <asp:TextBox ID="txtAmount" runat="server" CssClass="textComman" 
                           />
                 
                </td>
                <td width="170px"align='<%= Common.ChangeTDForDefaultLeft()%>' >
                  Enquiry Date</td>
                <td width="1px">
                    :
                </td>
                <td align='<%= Common.ChangeTDForDefaultLeft()%>'>
                   <asp:TextBox ID="txtIDate" runat="server" CssClass="textComman"  />
                 
                    <cc1:CalendarExtender ID="Calender" runat="server" Format="dd/MMM/yyyy" 
                        TargetControlID="txtIDate" />
                 
                </td>
    
            </tr>
    
            <tr>
                                              
                                                <td align="center" colspan="6" >
                                                    <table style="padding-left: 8px">
                                                        <tr>
                                                           
                                                                         <td width="80px">
                                                                <asp:Button ID="btnSave" runat="server" Text="<%$ Resources:Attendance,Save %>" OnClick="btnSave_Click" Visible="false" CssClass="buttonCommman"
                                                                    ValidationGroup="a" />
                                                            </td>
                                                            <td width="80px">
                                                                <asp:Button ID="btnReset" runat="server" Text="<%$ Resources:Attendance,Reset %>" OnClick="btnReset_Click"  CssClass="buttonCommman"
                                                                    CausesValidation="False" />
                                                            </td>
                                                            <td width="80px">
                                                                <asp:Button ID="btnCancel" runat="server" Text="<%$ Resources:Attendance,Cancel %>" OnClick="btnCancel_Click" CssClass="buttonCommman"
                                                                     CausesValidation="False" />
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
                                                            <asp:ListItem Selected="True" Text="<%$ Resources:Attendance,Department Name %>" Value="StudentName"></asp:ListItem>
                                                                <asp:ListItem Text="<%$ Resources:Attendance,Department Name(Local) %>" Value="MotherName"></asp:ListItem>
                                                                <asp:ListItem Text="<%$ Resources:Attendance,Department Code %>" Value="FatherName"></asp:ListItem>
               
                                                                <asp:ListItem Text="<%$ Resources:Attendance,Parent Department Name %>" Value="Class">
                                                                
                                                                </asp:ListItem>                                           
                                                                <asp:ListItem Text="<%$ Resources:Attendance,Department Id %>" Value="Student_Id"></asp:ListItem>
                                                                <asp:ListItem Text="<%$ Resources:Attendance,Phone No. %>" Value="Mobile_No"></asp:ListItem>
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
                                        <asp:GridView ID="gvDepMasterBin" PageSize="<%# SystemParameter.GetPageSize() %>"
                                            runat="server" AutoGenerateColumns="False" Width="100%" AllowPaging="True" OnPageIndexChanging="gvDepMasterBin_PageIndexChanging"
                                            OnSorting="gvDepMasterBin_OnSorting" AllowSorting="true" CssClass="grid">
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
                                                    <asp:TemplateField HeaderText="Student Id" SortExpression="Student_Id">
                                                    <ItemTemplate>
                                        
                                                        <asp:Label ID="lblDepartmentId1"  runat="server" Text='<%# Eval("Student_Id") %>'></asp:Label>
                                                        
                                                        
                                                    </ItemTemplate>
                                                    <ItemStyle CssClass="grid" />
                                                </asp:TemplateField>
                                              
                                                  <asp:TemplateField HeaderText="Student Name" SortExpression="StudentName">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lbEDepName" runat="server" Text='<%# Eval("StudentName") %>'></asp:Label>
                                                         <asp:Label ID="lblStudentId" runat="server" Visible="false"  Text='<%# Eval("Student_Id") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle CssClass="grid" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="FatherName" SortExpression="FatherName">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblStudentName" runat="server" Text='<%# Eval("FatherName") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle CssClass="grid" />
                                                </asp:TemplateField>
                                            
                                                <asp:TemplateField HeaderText="MotherName" SortExpression="MotherName">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblMotherName" runat="server" Text='<%# Eval("MotherName") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle CssClass="grid" />
                                                </asp:TemplateField>
                                             <asp:TemplateField HeaderText="Class" SortExpression="ClassName">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblClass" runat="server" Text='<%# Eval("Class") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle CssClass="grid" />
                                                </asp:TemplateField>
                                               
                                                <asp:TemplateField HeaderText="Mobile No" SortExpression="Mobile_No">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblMobileNo" runat="server" Text='<%# Eval("Mobile_No") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle CssClass="grid" />
                                                </asp:TemplateField>
                                          <asp:TemplateField HeaderText="Address" SortExpression="Address">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblAddress" runat="server" Text='<%# Eval("Address") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle CssClass="grid" />
                                                </asp:TemplateField>
                                         <asp:TemplateField HeaderText="Detail" SortExpression="Detail">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblDetail" runat="server" Text='<%# Eval("Detail") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle CssClass="grid" />
                                                </asp:TemplateField>
                                      <asp:TemplateField HeaderText="Amount" SortExpression="Amount">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblAmount" runat="server" Text='<%# Eval("Amount") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle CssClass="grid" />
                                                </asp:TemplateField>
                                           <asp:TemplateField HeaderText="IDate" SortExpression="IDate">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblIDate" runat="server" Text='<%# Eval("Idate") %>'></asp:Label>
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



