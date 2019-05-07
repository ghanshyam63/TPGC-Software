<%@ Page Language="C#" MasterPageFile="~/ERPMaster.master" AutoEventWireup="true"
    CodeFile="PurchaseGoodsRec.aspx.cs" Inherits="Purchase_PurchaseGoodsRec" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <table width="100%">
                <tr>
                    <td>
                        <table width="100%" cellpadding="0" cellspacing="0" bordercolor="#F0F0F0">
                            <tr bgcolor="#90BDE9">
                                <td align="left">
                                    <table>
                                        <tr>
                                            <td>
                                                <img src="../Images/product_icon.png" width="31" height="30" alt="D" />
                                            </td>
                                            <td>
                                                <img src="../Images/seperater.png" width="2" height="43" alt="SS" />
                                            </td>
                                            <td style="padding-left: 5px">
                                                <asp:Label ID="lblHeader" runat="server" Text="<%$ Resources:Attendance,Purchase Goods Receive %>"
                                                    CssClass="LableHeaderTitle"></asp:Label>
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
                                                                <asp:ListItem Selected="True" Text="<%$ Resources:Attendance,Invoice No %>" Value="InvoiceNo"></asp:ListItem>
                                                                <asp:ListItem Text="<%$ Resources:Attendance,Invoice Date %>" Value="InvoiceDate"></asp:ListItem>
                                                                <asp:ListItem Text="<%$ Resources:Attendance,Supplier Invoice No %>" Value="SupInvoiceNo"></asp:ListItem>
                                                                <asp:ListItem Text="<%$ Resources:Attendance,Supplier Invoice Date %>" Value="SupInvoiceDate"></asp:ListItem>
                                                            </asp:DropDownList>
                                                        </td>
                                                        <td width="135px">
                                                            <asp:DropDownList ID="ddlOption" runat="server" CssClass="DropdownSearch" Height="25px"
                                                                Width="120px">
                                                                <asp:ListItem Text="<%$ Resources:Attendance,--Select-- %>"></asp:ListItem>
                                                                <asp:ListItem Text="<%$ Resources:Attendance,Equal %>"></asp:ListItem>
                                                                <asp:ListItem Text="<%$ Resources:Attendance,Like %>"></asp:ListItem>
                                                                <asp:ListItem Selected="True" Text="<%$ Resources:Attendance,Contains %>"></asp:ListItem>
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
                                            <asp:HiddenField ID="HdnEdit" runat="server" />
                                        </asp:Panel>
                                        <asp:GridView ID="GvPurchaseInvocie" PageSize="<%# SystemParameter.GetPageSize() %>"
                                            runat="server" AutoGenerateColumns="False" Width="100%" AllowPaging="True" AllowSorting="True"
                                            CssClass="grid" OnSorting="GvPurchaseInvocie_OnSorting" OnPageIndexChanging="GvPurchaseInvocie_PageIndexChanging">
                                            <Columns>
                                                <asp:TemplateField HeaderText="<%$ Resources:Attendance,Edit %>">
                                                    <ItemTemplate>
                                                        <asp:ImageButton ID="btnEdit" runat="server" CommandArgument='<%# Eval("TransId") %>'
                                                            ImageUrl="~/Images/edit.png" CausesValidation="False" OnCommand="btnEdit_Command"
                                                            ToolTip="<%$ Resources:Attendance,Edit %>" Visible="false" /></ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Center" />
                                                    <ItemStyle CssClass="grid" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="<%$ Resources:Attendance,Invoice No %>" SortExpression="InvoiceNo">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lbInvoiceNo" runat="server" Text='<%# Eval("InvoiceNo") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle CssClass="grid" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="<%$ Resources:Attendance,Invoice Date %>" SortExpression="InvoiceDate">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblInvoiceDate" runat="server" Text='<%# GetDateFormat(Eval("InvoiceDate").ToString()) %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle CssClass="grid" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="<%$ Resources:Attendance,Supplier Invoice No %>" SortExpression="SupInvoiceNo">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblSupInvoiceNo" runat="server" Text='<%# Eval("SupInvoiceNo") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle CssClass="grid" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="<%$ Resources:Attendance,Supplier Invoice Date %>"
                                                    SortExpression="SupInvoiceDate">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblSupInvoiceDate" runat="server" Text='<%# GetDateFormat(Eval("SupInvoiceDate").ToString()) %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle CssClass="grid" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="<%$ Resources:Attendance,Supplier Name %>" SortExpression="SupplierId">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblSupName" runat="server" Text='<%# GetSupplierName(Eval("SupplierId").ToString()) %>'></asp:Label>
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
                                    <asp:Panel ID="PnlProduct" runat="server">
                                        <asp:GridView ID="gvInvoice" PageSize="<%# SystemParameter.GetPageSize() %>" runat="server"
                                            AutoGenerateColumns="False" Width="100%" ShowHeader="false">
                                            <Columns>
                                                <asp:TemplateField>
                                                    <ItemTemplate>
                                                        <asp:Label ID="lbInvNo" runat="server" Font-Bold="true" Font-Size="14px" Text="<%$ Resources:Attendance,Invoice No %>"></asp:Label>
                                                        :
                                                        <asp:Label ID="lbPONo" runat="server" Font-Bold="true" Font-Size="14px" Text='<%# Eval("InvoiceNo") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle CssClass="grid" />
                                                </asp:TemplateField>
                                                <asp:TemplateField>
                                                    <ItemTemplate>
                                                        <asp:Label ID="lbInvDate" runat="server" Font-Bold="true" Font-Size="14px" Text="<%$ Resources:Attendance,Invoice Date %>"></asp:Label>
                                                        :
                                                        <asp:Label ID="lblPODate" runat="server" Font-Bold="true" Font-Size="14px" Text='<%# GetDateFormat(Eval("InvoiceDate").ToString()) %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle CssClass="grid" />
                                                </asp:TemplateField>
                                                <asp:TemplateField>
                                                    <ItemTemplate>
                                                        <asp:Label ID="lbSupInvoiceNo" runat="server" Font-Bold="true" Font-Size="14px" Text="<%$ Resources:Attendance,Supplier Invoice No %>"></asp:Label>
                                                        :
                                                        <asp:Label ID="lblDeliveryDate" runat="server" Font-Bold="true" Font-Size="14px"
                                                            Text='<%# Eval("SupInvoiceNo") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle CssClass="grid" />
                                                </asp:TemplateField>
                                                <asp:TemplateField>
                                                    <ItemTemplate>
                                                        <asp:Label ID="lbSupInvoiceDate" runat="server" Font-Bold="true" Font-Size="14px"
                                                            Text="<%$ Resources:Attendance,Supplier Invoice Date %>"></asp:Label>
                                                        :
                                                        <asp:Label ID="lblOrderType" runat="server" Font-Bold="true" Font-Size="14px" Text='<%# GetDateFormat(Eval("SupInvoiceDate").ToString()) %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle CssClass="grid" />
                                                </asp:TemplateField>
                                            </Columns>
                                            <AlternatingRowStyle CssClass="InvgridAltRow"></AlternatingRowStyle>
                                            <HeaderStyle CssClass="Invgridheader" />
                                            <PagerStyle CssClass="Invgridheader" />
                                            <RowStyle CssClass="Invgridrow" HorizontalAlign="Center"></RowStyle>
                                        </asp:GridView>
                                        <asp:GridView ID="GvProduct" runat="server" AutoGenerateColumns="False" Width="100%"
                                            CssClass="grid">
                                            <Columns>
                                                <asp:TemplateField HeaderText="<%$ Resources:Attendance,Product Name %>">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblProductId" runat="server" Text='<%# ProductName(Eval("ProductId").ToString()) %>'></asp:Label>
                                                        <asp:Label ID="lblTransID" Visible="false" runat="server" Text='<%# Eval("TransID") %>'></asp:Label>
                                                        <asp:HiddenField ID="hdnProductId" runat="server" Value='<%# Eval("ProductId") %>' />
                                                    </ItemTemplate>
                                                    <FooterStyle BorderStyle="None" />
                                                    <ItemStyle CssClass="grid" Width="50%" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="<%$ Resources:Attendance,Unit Name %>">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblUnit" runat="server" Text='<%# UnitName(Eval("UnitId").ToString()) %>'></asp:Label>
                                                        <asp:HiddenField ID="hdnUnitId" runat="server" Value='<%# Eval("UnitId") %>' />
                                                    </ItemTemplate>
                                                    <FooterStyle BorderStyle="None" />
                                                    <ItemStyle CssClass="grid" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="<%$ Resources:Attendance,Invoice Qty %>">
                                                    <ItemTemplate>
                                                        <asp:Label ID="QtyReceived" runat="server" Text='<%# Eval("InvoiceQty") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <FooterStyle BorderStyle="None" />
                                                    <ItemStyle CssClass="grid" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="<%$ Resources:Attendance,Total Received Qty %>">
                                                    <ItemTemplate>
                                                        <asp:Label ID="QtyField1" runat="server" Text='<%# GetTotQty(Eval("RecQty").ToString()) %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <FooterStyle BorderStyle="None" />
                                                    <ItemStyle CssClass="grid" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="<%$ Resources:Attendance,Received %>">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtRecQty" runat="server" CssClass="textCommanSearch" Width="80px"
                                                            Height="10px" Text="0" AutoPostBack="True" OnTextChanged="txtRecQty_TextChanged"></asp:TextBox>
                                                    </ItemTemplate>
                                                    <FooterStyle BorderStyle="None" />
                                                    <ItemStyle CssClass="grid" />
                                                </asp:TemplateField>
                                            </Columns>
                                            <AlternatingRowStyle CssClass="InvgridAltRow"></AlternatingRowStyle>
                                            <HeaderStyle CssClass="Invgridheader" />
                                            <FooterStyle CssClass="Invgridheader" />
                                            <PagerStyle CssClass="Invgridheader" />
                                            <RowStyle CssClass="Invgridrow" HorizontalAlign="Center"></RowStyle>
                                        </asp:GridView>
                                        <table width="100%">
                                            <tr>
                                                <td align="right">
                                                    <table>
                                                        <tr>
                                                            <td width="90px" align="left">
                                                                <asp:Button ID="btnSave" runat="server" Text="<%$ Resources:Attendance,Save %>" CssClass="buttonCommman"
                                                                    OnClick="btnSave_Click" Visible="false" />
                                                            </td>
                                                            <td>
                                                                <asp:Button ID="btnCancel" runat="server" Text="<%$ Resources:Attendance,Cancel %>"
                                                                    CssClass="buttonCommman" OnClick="btnCancel_Click" />
                                                            </td>
                                                        </tr>
                                                    </table>
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
            <asp:Panel ID="pnlProduct1" runat="server" class="MsgOverlayAddress" Visible="False">
                <asp:Panel ID="pnlProduct2" runat="server" class="MsgPopUpPanelAddress" Visible="False">
                    <asp:Panel ID="pnlProduct3" runat="server" Style="width: 100%; height: 100%; text-align: center;">
                        <table width="100%" style="background-image: url(../Images/bg_repeat.jpg); background-repeat: repeat;">
                            <tr>
                                <td align="<%= Common.ChangeTDForDefaultLeft()%>">
                                    <asp:Label ID="lblProductHeader" runat="server" Font-Size="14px" Font-Bold="true"
                                        CssClass="labelComman" Text="<%$ Resources:Attendance, Serial No %>"></asp:Label>
                                </td>
                                <td align="right">
                                    <asp:ImageButton ID="btnClosePanel" runat="server" ImageUrl="~/Images/close.png"
                                        CausesValidation="False" OnClick="btnClosePanel_Click" Height="20px" Width="20px" />
                                </td>
                            </tr>
                        </table>
                        <table width="100%" >
                            <tr>
                                <td>
                                    <asp:Panel ID="pnlSerial" runat="server" ScrollBars="Vertical" Width="300px" Height="252px">
                                        <asp:GridView ID="gvSerial" runat="server" AutoGenerateColumns="False" Width="100%"
                                            AllowSorting="True" CssClass="grid" ShowHeader="false">
                                            <Columns>
                                                <asp:TemplateField>
                                                    <ItemTemplate>
                                                        <%#Container.DataItemIndex+1 %>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="<%$ Resources:Attendance,S No. %>">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtSerialNo" runat="server" Text='<%#Eval("SerialNo")%>' AutoPostBack="true"
                                                            CssClass="textCommanSearch" Width="250px" Height="10px" OnTextChanged="txtSerialNo_TextChanged"></asp:TextBox>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                            <AlternatingRowStyle CssClass="InvgridAltRow"></AlternatingRowStyle>
                                            <HeaderStyle CssClass="Invgridheader" />
                                            <PagerStyle CssClass="Invgridheader" />
                                            <RowStyle CssClass="Invgridrow" HorizontalAlign="Center"></RowStyle>
                                        </asp:GridView>
                                    </asp:Panel>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <center>
                                        <asp:Button ID="BtnSerialSave" runat="server" Text="<%$ Resources:Attendance,Save %>"
                                            CssClass="buttonCommman" OnClick="BtnSerialSave_Click" />
                                    </center>
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                </asp:Panel>
            </asp:Panel>
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
