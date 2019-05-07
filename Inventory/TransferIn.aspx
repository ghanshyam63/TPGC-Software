<%@ Page Title="" Language="C#" MasterPageFile="~/ERPMaster.master" AutoEventWireup="true" CodeFile="TransferIn.aspx.cs" Inherits="Inventory_TransferIn" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
  
    <asp:UpdatePanel ID="upallpage" runat="server">
        <Triggers>
            <asp:PostBackTrigger ControlID="btnList" />
            <asp:PostBackTrigger ControlID="btnNew" />
            <asp:PostBackTrigger ControlID="btnSave" />
            
        </Triggers>
        <ContentTemplate>
            <table width="100%">
                <tr>
                    <td align='<%= Common.ChangeTDForDefaultLeft()%>'>
                        <table width="100%" cellpadding="0" cellspacing="0" bordercolor="#F0F0F0">
                            <tr bgcolor="#90BDE9">
                                <td>
                                    <table>
                                        <tr>
                                            <td>
                                                <img src="../Images/product_icon.png" width="31" height="30" alt="D" />
                                            </td>
                                            <td>
                                                <img src="../Images/seperater.png" width="2" height="43" alt="SS" />
                                            </td>
                                            <td style="padding-left: 5px">
                                                <asp:Label ID="lblHeader" runat="server" Text="<%$ Resources:Attendance,Transfer In %>"
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
                                                        padding-top: 3px; background-image: url('../Images/List.png' ); background-repeat: no-repeat;
                                                        height: 49px; background-position: 5px 15px; font: bold 14px Trebuchet MS; color: #000000;" />
                                                </asp:Panel>
                                            </td>
                                            <td>
                                                <asp:Panel ID="pnlMenuNew" runat="server" CssClass="a">
                                                    <asp:Button ID="btnNew" runat="server" Text="<%$ Resources:Attendance,New %>" Width="90px"
                                                        BorderStyle="none" BackColor="Transparent" OnClick="btnNew_Click" Style="padding-left: 10px;
                                                        padding-top: 3px; background-image: url('../Images/New.png' ); background-repeat: no-repeat;
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
                                                                <asp:ListItem Text="<%$ Resources:Attendance, Voucher No. %>" Value="VoucherNo" Selected="True"></asp:ListItem>
                                                                <asp:ListItem Text="<%$ Resources:Attendance, Transfer Date %>" Value="TDate"></asp:ListItem>
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
                                                        <td width="270px">
                                                            <asp:TextBox ID="txtValue" runat="server" CssClass="textCommanSearch" Height="14px"
                                                                Width="250px"></asp:TextBox>
                                                        </td>
                                                        <td width="50px" align="center">
                                                            <asp:ImageButton ID="btnbind" runat="server" CausesValidation="False" Height="25px"
                                                                ImageUrl="~/Images/search.png" OnClick="btnbindrpt_Click" Width="25px" ToolTip="<%$ Resources:Attendance,Search %>">
                                                            </asp:ImageButton>
                                                        </td>
                                                        <td>
                                                            <asp:Panel ID="PnlRefresh" runat="server" DefaultButton="btnRefresh">
                                                                <asp:ImageButton ID="btnRefresh" runat="server" CausesValidation="False" Height="25px"
                                                                    ImageUrl="~/Images/refresh.png" OnClick="btnRefreshReport_Click" Width="25px"
                                                                    ToolTip="<%$ Resources:Attendance,Refresh %>"></asp:ImageButton>
                                                            </asp:Panel>
                                                        </td>
                                                        <td align="center">
                                                            <asp:Label ID="lblTotalRecords" runat="server" Text="<%$ Resources:Attendance,Total Records: 0 %>"
                                                                CssClass="labelComman"></asp:Label>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </div>
                                        </asp:Panel>
                                        <asp:GridView ID="GvTransfer" PageSize="<%# SystemParameter.GetPageSize() %>" runat="server"
                                            AutoGenerateColumns="False" Width="100%" AllowPaging="True" AllowSorting="True"
                                            OnPageIndexChanging="GvTransfer_PageIndexChanging" OnSorting="GvTransfer_Sorting"
                                            CssClass="grid">
                                            <Columns>
                                                <asp:TemplateField HeaderText="<%$ Resources:Attendance,Edit %>">
                                                    <ItemTemplate>
                                                        <asp:ImageButton ID="btnEdit" runat="server" CommandArgument='<%# Eval("Trans_Id") %>'
                                                            ImageUrl="~/Images/edit.png" CausesValidation="False" OnCommand="btnEdit_Command"
                                                            ToolTip="<%$ Resources:Attendance,Edit %>" Visible="false" /></ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Center" />
                                                    <ItemStyle CssClass="grid" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="<%$ Resources:Attendance,Voucher No. %>" SortExpression="VoucherNo">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblVocherNo" runat="server" Text='<%# Eval("VoucherNo") %>'></asp:Label>
                                                        <asp:Label ID="lblTransId" Visible="false" runat="server" Text='<%# Eval("Trans_Id") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle CssClass="grid" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="<%$ Resources:Attendance,Transfer Date %>" SortExpression="TDate">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblTransDate" runat="server" Text='<%# Convert.ToDateTime(Eval("TDate").ToString()).ToString("dd-MMM-yyyy") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle CssClass="grid" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="<%$ Resources:Attendance,Request No %>" SortExpression="Request_No">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblLocation_Name" runat="server" Text='<%# Eval("Request_No") %>'></asp:Label>
                                                        <asp:Label ID="lblLocationId" Visible="false" runat="server" Text='<%# Eval("ToLocationID") %>'></asp:Label></ItemTemplate>
                                                    <ItemStyle CssClass="grid" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="<%$ Resources:Attendance,Request Date %>">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblRequestDate" runat="server" Text='<%# Convert.ToDateTime(Eval("Request_Date").ToString()).ToString("dd-MMM-yyyy") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle CssClass="grid" />
                                                </asp:TemplateField>
                                                 <asp:TemplateField HeaderText="<%$ Resources:Attendance,RequestOut Date %>">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblRequestOutDate" runat="server" Text='<%# Convert.ToDateTime(Eval("OutDate").ToString()).ToString("dd-MMM-yyyy") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle CssClass="grid" />
                                                </asp:TemplateField>
                                           
                                            </Columns>
                                            <AlternatingRowStyle CssClass="InvgridAltRow"></AlternatingRowStyle>
                                            <HeaderStyle CssClass="Invgridheader" />
                                            <PagerStyle CssClass="Invgridheader" />
                                            <RowStyle CssClass="Invgridrow" HorizontalAlign="Center"></RowStyle>
                                        </asp:GridView>
                                        <asp:HiddenField ID="hdnSort" runat="server" />
                                    </asp:Panel>
                                    <asp:Panel ID="PnlNewEdit" runat="server" DefaultButton="btnSave">
                                        <table width="100%" style="padding-left: 43px">
                                            <tr>
                                                <td align='<%= Common.ChangeTDForDefaultLeft()%>' width="150px">
                                                    <asp:Label ID="lblTransferDate" runat="server" CssClass="labelComman" Text="<%$ Resources:Attendance,Transfer Date %>"></asp:Label>
                                                </td>
                                                <td align="center">
                                                    :
                                                </td>
                                                <td align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                                    <asp:TextBox ID="txtTransferDate" runat="server" CssClass="textComman" 
                                                        AutoPostBack="True" ontextchanged="txtTransferDate_TextChanged" />
                                                    <cc1:CalendarExtender ID="Calender" runat="server" TargetControlID="txtTransferDate"
                                                        Format="dd/MMM/yyyy" />
                                                </td>
                                                <td align='<%= Common.ChangeTDForDefaultLeft()%>' width="150px">
                                                    <asp:Label ID="lblVoucherNo" runat="server" CssClass="labelComman" Text="<%$ Resources:Attendance,Voucher No. %>"></asp:Label>
                                                </td>
                                                <td align="center">
                                                    :
                                                </td>
                                                <td align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                                    <asp:TextBox ID="txtVoucherNo" runat="server" CssClass="textComman" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="6" align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                                    <asp:Panel ID="pnlTrans" runat="server" Visible="false">
                                                        <table width="100%">
                                                            <tr>
                                                                <td align='<%= Common.ChangeTDForDefaultLeft()%>' width="145px">
                                                                    <asp:Label ID="lblTransReqDate" runat="server" CssClass="labelComman" Text="<%$ Resources:Attendance,Transfer Request Date %>" />
                                                                </td>
                                                                <td align="center">
                                                                    :
                                                                </td>
                                                                <td align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                                                    <asp:TextBox ID="txtTransReqDate" runat="server" CssClass="textComman" ReadOnly="True" />
                                                                    <cc1:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtTransReqDate"
                                                                        Format="dd/MMM/yyyy" />
                                                                </td>
                                                                <td align='<%= Common.ChangeTDForDefaultLeft()%>' width="150px">
                                                                    <asp:Label ID="lblTransReqNo" runat="server" CssClass="labelComman" Text="<%$ Resources:Attendance,Transfer Request No.%>" />
                                                                </td>
                                                                <td align="center">
                                                                    :
                                                                </td>
                                                                <td align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                                                    <asp:TextBox ID="txtTransNo" runat="server" CssClass="textComman" ReadOnly="True" />
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                                                    <asp:Label ID="lblLocationName" runat="server" CssClass="labelComman" Text="<%$ Resources:Attendance,From Location %>"></asp:Label>
                                                                </td>
                                                                <td width="1px">
                                                                    :
                                                                </td>
                                                                <td align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                                                    <asp:TextBox ID="txtLocationName" Width="250px" runat="server" CssClass="textComman"></asp:TextBox>
                                                                </td>
                                                                <td align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                                                <asp:CheckBox ID="chkPost" runat="server" CssClass="labelComman" Text="<%$ Resources:Attendance,Post %>" />
                                                                        </td>
                                                            </tr>
                                                        </table>
                                                    </asp:Panel>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="6" align="center">
                                                    <asp:Panel ID="pnlProduct" runat="server">
                                                        <asp:GridView ID="gvEditProduct" runat="server" Width="100%" CssClass="grid" AutoGenerateColumns="False">
                                                            <AlternatingRowStyle CssClass="InvgridAltRow" />
                                                            <Columns>
                                                            
                                                                <asp:TemplateField HeaderText="<%$ Resources:Attendance,Serial No %>">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblSerialNO" runat="server" Text='<%# Eval("Serial_No") %>'></asp:Label>
                                                                        <asp:Label ID="lblTransId" Visible="false" runat="server" Text='<%# Eval("Trans_Id") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                    <ItemStyle CssClass="grid" />
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="<%$ Resources:Attendance,Product Name %>">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblProductId" runat="server" Text='<%# ProductName(Eval("ProductId").ToString()) %>'></asp:Label>
                                                                        <asp:Label ID="lblPId" runat="server" Visible="false" Text='<%# Eval("ProductId").ToString() %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                    <ItemStyle CssClass="grid" />
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="<%$ Resources:Attendance,Unit Name %>">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblUnit" runat="server" Text='<%# UnitName(Eval("Unit_Id").ToString()) %>'></asp:Label>
                                                                        <asp:Label ID="lblUnitId" runat="server" Visible="false" Text='<%# Eval("Unit_Id").ToString() %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                    <ItemStyle CssClass="grid" />
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="<%$ Resources:Attendance,Unit Cost %>">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblunitcost" runat="server" Text='<%# Eval("UnitCost") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                    <ItemStyle CssClass="grid" />
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="<%$ Resources:Attendance,Request Quantity %>">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblReqQty" runat="server" Text='<%# Eval("RequestQty") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                    <ItemStyle CssClass="grid" />
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="<%$ Resources:Attendance,Out Quantity %>">
                                                                    <ItemTemplate>
                                                                              <asp:Label ID="lbloutqty" runat="server"  Text='<%# Eval("OutQty").ToString() %>'></asp:Label>
                                                                    
                                                                    </ItemTemplate>
                                                                    <ItemStyle CssClass="grid" />
                                                                </asp:TemplateField>
                                                                 <asp:TemplateField HeaderText="<%$ Resources:Attendance,In Quantity %>">
                                                                    <ItemTemplate>
                                                                        <asp:TextBox ID="txtInQty" runat="server" AutoPostBack="true" Width="80px" Height="10px" CssClass="textCommanSearch"
                                                                           OnTextChanged="txtOutQty_TextChanged" Text='<%# Eval("ReceivedQty").ToString() %>'></asp:TextBox>
                                                                          
                                                                    </ItemTemplate>
                                                                    <ItemStyle CssClass="grid" />
                                                                </asp:TemplateField>
                                                           
                                                            </Columns>
                                                            <HeaderStyle CssClass="Invgridheader" />
                                                            <PagerStyle CssClass="Invgridheader" />
                                                            <RowStyle CssClass="Invgridrow" HorizontalAlign="Center" />
                                                        </asp:GridView>
                                                    </asp:Panel>
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
                                                                <asp:Button ID="BtnReset" runat="server" Text="<%$ Resources:Attendance,Reset %>"
                                                                    CssClass="buttonCommman" CausesValidation="False" OnClick="BtnReset_Click" />
                                                            </td>
                                                            <td width="90px">
                                                                <asp:Button ID="btnPICancel" runat="server" CssClass="buttonCommman" Text="<%$ Resources:Attendance,Cancel %>"
                                                                    CausesValidation="False" OnClick="btnCancel_Click" />
                                                            </td>
                                                        </tr>
                                                    </table>
                                                    <asp:HiddenField ID="editid" runat="server" />
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

