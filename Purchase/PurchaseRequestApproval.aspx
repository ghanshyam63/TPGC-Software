<%@ Page Language="C#" MasterPageFile="~/ERPMaster.master" AutoEventWireup="true"
    CodeFile="PurchaseRequestApproval.aspx.cs" Inherits="Purchase_PurchaseRequestApproval"
    Title="Untitled Page" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
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
                            <asp:Label ID="lblHeader" runat="server" Text="<%$ Resources:Attendance,Department Approval %>"
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
                                            <asp:ListItem Selected="True" Text="<%$ Resources:Attendance,Request No %>" Value="RequestNo"></asp:ListItem>
                                            <asp:ListItem Text="<%$ Resources:Attendance,Request Date %>" Value="RequestDate"></asp:ListItem>
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
                    </asp:Panel>
                    <asp:GridView ID="gvPurchaseRequest" PageSize="<%# SystemParameter.GetPageSize() %>"
                        runat="server" AutoGenerateColumns="False" Width="100%" AllowPaging="True" AllowSorting="True"
                        CssClass="grid" OnPageIndexChanging="gvPurchaseRequest_PageIndexChanging" OnSorting="gvPurchaseRequest_Sorting">
                        <Columns>
                            <asp:TemplateField HeaderText="<%$ Resources:Attendance,Detail %>">
                                <ItemTemplate>
                                    <asp:ImageButton ID="IbtnDetail" runat="server" CausesValidation="False" CommandArgument='<%# Eval("Trans_Id") %>'
                                        ImageUrl="~/Images/Detail.png" Width="20px" Height="20px" OnCommand="IbtnDetail_Command"
                                        ToolTip="<%$ Resources:Attendance,Detail %>" />
                                    <asp:ImageButton ID="IbtnBack" runat="server" CausesValidation="False" CommandArgument='<%# Eval("Trans_Id") %>'
                                        ImageUrl="~/Images/PullBlue.png" Visible="false" OnCommand="IbtnBack_Command"
                                        ToolTip="<%$ Resources:Attendance,Back %>" />
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />
                                <ItemStyle CssClass="grid" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="<%$ Resources:Attendance,Request No %>" SortExpression="RequestNo">
                                <ItemTemplate>
                                    <asp:Label ID="lblRequestNo" runat="server" Text='<%# Eval("RequestNo") %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle CssClass="grid" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="<%$ Resources:Attendance,Request Date %>" SortExpression="RequestDate">
                                <ItemTemplate>
                                    <asp:Label ID="lblRequestDate" runat="server" Text='<%# Convert.ToDateTime(Eval("RequestDate").ToString()).ToString("dd-MMM-yyyy") %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle CssClass="grid" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="<%$ Resources:Attendance,Expected Delivery Date %>"
                                SortExpression="ExpDelDate">
                                <ItemTemplate>
                                    <asp:Label ID="lblExpDelDate" runat="server" Text='<%# Convert.ToDateTime(Eval("ExpDelDate").ToString()).ToString("dd-MMM-yyyy") %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle CssClass="grid" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="<%$ Resources:Attendance,Approve %>">
                                <ItemTemplate>
                                    <asp:ImageButton ID="IbtnApprove" runat="server" CommandArgument='<%# Eval("Trans_Id") %>'
                                        ImageUrl="~/Images/approve.png" CausesValidation="False" OnCommand="btnApprove_Command"
                                        ToolTip="<%$ Resources:Attendance,Approve %>" /></ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />
                                <ItemStyle CssClass="grid" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="<%$ Resources:Attendance,Reject %>">
                                <ItemTemplate>
                                    <asp:ImageButton ID="IbtnReject" runat="server" CausesValidation="False" CommandArgument='<%# Eval("Trans_Id") %>'
                                        ImageUrl="~/Images/disapprove.png" OnCommand="IbtnReject_Command" ToolTip="<%$ Resources:Attendance,Reject %>" />
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />
                                <ItemStyle CssClass="grid" />
                            </asp:TemplateField>
                        </Columns>
                        <AlternatingRowStyle CssClass="InvgridAltRow"></AlternatingRowStyle>
                        <HeaderStyle CssClass="Invgridheader" />
                        <PagerStyle CssClass="Invgridheader" />
                        <RowStyle CssClass="Invgridrow" HorizontalAlign="Center"></RowStyle>
                    </asp:GridView>
                    <table width="100%" cellspacing="0" cellpadding="0">
                        <tr>
                            <td style="padding-left: 80px;">
                                <asp:Panel ID="pnlDetail" runat="server">
                                    <table width="90%" cellspacing="0" cellpadding="0">
                                        <tr>
                                            <td>
                                                <asp:GridView ID="gvProductRequest" PageSize="<%# SystemParameter.GetPageSize() %>"
                                                    runat="server" AutoGenerateColumns="False" Width="100%" CssClass="grid">
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="<%$ Resources:Attendance,Serial No %>">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblSerialNO" runat="server" Text='<%# Eval("Serial_No") %>'></asp:Label>
                                                            </ItemTemplate>
                                                            <ItemStyle CssClass="grid" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="<%$ Resources:Attendance,Product Name %>">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblProductId" runat="server" Text='<%# ProductName(Eval("Product_Id").ToString()) %>'></asp:Label>
                                                            </ItemTemplate>
                                                            <ItemStyle CssClass="grid" Width="200px" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="<%$ Resources:Attendance,Unit Name %>">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblUnit" runat="server" Text='<%# UnitName(Eval("UnitId").ToString()) %>'></asp:Label>
                                                            </ItemTemplate>
                                                            <ItemStyle CssClass="grid" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="<%$ Resources:Attendance,Quantity %>">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblReqQty" runat="server" Text='<%# Eval("ReqQty") %>'></asp:Label>
                                                            </ItemTemplate>
                                                            <ItemStyle CssClass="grid" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="<%$ Resources:Attendance,Description %>" ItemStyle-Width="50%">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblDescription" runat="server" Text='<%# Eval("ProductDescription") %>'></asp:Label>
                                                            </ItemTemplate>
                                                            <ItemStyle CssClass="grid" />
                                                        </asp:TemplateField>
                                                    </Columns>
                                                    <AlternatingRowStyle CssClass="InvgridAltRow"></AlternatingRowStyle>
                                                    <HeaderStyle CssClass="Invgridheader" />
                                                    <PagerStyle CssClass="Invgridheader" />
                                                    <RowStyle CssClass="Invgridrow" HorizontalAlign="Center"></RowStyle>
                                                </asp:GridView>
                                                <asp:Label ID="lblDescription" runat="server" Visible="false" CssClass="labelComman"
                                                    Font-Size="14px">
                                                </asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:TextBox ID="txtDescription" runat="server" ReadOnly="true" TextMode="MultiLine"
                                                    Visible="false" Height="35px" Width="99%" CssClass="textComman"></asp:TextBox>
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
