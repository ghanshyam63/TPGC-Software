<%@ Page Language="C#" MasterPageFile="~/ERPMaster.master" AutoEventWireup="true"
    CodeFile="ShiftManagement.aspx.cs" Inherits="Attendance_ShiftManagement" Title="Time Table Setup" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <link href="../CSS/InvStyle.css" rel="stylesheet" type="text/css" />
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <Triggers>
            <asp:PostBackTrigger ControlID="btnList" />
            <asp:PostBackTrigger ControlID="btnNew" />
            <asp:PostBackTrigger ControlID="btnBin" />
            <asp:PostBackTrigger ControlID="gvShift" />
        </Triggers>
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
                                                <asp:Label ID="lblHeader" runat="server" Text="<%$ Resources:Attendance,Shift Management Setup %>"
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
                                                        padding-top: 3px; background-image: url(  '../Images/List.png' ); background-repeat: no-repeat;
                                                        height: 49px; background-position: 5px 15px; font: bold 14px Trebuchet MS; color: #000000;" />
                                                </asp:Panel>
                                            </td>
                                            <td>
                                                <asp:Panel ID="pnlMenuNew" runat="server" CssClass="a">
                                                    <asp:Button ID="btnNew" runat="server" Text="<%$ Resources:Attendance,New %>" Width="90px"
                                                        BorderStyle="none" BackColor="Transparent" OnClick="btnNew_Click" Style="padding-left: 10px;
                                                        padding-top: 3px; background-image: url(  '../Images/New.png' ); background-repeat: no-repeat;
                                                        height: 49px; background-position: 5px 15px; font: bold 14px Trebuchet MS; color: #000000;" />
                                                </asp:Panel>
                                            </td>
                                            <td>
                                                <asp:Panel ID="pnlMenuBin" runat="server" CssClass="a" Width="100%">
                                                    <asp:Button ID="btnBin" runat="server" Text="<%$ Resources:Attendance,Bin %>" Width="90px"
                                                        BorderStyle="none" BackColor="Transparent" OnClick="btnBin_Click" Style="padding-left: 10px;
                                                        padding-top: 3px; background-image: url(  '../Images/Bin.png' ); background-repeat: no-repeat;
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
                                                                <asp:ListItem Selected="True" Text="<%$ Resources:Attendance,Shift Name %>" Value="Shift_Name"></asp:ListItem>
                                                                <asp:ListItem Text="<%$ Resources:Attendance,Shift Name(Local) %>" Value="Shift_Name_L"></asp:ListItem>
                                                                <asp:ListItem Text="<%$ Resources:Attendance,Cycle Unit %>" Value="Cycle_Unit"></asp:ListItem>
                                                                <asp:ListItem Text="<%$ Resources:Attendance,Cycle No. %>" Value="Cycle_No"></asp:ListItem>
                                                                <asp:ListItem Text="<%$ Resources:Attendance,Shift Id %>" Value="Shift_Id"></asp:ListItem>
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
                                        <asp:GridView ID="gvShift" PageSize="<%# SystemParameter.GetPageSize() %>" runat="server"
                                            AutoGenerateColumns="False" Width="100%" AllowPaging="True" AllowSorting="True"
                                            CssClass="grid" OnPageIndexChanging="gvShift_PageIndexChanging" OnSorting="gvShift_OnSorting">
                                            <Columns>
                                                <asp:TemplateField HeaderText="<%$ Resources:Attendance,Edit %>">
                                                    <ItemTemplate>
                                                        <asp:ImageButton ID="btnEdit" runat="server" CommandArgument='<%# Eval("Shift_Id") %>'
                                                            ImageUrl="~/Images/edit.png" CausesValidation="False" OnCommand="btnEdit_Command"
                                                            Visible="false" ToolTip="<%$ Resources:Attendance,Edit %>" /></ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Center" />
                                                    <ItemStyle CssClass="grid" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="<%$ Resources:Attendance,Delete %>">
                                                    <ItemTemplate>
                                                        <asp:ImageButton ID="IbtnDelete" runat="server" CausesValidation="False" CommandArgument='<%# Eval("Shift_Id") %>'
                                                            ImageUrl="~/Images/Erase.png" OnCommand="IbtnDelete_Command" Visible="false"
                                                            ToolTip="<%$ Resources:Attendance,Delete %>" />
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Center" />
                                                    <ItemStyle CssClass="grid" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="<%$ Resources:Attendance,Shift Id %>" SortExpression="Shift_Id">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblShiftId1" runat="server" Text='<%# Eval("Shift_Id") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle CssClass="grid" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="<%$ Resources:Attendance,Shift Name %>" SortExpression="Shift_Name">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lbShiftName" runat="server" Text='<%# Eval("Shift_Name") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle CssClass="grid" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="<%$ Resources:Attendance,Shift Name(Local) %>" SortExpression="Shift_Name_L">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblShiftNameL" runat="server" Text='<%# Eval("Shift_Name_L") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle CssClass="grid" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="<%$ Resources:Attendance,Cycle No. %>" SortExpression="Cycle_No">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblCycleNo" runat="server" Text='<%# Eval("Cycle_No") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle CssClass="grid" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="<%$ Resources:Attendance,Cycle Unit %>" SortExpression="Cycle_Unit">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblCycleUnit" runat="server" Text='<%# Eval("Cycle_Unit") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle CssClass="grid" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="<%$ Resources:Attendance,Apply From %>" SortExpression="Apply_From">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblApplyDate" runat="server" Text='<%# GetDate(Eval("Apply_From")) %>'></asp:Label>
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
                                    <asp:Panel ID="PnlNewEdit" runat="server">
                                        <asp:HiddenField ID="editid" runat="server" />
                                        <asp:Panel ID="pnlShiftNew" runat="server" DefaultButton="btnSave">
                                            <table width="100%" style="padding-left: 43px">
                                                <tr>
                                                    <td width="170px" align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                                        <asp:Label ID="lblShiftName" runat="server" CssClass="labelComman" Text="<%$ Resources:Attendance,Shift Name %>"></asp:Label>
                                                    </td>
                                                    <td width="1px">
                                                        :
                                                        
                                                    </td>
                                                    <td align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                                        <asp:TextBox ID="txtShiftName"  BackColor="#e3e3e3" Width="250px" runat="server" AutoPostBack="true"  OnTextChanged="txtShiftName_OnTextChanged"
                                                            CssClass="textComman" />
                                                        <cc1:AutoCompleteExtender ID="AutoCompleteExtender1" runat="server" DelimiterCharacters=""
                                                            Enabled="True" ServiceMethod="GetCompletionListShiftName" ServicePath="" CompletionInterval="100"
                                                            MinimumPrefixLength="1" CompletionSetCount="1" TargetControlID="txtShiftName"
                                                            UseContextKey="True" CompletionListCssClass="completionList" CompletionListItemCssClass="listItem"
                                                            CompletionListHighlightedItemCssClass="itemHighlighted">
                                                        </cc1:AutoCompleteExtender>
                                                    </td>
                                                    <td width="170px" align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                                        <asp:Label ID="lblShiftNameL" runat="server" CssClass="labelComman" Text="<%$ Resources:Attendance,Shift Name(Local) %>"></asp:Label>
                                                    </td>
                                                    <td width="1px">
                                                        :
                                                    </td>
                                                    <td align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                                        <asp:TextBox ID="txtShiftNameL"  Width="250px" runat="server" CssClass="textComman" />
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td width="170px" align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                                        <asp:Label ID="lblShiftCode" runat="server" CssClass="labelComman" Text="<%$ Resources:Attendance,Cycle No. %>"></asp:Label>
                                                    </td>
                                                    <td width="1px">
                                                        :
                                                    </td>
                                                    <td align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                                        <asp:TextBox ID="txtCycleNo"  Width="250px" runat="server" CssClass="textComman" />
                                                        <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" Enabled="True"
                                                            TargetControlID="txtCycleNo" ValidChars="1,2,3,4,5,6,7,8,9,0">
                                                        </cc1:FilteredTextBoxExtender>
                                                    </td>
                                                    <td width="170px" align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                                        <asp:Label ID="Label1" runat="server" CssClass="labelComman" Text="<%$ Resources:Attendance,Cycle Unit %>"></asp:Label>
                                                    </td>
                                                    <td width="1px">
                                                        :
                                                    </td>
                                                    <td align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                                        <asp:DropDownList ID="ddlCycleUnit" runat="server" Width="260px"  CssClass="labelComman">
                                                            <asp:ListItem Selected="True" Value="7">Week</asp:ListItem>
                                                            <asp:ListItem Value="1">Day</asp:ListItem>
                                                            <asp:ListItem Value="31">Month</asp:ListItem>
                                                        </asp:DropDownList>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td width="170px" align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                                        <asp:Label ID="Label2" runat="server" CssClass="labelComman" Text="<%$ Resources:Attendance,Apply From %>"></asp:Label>
                                                    </td>
                                                    <td width="1px">
                                                        :
                                                    </td>
                                                    <td align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                                        <asp:TextBox ID="txtApplyFrom"  Width="250px" runat="server" CssClass="textComman" />
                                                        <cc1:CalendarExtender ID="CalendarExtender2" runat="server" Enabled="True" TargetControlID="txtApplyFrom"
                                                            Format="dd-MMM-yyyy">
                                                        </cc1:CalendarExtender>
                                                    </td>
                                                    <td colspan="3">
                                                    </td>
                                                </tr>
                                                <tr>
                                                <td>
                                                </td>
                                                <td>
                                                </td>
                                                    <td  align="left" style="padding-left:12px;" >
                                                        <asp:Button ID="btnSave" runat="server" TabIndex="107" Text="<%$ Resources:Attendance,Save %>"
                                                            Visible="false" CssClass="buttonCommman" ValidationGroup="a" OnClick="btnSave_Click" />
                                                        &nbsp;
                                                        <asp:Button ID="btnReset" runat="server" TabIndex="108" Text="<%$ Resources:Attendance,Reset %>"
                                                            CssClass="buttonCommman" CausesValidation="False" OnClick="btnReset_Click" />
                                                        &nbsp;
                                                        <asp:Button ID="btnCancel" runat="server" TabIndex="109" Text="<%$ Resources:Attendance,Cancel %>"
                                                            CssClass="buttonCommman" CausesValidation="False" OnClick="btnCancel_Click" />
                                                    </td>
                                                    <td>
                                                </td>
                                                <td>
                                                </td>
                                                    <td  align="left" style="padding-left:12px;">
                                                        <asp:Button ID="btnAddTime" runat="server" Text="<%$ Resources:Attendance,Add Time %>" OnClick="btnAddShift_Click"
                                                            TabIndex="7" CssClass="buttonCommman" />
                                                        &nbsp;
                                                        <asp:Button ID="btnClearAll" runat="server" Text="<%$ Resources:Attendance,Clear All %>" OnClick="btnClearAll_OnClick"
                                                            TabIndex="7" CssClass="buttonCommman" />
                                                        
                                                         &nbsp;
                                                        <asp:Button ID="btnDelete" runat="server" CausesValidation="False" Text="<%$ Resources:Attendance,Delete %>"
                                                            OnClick="btnDelete_Click" TabIndex="8" CssClass="buttonCommman" />
                                                    </td>
                                                </tr>
                                            </table>
                                        </asp:Panel>
                                        <table width="100%">
                                            <tr id="TrShiftName" runat="server" visible="False">
                                                <td id="Td1" runat="server">
                                                    &nbsp;
                                                </td>
                                                <td id="Td2" runat="server">
                                                    <table cellpadding="3" cellspacing="3" width="100%">
                                                        <tr>
                                                            <td align="left" width="50px">
                                                                <asp:Label ID="Label3" runat="server" CssClass="labelComman" Font-Bold="True" Text="<%$ Resources:Attendance,Shift Id %>"></asp:Label>
                                                            </td>
                                                            <td>
                                                                :
                                                            </td>
                                                            <td align="left" width="50px">
                                                                <asp:Label ID="txtShiftId" CssClass="labelComman" runat="server" Font-Bold="True"></asp:Label>
                                                            </td>
                                                            <td align="left" width="100px">
                                                                <asp:Label ID="Label4" CssClass="labelComman" runat="server" Font-Bold="True" Text="<%$ Resources:Attendance,Shift Name %>"></asp:Label>
                                                            </td>
                                                            <td>
                                                                :
                                                            </td>
                                                            <td align="left" width="400px">
                                                                <asp:Label ID="lblShiftNameIs" CssClass="labelComman" runat="server" Font-Bold="True"></asp:Label>
                                                            </td>
                                                            <td align="left" width="100px">
                                                                <asp:Button ID="btnOk" runat="server" CausesValidation="False" CssClass="buttonCommman"
                                                                    ImageUrl="~/Images/buttonSave.png" OnClick="btnOk_Click" TabIndex="2" Text="<%$ Resources:Attendance,Save %>" />
                                                            </td>
                                                            <td align="left" width="100px">
                                                                <asp:Button ID="btnCancelPanel" runat="server" CausesValidation="False" CssClass="buttonCommman"
                                                                    ImageUrl="~/Images/buttonCancel.png" OnClick="btnCancelPanel_Click1" TabIndex="4"
                                                                    Text="<%$ Resources:Attendance,Cancel %>" />
                                                            </td>
                                                            
                                                           <td align="left" width="100px">
                                                         <asp:Button ID="Button3" runat="server" Text="<%$ Resources:Attendance,Select All %>" OnClick="btnSelectAll_OnClick"
                                                            TabIndex="7" CssClass="buttonCommman" />
                                                            </td>
                                                            <td align="left" width="100px">
                                                                <asp:Button ID="Button1" runat="server" CausesValidation="False" CssClass="buttonCommman"
                                                                    OnClick="Button1_Click" TabIndex="4" Text="<%$ Resources:Attendance,Clear All %>" />
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="height: 18px">
                                                    &nbsp;
                                                </td>
                                                <td style="height: 18px">
                                                    <asp:Panel ID="PanelShiftAss" runat="server" ScrollBars="Auto" Visible="False">
                                                        <table width="100%">
                                                            <tr>
                                                                <td valign="top" width="20%">
                                                                    <table width="100%">
                                                                        <tr>
                                                                            <td align="<%= Common.ChangeTDForDefaultLeft()%>" bgcolor="#90bde9">
                                                                                <asp:Label ID="lblSelectShift" runat="server" Font-Bold="True" Font-Names="Verdana"
                                                                                    Font-Size="10pt" Style="font-weight: 700" Text="<%$ Resources:Attendance,Select Time Table %>"  ></asp:Label>
                                                                                &nbsp;
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td align="<%= Common.ChangeTDForDefaultLeft()%>" bgcolor="#ededed">
                                                                                <asp:Panel runat="server" ID="pnlTime" ScrollBars="Auto" Height="500px">
                                                                                    <asp:CheckBoxList ID="chkTimeTableList" runat="server" AutoPostBack="True" CellPadding="5"
                                                                                        CellSpacing="5" CssClass="labelComman" OnSelectedIndexChanged="chkTimeTableList_SelectedIndexChanged"
                                                                                        RepeatColumns="1" TabIndex="1">
                                                                                    </asp:CheckBoxList>
                                                                                </asp:Panel>
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </td>
                                                                <td valign="top">
                                                                    <table width="100%">
                                                                        <tr>
                                                                            <td align="<%= Common.ChangeTDForDefaultLeft()%>" bgcolor="#90bde9">
                                                                                <asp:Label ID="lblselectdate" runat="server" Font-Bold="True" Font-Names="Verdana"
                                                                                    Font-Size="10pt" Style="font-weight: 700" Text="<%$ Resources:Attendance,Select days for time table %>" ></asp:Label>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td align="<%= Common.ChangeTDForDefaultLeft()%>" bgcolor="#ededed">
                                                                                <asp:Panel runat="server" ID="Panel1" ScrollBars="Auto" Width="650px" Height="500px">
                                                                                    <asp:CheckBoxList ID="chkDayUnderPeriod" runat="server" RepeatDirection="Vertical"
                                                                                        TabIndex="3" Width="100%" RepeatColumns="3" CssClass="labelComman">
                                                                                    </asp:CheckBoxList>
                                                                                </asp:Panel>
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td align="center" colspan="2">
                                                                    <asp:Button ID="btnBack" runat="server" CssClass="buttonCommman" OnClick="btnBack_Click"
                                                                        Text="Back" Visible="False" />
                                                                    <asp:Button ID="btnNext" runat="server" CssClass="buttonCommman" OnClick="btnNext_Click"
                                                                        Text="Next" Visible="False" />
                                                                    <asp:Button ID="Button2" runat="server" CausesValidation="False" CssClass="buttonCommman"
                                                                        ImageUrl="~/Images/buttonCancel.png" OnClick="Button2_Click" TabIndex="4" Text="View"
                                                                        Visible="False" />
                                                                    &nbsp; &nbsp; &nbsp;
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </asp:Panel>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    &nbsp;
                                                </td>
                                                <td>
                                                    <asp:Panel ID="PanView" runat="server" BorderColor="Black" BorderStyle="Double" BorderWidth="1px"
                                                        Height="200px" ScrollBars="Auto" Visible="False">
                                                        <table width="100%">
                                                            <tr>
                                                                <td valign="top" width="100%">
                                                                    <table>
                                                                        <tr>
                                                                            <td id="Td8" runat="server" align="<%= Common.ChangeTDForDefaultLeft()%>" style="width: 558px">
                                                                                <table style="width: 452px">
                                                                                    <tr>
                                                                                        <td align="center" width="250px">
                                                                                            <asp:Label ID="lbls" runat="server" Text="Days"></asp:Label>
                                                                                        </td>
                                                                                        <td>
                                                                                            <asp:Label ID="Label5" runat="server" Text="Time Table"></asp:Label>
                                                                                        </td>
                                                                                    </tr>
                                                                                    <tr>
                                                                                        <td colspan="2">
                                                                                            <asp:DataList ID="dlView" runat="server" Width="100%">
                                                                                                <ItemTemplate>
                                                                                                    <table style="width: 330px">
                                                                                                        <tr>
                                                                                                            <td>
                                                                                                                <asp:Label ID="lblDays" runat="server" Text='<%#Eval("Days") %>' />
                                                                                                            </td>
                                                                                                            <td align="right">
                                                                                                                <asp:DataList ID="GvTime" runat="server" RepeatDirection="Horizontal">
                                                                                                                    <ItemTemplate>
                                                                                                                        <asp:Label ID="lblTimeId" runat="server" Text='<%#Eval("TimeTableId") %>' />
                                                                                                                    </ItemTemplate>
                                                                                                                </asp:DataList>
                                                                                                            </td>
                                                                                                        </tr>
                                                                                                    </table>
                                                                                                </ItemTemplate>
                                                                                            </asp:DataList>
                                                                                        </td>
                                                                                    </tr>
                                                                                </table>
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
                                    </asp:Panel>
                                    <asp:Panel ID="PnlViewShift" runat="server" Height="500px" ScrollBars="Auto">
                                        <asp:GridView ID="gvShiftView" runat="server" AutoGenerateColumns="false" PageSize="<%# SystemParameter.GetPageSize() %>"
                                            Width="100%" CssClass="grid">
                                            <Columns>
                                                <asp:TemplateField>
                                                    <HeaderTemplate>
                                                        <asp:CheckBox ID="chkAllDay" runat="server" AutoPostBack="true" OnCheckedChanged="ChkAllDay_CheckedChanged" />
                                                    </HeaderTemplate>
                                                    <HeaderStyle HorizontalAlign="Left" />
                                                    <ItemTemplate>
                                                        <asp:CheckBox ID="chkDay" runat="server" />
                                                        <asp:HiddenField ID="hdDate" runat="server" Value='<%# Eval("EDutyTime") %>' />
                                                        <asp:HiddenField ID="hdnCycle_Type" runat="server" Value='<%# Eval("Cycle_Type") %>' />
                                                        <asp:HiddenField ID="hdnCycle_Day" runat="server" Value='<%# Eval("Cycle_Day") %>' />
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Left" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="<%$ Resources:Attendance,Day %>">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblDAy" runat="server" Text='<%# WriteDays(Eval("EDutyTime")) %>' />
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Left" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="<%$ Resources:Attendance,Time Table %>">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblTimeTable" runat="server" Text='<%# Eval("TimeTable_Id") %>' />
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Left" />
                                                </asp:TemplateField>
                                            </Columns>
                                            <AlternatingRowStyle CssClass="InvgridAltRow"></AlternatingRowStyle>
                                            <HeaderStyle CssClass="Invgridheader" />
                                            <PagerStyle CssClass="Invgridheader" />
                                           
                                            <RowStyle CssClass="Invgridrow" HorizontalAlign="Center"></RowStyle>
                                        </asp:GridView>
                                        <br />
                                         <asp:GridView ID="gvShiftNew"  OnRowDataBound="gvShiftNew_RowDataBound" runat="server" AutoGenerateColumns="true" PageSize="<%# SystemParameter.GetPageSize() %>"
                                            Width="100%" CssClass="grid">
                                            <Columns>
                                             <asp:TemplateField>
                                                    <HeaderTemplate>
                                                        <asp:CheckBox ID="chkAllDay" runat="server" AutoPostBack="true" OnCheckedChanged="ChkAllDay_CheckedChanged1" />
                                                    </HeaderTemplate>
                                                    <HeaderStyle HorizontalAlign="Left" />
                                                    <ItemTemplate>
                                                        <asp:CheckBox ID="chkDay" runat="server" />
                                                        
                                                        <asp:HiddenField ID="hdnCycle_Type" runat="server" Value='<%# Eval("Cycle_Type") %>' />
                                                        <asp:HiddenField ID="hdnCycle_Day" runat="server" Value='<%# Eval("Cycle_Day") %>' />
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Left" />
                                                </asp:TemplateField>
                                            
                                            </Columns>
                                             <SelectedRowStyle CssClass="Invgridrow" />
                                             <AlternatingRowStyle CssClass="InvgridAltRow"></AlternatingRowStyle>
                                            <HeaderStyle CssClass="Invgridheader" />
                                            <PagerStyle CssClass="Invgridheader" />
                                            <RowStyle CssClass="Invgridrow" HorizontalAlign="Center"></RowStyle>
                             
                                            </asp:GridView>
                                            
                                    </asp:Panel>
                                    <asp:Panel ID="PnlBin" runat="server">
                                        <asp:Panel ID="pnlbinsearch" runat="server" DefaultButton="btnbinbind">
                                            <div width="100%" style="background-image: url(../Images/bg_repeat.jpg); background-repeat: repeat;">
                                                <table width="100%" style="padding-left: 20px; height: 38px">
                                                    <tr>
                                                        <td width="90px">
                                                            <asp:Label ID="lblbinSelectField" runat="server" Text="<%$ Resources:Attendance,Select Field %>"
                                                                CssClass="labelComman"></asp:Label>
                                                        </td>
                                                        <td width="180px">
                                                            <asp:DropDownList ID="ddlbinFieldName" runat="server" CssClass="DropdownSearch" Height="25px"
                                                                Width="170px">
                                                                <asp:ListItem Selected="True" Text="<%$ Resources:Attendance,Shift Name %>" Value="Shift_Name"></asp:ListItem>
                                                                <asp:ListItem Text="<%$ Resources:Attendance,Shift Name(Local) %>" Value="Shift_Name_L"></asp:ListItem>
                                                                <asp:ListItem Text="<%$ Resources:Attendance,Cycle Unit %>" Value="Cycle_Unit"></asp:ListItem>
                                                                <asp:ListItem Text="<%$ Resources:Attendance,Cycle No. %>" Value="Cycle_No"></asp:ListItem>
                                                                <asp:ListItem Text="<%$ Resources:Attendance,Shift Id %>" Value="Shift_Id"></asp:ListItem>
                                                            </asp:DropDownList>
                                                        </td>
                                                        <td width="135px">
                                                            <asp:DropDownList ID="ddlbinOption" runat="server" CssClass="DropdownSearch" Height="25px"
                                                                Width="120px">
                                                                <asp:ListItem Text="<%$ Resources:Attendance,--Select-- %>"></asp:ListItem>
                                                                <asp:ListItem Text="<%$ Resources:Attendance,Equal %>"></asp:ListItem>
                                                                <asp:ListItem Selected="True" Text="<%$ Resources:Attendance,Contains %>"></asp:ListItem>
                                                                <asp:ListItem Text="<%$ Resources:Attendance,Like %>"></asp:ListItem>
                                                            </asp:DropDownList>
                                                        </td>
                                                        <td width="24%">
                                                            <asp:TextBox ID="txtbinValue" runat="server" CssClass="textCommanSearch" Height="14px"
                                                                Width="100%"></asp:TextBox>
                                                        </td>
                                                        <td width="50px" align="center">
                                                            <asp:ImageButton ID="btnbinbind" runat="server" CausesValidation="False" Height="25px"
                                                                ImageUrl="~/Images/search.png" OnClick="btnbinbind_Click" Width="25px" ToolTip="<%$ Resources:Attendance,Search %>">
                                                            </asp:ImageButton>
                                                        </td>
                                                        <td>
                                                            <asp:Panel ID="Panel2" runat="server" DefaultButton="btnbinRefresh">
                                                                <asp:ImageButton ID="btnbinRefresh" runat="server" CausesValidation="False" Height="25px"
                                                                    ImageUrl="~/Images/refresh.png" OnClick="btnbinRefresh_Click" Width="25px" ToolTip="<%$ Resources:Attendance,Refresh %>">
                                                                </asp:ImageButton>
                                                            </asp:Panel>
                                                        </td>
                                                        <td width="30px" align="center">
                                                            <asp:Panel ID="pnlimgBtnRestore" runat="server" DefaultButton="imgBtnRestore">
                                                                <asp:ImageButton ID="imgBtnRestore" Height="25px" Width="25px" CausesValidation="False"
                                                                    Visible="false" runat="server" ImageUrl="~/Images/active.png" OnClick="imgBtnRestore_Click"
                                                                    ToolTip="<%$ Resources:Attendance, Active %>" />
                                                            </asp:Panel>
                                                        </td>
                                                        <td>
                                                            <asp:Panel ID="pnlImgbtnSelectAll" runat="server" DefaultButton="ImgbtnSelectAll">
                                                                <asp:ImageButton ID="ImgbtnSelectAll" runat="server" OnClick="ImgbtnSelectAll_Click"
                                                                    Visible="false" ToolTip="<%$ Resources:Attendance, Select All %>" ImageUrl="~/Images/selectAll.png" />
                                                            </asp:Panel>
                                                        </td>
                                                        <td align="center">
                                                            <asp:Label ID="lblbinTotalRecords" runat="server" Text="<%$ Resources:Attendance,Total Records: 0 %>"
                                                                CssClass="labelComman"></asp:Label>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </div>
                                        </asp:Panel>
                                        <asp:GridView ID="gvShiftBin" PageSize="<%# SystemParameter.GetPageSize() %>" runat="server"
                                            AutoGenerateColumns="False" Width="100%" AllowPaging="True" OnPageIndexChanging="gvShiftBin_PageIndexChanging"
                                            OnSorting="gvShiftBin_OnSorting" AllowSorting="true" CssClass="grid">
                                            <Columns>
                                                <asp:TemplateField>
                                                    <ItemTemplate>
                                                        <asp:CheckBox ID="chkgvSelect" runat="server" OnCheckedChanged="chkgvSelect_CheckedChanged"
                                                            AutoPostBack="true" />
                                                    </ItemTemplate>
                                                    <HeaderTemplate>
                                                        <asp:CheckBox ID="chkgvSelectAll" runat="server" OnCheckedChanged="chkgvSelectAll_CheckedChanged"
                                                            AutoPostBack="true" />
                                                    </HeaderTemplate>
                                                    <ItemStyle HorizontalAlign="Center" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="<%$ Resources:Attendance,Time Table Id %>" SortExpression="Shift_Id">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblShiftId1" runat="server" Text='<%# Eval("Shift_Id") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle CssClass="grid" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="<%$ Resources:Attendance,Time Table Name %>" SortExpression="Shift_Name">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lbShiftName" runat="server" Text='<%# Eval("Shift_Name") %>'></asp:Label>
                                                        <asp:Label ID="lblShiftId" Visible="false" runat="server" Text='<%# Eval("Shift_Id") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle CssClass="grid" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="<%$ Resources:Attendance,Time Table Name(Local) %>"
                                                    SortExpression="Shift_Name_L">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblShiftNameL" runat="server" Text='<%# Eval("Shift_Name_L") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle CssClass="grid" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="<%$ Resources:Attendance,Cycle No. %>" SortExpression="Cycle_No">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblCycleNo" runat="server" Text='<%# Eval("Cycle_No") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle CssClass="grid" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="<%$ Resources:Attendance,Cycle Unit %>" SortExpression="Cycle_Unit">
                                                    <ItemTemplate>
                                                            <asp:Label ID="lblCycleUnit" runat="server" Text='<%# Eval("Cycle_Unit") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle CssClass="grid" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="<%$ Resources:Attendance,Apply From %>" SortExpression="Apply_From">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblApplyDate" runat="server" Text='<%# GetDate(Eval("Apply_From")) %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle CssClass="grid" />
                                                </asp:TemplateField>
                                            </Columns>
                                            <AlternatingRowStyle CssClass="InvgridAltRow"></AlternatingRowStyle>
                                            <HeaderStyle CssClass="Invgridheader" />
                                            <PagerStyle CssClass="Invgridheader" />
                                            <RowStyle CssClass="Invgridrow" HorizontalAlign="Center"></RowStyle>
                                        </asp:GridView>
                                        <asp:HiddenField ID="HDFSortbin" runat="server" />
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
