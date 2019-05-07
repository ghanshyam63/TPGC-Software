<%@ Page Language="C#" MasterPageFile="~/ERPMaster.master" AutoEventWireup="true"
    CodeFile="Home.aspx.cs" Inherits="MasterSetUp_Home" Title="Untitled Page" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <link href="../CSS/InvStyle.css" rel="stylesheet" type="text/css" />
     
    <asp:Panel ID="pnl1" runat="server" class="MsgOverlayAddress" Visible="False">
        <asp:Panel ID="pnl2" runat="server" class="MsgPopUpPanelAddress" Visible="False">
            <asp:Panel ID="pnl3" DefaultButton="btnSave" runat="server" Style="width: 100%;
                height: 100%; text-align: center;">
                <table width="100%" style="background-image: url(../Images/bg_repeat.jpg); background-repeat: repeat;">
                    <tr>
                        <td align="<%= Common.ChangeTDForDefaultLeft()%>">
                            <asp:Label ID="lblAddressHeader" runat="server" Font-Size="14px" Font-Bold="true"
                                CssClass="labelComman" Text="<%$ Resources:Attendance,Location Setup %>"></asp:Label>
                        </td>
                        <td align="right">
                        
                        </td>
                    </tr>
                </table>
                <table width="100%" style="padding-left: 43px">
                    <tr>
                        <td align='<%= Common.ChangeTDForDefaultLeft()%>'>
                            <asp:Label ID="lblCompany" runat="server" CssClass="labelComman" Text="<%$ Resources:Attendance,Company %>"></asp:Label>
                        </td>
                        <td align="center">
                            :
                        </td>
                        <td align='<%= Common.ChangeTDForDefaultLeft()%>'>
                            <asp:DropDownList ID="ddlCompany" runat="server" CssClass="textComman" OnSelectedIndexChanged="ddlCompany_SelectedIndexChanged"
                                AutoPostBack="true" />
                        </td>
                    </tr>
                    <tr>
                        <td align='<%= Common.ChangeTDForDefaultLeft()%>'>
                            <asp:Label ID="lblBrand" runat="server" CssClass="labelComman" Text="<%$ Resources:Attendance,Brand %>" />
                        </td>
                        <td align="center">
                            :
                        </td>
                        <td align='<%= Common.ChangeTDForDefaultLeft()%>'>
                            <asp:DropDownList ID="ddlBrand" runat="server" CssClass="textComman" OnSelectedIndexChanged="ddlBrand_SelectedIndexChanged"
                                AutoPostBack="true" />
                        </td>
                    </tr>
                    <tr>
                        <td align='<%= Common.ChangeTDForDefaultLeft()%>'>
                            <asp:Label ID="lblLocation" runat="server" CssClass="labelComman" Text="<%$ Resources:Attendance,Location %>" />
                        </td>
                        <td align="center">
                            :
                        </td>
                        <td align='<%= Common.ChangeTDForDefaultLeft()%>'>
                            <asp:DropDownList ID="ddlLocation" runat="server" CssClass="textComman" OnSelectedIndexChanged="ddlLocation_SelectedIndexChanged"
                                AutoPostBack="true" />
                        </td>
                    </tr>
                     <tr>
                        <td align='<%= Common.ChangeTDForDefaultLeft()%>'>
                            <asp:Label ID="Label1" runat="server" CssClass="labelComman" Text="<%$ Resources:Attendance,Language %>" />
                        </td>
                        <td align="center">
                            :
                        </td>
                        <td align='<%= Common.ChangeTDForDefaultLeft()%>'>
                          <asp:DropDownList ID="ddlLanguage" runat="server" AutoPostBack="True" CssClass="textComman" OnTextChanged="ddlLanguage_SelectedIndexChanged">
                                            <asp:ListItem Value="1">English</asp:ListItem>
                                            <asp:ListItem Value="2">Arabic</asp:ListItem>
                                        </asp:DropDownList>
                        </td>
                    </tr>
                    
                    
                    
                      
                    
                    
                    <tr>
                        <td colspan="6" align="center" style="padding-left: 10px">
                            <table>
                                <tr>
                                    <td width="90px">
                                        <asp:Button ID="btnSave" runat="server" Text="<%$ Resources:Attendance,Save %>" CssClass="buttonCommman"
                                            OnClick="btnSave_Click" />
                                    </td>
                                    <td width="90px">
                                    </td>
                                </tr>
                            </table>
                            <asp:HiddenField ID="HiddenField1" runat="server" />
                        </td>
                    </tr>
                </table>
            </asp:Panel>
        </asp:Panel>
    </asp:Panel>
   
</asp:Content>
