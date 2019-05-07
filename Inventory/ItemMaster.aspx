<%@ Page Language="C#" MasterPageFile="~/ERPMaster.master" AutoEventWireup="true"
    CodeFile="ItemMaster.aspx.cs" Inherits="Inventory_ItemMaster" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit.HTMLEditor"
    TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">
        <Triggers>
            <%--<asp:PostBackTrigger ControlID="btnloadimg" />
            
            <asp:PostBackTrigger ControlID="gvProduct" />--%>
      
            <asp:PostBackTrigger ControlID="btnList" />
            <asp:PostBackTrigger ControlID="btnNew" />
            <asp:PostBackTrigger ControlID="btnBin" />
             <asp:PostBackTrigger ControlID="dtlistProduct" />
             </Triggers>
        <ContentTemplate>
            <table width="100%">
                <tr align='<%= Common.ChangeTDForDefaultLeft()%>'>
                    <td>
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
                                                <asp:Label ID="lblHeader" runat="server" Text="<%$ Resources:Attendance,Product Setup %>"
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
                                            <td>
                                                <asp:Panel ID="pnlMenuBin" runat="server" CssClass="a" Width="100%">
                                                    <asp:Button ID="btnBin" runat="server" Text="<%$ Resources:Attendance,Bin %>" Width="90px"
                                                        BorderStyle="none" BackColor="Transparent" OnClick="btnBin_Click" Style="padding-left: 10px;
                                                        padding-top: 3px; background-image: url('../Images/Bin.png' ); background-repeat: no-repeat;
                                                        height: 49px; background-position: 5px 15px; font: bold 14px Trebuchet MS; color: #000000;" />
                                                </asp:Panel>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td bgcolor="#ccddee" colspan="4" width="100%" height="500px" valign="top">
                                    <asp:Panel ID="pnlList" runat="server" Visible="false">
                                        <asp:Panel ID="pnlFilterRecords" runat="server" DefaultButton="btngo">
                                            <table width="100%" style="padding-left: 43px">
                                                <tr>
                                                    <td align='<%= Common.ChangeTDForDefaultLeft()%>' width="150px">
                                                        <asp:Label ID="lblBrandsearch" runat="server" Text="<%$ Resources:Attendance,Manufacturing Brand %>"
                                                            CssClass="labelComman"></asp:Label>
                                                    </td>
                                                    <td width="300px">
                                                        <asp:DropDownList ID="ddlbrandsearch" runat="server" CssClass="textComman" Width="270px">
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td align='<%= Common.ChangeTDForDefaultLeft()%>' width="120px">
                                                        <asp:Label ID="lbllocationsearch" runat="server" Text="<%$ Resources:Attendance,Category %>"
                                                            CssClass="labelComman" />
                                                    </td>
                                                    <td>
                                                        <asp:DropDownList ID="ddlcategorysearch" runat="server" CssClass="textComman" Width="270px">
                                                        </asp:DropDownList>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td colspan="3">
                                                    </td>
                                                    <td align="right" style="padding-right: 95px">
                                                        <table>
                                                            <tr>
                                                                <td style="padding-right: 20px">
                                                                    <asp:Button ID="btngo" runat="server" CausesValidation="False" Text="<%$ Resources:Attendance,Go %>"
                                                                        CssClass="buttonCommman" OnClick="btngo_Click" Height="25px" />
                                                                </td>
                                                                <td>
                                                                    <asp:Panel ID="Panel5" runat="server" DefaultButton="btnResetSreach">
                                                                        <asp:Button ID="btnResetSreach" runat="server" CausesValidation="False" Text="<%$ Resources:Attendance,Reset %>"
                                                                            CssClass="buttonCommman" OnClick="btnResetSreach_Click" Height="25px" />
                                                                    </asp:Panel>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                            </table>
                                        </asp:Panel>
                                        <asp:Panel ID="pnlSearchRecords" runat="server" DefaultButton="btnbind">
                                            <div width="100%" style="background-image: url(../Images/bg_repeat.jpg); background-repeat: repeat;">
                                                <table width="100%" style="padding-left: 20px; height: 38px">
                                                    <tr>
                                                        <td width="90px">
                                                            <asp:Label ID="lblSelectOption" runat="server" CssClass="labelComman" Text="<%$ Resources:Attendance,Select Option %>"></asp:Label>
                                                        </td>
                                                        <td width="180px">
                                                            <asp:DropDownList ID="ddlFieldName" runat="server" CssClass="DropdownSearch" Height="25px"
                                                                Width="170px">
                                                                <asp:ListItem Text="<%$ Resources:Attendance,Product Id %>" Value="ProductId"></asp:ListItem>
                                                                <asp:ListItem Text="<%$ Resources:Attendance,Product Name %>" Value="EProductName"
                                                                    Selected="True"></asp:ListItem>
                                                                <asp:ListItem Text="<%$ Resources:Attendance,Model No. %>" Value="ModelNo"></asp:ListItem>
                                                                <asp:ListItem Text="<%$ Resources:Attendance,Item Type %>" Value="ItemType"></asp:ListItem>
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
                                                                Width="100%" />
                                                        </td>
                                                        <td width="50px" align="center">
                                                            <asp:ImageButton ID="btnbind" runat="server" CausesValidation="False" Height="25px"
                                                                ImageUrl="~/Images/search.png" Width="25px" OnClick="btnbind_Click" ToolTip="<%$ Resources:Attendance,Search %>" />
                                                        </td>
                                                        <td width="50px" align="center">
                                                            <asp:Panel ID="pnlRefresh" runat="server" DefaultButton="btnRefresh">
                                                                <asp:ImageButton ID="btnRefresh" runat="server" CausesValidation="False" Height="25px"
                                                                    ImageUrl="~/Images/refresh.png" Width="25px" OnClick="btnRefresh_Click" ToolTip="<%$ Resources:Attendance,Refresh %>">
                                                                </asp:ImageButton>
                                                            </asp:Panel>
                                                        </td>
                                                        <td width="50px" align="center">
                                                            <asp:Panel ID="Panel1" runat="server" DefaultButton="imbBtnGrid">
                                                                <asp:ImageButton ID="imbBtnGrid" Height="25px" Width="25px" CausesValidation="False"
                                                                    runat="server" ImageUrl="~/Images/a1.png" OnClick="imbBtnGrid_Click" ToolTip="<%$ Resources:Attendance, Grid View %>" />
                                                            </asp:Panel>
                                                            <asp:Panel ID="Panel2" runat="server" DefaultButton="imgBtnDatalist">
                                                                <asp:ImageButton ID="imgBtnDatalist" Height="25px" Width="25px" CausesValidation="False"
                                                                    runat="server" ImageUrl="~/Images/NewTree.png" OnClick="imgBtnDatalist_Click"
                                                                    ToolTip="<%$ Resources:Attendance,List View %>" Visible="False" />
                                                            </asp:Panel>
                                                        </td>
                                                        <td>
                                                            <td align="center">
                                                                <asp:Label ID="lblTotalRecord" runat="server" Text="<%$ Resources:Attendance,Total Records: 0 %>"
                                                                    CssClass="labelComman"></asp:Label>
                                                            </td>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </div>
                                        </asp:Panel>
                                        <asp:GridView ID="gvProduct" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                                            OnPageIndexChanging="gvProduct_PageIndexChanging" CssClass="grid" Width="100%"
                                            DataKeyNames="ProductId" PageSize="<%# SystemParameter.GetPageSize() %>" Visible="false">
                                            <Columns>
                                                <asp:TemplateField HeaderText="<%$ Resources:Attendance,Edit %>">
                                                    <ItemTemplate>
                                                        <asp:ImageButton ID="btnEdit" runat="server" CausesValidation="False" CommandArgument='<%# Eval("ProductId") %>'
                                                            ImageUrl="~/Images/edit.png" Visible="false" OnCommand="btnEdit_Command" Width="16px" />
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Center" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="<%$ Resources:Attendance,Product Id %>">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblProductId" runat="server" Text='<%# Eval("ProductCode") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle CssClass="grid" />
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="EProductName" HeaderText="<%$ Resources:Attendance,Product Name %>"
                                                    SortExpression="EProductName" ItemStyle-CssClass="grid" ItemStyle-Width="40%" />
                                                <asp:TemplateField HeaderText="<%$ Resources:Attendance,Model No %>">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblProductCode" runat="server" Text='<%# Eval("ModelNo") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle CssClass="grid" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="<%$ Resources:Attendance,Item Type %>">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblItemType" runat="server" Text='<%# GetItemType(Eval("ItemType").ToString()) %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle CssClass="grid" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="<%$ Resources:Attendance, Product Unit %>">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblPUnit" runat="server" Text='<%#  GetUnitName(Eval("UnitId").ToString()) %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle CssClass="grid" />
                                                </asp:TemplateField>
                                            </Columns>
                                            <AlternatingRowStyle CssClass="InvgridAltRow" />
                                            <HeaderStyle CssClass="Invgridheader" BorderStyle="Solid" BorderColor="#6E6E6E" BorderWidth="1px" />
                                            <PagerStyle CssClass="Invgridheader" />
                                            <RowStyle CssClass="Invgridrow" HorizontalAlign="Center" />
                                        </asp:GridView>
                                        <br />
                                        <asp:DataList ID="dtlistProduct" runat="server" RepeatColumns="3" RepeatDirection="Horizontal"
                                            Width="100%">
                                            <ItemTemplate>
                                                <div class="product_box">
                                                    <table width="100%">
                                                        <tr>
                                                            <td colspan="4">
                                                                <asp:LinkButton ID="lbldlProductName" runat="server" Font-Bold="true" ForeColor="#1886b9"
                                                                    CssClass="labelComman" Style="text-decoration: none;" Width="310px" OnCommand="btnEdit_Command"
                                                                    CommandArgument='<%# Eval("ProductId") %>' Text='<%# Eval("EProductName") %>'
                                                                    Enabled="False"></asp:LinkButton>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td valign="top" rowspan="6" width="1px">
                                                                <asp:ImageButton ID="btnImgProduct" runat="server" OnCommand="btnEdit_Command" CommandArgument='<%# Eval("ProductId") %>'
                                                                    Width="120px" Height="120px" ImageUrl='<%# "~/Handler.ashx?ImID="+ Eval("ProductId") %>'
                                                                    Enabled="False" />
                                                            </td>
                                                            <td width="60px">
                                                                <asp:Label ID="lblProductId" runat="server" Text="<%$ Resources:Attendance,Product Id %>"
                                                                    CssClass="labelComman" Width="60px"></asp:Label>
                                                            </td>
                                                            <td width="3px">
                                                                :
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="lbldlProductId" runat="server" Width="135px" CssClass="labelComman"
                                                                    Text='<%# Eval("ProductCode") %>'></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <asp:Label ID="lblModelNo" runat="server" Text="<%$ Resources:Attendance,Model No. %>"
                                                                    CssClass="labelComman"></asp:Label>
                                                            </td>
                                                            <td>
                                                                :
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="lbldltModelNo" runat="server" CssClass="labelComman" Width="100px"
                                                                    Text='<%# Eval("ModelNo") %>'></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <asp:Label ID="lblItypeType" runat="server" Text="<%$ Resources:Attendance,Item Type %>"
                                                                    CssClass="labelComman"></asp:Label>
                                                            </td>
                                                            <td>
                                                                :
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="lbldlItypeType" runat="server" CssClass="labelComman" Text='<%# GetItemType(Eval("ItemType").ToString()) %>'></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <asp:Label ID="lblCostPrice" runat="server" Text="<%$ Resources:Attendance, Cost Price %>"
                                                                    CssClass="labelComman"></asp:Label>
                                                            </td>
                                                            <td>
                                                                :
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="lbldlCostPrice" runat="server" CssClass="labelComman" Text='<%# Eval("CostPrice") %>'></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <asp:Label ID="lblProductUnit" runat="server" Text="<%$ Resources:Attendance,Unit %>"
                                                                    CssClass="labelComman"></asp:Label>
                                                            </td>
                                                            <td>
                                                                :
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="lblUnit" runat="server" CssClass="labelComman" Text='<%# GetUnitName(Eval("UnitId").ToString()) %>'></asp:Label>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </div>
                                                <br />
                                            </ItemTemplate>
                                        </asp:DataList>
                                        <asp:HiddenField ID="HDFSort" runat="server" />
                                        <div>
                                            <table style="width: 100%; padding-left: 43px">
                                                <tr>
                                                    <td>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="center">
                                                        <table>
                                                            <tr>
                                                                <td>
                                                                    <asp:LinkButton ID="lnkFirst" runat="server" Font-Size="Small" Font-Bold="True" ForeColor="#1886b9"
                                                                        CausesValidation="False" OnClick="lnkFirst_Click" Style="text-decoration: none">First</asp:LinkButton>
                                                                </td>
                                                                <td width="3px">
                                                                </td>
                                                                <td>
                                                                    <asp:LinkButton ID="lnkPrev" runat="server" Font-Bold="True" Font-Size="Small" ForeColor="#1886b9"
                                                                        CausesValidation="False" OnClick="lnkPrev_Click" Style="text-decoration: none">Prev</asp:LinkButton>
                                                                </td>
                                                                <td width="3px">
                                                                </td>
                                                                <td>
                                                                    <asp:LinkButton ID="lnkNext" runat="server" Font-Size="Small" Font-Bold="True" ForeColor="#1886b9"
                                                                        CausesValidation="False" OnClick="lnkNext_Click" Style="text-decoration: none">Next</asp:LinkButton>
                                                                </td>
                                                                <td width="3px">
                                                                </td>
                                                                <td>
                                                                    <asp:LinkButton ID="lnkLast" runat="server" Font-Bold="True" Font-Size="Small" ForeColor="#1886b9"
                                                                        CausesValidation="False" OnClick="lnkLast_Click" Style="text-decoration: none">Last</asp:LinkButton>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                        <asp:HiddenField ID="hdnProductId" runat="server" />
                                                    </td>
                                                </tr>
                                            </table>
                                        </div>
                                    </asp:Panel>
                                    <asp:Panel ID="pnlNewEdit" runat="server">
                                        <table width="100%" style="padding-left: 43px">
                                            <tr>
                                                <td width="90px" align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                                    <asp:Label ID="lblProductId" runat="server" Text="<%$ Resources:Attendance,Product Id %>"
                                                        CssClass="labelComman"></asp:Label>
                                                </td>
                                                <td>
                                                    :
                                                </td>
                                                <td align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                                    <asp:TextBox ID="txtProductId" runat="server" CssClass="textComman" AutoPostBack="true"
                                                        OnTextChanged="txtProductId_TextChanged" BackColor="#e3e3e3"></asp:TextBox>
                                                    <cc1:AutoCompleteExtender ID="AutoCompleteExtender3" runat="server" CompletionInterval="100"
                                                        DelimiterCharacters="" Enabled="True" MinimumPrefixLength="1" ServiceMethod="GetCompletionListProductId"
                                                        ServicePath="" TargetControlID="txtProductId" UseContextKey="True" CompletionListCssClass="completionList"
                                                        CompletionListItemCssClass="listItem" CompletionListHighlightedItemCssClass="itemHighlighted">
                                                    </cc1:AutoCompleteExtender>
                                                </td>
                                                <td colspan="3" rowspan="4" style="padding-top: 10PX" align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                                    <table>
                                                        <tr>
                                                            <td width="110px" align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                                                <asp:Label ID="lblPImage" runat="server" CssClass="labelComman" Text="<%$ Resources:Attendance,Product Image %>"></asp:Label>
                                                            </td>
                                                            <td>
                                                                :
                                                            </td>
                                                            <td>
                                                                <asp:FileUpload ID="fugProduct" runat="server" Width="300px" />
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td colspan="3" align='<%= Common.ChangeTDForDefaultLeft()%>' style="padding-left: 120px">
                                                                <table>
                                                                    <tr>
                                                                        <td>
                                                                            <asp:Image ID="imgProduct" runat="server" Height="120px" Width="150px" />
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td valign="top">
                                                                            <asp:Button ID="btnloadimg" runat="server" CssClass="buttonLoad" Height="18px" Text="<%$ Resources:Attendance,Load %>"
                                                                                Width="150px" OnClick="btnloadimg_Click" />
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td width="90px" align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                                    <asp:Label ID="lblModelNo" runat="server" Text="<%$ Resources:Attendance,Model No. %>"
                                                        CssClass="labelComman"></asp:Label>
                                                </td>
                                                <td>
                                                    :
                                                </td>
                                                <td align='<%= Common.ChangeTDForDefaultLeft()%>' colspan="2">
                                                    <asp:TextBox ID="txtModelNo" runat="server" CssClass="textComman" Width="95%"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td width="120px" align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                                    <asp:Label ID="lblEProductName" runat="server" Text="<%$ Resources:Attendance,Product Name %>"
                                                        CssClass="labelComman"></asp:Label>
                                                </td>
                                                <td>
                                                    :
                                                </td>
                                                <td align='<%= Common.ChangeTDForDefaultLeft()%>' colspan="2">
                                                    <asp:TextBox ID="txtEProductName" runat="server" CssClass="textComman" BackColor="#e3e3e3"
                                                        Width="95%"></asp:TextBox>
                                                    <cc1:AutoCompleteExtender ID="AutoCompleteExtender1" runat="server" CompletionInterval="100"
                                                        DelimiterCharacters="" Enabled="True" MinimumPrefixLength="1" ServiceMethod="GetCompletionListProductName"
                                                        ServicePath="" TargetControlID="txtEProductName" UseContextKey="True" CompletionListCssClass="completionList"
                                                        CompletionListItemCssClass="listItem" CompletionListHighlightedItemCssClass="itemHighlighted">
                                                    </cc1:AutoCompleteExtender>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td width="150px" align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                                    <asp:Label ID="lblLProductName" runat="server" Text="<%$ Resources:Attendance,Product Name(Ar) %>"
                                                        CssClass="labelComman"></asp:Label>
                                                </td>
                                                <td>
                                                    :
                                                </td>
                                                <td align='<%= Common.ChangeTDForDefaultLeft()%>' colspan="2">
                                                    <asp:TextBox ID="txtLProductName" runat="server" CssClass="textComman" Width="95%"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="6">
                                                    <hr style="color: #FFFFFF; float: left; width: 851px;" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td width="120px" align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                                    <asp:Label ID="lblItypeType" runat="server" Text="<%$ Resources:Attendance,Item Type %>"
                                                        CssClass="labelComman"></asp:Label>
                                                </td>
                                                <td>
                                                    :
                                                </td>
                                                <td align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                                    <asp:DropDownList ID="ddlItemType" Width="260px" runat="server" CssClass="textComman"
                                                        AutoPostBack="True" OnSelectedIndexChanged="ddlItypeType_SelectedIndexChanged">
                                                        <asp:ListItem Selected="True" Text="--Select--" Value="0"></asp:ListItem>
                                                        <asp:ListItem Text="Stockable" Value="S"></asp:ListItem>
                                                        <asp:ListItem Text="Non Stockable" Value="NS"></asp:ListItem>
                                                        <asp:ListItem Text="Assemble" Value="A"></asp:ListItem>
                                                        <asp:ListItem Text="KIT" Value="K"></asp:ListItem>
                                                    </asp:DropDownList>
                                                </td>
                                                <td id="tdR1" runat="server" visible="False" align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                                    <asp:Label ID="lblReorderQty" runat="server" Text="<%$ Resources:Attendance,Reorder Level %>"
                                                        CssClass="labelComman" />
                                                </td>
                                                <td id="tdR2" runat="server" visible="False">
                                                    :
                                                </td>
                                                <td id="tdR3" runat="server" visible="False" align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                                    <asp:TextBox ID="txtReorderQty" runat="server" CssClass="textComman" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                                    <asp:Label ID="lblMaintainStock" runat="server" Text="<%$ Resources:Attendance,Inventory Type %>"
                                                        CssClass="labelComman" />
                                                </td>
                                                <td>
                                                    :
                                                </td>
                                                <td align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                                    <asp:DropDownList ID="ddlMaintainStock" Width="260px" runat="server" CssClass="textComman"
                                                        AutoPostBack="True" OnSelectedIndexChanged="ddlMaintainStock_SelectedIndexChanged">
                                                        <asp:ListItem Value="--Select--" Text="--Select--"></asp:ListItem>
                                                        <asp:ListItem Value="BW" Text="Batch Wise"></asp:ListItem>
                                                        <asp:ListItem Value="PP" Text="Particular Product"></asp:ListItem>
                                                        <asp:ListItem Value="LNO" Text="LOT No ."></asp:ListItem>
                                                        <asp:ListItem Value="SNO" Text="Serial No ."></asp:ListItem>
                                                        <asp:ListItem Value="LIFO" Text="LIFO"></asp:ListItem>
                                                        <asp:ListItem Value="FIFO" Text="FIFO"></asp:ListItem>
                                                    </asp:DropDownList>
                                                </td>
                                                <td id="TdTypeOfBatchNo1" visible="False" align='<%= Common.ChangeTDForDefaultLeft()%>'
                                                    runat="server">
                                                    <asp:Label ID="lblTypeOfBatchNo" runat="server" CssClass="labelComman" Text="<%$ Resources:Attendance,Type %>" />
                                                </td>
                                                <td id="TdTypeOfBatchNo2" runat="server" visible="False">
                                                    :
                                                </td>
                                                <td id="TdTypeOfBatchNo3" visible="False" align='<%= Common.ChangeTDForDefaultLeft()%>'
                                                    runat="server">
                                                    <asp:DropDownList ID="ddlTypeOfBatchNo" runat="server" Width="260px" CssClass="textComman">
                                                        <asp:ListItem Text="Internally" Value="Internally"></asp:ListItem>
                                                        <asp:ListItem Text="Externally" Value="Externally"></asp:ListItem>
                                                    </asp:DropDownList>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td width="120px" align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                                    <asp:Label ID="lbldimensional" runat="server" Text="<%$ Resources:Attendance,Dimensional %>"
                                                        CssClass="labelComman"></asp:Label>
                                                </td>
                                                <td>
                                                    :
                                                </td>
                                                <td align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                                    <table cellspacing="0">
                                                        <tr>
                                                            <td>
                                                                <asp:TextBox ID="txtLenth" runat="server" Width="66px" CssClass="textCommanSmall"></asp:TextBox>
                                                                <asp:Label ID="lblWx" runat="server" Text="X" CssClass="labelComman"></asp:Label>
                                                                <cc1:TextBoxWatermarkExtender ID="txtLenth_TextBoxWatermarkExtender" runat="server"
                                                                    WatermarkText="L" TargetControlID="txtLenth">
                                                                </cc1:TextBoxWatermarkExtender>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txtHeight" runat="server" Width="66px" CssClass="textCommanSmall"></asp:TextBox>
                                                                <asp:Label ID="lblLx" runat="server" Text="X" CssClass="labelComman"></asp:Label>
                                                                <cc1:TextBoxWatermarkExtender ID="txtHeight_TextBoxWatermarkExtender" runat="server"
                                                                    WatermarkText="H" TargetControlID="txtHeight">
                                                                </cc1:TextBoxWatermarkExtender>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txtDepth" runat="server" Width="66px" CssClass="textCommanSmall"></asp:TextBox>
                                                                <cc1:TextBoxWatermarkExtender ID="txtDepth_TextBoxWatermarkExtender" runat="server"
                                                                    WatermarkText="D" TargetControlID="txtDepth">
                                                                </cc1:TextBoxWatermarkExtender>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                                <td width="120px" align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                                    <asp:Label ID="lblalternateId" runat="server" Text="<%$ Resources:Attendance,Alternate Id %>"
                                                        CssClass="labelComman"></asp:Label>
                                                </td>
                                                <td>
                                                    :
                                                </td>
                                                <td align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                                    <table cellspacing="0">
                                                        <tr>
                                                            <td>
                                                                <asp:TextBox ID="txtAlterId1" runat="server" Width="73px" CssClass="textCommanSmall"></asp:TextBox>
                                                                <cc1:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender1" runat="server" WatermarkText="AltId-1"
                                                                    TargetControlID="txtAlterId1">
                                                                </cc1:TextBoxWatermarkExtender>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txtAlterId2" runat="server" Width="73px" CssClass="textCommanSmall"></asp:TextBox>
                                                                <cc1:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender2" runat="server" WatermarkText="AltId-2"
                                                                    TargetControlID="txtAlterId2">
                                                                </cc1:TextBoxWatermarkExtender>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txtAlterId3" runat="server" Width="73px" CssClass="textCommanSmall"></asp:TextBox>
                                                                <cc1:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender3" runat="server" WatermarkText="AltId-3"
                                                                    TargetControlID="txtAlterId3">
                                                                </cc1:TextBoxWatermarkExtender>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="6">
                                                    <hr style="color: #FFFFFF; float: left; width: 851px;" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                                    <asp:Label ID="lblPartNo" runat="server" CssClass="labelComman" Text="<%$ Resources:Attendance,Part No. %>"></asp:Label>
                                                </td>
                                                <td>
                                                    :
                                                </td>
                                                <td align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                                    <asp:TextBox ID="txtPartNo" runat="server" ReadOnly="True" CssClass="textComman"></asp:TextBox>
                                                </td>
                                                <td width="113px" align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                                    <asp:Label ID="lblHSCode" runat="server" CssClass="labelComman" Text="<%$ Resources:Attendance,HS Code %>"></asp:Label>
                                                </td>
                                                <td>
                                                    :
                                                </td>
                                                <td align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                                    <asp:TextBox ID="txtHasCode" runat="server" CssClass="textComman" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                                    <asp:Label ID="lblStatus" runat="server" CssClass="labelComman" Text="<%$ Resources:Attendance,status %>"></asp:Label>
                                                </td>
                                                <td>
                                                    :
                                                </td>
                                                <td align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                                    <asp:DropDownList ID="ddlstatus" runat="server" Width="260px" CssClass="textComman"
                                                        Visible="false">
                                                        <asp:ListItem Value="True">Active</asp:ListItem>
                                                        <asp:ListItem Value="False">InActive</asp:ListItem>
                                                        <asp:ListItem>Discontinue</asp:ListItem>
                                                    </asp:DropDownList>
                                                </td>
                                                <td align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                                    <asp:Label ID="lbldoesProductHas" runat="server" CssClass="labelComman" Text="<%$ Resources:Attendance,Does Product Has %>"></asp:Label>
                                                </td>
                                                <td>
                                                    :
                                                </td>
                                                <td align='<%= Common.ChangeTDForDefaultLeft()%>' rowspan="2" valign="top" style="padding-top: 10px">
                                                    <table>
                                                        <tr>
                                                            <td align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                                                <asp:CheckBox ID="ChkHasBatchNo" runat="server" CssClass="labelComman" Text="<%$ Resources:Attendance,Batch No %>" />
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td align='<%= Common.ChangeTDForDefaultLeft()%>' style="padding-top: 15px">
                                                                <asp:CheckBox ID="ChkHasSerialNo" runat="server" CssClass="labelComman" Text="<%$ Resources:Attendance,Serial No %>" />
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                                    <asp:Label ID="lblProductUnit" CssClass="labelComman" runat="server" Text="<%$ Resources:Attendance,Product Unit %>" />
                                                </td>
                                                <td>
                                                    :
                                                </td>
                                                <td align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                                    <asp:TextBox ID="txtProductUnit" runat="server" CssClass="textComman" AutoPostBack="true"
                                                        BackColor="#e3e3e3" OnTextChanged="txtProductUnit_TextChanged" />
                                                    <cc1:AutoCompleteExtender ID="AutoCompleteExtender2" runat="server" CompletionInterval="100"
                                                        DelimiterCharacters="" Enabled="True" MinimumPrefixLength="1" ServiceMethod="GetCompletionList3"
                                                        ServicePath="" TargetControlID="txtProductUnit" UseContextKey="True" CompletionListCssClass="completionList"
                                                        CompletionListItemCssClass="listItem" CompletionListHighlightedItemCssClass="itemHighlighted">
                                                    </cc1:AutoCompleteExtender>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="6">
                                                    <hr style="color: #FFFFFF; float: left; width: 851px;" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="height: 20px">
                                                </td>
                                            </tr>
                                        </table>
                                        <table width="100%" style="padding-left: 43px">
                                            <tr>
                                                <td>
                                                    <cc1:TabContainer ID="TabContainer1" runat="server" ActiveTabIndex="0" Width="97%"
                                                        CssClass="Tab">
                                                        <cc1:TabPanel ID="TabPanel1" runat="server" HeaderText="<%$ Resources:Attendance,Description %>">
                                                            <ContentTemplate>
                                                                <table style="background-image: url(../Images/bgs.png);" width="100%">
                                                                    <tr>
                                                                        <td>
                                                                            <cc1:Editor ID="txtDesc" runat="server" Width="100%" />
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </ContentTemplate>
                                                        </cc1:TabPanel>
                                                        <cc1:TabPanel ID="TabGenral" runat="server" HeaderText="<%$ Resources:Attendance,General %>">
                                                            <ContentTemplate>
                                                                <table style="background-image: url(../Images/bgs.png);">
                                                                    <tr>
                                                                        <td>
                                                                            <table style="padding-left: 30px; padding-top: 5px">
                                                                                <tr>
                                                                                    <td align="<%= Common.ChangeTDForDefaultLeft()%>" width="90px">
                                                                                        <asp:Label ID="lblProductCountry" runat="server" Text="<%$ Resources:Attendance,Made in %>"
                                                                                            CssClass="labelComman" />
                                                                                    </td>
                                                                                    <td>
                                                                                        :
                                                                                    </td>
                                                                                    <td align="<%= Common.ChangeTDForDefaultLeft()%>" width="150px">
                                                                                        <asp:TextBox ID="txtProductCountry" runat="server" BackColor="#E3E3E3" Width="90px"
                                                                                            CssClass="textCommanSmall" AutoPostBack="True" OnTextChanged="txtProductCountry_TextChanged" />
                                                                                        <cc1:AutoCompleteExtender ID="txtProductCountry_AutoCompleteExtender" runat="server"
                                                                                            CompletionInterval="100" DelimiterCharacters="" Enabled="True" MinimumPrefixLength="1"
                                                                                            ServiceMethod="GetCompletionList_Contry" ServicePath="" TargetControlID="txtProductCountry"
                                                                                            UseContextKey="True" CompletionListCssClass="completionList" CompletionListItemCssClass="listItem"
                                                                                            CompletionListHighlightedItemCssClass="itemHighlighted">
                                                                                        </cc1:AutoCompleteExtender>
                                                                                    </td>
                                                                                    <td align="<%= Common.ChangeTDForDefaultLeft()%>" width="120px">
                                                                                        <asp:Label ID="lblWholesalePrice" runat="server" Text="<%$ Resources:Attendance,Wholesale Price %>"
                                                                                            CssClass="labelComman" />
                                                                                    </td>
                                                                                    <td>
                                                                                        :
                                                                                    </td>
                                                                                    <td align="<%= Common.ChangeTDForDefaultLeft()%>" width="150px">
                                                                                        <asp:TextBox ID="txtWholesalePrice" runat="server" Width="90px" CssClass="textCommanSmall" />
                                                                                        <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender5" runat="server" Enabled="True"
                                                                                            TargetControlID="txtWholesalePrice" ValidChars="1,2,3,4,5,6,7,8,9,0,.">
                                                                                        </cc1:FilteredTextBoxExtender>
                                                                                    </td>
                                                                                    <td align="<%= Common.ChangeTDForDefaultLeft()%>" width="120px">
                                                                                        <asp:Label ID="lblSalesPrice1" runat="server" Text="<%$ Resources:Attendance,Sales Price 1 %>"
                                                                                            CssClass="labelComman"></asp:Label>
                                                                                    </td>
                                                                                    <td>
                                                                                        :
                                                                                    </td>
                                                                                    <td align="<%= Common.ChangeTDForDefaultLeft()%>">
                                                                                        <asp:TextBox ID="txtSalesPrice1" runat="server" Width="90px" CssClass="textCommanSmall"></asp:TextBox>
                                                                                        <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender3" runat="server" Enabled="True"
                                                                                            TargetControlID="txtSalesPrice1" ValidChars="1,2,3,4,5,6,7,8,9,0,.">
                                                                                        </cc1:FilteredTextBoxExtender>
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td align="<%= Common.ChangeTDForDefaultLeft()%>">
                                                                                        <asp:Label ID="lblProductColor" runat="server" Text="<%$ Resources:Attendance,Color %>"
                                                                                            CssClass="labelComman" />
                                                                                    </td>
                                                                                    <td>
                                                                                        :
                                                                                    </td>
                                                                                    <td align="<%= Common.ChangeTDForDefaultLeft()%>">
                                                                                        <asp:TextBox ID="txtProductColor" runat="server" CssClass="textCommanSmall" Width="90px" />
                                                                                        <cc1:ColorPickerExtender ID="txtCardColor_ColorPickerExtender" runat="server" Enabled="True"
                                                                                            TargetControlID="txtProductColor" SampleControlID="txtProductColor">
                                                                                        </cc1:ColorPickerExtender>
                                                                                    </td>
                                                                                    <td align="<%= Common.ChangeTDForDefaultLeft()%>">
                                                                                        <asp:Label ID="lblDiscount" runat="server" Text="<%$ Resources:Attendance,Discount% %>"
                                                                                            CssClass="labelComman" />
                                                                                    </td>
                                                                                    <td>
                                                                                        :
                                                                                    </td>
                                                                                    <td align="<%= Common.ChangeTDForDefaultLeft()%>">
                                                                                        <asp:TextBox ID="txtDiscount" runat="server" CssClass="textCommanSmall" Width="90px" />
                                                                                        <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender4" runat="server" Enabled="True"
                                                                                            TargetControlID="txtDiscount" ValidChars="1,2,3,4,5,6,7,8,9,0,.">
                                                                                        </cc1:FilteredTextBoxExtender>
                                                                                    </td>
                                                                                    <td align="<%= Common.ChangeTDForDefaultLeft()%>">
                                                                                        <asp:Label ID="lblSalesPrice2" runat="server" Text="<%$ Resources:Attendance,Sales Price 2 %>"
                                                                                            CssClass="labelComman"></asp:Label>
                                                                                    </td>
                                                                                    <td>
                                                                                        :
                                                                                    </td>
                                                                                    <td align="<%= Common.ChangeTDForDefaultLeft()%>">
                                                                                        <asp:TextBox ID="txtSalesPrice2" runat="server" CssClass="textCommanSmall" Width="90px"></asp:TextBox>
                                                                                        <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server" Enabled="True"
                                                                                            TargetControlID="txtSalesPrice2" ValidChars="1,2,3,4,5,6,7,8,9,0,.">
                                                                                        </cc1:FilteredTextBoxExtender>
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td align="<%= Common.ChangeTDForDefaultLeft()%>">
                                                                                        <asp:Label ID="lblCostPrice" runat="server" Text="<%$ Resources:Attendance,Cost Price %>"
                                                                                            CssClass="labelComman" />
                                                                                    </td>
                                                                                    <td>
                                                                                        :
                                                                                    </td>
                                                                                    <td align="<%= Common.ChangeTDForDefaultLeft()%>">
                                                                                        <asp:TextBox ID="txtCostPrice" runat="server" CssClass="textCommanSmall" Width="90px" />
                                                                                        <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender6" runat="server" Enabled="True"
                                                                                            TargetControlID="txtCostPrice" ValidChars="1,2,3,4,5,6,7,8,9,0,.">
                                                                                        </cc1:FilteredTextBoxExtender>
                                                                                    </td>
                                                                                    <td align="<%= Common.ChangeTDForDefaultLeft()%>">
                                                                                        <asp:Label ID="lblProfit" runat="server" Text="<%$ Resources:Attendance,Profit %>"
                                                                                            CssClass="labelComman" />
                                                                                    </td>
                                                                                    <td>
                                                                                        :
                                                                                    </td>
                                                                                    <td align="<%= Common.ChangeTDForDefaultLeft()%>">
                                                                                        <asp:TextBox ID="txtprofit" runat="server" CssClass="textCommanSmall" Width="90px" />
                                                                                        <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender7" runat="server" Enabled="True"
                                                                                            TargetControlID="txtprofit" ValidChars="1,2,3,4,5,6,7,8,9,0,.">
                                                                                        </cc1:FilteredTextBoxExtender>
                                                                                    </td>
                                                                                    <td align="<%= Common.ChangeTDForDefaultLeft()%>">
                                                                                        <asp:Label ID="lblSalesPrice3" runat="server" Text="<%$ Resources:Attendance,Sales Price 3 %>"
                                                                                            CssClass="labelComman"></asp:Label>
                                                                                    </td>
                                                                                    <td>
                                                                                        :
                                                                                    </td>
                                                                                    <td align="<%= Common.ChangeTDForDefaultLeft()%>">
                                                                                        <asp:TextBox ID="txtSalesPrice3" runat="server" CssClass="textCommanSmall" Width="90px"></asp:TextBox>
                                                                                        <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" Enabled="True"
                                                                                            TargetControlID="txtSalesPrice3" ValidChars="1,2,3,4,5,6,7,8,9,0,.">
                                                                                        </cc1:FilteredTextBoxExtender>
                                                                                    </td>
                                                                                </tr>
                                                                            </table>
                                                                            <table style="padding-left: 30px; padding-bottom: 10px">
                                                                                <tr>
                                                                                    <td colspan="4">
                                                                                        <hr style="color: #FFFFFF; float: left; width: 770px;" />
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td align='<%= Common.ChangeTDForDefaultLeft()%>' width="250px">
                                                                                        <asp:Label ID="lblminimum" runat="server" Text="<%$ Resources:Attendance,Minimum Quantity %>"
                                                                                            CssClass="labelComman" />
                                                                                    </td>
                                                                                    <td align='<%= Common.ChangeTDForDefaultLeft()%>' style="padding-left: 23px" width="250px">
                                                                                        <asp:Label ID="lblMaximum" runat="server" Text="<%$ Resources:Attendance,Maximum Quantity %>"
                                                                                            CssClass="labelComman" />
                                                                                    </td>
                                                                                    <td align='<%= Common.ChangeTDForDefaultLeft()%>' style="padding-left: 23px" width="250px">
                                                                                        <asp:Label ID="lblDamage" runat="server" Text="<%$ Resources:Attendance,Damage Quantity %>"
                                                                                            CssClass="labelComman" />
                                                                                    </td>
                                                                                    <td align='<%= Common.ChangeTDForDefaultLeft()%>' style="padding-left: 23px" width="250px">
                                                                                        <asp:Label ID="lblExpired" runat="server" Text="<%$ Resources:Attendance,Expired Quantity %>"
                                                                                            CssClass="labelComman" />
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td align="<%= Common.ChangeTDForDefaultLeft()%>">
                                                                                        <asp:TextBox ID="txtMiniQty" runat="server" CssClass="textCommanSmall" Width="120px" />
                                                                                        <cc1:FilteredTextBoxExtender ID="txtMiniQty_FilteredTextBoxExtender" runat="server"
                                                                                            Enabled="True" TargetControlID="txtMiniQty" ValidChars="1,2,3,4,5,6,7,8,9,0,.">
                                                                                        </cc1:FilteredTextBoxExtender>
                                                                                    </td>
                                                                                    <td align='<%= Common.ChangeTDForDefaultLeft()%>' style="padding-left: 23px">
                                                                                        <asp:TextBox ID="txtMaxQty" runat="server" CssClass="textCommanSmall" Width="120px" />
                                                                                        <cc1:FilteredTextBoxExtender ID="txtMaxQty_FilteredTextBoxExtender" runat="server"
                                                                                            Enabled="True" TargetControlID="txtMaxQty" ValidChars="1,2,3,4,5,6,7,8,9,0,.">
                                                                                        </cc1:FilteredTextBoxExtender>
                                                                                    </td>
                                                                                    <td align='<%= Common.ChangeTDForDefaultLeft()%>' style="padding-left: 23px">
                                                                                        <asp:TextBox ID="txtDamageQty" runat="server" ReadOnly="True" CssClass="textCommanSmall"
                                                                                            Width="120px" />
                                                                                        <cc1:FilteredTextBoxExtender ID="txtDamageQty_FilteredTextBoxExtender" runat="server"
                                                                                            Enabled="True" TargetControlID="txtDamageQty" ValidChars="1,2,3,4,5,6,7,8,9,0,.">
                                                                                        </cc1:FilteredTextBoxExtender>
                                                                                    </td>
                                                                                    <td align='<%= Common.ChangeTDForDefaultLeft()%>' style="padding-left: 23px">
                                                                                        <asp:TextBox ID="txtExpQty" runat="server" ReadOnly="True" CssClass="textCommanSmall"
                                                                                            Width="120px" />
                                                                                        <cc1:FilteredTextBoxExtender ID="txtExpQty_FilteredTextBoxExtender" runat="server"
                                                                                            Enabled="True" TargetControlID="txtExpQty" ValidChars="1,2,3,4,5,6,7,8,9,0,.">
                                                                                        </cc1:FilteredTextBoxExtender>
                                                                                    </td>
                                                                                </tr>
                                                                            </table>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </ContentTemplate>
                                                        </cc1:TabPanel>
                                                        <cc1:TabPanel ID="Tabpersonalinfo" runat="server" HeaderText="<%$ Resources:Attendance,Product Info %>"
                                                            BorderColor="#C4C4C4">
                                                            <ContentTemplate>
                                                                <table style="background-image: url(../Images/bgs.png); height: 100%" width="100%">
                                                                    <tr>
                                                                        <td width="50%">
                                                                            <table width="100%" style="padding-left: 20px; padding-top: 5px; padding-bottom: 10px">
                                                                                <tr>
                                                                                    <td>
                                                                                        <asp:Label ID="lblProductBrand" runat="server" Font-Bold="True" Font-Names="arial"
                                                                                            Font-Size="13px" ForeColor="#666666" Text="<%$ Resources:Attendance,Manufacturing Brand %>"></asp:Label>
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td>
                                                                                        <asp:ListBox ID="lstProductBrand" runat="server" Height="156px" Width="171px" SelectionMode="Multiple"
                                                                                            CssClass="list" Font-Names="Trebuchet MS" Font-Size="Small" ForeColor="Gray">
                                                                                        </asp:ListBox>
                                                                                    </td>
                                                                                    <td rowspan="4">
                                                                                        <table>
                                                                                            <tr>
                                                                                                <td height="30px">
                                                                                                    <asp:Button ID="btnpushBrand" runat="server" BorderStyle="None" BackColor="Transparent"
                                                                                                        CssClass="btnPush" OnClick="btnpushBrand_Click" />
                                                                                                </td>
                                                                                            </tr>
                                                                                            <tr>
                                                                                                <td height="30px">
                                                                                                    <asp:Button ID="btnpullBrand" runat="server" BorderStyle="None" BackColor="Transparent"
                                                                                                        CssClass="btnPull" OnClick="btnpullBrand_Click" />
                                                                                                </td>
                                                                                            </tr>
                                                                                            <tr>
                                                                                                <td height="30px">
                                                                                                    <asp:Button ID="btnpushBrandAll" runat="server" BorderStyle="None" BackColor="Transparent"
                                                                                                        CssClass="btnPushAll" OnClick="btnpushBrandAll_Click" />
                                                                                                </td>
                                                                                            </tr>
                                                                                            <tr>
                                                                                                <td height="30px">
                                                                                                    <asp:Button ID="btnpullBrandAll" runat="server" BorderStyle="None" BackColor="Transparent"
                                                                                                        CssClass="btnPullAll" OnClick="btnpullBrandAll_Click" />
                                                                                                </td>
                                                                                            </tr>
                                                                                        </table>
                                                                                    </td>
                                                                                    <td>
                                                                                        <asp:ListBox ID="lstSelectedProductBrand" runat="server" Height="156px" Width="171px"
                                                                                            SelectionMode="Multiple" CssClass="list" Font-Names="Trebuchet MS" Font-Size="Small"
                                                                                            ForeColor="Gray" Font-Bold="true"></asp:ListBox>
                                                                                    </td>
                                                                                </tr>
                                                                            </table>
                                                                        </td>
                                                                        <td style="border-left: solid 1px #FFFFFF" width="50%">
                                                                            <table width="100%" style="padding-left: 20px; padding-top: 5px; padding-bottom: 10px">
                                                                                <tr>
                                                                                    <td>
                                                                                        <asp:Label ID="lblCategory" runat="server" Font-Bold="True" Font-Names="arial" Font-Size="13px"
                                                                                            ForeColor="#666666" Text="<%$ Resources:Attendance,Category %>"></asp:Label>
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td>
                                                                                        <asp:ListBox ID="lstProductCategory" runat="server" Height="156px" Width="171px"
                                                                                            SelectionMode="Multiple" CssClass="list" Font-Names="Trebuchet MS" Font-Size="Small"
                                                                                            ForeColor="Gray"></asp:ListBox>
                                                                                    </td>
                                                                                    <td rowspan="4">
                                                                                        <table>
                                                                                            <tr>
                                                                                                <td height="30px">
                                                                                                    <asp:Button ID="btnPushCate" runat="server" BorderStyle="None" BackColor="Transparent"
                                                                                                        CssClass="btnPush" OnClick="btnPushCate_Click" />
                                                                                                </td>
                                                                                            </tr>
                                                                                            <tr>
                                                                                                <td height="30px">
                                                                                                    <asp:Button ID="btnPullCate" runat="server" BorderStyle="None" BackColor="Transparent"
                                                                                                        CssClass="btnPull" OnClick="btnPullCate_Click" />
                                                                                                </td>
                                                                                            </tr>
                                                                                            <tr>
                                                                                                <td height="30px">
                                                                                                    <asp:Button ID="btnPushAllCate" runat="server" BorderStyle="None" BackColor="Transparent"
                                                                                                        CssClass="btnPushAll" OnClick="btnPushAllCate_Click" />
                                                                                                </td>
                                                                                            </tr>
                                                                                            <tr>
                                                                                                <td height="30px">
                                                                                                    <asp:Button ID="btnPullAllCate" runat="server" BorderStyle="None" BackColor="Transparent"
                                                                                                        CssClass="btnPullAll" OnClick="btnPullAllCate_Click" />
                                                                                                </td>
                                                                                            </tr>
                                                                                        </table>
                                                                                    </td>
                                                                                    <td>
                                                                                        <asp:ListBox ID="lstSelectProductCategory" runat="server" Height="156px" Width="171px"
                                                                                            SelectionMode="Multiple" CssClass="list" Font-Names="Trebuchet MS" Font-Size="Small"
                                                                                            ForeColor="Gray" Font-Bold="true"></asp:ListBox>
                                                                                    </td>
                                                                                </tr>
                                                                            </table>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </ContentTemplate>
                                                        </cc1:TabPanel>
                                                        <cc1:TabPanel ID="TabBrandLocation" runat="server" HeaderText="<%$ Resources:Attendance,Company Brand & Location %>">
                                                            <ContentTemplate>
                                                                <table style="background-image: url(../Images/bgs.png); height: 100%" width="100%">
                                                                    <tr>
                                                                        <td width="30%">
                                                                            <table width="100%" style="padding-left: 20px; padding-top: 5px; padding-bottom: 10px">
                                                                                <tr>
                                                                                    <td>
                                                                                        <asp:Label ID="lbl" runat="server" Font-Bold="True" Font-Names="arial" Font-Size="13px"
                                                                                            ForeColor="#666666" Text="<%$ Resources:Attendance,Brand %>"></asp:Label>
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td>
                                                                                        <asp:Panel ID="pnlBrand" runat="server" Height="115px" Width="171px" BorderStyle="Solid"
                                                                                            BorderWidth="1px" BorderColor="#abadb3" BackColor="White" ScrollBars="Auto">
                                                                                            <asp:CheckBoxList ID="ChkBrand" runat="server" RepeatColumns="1" AutoPostBack="True"
                                                                                                Font-Names="Trebuchet MS" Font-Size="Small" ForeColor="Gray" />
                                                                                        </asp:Panel>
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td>
                                                                                        <asp:Button ID="btnGetlocation" runat="server" Text="<%$ Resources:Attendance,Get Location %>"
                                                                                            BackColor="Transparent" BorderStyle="None" Font-Bold="true" CssClass="btnLocation"
                                                                                            Width="171px" Font-Size="13px" Font-Names=" Arial" OnClick="btnGetlocation_Click" />
                                                                                    </td>
                                                                                </tr>
                                                                            </table>
                                                                        </td>
                                                                        <td style="border-left: solid 1px #FFFFFF; padding-top: 10px; padding-left: 30px;"
                                                                            valign="top">
                                                                            <asp:Label ID="Label1" runat="server" Font-Bold="True" Font-Names="arial" Font-Size="13px"
                                                                                ForeColor="#666666" Text="<%$ Resources:Attendance,Location %>"></asp:Label>
                                                                            <table>
                                                                                <tr>
                                                                                    <td>
                                                                                        <asp:ListBox ID="lstLocation" runat="server" Height="155px" Width="171px" SelectionMode="Multiple"
                                                                                            CssClass="list" Font-Names="Trebuchet MS" Font-Size="Small" ForeColor="Gray">
                                                                                        </asp:ListBox>
                                                                                    </td>
                                                                                    <td rowspan="4">
                                                                                        <table>
                                                                                            <tr>
                                                                                                <td height="30px">
                                                                                                    <asp:Button ID="btnPushLoc" runat="server" BorderStyle="None" BackColor="Transparent"
                                                                                                        CssClass="btnPush" OnClick="btnPushLoc_Click" />
                                                                                                </td>
                                                                                            </tr>
                                                                                            <tr>
                                                                                                <td height="30px">
                                                                                                    <asp:Button ID="btnPullLoc" runat="server" BorderStyle="None" BackColor="Transparent"
                                                                                                        CssClass="btnPull" OnClick="btnPullLoc_Click" />
                                                                                                </td>
                                                                                            </tr>
                                                                                            <tr>
                                                                                                <td height="30px">
                                                                                                    <asp:Button ID="btnPushAllLoc" runat="server" BorderStyle="None" BackColor="Transparent"
                                                                                                        CssClass="btnPushAll" OnClick="btnPushAllLoc_Click" />
                                                                                                </td>
                                                                                            </tr>
                                                                                            <tr>
                                                                                                <td height="30px">
                                                                                                    <asp:Button ID="btnPullAllLoc" runat="server" BorderStyle="None" BackColor="Transparent"
                                                                                                        CssClass="btnPullAll" OnClick="btnPullAllLoc_Click" />
                                                                                                </td>
                                                                                            </tr>
                                                                                        </table>
                                                                                    </td>
                                                                                    <td>
                                                                                        <asp:ListBox ID="lstLocationSelect" runat="server" Height="155px" Width="171px" SelectionMode="Multiple"
                                                                                            CssClass="list" Font-Bold="true" Font-Names="Trebuchet MS" Font-Size="Small"
                                                                                            ForeColor="Gray"></asp:ListBox>
                                                                                    </td>
                                                                                </tr>
                                                                            </table>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </ContentTemplate>
                                                        </cc1:TabPanel>
                                                        <cc1:TabPanel ID="TabProductSupplier" runat="server" HeaderText="<%$ Resources:Attendance,Product Supplier %>">
                                                            <ContentTemplate>
                                                                <table style="background-image: url(../Images/bgs.png); height: 203px" width="100%">
                                                                    <tr>
                                                                        <td valign="top">
                                                                            <asp:Panel ID="panPS" runat="server" DefaultButton="IbtnAddProductSupplierCode">
                                                                                <table width="100%" style="padding-left: 20px; padding-top: 5px; padding-bottom: 10px">
                                                                                    <tr>
                                                                                        <td align='<%= Common.ChangeTDForDefaultLeft()%>' width="100px">
                                                                                            <asp:Label ID="lblSupplier" runat="server" CssClass="labelComman" Text="<%$ Resources:Attendance,Select Supplier %>" />
                                                                                        </td>
                                                                                        <td>
                                                                                            :
                                                                                        </td>
                                                                                        <td align='<%= Common.ChangeTDForDefaultLeft()%>' width="250PX">
                                                                                            <asp:TextBox ID="txtSuppliers" runat="server" OnTextChanged="txtSuppliers_OnTextChanged"
                                                                                                AutoPostBack="True" CssClass="textComman" Width="200px"></asp:TextBox>
                                                                                            <cc1:AutoCompleteExtender ID="txtSuppliers_AutoCompleteExtender" runat="server" CompletionInterval="100"
                                                                                                DelimiterCharacters="" Enabled="True" MinimumPrefixLength="1" ServiceMethod="GetCompletionList_Supplier"
                                                                                                ServicePath="" TargetControlID="txtSuppliers" UseContextKey="True" CompletionListCssClass="completionList"
                                                                                                CompletionListItemCssClass="listItem" CompletionListHighlightedItemCssClass="itemHighlighted">
                                                                                            </cc1:AutoCompleteExtender>
                                                                                        </td>
                                                                                        <td align='<%= Common.ChangeTDForDefaultLeft()%>' width="100px">
                                                                                            <asp:Label ID="lblProCode" runat="server" CssClass="labelComman" Text="<%$ Resources:Attendance,Supplier Code %>" />
                                                                                        </td>
                                                                                        <td>
                                                                                            :
                                                                                        </td>
                                                                                        <td align='<%= Common.ChangeTDForDefaultLeft()%>' width="180px">
                                                                                            <asp:TextBox ID="txtProductSupplierCode" runat="server" Width="180px" CssClass="textComman"></asp:TextBox>
                                                                                        </td>
                                                                                        <td align='<%= Common.ChangeTDForDefaultLeft()%>' style="padding-top: 8px">
                                                                                            <asp:ImageButton ID="IbtnAddProductSupplierCode" runat="server" CausesValidation="False"
                                                                                                Height="29px" ImageUrl="~/Images/add.png" OnClick="IbtnAddProductSupplierCode_Click"
                                                                                                Width="35px" ToolTip="<%$ Resources:Attendance,Add %>" />
                                                                                            <asp:HiddenField ID="hdnProductSupplierCode" runat="server" Value="0" />
                                                                                        </td>
                                                                                    </tr>
                                                                                    <tr>
                                                                                        <td colspan="2">
                                                                                        </td>
                                                                                        <td colspan="4" align='<%= Common.ChangeTDForDefaultLeft()%>' style="padding-top: 20PX">
                                                                                            <asp:GridView ID="GridProductSupplierCode" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                                                                                                BorderStyle="Solid" Width="100%" CssClass="grid" PageSize="5" OnPageIndexChanging="GridProductSupplierCode_PageIndexChanging">
                                                                                                <Columns>
                                                                                                    <asp:TemplateField HeaderText="<%$ Resources:Attendance,Delete %>">
                                                                                                        <ItemTemplate>
                                                                                                            <asp:ImageButton ID="IbtnDeleteSupplier" runat="server" CausesValidation="False"
                                                                                                                CommandArgument='<%# Eval("Supplier_Id") %>' ImageUrl="~/Images/Erase.png" Width="16px"
                                                                                                                ToolTip="<%$ Resources:Attendance,Delete %>" OnCommand="IbtnDeleteSupplier_Command" />
                                                                                                        </ItemTemplate>
                                                                                                        <ItemStyle HorizontalAlign="Center" CssClass="grid" />
                                                                                                    </asp:TemplateField>
                                                                                                    <asp:TemplateField HeaderText="<%$ Resources:Attendance,Supplier Name %>" SortExpression="Name">
                                                                                                        <ItemTemplate>
                                                                                                            <asp:Label ID="lblgvSupplierName" runat="server" Text='<%#Eval("Name") %>' />
                                                                                                        </ItemTemplate>
                                                                                                        <ItemStyle CssClass="grid" Width="60%" />
                                                                                                    </asp:TemplateField>
                                                                                                    <asp:TemplateField HeaderText="<%$ Resources:Attendance, Product Supplier Code %>"
                                                                                                        SortExpression="ProductSupplierCode">
                                                                                                        <ItemTemplate>
                                                                                                            <asp:Label ID="lblgvProductSupplierCode" runat="server" Text='<%#Eval("ProductSupplierCode") %>' /></ItemTemplate>
                                                                                                        <ItemStyle CssClass="grid" />
                                                                                                    </asp:TemplateField>
                                                                                                </Columns>
                                                                                                <AlternatingRowStyle CssClass="InvgridAltRow" />
                                                                                                <HeaderStyle CssClass="Invgridheader" BorderStyle="Solid" BorderColor="#6E6E6E" BorderWidth="1px" />
                                                                                                <PagerStyle CssClass="Invgridheader" />
                                                                                                <RowStyle CssClass="Invgridrow" HorizontalAlign="Center" />
                                                                                            </asp:GridView>
                                                                                            <asp:HiddenField ID="hdnfPSC" runat="server" />
                                                                                        </td>
                                                                                    </tr>
                                                                                </table>
                                                                            </asp:Panel>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </ContentTemplate>
                                                        </cc1:TabPanel>
                                                    </cc1:TabContainer>
                                                </td>
                                            </tr>
                                        </table>
                                        <table width="100%">
                                            <tr>
                                                <td colspan="6" align="center">
                                                    <table>
                                                        <tr>
                                                            <td width="80px">
                                                                <asp:Button ID="btnSave" runat="server" CssClass="buttonCommman" Text="<%$ Resources:Attendance,Save %>"
                                                                    OnClick="btnSave_Click" Visible="false" />
                                                            </td>
                                                            <td style="padding-left: 5px" width="80px">
                                                                <asp:Panel ID="PnlReset" runat="server" DefaultButton="btnReset">
                                                                    <asp:Button ID="btnReset" runat="server" CssClass="buttonCommman" Text="<%$ Resources:Attendance,Reset %>"
                                                                        OnClick="btnReset_Click" />
                                                                </asp:Panel>
                                                            </td>
                                                            <td style="padding-left: 5px" width="80px">
                                                                <asp:Panel ID="PnlCancel" runat="server" DefaultButton="btnCancel">
                                                                    <asp:Button ID="btnCancel" runat="server" CssClass="buttonCommman" Text="<%$ Resources:Attendance,Cancel %>"
                                                                        OnClick="btnCancel_Click" />
                                                                </asp:Panel>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                        </table>
                                    </asp:Panel>
                                    <asp:Panel ID="pnlBin" runat="server" Visible="false">
                                        <asp:Panel ID="pnlBinFilterRecords" runat="server" DefaultButton="btnbingo">
                                            <table width="100%" style="padding-left: 43px">
                                                <tr>
                                                    <td align='<%= Common.ChangeTDForDefaultLeft()%>' width="150px">
                                                        <asp:Label ID="lblBinBrand" runat="server" Text="<%$ Resources:Attendance,Manufacturing Brand  %>"
                                                            CssClass="labelComman"></asp:Label>
                                                    </td>
                                                    <td width="300px">
                                                        <asp:DropDownList ID="ddlBinBrandSearch" runat="server" CssClass="textComman" Width="270px">
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td align='<%= Common.ChangeTDForDefaultLeft()%>' width="120px">
                                                        <asp:Label ID="lblbinCategory" runat="server" Text="<%$ Resources:Attendance,Category %>"
                                                            CssClass="labelComman" />
                                                    </td>
                                                    <td>
                                                        <asp:DropDownList ID="ddlBinCategorySearch" runat="server" CssClass="textComman"
                                                            Width="270px">
                                                        </asp:DropDownList>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td colspan="3">
                                                    </td>
                                                    <td align="right" style="padding-right: 95px">
                                                        <table>
                                                            <tr>
                                                                <td style="padding-right: 20px">
                                                                    <asp:Button ID="btnbingo" runat="server" CausesValidation="False" Text="<%$ Resources:Attendance,Go %>"
                                                                        CssClass="buttonCommman" OnClick="btnbingo_Click" Height="25px" />
                                                                </td>
                                                                <td>
                                                                    <asp:Panel ID="Panel6" runat="server" DefaultButton="btnBinResetSreach">
                                                                        <asp:Button ID="btnBinResetSreach" runat="server" CausesValidation="False" Text="<%$ Resources:Attendance,Reset %>"
                                                                            CssClass="buttonCommman" OnClick="btnBinResetSreach_Click" Height="25px" />
                                                                    </asp:Panel>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                            </table>
                                        </asp:Panel>
                                        <asp:Panel ID="pnlBinSearchRecords" runat="server" DefaultButton="btnbinbind">
                                            <div width="100%" style="background-image: url(../Images/bg_repeat.jpg); background-repeat: repeat;">
                                                <table width="100%" style="padding-left: 20px; height: 38px;">
                                                    <tr>
                                                        <td width="90px">
                                                            <asp:Label ID="lblbinSelectOption" runat="server" CssClass="labelComman" Text="<%$ Resources:Attendance,Select Option %>"></asp:Label>
                                                        </td>
                                                        <td width="170px">
                                                            <asp:DropDownList ID="ddlbinFieldName" runat="server" CssClass="DropdownSearch" Height="25px"
                                                                Width="165px">
                                                                <asp:ListItem Text="<%$ Resources:Attendance,Product Id %>" Value="ProductId"></asp:ListItem>
                                                                <asp:ListItem Text="<%$ Resources:Attendance,Product Name %>" Value="EProductName"
                                                                    Selected="True"></asp:ListItem>
                                                                <asp:ListItem Text="<%$ Resources:Attendance,Model No. %>" Value="ModelNo"></asp:ListItem>
                                                                <asp:ListItem Text="<%$ Resources:Attendance,Item Type %>" Value="ItemType"></asp:ListItem>
                                                            </asp:DropDownList>
                                                        </td>
                                                        <td width="125px">
                                                            <asp:DropDownList ID="ddlbinOption" runat="server" CssClass="DropdownSearch" Height="25px"
                                                                Width="120px">
                                                                <asp:ListItem Text="<%$ Resources:Attendance,--Select-- %>"></asp:ListItem>
                                                                <asp:ListItem Text="<%$ Resources:Attendance,Equal %>"></asp:ListItem>
                                                                <asp:ListItem Text="<%$ Resources:Attendance,Like %>"></asp:ListItem>
                                                                <asp:ListItem Selected="True" Text="<%$ Resources:Attendance,Contains %>"></asp:ListItem>
                                                            </asp:DropDownList>
                                                        </td>
                                                        <td width="24%">
                                                            <asp:TextBox ID="txtbinVal" runat="server" CssClass="textCommanSearch" Height="14px"
                                                                Width="100%" />
                                                        </td>
                                                        <td width="50px" align="center">
                                                            <asp:ImageButton ID="btnbinbind" runat="server" CausesValidation="False" Height="25px"
                                                                ImageUrl="~/Images/search.png" Width="25px" OnClick="btnbinbind_Click" ToolTip="<%$ Resources:Attendance,Search %>" />
                                                        </td>
                                                        <td width="30px" align="center">
                                                            <asp:Panel ID="Panel7" runat="server" DefaultButton="btnRefresh">
                                                                <asp:ImageButton ID="btnbinRefresh" runat="server" CausesValidation="False" Height="25px"
                                                                    ImageUrl="~/Images/refresh.png" Width="25px" OnClick="btnbinRefresh_Click" ToolTip="<%$ Resources:Attendance,Refresh %>">
                                                                </asp:ImageButton>
                                                            </asp:Panel>
                                                        </td>
                                                        <td width="30px" align="center">
                                                            <asp:Panel ID="Panel8" runat="server" DefaultButton="imgBtnbinGrid">
                                                                <asp:ImageButton ID="imgBtnbinGrid" Height="25px" Width="25px" CausesValidation="False"
                                                                    runat="server" ImageUrl="~/Images/a1.png" OnClick="imgBtnbinGrid_Click" ToolTip="<%$ Resources:Attendance, Grid View %>" />
                                                            </asp:Panel>
                                                            <asp:Panel ID="Panel9" runat="server" DefaultButton="imgBtnbinDatalist">
                                                                <asp:ImageButton ID="imgBtnbinDatalist" Height="25px" Width="25px" CausesValidation="False"
                                                                    runat="server" ImageUrl="~/Images/NewTree.png" OnClick="imgbtnbinDatalist_Click"
                                                                    ToolTip="<%$ Resources:Attendance,List View %>" Visible="False" />
                                                            </asp:Panel>
                                                        </td>
                                                        <td width="30px" align="center">
                                                            <asp:Panel ID="Panel3" runat="server" DefaultButton="imgBtnRestore">
                                                                <asp:ImageButton ID="imgBtnRestore" Height="25px" Width="25px" CausesValidation="False"
                                                                    runat="server" ImageUrl="~/Images/active.png" Visible="false" OnClick="imgBtnRestore_Click"
                                                                    ToolTip="<%$ Resources:Attendance, Active %>" />
                                                            </asp:Panel>
                                                        </td>
                                                        <td>
                                                            <asp:Panel ID="Panel4" runat="server" DefaultButton="ImgbtnSelectAll">
                                                                <asp:ImageButton ID="ImgbtnSelectAll" runat="server" OnClick="ImgbtnSelectAll_Click"
                                                                    ToolTip="<%$ Resources:Attendance, Select All %>" Visible="false" AutoPostBack="true"
                                                                    ImageUrl="~/Images/selectAll.png" />
                                                            </asp:Panel>
                                                        </td>
                                                        <td align="center">
                                                            <asp:Label ID="lblbinTotalRecord" runat="server" Text="<%$ Resources:Attendance,Total Records: 0 %>"
                                                                CssClass="labelComman"></asp:Label>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </div>
                                        </asp:Panel>
                                        <asp:Label ID="lblSelectedRecord" runat="server" Visible="false"></asp:Label>
                                        <asp:GridView ID="gvBinProduct" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                                            OnPageIndexChanging="gvBinProduct_PageIndexChanging" CssClass="grid" Width="100%"
                                            DataKeyNames="ProductId" PageSize="<%# SystemParameter.GetPageSize() %>" Visible="false">
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
                                                <asp:TemplateField HeaderText="<%$ Resources:Attendance,Product Id %>">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblProductId" runat="server" Text='<%# Eval("ProductId") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle CssClass="grid" />
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="EProductName" HeaderText="<%$ Resources:Attendance,Product Name %>"
                                                    SortExpression="EProductName" />
                                                <asp:TemplateField HeaderText="<%$ Resources:Attendance,Model No %>">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblProductCode" runat="server" Text='<%# Eval("ModelNo") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle CssClass="grid" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="<%$ Resources:Attendance,Item Type %>">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblItemType" runat="server" Text='<%# GetItemType(Eval("ItemType").ToString()) %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle CssClass="grid" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="<%$ Resources:Attendance, Product Unit %>">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblPUnit" runat="server" Text='<%#  GetUnitName(Eval("UnitId").ToString()) %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle CssClass="grid" />
                                                </asp:TemplateField>
                                            </Columns>
                                            <AlternatingRowStyle CssClass="InvgridAltRow" />
                                            <HeaderStyle CssClass="Invgridheader" BorderStyle="Solid" BorderColor="#6E6E6E" BorderWidth="1px" />
                                            <PagerStyle CssClass="Invgridheader" />
                                            <RowStyle CssClass="Invgridrow" HorizontalAlign="Center" />
                                        </asp:GridView>
                                        <br />
                                        <asp:DataList ID="dtlistbinProduct" runat="server" RepeatColumns="3" RepeatDirection="Horizontal"
                                            Width="100%">
                                            <ItemTemplate>
                                                <div class="product_box">
                                                    <table width="100%">
                                                        <tr>
                                                            <td colspan="4">
                                                                <asp:Label ID="lbldlProductName" runat="server" Font-Bold="true" ForeColor="#1886b9"
                                                                    CssClass="labelComman" Width="310px" Text='<%# Eval("EProductName") %>'></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td valign="top" rowspan="6" width="1px">
                                                                <asp:ImageButton ID="btnImgProduct" runat="server" Width="120px" Height="120px" ImageUrl='<%# "~/Handler.ashx?ImID="+ Eval("ProductId") %>' />
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td width="60px">
                                                                <asp:Label ID="lblProductId" runat="server" Text="<%$ Resources:Attendance,Product Id %>"
                                                                    CssClass="labelComman" Width="60px"></asp:Label>
                                                            </td>
                                                            <td width="3px">
                                                                :
                                                            </td>
                                                            <td align="left">
                                                                <asp:Label ID="lbldlProductId" runat="server" CssClass="labelComman" Width="135px"
                                                                    Text='<%# Eval("ProductId") %>'></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <asp:Label ID="lblModelNo" runat="server" Text="<%$ Resources:Attendance,Model No. %>"
                                                                    CssClass="labelComman"></asp:Label>
                                                            </td>
                                                            <td>
                                                                :
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="lbldltModelNo" runat="server" CssClass="labelComman" Text='<%# Eval("ModelNo") %>'></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <asp:Label ID="lblItypeType" runat="server" Text="<%$ Resources:Attendance,Item Type %>"
                                                                    CssClass="labelComman"></asp:Label>
                                                            </td>
                                                            <td>
                                                                :
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="lbldlItypeType" runat="server" CssClass="labelComman" Text='<%# GetItemType(Eval("ItemType").ToString()) %>'></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <asp:Label ID="lblCostPrice" runat="server" Text="<%$ Resources:Attendance, Cost Price %>"
                                                                    CssClass="labelComman"></asp:Label>
                                                            </td>
                                                            <td>
                                                                :
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="lbldlCostPrice" runat="server" CssClass="labelComman" Text='<%# Eval("CostPrice") %>'></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <asp:Label ID="lblgvProductUnit" runat="server" Text="<%$ Resources:Attendance, Unit %>"
                                                                    CssClass="labelComman"></asp:Label>
                                                            </td>
                                                            <td>
                                                                :
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="lblgvPUnit" runat="server" CssClass="labelComman" Text='<%# GetUnitName(Eval("UnitId").ToString()) %>'></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <asp:CheckBox ID="chkActive" runat="server" AutoPostBack="True" Text="Active" CssClass="labelComman"
                                                                    OnCheckedChanged="chkActive_CheckedChanged" Visible="false" />
                                                                <asp:HiddenField ID="hdnChkActive" runat="server" Value='<%# Eval("ProductId") %>' />
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </div>
                                                <br />
                                            </ItemTemplate>
                                        </asp:DataList>
                                        <div>
                                            <table style="width: 100%; padding-left: 43px">
                                                <tr>
                                                    <td>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="center">
                                                        <table>
                                                            <tr>
                                                                <td>
                                                                    <asp:LinkButton ID="lnkbinFirst" runat="server" Font-Size="Small" Font-Bold="True"
                                                                        ForeColor="#1886b9" CausesValidation="False" OnClick="lnkbinFirst_Click" Style="text-decoration: none">First</asp:LinkButton>
                                                                </td>
                                                                <td width="3px">
                                                                </td>
                                                                <td>
                                                                    <asp:LinkButton ID="lnkbinPrev" runat="server" Font-Bold="True" Font-Size="Small"
                                                                        ForeColor="#1886b9" CausesValidation="False" OnClick="lnkbinPrev_Click" Style="text-decoration: none">Prev</asp:LinkButton>
                                                                </td>
                                                                <td width="3px">
                                                                </td>
                                                                <td>
                                                                    <asp:LinkButton ID="lnkbinNext" runat="server" Font-Size="Small" Font-Bold="True"
                                                                        ForeColor="#1886b9" CausesValidation="False" OnClick="lnkbinNext_Click" Style="text-decoration: none">Next</asp:LinkButton>
                                                                </td>
                                                                <td width="3px">
                                                                </td>
                                                                <td>
                                                                    <asp:LinkButton ID="lnkbinLast" runat="server" Font-Bold="True" Font-Size="Small"
                                                                        ForeColor="#1886b9" CausesValidation="False" OnClick="lnkbinLast_Click" Style="text-decoration: none">Last</asp:LinkButton>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                            </table>
                                        </div>
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
