<%@ Page Language="C#" MasterPageFile="~/ERPMaster.master" AutoEventWireup="true" CodeFile="DeviceParameter.aspx.cs" Inherits="Device_DeviceParameter" Title="Untitled Page" %>

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
                                                <asp:Label ID="Label14" runat="server" Text="<%$ Resources:Attendance,Service Parameter Setup %>"
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
                              

<asp:Panel ID="PnlService" runat="server" >
<table style="padding-left:10px" width="100%">
<tr>
<td  align="center"  colspan="7">
 <asp:Label ID="lblHeader" runat="server" Text="<%$ Resources:Attendance,Service Parameter%>"
                         CssClass="labelComman"  Font-Size ="14px"    Font-Bold="True" ></asp:Label>
</td>
</tr>
<tr>
<td  width="210px" align='<%= Common.ChangeTDForDefaultLeft()%>'>
   <asp:Label ID="lblServiceInterval" runat="server" CssClass="labelComman" Text="<%$ Resources:Attendance,Service Interval%>" ></asp:Label>
                                              
</td>
<td>:</td>
<td   align='<%= Common.ChangeTDForDefaultLeft()%>'>
 <asp:TextBox ID="txtServiceInterval"  CssClass="textComman" runat="server" Width="50px" ></asp:TextBox><asp:Label ID="Label1" runat="server" CssClass="labelComman" Text="In Minutes" ></asp:Label>
</td>
</tr>

<tr>
<td align='<%= Common.ChangeTDForDefaultLeft()%>'>
   <asp:Label ID="Label8" runat="server" CssClass="labelComman" Text="<%$ Resources:Attendance,Service Log Path %>" ></asp:Label>
                                              
</td>
<td>:</td>
<td   align='<%= Common.ChangeTDForDefaultLeft()%>'>
 <asp:TextBox ID="txtServiceLogPath" CssClass="textComman" runat="server" Width="170px" ></asp:TextBox>
</td>
</tr>



<tr>
<td align='<%= Common.ChangeTDForDefaultLeft()%>'>
   <asp:Label ID="Label5" runat="server" CssClass="labelComman" Text="<%$ Resources:Attendance,Add New User %>"></asp:Label>
                                              
</td>
<td>:</td>
<td   align='<%= Common.ChangeTDForDefaultLeft()%>'>
 <asp:CheckBox ID="chkNewUser" CssClass="labelComman" runat="server" Text="<%$ Resources:Attendance,Add New User %>"
        AutoPostBack="True" oncheckedchanged="chkNewUser_CheckedChanged"   />
</td>


</tr>
<tr runat="server"  id="trdfval" >
<td  align='<%= Common.ChangeTDForDefaultLeft()%>'  >
   <asp:Label ID="Label6" runat="server" CssClass="labelComman" Text="<%$ Resources:Attendance,Default Values %>" ></asp:Label>
                                              
</td>
<td>:</td>
<td align='<%= Common.ChangeTDForDefaultLeft()%>' >   
 <asp:CheckBox ID="chkDefault" runat="server"  CssClass="labelComman" AutoPostBack="True" oncheckedchanged="chkDefault_CheckedChanged"  />
</td>




</tr>


<tr>
<td colspan="3">
<asp:Panel runat="server" ID="pnlAddNewUser">
<table  width="100%">


<tr runat="server" id="trDefault">
<td>
<table width="100%">



<tr>


<td align='<%= Common.ChangeTDForDefaultLeft()%>' width="205px"> 
<asp:Label ID="Label10" runat="server" CssClass="labelComman" Text="<%$ Resources:Attendance,Brand %>"></asp:Label>   </td>
<td >:</td>
<td    align='<%= Common.ChangeTDForDefaultLeft()%>' >
<asp:DropDownList ID="ddlBrand" Width="185px"  Height="21px"  runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlbrand_OnSelectedIndexChanged" CssClass="DropdownSearch" ></asp:DropDownList>
</td>
</tr>

<tr>

<td align='<%= Common.ChangeTDForDefaultLeft()%>' > 
<asp:Label ID="Label11" runat="server" CssClass="labelComman" Text="<%$ Resources:Attendance,Location %>"></asp:Label>   </td>
<td >:</td>
<td  align='<%= Common.ChangeTDForDefaultLeft()%>'>
<asp:DropDownList ID="ddlLocation"  Height="21px" OnSelectedIndexChanged="ddllocation_OnSelectedIndexChanged" AutoPostBack="true"  runat="server"  Width="185px" CssClass="DropdownSearch" ></asp:DropDownList>
<asp:HiddenField ID="hdSId" runat="server" Value="" />
</td>

</tr>

<tr>

<td align='<%= Common.ChangeTDForDefaultLeft()%>' > 
<asp:Label ID="Label13" runat="server" CssClass="labelComman" Text="<%$ Resources:Attendance,Department %>"></asp:Label>   </td>
<td >:</td>
<td   align='<%= Common.ChangeTDForDefaultLeft()%>'>
<asp:DropDownList ID="ddlDepartment"   runat="server"  Width="185px"  CssClass="DropdownSearch"  ></asp:DropDownList>

</td>

</tr>



</table>

</td>
</tr>



</table>
</asp:Panel>
</td>
</tr>


    <tr>
        <td   align="center" colspan="7">
        <asp:Button ID="BtnUpdate" Text="<%$ Resources:Attendance,Update %>"   CssClass="buttonCommman" runat="server" 
                onclick="BtnUpdate_Click" />
          
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

