<%@ Page Language="C#" MasterPageFile="~/ERPMaster.master" AutoEventWireup="true"
    CodeFile="DownloadUser.aspx.cs" Inherits="Device_DownloadUser" Title="Untitled Page" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
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
                                                <asp:Label ID="lblHeader" runat="server" Text="<%$ Resources:Attendance,Download User Setup %>"
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
                                <asp:Panel  ID="pnlList" runat="server" >
                                <table width="100%">
                                
                                
                                
                                 <tr>
                                                <td align='<%= Common.ChangeTDForDefaultLeft()%>' style="width: 192px">
                                                    <asp:RadioButton ID="rbtnAll" runat="server"  CssClass="labelComman"  Checked="True" GroupName="userop" Text="<%$ Resources:Attendance,All User%>" />
                                                    &nbsp;<asp:RadioButton ID="rbtnNew" runat="server" CssClass="labelComman"   GroupName="userop" Text="<%$ Resources:Attendance,New User%>" />
                                                </td>
                                                <td align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                                    <asp:Button ID="btnDownload" runat="server" OnClick="btnDownload_Click" Text="<%$ Resources:Attendance,Download%>"
                                                        CssClass="buttonCommman" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align='<%= Common.ChangeTDForDefaultLeft()%>' >
                                                    &nbsp;
                                                </td>
                                                <td>
                                                    &nbsp;
                                                </td>
                                            </tr>
                                
                                <tr><td colspan="2">                                
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
                                <asp:Panel ID="pnlDeviceOp" runat="server" Height ="430px">
                            <table width="100%">
                                <tr>
                                    <td align="center" >
                                        <asp:Panel ID="pnlManage" runat="server" BorderColor="#CCCCCC">
                                            <table width="100%">
                                                <tr>
                                                    <td align="left">
                                                        <asp:LinkButton ID="lnkBackFromManage" CssClass="labelComman" runat="server" OnClick="lnkBackFromManage_Click"
                                                            Text="<%$ Resources:Attendance,Back%>"></asp:LinkButton>
                                                       
                                                    </td>
                                                    <td align='<%= Common.ChangeTDForDefaultLeft()%>' width="20%">
                                                    <asp:Label ID="Label8" CssClass="labelComman" runat="server" Text="<%$ Resources:Attendance,Download With%>"></asp:Label>
                                                </td>
                                                <td width="1%">
                                                    :
                                                </td>
                                                <td align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                                    <asp:CheckBox CssClass="labelComman" ID="chkFinger" runat="server" Text="<%$ Resources:Attendance,Finger%>" />
                                                    <asp:CheckBox CssClass="labelComman"  ID="chkFace" runat="server" Text="<%$ Resources:Attendance,Face%>" />
                                                    <asp:HiddenField ID="EditId" runat="server" />
                                                </td>
                                                    </tr>
                                                    
                                                    <tr>
                                                <td align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                                    <asp:Label ID="Label9" CssClass="labelComman" runat="server" Text="<%$ Resources:Attendance,User Count%>"></asp:Label>
                                                    <b>:</b><asp:Label  CssClass="labelComman"  ID="lblUserCount" runat="server"></asp:Label>
                                                </td>
                                                <td   style="padding-left:10px;" align="<%= Common.ChangeTDForDefaultLeft()%>" valign="middle" colspan="3">
                                                    <asp:Button ID="btnSaveSelected" Width="200px" runat="server" CssClass="buttonCommman" OnClick="btnSaveSelected_Click"
                                                        Text="<%$ Resources:Attendance,Save Selected Record%>" />
                                                    &nbsp;
                                                </td>
                                            </tr>   
                                                    <tr>
                                                    <td colspan="4">
                                                       <asp:GridView ID="gvUser" PageSize="50" runat="server" AutoGenerateColumns="False"
                                            DataKeyNames="sdwEnrollNumber,sName,iPrivilege,bEnabled,sCardNumber,Emp_Id,sPassword,IP,Port,Device_Id"
                                            Width="100%" AllowPaging="True" OnPageIndexChanging="gvUser_PageIndexChanging">
                                            <RowStyle CssClass="grid" />
                                            <Columns>
                                                <asp:TemplateField>
                                                    <ItemTemplate>
                                                        <asp:CheckBox ID="chkSel" runat="server" />
                                                    </ItemTemplate>
                                                    <HeaderTemplate>
                                                        <asp:CheckBox ID="chkSelAll" runat="server" AutoPostBack="True" OnCheckedChanged="chkSelAll_CheckedChanged" />
                                                    </HeaderTemplate>
                                                    <HeaderStyle HorizontalAlign="Left" />
                                                </asp:TemplateField>
                                                
                                                <asp:TemplateField HeaderText="<%$ Resources:Attendance,Enroll No.%>">
                                                    <ItemStyle Width="12%" />
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblEnrollNo" runat="server" Text='<%# Eval("Emp_Id") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                
                                                 <asp:TemplateField HeaderText="<%$ Resources:Attendance,Device Id %>">
                                                    <ItemStyle Width="12%" />
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblDevId" runat="server" Text='<%# Eval("Device_Id") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                 <asp:TemplateField HeaderText="<%$ Resources:Attendance,IP Address %>">
                                                    <ItemStyle Width="12%" />
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblIp" runat="server" Text='<%# Eval("IP") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                 <asp:TemplateField HeaderText="<%$ Resources:Attendance,Port No.%>">
                                                    <ItemStyle Width="12%" />
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblPort" runat="server" Text='<%# Eval("Port") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                
                                                <asp:TemplateField HeaderText="<%$ Resources:Attendance,Card No.%>">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblCardNo" runat="server" Text='<%# Eval("sCardNumber") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                               
                                                
                                                 <asp:TemplateField HeaderText="<%$ Resources:Attendance,Password %>" Visible="False">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblEmpId" runat="server" Text='<%# Eval("sPassword") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="<%$ Resources:Attendance,Name%>">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblEmpName" runat="server" Text='<%# Eval("sName") %>'></asp:Label>
                                                    </ItemTemplate>
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
