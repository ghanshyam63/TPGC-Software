<%@ Page Language="C#" MasterPageFile="~/ERPMaster.master" AutoEventWireup="true"
    CodeFile="EmployeeMaster.aspx.cs" Inherits="Master_EmployeeMaster" Title="Employee Master" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <link href="../CSS/InvStyle.css" rel="stylesheet" type="text/css" />
    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
        <Triggers>
            <asp:PostBackTrigger ControlID="btnList" />
            <asp:PostBackTrigger ControlID="btnNew" />
            <asp:PostBackTrigger ControlID="btnBin" />
            
            
            <asp:PostBackTrigger ControlID="btnPen" />
<asp:PostBackTrigger ControlID="btnUpload3" />
<asp:PostBackTrigger ControlID="btnOt7" />
<asp:PostBackTrigger ControlID="btnSal3" />

<asp:PostBackTrigger ControlID="btnNot" />
<asp:PostBackTrigger ControlID="btnLeave" />
            <asp:PostBackTrigger ControlID="dtlistEmp" />
            <asp:PostBackTrigger ControlID="gvEmp" />
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
                                            <td width="200px" style="padding-left: 5px">
                                                <asp:Label ID="lblHeader" runat="server" Text="<%$ Resources:Attendance,Employee Setup %>"
                                                    CssClass="LableHeaderTitle"></asp:Label>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                                <td align="right">
                                    <table cellpadding="0" cellspacing="0">
                                        <tr>
                                            <td>
                                                <asp:Panel ID="pnlMenuList" runat="server" CssClass="a" Width="100%">
                                                    <asp:Button ID="btnList" runat="server" Text="<%$ Resources:Attendance,List %>" Width="80px"
                                                        BorderStyle="none" BackColor="Transparent" OnClick="btnList_Click" Style="padding-left: 20px;
                                                        padding-top: 3px; background-image: url('../Images/List.png' ); background-repeat: no-repeat;
                                                        height: 49px; background-position: 5px 15px; font: bold 14px Trebuchet MS; color: #000000;" />
                                                </asp:Panel>
                                            </td>
                                            <td>
                                                <asp:Panel ID="pnlMenuNew" runat="server" CssClass="a" Width="100%">
                                                    <asp:Button ID="btnNew" runat="server" Text="<%$ Resources:Attendance,New %>" Width="80px"
                                                        BorderStyle="none" BackColor="Transparent" OnClick="btnNew_Click" Style="padding-left: 15px;
                                                        padding-top: 3px; background-image: url('../Images/New.png' ); background-repeat: no-repeat;
                                                        height: 49px; background-position: 5px 15px; font: bold 14px Trebuchet MS; color: #000000;" />
                                                </asp:Panel>
                                            </td>
                                            <td>
                                                <asp:Panel ID="pnlMenuBin" runat="server" CssClass="a" Width="100%">
                                                    <asp:Button ID="btnBin" runat="server" Text="<%$ Resources:Attendance,Bin %>" Width="80px"
                                                        BorderStyle="none" BackColor="Transparent" OnClick="btnBin_Click" Style="padding-left: 10px;
                                                        padding-top: 3px; background-image: url('../Images/Bin.png' ); background-repeat: no-repeat;
                                                        height: 49px; background-position: 5px 15px; font: bold 14px Trebuchet MS; color: #000000;" />
                                                </asp:Panel>
                                            </td>
                                            <td>
                                                <asp:Panel ID="pnlLeave" runat="server" CssClass="a" Width="100%">
                                                    <asp:Button ID="btnLeave" runat="server" Text="<%$ Resources:Attendance,Leave %>"
                                                        Width="90px" BorderStyle="none" BackColor="Transparent" OnClick="btnLeave_Click"
                                                        Style="padding-left: 20px; padding-top: 3px; background-image: url('../Images/Leave.png' );
                                                        background-repeat: no-repeat; height: 49px; background-position: 5px 15px; font: bold 14px Trebuchet MS;
                                                        color: #000000;" />
                                                </asp:Panel>
                                            </td>
                                            <td>
                                                <asp:Panel ID="pnlNotice" runat="server" CssClass="a" Width="100%">
                                                    <asp:Button ID="btnNot" runat="server" Text="<%$ Resources:Attendance,Alert %>"
                                                        Width="80px" BorderStyle="none" BackColor="Transparent" OnClick="btnNotice_Click"
                                                        Style="padding-left: 20px; padding-top: 3px; background-image: url('../Images/alert.png' );
                                                        background-repeat: no-repeat; height: 49px; background-position: 5px 15px; font: bold 14px Trebuchet MS;
                                                        color: #000000;" />
                                                </asp:Panel>
                                            </td>
                                            <td>
                                                <asp:Panel ID="pnlSalary" runat="server" CssClass="a" Width="100%">
                                                    <asp:Button ID="btnSal3" runat="server" Text="<%$ Resources:Attendance,Salary %>"
                                                        Width="80px" BorderStyle="none" BackColor="Transparent" OnClick="btnSalary_Click"
                                                        Style="padding-left: 20px; padding-top: 3px; background-image: url('../Images/salary.png' );
                                                        background-repeat: no-repeat; height: 49px; background-position: 5px 15px; font: bold 14px Trebuchet MS;
                                                        color: #000000;" />
                                                </asp:Panel>
                                            </td>
                                            <td>
                                                <asp:Panel ID="pnlOTPartial" runat="server" CssClass="a" Width="100%">
                                                    <asp:Button ID="btnOt7" runat="server" Text="<%$ Resources:Attendance,OT/PL %>"
                                                        Width="80px" BorderStyle="none" BackColor="Transparent" OnClick="btnOTPartial_Click"
                                                        Style="padding-left: 20px; padding-top: 3px; background-image: url('../Images/otpl.png' );
                                                        background-repeat: no-repeat; height: 49px; background-position: 5px 15px; font: bold 14px Trebuchet MS;
                                                        color: #000000;" />
                                                </asp:Panel>
                                            </td>
                                            <td>
                                                <asp:Panel ID="pnlUpload" runat="server" CssClass="a" Width="100%">
                                                    <asp:Button ID="btnUpload3" runat="server" Text="<%$ Resources:Attendance,Upload %>"
                                                        Width="90px" BorderStyle="none" BackColor="Transparent" OnClick="btnUpload_Click"
                                                        Style="padding-left: 20px; padding-top: 3px; background-image: url('../Images/upload.png' );
                                                        background-repeat: no-repeat; height: 49px; background-position: 5px 15px; font: bold 14px Trebuchet MS;
                                                        color: #000000;" />
                                                </asp:Panel>
                                            </td>
                                            <td>
                                                <asp:Panel ID="pnlPanelty" runat="server" CssClass="a" Width="100%">
                                                    <asp:Button ID="btnPen" runat="server" Text="<%$ Resources:Attendance,Penalty  %>"
                                                        Width="90px" BorderStyle="none" BackColor="Transparent" OnClick="btnPanelty_Click"
                                                        Style="padding-left: 20px; padding-top: 3px; background-image: url('../Images/penalty.png' );
                                                        background-repeat: no-repeat; height: 49px; background-position: 5px 15px; font: bold 14px Trebuchet MS;
                                                        color: #000000;" />
                                                </asp:Panel>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td bgcolor="#ccddee" colspan="4" width="100%" height="800px" valign="top">
                                    <asp:Panel ID="pnlList" runat="server" Visible="false">
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
                                                                <asp:ListItem Text="<%$ Resources:Attendance,Employee Code %>" Value="Emp_Code"></asp:ListItem>
                                                                <asp:ListItem Text="<%$ Resources:Attendance,Employee Name %>" Value="Emp_Name" Selected="True"></asp:ListItem>
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
                                            <asp:GridView ID="gvEmp" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                                                OnPageIndexChanging="gvEmp_PageIndexChanging" CssClass="grid" Width="100%" DataKeyNames="Emp_Id"
                                                PageSize="<%# SystemParameter.GetPageSize() %>" Visible="false">
                                                <Columns>
                                                    <asp:TemplateField HeaderText="<%$ Resources:Attendance,Edit %>">
                                                        <ItemTemplate>
                                                            <asp:ImageButton ID="btnEdit" runat="server" CausesValidation="False" CommandArgument='<%# Eval("Emp_Id") %>'
                                                                ImageUrl="~/Images/edit.png" Visible="false" OnCommand="btnEdit_Command" Width="16px" />
                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Center" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="<%$ Resources:Attendance,Delete %>">
                                                        <ItemTemplate>
                                                            <asp:ImageButton ID="btnDelete" runat="server" CausesValidation="False" CommandArgument='<%# Eval("Emp_Id") %>'
                                                                ImageUrl="~/Images/Erase.png" Visible="false" OnCommand="btnDelete_Command" Width="16px" />
                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Center" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="<%$ Resources:Attendance,Employee Code %>">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblEmpCode" runat="server" Text='<%# Eval("Emp_Code") %>'></asp:Label>
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
                                            <br />
                                            <asp:DataList ID="dtlistEmp" runat="server" RepeatColumns="3" RepeatDirection="Horizontal"
                                                Width="100%">
                                                <ItemTemplate>
                                                    <div class="product_box">
                                                        <table cellpadding="0" cellspacing="0" width="300px">
                                                            <tr>
                                                                <td align='<%= Common.ChangeTDForDefaultLeft()%>' colspan="2">
                                                                    <asp:LinkButton ID="lnkEmpname" runat="server" CausesValidation="false" ForeColor="#1886b9"
                                                                        Font-Bold="true" Enabled="false" CommandArgument='<%# Eval("Emp_Id") %>' OnCommand="lnkEditCommand"
                                                                        Text='<%# GetEmployeeName(Eval("Emp_Id"))%>'></asp:LinkButton>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td align='<%= Common.ChangeTDForDefaultLeft()%>' colspan="2">
                                                                    <asp:Label ID="lblEmalid" runat="server" Text='<%# getEmailId(Eval("Emp_Id")) %>'></asp:Label>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td align="<%= Common.ChangeTDForDefaultLeft()%>" rowspan="7" style="width: 130px">
                                                                    <asp:ImageButton ID="ImgBtnEmp" runat="server" CausesValidation="false" CommandArgument='<%# Eval("Emp_Id") %>'
                                                                        Height="125px" ImageUrl='<%#getImageByEmpId( Eval("Emp_Id")) %>' OnCommand="ImgBtnEmpEdit"
                                                                        Enabled="false" Width="125px" />
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                                                    <asp:Label ID="lblGvEmpId" runat="server" Text='<%# Eval("Emp_Code") %>'></asp:Label>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                                                    <asp:Label ID="lblDesignation" runat="server" Text='<%# getdesg(Eval("Emp_Id")) %>'></asp:Label>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                                                    <asp:Label ID="lblDepartment" runat="server" Text='<%# getdepartment(Eval("Emp_Id")) %>'></asp:Label>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                                                    DOB :
                                                                    <asp:Label ID="Label15" runat="server" Text='<%# getdate(Eval("DOB")) %>' />
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                                                    DOJ :
                                                                    <asp:Label ID="Label16" runat="server" Text='<%# getdate(Eval("DOJ")) %>'></asp:Label>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                                                    <asp:Label ID="lblMobile" runat="server" Text='<%#getMobileNo( Eval("Emp_Id")) %>'></asp:Label>
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
                                    </asp:Panel>
                                    <asp:Panel ID="pnlNewEdit" runat="server" DefaultButton="btnSave">
                                        <table width="100%" style="padding-left: 43px">
                                            <tr>
                                                <td align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                                    <asp:Label ID="lblEmployeeCode" runat="server" CssClass="labelComman" Text="<%$ Resources:Attendance,Employee Code %>"></asp:Label>
                                                </td>
                                                <td width="1px">
                                                    :
                                                </td>
                                                <td align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                                    <asp:TextBox ID="txtEmployeeCode" TabIndex="101" Width="250px" runat="server" CssClass="textComman"
                                                        BackColor="#e3e3e3" />
                                                    <cc1:AutoCompleteExtender ID="txtCompCode_AutoCompleteExtender" runat="server" DelimiterCharacters=""
                                                        Enabled="True" ServiceMethod="GetCompletionListEmployeeCode" ServicePath="" CompletionInterval="100"
                                                        MinimumPrefixLength="1" CompletionSetCount="1" TargetControlID="txtEmployeeCode"
                                                        UseContextKey="True" CompletionListCssClass="completionList" CompletionListItemCssClass="listItem"
                                                        CompletionListHighlightedItemCssClass="itemHighlighted">
                                                    </cc1:AutoCompleteExtender>
                                                </td>
                                                <td>
                                                </td>
                                                <td rowspan="4" colspan="3">
                                                    <table>
                                                        <tr>
                                                            <td width="140px" align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                                                <asp:Label ID="lblEmpLogo" runat="server" CssClass="labelComman" Text="<%$ Resources:Attendance,Image  %>"></asp:Label>
                                                            </td>
                                                            <td width="1px" valign="top">
                                                                :
                                                            </td>
                                                            <td align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                                                <cc1:AsyncFileUpload ID="FULogoPath" TabIndex="102" Width="250px" runat="server"
                                                                    OnUploadedComplete="FULogoPath_UploadedComplete" FailedValidation="False" />
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td colspan="2">
                                                            </td>
                                                            <td align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                                                <asp:Image ID="imgLogo" runat="server" Width="90px" Height="90px" />
                                                                <br />
                                                                &nbsp;&nbsp;
                                                                <asp:Button ID="btnUpload" TabIndex="103" runat="server" Text="<%$ Resources:Attendance,Load %>"
                                                                    CssClass="buttonCommman" OnClick="btnUpload1_Click" Height="15px" />
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td width="180px" align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                                    <asp:Label ID="lblEmployeeName" runat="server" CssClass="labelComman" Text="<%$ Resources:Attendance,Employee Name %>"></asp:Label>
                                                </td>
                                                <td width="1px">
                                                    :
                                                </td>
                                                <td align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                                    <asp:TextBox ID="txtEmployeeName" TabIndex="104" BackColor="#e3e3e3" Width="250px"
                                                        OnTextChanged="txtEmpName_OnTextChanged" AutoPostBack="true" runat="server" CssClass="textComman" />
                                                    <cc1:AutoCompleteExtender ID="AutoCompleteExtender1" runat="server" DelimiterCharacters=""
                                                        Enabled="True" ServiceMethod="GetCompletionListEmployeeName" ServicePath="" CompletionInterval="100"
                                                        MinimumPrefixLength="1" CompletionSetCount="1" TargetControlID="txtEmployeeName"
                                                        UseContextKey="True" CompletionListCssClass="completionList" CompletionListItemCssClass="listItem"
                                                        CompletionListHighlightedItemCssClass="itemHighlighted">
                                                    </cc1:AutoCompleteExtender>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td width="160px" align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                                    <asp:Label ID="lblEmployeeL" runat="server" CssClass="labelComman" Text="<%$ Resources:Attendance,Employee Name(Local) %>"></asp:Label>
                                                </td>
                                                <td width="1px">
                                                    :
                                                </td>
                                                <td align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                                    <asp:TextBox ID="txtEmployeeL" TabIndex="105" Width="250px" runat="server" CssClass="textComman" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td width="160px" align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                                    <asp:Label ID="Label6" runat="server" CssClass="labelComman" Text="<%$ Resources:Attendance,Civil Id %>"></asp:Label>
                                                </td>
                                                <td width="1px">
                                                    :
                                                </td>
                                                <td align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                                    <asp:TextBox ID="txtCivilId" TabIndex="106" Width="250px" runat="server" CssClass="textComman" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                                    <asp:Label ID="lblAddressName" runat="server" CssClass="labelComman" Text="<%$ Resources:Attendance,Address Name %>" />
                                                </td>
                                                <td align="center">
                                                    :
                                                </td>
                                                <td colspan="4" align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                                    <asp:Panel ID="pnlAddress" runat="server" DefaultButton="imgAddAddressName">
                                                        <table>
                                                            <tr>
                                                                <td>
                                                                    <asp:TextBox ID="txtAddressName" runat="server" CssClass="textComman" AutoPostBack="true"
                                                                        TabIndex="106" OnTextChanged="txtAddressName_TextChanged" BackColor="#e3e3e3" />
                                                                    <cc1:AutoCompleteExtender ID="AutoCompleteExtender3" runat="server" DelimiterCharacters=""
                                                                        Enabled="True" ServiceMethod="GetCompletionListAddressName" ServicePath="" CompletionInterval="100"
                                                                        MinimumPrefixLength="1" CompletionSetCount="1" TargetControlID="txtAddressName"
                                                                        UseContextKey="True" CompletionListCssClass="completionList" CompletionListItemCssClass="listItem"
                                                                        CompletionListHighlightedItemCssClass="itemHighlighted">
                                                                    </cc1:AutoCompleteExtender>
                                                                </td>
                                                                <td valign="bottom">
                                                                    <asp:Button ID="imgAddAddressName" runat="server" Text="<%$ Resources:Attendance,Add %>"
                                                                        BackColor="Transparent" BorderStyle="None" Font-Bold="true" CssClass="btnNewAdress"
                                                                        Font-Size="13px" Font-Names=" Arial" Width="92px" Height="32px" OnClick="imgAddAddressName_Click" />
                                                                </td>
                                                                <td valign="bottom">
                                                                    <asp:Button ID="btnAddNewAddress" runat="server" Text="<%$ Resources:Attendance,New %>"
                                                                        BackColor="Transparent" BorderStyle="None" Font-Bold="true" CssClass="btnAddAdress"
                                                                        Font-Size="13px" Font-Names=" Arial" Width="92px" Height="32px" OnClick="btnAddNewAddress_Click" />
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </asp:Panel>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="6" align="center">
                                                    <asp:GridView ID="GvAddressName" runat="server" Width="60%" CssClass="grid" AutoGenerateColumns="False"
                                                        TabIndex="110">
                                                        <AlternatingRowStyle CssClass="InvgridAltRow" />
                                                        <Columns>
                                                            <asp:TemplateField HeaderText="Edit">
                                                                <ItemTemplate>
                                                                    <asp:ImageButton ID="imgBtnAddressEdit" runat="server" CommandArgument='<%# Eval("Trans_Id") %>'
                                                                        ImageUrl="~/Images/edit.png" Width="16px" OnCommand="btnAddressEdit_Command" />
                                                                </ItemTemplate>
                                                                <ItemStyle CssClass="grid" HorizontalAlign="Center" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Delete">
                                                                <ItemTemplate>
                                                                    <asp:ImageButton ID="imgBtnDelete" runat="server" CommandArgument='<%# Eval("Trans_Id") %>'
                                                                        Height="16px" ImageUrl="~/Images/Erase.png" Width="16px" OnCommand="btnAddressDelete_Command" />
                                                                    <%--<cc1:ConfirmButtonExtender ID="ConfirmButtonExtender1" runat="server" ConfirmText="<%$ Resources:Attendance,Are You Sure You Want to Delete? %>"
                                                                        TargetControlID="imgBtnDelete">
                                                                    </cc1:ConfirmButtonExtender>--%>
                                                                </ItemTemplate>
                                                                <ItemStyle CssClass="grid" HorizontalAlign="Center" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="<%$ Resources:Attendance,S No. %>">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblSNo" runat="server" Text='<%#Eval("Trans_Id") %>' />
                                                                </ItemTemplate>
                                                                <ItemStyle CssClass="grid" HorizontalAlign="Center" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="<%$ Resources:Attendance, Address Name %>">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblgvAddressName" runat="server" Text='<%#Eval("Address_Name") %>' />
                                                                </ItemTemplate>
                                                                <ItemStyle CssClass="grid" HorizontalAlign="Center" />
                                                            </asp:TemplateField>
                                                        </Columns>
                                                        <HeaderStyle CssClass="Invgridheader" />
                                                        <PagerStyle CssClass="Invgridheader" />
                                                        <RowStyle CssClass="Invgridrow" HorizontalAlign="Center" />
                                                    </asp:GridView>
                                                    <asp:HiddenField ID="hdnAddressId" runat="server" />
                                                    <asp:HiddenField ID="hdnAddressName" runat="server" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td width="120px" align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                                    <asp:Label ID="Label7" runat="server" CssClass="labelComman" Text="<%$ Resources:Attendance,Email Id %>"></asp:Label>
                                                </td>
                                                <td width="1px">
                                                    :
                                                </td>
                                                <td align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                                    <asp:TextBox ID="txtEmailId" TabIndex="111" Width="250px" runat="server" CssClass="textComman" />
                                                </td>
                                                <td>
                                                </td>
                                                <td colspan="3">
                                                    <table>
                                                        <tr>
                                                            <td width="140px" align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                                                <asp:Label ID="Label8" runat="server" CssClass="labelComman" Text="<%$ Resources:Attendance,Phone No. %>"></asp:Label>
                                                            </td>
                                                            <td width="1px">
                                                                :
                                                            </td>
                                                            <td align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                                                <asp:TextBox ID="txtPhoneNo" TabIndex="112" Width="250px" runat="server" CssClass="textComman" />
                                                                
                                                                 <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender18" runat="server" Enabled="True"
                                                                TargetControlID="txtPhoneNo" ValidChars="1,2,3,4,5,6,7,8,9,0">
                                                            </cc1:FilteredTextBoxExtender>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td width="120px" align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                                    <asp:Label ID="Label1" runat="server" CssClass="labelComman" Text="<%$ Resources:Attendance,Date of Birth %>"></asp:Label>
                                                </td>
                                                <td width="1px">
                                                    :
                                                </td>
                                                <td align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                                    <asp:TextBox ID="txtDob" TabIndex="113" Width="250px" runat="server" CssClass="textComman" />
                                                    <cc1:CalendarExtender ID="CalendarExtender2" runat="server" Enabled="True" TargetControlID="txtDob"
                                                          >
                                                    </cc1:CalendarExtender>
                                                </td>
                                                <td>
                                                </td>
                                                <td colspan="3">
                                                    <table>
                                                        <tr>
                                                            <td width="140px" align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                                                <asp:Label ID="Label2" runat="server" CssClass="labelComman" Text="<%$ Resources:Attendance,Date of Joining %>"></asp:Label>
                                                            </td>
                                                            <td width="1px">
                                                                :
                                                            </td>
                                                            <td align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                                                <asp:TextBox ID="txtDoj" TabIndex="114" Width="250px" runat="server" CssClass="textComman" />
                                                                <cc1:CalendarExtender ID="CalendarExtender1" runat="server" Enabled="True" TargetControlID="txtDoj"
                                                                     >
                                                                </cc1:CalendarExtender>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td width="120px" align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                                    <asp:Label ID="lblCountry" runat="server" CssClass="labelComman" Text="<%$ Resources:Attendance,Department Name %>"></asp:Label>
                                                </td>
                                                <td width="1px">
                                                    :
                                                </td>
                                                <td align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                                    <asp:DropDownList ID="ddlDepartment" TabIndex="115" Width="262px" runat="server"
                                                        CssClass="textComman" />
                                                </td>
                                                <td>
                                                </td>
                                                <td colspan="3">
                                                    <table>
                                                        <tr>
                                                            <td width="140px" align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                                                <asp:Label ID="lblParentCompany" runat="server" CssClass="labelComman" Text="<%$ Resources:Attendance,Designation Name %>"></asp:Label>
                                                            </td>
                                                            <td width="1px">
                                                                :
                                                            </td>
                                                            <td align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                                                <asp:DropDownList ID="ddlDesignation" TabIndex="116" Width="262px" runat="server"
                                                                    CssClass="textComman" />
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td width="120px" align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                                    <asp:Label ID="lblCurrency" runat="server" CssClass="labelComman" Text="<%$ Resources:Attendance,Religion Name %>"></asp:Label>
                                                </td>
                                                <td width="1px">
                                                    :
                                                </td>
                                                <td align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                                    <asp:DropDownList ID="ddlReligion" TabIndex="117" Width="262px" runat="server" CssClass="textComman" />
                                                </td>
                                                <td>
                                                </td>
                                                <td colspan="3">
                                                    <table>
                                                        <tr>
                                                            <td width="140px" align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                                                <asp:Label ID="lblQualification" runat="server" CssClass="labelComman" Text="<%$ Resources:Attendance,Qualification Name %>"></asp:Label>
                                                            </td>
                                                            <td width="1px">
                                                                :
                                                            </td>
                                                            <td align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                                                <asp:DropDownList ID="ddlQualification" TabIndex="118" Width="262px" runat="server"
                                                                    CssClass="textComman" />
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td width="120px" align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                                    <asp:Label ID="lblManager" runat="server" CssClass="labelComman" Text="<%$ Resources:Attendance,Nationality Name %>"></asp:Label>
                                                </td>
                                                <td width="1px">
                                                    :
                                                </td>
                                                <td align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                                    <asp:DropDownList ID="ddlNationality" TabIndex="119" Width="262px" runat="server"
                                                        CssClass="textComman">
                                                    </asp:DropDownList>
                                                </td>
                                                <td>
                                                </td>
                                                <td colspan="3">
                                                    <table>
                                                        <tr>
                                                            <td width="140px" align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                                                <asp:Label ID="Label3" runat="server" CssClass="labelComman" Text="<%$ Resources:Attendance,Employee Type %>"></asp:Label>
                                                            </td>
                                                            <td width="1px">
                                                                :
                                                            </td>
                                                            <td align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                                                <asp:DropDownList ID="ddlEmpType" TabIndex="120" Width="262px" runat="server" CssClass="textComman">
                                                                    <asp:ListItem Text="<%$ Resources:Attendance,On Role %>" Value="On Role"></asp:ListItem>
                                                                    <asp:ListItem Text="<%$ Resources:Attendance,Off Role %>" Value="Off Role"></asp:ListItem>
                                                                </asp:DropDownList>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td width="120px" align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                                    <asp:Label ID="Label4" runat="server" CssClass="labelComman" Text="<%$ Resources:Attendance,Gender %>"></asp:Label>
                                                </td>
                                                <td width="1px">
                                                    :
                                                </td>
                                                <td align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                                    <asp:DropDownList ID="ddlGender" TabIndex="121" Width="262px" runat="server" CssClass="textComman">
                                                        <asp:ListItem Text="<%$ Resources:Attendance,Male %>" Value="M"></asp:ListItem>
                                                        <asp:ListItem Text="<%$ Resources:Attendance,Female %>" Value="F"></asp:ListItem>
                                                    </asp:DropDownList>
                                                </td>
                                                <td>
                                                </td>
                                                <td colspan="3">
                                                    <table>
                                                        <tr>
                                                            <td width="140px" align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                                                <asp:Label ID="Label5" runat="server" CssClass="labelComman" Text="<%$ Resources:Attendance,Termination Date %>"></asp:Label>
                                                            </td>
                                                            <td width="1px">
                                                                :
                                                            </td>
                                                            <td align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                                                <asp:TextBox ID="txtTermDate" TabIndex="122" Width="250px" runat="server" CssClass="textComman" />
                                                                <cc1:CalendarExtender ID="txtTermDate_CalendarExtender" runat="server" Enabled="True"
                                                                    TargetControlID="txtTermDate" >
                                                                </cc1:CalendarExtender>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                            
                                            <tr>
                                            <td colspan="6">
                                             <table width="100%" >
                                            <tr>
                                                <td align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                                    <cc1:TabContainer ID="TabContainer1" runat="server" ActiveTabIndex="0" width="100%" 
                                                        CssClass="Tab">
                                                        <cc1:TabPanel ID="TabGenral" width="100%"  runat="server" HeaderText="<%$ Resources:Attendance,General %>">
                                                            <ContentTemplate>
                                                                <table width="948px" style="background-image: url(../Images/bgs.png);">
                                                                    <tr>
                                                                        <td>
                                                                            <table style="padding-left: 30px; padding-top: 5px" width="948px">
                                                                              <tr>
                                                                               <td align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                                                                   <asp:Label ID="Label76" runat="server" CssClass="labelComman" Text="<%$ Resources:Attendance,Bank Name %>"></asp:Label>
                                                           
                                                                               </td>
                                                                             <td align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                                                             :
                                                                             
                                                                             </td>
                                                                              <td align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                                                               <asp:TextBox ID="txtBankName"  BackColor="#C3C3C3" Width="150px" runat="server" 
                                                                                      CssClass="textComman" />
                                                                               <cc1:AutoCompleteExtender ID="AutoCompleteExtender2" runat="server" DelimiterCharacters=""
                                                                Enabled="True" ServiceMethod="GetCompletionListBankName" ServicePath="" CompletionInterval="100"
                                                                MinimumPrefixLength="1" CompletionSetCount="1" TargetControlID="txtBankName"
                                                                UseContextKey="True" CompletionListCssClass="completionList" CompletionListItemCssClass="listItem"
                                                                CompletionListHighlightedItemCssClass="itemHighlighted">
                                                            </cc1:AutoCompleteExtender>
                                                                              
                                                                              </td>
                                                                           
                                                                              
                                                                    
                                                                            <td align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                                                             <asp:Label ID="Label77" runat="server" CssClass="labelComman" Text="<%$ Resources:Attendance,Account Type %>"></asp:Label>
                                                           
                                                                            
                                                                            </td>
                                                                             <td align='<%= Common.ChangeTDForDefaultLeft()%>'>:</td>
                                                                              <td align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                                                              
                                                                               <asp:DropDownList ID="ddlAcountType"  Width="102px" runat="server" CssClass="textComman">
                                                        <asp:ListItem Value="Current"></asp:ListItem>
                                                        <asp:ListItem Value="Saving"></asp:ListItem>
                                                    </asp:DropDownList>
                                                                              
                                                                              </td>
                                                                             
                                                                              
                                                                            
                                                                             <td align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                                                             
                                                                              <asp:Label ID="Label78" runat="server" CssClass="labelComman" Text="<%$ Resources:Attendance,Account No. %>"></asp:Label>
                                                           
                                                                             
                                                                             </td>
                                                                             <td align='<%= Common.ChangeTDForDefaultLeft()%>'>:</td>
                                                                              <td align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                                                              
                                                                                <asp:TextBox ID="txtAccountNo"  Width="150px" runat="server" CssClass="textComman" />
                                                                            
                                                                              </td>
                                                                              </tr>
                                                                            </table>
                                                                            
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </ContentTemplate>
                                                        </cc1:TabPanel>
                                                        </cc1:TabContainer>
                                            </td>
                                            </tr>
                                            </table>
                                            <tr>
                                                <td align="center" colspan="6">
                                                    <table style="padding-left: 8px">
                                                        <tr>
                                                            <td width="80px">
                                                                <asp:Button ID="btnSave" runat="server" CssClass="buttonCommman" OnClick="btnSave_Click"
                                                                    TabIndex="123" Text="<%$ Resources:Attendance,Save %>" ValidationGroup="a" Visible="false" />
                                                            </td>
                                                            <td width="80px">
                                                                <asp:Button ID="btnReset" runat="server" CausesValidation="False" CssClass="buttonCommman"
                                                                    OnClick="btnReset_Click" TabIndex="124" Text="<%$ Resources:Attendance,Reset %>" />
                                                            </td>
                                                            <td width="80px">
                                                                <asp:Button ID="btnCancel" runat="server" CausesValidation="False" CssClass="buttonCommman"
                                                                    OnClick="btnCancel_Click" TabIndex="125" Text="<%$ Resources:Attendance,Cancel %>" />
                                                            </td>
                                                        </tr>
                                                    </table>
                                                    <asp:HiddenField ID="editid" runat="server" />
                                                </td>
                                            </tr>
                                        </table>
                                    </asp:Panel>
                                    <asp:Panel ID="pnlBin" runat="server" Visible="false">
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
                                                                <asp:ListItem Text="<%$ Resources:Attendance,Employee Code %>" Value="Emp_Code"></asp:ListItem>
                                                                <asp:ListItem Text="<%$ Resources:Attendance,Employee Name %>" Value="Emp_Name" Selected="True"></asp:ListItem>
                                                            </asp:DropDownList>
                                                        </td>
                                                        <td width="125px">
                                                            <asp:DropDownList ID="ddlbinOption" runat="server" CssClass="DropdownSearch" Height="25px"
                                                                Width="120px">
                                                                <asp:ListItem Text="<%$ Resources:Attendance,--Select-- %>"></asp:ListItem>
                                                                <asp:ListItem Text="<%$ Resources:Attendance,Equal %>"></asp:ListItem>
                                                                <asp:ListItem Selected="True" Text="<%$ Resources:Attendance,Contains %>"></asp:ListItem>
                                                                <asp:ListItem Text="<%$ Resources:Attendance,Like %>"></asp:ListItem>
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
                                                            <asp:ImageButton ID="imgBtnRestore" Height="25px" Width="25px" CausesValidation="False"
                                                                runat="server" ImageUrl="~/Images/active.png" OnClick="imgBtnRestore_Click" ToolTip="<%$ Resources:Attendance, Active %>" />
                                                        </td>
                                                        <td>
                                                            <asp:ImageButton ID="ImgbtnSelectAll" runat="server" OnClick="ImgbtnSelectAll_Click"
                                                                ToolTip="<%$ Resources:Attendance, Select All %>" AutoPostBack="true" ImageUrl="~/Images/selectAll.png" />
                                                        </td>
                                                        <td align="center">
                                                            <asp:Label ID="lblbinTotalRecord" runat="server" Text="<%$ Resources:Attendance,Total Records: 0 %>"
                                                                CssClass="labelComman"></asp:Label>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </div>
                                            <asp:Label ID="lblSelectedRecord" runat="server" Visible="false"></asp:Label>
                                            <asp:GridView ID="gvBinEmp" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                                                OnPageIndexChanging="gvBinEmp_PageIndexChanging" CssClass="grid" Width="100%"
                                                DataKeyNames="Emp_Id" PageSize="<%# SystemParameter.GetPageSize() %>" Visible="false">
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
                                            <br />
                                            <asp:DataList ID="dtlistbinEmp" runat="server" HorizontalAlign="Center" RepeatColumns="3"
                                                RepeatDirection="Horizontal" Width="100%">
                                                <ItemTemplate>
                                                    <div class="product_box">
                                                        <table cellpadding="0" cellspacing="0" width="300px">
                                                            <tr>
                                                                <td align='<%= Common.ChangeTDForDefaultLeft()%>' colspan="2">
                                                                    <asp:Label ID="lnkEmpname" runat="server" Font-Bold="true" ForeColor="#1886b9" Text='<%# GetEmployeeName(Eval("Emp_Id"))%>'></asp:Label>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td align='<%= Common.ChangeTDForDefaultLeft()%>' colspan="2">
                                                                    <asp:Label ID="lblEmalid" runat="server" Text='<%# getEmailId(Eval("Emp_Id")) %>'></asp:Label>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td align="<%= Common.ChangeTDForDefaultLeft()%>" rowspan="7" style="width: 130px">
                                                                    <asp:ImageButton ID="ImgBtnEmp" runat="server" CausesValidation="false" CommandArgument='<%# Eval("Emp_Id") %>'
                                                                        Height="125px" ImageUrl='<%#getImageByEmpId( Eval("Emp_Id")) %>' OnCommand="ImgBtnEmpEdit"
                                                                        Width="125px" />
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                                                    <asp:Label ID="lblGvEmpId" runat="server" Text='<%# Eval("Emp_Code") %>'></asp:Label>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                                                    <asp:Label ID="lblDesignation" runat="server" Text='<%# getdesg(Eval("Emp_Id")) %>'></asp:Label>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                                                    <asp:Label ID="lblDepartment" runat="server" Text='<%# getdepartment(Eval("Emp_Id")) %>'></asp:Label>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                                                    DOB :
                                                                    <asp:Label ID="Label15" runat="server" Text='<%# getdate(Eval("DOB")) %>' />
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                                                    DOJ :
                                                                    <asp:Label ID="Label16" runat="server" Text='<%# getdate(Eval("DOJ")) %>'></asp:Label>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                                                    <asp:Label ID="lblMobile" runat="server" Text='<%#getMobileNo( Eval("Emp_Id")) %>'></asp:Label>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td align="<%= Common.ChangeTDForDefaultLeft()%>">
                                                                    <asp:CheckBox ID="chbAcInctive" runat="server" AutoPostBack="True" Checked='<%# checkDatalist(Eval("Emp_Id"))%>'
                                                                        OnCheckedChanged="chbAcInctive_CheckedChanged" Text="Active" Visible="false" />
                                                                    <asp:HiddenField ID="hdnChbActive" runat="server" Value='<%# Eval("Emp_Id")%>' />
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </div>
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
                                    </asp:Panel>
                                    <asp:Panel ID="PnlEmployeeLeave" runat="server" Visible="false">
                                        <table width="100%">
                                            <tr>
                                                <td align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                                    <asp:RadioButton ID="rbtnGroup" CssClass="labelComman" OnCheckedChanged="EmpGroup_CheckedChanged"
                                                        runat="server" Text="<%$ Resources:Attendance,Group %>" Font-Bold="true" GroupName="EmpGroup"
                                                        AutoPostBack="true" />
                                                    <asp:RadioButton ID="rbtnEmp" runat="server" CssClass="labelComman" AutoPostBack="true"
                                                        Text="<%$ Resources:Attendance,Employee %>" GroupName="EmpGroup" Font-Bold="true"
                                                        OnCheckedChanged="EmpGroup_CheckedChanged" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:Panel ID="pnlGroup" runat="server">
                                                        <table width="100%">
                                                            <tr>
                                                                <td>
                                                                    <table width="100%">
                                                                        <tr>
                                                                            <td valign="top" align='<%= Common.ChangeTDForDefaultLeft()%>' width="170px">
                                                                                <asp:ListBox ID="lbxGroup" runat="server" Height="211px" Width="171px" SelectionMode="Multiple"
                                                                                    AutoPostBack="true" OnSelectedIndexChanged="lbxGroup_SelectedIndexChanged" CssClass="list"
                                                                                    Font-Names="Trebuchet MS" Font-Size="Small" ForeColor="Gray"></asp:ListBox>
                                                                            </td>
                                                                            <td valign="top" align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                                                                <asp:GridView ID="gvEmployee" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                                                                                    OnPageIndexChanging="gvEmp1_PageIndexChanging" CssClass="grid" Width="100%" PageSize="<%# SystemParameter.GetPageSize() %>">
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
                                                                    <asp:Label ID="lblEmp" runat="server" Visible="false"></asp:Label>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </asp:Panel>
                                                    <asp:Panel ID="Panel4" runat="server" DefaultButton="imgBtnLeaveBind">
                                                        <div width="100%" style="background-image: url(../Images/bg_repeat.jpg); background-repeat: repeat;">
                                                            <table width="100%" style="padding-left: 20px; height: 38px;">
                                                                <tr>
                                                                    <td width="90px">
                                                                        <asp:Label ID="Label11" runat="server" CssClass="labelComman" Text="<%$ Resources:Attendance,Select Option %>"></asp:Label>
                                                                    </td>
                                                                    <td width="170px">
                                                                        <asp:DropDownList ID="ddlField1" runat="server" CssClass="DropdownSearch" Height="25px"
                                                                            Width="165px">
                                                                            <asp:ListItem Text="<%$ Resources:Attendance,Employee Code %>" Value="Emp_Code"></asp:ListItem>
                                                                            <asp:ListItem Text="<%$ Resources:Attendance,Employee Name %>" Value="Emp_Name" Selected="True"></asp:ListItem>
                                                                        </asp:DropDownList>
                                                                    </td>
                                                                    <td width="125px">
                                                                        <asp:DropDownList ID="ddlOption1" runat="server" CssClass="DropdownSearch" Height="25px"
                                                                            Width="120px">
                                                                            <asp:ListItem Text="<%$ Resources:Attendance,--Select-- %>"></asp:ListItem>
                                                                            <asp:ListItem Text="<%$ Resources:Attendance,Equal %>"></asp:ListItem>
                                                                            <asp:ListItem Selected="True" Text="<%$ Resources:Attendance,Contains %>"></asp:ListItem>
                                                                            <asp:ListItem Text="<%$ Resources:Attendance,Like %>"></asp:ListItem>
                                                                        </asp:DropDownList>
                                                                    </td>
                                                                    <td width="24%">
                                                                        <asp:TextBox ID="txtValue1" runat="server" CssClass="textCommanSearch" Height="14px"
                                                                            Width="100%" />
                                                                    </td>
                                                                    <td width="50px" align="center">
                                                                        <asp:ImageButton ID="imgBtnLeaveBind" runat="server" CausesValidation="False" Height="25px"
                                                                            ImageUrl="~/Images/search.png" Width="25px" OnClick="btnLeavebind_Click" ToolTip="<%$ Resources:Attendance,Search %>" />
                                                                    </td>
                                                                    <td width="30px" align="center">
                                                                        <asp:Panel ID="Panel5" runat="server" DefaultButton="btnRefresh">
                                                                            <asp:ImageButton ID="ImageButton2" runat="server" CausesValidation="False" Height="25px"
                                                                                ImageUrl="~/Images/refresh.png" Width="25px" OnClick="btnLeaveRefresh_Click"
                                                                                ToolTip="<%$ Resources:Attendance,Refresh %>"></asp:ImageButton>
                                                                        </asp:Panel>
                                                                    </td>
                                                                    <td width="30px" align="center">
                                                                    </td>
                                                                    <td width="30px" align="center">
                                                                    </td>
                                                                    <td>
                                                                        <asp:ImageButton ID="ImageButton6" runat="server" OnClick="ImgbtnSelectAll_ClickLeave"
                                                                            ToolTip="<%$ Resources:Attendance, Select All %>" AutoPostBack="true" ImageUrl="~/Images/selectAll.png" />
                                                                    </td>
                                                                    <td align="center">
                                                                        <asp:Label ID="lblTotalRecordsLeave" runat="server" Text="<%$ Resources:Attendance,Total Records: 0 %>"
                                                                            CssClass="labelComman"></asp:Label>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </div>
                                                        <asp:Label ID="lblSelectRecd" runat="server" Visible="false"></asp:Label>
                                                        <asp:GridView ID="gvEmpLeave" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                                                            OnPageIndexChanging="gvEmpLeave_PageIndexChanging" CssClass="grid" Width="100%"
                                                            DataKeyNames="Emp_Id" PageSize="<%# SystemParameter.GetPageSize() %>" Visible="false">
                                                            <Columns>
                                                                <asp:TemplateField>
                                                                    <ItemTemplate>
                                                                        <asp:CheckBox ID="chkgvSelect" runat="server" AutoPostBack="true" OnCheckedChanged="chkgvSelect_CheckedChangedLeave" />
                                                                    </ItemTemplate>
                                                                    <HeaderTemplate>
                                                                        <asp:CheckBox ID="chkgvSelectAll" runat="server" OnCheckedChanged="chkgvSelectAll_CheckedChangedLeave"
                                                                            AutoPostBack="true" />
                                                                    </HeaderTemplate>
                                                                    <ItemStyle HorizontalAlign="Center" />
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="<%$ Resources:Attendance,Edit %>">
                                                                    <ItemTemplate>
                                                                        <asp:ImageButton ID="btnEdit" runat="server" CausesValidation="False" CommandArgument='<%# Eval("Emp_Id") %>'
                                                                            ImageUrl="~/Images/edit.png" Visible="true" OnCommand="btnEditLeave_Command"
                                                                            Width="16px" />
                                                                    </ItemTemplate>
                                                                    <ItemStyle HorizontalAlign="Center" />
                                                                </asp:TemplateField>
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
                                                    <hr />
                                                    <asp:Panel ID="Panel3" runat="server" Width="100%" DefaultButton="btnSaveLeave">
                                                        <table width="100%">
                                                            <tr>
                                                                <td style="padding-top: 5px;">
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td align="<%= Common.ChangeTDForDefaultLeft()%>" width="104">
                                                                    <asp:Label ID="lblLeaveType" runat="server" CssClass="labelComman" Text="<%$ Resources:Attendance,Leave Type %>"></asp:Label>
                                                                </td>
                                                                <td align="<%= Common.ChangeTDForDefaultLeft()%>">
                                                                    :
                                                                </td>
                                                                <td align="<%= Common.ChangeTDForDefaultLeft()%>">
                                                                    <asp:DropDownList ID="ddlLeaveType" runat="server" CssClass="labelComman" TabIndex="1"
                                                                        Width="200px">
                                                                    </asp:DropDownList>
                                                                </td>
                                                                <td align="<%= Common.ChangeTDForDefaultLeft()%>">
                                                                    <asp:Label ID="lblTotalLeave" runat="server" CssClass="labelComman" Text="<%$ Resources:Attendance,Total Leave Day %>"></asp:Label>
                                                                </td>
                                                                <td align="<%= Common.ChangeTDForDefaultLeft()%>">
                                                                    :
                                                                </td>
                                                                <td align="<%= Common.ChangeTDForDefaultLeft()%>">
                                                                    <asp:TextBox ID="txtTotalLeave" runat="server" CssClass="labelComman" TabIndex="2"
                                                                        Width="195px"></asp:TextBox>
                                                                    <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender5" runat="server" Enabled="True"
                                                                        TargetControlID="txtTotalLeave" ValidChars="1,2,3,4,5,6,7,8,9,0,.">
                                                                    </cc1:FilteredTextBoxExtender>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td align="<%= Common.ChangeTDForDefaultLeft()%>">
                                                                    <asp:Label ID="lblPaidLeave" runat="server" CssClass="labelComman" Text="<%$ Resources:Attendance,Paid Leave Day %>"></asp:Label>
                                                                </td>
                                                                <td align="<%= Common.ChangeTDForDefaultLeft()%>">
                                                                    :
                                                                </td>
                                                                <td align="<%= Common.ChangeTDForDefaultLeft()%>">
                                                                    <asp:TextBox ID="txtPaidLeave" runat="server" TabIndex="3" Width="195px" CssClass="labelComman"></asp:TextBox>
                                                                    <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender4" runat="server" Enabled="True"
                                                                        TargetControlID="txtPaidLeave" ValidChars="1,2,3,4,5,6,7,8,9,0,.">
                                                                    </cc1:FilteredTextBoxExtender>
                                                                </td>
                                                                <td align="<%= Common.ChangeTDForDefaultLeft()%>">
                                                                    <asp:Label ID="lblPerOfSal" runat="server" CssClass="labelComman" Text="<%$ Resources:Attendance,Percentage Of Salary %>"></asp:Label>
                                                                </td>
                                                                <td align="<%= Common.ChangeTDForDefaultLeft()%>">
                                                                    :
                                                                </td>
                                                                <td align="<%= Common.ChangeTDForDefaultLeft()%>">
                                                                    <asp:TextBox ID="txtPerSal" runat="server" Width="195px" TabIndex="4" CssClass="labelComman"></asp:TextBox>
                                                                    <cc1:FilteredTextBoxExtender ID="txtMobile_FilteredTextBoxExtender" runat="server"
                                                                        Enabled="True" TargetControlID="txtPerSal" ValidChars="1,2,3,4,5,6,7,8,9,0,.">
                                                                    </cc1:FilteredTextBoxExtender>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td align="<%= Common.ChangeTDForDefaultLeft()%>">
                                                                    <asp:Label ID="lblSchType" runat="server" CssClass="labelComman" Text="<%$ Resources:Attendance,Schedule Type %>"></asp:Label>
                                                                </td>
                                                                <td>
                                                                    :
                                                                </td>
                                                                <td align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                                                    <asp:DropDownList ID="ddlSchType" runat="server" AutoPostBack="True" CssClass="labelComman"
                                                                        OnSelectedIndexChanged="ddlSchedule_SelectedIndexChanged" TabIndex="5" Width="200px">
                                                                        <asp:ListItem Value="0">--Select--</asp:ListItem>
                                                                        <asp:ListItem Value="Monthly">Monthly</asp:ListItem>
                                                                        <asp:ListItem Value="Yearly">Yearly</asp:ListItem>
                                                                    </asp:DropDownList>
                                                                </td>
                                                                <td align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                                                    <asp:CheckBox ID="chkMonthCarry" Visible="false" runat="server" CssClass="labelComman"
                                                                        TabIndex="6" Text="<%$ Resources:Attendance,IsMonthCarry %>" />
                                                                </td>
                                                                <td align="left">
                                                                    &#160;&#160;
                                                                </td>
                                                                <td align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                                                    <asp:CheckBox ID="chkYearCarry" CssClass="labelComman" runat="server" TabIndex="7"
                                                                        Text="<%$ Resources:Attendance,IsYearCarry %>" />
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td colspan="2">
                                                                </td>
                                                                <td colspan="4" align="<%= Common.ChangeTDForDefaultLeft()%>" style="padding-left: 12px;">
                                                                    <asp:Button ID="btnSaveLeave" runat="server" CssClass="buttonCommman" OnClick="btnSaveLeave_Click"
                                                                        TabIndex="8" Text="<%$ Resources:Attendance,Save %>" ValidationGroup="leavesave"
                                                                        Width="65px" />
                                                                    &nbsp;
                                                                    <asp:Button ID="btnResetLeave" runat="server" CausesValidation="False" CssClass="buttonCommman"
                                                                        OnClick="btnResetLeave_Click" TabIndex="9" Text="<%$ Resources:Attendance,Reset %>"
                                                                        Width="65px" />
                                                                    &nbsp;
                                                                    <asp:Button ID="btnCancelLeave" runat="server" CausesValidation="False" CssClass="buttonCommman"
                                                                        OnClick="btnCancelLeave_Click" TabIndex="10" Text="<%$ Resources:Attendance,Cancel %>"
                                                                        Width="65px" />
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td colspan="6">
                                                                    <asp:GridView ID="gridEmpLeave" runat="server" AutoGenerateColumns="False" DataKeyNames="Trans_No"
                                                                        PageSize="<%# SystemParameter.GetPageSize() %>" TabIndex="10" Width="100%">
                                                                        <Columns>
                                                                            <asp:TemplateField HeaderText="<%$ Resources:Attendance,LeaveType %>">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblleavetype0" runat="server" Text='<%# GetLeaveTypeName(Eval("LeaveType_Id")) %>'></asp:Label>
                                                                                    <asp:HiddenField ID="hdnLeaveTypeId" runat="server" Value='<%# Eval("LeaveType_Id") %>' />
                                                                                    <asp:HiddenField ID="hdnTranNo" runat="server" Value='<%# Eval("Trans_No") %>' />
                                                                                    <asp:HiddenField ID="hdnEmpId1" runat="server" Value='<%# Eval("Emp_Id") %>' />
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="<%$ Resources:Attendance,Total Leave %>">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="txtNoOfLeave0" runat="server" Text='<%# Eval("Total_Leave") %>' Width="110px"></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="<%$ Resources:Attendance,Paid Leave %>">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="txtPAidLEave1" runat="server" Text='<%# Eval("Paid_Leave") %>' Width="110px"></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="<%$ Resources:Attendance,Percentage Of Salary %>">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="TextBox1" runat="server" Text='<%# Eval("Percentage_Of_Salary") %>'
                                                                                        Width="125px"></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="<%$ Resources:Attendance,Schedule Type %>">
                                                                                <ItemTemplate>
                                                                                    <asp:DropDownList ID="ddlSchType0" Enabled="false" runat="server" SelectedValue='<%# Eval("Shedule_Type") %>'
                                                                                        Width="110px" AutoPostBack="true" OnSelectedIndexChanged="ddlScheduleGrid_SelectedIndexChanged">
                                                                                        <asp:ListItem Value="Monthly">Monthly</asp:ListItem>
                                                                                        <asp:ListItem Value="Yearly">Yearly</asp:ListItem>
                                                                                    </asp:DropDownList>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="<%$ Resources:Attendance,IsYearCarry %>">
                                                                                <ItemTemplate>
                                                                                    <asp:CheckBox ID="chkYearCarry0" Enabled="false" runat="server" Checked='<%# Eval("Is_YearCarry") %>'
                                                                                        Text="<%$ Resources:Attendance,IsYearCarry %>" />
                                                                                </ItemTemplate>
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
                                                    </asp:Panel>
                                                </td>
                                            </tr>
                                        </table>
                                    </asp:Panel>
                                    <asp:Panel ID="PnlEmpNotification" runat="server" Visible="false">
                                        <table width="100%">
                                            <tr>
                                                <td align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                                    <asp:RadioButton ID="rbtnGroupNF" CssClass="labelComman" OnCheckedChanged="EmpGroupNF_CheckedChanged"
                                                        runat="server" Text="<%$ Resources:Attendance,Group %>" Font-Bold="true" GroupName="EmpGroup"
                                                        AutoPostBack="true" />
                                                    <asp:RadioButton ID="rbtnEmpNF" runat="server" CssClass="labelComman" AutoPostBack="true"
                                                        Text="<%$ Resources:Attendance,Employee %>" GroupName="EmpGroup" Font-Bold="true"
                                                        OnCheckedChanged="EmpGroupNF_CheckedChanged" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:Panel ID="pnlGroupNF" runat="server">
                                                        <table width="100%">
                                                            <tr>
                                                                <td>
                                                                    <table width="100%">
                                                                        <tr>
                                                                            <td valign="top" align='<%= Common.ChangeTDForDefaultLeft()%>' width="170px">
                                                                                <asp:ListBox ID="lbxGroupNF" runat="server" Height="211px" Width="171px" SelectionMode="Multiple"
                                                                                    AutoPostBack="true" OnSelectedIndexChanged="lbxGroupNF_SelectedIndexChanged"
                                                                                    CssClass="list" Font-Names="Trebuchet MS" Font-Size="Small" ForeColor="Gray">
                                                                                </asp:ListBox>
                                                                            </td>
                                                                            <td valign="top" align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                                                                <asp:GridView ID="gvEmployeeNF" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                                                                                    OnPageIndexChanging="gvEmployeeNF_PageIndexChanging" CssClass="grid" Width="100%"
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
                                                                    <asp:Label ID="lblEmpNf" runat="server" Visible="false"></asp:Label>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </asp:Panel>
                                                    <asp:Panel ID="pnlEmpNf" runat="server" DefaultButton="ImageButton1">
                                                        <div width="100%" style="background-image: url(../Images/bg_repeat.jpg); background-repeat: repeat;">
                                                            <table width="100%" style="padding-left: 20px; height: 38px;">
                                                                <tr>
                                                                    <td width="90px">
                                                                        <asp:Label ID="Label17" runat="server" CssClass="labelComman" Text="<%$ Resources:Attendance,Select Option %>"></asp:Label>
                                                                    </td>
                                                                    <td width="170px">
                                                                        <asp:DropDownList ID="ddlFieldNF" runat="server" CssClass="DropdownSearch" Height="25px"
                                                                            Width="165px">
                                                                            <asp:ListItem Text="<%$ Resources:Attendance,Employee Code %>" Value="Emp_Code"></asp:ListItem>
                                                                            <asp:ListItem Text="<%$ Resources:Attendance,Employee Name %>" Value="Emp_Name" Selected="True"></asp:ListItem>
                                                                        </asp:DropDownList>
                                                                    </td>
                                                                    <td width="125px">
                                                                        <asp:DropDownList ID="ddlOptionNF" runat="server" CssClass="DropdownSearch" Height="25px"
                                                                            Width="120px">
                                                                            <asp:ListItem Text="<%$ Resources:Attendance,--Select-- %>"></asp:ListItem>
                                                                            <asp:ListItem Text="<%$ Resources:Attendance,Equal %>"></asp:ListItem>
                                                                            <asp:ListItem Selected="True" Text="<%$ Resources:Attendance,Contains %>"></asp:ListItem>
                                                                            <asp:ListItem Text="<%$ Resources:Attendance,Like %>"></asp:ListItem>
                                                                        </asp:DropDownList>
                                                                    </td>
                                                                    <td width="24%">
                                                                        <asp:TextBox ID="txtValueNF" runat="server" CssClass="textCommanSearch" Height="14px"
                                                                            Width="100%" />
                                                                    </td>
                                                                    <td width="50px" align="center">
                                                                        <asp:ImageButton ID="ImageButton1" runat="server" CausesValidation="False" Height="25px"
                                                                            ImageUrl="~/Images/search.png" Width="25px" OnClick="btnNFbind_Click" ToolTip="<%$ Resources:Attendance,Search %>" />
                                                                    </td>
                                                                    <td width="30px" align="center">
                                                                        <asp:Panel ID="Panel11" runat="server" DefaultButton="btnRefresh">
                                                                            <asp:ImageButton ID="ImageButton3" runat="server" CausesValidation="False" Height="25px"
                                                                                ImageUrl="~/Images/refresh.png" Width="25px" OnClick="btnNFRefresh_Click" ToolTip="<%$ Resources:Attendance,Refresh %>">
                                                                            </asp:ImageButton>
                                                                        </asp:Panel>
                                                                    </td>
                                                                    <td width="30px" align="center">
                                                                    </td>
                                                                    <td width="30px" align="center">
                                                                    </td>
                                                                    <td>
                                                                        <asp:ImageButton ID="ImageButton4" runat="server" OnClick="ImgbtnSelectAll_ClickNF"
                                                                            ToolTip="<%$ Resources:Attendance, Select All %>" AutoPostBack="true" ImageUrl="~/Images/selectAll.png" />
                                                                    </td>
                                                                    <td align="center">
                                                                        <asp:Label ID="lblTotalRecordNF" runat="server" Text="<%$ Resources:Attendance,Total Records: 0 %>"
                                                                            CssClass="labelComman"></asp:Label>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </div>
                                                        <asp:Label ID="lblSelectRecordNF" runat="server" Visible="false"></asp:Label>
                                                        <asp:GridView ID="gvEmpNF" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                                                            OnPageIndexChanging="gvEmpNF_PageIndexChanging" CssClass="grid" Width="100%"
                                                            DataKeyNames="Emp_Id" PageSize="<%# SystemParameter.GetPageSize() %>" Visible="false">
                                                            <Columns>
                                                                <asp:TemplateField>
                                                                    <ItemTemplate>
                                                                        <asp:CheckBox ID="chkgvSelect" runat="server" AutoPostBack="true" OnCheckedChanged="chkgvSelect_CheckedChangedNF" />
                                                                    </ItemTemplate>
                                                                    <HeaderTemplate>
                                                                        <asp:CheckBox ID="chkgvSelectAll" runat="server" OnCheckedChanged="chkgvSelectAll_CheckedChangedNF"
                                                                            AutoPostBack="true" />
                                                                    </HeaderTemplate>
                                                                    <ItemStyle HorizontalAlign="Center" />
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="<%$ Resources:Attendance,Edit %>">
                                                                    <ItemTemplate>
                                                                        <asp:ImageButton ID="btnEdit" runat="server" CausesValidation="False" CommandArgument='<%# Eval("Emp_Id") %>'
                                                                            ImageUrl="~/Images/edit.png" Visible="true" OnCommand="btnEditNF_Command" Width="16px" />
                                                                    </ItemTemplate>
                                                                    <ItemStyle HorizontalAlign="Center" />
                                                                </asp:TemplateField>
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
                                                    <hr />
                                                    <asp:Panel ID="Panel12" runat="server" Width="100%" DefaultButton="Button3">
                                                        <table width="100%" style="background-image: url(../Images/bgs.png); padding-left: 20px;
                                                            padding-right: 20px; padding-bottom: 20px; height: 160px" cellpadding="0" cellspacing="0">
                                                            <tr>
                                                                <td colspan="2" align="<%= Common.ChangeTDForDefaultLeft()%>">
                                                                    <br />
                                                                    <asp:Label ID="Label18" runat="server" ForeColor="#666666" Font-Names="arial" Font-Size="13px"
                                                                        Font-Bold="true" Text="<%$ Resources:Attendance,SMS %>"></asp:Label>
                                                                </td>
                                                                <td style="padding-left: 10px" colspan="2" align="<%= Common.ChangeTDForDefaultLeft()%>">
                                                                    <br />
                                                                    <asp:Label ID="Label19" runat="server" ForeColor="#666666" Font-Bold="true" Font-Names="arial"
                                                                        Font-Size="13px" Text="<%$ Resources:Attendance,Report %>"></asp:Label>
                                                                </td>
                                                            </tr>
                                                            <tr style="background-color: White;">
                                                                <td width="200px" align="<%= Common.ChangeTDForDefaultLeft()%>">
                                                                    <asp:CheckBox ID="chkSMSDocExp" runat="server" Font-Names="Trebuchet MS" Font-Size="Small"
                                                                        ForeColor="Gray" Text="<%$ Resources:Attendance,Document Expiration  %>"></asp:CheckBox>
                                                                </td>
                                                                <td align="<%= Common.ChangeTDForDefaultLeft()%>">
                                                                    <asp:CheckBox ID="ChkSMSPartial" runat="server" Font-Names="Trebuchet MS" Font-Size="Small"
                                                                        ForeColor="Gray" Text="<%$ Resources:Attendance,Partial Leave Fault  %>"></asp:CheckBox>
                                                                </td>
                                                                <td width="200px" style="border-left: solid 1px #c2c2c2; padding-left: 10px" align="<%= Common.ChangeTDForDefaultLeft()%>">
                                                                    <asp:CheckBox ID="ChkRptInOut" runat="server" Font-Names="Trebuchet MS" Font-Size="Small"
                                                                        ForeColor="Gray" Text="<%$ Resources:Attendance,In Out %>"></asp:CheckBox>
                                                                </td>
                                                                <td align="<%= Common.ChangeTDForDefaultLeft()%>">
                                                                    <asp:CheckBox ID="ChkRptSalary" runat="server" Font-Names="Trebuchet MS" Font-Size="Small"
                                                                        ForeColor="Gray" Text="<%$ Resources:Attendance,Salary  %>"></asp:CheckBox>
                                                                </td>
                                                            </tr>
                                                            <tr style="background-color: White;">
                                                                <td align="<%= Common.ChangeTDForDefaultLeft()%>">
                                                                    <asp:CheckBox ID="chkSMSAbsent" runat="server" Font-Names="Trebuchet MS" Font-Size="Small"
                                                                        ForeColor="Gray" Text="<%$ Resources:Attendance,Absent  %>"></asp:CheckBox>
                                                                </td>
                                                                <td align="<%= Common.ChangeTDForDefaultLeft()%>">
                                                                    <asp:CheckBox ID="chkSMSNoClock" runat="server" Font-Names="Trebuchet MS" Font-Size="Small"
                                                                        ForeColor="Gray" Text="<%$ Resources:Attendance,No Clock %>"></asp:CheckBox>
                                                                </td>
                                                                <td align="<%= Common.ChangeTDForDefaultLeft()%>" style="border-left: solid 1px #c2c2c2;
                                                                    padding-left: 10px">
                                                                    <asp:CheckBox ID="ChkRptAbsent" runat="server" Font-Names="Trebuchet MS" Font-Size="Small"
                                                                        ForeColor="Gray" Text="<%$ Resources:Attendance,Absent  %>"></asp:CheckBox>
                                                                </td>
                                                                <td align="<%= Common.ChangeTDForDefaultLeft()%>">
                                                                    <asp:CheckBox ID="ChkRptOvertime" runat="server" Font-Names="Trebuchet MS" Font-Size="Small"
                                                                        ForeColor="Gray" Text="<%$ Resources:Attendance,Over Time %>"></asp:CheckBox>
                                                                </td>
                                                            </tr>
                                                            <tr style="background-color: White;">
                                                                <td align="<%= Common.ChangeTDForDefaultLeft()%>">
                                                                    <asp:CheckBox ID="chkSMSLate" runat="server" Font-Names="Trebuchet MS" Font-Size="Small"
                                                                        ForeColor="Gray" Text="<%$ Resources:Attendance,Late  %>"></asp:CheckBox>
                                                                </td>
                                                                <td>
                                                                </td>
                                                                <td style="border-left: solid 1px #c2c2c2; padding-left: 10px;" align="<%= Common.ChangeTDForDefaultLeft()%>">
                                                                    <asp:CheckBox ID="chkRptLate" runat="server" Font-Names="Trebuchet MS" Font-Size="Small"
                                                                        ForeColor="Gray" Text="<%$ Resources:Attendance,Late %>"></asp:CheckBox>
                                                                </td>
                                                                <td align="<%= Common.ChangeTDForDefaultLeft()%>">
                                                                    <asp:CheckBox ID="chkRptDoc" runat="server" Font-Names="Trebuchet MS" Font-Size="Small"
                                                                        ForeColor="Gray" Text="<%$ Resources:Attendance,Document Expiration %>"></asp:CheckBox>
                                                                </td>
                                                            </tr>
                                                            <tr style="background-color: White;">
                                                                <td align="<%= Common.ChangeTDForDefaultLeft()%>">
                                                                    <asp:CheckBox ID="chkSMSEarly" runat="server" Font-Names="Trebuchet MS" Font-Size="Small"
                                                                        ForeColor="Gray" Text="<%$ Resources:Attendance,Early Out  %>"></asp:CheckBox>
                                                                </td>
                                                                <td>
                                                                </td>
                                                                <td style="border-left: solid 1px #c2c2c2; padding-left: 10px" align="<%= Common.ChangeTDForDefaultLeft()%>">
                                                                    <asp:CheckBox ID="chkRptEarly" runat="server" Font-Names="Trebuchet MS" Font-Size="Small"
                                                                        ForeColor="Gray" Text="<%$ Resources:Attendance,Early Out %>"></asp:CheckBox>
                                                                </td>
                                                                <td align="<%= Common.ChangeTDForDefaultLeft()%>">
                                                                    <asp:CheckBox ID="chkRptViolation" runat="server" Font-Names="Trebuchet MS" Font-Size="Small"
                                                                        ForeColor="Gray" Text="<%$ Resources:Attendance,Violation  %>"></asp:CheckBox>
                                                                </td>
                                                            </tr>
                                                            <tr style="background-color: White;">
                                                                <td align="<%= Common.ChangeTDForDefaultLeft()%>">
                                                                    <asp:CheckBox ID="ChkSmsLeave" runat="server" Font-Names="Trebuchet MS" Font-Size="Small"
                                                                        ForeColor="Gray" Text="<%$ Resources:Attendance,Leave Approved  %>"></asp:CheckBox>
                                                                </td>
                                                                <td>
                                                                </td>
                                                                <td style="border-left: solid 1px #c2c2c2; padding-left: 10px" align="<%= Common.ChangeTDForDefaultLeft()%>">
                                                                    <asp:CheckBox ID="ChkRptLog" runat="server" Font-Names="Trebuchet MS" Font-Size="Small"
                                                                        ForeColor="Gray" Text="<%$ Resources:Attendance,Log %>"></asp:CheckBox>
                                                                </td>
                                                                <td>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td colspan="4" align="center">
                                                                    <asp:Button ID="Button3" runat="server" CssClass="buttonCommman" OnClick="btnSaveNF_Click"
                                                                        TabIndex="8" Text="<%$ Resources:Attendance,Save %>" Width="50px" />
                                                                    &nbsp;
                                                                    <asp:Button ID="Button4" runat="server" CausesValidation="False" CssClass="buttonCommman"
                                                                        OnClick="btnResetNF_Click" TabIndex="9" Text="<%$ Resources:Attendance,Reset %>"
                                                                        Width="60px" />
                                                                    &nbsp;
                                                                    <asp:Button ID="Button5" runat="server" CausesValidation="False" CssClass="buttonCommman"
                                                                        OnClick="btnCancelNF_Click" TabIndex="10" Text="<%$ Resources:Attendance,Cancel %>"
                                                                        Width="60px" />
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </asp:Panel>
                                                </td>
                                            </tr>
                                        </table>
                                    </asp:Panel>
                                    <asp:Panel ID="PnlEmpSalary" runat="server" Visible="false"  >
                                        <table width="100%">
                                            <tr>
                                                <td align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                                    <asp:RadioButton ID="rbtnGroupSal" CssClass="labelComman" OnCheckedChanged="EmpGroupSal_CheckedChanged"
                                                        runat="server" Text="<%$ Resources:Attendance,Group %>" Font-Bold="true" GroupName="EmpGroupSal"
                                                        AutoPostBack="true" />
                                                    <asp:RadioButton ID="rbtnEmpSal" runat="server" CssClass="labelComman" AutoPostBack="true"
                                                        Text="<%$ Resources:Attendance,Employee %>" GroupName="EmpGroupSal" Font-Bold="true"
                                                        OnCheckedChanged="EmpGroupSal_CheckedChanged" />
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
                                                    <asp:Panel ID="pnlEmpSal" runat="server" DefaultButton="ImageButton8">
                                                        <div width="100%" style="background-image: url(../Images/bg_repeat.jpg); background-repeat: repeat;">
                                                            <table width="100%" style="padding-left: 20px; height: 38px;">
                                                                <tr>
                                                                    <td width="90px">
                                                                        <asp:Label ID="Label24" runat="server" CssClass="labelComman" Text="<%$ Resources:Attendance,Select Option %>"></asp:Label>
                                                                    </td>
                                                                    <td width="170px">
                                                                        <asp:DropDownList ID="ddlFieldSal" runat="server" CssClass="DropdownSearch" Height="25px"
                                                                            Width="165px">
                                                                            <asp:ListItem Text="<%$ Resources:Attendance,Employee Code %>" Value="Emp_Code"></asp:ListItem>
                                                                            <asp:ListItem Text="<%$ Resources:Attendance,Employee Name %>" Value="Emp_Name" Selected="True"></asp:ListItem>
                                                                        </asp:DropDownList>
                                                                    </td>
                                                                    <td width="125px">
                                                                        <asp:DropDownList ID="ddlOptionSal" runat="server" CssClass="DropdownSearch" Height="25px"
                                                                            Width="120px">
                                                                            <asp:ListItem Text="<%$ Resources:Attendance,--Select-- %>"></asp:ListItem>
                                                                            <asp:ListItem Text="<%$ Resources:Attendance,Equal %>"></asp:ListItem>
                                                                            <asp:ListItem Selected="True" Text="<%$ Resources:Attendance,Contains %>"></asp:ListItem>
                                                                            <asp:ListItem Text="<%$ Resources:Attendance,Like %>"></asp:ListItem>
                                                                        </asp:DropDownList>
                                                                    </td>
                                                                    <td width="24%">
                                                                        <asp:TextBox ID="txtValueSal" runat="server" CssClass="textCommanSearch" Height="14px"
                                                                            Width="100%" />
                                                                    </td>
                                                                    <td width="50px" align="center">
                                                                        <asp:ImageButton ID="ImageButton8" runat="server" CausesValidation="False" Height="25px"
                                                                            ImageUrl="~/Images/search.png" Width="25px" OnClick="btnSalarybind_Click" ToolTip="<%$ Resources:Attendance,Search %>" />
                                                                    </td>
                                                                    <td width="30px" align="center">
                                                                        <asp:Panel ID="Panel15" runat="server" DefaultButton="btnRefresh">
                                                                            <asp:ImageButton ID="ImageButton9" runat="server" CausesValidation="False" Height="25px"
                                                                                ImageUrl="~/Images/refresh.png" Width="25px" OnClick="btnSalaryRefresh_Click"
                                                                                ToolTip="<%$ Resources:Attendance,Refresh %>"></asp:ImageButton>
                                                                        </asp:Panel>
                                                                    </td>
                                                                    <td width="30px" align="center">
                                                                    </td>
                                                                    <td width="30px" align="center">
                                                                    </td>
                                                                    <td>
                                                                        <asp:ImageButton ID="ImageButton10" runat="server" OnClick="ImgbtnSelectAll_ClickSalary"
                                                                            ToolTip="<%$ Resources:Attendance, Select All %>" AutoPostBack="true" ImageUrl="~/Images/selectAll.png" />
                                                                    </td>
                                                                    <td align="center">
                                                                        <asp:Label ID="lblTotalRecordSal" runat="server" Text="<%$ Resources:Attendance,Total Records: 0 %>"
                                                                            CssClass="labelComman"></asp:Label>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </div>
                                                        <asp:Label ID="lblSelectRecordSal" runat="server" Visible="false"></asp:Label>
                                                        <asp:GridView ID="gvEmpSalary" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                                                            OnPageIndexChanging="gvEmpSal_PageIndexChanging" CssClass="grid" Width="100%"
                                                            DataKeyNames="Emp_Id" PageSize="<%# SystemParameter.GetPageSize() %>" Visible="false">
                                                            <Columns>
                                                                <asp:TemplateField>
                                                                    <ItemTemplate>
                                                                        <asp:CheckBox ID="chkgvSelect" runat="server" AutoPostBack="true" OnCheckedChanged="chkgvSelect_CheckedChangedSal" />
                                                                    </ItemTemplate>
                                                                    <HeaderTemplate>
                                                                        <asp:CheckBox ID="chkgvSelectAll" runat="server" OnCheckedChanged="chkgvSelectAll_CheckedChangedSal"
                                                                            AutoPostBack="true" />
                                                                    </HeaderTemplate>
                                                                    <ItemStyle HorizontalAlign="Center" />
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="<%$ Resources:Attendance,Edit %>">
                                                                    <ItemTemplate>
                                                                        <asp:ImageButton ID="btnEdit" runat="server" CausesValidation="False" CommandArgument='<%# Eval("Emp_Id") %>'
                                                                            ImageUrl="~/Images/edit.png" Visible="true" OnCommand="btnEditSalary_Command"
                                                                            Width="16px" />
                                                                    </ItemTemplate>
                                                                    <ItemStyle HorizontalAlign="Center" />
                                                                </asp:TemplateField>
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
                                                    <hr />
                                                    <asp:Panel ID="pnlEmpSalEnter" runat="server" Width="100%" DefaultButton="Button10">
                                                        <table width="100%">
                                                            <tr>
                                                                <td style="padding-top: 5px;">
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td align="<%= Common.ChangeTDForDefaultLeft()%>">
                                                                    <asp:Label ID="Label27" runat="server" CssClass="labelComman" Text="<%$ Resources:Attendance,Basic Salary %>"></asp:Label>
                                                                </td>
                                                                <td align="<%= Common.ChangeTDForDefaultLeft()%>">
                                                                    :
                                                                </td>
                                                                <td align="<%= Common.ChangeTDForDefaultLeft()%>">
                                                                    <asp:TextBox ID="txtBasic" runat="server" Width="130px" CssClass="labelComman"></asp:TextBox>
                                                                    <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server" Enabled="True"
                                                                        TargetControlID="txtBasic" ValidChars="1,2,3,4,5,6,7,8,9,0,.">
                                                                    </cc1:FilteredTextBoxExtender>
                                                                </td>
                                                                <td align="<%= Common.ChangeTDForDefaultLeft()%>">
                                                                    <asp:Label ID="Label28" runat="server" CssClass="labelComman" Text="<%$ Resources:Attendance,Payment Type %>"></asp:Label>
                                                                </td>
                                                                <td align="<%= Common.ChangeTDForDefaultLeft()%>">
                                                                    :
                                                                </td>
                                                                <td align="<%= Common.ChangeTDForDefaultLeft()%>">
                                                                    <asp:DropDownList ID="ddlPayment" Width="135px" runat="server" CssClass="DropdownSearch">
                                                                        <asp:ListItem Value="Hourly">Hourly</asp:ListItem>
                                                                        <asp:ListItem Value="Daily">Daily</asp:ListItem>
                                                                        <asp:ListItem Value="Weekly">Weekly</asp:ListItem>
                                                                        <asp:ListItem Value="Montly">Monthly</asp:ListItem>
                                                                    </asp:DropDownList>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td align="<%= Common.ChangeTDForDefaultLeft()%>">
                                                                    <asp:Label ID="Label31" runat="server" CssClass="labelComman" Text="<%$ Resources:Attendance,Work Minute %>"></asp:Label>
                                                                </td>
                                                                <td align="<%= Common.ChangeTDForDefaultLeft()%>">
                                                                    :
                                                                </td>
                                                                <td align="<%= Common.ChangeTDForDefaultLeft()%>">
                                                                    <asp:TextBox ID="txtWorkMinute" runat="server" Width="130px" CssClass="labelComman"></asp:TextBox>
                                                                    <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender3" runat="server" Enabled="True"
                                                                        TargetControlID="txtWorkMinute" ValidChars="1,2,3,4,5,6,7,8,9,0,.">
                                                                    </cc1:FilteredTextBoxExtender>
                                                                </td>
                                                                <td align="<%= Common.ChangeTDForDefaultLeft()%>">
                                                                    <asp:Label ID="Label32" runat="server" CssClass="labelComman" Text="<%$ Resources:Attendance,Work Calculation Method %>"></asp:Label>
                                                                </td>
                                                                <td align="<%= Common.ChangeTDForDefaultLeft()%>">
                                                                    :
                                                                </td>
                                                                <td align="<%= Common.ChangeTDForDefaultLeft()%>">
                                                                    <asp:DropDownList ID="ddlWorkCalMethod" Width="135px" runat="server" CssClass="DropdownSearch">
                                                                        <asp:ListItem Value="PairWise">PairWise</asp:ListItem>
                                                                        <asp:ListItem Value="InOut">InOut</asp:ListItem>
                                                                    </asp:DropDownList>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td align="<%= Common.ChangeTDForDefaultLeft()%>">
                                                                    <asp:Label ID="Label29" runat="server" CssClass="labelComman" Text="<%$ Resources:Attendance,Currency %>"></asp:Label>
                                                                </td>
                                                                <td align="<%= Common.ChangeTDForDefaultLeft()%>">
                                                                    :
                                                                </td>
                                                                <td align="<%= Common.ChangeTDForDefaultLeft()%>">
                                                                    <asp:DropDownList ID="ddlCurrency" Width="135px" runat="server" CssClass="DropdownSearch">
                                                                    </asp:DropDownList>
                                                                </td>
                                                            </tr>
                                                            
                                                             <tr>
                                                                <td align="<%= Common.ChangeTDForDefaultLeft()%>">
                                                                    <asp:Label ID="Label79" runat="server" CssClass="labelComman" Text="<%$ Resources:Attendance,Employee In Payroll %>"></asp:Label>
                                                                </td>
                                                                <td align="<%= Common.ChangeTDForDefaultLeft()%>">
                                                                    :
                                                                </td>
                                                                <td align="<%= Common.ChangeTDForDefaultLeft()%>">
                                                                    <asp:CheckBox ID="chkEmpINPayroll"  runat="server" CssClass="labelComman" AutoPostBack="true" OnCheckedChanged="chkEmpINPayroll_OnCheckedChanged" />
                                                                   
                                                                </td>
                                                          </tr>
                                                             <tr>
                                                           
                                                                <td align="<%= Common.ChangeTDForDefaultLeft()%>">
                                                                    <asp:Label ID="Label80" runat="server" CssClass="labelComman" Text="<%$ Resources:Attendance,Employee PF %>"></asp:Label>
                                                                </td>
                                                                <td align="<%= Common.ChangeTDForDefaultLeft()%>">
                                                                    :
                                                                </td>
                                                                <td align="<%= Common.ChangeTDForDefaultLeft()%>">
                                                                    <asp:CheckBox ID="chkEmpPf"  runat="server" CssClass="labelComman" />
                                                                   
                                                                </td>
                                                           
                                                            
                                                            
                                                                <td align="<%= Common.ChangeTDForDefaultLeft()%>">
                                                                    <asp:Label ID="Label81" runat="server" CssClass="labelComman" Text="<%$ Resources:Attendance,Employee ESIC %>"></asp:Label>
                                                                </td>
                                                                <td align="<%= Common.ChangeTDForDefaultLeft()%>">
                                                                    :
                                                                </td>
                                                                <td align="<%= Common.ChangeTDForDefaultLeft()%>">
                                                                    <asp:CheckBox ID="chkEmpEsic"  runat="server" CssClass="labelComman" />
                                                                   
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                </td>
                                                                <td>
                                                                </td>
                                                                <td align="<%= Common.ChangeTDForDefaultLeft()%>" style="padding-left: 10PX;">
                                                                    <asp:Button ID="Button10" runat="server" CssClass="buttonCommman" OnClick="btnSaveSal_Click"
                                                                        Text="<%$ Resources:Attendance,Save %>" Width="50px" />
                                                                    &nbsp;
                                                                    <asp:Button ID="Button11" runat="server" CausesValidation="False" CssClass="buttonCommman"
                                                                        OnClick="btnResetSal_Click" Text="<%$ Resources:Attendance,Reset %>" Width="60px" />
                                                                    &nbsp;
                                                                    <asp:Button ID="Button12" runat="server" CausesValidation="False" CssClass="buttonCommman"
                                                                        OnClick="btnCancelSal_Click" Text="<%$ Resources:Attendance,Cancel %>" Width="60px" />
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </asp:Panel>
                                                </td>
                                            </tr>
                                        </table>
                                    </asp:Panel>
                                    <asp:Panel ID="pnlEmpUpload" runat="server" Visible="false">
                                        <table width="100%">
                                            <tr>
                                                <td align="left">
                                                    <asp:HyperLink CssClass="labelComman" ID="HyperLink1" runat="server" NavigateUrl="~/CompanyResource/EmpWithNesField.xls"
                                                        Text="<%$ Resources:Attendance,Download Excel Format For Necassary Field%>"></asp:HyperLink>
                                                </td>
                                                <td align="right">
                                                    <asp:HyperLink CssClass="labelComman" ID="HyperLink2" runat="server" NavigateUrl="~/CompanyResource/EmpWithAllField.xlsx"
                                                        Text="<%$ Resources:Attendance,Download Excel Format For All Field%>"></asp:HyperLink>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="2">
                                                    <asp:Panel ID="pnlUpload1" runat="server" Width="100%" DefaultButton="btnConnect" HorizontalAlign="Center">
                                                        <table width="100%">
                                                            <tr>
                                                                <td style="width: 254px">
                                                                    &nbsp;
                                                                </td>
                                                                <td align="left" width="120">
                                                                    <asp:Label runat="server" CssClass="labelComman" Text="Browse Excel File" ID="Label66"></asp:Label>
                                                                </td>
                                                                <td style="width: 2px">
                                                                    :
                                                                </td>
                                                                <td align="left">
                                                                    <cc1:AsyncFileUpload CssClass="labelComman" ID="fileLoad" runat="server" Width="217px" />
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td style="width: 254px">
                                                                    &nbsp;
                                                                </td>
                                                                <td style="width: 91px">
                                                                    &nbsp;
                                                                </td>
                                                                <td style="width: 2px">
                                                                    &nbsp;
                                                                </td>
                                                                <td style="padding-left: 12px;" align="left">
                                                                    <asp:Button ID="btnConnect" CssClass="buttonCommman" runat="server" CausesValidation="False"
                                                                        OnClick="btnConnect_Click" Text="<%$ Resources:Attendance,Connect To DataBase %>"
                                                                        Width="217px" />
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td style="width: 254px">
                                                                    &nbsp;
                                                                </td>
                                                                <td align="left" style="width: 91px">
                                                                    <asp:Label ID="Label67" CssClass="labelComman" runat="server" Text="Select Sheet"></asp:Label>
                                                                </td>
                                                                <td style="width: 2px">
                                                                    :
                                                                </td>
                                                                <td align="left">
                                                                    <asp:DropDownList ID="ddlTables" CssClass="dropdownsearch" runat="server" Height="21px"
                                                                        Width="217px">
                                                                    </asp:DropDownList>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td style="width: 254px">
                                                                    &nbsp;
                                                                </td>
                                                                <td style="width: 91px">
                                                                    &nbsp;
                                                                </td>
                                                                <td style="width: 2px">
                                                                    &nbsp;
                                                                </td>
                                                                <td align="left">
                                                                    <asp:CheckBox ID="chkNecField" CssClass="labelComman" runat="server" Text="<%$ Resources:Attendance,Only Necessary Field%>" />
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td style="width: 254px">
                                                                    &nbsp;
                                                                </td>
                                                                <td style="width: 91px">
                                                                    &nbsp;
                                                                </td>
                                                                <td style="width: 2px">
                                                                    &nbsp;
                                                                </td>
                                                                <td style="padding-left: 12px;" align="left">
                                                                    <asp:Button ID="btnviewcolumns" CssClass="buttonCommman" runat="server" CausesValidation="False"
                                                                        OnClick="btnviewcolumns_Click" Text="<%$ Resources:Attendance,Map Column %>"
                                                                        Width="217px" />
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </asp:Panel>
                                                    <asp:Panel ID="pnlMap" runat="server" Width="100%" ScrollBars="Both" Height="500px"
                                                        HorizontalAlign="Center">
                                                        <table width="100%">
                                                            <tr>
                                                                <td align="center">
                                                                    <asp:Button ID="btnUploadTemp" CssClass="buttonCommman" runat="server" OnClick="btnUpload_Click2"
                                                                        Text="<%$ Resources:Attendance,Show Data %>" Width="105px" />
                                                                    <asp:Button ID="btncancel1" CssClass="buttonCommman" runat="server" OnClick="btncancel_Click"
                                                                        Text="<%$ Resources:Attendance,Reset %>" />
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td align="center">
                                                                    <asp:GridView ID="gvFieldMapping" runat="server" CssClass="grid" AutoGenerateColumns="False"
                                                                        DataKeyNames="Nec" OnRowDataBound="gvFieldMapping_RowDataBound">
                                                                        <Columns>
                                                                            <asp:TemplateField>
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblCompulsery" runat="server"></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="<%$ Resources:Attendance,Column %>">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblColName" runat="server" Text='<%# Eval("Column_Name") %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="<%$ Resources:Attendance,Type %>">
                                                                                <ItemTemplate>
                                                                                    <asp:DropDownList ID="ddlExcelCol" runat="server">
                                                                                    </asp:DropDownList>
                                                                                </ItemTemplate>
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
                                                        &nbsp;</asp:Panel>
                                                    <asp:Panel ID="pnlshowdata" runat="server" Height="500px" ScrollBars="Both" Width="100%"
                                                        HorizontalAlign="left">
                                                        <table width="100%">
                                                            <tr>
                                                                <td align="center">
                                                                    <asp:DropDownList ID="ddlFiltercol" CssClass="dropdownsearch" Height="25px" runat="server">
                                                                    </asp:DropDownList>
                                                                    <asp:TextBox ID="txtfiltercol" CssClass="textComman" runat="server"></asp:TextBox>
                                                                    &nbsp;&nbsp;
                                                                    <asp:Button ID="btnFilter" CssClass="buttonCommman" runat="server" OnClick="btnFilter_Click"
                                                                        Text="<%$ Resources:Attendance,Filter %>" />
                                                                    &nbsp;&nbsp;
                                                                    <asp:Button ID="btnresetgv" CssClass="buttonCommman" runat="server" OnClick="btnresetgv_Click"
                                                                        Text="<%$ Resources:Attendance,Reset %>" />
                                                                    &nbsp;&nbsp;
                                                                    <asp:Button ID="btnBackToMapData" Width="120px" CssClass="buttonCommman" runat="server"
                                                                        Text="<%$ Resources:Attendance,Back To FileUpload %>" OnClick="btnBackToMapData_Click" />
                                                                    <br />
                                                                    <asp:Button ID="Button21" runat="server" CssClass="buttonCommman" OnClick="btnUpload_Click1"
                                                                        Text="<%$ Resources:Attendance,Upload Data %>" Width="107px" />
                                                                    <asp:GridView ID="gvSelected" runat="server" Width="100%" CssClass="grid">
                                                                        <AlternatingRowStyle CssClass="InvgridAltRow" />
                                                                        <HeaderStyle CssClass="Invgridheader" BorderStyle="Solid" BorderColor="#6E6E6E" BorderWidth="1px" />
                                                                        <PagerStyle CssClass="Invgridheader" />
                                                                        <RowStyle CssClass="Invgridrow" HorizontalAlign="Center" />
                                                                    </asp:GridView>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </asp:Panel>
                                                </td>
                                            </tr>
                                        </table>
                                    </asp:Panel>
                                    <asp:Panel ID="pnlOTPL" runat="server" Visible="false">
                                        <table width="100%">
                                            <tr>
                                                <td align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                                    <asp:RadioButton ID="rbtnGroupOT" CssClass="labelComman" OnCheckedChanged="EmpGroupOT_CheckedChanged"
                                                        runat="server" Text="<%$ Resources:Attendance,Group %>" Font-Bold="true" GroupName="EmpGroupOT"
                                                        AutoPostBack="true" />
                                                    <asp:RadioButton ID="rbtnEmpOT" runat="server" CssClass="labelComman" AutoPostBack="true"
                                                        Text="<%$ Resources:Attendance,Employee %>" GroupName="EmpGroupOT" Font-Bold="true"
                                                        OnCheckedChanged="EmpGroupOT_CheckedChanged" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:Panel ID="pnlGroupOT" runat="server">
                                                        <table width="100%">
                                                            <tr>
                                                                <td>
                                                                    <table width="100%">
                                                                        <tr>
                                                                            <td valign="top" align='<%= Common.ChangeTDForDefaultLeft()%>' width="170px">
                                                                                <asp:ListBox ID="lbxGroupOT" runat="server" Height="211px" Width="171px" SelectionMode="Multiple"
                                                                                    AutoPostBack="true" OnSelectedIndexChanged="lbxGroupOT_SelectedIndexChanged"
                                                                                    CssClass="list" Font-Names="Trebuchet MS" Font-Size="Small" ForeColor="Gray">
                                                                                </asp:ListBox>
                                                                            </td>
                                                                            <td valign="top" align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                                                                <asp:GridView ID="gvEmployeeOT" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                                                                                    OnPageIndexChanging="gvEmployeeOT_PageIndexChanging" CssClass="grid" Width="100%"
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
                                                                    <asp:Label ID="lblEmpOT" runat="server" Visible="false"></asp:Label>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </asp:Panel>
                                                    <asp:Panel ID="pnlEmpOT" runat="server" DefaultButton="ImageButton12">
                                                        <div width="100%" style="background-image: url(../Images/bg_repeat.jpg); background-repeat: repeat;">
                                                            <table width="100%" style="padding-left: 20px; height: 38px;">
                                                                <tr>
                                                                    <td width="90px">
                                                                        <asp:Label ID="Label33" runat="server" CssClass="labelComman" Text="<%$ Resources:Attendance,Select Option %>"></asp:Label>
                                                                    </td>
                                                                    <td width="170px">
                                                                        <asp:DropDownList ID="ddlFieldOT" runat="server" CssClass="DropdownSearch" Height="25px"
                                                                            Width="165px">
                                                                            <asp:ListItem Text="<%$ Resources:Attendance,Employee Code %>" Value="Emp_Code"></asp:ListItem>
                                                                            <asp:ListItem Text="<%$ Resources:Attendance,Employee Name %>" Value="Emp_Name" Selected="True"></asp:ListItem>
                                                                        </asp:DropDownList>
                                                                    </td>
                                                                    <td width="125px">
                                                                        <asp:DropDownList ID="ddlOptionOT" runat="server" CssClass="DropdownSearch" Height="25px"
                                                                            Width="120px">
                                                                            <asp:ListItem Text="<%$ Resources:Attendance,--Select-- %>"></asp:ListItem>
                                                                            <asp:ListItem Text="<%$ Resources:Attendance,Equal %>"></asp:ListItem>
                                                                            <asp:ListItem Selected="True" Text="<%$ Resources:Attendance,Contains %>"></asp:ListItem>
                                                                            <asp:ListItem Text="<%$ Resources:Attendance,Like %>"></asp:ListItem>
                                                                        </asp:DropDownList>
                                                                    </td>
                                                                    <td width="24%">
                                                                        <asp:TextBox ID="txtValueOT" runat="server" CssClass="textCommanSearch" Height="14px"
                                                                            Width="100%" />
                                                                    </td>
                                                                    <td width="50px" align="center">
                                                                        <asp:ImageButton ID="ImageButton12" runat="server" CausesValidation="False" Height="25px"
                                                                            ImageUrl="~/Images/search.png" Width="25px" OnClick="btnOTbind_Click" ToolTip="<%$ Resources:Attendance,Search %>" />
                                                                    </td>
                                                                    <td width="30px" align="center">
                                                                        <asp:Panel ID="Panel19" runat="server" DefaultButton="btnRefresh">
                                                                            <asp:ImageButton ID="ImageButton13" runat="server" CausesValidation="False" Height="25px"
                                                                                ImageUrl="~/Images/refresh.png" Width="25px" OnClick="btnOTRefresh_Click" ToolTip="<%$ Resources:Attendance,Refresh %>">
                                                                            </asp:ImageButton>
                                                                        </asp:Panel>
                                                                    </td>
                                                                    <td width="30px" align="center">
                                                                    </td>
                                                                    <td width="30px" align="center">
                                                                    </td>
                                                                    <td>
                                                                        <asp:ImageButton ID="ImageButton14" runat="server" OnClick="ImgbtnSelectAll_ClickOT"
                                                                            ToolTip="<%$ Resources:Attendance, Select All %>" AutoPostBack="true" ImageUrl="~/Images/selectAll.png" />
                                                                    </td>
                                                                    <td align="center">
                                                                        <asp:Label ID="lblTotalRecordOT" runat="server" Text="<%$ Resources:Attendance,Total Records: 0 %>"
                                                                            CssClass="labelComman"></asp:Label>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </div>
                                                        <asp:Label ID="lblSelectRecordOT" runat="server" Visible="false"></asp:Label>
                                                        <asp:GridView ID="gvEmpOT" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                                                            OnPageIndexChanging="gvEmpOT_PageIndexChanging" CssClass="grid" Width="100%"
                                                            DataKeyNames="Emp_Id" PageSize="<%# SystemParameter.GetPageSize() %>" Visible="false">
                                                            <Columns>
                                                                <asp:TemplateField>
                                                                    <ItemTemplate>
                                                                        <asp:CheckBox ID="chkgvSelect" runat="server" AutoPostBack="true" OnCheckedChanged="chkgvSelect_CheckedChangedOT" />
                                                                    </ItemTemplate>
                                                                    <HeaderTemplate>
                                                                        <asp:CheckBox ID="chkgvSelectAll" runat="server" OnCheckedChanged="chkgvSelectAll_CheckedChangedOT"
                                                                            AutoPostBack="true" />
                                                                    </HeaderTemplate>
                                                                    <ItemStyle HorizontalAlign="Center" />
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="<%$ Resources:Attendance,Edit %>">
                                                                    <ItemTemplate>
                                                                        <asp:ImageButton ID="btnEdit" runat="server" CausesValidation="False" CommandArgument='<%# Eval("Emp_Id") %>'
                                                                            ImageUrl="~/Images/edit.png" Visible="true" OnCommand="btnEditOT_Command" Width="16px" />
                                                                    </ItemTemplate>
                                                                    <ItemStyle HorizontalAlign="Center" />
                                                                </asp:TemplateField>
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
                                                    <hr />
                                                    <asp:Panel ID="Panel20" runat="server" Width="100%" DefaultButton="Button16">
                                                        <table width="100%">
                                                            <tr>
                                                                <td style="padding-top: 5px;">
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                                                    <fieldset style="height: 190px">
                                                                        <legend>
                                                                            <asp:Label ID="Label48" Font-Bold="true" Font-Size="13px" Font-Names="Arial" runat="server"
                                                                                Text="<%$ Resources:Attendance,Partial Leave %>"></asp:Label></legend>
                                                                        <table width="100%">
                                                                            <tr>
                                                                                <td width="230px" align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                                                                    <asp:Label ID="Label35" runat="server" CssClass="labelComman" Text="<%$ Resources:Attendance,Partial Leave Functionality %>"></asp:Label>
                                                                                </td>
                                                                                <td width="1px">
                                                                                    :
                                                                                </td>
                                                                                <td align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                                                                    <asp:RadioButton ID="rbtnPartialEnable" GroupName="Partial" Text="<%$ Resources:Attendance,Enable %>"
                                                                                        runat="server" CssClass="labelComman" AutoPostBack="true" OnCheckedChanged="rbtPartial_OnCheckedChanged" />
                                                                                    &nbsp;&nbsp;
                                                                                    <asp:RadioButton ID="rbtnPartialDisable" GroupName="Partial" Text="<%$ Resources:Attendance,Disable %>"
                                                                                        runat="server" CssClass="labelComman" AutoPostBack="true" OnCheckedChanged="rbtPartial_OnCheckedChanged" />
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                                                                    <asp:Label ID="Label36" runat="server" CssClass="labelComman" Text="<%$ Resources:Attendance,Total Minutes %>"></asp:Label>
                                                                                </td>
                                                                                <td width="1px">
                                                                                    :
                                                                                </td>
                                                                                <td align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                                                                    <asp:TextBox ID="txtTotalMinutes" Width="150px" runat="server" CssClass="textComman" />
                                                                                    <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender8" runat="server" Enabled="True"
                                                                                        TargetControlID="txtTotalMinutes" ValidChars="1,2,3,4,5,6,7,8,9,0">
                                                                                    </cc1:FilteredTextBoxExtender>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                                                                    <asp:Label ID="Label41" runat="server" CssClass="labelComman" Text="<%$ Resources:Attendance,Minute Use in a Day %>"></asp:Label>
                                                                                </td>
                                                                                <td width="1px">
                                                                                    :
                                                                                </td>
                                                                                <td align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                                                                    <asp:TextBox ID="txtMinuteday" Width="150px" runat="server" CssClass="textComman" />
                                                                                    <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender9" runat="server" Enabled="True"
                                                                                        TargetControlID="txtMinuteday" ValidChars="1,2,3,4,5,6,7,8,9,0">
                                                                                    </cc1:FilteredTextBoxExtender>
                                                                                </td>
                                                                            </tr>
                                                                            <tr id="Tr1" runat="server" visible="false">
                                                                                <td align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                                                                    <asp:Label ID="Label42" runat="server" CssClass="labelComman" Text="<%$ Resources:Attendance,Carry Forward Minutes %>"></asp:Label>
                                                                                </td>
                                                                                <td width="1px">
                                                                                    :
                                                                                </td>
                                                                                <td align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                                                                    <asp:RadioButton ID="rbtnCarryYes" GroupName="SMS" Text="<%$ Resources:Attendance,Yes %>"
                                                                                        runat="server" CssClass="labelComman" />
                                                                                    &nbsp;&nbsp;
                                                                                    <asp:RadioButton ID="rbtnCarryNo" GroupName="SMS" Text="<%$ Resources:Attendance,No %>"
                                                                                        runat="server" CssClass="labelComman" />
                                                                                </td>
                                                                            </tr>
                                                                        </table>
                                                                    </fieldset>
                                                                </td>
                                                                <td>
                                                                    <fieldset style="height: 190px">
                                                                        <legend>
                                                                            <asp:Label ID="Label49" Font-Bold="true" Font-Size="13px" Font-Names="Arial" runat="server"
                                                                                Text="<%$ Resources:Attendance,Over Time %>"></asp:Label></legend>
                                                                        <table width="100%">
                                                                            <tr>
                                                                                <td width="230px" align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                                                                    <asp:Label ID="Label43" runat="server" CssClass="labelComman" Text="<%$ Resources:Attendance,Over Time Functionality %>"></asp:Label>
                                                                                </td>
                                                                                <td width="1px">
                                                                                    :
                                                                                </td>
                                                                                <td align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                                                                    <asp:RadioButton ID="rbtnOTEnable" GroupName="OT" Text="<%$ Resources:Attendance,Enable %>"
                                                                                        runat="server" CssClass="labelComman" AutoPostBack="true" OnCheckedChanged="rbtOT_OnCheckedChanged" />
                                                                                    &nbsp;&nbsp;
                                                                                    <asp:RadioButton ID="rbtnOTDisable" GroupName="OT" Text="<%$ Resources:Attendance,Disable %>"
                                                                                        runat="server" CssClass="labelComman" AutoPostBack="true" OnCheckedChanged="rbtOT_OnCheckedChanged" />
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td width="230px" align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                                                                    <asp:Label ID="Label44" runat="server" CssClass="labelComman" Text="<%$ Resources:Attendance,Calculation Method %>"></asp:Label>
                                                                                </td>
                                                                                <td width="1px">
                                                                                    :
                                                                                </td>
                                                                                <td align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                                                                    <asp:DropDownList ID="ddlOTCalc" Width="135px" runat="server" CssClass="DropdownSearch">
                                                                                        <asp:ListItem Text="In" Value="In"></asp:ListItem>
                                                                                        <asp:ListItem Text="Out" Value="Out"></asp:ListItem>
                                                                                        <asp:ListItem Text="Both" Value="Both"></asp:ListItem>
                                                                                        <asp:ListItem Text="Work Hour" Value="Work Hour"></asp:ListItem>
                                                                                    </asp:DropDownList>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td width="230px" align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                                                                    <asp:Label ID="Label45" runat="server" CssClass="labelComman" Text="<%$ Resources:Attendance,Value(Normal Day) %>"></asp:Label>
                                                                                </td>
                                                                                <td width="1px">
                                                                                    :
                                                                                </td>
                                                                                <td align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                                                                    <asp:TextBox ID="txtNoralType" Width="45px" runat="server" CssClass="textComman" />&nbsp;
                                                                                    <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender10" runat="server" Enabled="True"
                                                                                        TargetControlID="txtNoralType" ValidChars="1,2,3,4,5,6,7,8,9,0,.">
                                                                                    </cc1:FilteredTextBoxExtender>
                                                                                    <asp:DropDownList ID="ddlNormalType" Width="70px" runat="server" CssClass="DropdownSearch">
                                                                                        <asp:ListItem Text="Fixed" Value="1"></asp:ListItem>
                                                                                        <asp:ListItem Text="%" Value="2"></asp:ListItem>
                                                                                    </asp:DropDownList>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td width="230px" align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                                                                    <asp:Label ID="Label46" runat="server" CssClass="labelComman" Text="<%$ Resources:Attendance,Value(Week Off) %>"></asp:Label>
                                                                                </td>
                                                                                <td width="1px">
                                                                                    :
                                                                                </td>
                                                                                <td align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                                                                    <asp:TextBox ID="txtWeekOffValue" Width="45px" runat="server" CssClass="textComman" />&nbsp;
                                                                                    <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender11" runat="server" Enabled="True"
                                                                                        TargetControlID="txtWeekOffValue" ValidChars="1,2,3,4,5,6,7,8,9,0,.">
                                                                                    </cc1:FilteredTextBoxExtender>
                                                                                    <asp:DropDownList ID="ddlWeekOffType" Width="70px" runat="server" CssClass="DropdownSearch">
                                                                                        <asp:ListItem Text="Fixed" Value="1"></asp:ListItem>
                                                                                        <asp:ListItem Text="%" Value="2"></asp:ListItem>
                                                                                    </asp:DropDownList>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td width="230px" align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                                                                    <asp:Label ID="Label47" runat="server" CssClass="labelComman" Text="<%$ Resources:Attendance,Value(Holiday) %>"></asp:Label>
                                                                                </td>
                                                                                <td width="1px">
                                                                                    :
                                                                                </td>
                                                                                <td align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                                                                    <asp:TextBox ID="txtHolidayValue" Width="45px" runat="server" CssClass="textComman" />&nbsp;
                                                                                    <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender12" runat="server" Enabled="True"
                                                                                        TargetControlID="txtHolidayValue" ValidChars="1,2,3,4,5,6,7,8,9,0,.">
                                                                                    </cc1:FilteredTextBoxExtender>
                                                                                    <asp:DropDownList ID="ddlHolidayType" Width="70px" runat="server" CssClass="DropdownSearch">
                                                                                        <asp:ListItem Text="Fixed" Value="1"></asp:ListItem>
                                                                                        <asp:ListItem Text="%" Value="2"></asp:ListItem>
                                                                                    </asp:DropDownList>
                                                                                </td>
                                                                            </tr>
                                                                        </table>
                                                                    </fieldset>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <asp:HiddenField ID="hdnEmpIdOt" runat="server" />
                                                                <td align="center" colspan="6" style="padding-bottom: 10PX;">
                                                                    <asp:Button ID="Button16" runat="server" CssClass="buttonCommman" OnClick="btnSaveOT_Click"
                                                                        Text="<%$ Resources:Attendance,Save %>" Width="50px" />
                                                                    &nbsp;
                                                                    <asp:Button ID="Button17" runat="server" CausesValidation="False" CssClass="buttonCommman"
                                                                        OnClick="btnResetOT_Click" Text="<%$ Resources:Attendance,Reset %>" Width="60px" />
                                                                    &nbsp;
                                                                    <asp:Button ID="Button18" runat="server" CausesValidation="False" CssClass="buttonCommman"
                                                                        OnClick="btnCancelOT_Click" Text="<%$ Resources:Attendance,Cancel %>" Width="60px" />
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </asp:Panel>
                                                </td>
                                            </tr>
                                        </table>
                                    </asp:Panel>
                                    <asp:Panel ID="pnlSal1" runat="server" class="MsgOverlayAddress" Visible="False">
                                        <asp:Panel ID="pnlSal2" runat="server" class="MsgPopUpPanelAddress" Visible="False">
                                            <asp:Panel ID="Panel18" runat="server" DefaultButton="Button13" Style="width: 100%;
                                                height: 100%; text-align: center;">
                                                <table width="100%" style="background-image: url(../Images/bg_repeat.jpg); background-repeat: repeat;">
                                                    <tr>
                                                        <td align="<%= Common.ChangeTDForDefaultLeft()%>">
                                                            <asp:Label ID="Label26" runat="server" Font-Size="14px" Font-Bold="true" CssClass="labelComman"
                                                                Text="<%$ Resources:Attendance,Edit %>"></asp:Label>
                                                        </td>
                                                        <td align="right">
                                                            <asp:ImageButton ID="ImageButton11" runat="server" ImageUrl="~/Images/close.png"
                                                                CausesValidation="False" OnClick="btnClosePanel_Click5" Height="20px" Width="20px" />
                                                        </td>
                                                    </tr>
                                                </table>
                                                <table width="100%" style="background-image: url(../Images/bgs.png); padding-left: 20px;
                                                    padding-right: 20px; padding-bottom: 20px; height: 160px">
                                                    <tr>
                                                        <td width="150px" align="<%= Common.ChangeTDForDefaultLeft()%>">
                                                            <asp:Label ID="Label30" runat="server" ForeColor="#666666" Font-Bold="true" Font-Names="arial"
                                                                Font-Size="13px" Text="<%$ Resources:Attendance,Employee Code %>"></asp:Label>
                                                            <br />
                                                        </td>
                                                        <td>
                                                            :
                                                        </td>
                                                        <td width="100px" align="<%= Common.ChangeTDForDefaultLeft()%>">
                                                            <asp:Label ID="lblEmpCodeSal" runat="server" ForeColor="#666666" Font-Bold="true"
                                                                Font-Names="arial" Font-Size="13px"></asp:Label>
                                                        </td>
                                                        <td>
                                                            &nbsp;
                                                        </td>
                                                        <td width="150px" align="<%= Common.ChangeTDForDefaultLeft()%>">
                                                            <asp:Label ID="Label34" runat="server" ForeColor="#666666" Font-Bold="true" Font-Names="arial"
                                                                Font-Size="13px" Text="<%$ Resources:Attendance,Employee Name %>"></asp:Label>
                                                            <br />
                                                        </td>
                                                        <td>
                                                            :
                                                        </td>
                                                        <td width="250px" align="<%= Common.ChangeTDForDefaultLeft()%>">
                                                            <asp:Label ID="lblEmpNameSal" runat="server" ForeColor="#666666" Font-Bold="true"
                                                                Font-Names="arial" Font-Size="13px"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="<%= Common.ChangeTDForDefaultLeft()%>">
                                                            <asp:Label ID="lblBasic" runat="server" CssClass="labelComman" Text="<%$ Resources:Attendance,Basic Salary %>"></asp:Label>
                                                        </td>
                                                        <td width="1px" align="<%= Common.ChangeTDForDefaultLeft()%>">
                                                            :
                                                        </td>
                                                        <td align="<%= Common.ChangeTDForDefaultLeft()%>">
                                                            <asp:TextBox ID="txtBasic1" runat="server" Width="130px" CssClass="labelComman"></asp:TextBox>
                                                            <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender6" runat="server" Enabled="True"
                                                                TargetControlID="txtBasic1" ValidChars="1,2,3,4,5,6,7,8,9,0,.">
                                                            </cc1:FilteredTextBoxExtender>
                                                        </td>
                                                        <td>
                                                            &nbsp;&nbsp;
                                                        </td>
                                                        <td align="<%= Common.ChangeTDForDefaultLeft()%>">
                                                            <asp:Label ID="Label37" runat="server" CssClass="labelComman" Text="<%$ Resources:Attendance,Payment Type %>"></asp:Label>
                                                        </td>
                                                        <td width="1px" align="<%= Common.ChangeTDForDefaultLeft()%>">
                                                            :
                                                        </td>
                                                        <td align="<%= Common.ChangeTDForDefaultLeft()%>">
                                                            <asp:DropDownList ID="ddlPaymentType1" Width="135px" runat="server" CssClass="DropdownSearch">
                                                                <asp:ListItem Value="Hourly">Hourly</asp:ListItem>
                                                                <asp:ListItem Value="Daily">Daily</asp:ListItem>
                                                                <asp:ListItem Value="Weekly">Weekly</asp:ListItem>
                                                                <asp:ListItem Value="Montly">Monthly</asp:ListItem>
                                                            </asp:DropDownList>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="<%= Common.ChangeTDForDefaultLeft()%>">
                                                            <asp:Label ID="Label38" runat="server" CssClass="labelComman" Text="<%$ Resources:Attendance,Work Minute %>"></asp:Label>
                                                        </td>
                                                        <td width="1px" align="<%= Common.ChangeTDForDefaultLeft()%>">
                                                            :
                                                        </td>
                                                        <td align="<%= Common.ChangeTDForDefaultLeft()%>">
                                                            <asp:TextBox ID="txtWorkMin1" runat="server" Width="130px" CssClass="labelComman"></asp:TextBox>
                                                            <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender7" runat="server" Enabled="True"
                                                                TargetControlID="txtWorkMinute" ValidChars="1,2,3,4,5,6,7,8,9,0,.">
                                                            </cc1:FilteredTextBoxExtender>
                                                        </td>
                                                        <td>
                                                            &nbsp;&nbsp;
                                                        </td>
                                                        <td align="<%= Common.ChangeTDForDefaultLeft()%>">
                                                            <asp:Label ID="Label39" runat="server" CssClass="labelComman" Text="<%$ Resources:Attendance,Work Calculation Method %>"></asp:Label>
                                                        </td>
                                                        <td width="1px" align="<%= Common.ChangeTDForDefaultLeft()%>">
                                                            :
                                                        </td>
                                                        <td align="<%= Common.ChangeTDForDefaultLeft()%>">
                                                            <asp:DropDownList ID="ddlWorkCal1" Width="135px" runat="server" CssClass="DropdownSearch">
                                                                <asp:ListItem Value="PairWise">PairWise</asp:ListItem>
                                                                <asp:ListItem Value="InOut">InOut</asp:ListItem>
                                                            </asp:DropDownList>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="<%= Common.ChangeTDForDefaultLeft()%>">
                                                            <asp:Label ID="Label40" runat="server" CssClass="labelComman" Text="<%$ Resources:Attendance,Currency %>"></asp:Label>
                                                        </td>
                                                        <td width="1px" align="<%= Common.ChangeTDForDefaultLeft()%>">
                                                            :
                                                        </td>
                                                        <td align="<%= Common.ChangeTDForDefaultLeft()%>">
                                                            <asp:DropDownList ID="ddlCurrency1" Width="135px" runat="server" CssClass="DropdownSearch">
                                                            </asp:DropDownList>
                                                        </td>
                                                    </tr>
                                                    
                                                    
   <tr>
                                                                <td align="<%= Common.ChangeTDForDefaultLeft()%>">
                                                                    <asp:Label ID="Label82" runat="server" CssClass="labelComman" Text="<%$ Resources:Attendance,Employee In Payroll %>"></asp:Label>
                                                                </td>
                                                                <td align="<%= Common.ChangeTDForDefaultLeft()%>">
                                                                    :
                                                                </td>
                                                                <td align="<%= Common.ChangeTDForDefaultLeft()%>">
                                                                    <asp:CheckBox ID="chkEmpINPayroll1"  runat="server" CssClass="labelComman" AutoPostBack="true" OnCheckedChanged="chkEmpINPayroll_OnCheckedChanged1" />
                                                                   
                                                                </td>
                                                          </tr>
                                                             <tr>
                                                           
                                                                <td align="<%= Common.ChangeTDForDefaultLeft()%>">
                                                                    <asp:Label ID="Label83" runat="server" CssClass="labelComman" Text="<%$ Resources:Attendance,Employee PF %>"></asp:Label>
                                                                </td>
                                                                <td align="<%= Common.ChangeTDForDefaultLeft()%>">
                                                                    :
                                                                </td>
                                                                <td Width="130px" align="<%= Common.ChangeTDForDefaultLeft()%>">
                                                                    <asp:CheckBox ID="chkEmpPf1"  runat="server" CssClass="labelComman" />
                                                                   
                                                                </td>
                                                           
                                                              <td>
                                                            &nbsp;&nbsp;
                                                        </td>
                                                            
                                                                <td align="<%= Common.ChangeTDForDefaultLeft()%>">
                                                                    <asp:Label ID="Label84" runat="server" CssClass="labelComman" Text="<%$ Resources:Attendance,Employee ESIC %>"></asp:Label>
                                                                </td>
                                                                <td align="<%= Common.ChangeTDForDefaultLeft()%>">
                                                                    :
                                                                </td>
                                                                <td align="<%= Common.ChangeTDForDefaultLeft()%>">
                                                                    <asp:CheckBox ID="chkEmpEsic1"  runat="server" CssClass="labelComman" />
                                                                   
                                                                </td>
                                                            </tr>
                                                    
                                                    
                                                    <tr>
                                                        <td>
                                                        </td>
                                                        <td>
                                                        </td>
                                                        <td colspan="4" align='<%= Common.ChangeTDForDefaultLeft()%>' style="padding-left: 10PX;">
                                                            <asp:Button ID="Button13" runat="server" CssClass="buttonCommman" OnClick="btnUpdateSal_Click"
                                                                Text="<%$ Resources:Attendance,Save %>" Width="75px" />
                                                            &nbsp;
                                                            <asp:Button ID="Button14" runat="server" CausesValidation="False" CssClass="buttonCommman"
                                                                OnClick="btnCancelPopSal_Click" Text="<%$ Resources:Attendance,Cancel %>" Width="75px" />
                                                            <asp:HiddenField ID="hdnEmpIdSal" runat="server" />
                                                        </td>
                                                    </tr>
                                                </table>
                                            </asp:Panel>
                                        </asp:Panel>
                                    </asp:Panel>
                                    <asp:Panel ID="pnlNotice1" runat="server" class="MsgOverlayAddress" Visible="False">
                                        <asp:Panel ID="pnlNotice2" runat="server" class="MsgPopUpPanelAddress" Visible="False">
                                            <asp:Panel ID="Panel14" runat="server" DefaultButton="Button7" Style="width: 800px;
                                                height: 100%; text-align: center;">
                                                <table width="100%" style="background-image: url(../Images/bg_repeat.jpg); background-repeat: repeat;">
                                                    <tr>
                                                        <td align="<%= Common.ChangeTDForDefaultLeft()%>">
                                                            <asp:Label ID="Label20" runat="server" Font-Size="14px" Font-Bold="true" CssClass="labelComman"
                                                                Text="<%$ Resources:Attendance,Edit %>"></asp:Label>
                                                        </td>
                                                        <td align="right">
                                                            <asp:ImageButton ID="ImageButton7" runat="server" ImageUrl="~/Images/close.png" CausesValidation="False"
                                                                OnClick="btnClosePanel_ClickNf" Height="20px" Width="20px" />
                                                        </td>
                                                    </tr>
                                                </table>
                                                <table width="100%" style="background-image: url(../Images/bgs.png); padding-left: 20px;
                                                    padding-right: 20px; padding-bottom: 20px; height: 160px" cellpadding="0" cellspacing="0">
                                                    <tr>
                                                        <td>
                                                            <br />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td width="100px" align="<%= Common.ChangeTDForDefaultLeft()%>">
                                                            <asp:Label ID="Label23" runat="server" ForeColor="#666666" Font-Bold="true" Font-Names="arial"
                                                                Font-Size="13px" Text="<%$ Resources:Attendance,Employee Code %>"></asp:Label>
                                                            <br />
                                                        </td>
                                                        <td width="2px">
                                                            :
                                                        </td>
                                                        <td width="50px" align="<%= Common.ChangeTDForDefaultLeft()%>">
                                                            <asp:Label ID="lblEmpCodeNF" runat="server" ForeColor="#666666" Font-Bold="true"
                                                                Font-Names="arial" Font-Size="13px"></asp:Label>
                                                        </td>
                                                        <td style="padding-left: 10px" width="150px" align="<%= Common.ChangeTDForDefaultLeft()%>">
                                                            <asp:Label ID="Label25" runat="server" ForeColor="#666666" Font-Bold="true" Font-Names="arial"
                                                                Font-Size="13px" Text="<%$ Resources:Attendance,Employee Name %>"></asp:Label>
                                                            <br />
                                                        </td>
                                                        <td width="2px">
                                                            :
                                                        </td>
                                                        <td width="150px" align="<%= Common.ChangeTDForDefaultLeft()%>">
                                                            <asp:Label ID="lblEmpNameNf" runat="server" ForeColor="#666666" Font-Bold="true"
                                                                Font-Names="arial" Font-Size="13px"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <br />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="3" align="<%= Common.ChangeTDForDefaultLeft()%>">
                                                            <asp:Label ID="Label21" runat="server" ForeColor="#666666" Font-Bold="true" Font-Names="arial"
                                                                Font-Size="13px" Text="<%$ Resources:Attendance,SMS %>"></asp:Label>
                                                        </td>
                                                        <td colspan="3" style="padding-left: 10px" align="<%= Common.ChangeTDForDefaultLeft()%>">
                                                            <asp:Label ID="Label22" runat="server" ForeColor="#666666" Font-Bold="true" Font-Names="arial"
                                                                Font-Size="13px" Text="<%$ Resources:Attendance,Report %>"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr style="background-color: White;">
                                                        <td align="<%= Common.ChangeTDForDefaultLeft()%>" width="250px">
                                                            <asp:CheckBox ID="chkSMSDocExp1" runat="server" Font-Names="Trebuchet MS" Font-Size="Small"
                                                                ForeColor="Gray" Text="<%$ Resources:Attendance,Document Expiration  %>"></asp:CheckBox>
                                                        </td>
                                                        <td align="<%= Common.ChangeTDForDefaultLeft()%>" width="250px">
                                                            <asp:CheckBox ID="ChkSMSPartial1" runat="server" Font-Names="Trebuchet MS" Font-Size="Small"
                                                                ForeColor="Gray" Text="<%$ Resources:Attendance,Partial Leave Fault  %>"></asp:CheckBox>
                                                        </td>
                                                        <td width="100px">
                                                        </td>
                                                        <td width="250px" style="border-left: solid 1px #c2c2c2; padding-left: 10px" align="<%= Common.ChangeTDForDefaultLeft()%>">
                                                            <asp:CheckBox ID="ChkRptInOut1" runat="server" Font-Names="Trebuchet MS" Font-Size="Small"
                                                                ForeColor="Gray" Text="<%$ Resources:Attendance,In Out %>"></asp:CheckBox>
                                                        </td>
                                                        <td width="250px" align="<%= Common.ChangeTDForDefaultLeft()%>">
                                                            <asp:CheckBox ID="ChkRptSalary1" runat="server" Font-Names="Trebuchet MS" Font-Size="Small"
                                                                ForeColor="Gray" Text="<%$ Resources:Attendance,Salary  %>"></asp:CheckBox>
                                                        </td>
                                                    </tr>
                                                    <tr style="background-color: White;">
                                                        <td align="<%= Common.ChangeTDForDefaultLeft()%>">
                                                            <asp:CheckBox ID="chkSMSAbsent1" runat="server" Font-Names="Trebuchet MS" Font-Size="Small"
                                                                ForeColor="Gray" Text="<%$ Resources:Attendance,Absent  %>"></asp:CheckBox>
                                                        </td>
                                                        <td align="<%= Common.ChangeTDForDefaultLeft()%>">
                                                            <asp:CheckBox ID="chkSMSNoClock1" runat="server" Font-Names="Trebuchet MS" Font-Size="Small"
                                                                ForeColor="Gray" Text="<%$ Resources:Attendance,No Clock %>"></asp:CheckBox>
                                                        </td>
                                                        <td>
                                                        </td>
                                                        <td style="border-left: solid 1px #c2c2c2; padding-left: 10px" align="<%= Common.ChangeTDForDefaultLeft()%>">
                                                            <asp:CheckBox ID="ChkRptAbsent1" runat="server" Font-Names="Trebuchet MS" Font-Size="Small"
                                                                ForeColor="Gray" Text="<%$ Resources:Attendance,Absent  %>"></asp:CheckBox>
                                                        </td>
                                                        <td align="<%= Common.ChangeTDForDefaultLeft()%>">
                                                            <asp:CheckBox ID="ChkRptOvertime1" runat="server" Font-Names="Trebuchet MS" Font-Size="Small"
                                                                ForeColor="Gray" Text="<%$ Resources:Attendance,Over Time %>"></asp:CheckBox>
                                                        </td>
                                                    </tr>
                                                    <tr style="background-color: White;">
                                                        <td align="<%= Common.ChangeTDForDefaultLeft()%>">
                                                            <asp:CheckBox ID="chkSMSLate1" runat="server" Font-Names="Trebuchet MS" Font-Size="Small"
                                                                ForeColor="Gray" Text="<%$ Resources:Attendance,Late  %>"></asp:CheckBox>
                                                        </td>
                                                        <td>
                                                        </td>
                                                        <td>
                                                        </td>
                                                        <td style="border-left: solid 1px #c2c2c2; padding-left: 10px" align="<%= Common.ChangeTDForDefaultLeft()%>">
                                                            <asp:CheckBox ID="chkRptLate1" runat="server" Font-Names="Trebuchet MS" Font-Size="Small"
                                                                ForeColor="Gray" Text="<%$ Resources:Attendance,Late %>"></asp:CheckBox>
                                                        </td>
                                                        <td align="<%= Common.ChangeTDForDefaultLeft()%>">
                                                            <asp:CheckBox ID="chkRptDoc1" runat="server" Font-Names="Trebuchet MS" Font-Size="Small"
                                                                ForeColor="Gray" Text="<%$ Resources:Attendance,Document Expiration %>"></asp:CheckBox>
                                                        </td>
                                                    </tr>
                                                    <tr style="background-color: White;">
                                                        <td align="<%= Common.ChangeTDForDefaultLeft()%>">
                                                            <asp:CheckBox ID="chkSMSEarly1" runat="server" Font-Names="Trebuchet MS" Font-Size="Small"
                                                                ForeColor="Gray" Text="<%$ Resources:Attendance,Early Out  %>"></asp:CheckBox>
                                                        </td>
                                                        <td>
                                                        </td>
                                                        <td>
                                                        </td>
                                                        <td style="border-left: solid 1px #c2c2c2; padding-left: 10px" align="<%= Common.ChangeTDForDefaultLeft()%>">
                                                            <asp:CheckBox ID="chkRptEarly1" runat="server" Font-Names="Trebuchet MS" Font-Size="Small"
                                                                ForeColor="Gray" Text="<%$ Resources:Attendance,Early Out %>"></asp:CheckBox>
                                                        </td>
                                                        <td align="<%= Common.ChangeTDForDefaultLeft()%>">
                                                            <asp:CheckBox ID="chkRptViolation1" runat="server" Font-Names="Trebuchet MS" Font-Size="Small"
                                                                ForeColor="Gray" Text="<%$ Resources:Attendance,Violation  %>"></asp:CheckBox>
                                                        </td>
                                                    </tr>
                                                    <tr style="background-color: White;">
                                                        <td align="<%= Common.ChangeTDForDefaultLeft()%>">
                                                            <asp:CheckBox ID="ChkSmsLeave1" runat="server" Font-Names="Trebuchet MS" Font-Size="Small"
                                                                ForeColor="Gray" Text="<%$ Resources:Attendance,Leave Approved  %>"></asp:CheckBox>
                                                        </td>
                                                        <td>
                                                        </td>
                                                        <td>
                                                        </td>
                                                        <td style="border-left: solid 1px #c2c2c2; padding-left: 10px" align="<%= Common.ChangeTDForDefaultLeft()%>">
                                                            <asp:CheckBox ID="ChkRptLog1" runat="server" Font-Names="Trebuchet MS" Font-Size="Small"
                                                                ForeColor="Gray" Text="<%$ Resources:Attendance,Log %>"></asp:CheckBox>
                                                        </td>
                                                        <td>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="6">
                                                            <asp:Button ID="Button7" runat="server" CssClass="buttonCommman" OnClick="btnUpdateNotice_Click"
                                                                TabIndex="8" Text="<%$ Resources:Attendance,Save %>" Width="75px" />
                                                            &nbsp;
                                                            <asp:Button ID="Button8" runat="server" CausesValidation="False" CssClass="buttonCommman"
                                                                OnClick="btnCancelPopLeave_Click1" TabIndex="9" Text="<%$ Resources:Attendance,Cancel %>"
                                                                Width="75px" />
                                                            <asp:HiddenField ID="hdnEmpIdNF" runat="server" />
                                                        </td>
                                                    </tr>
                                                </table>
                                            </asp:Panel>
                                        </asp:Panel>
                                    </asp:Panel>
                                    <asp:Panel ID="pnl1" runat="server" class="MsgOverlayAddress" Visible="False">
                                        <asp:Panel ID="pnl2" runat="server" class="MsgPopUpPanelAddress" Visible="False">
                                            <asp:Panel ID="pnl3" runat="server" DefaultButton="Button1" Style="width: 100%; height: 100%;
                                                text-align: center;">
                                                <table width="100%" style="background-image: url(../Images/bg_repeat.jpg); background-repeat: repeat;">
                                                    <tr>
                                                        <td align="<%= Common.ChangeTDForDefaultLeft()%>">
                                                            <asp:Label ID="Label12" runat="server" Font-Size="14px" Font-Bold="true" CssClass="labelComman"
                                                                Text="<%$ Resources:Attendance,Edit %>"></asp:Label>
                                                        </td>
                                                        <td align="right">
                                                            <asp:ImageButton ID="ImageButton5" runat="server" ImageUrl="~/Images/close.png" CausesValidation="False"
                                                                OnClick="btnClosePanel_Click1" Height="20px" Width="20px" />
                                                        </td>
                                                    </tr>
                                                </table>
                                                <table width="100%" style="padding-left: 43px">
                                                    <tr>
                                                        <td width="100px" align="<%= Common.ChangeTDForDefaultLeft()%>">
                                                            <asp:Label ID="Label14" runat="server" Font-Size="14px" Font-Bold="true" CssClass="labelComman"
                                                                Text="<%$ Resources:Attendance,Employee Code %>"></asp:Label>
                                                        </td>
                                                        <td width="1px">
                                                            :
                                                        </td>
                                                        <td width="50px" align="<%= Common.ChangeTDForDefaultLeft()%>">
                                                            <asp:Label ID="lblEmpCodeLeave" runat="server" Font-Size="14px" Font-Bold="true"
                                                                CssClass="labelComman"></asp:Label>
                                                        </td>
                                                        <td width="100px" salign="<%= Common.ChangeTDForDefaultLeft()%>">
                                                            <asp:Label ID="Label13" runat="server" Font-Size="14px" Font-Bold="true" CssClass="labelComman"
                                                                Text="<%$ Resources:Attendance,Employee Name %>"></asp:Label>
                                                        </td>
                                                        <td width="1px">
                                                            :
                                                        </td>
                                                        <td width="300px" align="<%= Common.ChangeTDForDefaultLeft()%>">
                                                            <asp:Label ID="lblEmpNameLeave" runat="server" Font-Size="14px" Font-Bold="true"
                                                                CssClass="labelComman"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="6">
                                                            <asp:GridView ID="gvLeaveEmp" runat="server" AutoGenerateColumns="False" DataKeyNames="Trans_No"
                                                                PageSize="<%# SystemParameter.GetPageSize() %>" TabIndex="10" Width="100%">
                                                                <Columns>
                                                                    <asp:TemplateField HeaderText="<%$ Resources:Attendance,Delete %>">
                                                                        <ItemTemplate>
                                                                            <asp:ImageButton ID="IbtnDelete" runat="server" CausesValidation="False" CommandName='<%# Eval("Emp_Id") %>'
                                                                                CommandArgument='<%# Eval("LeaveType_Id") %>' ImageUrl="~/Images/Erase.png" OnCommand="btnDeleteLeave_Command"
                                                                                Visible="true" ToolTip="<%$ Resources:Attendance,Delete %>" />
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="<%$ Resources:Attendance,LeaveType %>">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblleavetype0" runat="server" Text='<%# GetLeaveTypeName(Eval("LeaveType_Id")) %>'></asp:Label>
                                                                            <asp:HiddenField ID="hdnLeaveTypeId" runat="server" Value='<%# Eval("LeaveType_Id") %>' />
                                                                            <asp:HiddenField ID="hdnTranNo" runat="server" Value='<%# Eval("Trans_No") %>' />
                                                                            <asp:HiddenField ID="hdnEmpId1" runat="server" Value='<%# Eval("Emp_Id") %>' />
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="<%$ Resources:Attendance,Total Leave %>">
                                                                        <ItemTemplate>
                                                                            <asp:TextBox ID="txtNoOfLeave0" runat="server" Text='<%# Eval("Total_Leave") %>'
                                                                                Width="110px"></asp:TextBox>
                                                                            <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" TargetControlID="txtNoOfLeave0"
                                                                                ValidChars="0,1,2,3,4,5,6,7,8,9">
                                                                            </cc1:FilteredTextBoxExtender>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="<%$ Resources:Attendance,Paid Leave %>">
                                                                        <ItemTemplate>
                                                                            <asp:TextBox ID="txtPAidLEave1" runat="server" Text='<%# Eval("Paid_Leave") %>' Width="110px"></asp:TextBox>
                                                                            <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtenderPAidLeave" runat="server"
                                                                                TargetControlID="txtPAidLEave1" ValidChars="0,1,2,3,4,5,6,7,8,9">
                                                                            </cc1:FilteredTextBoxExtender>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="<%$ Resources:Attendance,Percentage Of Salary %>">
                                                                        <ItemTemplate>
                                                                            <asp:TextBox ID="TextBox1" runat="server" Text='<%# Eval("Percentage_Of_Salary") %>'
                                                                                Width="125px"></asp:TextBox>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="<%$ Resources:Attendance,Schedule Type %>">
                                                                        <ItemTemplate>
                                                                            <asp:DropDownList ID="ddlSchType0" runat="server" SelectedValue='<%# Eval("Shedule_Type") %>'
                                                                                Width="110px" AutoPostBack="true" OnSelectedIndexChanged="ddlScheduleGrid_SelectedIndexChanged">
                                                                                <asp:ListItem Value="Monthly">Monthly</asp:ListItem>
                                                                                <asp:ListItem Value="Yearly">Yearly</asp:ListItem>
                                                                            </asp:DropDownList>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="<%$ Resources:Attendance,IsYearCarry %>">
                                                                        <ItemTemplate>
                                                                            <asp:CheckBox ID="chkYearCarry0" runat="server" Checked='<%# Eval("Is_YearCarry") %>'
                                                                                Text="<%$ Resources:Attendance,IsYearCarry %>" />
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                </Columns>
                                                                <AlternatingRowStyle CssClass="InvgridAltRow" />
                                                                <HeaderStyle CssClass="Invgridheader" BorderStyle="Solid" BorderColor="#6E6E6E" BorderWidth="1px" />
                                                                <PagerStyle CssClass="Invgridheader" />
                                                                <RowStyle CssClass="Invgridrow" HorizontalAlign="Center" />
                                                            </asp:GridView>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="3">
                                                            <asp:Button ID="Button1" runat="server" CssClass="buttonCommman" OnClick="btnUpdateLeave_Click"
                                                                TabIndex="8" Text="<%$ Resources:Attendance,Save %>" Width="50px" />
                                                            &nbsp;
                                                            <asp:Button ID="Button2" runat="server" CausesValidation="False" CssClass="buttonCommman"
                                                                OnClick="btnCancelPopLeave_Click" TabIndex="9" Text="<%$ Resources:Attendance,Cancel %>"
                                                                Width="60px" />
                                                        </td>
                                                    </tr>
                                                </table>
                                            </asp:Panel>
                                        </asp:Panel>
                                    </asp:Panel>
                                    <asp:Panel ID="pnlAddress1" runat="server" class="MsgOverlayAddress" Visible="False">
                                        <asp:Panel ID="pnlAddress2" runat="server" class="MsgPopUpPanelAddress" Visible="False">
                                            <asp:Panel ID="pnlAddress3" runat="server" DefaultButton="btnAddressSave" Style="width: 100%;
                                                height: 100%; text-align: center;">
                                                <table width="100%" style="background-image: url(../Images/bg_repeat.jpg); background-repeat: repeat;">
                                                    <tr>
                                                        <td align="<%= Common.ChangeTDForDefaultLeft()%>">
                                                            <asp:Label ID="lblAddressHeader" runat="server" Font-Size="14px" Font-Bold="true"
                                                                CssClass="labelComman" Text="<%$ Resources:Attendance, Address Setup %>"></asp:Label>
                                                        </td>
                                                        <td align="right">
                                                            <asp:ImageButton ID="btnClosePanel" runat="server" ImageUrl="~/Images/close.png"
                                                                CausesValidation="False" OnClick="btnClosePanel_Click" Height="20px" Width="20px" />
                                                        </td>
                                                    </tr>
                                                </table>
                                                <table width="100%" style="padding-left: 43px">
                                                    <tr>
                                                        <td align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                                            <asp:Label ID="lblAddressCategory" runat="server" CssClass="labelComman" Text="<%$ Resources:Attendance,Address Category %>"></asp:Label>
                                                        </td>
                                                        <td align="center">
                                                            :
                                                        </td>
                                                        <td align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                                            <asp:DropDownList ID="ddlAddressCategory" runat="server" CssClass="textComman" />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                                            <asp:Label ID="Label9" runat="server" CssClass="labelComman" Text="<%$ Resources:Attendance,Address Name %>" />
                                                        </td>
                                                        <td align="center">
                                                            :
                                                        </td>
                                                        <td align='<%= Common.ChangeTDForDefaultLeft()%>' colspan="4">
                                                            <asp:TextBox ID="txtAddressNameNew" runat="server" CssClass="textComman" AutoPostBack="true"
                                                                OnTextChanged="txtAddressNameNew_TextChanged" Width="610px" BackColor="#c3c3c3" />
                                                            <cc1:AutoCompleteExtender ID="AutoCompleteExtender4" runat="server" DelimiterCharacters=""
                                                                Enabled="True" ServiceMethod="GetCompletionListNewAddress" ServicePath="" CompletionInterval="100"
                                                                MinimumPrefixLength="1" CompletionSetCount="1" TargetControlID="txtAddressNameNew"
                                                                UseContextKey="True" CompletionListCssClass="completionList" CompletionListItemCssClass="listItem"
                                                                CompletionListHighlightedItemCssClass="itemHighlighted">
                                                            </cc1:AutoCompleteExtender>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                                            <asp:Label ID="lblAddress" runat="server" CssClass="labelComman" Text="<%$ Resources:Attendance,Address %>" />
                                                        </td>
                                                        <td align="center">
                                                            :
                                                        </td>
                                                        <td align='<%= Common.ChangeTDForDefaultLeft()%>' colspan="4">
                                                            <asp:TextBox ID="txtAddress" Width="610px" TextMode="MultiLine" runat="server" CssClass="textComman" />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                                            <asp:Label ID="lblStreet" runat="server" CssClass="labelComman" Text="<%$ Resources:Attendance,Street %>" />
                                                        </td>
                                                        <td>
                                                            :
                                                        </td>
                                                        <td align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                                            <asp:TextBox ID="txtStreet" runat="server" CssClass="textComman" />
                                                        </td>
                                                        <td align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                                            <asp:Label ID="lblBlock" runat="server" CssClass="labelComman" Text="<%$ Resources:Attendance,Block %>" />
                                                        </td>
                                                        <td>
                                                            :
                                                        </td>
                                                        <td align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                                            <asp:TextBox ID="txtBlock" runat="server" CssClass="textComman" />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                                            <asp:Label ID="lblAvenue" runat="server" CssClass="labelComman" Text="<%$ Resources:Attendance,Avenue %>" />
                                                        </td>
                                                        <td>
                                                            :
                                                        </td>
                                                        <td align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                                            <asp:TextBox ID="txtAvenue" runat="server" CssClass="textComman" />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                                            <asp:Label ID="Label10" runat="server" CssClass="labelComman" Text="<%$ Resources:Attendance,Country %>" />
                                                        </td>
                                                        <td>
                                                            :
                                                        </td>
                                                        <td align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                                            <asp:DropDownList ID="ddlCountry" runat="server" CssClass="textComman" />
                                                        </td>
                                                        <td align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                                            <asp:Label ID="lblState" runat="server" CssClass="labelComman" Text="<%$ Resources:Attendance,State %>" />
                                                        </td>
                                                        <td>
                                                            :
                                                        </td>
                                                        <td align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                                            <asp:TextBox ID="txtState" runat="server" CssClass="textComman" />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                                            <asp:Label ID="lblCity" runat="server" CssClass="labelComman" Text="<%$ Resources:Attendance,City %>" />
                                                        </td>
                                                        <td>
                                                            :
                                                        </td>
                                                        <td align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                                            <asp:TextBox ID="txtCity" runat="server" CssClass="textComman" />
                                                        </td>
                                                        <td align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                                            <asp:Label ID="lblPinCode" runat="server" CssClass="labelComman" Text="<%$ Resources:Attendance,PinCode %>" />
                                                        </td>
                                                        <td>
                                                            :
                                                        </td>
                                                        <td align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                                            <asp:TextBox ID="txtPinCode" runat="server" CssClass="textComman" />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                                            <asp:Label ID="lblPhoneNo1" runat="server" CssClass="labelComman" Text="<%$ Resources:Attendance,Phone No 1 %>" />
                                                        </td>
                                                        <td>
                                                            :
                                                        </td>
                                                        <td align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                                            <asp:TextBox ID="txtPhoneNo1" runat="server" CssClass="textComman" />
                                                        </td>
                                                        <td align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                                            <asp:Label ID="lblPhoneNo2" runat="server" CssClass="labelComman" Text="<%$ Resources:Attendance,Phone No 2 %>" />
                                                        </td>
                                                        <td>
                                                            :
                                                        </td>
                                                        <td align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                                            <asp:TextBox ID="txtPhoneNo2" runat="server" CssClass="textComman" />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                                            <asp:Label ID="lblMobileNo1" runat="server" CssClass="labelComman" Text="<%$ Resources:Attendance,Mobile No 1 %>" />
                                                        </td>
                                                        <td>
                                                            :
                                                        </td>
                                                        <td align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                                            <asp:TextBox ID="txtMobileNo1" runat="server" CssClass="textComman" />
                                                        </td>
                                                        <td align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                                            <asp:Label ID="lblMobileNo2" runat="server" CssClass="labelComman" Text="<%$ Resources:Attendance,Mobile No 2 %>" />
                                                        </td>
                                                        <td>
                                                            :
                                                        </td>
                                                        <td align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                                            <asp:TextBox ID="txtMobileNo2" runat="server" CssClass="textComman" />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                                            <asp:Label ID="lblEmailId1" runat="server" CssClass="labelComman" Text="<%$ Resources:Attendance,Email Id 1 %>" />
                                                        </td>
                                                        <td>
                                                            :
                                                        </td>
                                                        <td align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                                            <asp:TextBox ID="txtEmailId1" runat="server" CssClass="textComman" />
                                                        </td>
                                                        <td align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                                            <asp:Label ID="lblEmailId2" runat="server" CssClass="labelComman" Text="<%$ Resources:Attendance,Email Id 2 %>" />
                                                        </td>
                                                        <td>
                                                            :
                                                        </td>
                                                        <td align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                                            <asp:TextBox ID="txtEmailId2" runat="server" CssClass="textComman" />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                                            <asp:Label ID="lblFaxNo" runat="server" CssClass="labelComman" Text="<%$ Resources:Attendance,Fax No %>" />
                                                        </td>
                                                        <td>
                                                            :
                                                        </td>
                                                        <td align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                                            <asp:TextBox ID="txtFaxNo" runat="server" CssClass="textComman" />
                                                        </td>
                                                        <td align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                                            <asp:Label ID="lblWebsite" runat="server" CssClass="labelComman" Text="<%$ Resources:Attendance,Website %>" />
                                                        </td>
                                                        <td>
                                                            :
                                                        </td>
                                                        <td align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                                            <asp:TextBox ID="txtWebsite" runat="server" CssClass="textComman" />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                                            <asp:Label ID="lblLongitude" runat="server" CssClass="labelComman" Text="<%$ Resources:Attendance,Longitude %>" />
                                                        </td>
                                                        <td>
                                                            :
                                                        </td>
                                                        <td align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                                            <asp:TextBox ID="txtLongitude" runat="server" CssClass="textComman" />
                                                        </td>
                                                        <td align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                                            <asp:Label ID="lblLatitude" runat="server" CssClass="labelComman" Text="<%$ Resources:Attendance,Latitude %>" />
                                                        </td>
                                                        <td>
                                                            :
                                                        </td>
                                                        <td align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                                            <asp:TextBox ID="txtLatitude" runat="server" CssClass="textComman" />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="6" align="center" style="padding-left: 10px">
                                                            <table>
                                                                <tr>
                                                                    <td width="90px">
                                                                        <asp:Button ID="btnAddressSave" runat="server" Text="<%$ Resources:Attendance,Save %>"
                                                                            CssClass="buttonCommman" OnClick="btnAddressSave_Click" />
                                                                    </td>
                                                                    <td width="90px">
                                                                        <asp:Button ID="btnAddressReset" runat="server" Text="<%$ Resources:Attendance,Reset %>"
                                                                            CssClass="buttonCommman" CausesValidation="False" OnClick="btnAddressReset_Click" />
                                                                    </td>
                                                                    <td width="90px">
                                                                        <asp:Button ID="btnAddressCancel" runat="server" CssClass="buttonCommman" Text="<%$ Resources:Attendance,Cancel %>"
                                                                            CausesValidation="False" OnClick="btnAddressCancel_Click" />
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
                                    <asp:Panel ID="pnlOT1" runat="server" class="MsgOverlayAddress" Visible="False">
                                        <asp:Panel ID="pnlOT2" runat="server" class="MsgPopUpPanelAddress" Visible="False">
                                            <asp:Panel ID="Panel22" runat="server" DefaultButton="btnUpdateOt" Style="width: 100%;
                                                height: 100%; text-align: center;">
                                                <table width="100%" style="background-image: url(../Images/bg_repeat.jpg); background-repeat: repeat;">
                                                    <tr>
                                                        <td align="<%= Common.ChangeTDForDefaultLeft()%>">
                                                            <asp:Label ID="Label50" runat="server" Font-Size="14px" Font-Bold="true" CssClass="labelComman"
                                                                Text="<%$ Resources:Attendance,Edit %>"></asp:Label>
                                                        </td>
                                                        <td align="right">
                                                            <asp:ImageButton ID="ImageButton15" runat="server" ImageUrl="~/Images/close.png"
                                                                CausesValidation="False" OnClick="btnClosePanel_Click5" Height="20px" Width="20px" />
                                                        </td>
                                                    </tr>
                                                </table>
                                                <table width="100%" style="background-image: url(../Images/bgs.png); padding-left: 20px;
                                                    padding-right: 20px; padding-bottom: 0px; height: 150px">
                                                    <tr>
                                                        <td style="padding-left: 8px;" align="<%= Common.ChangeTDForDefaultLeft()%>">
                                                            <asp:Label ID="Label51" runat="server" ForeColor="#666666" Font-Bold="true" Font-Names="arial"
                                                                Font-Size="13px" Text="<%$ Resources:Attendance,Employee Code %>"></asp:Label>
                                                            <br />
                                                        </td>
                                                        <td>
                                                            :
                                                        </td>
                                                        <td align="<%= Common.ChangeTDForDefaultLeft()%>">
                                                            <asp:Label ID="lblEmpCodeOT" runat="server" ForeColor="#666666" Font-Bold="true"
                                                                Font-Names="arial" Font-Size="13px"></asp:Label>
                                                        </td>
                                                        <td style="padding-left: 8px;" align="<%= Common.ChangeTDForDefaultLeft()%>">
                                                            <asp:Label ID="Label53" runat="server" ForeColor="#666666" Font-Bold="true" Font-Names="arial"
                                                                Font-Size="13px" Text="<%$ Resources:Attendance,Employee Name %>"></asp:Label>
                                                            <br />
                                                        </td>
                                                        <td>
                                                            :
                                                        </td>
                                                        <td align="<%= Common.ChangeTDForDefaultLeft()%>">
                                                            <asp:Label ID="lblEmpNameOT" runat="server" ForeColor="#666666" Font-Bold="true"
                                                                Font-Names="arial" Font-Size="13px"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="3" valign="top" align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                                            <table>
                                                                <tr>
                                                                    <td>
                                                                        <fieldset style="height: 140px">
                                                                            <legend>
                                                                                <asp:Label ID="Label55" Font-Bold="true" Font-Size="13px" Font-Names="Arial" runat="server"
                                                                                    Text="<%$ Resources:Attendance,Partial Leave %>"></asp:Label></legend>
                                                                            <table width="100%">
                                                                                <tr>
                                                                                    <td width="230px" align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                                                                        <asp:Label ID="Label56" runat="server" CssClass="labelComman" Text="<%$ Resources:Attendance,Partial Leave Functionality %>"></asp:Label>
                                                                                    </td>
                                                                                    <td width="1px">
                                                                                        :
                                                                                    </td>
                                                                                    <td align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                                                                        <asp:RadioButton ID="rbtnPartialEnable1" GroupName="Partial" Text="<%$ Resources:Attendance,Enable %>"
                                                                                            runat="server" CssClass="labelComman" AutoPostBack="true" OnCheckedChanged="rbtPartial1_OnCheckedChanged" />
                                                                                        &nbsp;&nbsp;
                                                                                        <asp:RadioButton ID="rbtnPartialDisable1" GroupName="Partial" Text="<%$ Resources:Attendance,Disable %>"
                                                                                            runat="server" CssClass="labelComman" AutoPostBack="true" OnCheckedChanged="rbtPartial1_OnCheckedChanged" />
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                                                                        <asp:Label ID="Label57" runat="server" CssClass="labelComman" Text="<%$ Resources:Attendance,Total Minutes %>"></asp:Label>
                                                                                    </td>
                                                                                    <td width="1px">
                                                                                        :
                                                                                    </td>
                                                                                    <td align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                                                                        <asp:TextBox ID="txtTotalMinutesP1" Width="150px" runat="server" CssClass="textComman" />
                                                                                        <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender13" runat="server" Enabled="True"
                                                                                            TargetControlID="txtTotalMinutes" ValidChars="1,2,3,4,5,6,7,8,9,0">
                                                                                        </cc1:FilteredTextBoxExtender>
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                                                                        <asp:Label ID="Label58" runat="server" CssClass="labelComman" Text="<%$ Resources:Attendance,Minute Use in a Day %>"></asp:Label>
                                                                                    </td>
                                                                                    <td width="1px">
                                                                                        :
                                                                                    </td>
                                                                                    <td align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                                                                        <asp:TextBox ID="txtMinuteOTOne" Width="150px" runat="server" CssClass="textComman" />
                                                                                        <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender14" runat="server" Enabled="True"
                                                                                            TargetControlID="txtMinuteday" ValidChars="1,2,3,4,5,6,7,8,9,0">
                                                                                        </cc1:FilteredTextBoxExtender>
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                                                                        <asp:Label ID="Label59" runat="server" CssClass="labelComman" Text="<%$ Resources:Attendance,Carry Forward Minutes %>"></asp:Label>
                                                                                    </td>
                                                                                    <td width="1px">
                                                                                        :
                                                                                    </td>
                                                                                    <td align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                                                                        <asp:RadioButton ID="rbtnCarryYes1" GroupName="Carry1" Text="<%$ Resources:Attendance,Yes %>"
                                                                                            runat="server" CssClass="labelComman" />
                                                                                        &nbsp;&nbsp;
                                                                                        <asp:RadioButton ID="rbtnCarryNo1" GroupName="Carry1" Text="<%$ Resources:Attendance,No %>"
                                                                                            runat="server" CssClass="labelComman" />
                                                                                    </td>
                                                                                </tr>
                                                                            </table>
                                                                        </fieldset>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="center">
                                                                        <asp:Button ID="btnUpdateOt" runat="server" CssClass="buttonCommman" OnClick="btnUpdateOT_Click"
                                                                            Text="<%$ Resources:Attendance,Save %>" Width="75px" />
                                                                        &nbsp; &nbsp; &nbsp;
                                                                        <asp:Button ID="Button20" runat="server" CausesValidation="False" CssClass="buttonCommman"
                                                                            OnClick="btnCancelPopOT_Click" Text="<%$ Resources:Attendance,Cancel %>" Width="75px" />
                                                                        <asp:HiddenField ID="HiddenField2" runat="server" />
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                        <td colspan="3" align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                                            <fieldset style="height: 190px">
                                                                <legend>
                                                                    <asp:Label ID="Label60" Font-Bold="true" Font-Size="13px" Font-Names="Arial" runat="server"
                                                                        Text="<%$ Resources:Attendance,Over Time %>"></asp:Label></legend>
                                                                <table width="100%">
                                                                    <tr>
                                                                        <td width="230px" align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                                                            <asp:Label ID="Label61" runat="server" CssClass="labelComman" Text="<%$ Resources:Attendance,Over Time Functionality %>"></asp:Label>
                                                                        </td>
                                                                        <td width="1px">
                                                                            :
                                                                        </td>
                                                                        <td align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                                                            <asp:RadioButton ID="rbtnOTEnable1" GroupName="OT" Text="<%$ Resources:Attendance,Enable %>"
                                                                                runat="server" CssClass="labelComman" AutoPostBack="true" OnCheckedChanged="rbtOT1_OnCheckedChanged" />
                                                                            &nbsp;&nbsp;
                                                                            <asp:RadioButton ID="rbtnOTDisable1" GroupName="OT" Text="<%$ Resources:Attendance,Disable %>"
                                                                                runat="server" CssClass="labelComman" AutoPostBack="true" OnCheckedChanged="rbtOT1_OnCheckedChanged" />
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td width="230px" align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                                                            <asp:Label ID="Label62" runat="server" CssClass="labelComman" Text="<%$ Resources:Attendance,Calculation Method %>"></asp:Label>
                                                                        </td>
                                                                        <td width="1px">
                                                                            :
                                                                        </td>
                                                                        <td align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                                                            <asp:DropDownList ID="ddlOTCalc1" Width="135px" runat="server" CssClass="DropdownSearch">
                                                                                <asp:ListItem Text="In" Value="In"></asp:ListItem>
                                                                                <asp:ListItem Text="Out" Value="Out"></asp:ListItem>
                                                                                <asp:ListItem Text="Both" Value="Both"></asp:ListItem>
                                                                                <asp:ListItem Text="Work Hour" Value="Work Hour"></asp:ListItem>
                                                                            </asp:DropDownList>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td width="230px" align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                                                            <asp:Label ID="Label63" runat="server" CssClass="labelComman" Text="<%$ Resources:Attendance,Value(Normal Day) %>"></asp:Label>
                                                                        </td>
                                                                        <td width="1px">
                                                                            :
                                                                        </td>
                                                                        <td align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                                                            <asp:TextBox ID="txtNormal1" Width="45px" runat="server" CssClass="textComman" />&nbsp;
                                                                            <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender15" runat="server" Enabled="True"
                                                                                TargetControlID="txtNormal1" ValidChars="1,2,3,4,5,6,7,8,9,0,.">
                                                                            </cc1:FilteredTextBoxExtender>
                                                                            <asp:DropDownList ID="ddlNormalType1" Width="70px" runat="server" CssClass="DropdownSearch">
                                                                                <asp:ListItem Text="Fixed" Value="1"></asp:ListItem>
                                                                                <asp:ListItem Text="%" Value="2"></asp:ListItem>
                                                                            </asp:DropDownList>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td width="230px" align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                                                            <asp:Label ID="Label64" runat="server" CssClass="labelComman" Text="<%$ Resources:Attendance,Value(Week Off) %>"></asp:Label>
                                                                        </td>
                                                                        <td width="1px">
                                                                            :
                                                                        </td>
                                                                        <td align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                                                            <asp:TextBox ID="txtWeekOffValue1" Width="45px" runat="server" CssClass="textComman" />&nbsp;
                                                                            <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender16" runat="server" Enabled="True"
                                                                                TargetControlID="txtWeekOffValue1" ValidChars="1,2,3,4,5,6,7,8,9,0,.">
                                                                            </cc1:FilteredTextBoxExtender>
                                                                            <asp:DropDownList ID="ddlWeekOffType1" Width="70px" runat="server" CssClass="DropdownSearch">
                                                                                <asp:ListItem Text="Fixed" Value="1"></asp:ListItem>
                                                                                <asp:ListItem Text="%" Value="2"></asp:ListItem>
                                                                            </asp:DropDownList>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td width="230px" align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                                                            <asp:Label ID="Label65" runat="server" CssClass="labelComman" Text="<%$ Resources:Attendance,Value(Holiday) %>"></asp:Label>
                                                                        </td>
                                                                        <td width="1px">
                                                                            :
                                                                        </td>
                                                                        <td align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                                                            <asp:TextBox ID="txtHolidayValue1" Width="45px" runat="server" CssClass="textComman" />&nbsp;
                                                                            <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender17" runat="server" Enabled="True"
                                                                                TargetControlID="txtHolidayValue1" ValidChars="1,2,3,4,5,6,7,8,9,0,.">
                                                                            </cc1:FilteredTextBoxExtender>
                                                                            <asp:DropDownList ID="ddlHolidayValue1" Width="70px" runat="server" CssClass="DropdownSearch">
                                                                                <asp:ListItem Text="Fixed" Value="1"></asp:ListItem>
                                                                                <asp:ListItem Text="%" Value="2"></asp:ListItem>
                                                                            </asp:DropDownList>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </fieldset>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="6" align="center" style="padding-bottom: 10PX;">
                                                        </td>
                                                    </tr>
                                                </table>
                                            </asp:Panel>
                                        </asp:Panel>
                                    </asp:Panel>
                                    <asp:Panel ID="PnlEmpPenalty" runat="server" Visible="false">
                                        <table width="100%">
                                            <tr>
                                                <td align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                                    <asp:RadioButton ID="rbtnPenaltyGroup" CssClass="labelComman" OnCheckedChanged="EmpPenalty_CheckedChanged"
                                                        runat="server" Text="<%$ Resources:Attendance,Group %>" Font-Bold="true" GroupName="EmpPenalty"
                                                        AutoPostBack="true" />
                                                    <asp:RadioButton ID="rbtnPenaltyEmp" runat="server" CssClass="labelComman" AutoPostBack="true"
                                                        Text="<%$ Resources:Attendance,Employee %>" GroupName="EmpPenalty" Font-Bold="true"
                                                        OnCheckedChanged="EmpPenalty_CheckedChanged" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:Panel ID="PnlGroupPenalty" runat="server">
                                                        <table width="100%">
                                                            <tr>
                                                                <td>
                                                                    <table width="100%">
                                                                        <tr>
                                                                            <td valign="top" align='<%= Common.ChangeTDForDefaultLeft()%>' width="170px">
                                                                                <asp:ListBox ID="lbxGroupPenalty" runat="server" Height="211px" Width="171px" SelectionMode="Multiple"
                                                                                    AutoPostBack="true" OnSelectedIndexChanged="lbxGroupPenalty_SelectedIndexChanged"
                                                                                    CssClass="list" Font-Names="Trebuchet MS" Font-Size="Small" ForeColor="Gray">
                                                                                </asp:ListBox>
                                                                            </td>
                                                                            <td valign="top" align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                                                                <asp:GridView ID="gvEmployeePenalty" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                                                                                    OnPageIndexChanging="gvEmployeePenalty_PageIndexChanging" CssClass="grid" Width="100%"
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
                                                                    <asp:Label ID="lblEmpPenalty" runat="server" Visible="false"></asp:Label>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </asp:Panel>
                                                    <asp:Panel ID="PnlPenaltyEmp" runat="server" DefaultButton="ImageButton16">
                                                        <div width="100%" style="background-image: url(../Images/bg_repeat.jpg); background-repeat: repeat;">
                                                            <table width="100%" style="padding-left: 20px; height: 38px;">
                                                                <tr>
                                                                    <td width="90px">
                                                                        <asp:Label ID="Label68" runat="server" CssClass="labelComman" Text="<%$ Resources:Attendance,Select Option %>"></asp:Label>
                                                                    </td>
                                                                    <td width="170px">
                                                                        <asp:DropDownList ID="ddlFieldPenalty" runat="server" CssClass="DropdownSearch" Height="25px"
                                                                            Width="165px">
                                                                            <asp:ListItem Text="<%$ Resources:Attendance,Employee Code %>" Value="Emp_Code"></asp:ListItem>
                                                                            <asp:ListItem Text="<%$ Resources:Attendance,Employee Name %>" Value="Emp_Name" Selected="True"></asp:ListItem>
                                                                        </asp:DropDownList>
                                                                    </td>
                                                                    <td width="125px">
                                                                        <asp:DropDownList ID="ddlOptionPenalty" runat="server" CssClass="DropdownSearch"
                                                                            Height="25px" Width="120px">
                                                                            <asp:ListItem Text="<%$ Resources:Attendance,--Select-- %>"></asp:ListItem>
                                                                            <asp:ListItem Text="<%$ Resources:Attendance,Equal %>"></asp:ListItem>
                                                                            <asp:ListItem Selected="True" Text="<%$ Resources:Attendance,Contains %>"></asp:ListItem>
                                                                            <asp:ListItem Text="<%$ Resources:Attendance,Like %>"></asp:ListItem>
                                                                        </asp:DropDownList>
                                                                    </td>
                                                                    <td width="24%">
                                                                        <asp:TextBox ID="txtValuePenalty" runat="server" CssClass="textCommanSearch" Height="14px"
                                                                            Width="100%" />
                                                                    </td>
                                                                    <td width="50px" align="center">
                                                                        <asp:ImageButton ID="ImageButton16" runat="server" CausesValidation="False" Height="25px"
                                                                            ImageUrl="~/Images/search.png" Width="25px" OnClick="btnPenaltybind_Click" ToolTip="<%$ Resources:Attendance,Search %>" />
                                                                    </td>
                                                                    <td width="30px" align="center">
                                                                        <asp:Panel ID="Panel16" runat="server" DefaultButton="btnRefresh">
                                                                            <asp:ImageButton ID="ImageButton17" runat="server" CausesValidation="False" Height="25px"
                                                                                ImageUrl="~/Images/refresh.png" Width="25px" OnClick="btnPenaltyRefresh_Click"
                                                                                ToolTip="<%$ Resources:Attendance,Refresh %>"></asp:ImageButton>
                                                                        </asp:Panel>
                                                                    </td>
                                                                    <td width="30px" align="center">
                                                                    </td>
                                                                    <td width="30px" align="center">
                                                                    </td>
                                                                    <td>
                                                                        <asp:ImageButton ID="ImageButton18" runat="server" OnClick="ImgbtnSelectAll_ClickPenalty"
                                                                            ToolTip="<%$ Resources:Attendance, Select All %>" AutoPostBack="true" ImageUrl="~/Images/selectAll.png" />
                                                                    </td>
                                                                    <td align="center">
                                                                        <asp:Label ID="lblTotalRecordPenalty" runat="server" Text="<%$ Resources:Attendance,Total Records: 0 %>"
                                                                            CssClass="labelComman"></asp:Label>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </div>
                                                        <asp:Label ID="lblSelectRecordPenalty" runat="server" Visible="false"></asp:Label>
                                                        <asp:GridView ID="gvEmpPenalty" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                                                            OnPageIndexChanging="gvEmpPenalty_PageIndexChanging" CssClass="grid" Width="100%"
                                                            DataKeyNames="Emp_Id" PageSize="<%# SystemParameter.GetPageSize() %>" Visible="false">
                                                            <Columns>
                                                                <asp:TemplateField>
                                                                    <ItemTemplate>
                                                                        <asp:CheckBox ID="chkgvSelect" runat="server" AutoPostBack="true" OnCheckedChanged="chkgvSelect_CheckedChangedPenalty" />
                                                                    </ItemTemplate>
                                                                    <HeaderTemplate>
                                                                        <asp:CheckBox ID="chkgvSelectAll" runat="server" OnCheckedChanged="chkgvSelectAll_CheckedChangedPenalty"
                                                                            AutoPostBack="true" />
                                                                    </HeaderTemplate>
                                                                    <ItemStyle HorizontalAlign="Center" />
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="<%$ Resources:Attendance,Edit %>">
                                                                    <ItemTemplate>
                                                                        <asp:ImageButton ID="btnEdit" runat="server" CausesValidation="False" CommandArgument='<%# Eval("Emp_Id") %>'
                                                                            ImageUrl="~/Images/edit.png" Visible="true" OnCommand="btnEditPenalty_Command"
                                                                            Width="16px" />
                                                                    </ItemTemplate>
                                                                    <ItemStyle HorizontalAlign="Center" />
                                                                </asp:TemplateField>
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
                                                    <hr />
                                                    <asp:Panel ID="Panel17" runat="server" Width="100%" DefaultButton="btnSavePenalty">
                                                        <table width="100%">
                                                            <tr>
                                                                <td style="padding-top: 5px;">
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td width="230px" align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                                                    <asp:Label ID="Label69" runat="server" CssClass="labelComman" Text="<%$ Resources:Attendance,Late In Functionality %>"></asp:Label>
                                                                </td>
                                                                <td width="1px">
                                                                    :
                                                                </td>
                                                                <td align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                                                    <asp:RadioButton ID="rbtLateInEnable" GroupName="Late" Text="<%$ Resources:Attendance,Enable %>"
                                                                        runat="server" CssClass="labelComman" />
                                                                    &nbsp;&nbsp;
                                                                    <asp:RadioButton ID="rbtLateInDisable" GroupName="Late" Text="<%$ Resources:Attendance,Disable %>"
                                                                        runat="server" CssClass="labelComman" />
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td width="230px" align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                                                    <asp:Label ID="Label175" runat="server" CssClass="labelComman" Text="<%$ Resources:Attendance,Early Out Functionality %>"></asp:Label>
                                                                </td>
                                                                <td width="1px">
                                                                    :
                                                                </td>
                                                                <td align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                                                    <asp:RadioButton ID="rbtEarlyOutEnable" GroupName="Early" Text="<%$ Resources:Attendance,Enable %>"
                                                                        runat="server" CssClass="labelComman" />
                                                                    &nbsp;&nbsp;
                                                                    <asp:RadioButton ID="rbtEarlyOutDisable" GroupName="Early" Text="<%$ Resources:Attendance,Disable %>"
                                                                        runat="server" CssClass="labelComman" />
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td width="230px" align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                                                    <asp:Label ID="Label70" runat="server" CssClass="labelComman" Text="<%$ Resources:Attendance,Absent Functionality %>"></asp:Label>
                                                                </td>
                                                                <td width="1px">
                                                                    :
                                                                </td>
                                                                <td align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                                                    <asp:RadioButton ID="rbtnAbsentEnable" GroupName="absent" Text="<%$ Resources:Attendance,Enable %>"
                                                                        runat="server" CssClass="labelComman" />
                                                                    &nbsp;&nbsp;
                                                                    <asp:RadioButton ID="rbtnAbsentDisable" GroupName="absent" Text="<%$ Resources:Attendance,Disable %>"
                                                                        runat="server" CssClass="labelComman" />
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <asp:HiddenField ID="hdnEmpIdPenalty" runat="server" />
                                                                <td align="center" colspan="6" style="padding-bottom: 10PX;">
                                                                    <asp:Button ID="btnSavePenalty" runat="server" CssClass="buttonCommman" OnClick="btnSavePenalty_Click"
                                                                        Text="<%$ Resources:Attendance,Save %>" Width="50px" />
                                                                    &nbsp;
                                                                    <asp:Button ID="btnResetPenalty" runat="server" CausesValidation="False" CssClass="buttonCommman"
                                                                        OnClick="btnResetPenalty_Click" Text="<%$ Resources:Attendance,Reset %>" Width="60px" />
                                                                    &nbsp;
                                                                    <asp:Button ID="btnCancelPenalty" runat="server" CausesValidation="False" CssClass="buttonCommman"
                                                                        OnClick="btnCancelPenalty_Click" Text="<%$ Resources:Attendance,Cancel %>" Width="60px" />
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </asp:Panel>
                                                </td>
                                            </tr>
                                        </table>
                                    </asp:Panel>
                                    <asp:Panel ID="pnlPen1" runat="server" class="MsgOverlayAddress" Visible="False">
                                        <asp:Panel ID="pnlPen2" runat="server" class="MsgPopUpPanelAddress" Visible="False">
                                            <asp:Panel ID="Panel13" runat="server" DefaultButton="Button23" Style="width: 100%;
                                                height: 100%; text-align: center;">
                                                <table width="100%" style="background-image: url(../Images/bg_repeat.jpg); background-repeat: repeat;">
                                                    <tr>
                                                        <td align="<%= Common.ChangeTDForDefaultLeft()%>">
                                                            <asp:Label ID="Label54" runat="server" Font-Size="14px" Font-Bold="true" CssClass="labelComman"
                                                                Text="<%$ Resources:Attendance,Edit %>"></asp:Label>
                                                        </td>
                                                        <td align="right">
                                                            <asp:ImageButton ID="ImageButton19" runat="server" ImageUrl="~/Images/close.png"
                                                                CausesValidation="False" OnClick="btnClosePanel_ClickPenalty" Height="20px" Width="20px" />
                                                        </td>
                                                    </tr>
                                                </table>
                                                <table width="100%" style="background-image: url(../Images/bgs.png); padding-left: 20px;
                                                    padding-right: 20px; padding-bottom: 0px; height: 150px">
                                                    <tr>
                                                        <td style="padding-left: 8px;" align="<%= Common.ChangeTDForDefaultLeft()%>">
                                                            <asp:Label ID="Label71" runat="server" ForeColor="#666666" Font-Bold="true" Font-Names="arial"
                                                                Font-Size="13px" Text="<%$ Resources:Attendance,Employee Code %>"></asp:Label>
                                                            <br />
                                                        </td>
                                                        <td>
                                                            :
                                                        </td>
                                                        <td align="<%= Common.ChangeTDForDefaultLeft()%>">
                                                            <asp:Label ID="lblEmpCodePen" runat="server" ForeColor="#666666" Font-Bold="true"
                                                                Font-Names="arial" Font-Size="13px"></asp:Label>
                                                        </td>
                                                        <td style="padding-left: 8px;" align="<%= Common.ChangeTDForDefaultLeft()%>">
                                                            <asp:Label ID="Label73" runat="server" ForeColor="#666666" Font-Bold="true" Font-Names="arial"
                                                                Font-Size="13px" Text="<%$ Resources:Attendance,Employee Name %>"></asp:Label>
                                                            <br />
                                                        </td>
                                                        <td>
                                                            :
                                                        </td>
                                                        <td align="<%= Common.ChangeTDForDefaultLeft()%>">
                                                            <asp:Label ID="lblEmpNamePen" runat="server" ForeColor="#666666" Font-Bold="true"
                                                                Font-Names="arial" Font-Size="13px"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td width="230px" align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                                            <asp:Label ID="Label72" runat="server" CssClass="labelComman" Text="<%$ Resources:Attendance,Late In Functionality %>"></asp:Label>
                                                        </td>
                                                        <td width="1px">
                                                            :
                                                        </td>
                                                        <td align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                                            <asp:RadioButton ID="rbtnLateInEnable" GroupName="Late" Text="<%$ Resources:Attendance,Enable %>"
                                                                runat="server" CssClass="labelComman" />
                                                            &nbsp;&nbsp;
                                                            <asp:RadioButton ID="rbtnLateInDisable" GroupName="Late" Text="<%$ Resources:Attendance,Disable %>"
                                                                runat="server" CssClass="labelComman" />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td width="230px" align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                                            <asp:Label ID="Label74" runat="server" CssClass="labelComman" Text="<%$ Resources:Attendance,Early Out Functionality %>"></asp:Label>
                                                        </td>
                                                        <td width="1px">
                                                            :
                                                        </td>
                                                        <td align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                                            <asp:RadioButton ID="rbtnEarlyEnable" GroupName="Early" Text="<%$ Resources:Attendance,Enable %>"
                                                                runat="server" CssClass="labelComman" />
                                                            &nbsp;&nbsp;
                                                            <asp:RadioButton ID="rbtnEarlyDisable" GroupName="Early" Text="<%$ Resources:Attendance,Disable %>"
                                                                runat="server" CssClass="labelComman" />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td width="230px" align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                                            <asp:Label ID="Label75" runat="server" CssClass="labelComman" Text="<%$ Resources:Attendance,Absent Functionality %>"></asp:Label>
                                                        </td>
                                                        <td width="1px">
                                                            :
                                                        </td>
                                                        <td align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                                            <asp:RadioButton ID="rbtnAbsentEnableP" GroupName="absent" Text="<%$ Resources:Attendance,Enable %>"
                                                                runat="server" CssClass="labelComman" />
                                                            &nbsp;&nbsp;
                                                            <asp:RadioButton ID="rbtnAbsentDisableP" GroupName="absent" Text="<%$ Resources:Attendance,Disable %>"
                                                                runat="server" CssClass="labelComman" />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="6" align="center" style="padding-bottom: 10PX;">
                                                            <table>
                                                                <tr>
                                                                    <td align="center">
                                                                        <asp:Button ID="Button23" runat="server" CssClass="buttonCommman" OnClick="btnUpdatePenalty_Click"
                                                                            Text="<%$ Resources:Attendance,Save %>" Width="75px" />
                                                                        &nbsp; &nbsp; &nbsp;
                                                                        <asp:Button ID="Button24" runat="server" CausesValidation="False" CssClass="buttonCommman"
                                                                            OnClick="btnCancelPopPenalty_Click" Text="<%$ Resources:Attendance,Cancel %>"
                                                                            Width="75px" />
                                                                        <asp:HiddenField ID="HiddenField3" runat="server" />
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </asp:Panel>
                                        </asp:Panel>
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
