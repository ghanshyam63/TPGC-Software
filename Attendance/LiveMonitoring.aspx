<%@ Page Language="C#" MasterPageFile="~/ERPMaster.master" AutoEventWireup="true" CodeFile="LiveMonitoring.aspx.cs" Inherits="Attendance_LiveMonitoring" Title="Untitled Page" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

 <link href="../CSS/InvStyle.css" rel="stylesheet" type="text/css" />

    

    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
    <table width="100%" cellpadding="0" cellspacing="0" bordercolor="#F0F0F0">
                            <tr bgcolor="#90BDE9">
                                <td colspan="2" align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                    <table >
                                        <tr>
                                            <td>
                                                <img src="../Images/product_icon.png" width="31" height="30" alt="D" />
                                            </td>
                                            <td>
                                                <img src="../Images/seperater.png" width="2" height="43" alt="SS" />
                                            </td>
                                            <td style="padding-left: 5px">
                                                <asp:Label ID="lblHeader" runat="server" Text="<%$ Resources:Attendance,Log Monitoring Setup%>"
                                                    CssClass="LableHeaderTitle"></asp:Label>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                                
                            </tr>
                          
                           
                              <tr  >
                    <td style="padding-bottom:10px;padding-top:10px;" align='<%= Common.ChangeTDForDefaultLeft()%>' width="200px">
                        <asp:Label ID="lblSelectRecord" Font-Bold="true" runat="server" Text="<%$ Resources:Attendance,Select Records : %>"
                            Font-Names="Verdana" Font-Size="12px"></asp:Label>
                    </td>
                    <td style="padding-bottom:10px;padding-top:10px;" align='<%= Common.ChangeTDForDefaultLeft()%>'>
                        <asp:DropDownList ID="ddlSelectRecord" runat="server" Width="200px" Height="23px"
                            Font-Names="Verdana" Font-Size="12px" AutoPostBack="True" OnSelectedIndexChanged="ddlSelectRecord_SelectedIndexChanged">
                            <asp:ListItem Text="10" Value="10"></asp:ListItem>
                            <asp:ListItem Text="20" Value="20"></asp:ListItem>
                            <asp:ListItem Text="50" Value="50"></asp:ListItem>
                            <asp:ListItem Text="100" Value="100"></asp:ListItem>
                            <asp:ListItem Text="All" Value="all"></asp:ListItem>
                        </asp:DropDownList>
                    </td>
                </tr>
                           
                          
                           <tr>
                           <td colspan="2">
                           
                            <asp:Panel ID="pnlGrid" runat="server" ScrollBars="Auto"  Height="350px" >
                             <asp:GridView ID="gvTheGrid" CssClass="grid"    runat="server" Width="100%" AutoGenerateColumns="False">
                             
                                <Columns>
                                   <asp:TemplateField HeaderStyle-HorizontalAlign="Center"      HeaderText="<%$ Resources:Attendance,ID %> " HeaderStyle-Width="80"
                                        ItemStyle-Width="60"   >
                                        <ItemTemplate>
                                            <asp:Label ID="lblEmpId" runat="server" Text='<%# Eval("Emp_Code") %>' />
                                        </ItemTemplate>
                                        <ItemStyle Width="80px" HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderStyle-HorizontalAlign="Center"     HeaderText="<%$ Resources:Attendance,Employee Name %> " HeaderStyle-Width="80"
                                        ItemStyle-Width="60"   >
                                        <ItemTemplate>
                                            <asp:Label ID="lblEmpId" runat="server" Text='<%# Eval("Emp_Name") %>' />
                                        </ItemTemplate>
                                        <ItemStyle Width="80px" HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField     HeaderText="<%$ Resources:Attendance,Date %> " HeaderStyle-Width="50"
                                        ItemStyle-Width="50">
                                        <ItemTemplate>
                                            <asp:Label ID="lblDate" runat="server" Text='<%# GetDate(Eval("Event_Date")) %>' />
                                        </ItemTemplate>
                                        <ItemStyle Width="50px" HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField     HeaderText="<%$ Resources:Attendance,Time %> " HeaderStyle-Width="30"
                                        ItemStyle-Width="50">
                                        <ItemTemplate>
                                            <asp:Label ID="lblTime" runat="server" Text='<%# Convert.ToDateTime(Eval("Event_Time")).ToString("HH:mm") %>' />
                                        </ItemTemplate>
                                        <ItemStyle Width="30px" HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    
                                    <asp:TemplateField     HeaderText="<%$ Resources:Attendance,IP Address %> " HeaderStyle-Width="50"
                                        ItemStyle-Width="50">
                                        <ItemTemplate>
                                            <asp:Label ID="lblDeviceNoG" runat="server" Text='<%# Eval("IP_Address") %>' />
                                        </ItemTemplate>
                                        <ItemStyle Width="50px" HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    
                                     <asp:TemplateField HeaderText="<%$ Resources:Attendance,Device Name %> " HeaderStyle-Width="60"
                                        >
                                        <ItemTemplate>
                                            <asp:Label ID="lblDeviceG" runat="server" Text='<%# Eval("Device_Name") %>' />
                                        </ItemTemplate>
                                        <ItemStyle Width="50px" HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    
                                    
                                    
                                    
                                    
                                   
                                  
                                </Columns>
                                
                                
                                  <AlternatingRowStyle CssClass="InvgridAltRow"></AlternatingRowStyle>
                                            <HeaderStyle CssClass="Invgridheader" />
                                            <PagerStyle CssClass="Invgridheader" />
                                            <RowStyle CssClass="Invgridrow" HorizontalAlign="Center"></RowStyle>
                              
                            </asp:GridView>
                            </asp:Panel>
                           </div>
                               <asp:Timer ID="Timer1" runat="server" Interval="6000" ontick="Timer1_Tick">
                               </asp:Timer>
                           
                           </td>
                           </tr>
                           </table>
                           </ContentTemplate>
                           </asp:UpdatePanel>
                           
                           
                           
                           </asp:Content>

