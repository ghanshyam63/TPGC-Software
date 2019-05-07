<%@ Page Language="C#" MasterPageFile="~/ERPMaster.master" AutoEventWireup="true"
    CodeFile="DeviceOperation.aspx.cs" Inherits="Device_DeviceOperation" Title="Untitled Page" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
  <link href="../CSS/InvStyle.css" rel="stylesheet" type="text/css" />
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
                                                <asp:Label ID="lblHeader" runat="server" Text="<%$ Resources:Attendance,Device Operation Setup %>"
                                                    CssClass="LableHeaderTitle"></asp:Label>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                                <td align="right">
                                    
                                </td>
                            </tr>
                            <tr>
                                <td bgcolor="#ccddee"   width="100%" height="500px" valign="top">
                                 <asp:HiddenField ID="HDFSort" runat="server" />
                                <asp:Panel  ID="pnlList" runat="server" >
                                <asp:GridView ID="gvDevice"  DataKeyNames="Device_Id,Port,IP_Address,Device_Name" PageSize="<%# SystemParameter.GetPageSize() %>" runat="server"
                                            AutoGenerateColumns="False" Width="100%" AllowPaging="True" AllowSorting="True"
                                            CssClass="grid" OnPageIndexChanging="gvDevice_PageIndexChanging" OnSorting="gvDevice_OnSorting">
                                            <Columns>
                                            
                                            
                                             <asp:TemplateField>
                                                    <ItemStyle  />
                                                    <ItemTemplate>
                                                        <asp:ImageButton ID="LnkDeviceOp" runat="server" OnClick="LnkDeviceOp_Click" ImageUrl="~/Images/deviceopration.png" ToolTip="<%$ Resources:Attendance,Device Operation%>" />
                                                    </ItemTemplate>
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
                                <asp:Panel ID="pnlDeviceOp" runat="server" Height ="430px">
                            <table width="100%">
                                <tr>
                                    <td align="center" >
                                        <asp:Panel ID="pnlManage" runat="server" BorderColor="#CCCCCC">
                                            <table width="80%">
                                                <tr>
                                                    <td align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                                        <asp:LinkButton ID="lnkBackFromManage" CssClass="labelComman" runat="server" OnClick="lnkBackFromManage_Click"
                                                            Text="<%$ Resources:Attendance,Back%>"></asp:LinkButton>
                                                       
                                                    </td>
                                                    <td>
                                                       
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align='<%= Common.ChangeTDForDefaultLeft()%>' >
                                                        <asp:Label ID="Label2" runat="server" 
                                                            Text="<%$ Resources:Attendance,Device Id %>" CssClass="labelComman" Font-Bold="True"></asp:Label>
                                                        :
                                                        <asp:Label ID="lblDeviceId" CssClass="labelComman" runat="server"></asp:Label>
                                                    </td>
                                                    <td align='<%= Common.ChangeTDForDefaultLeft()%>' style="height: 23px">
                                                        <asp:Label ID="Label1" runat="server"  CssClass="labelComman"
                                                            Text="<%$ Resources:Attendance,Device Name %>" Font-Bold="True"></asp:Label>
                                                        :
                                                        <asp:Label ID="lblDeviceWithId" CssClass="labelComman" runat="server"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align='<%= Common.ChangeTDForDefaultLeft()%>' style="padding-left:11px;">
                                                      <asp:Button ID="btnSynctime" runat="server" OnClick="btnSynctime_Click" Text="<%$ Resources:Attendance,Sync Time%>"
                                                            Width="200px" CssClass="buttonCommman" />
                                                    </td>
                                                    <td align='<%= Common.ChangeTDForDefaultLeft()%>' style="padding-left:11px;">
                                                        <asp:Button ID="btnClearLog" runat="server" OnClick="btnClearLog_Click" Text="<%$ Resources:Attendance,Clear Log%>"
                                                            Width="200px" CssClass="buttonCommman" />
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        &nbsp;
                                                    </td>
                                                    <td>
                                                        &nbsp;
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align='<%= Common.ChangeTDForDefaultLeft()%>' style="padding-left:11px;">
                                                        <asp:Button ID="btnRestrat" runat="server" OnClick="btnRestrat_Click" Text="<%$ Resources:Attendance,Restart Device%>"
                                                            Width="200px" CssClass="buttonCommman" />
                                                    </td>
                                                    <td align='<%= Common.ChangeTDForDefaultLeft()%>' style="padding-left:11px;">
                                                        <asp:Button ID="btnPowerOff" runat="server" OnClick="btnPowerOff_Click" Text="<%$ Resources:Attendance,Power Off Device%>"
                                                            Width="200px" CssClass="buttonCommman" />
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        &nbsp;
                                                    </td>
                                                    <td>
                                                        &nbsp;
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align='<%= Common.ChangeTDForDefaultLeft()%>' style="padding-left:11px;">
                                                        <asp:Button ID="btnInitialize" runat="server" OnClick="btnInitialize_Click" Text="<%$ Resources:Attendance,Initialize Device%>"
                                                            Width="200px" CssClass="buttonCommman" />
                                                        <cc1:ConfirmButtonExtender ID="btnInitialize_ConfirmButtonExtender" runat="server"
                                                            ConfirmText="<%$ Resources:Attendance,It will delete all data wish to continue?%>"
                                                            Enabled="True" TargetControlID="btnInitialize">
                                                        </cc1:ConfirmButtonExtender>
                                                    </td>
                                                    <td align='<%= Common.ChangeTDForDefaultLeft()%>' style="padding-left:11px;">
                                                        <asp:Button ID="btnClearadmin" runat="server" OnClick="btnClearadmin_Click" Text="<%$ Resources:Attendance,Clear Admin Privilege%>"
                                                            Width="200px" CssClass="buttonCommman" />
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        &nbsp;
                                                    </td>
                                                    <td>
                                                        &nbsp;
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
