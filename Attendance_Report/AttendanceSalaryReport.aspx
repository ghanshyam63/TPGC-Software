<%@ Page Language="C#"    MasterPageFile="~/ERPMaster.master" AutoEventWireup="true"  CodeFile="AttendanceSalaryReport.aspx.cs" Inherits="AttendanceReports_AttendanceSalaryReport" Title="Untitled Page" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="DevExpress.XtraReports.v10.2.Web, Version=10.2.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.XtraReports.Web" TagPrefix="dx" %>
  
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">


        <link href="../CSS/InvStyle.css" rel="stylesheet" type="text/css" />
       
       
         <asp:UpdatePanel ID="UpdatePanel1" runat="server">
     
        <ContentTemplate>
            <table  width="100%">
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
                                                <asp:Label ID="lblHeader" runat="server" Text="<%$ Resources:Attendance,Attendance Salary Report  %>"
                                                    CssClass="LableHeaderTitle"></asp:Label>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                                
                            </tr>
                            <tr>
                                <td bgcolor="#ccddee" colspan="4" width="100%" height="500px" valign="top">



<asp:Panel ID="pnlEmpAtt" runat="server" >

<table width="100%">
 <tr>
                                                <td align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                                    <asp:RadioButton ID="rbtnGroupSal" CssClass="labelComman" OnCheckedChanged="EmpGroupSal_CheckedChanged"
                                                        runat="server" Text="<%$ Resources:Attendance,Group %>" Font-Bold="true" GroupName="EmpGroupSal"
                                                        AutoPostBack="true" />
                                                    <asp:RadioButton ID="rbtnEmpSal" runat="server" CssClass="labelComman" AutoPostBack="true"
                                                        Text="<%$ Resources:Attendance,Employee %>" GroupName="EmpGroupSal" Font-Bold="true"
                                                        OnCheckedChanged="EmpGroupSal_CheckedChanged" />
                                                        <asp:Label ID="lblEmp" runat="server" ></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                            <td>
                                            <asp:Panel ID="pnlGroupSal" runat="server">
                                                        <table width="100%">
                                                            <tr>
                                                                <td>
                                                                    <table width="100%">
                                                                        <tr>
                                                                            <td valign="top" align='<%= Common.ChangeTDForDefaultLeft()%>' width="170px">
                                                                                <asp:ListBox ID="lbxGroupSal" runat="server" Height="211px" Width="171px" SelectionMode="Multiple"
                                                                                    AutoPostBack="true" OnSelectedIndexChanged="lbxGroupSal_SelectedIndexChanged"
                                                                                    CssClass="list" Font-Names="Trebuchet MS" Font-Size="Small" ForeColor="Gray">
                                                                                </asp:ListBox>
                                                                            </td>
                                                                            <td valign="top" align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                                                                <asp:GridView ID="gvEmployeeSal" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                                                                                    OnPageIndexChanging="gvEmployeeSal_PageIndexChanging" CssClass="grid" Width="100%"
                                                                                    PageSize="<%# SystemParameter.GetPageSize() %>">
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
                                                                    <asp:Label ID="Label52" runat="server" Visible="false"></asp:Label>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </asp:Panel>
       
                                            
                                            <asp:Panel ID="pnlEmp" runat="server" DefaultButton="ImageButton8">


                                                        <div width="100%" style="background-image: url(../Images/bg_repeat.jpg); background-repeat: repeat;">
                                                            <table width="100%" style="padding-left: 20px; height: 38px;">
                                                                <tr>
                                                                    <td width="90px">
                                                                        <asp:Label ID="Label24" runat="server" CssClass="labelComman" Text="<%$ Resources:Attendance,Select Option %>"></asp:Label>
                                                                    </td>
                                                                    <td width="170px">
                                                                        <asp:DropDownList ID="ddlField" runat="server" CssClass="DropdownSearch" Height="25px"
                                                                            Width="165px">
                                                                            <asp:ListItem Text="<%$ Resources:Attendance,Employee Code %>" Value="Emp_Code"></asp:ListItem>
                                                                            <asp:ListItem Text="<%$ Resources:Attendance,Employee Name %>" Value="Emp_Name" Selected="True"></asp:ListItem>
                                                                        </asp:DropDownList>
                                                                    </td>
                                                                    <td width="125px">
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
                                                                            Width="100%" />
                                                                    </td>
                                                                    <td width="50px" align="center">
                                                                        <asp:ImageButton ID="ImageButton8" runat="server" CausesValidation="False" Height="25px"
                                                                            ImageUrl="~/Images/search.png" Width="25px" OnClick="btnarybind_Click1" ToolTip="<%$ Resources:Attendance,Search %>" />
                                                                    </td>
                                                                    <td width="30px" align="center">
                                                                        <asp:Panel ID="Panel15" runat="server" DefaultButton="ImageButton9">
                                                                            <asp:ImageButton ID="ImageButton9" runat="server" CausesValidation="False" Height="25px"
                                                                                ImageUrl="~/Images/refresh.png" Width="25px" OnClick="btnaryRefresh_Click1"
                                                                                ToolTip="<%$ Resources:Attendance,Refresh %>"></asp:ImageButton>
                                                                        </asp:Panel>
                                                                    </td>
                                                                    <td width="30px" align="center">
                                                                    </td>
                                                                    <td width="30px" align="center">
                                                                    </td>
                                                                    <td>
                                                                        <asp:ImageButton ID="ImageButton10" runat="server" OnClick="ImgbtnSelectAll_Clickary"
                                                                            ToolTip="<%$ Resources:Attendance, Select All %>" AutoPostBack="true" ImageUrl="~/Images/selectAll.png" />
                                                                    </td>
                                                                    <td align="center">
                                                                        <asp:Label ID="lblTotalRecord" runat="server" Text="<%$ Resources:Attendance,Total Records: 0 %>"
                                                                            CssClass="labelComman"></asp:Label>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </div>
                                                        <asp:Label ID="lblSelectRecord" runat="server" Visible="false"></asp:Label>
                                                        <asp:GridView ID="gvEmployee" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                                                            OnPageIndexChanging="gvEmp_PageIndexChanging" CssClass="grid" Width="100%"
                                                            DataKeyNames="Emp_Id" PageSize="<%# SystemParameter.GetPageSize() %>" Visible="true">
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
                                                                <%--<asp:TemplateField HeaderText="<%$ Resources:Attendance,Edit %>">
                                                                    <ItemTemplate>
                                                                        <asp:ImageButton ID="btnEdit" runat="server" CausesValidation="False" CommandArgument='<%# Eval("Emp_Id") %>'
                                                                            ImageUrl="~/Images/edit.png" Visible="true" OnCommand="btnEditary_Command"
                                                                            Width="16px" />
                                                                    </ItemTemplate>
                                                                    <ItemStyle HorizontalAlign="Center" />
                                                                </asp:TemplateField>--%>
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
                                                        
                                                        </asp:Panel>
                                                          
                                            <asp:Panel ID="Panel1" runat="server" DefaultButton="btnLogProcess">

                                                        
                                                        <table runat="server"  width="100%">
                                                        
                                                        
                                                  <tr>
                                                        <td align="<%= Common.ChangeTDForDefaultLeft()%>">
                                                           <asp:Label ID="Label1" runat="server" CssClass="labelComman" Text="<%$ Resources:Attendance,Month %>"></asp:Label>
                                                             
                                                        
                                                        </td>
                                                         <td align="<%= Common.ChangeTDForDefaultLeft()%>">
                                                                    :
                                                                </td>
                                                               <td align="<%= Common.ChangeTDForDefaultLeft()%>">
                                                               <asp:DropDownList ID="ddlMonth" runat="server" CssClass="DropdownSearch" TabIndex="5"
                                                                        Width="158px">
                                                                        <asp:ListItem Text="--Select--" Value="0" />
                                                                        <asp:ListItem Text="January" Value="1" />
                                                                        <asp:ListItem Text="February" Value="2" />
                                                                        <asp:ListItem Text="March" Value="3" />
                                                                        <asp:ListItem Text="April" Value="4" />
                                                                        <asp:ListItem Text="May" Value="5" />
                                                                        <asp:ListItem Text="June" Value="6" />
                                                                        <asp:ListItem Text="July" Value="7" />
                                                                        <asp:ListItem Text="August" Value="8" />
                                                                        <asp:ListItem Text="September" Value="9" />
                                                                        <asp:ListItem Text="October" Value="10" />
                                                                        <asp:ListItem Text="November" Value="11" />
                                                                        <asp:ListItem Text="December" Value="12" />
                                                                    </asp:DropDownList>
                                                               
                                                               </td> 
                                                                <td align="<%= Common.ChangeTDForDefaultLeft()%>">
                                                           <asp:Label ID="Label2" runat="server" CssClass="labelComman" Text="<%$ Resources:Attendance,Year %>"></asp:Label>
                                                             
                                                        
                                                        </td>
                                                         <td align="<%= Common.ChangeTDForDefaultLeft()%>">
                                                                    :
                                                                </td>
                                                               <td align="<%= Common.ChangeTDForDefaultLeft()%>">
                                                               
                                                                  <asp:TextBox  ID="txtYear" runat="server" Width="130px" CssClass="labelComman"></asp:TextBox>
                                   <cc1:FilteredTextBoxExtender ID="txtLastTime_FilteredTextBoxExtender" runat="server"
                                                        Enabled="True" TargetControlID="txtYear" ValidChars="1,2,3,4,5,6,7,8,9,0">
                                                    </cc1:FilteredTextBoxExtender>
                                                               </td> 
                                                        </tr>
                                                         
                                                         
                                                         
                                                            <tr>
                                                            <td colspan="6" align="center" >
                                                            <asp:Button ID="btnLogProcess"  runat="server" OnClick="btnGenerate_Click" Text="<%$ Resources:Attendance,Next %>"
                                                                    CssClass="buttonCommman" Visible="true" />
                                                                     &nbsp; &nbsp;
                                                                     <asp:Button ID="btnReset" runat="server" OnClick="btnReset_Click" Text="<%$ Resources:Attendance,Reset %>"
                                                                    CssClass="buttonCommman" Visible="true" />
                                                                    
                                                                     &nbsp; &nbsp;  
                                                           
                                                            </td>
                                                            
                                                            </tr>
                                                        </table>
                                                        </asp:Panel>
                                            
                                            
                                            </td>
                                            </tr>
                                            
                                            
                                            </table>
                                   
                                                        
                                                    </asp:Panel>
 
 <asp:Panel ID="pnlReport" runat="server" >
                                           
                                           
                                           <table width="100%">
                                <tr>
                                    <td style="width: 25%" >
                                        <asp:LinkButton ID="lnkback" runat="server" CssClass="acc" OnClick="lnkback_Click" Text="<%$ Resources:Attendance,Back %>"></asp:LinkButton>
                                    </td>
                                   
                                  
                                </tr>
                                    <tr>
                            <td>
                                <dx:ReportToolbar ID="rptToolBar" runat="server" ShowDefaultButtons="False" ReportViewer="<%# rptViewer %>"
                                    Width="100%" AccessibilityCompliant="True">
                                    <Items>
                                        <dx:ReportToolbarButton ItemKind="Search" />
                                        <dx:ReportToolbarSeparator />
                                        <dx:ReportToolbarButton ItemKind="PrintReport" />
                                        <dx:ReportToolbarButton ItemKind="PrintPage" />
                                        <dx:ReportToolbarSeparator />
                                        <dx:ReportToolbarButton Enabled="False" ItemKind="FirstPage" />
                                        <dx:ReportToolbarButton Enabled="False" ItemKind="PreviousPage" />
                                        <dx:ReportToolbarLabel ItemKind="PageLabel" />
                                        <dx:ReportToolbarComboBox ItemKind="PageNumber" Width="65px">
                                        </dx:ReportToolbarComboBox>
                                        <dx:ReportToolbarLabel ItemKind="OfLabel" />
                                        <dx:ReportToolbarTextBox IsReadOnly="True" ItemKind="PageCount" />
                                        <dx:ReportToolbarButton ItemKind="NextPage" />
                                        <dx:ReportToolbarButton ItemKind="LastPage" />
                                        <dx:ReportToolbarSeparator />
                                        <dx:ReportToolbarButton ItemKind="SaveToDisk" />
                                        <dx:ReportToolbarButton ItemKind="SaveToWindow" />
                                        <dx:ReportToolbarComboBox ItemKind="SaveFormat" Width="70px">
                                            <Elements>
                                                <dx:ListElement Value="pdf" />
                                                <dx:ListElement Value="xls" />
                                                <dx:ListElement Value="xlsx" />
                                                <dx:ListElement Value="rtf" />
                                                <dx:ListElement Value="mht" />
                                                <dx:ListElement Value="html" />
                                                <dx:ListElement Value="txt" />
                                                <dx:ListElement Value="csv" />
                                                <dx:ListElement Value="png" />
                                            </Elements>
                                        </dx:ReportToolbarComboBox>
                                    </Items>
                                    <Styles>
                                        <LabelStyle>
                                            <Margins MarginLeft="3px" MarginRight="3px" />
                                        </LabelStyle>
                                    </Styles>
                                </dx:ReportToolbar>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Panel ID="pnlrptviewer" runat="server" Width="100%" Height="100%">
                                    <dx:ReportViewer ID="rptViewer" runat="server" AutoSize="False" Width="98%" Height="500px">
                                    </dx:ReportViewer>
                                </asp:Panel>
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

