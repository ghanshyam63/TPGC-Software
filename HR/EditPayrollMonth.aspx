<%@ Page Language="C#" MasterPageFile="~/ERPMaster.master" AutoEventWireup="true"
    CodeFile="EditPayrollMonth.aspx.cs" Inherits="HR_EditPayrollMonth" Title="Untitled Page" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <link href="../CSS/AJAX.css" rel="stylesheet" type="text/css" />
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <table width="100%" cellpadding="0" cellspacing="0" bordercolor="#F0F0F0">
                <tr>
                    <td align='<%= Common.ChangeTDForDefaultLeft()%>'>
                        <asp:Panel ID="pnlheader" runat="server" BackColor="#90BDE9">
                            <table>
                                <tr>
                                    <td>
                                        <img src="../Images/product_icon.png" width="31" height="30" alt="D" />
                                    </td>
                                    <td>
                                        <img src="../Images/seperater.png" width="2" height="43" alt="SS" />
                                    </td>
                                    <td style="padding-left: 5px">
                                        <asp:Label ID="lblHeader" runat="server" Text="<%$ Resources:Attendance,Edit Payroll %>"
                                            CssClass="LableHeaderTitle"></asp:Label>
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
                                            <asp:Panel ID="Panel5" runat="server" DefaultButton="ImageButton2">
                                                <asp:ImageButton ID="ImageButton2" runat="server" CausesValidation="False" Height="25px"
                                                    ImageUrl="~/Images/refresh.png" Width="25px" OnClick="btnLeaveRefresh_Click"
                                                    ToolTip="<%$ Resources:Attendance,Refresh %>"></asp:ImageButton>
                                            </asp:Panel>
                                        </td>
                                        <td width="30px" align="center">
                                        </td>
                                        <td width="30px" align="center">
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
                                DataKeyNames="Emp_Id" PageSize="<%# SystemParameter.GetPageSize() %>">
                                <Columns>
                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <asp:ImageButton ID="imgBtnEmpEdit" runat="server" ImageUrl="~/Images/edit.png" Width="16px"
                                                OnCommand="btnEmpEdit_Command" CommandArgument='<%# Eval("Emp_Id") %>' />
                                        </ItemTemplate>
                                        <ItemStyle CssClass="grid" HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="<%$ Resources:Attendance,Employee Code %>">
                                        <ItemTemplate>
                                            <asp:Label ID="lblEmpCode" runat="server" Text='<%# Eval("Emp_Id") %>'></asp:Label>
                                            <asp:Label ID="lblEmpId" Visible="false" runat="server" Text='<%# Eval("Emp_Id") %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle CssClass="grid" />
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="Emp_Name" HeaderText="<%$ Resources:Attendance,Employee Name %>"
                                        SortExpression="Emp_Name" ItemStyle-CssClass="grid" ItemStyle-Width="40%">
                                        <ItemStyle CssClass="grid" Width="40%" />
                                    </asp:BoundField>
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
                        <asp:Panel ID="pnlheadername" runat="server" Width="100%" Visible="False">
                            <table width="100%" style="background-image: url(../Images/bgs.png); padding-left: 20px;
                                padding-right: 20px; padding-bottom: 0px; height: 50px">
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
                            </table>
                        </asp:Panel>
                        <asp:Panel ID="pnlLeave" runat="server" Width="100%" Visible="false">
                            <table>
                                <tr>
                                    <td align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                        <table>
                                            <tr>
                                                <td align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                                    <asp:Panel ID="pnlMenuAllowance" runat="server" CssClass="a">
                                                        <asp:Button ID="btnAllowance" runat="server" Text="<%$ Resources:Attendance,Allowance %>"
                                                            Width="90px" BorderStyle="none" BackColor="Transparent" OnClick="btnAllowance_Click"
                                                            Style="padding-left: 10px; padding-top: 3px; background-repeat: no-repeat; height: 49px;
                                                            background-position: 5px 15px; font: bold 14px Trebuchet MS; color: #000000;" />
                                                    </asp:Panel>
                                                </td>
                                                <td align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                                    <asp:Panel ID="pnlMenuDeduction" runat="server" CssClass="a">
                                                        <asp:Button ID="btndeduction" runat="server" Text="<%$ Resources:Attendance,Deduction %>"
                                                            Width="90px" BorderStyle="none" BackColor="Transparent" OnClick="btndeduction_Click"
                                                            Style="padding-left: 10px; padding-top: 3px; background-repeat: no-repeat; height: 49px;
                                                            background-position: 5px 15px; font: bold 14px Trebuchet MS; color: #000000;" />
                                                    </asp:Panel>
                                                </td>
                                                <td align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                                    <asp:Panel ID="pnlMenupenaltyclaim" runat="server" CssClass="a" Width="100%">
                                                        <asp:Button ID="btnclaimpenalty" runat="server" Text="<%$ Resources:Attendance,Penalty/Claim %>"
                                                            Width="100px" BorderStyle="none" BackColor="Transparent" OnClick="btnclaimpenalty_Click"
                                                            Style="padding-left: 10px; padding-top: 3px; background-repeat: no-repeat; height: 49px;
                                                            background-position: 5px 15px; font: bold 14px Trebuchet MS; color: #000000;" />
                                                    </asp:Panel>
                                                </td>
                                                <td>
                                                    <asp:Panel ID="pnlmenuloan" runat="server" CssClass="a" Width="100%">
                                                        <asp:Button ID="btnloan" runat="server" Text="<%$ Resources:Attendance,Loan %>" Width="90px"
                                                            BorderStyle="none" BackColor="Transparent" OnClick="btnloan_Click" Style="padding-left: 10px;
                                                            padding-top: 3px; background-repeat: no-repeat; height: 49px; background-position: 5px 15px;
                                                            font: bold 14px Trebuchet MS; color: #000000;" />
                                                    </asp:Panel>
                                                </td>
                                                <td align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                                    <asp:Panel ID="pnlattendance" runat="server" CssClass="a">
                                                        <asp:Button ID="btnattendance" runat="server" Text="<%$ Resources:Attendance,Attendance1 %>"
                                                            Width="85px" BorderStyle="none" BackColor="Transparent" OnClick="btnattendance_Click"
                                                            Style="padding-left: 10px; padding-top: 3px; background-repeat: no-repeat; height: 49px;
                                                            background-position: 5px 15px; font: bold 14px Trebuchet MS; color: #000000;" />
                                                    </asp:Panel>
                                                </td>
                                                <td align="right">
                                                    <asp:LinkButton ID="lnkbtnback" runat="server" OnClick="lnkbtnback_Click">Back</asp:LinkButton>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                            </table>
                        </asp:Panel>
                        <asp:Panel ID="pnlgvallowance" runat="server" Visible="false">
                            <table width="100%">
                                <tr>
                                    <td>
                                        <asp:GridView ID="gvallowance" runat="server" AutoGenerateColumns="False" DataKeyNames="Emp_Id"
                                            PageSize="<%# SystemParameter.GetPageSize() %>" TabIndex="10" Width="100%" AllowPaging="True"
                                            OnPageIndexChanging="gvallowance_PageIndexChanging">
                                            <Columns>
                                                <asp:TemplateField HeaderText="<%$ Resources:Attendance,Allowance Type %>">
                                                    <ItemTemplate>
                                                        <asp:HiddenField ID="hdnEmpId" runat="server" Value='<%# Eval("Emp_Id") %>' />
                                                        <asp:HiddenField ID="hdnallowId" runat="server" Value='<%# Eval("Allowance_Id") %>' />
                                                        <asp:Label ID="lblType" runat="server" Text='<%# Eval("Allowance_Id") %>'></asp:Label>
                                                        <asp:HiddenField ID="hdntransId" runat="server" Value='<%# Eval("Trans_Id") %>' />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="<%$ Resources:Attendance, Allowance Value %>">
                                                    <ItemTemplate>
                                                        <asp:HiddenField ID="hdnRefId" runat="server" Value='<%# Eval("Ref_Id") %>' />
                                                        <asp:Label ID="lblAllowValue" runat="server" Text='<%# Eval("Allowance_Value") %>' />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="<%$ Resources:Attendance,Act Allowance Value %>">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtValue" runat="server" Visible="true" Text='<%# Eval("Act_Allowance_Value") %>'
                                                            Width="110px"></asp:TextBox>
                                                        <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtenderPAidLeave" runat="server"
                                                            TargetControlID="txtValue" ValidChars="0,1,2,3,4,5,6,7,8,9,.">
                                                        </cc1:FilteredTextBoxExtender>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField>
                                                    <ItemTemplate>
                                                        <asp:CheckBox ID="chkallowance" runat="server" Checked="true" />
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
                            <table style="padding-left: 15px;">
                                <tr>
                                    <td align="center">
                                        <asp:Button ID="btnsaveallow" runat="server" CausesValidation="False" CssClass="buttonCommman"
                                            OnClick="btnsaveallow_Click" TabIndex="10" Text="<%$ Resources:Attendance,Save %>"
                                            Width="60px" />
                                        &nbsp;&nbsp;
                                        <asp:Button ID="btncencelallow" runat="server" CausesValidation="False" CssClass="buttonCommman"
                                            TabIndex="9" Text="<%$ Resources:Attendance,Cancel %>" OnClick="btncencelallow_Click"
                                            Width="60px" />
                                    </td>
                                </tr>
                            </table>
                        </asp:Panel>
                        <asp:Panel ID="pnlgvdeduuc" runat="server" Visible="false">
                            <asp:GridView ID="gvdeduction" runat="server" AutoGenerateColumns="False" DataKeyNames="Emp_Id"
                                PageSize="<%# SystemParameter.GetPageSize() %>" TabIndex="10" Width="100%" AllowPaging="True"
                                OnPageIndexChanging="gvdeduction_PageIndexChanging">
                                <Columns>
                                    <asp:TemplateField HeaderText="<%$ Resources:Attendance,Deduction %>">
                                        <ItemTemplate>
                                            <asp:HiddenField ID="hdnEmpId" runat="server" Value='<%# Eval("Emp_Id") %>' />
                                            <asp:HiddenField ID="hdnDeducId" runat="server" Value='<%# Eval("Deduction_Id") %>' />
                                            <asp:Label ID="lblTypededuc" runat="server" Text='<%# Eval("Deduction_Id") %>'></asp:Label>
                                            <asp:HiddenField ID="hdntransIddeduc" runat="server" Value='<%# Eval("Trans_Id") %>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="<%$ Resources:Attendance,Deduction Value %>">
                                        <ItemTemplate>
                                            <asp:HiddenField ID="hdnRefId" runat="server" Value='<%# Eval("Ref_Id") %>' />
                                            <asp:Label ID="lblDeductValue" runat="server" Text='<%# Eval("Deduction_Value") %>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="<%$ Resources:Attendance,Act Deduction Value %>">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtDeducValue" runat="server" Text='<%# Eval("Act_Deduction_Value") %>'
                                                Width="110px"></asp:TextBox>
                                            <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtenderPAidLeave" runat="server"
                                                TargetControlID="txtDeducValue" ValidChars="0,1,2,3,4,5,6,7,8,9,.">
                                            </cc1:FilteredTextBoxExtender>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <asp:CheckBox ID="chkdeduc" runat="server" Checked="true" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                                <AlternatingRowStyle CssClass="InvgridAltRow" />
                                <HeaderStyle CssClass="Invgridheader" BorderStyle="Solid" BorderColor="#6E6E6E" BorderWidth="1px" />
                                <PagerStyle CssClass="Invgridheader" />
                                <RowStyle CssClass="Invgridrow" HorizontalAlign="Center" />
                            </asp:GridView>
                            <table style="padding-left: 15px;">
                                <tr>
                                    <td align="center">
                                        <asp:Button ID="btnSave" runat="server" CausesValidation="False" CssClass="buttonCommman"
                                            OnClick="BtnSave_Click" TabIndex="10" Text="<%$ Resources:Attendance,Save %>"
                                            Width="60px" />
                                        &nbsp;&nbsp;
                                        <asp:Button ID="btncencelDeduc" runat="server" CausesValidation="False" CssClass="buttonCommman"
                                            TabIndex="9" Text="<%$ Resources:Attendance,Cancel %>" Width="60px" OnClick="btncencelDeduc_Click" />
                                    </td>
                                </tr>
                            </table>
                        </asp:Panel>
                        <asp:Panel ID="pnlpenaltyclaim" runat="server" Width="100%" Visible="false">
                            <table width="100%" style="background-image: url(../Images/bgs.png); padding-left: 80px;
                                padding-right: 0px; padding-bottom: 0px;">
                                <tr>
                                    <td align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                        <asp:Label ID="lblpenalty" runat="server" Text="Total Penalty" ForeColor="#666666"
                                            Font-Bold="true" Font-Names="arial" Font-Size="13px" />
                                    </td>
                                    <td>
                                    </td>
                                    <td align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                        <asp:Label ID="lbltotalpenalty" runat="server" Text='<%# Eval("Employee_Penalty") %>'
                                            ForeColor="#666666" Font-Bold="true" Font-Names="arial" Font-Size="13px"></asp:Label>
                                    </td>
                                    <td>
                                    </td>
                                    <td align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                        <asp:TextBox ID="txttotalpeanlty" runat="server" Text='<%# Eval("Employee_Penalty") %>'
                                            Width="110px"></asp:TextBox>
                                        <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtenderPAidLeave" runat="server"
                                            TargetControlID="txttotalpeanlty" ValidChars="0,1,2,3,4,5,6,7,8,9,.">
                                        </cc1:FilteredTextBoxExtender>
                                    </td>
                                    <td>
                                        <asp:CheckBox ID="chkpenalty" runat="server" Checked="true" />
                                    </td>
                                </tr>
                                <tr>
                                    <td align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                        <asp:Label ID="lblclaim" runat="server" Text="Total Claim" ForeColor="#666666" Font-Bold="true"
                                            Font-Names="arial" Font-Size="13px" />
                                    </td>
                                    <td>
                                    </td>
                                    <td align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                        <asp:Label ID="lbltotalclaim" runat="server" Text='<%# Eval("Employee_Claim") %>'
                                            ForeColor="#666666" Font-Bold="true" Font-Names="arial" Font-Size="13px"></asp:Label>
                                    </td>
                                    <td>
                                    </td>
                                    <td align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                        <asp:TextBox ID="txttotalclaim" runat="server" Text='<%# Eval("Employee_Claim") %>'
                                            Width="110px"></asp:TextBox>
                                        <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" TargetControlID="txttotalclaim"
                                            ValidChars="0,1,2,3,4,5,6,7,8,9,.">
                                        </cc1:FilteredTextBoxExtender>
                                    </td>
                                    <td>
                                        <asp:CheckBox ID="chkclaim" runat="server" Checked="true" />
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="lblduepayamt" runat="server" Text=" Adjustment Amount" ForeColor="#666666"
                                            Font-Bold="true" Font-Names="arial" Font-Size="13px" />
                                    </td>
                                    <td>
                                    </td>
                                    <td align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                        <asp:Label ID="lbldueamt" runat="server" Text="" ForeColor="#666666" Font-Bold="true"
                                            Font-Names="arial" Font-Size="13px"></asp:Label>
                                    </td>
                                    <td>
                                    </td>
                                    <td align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                        <asp:TextBox ID="txtdueamt" runat="server" Text="" Width="110px"></asp:TextBox>
                                        <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server" TargetControlID="txtdueamt"
                                            ValidChars="0,1,2,3,4,5,6,7,8,9,-,.">
                                        </cc1:FilteredTextBoxExtender>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="6" align="center">
                                        <asp:Button ID="btnsaveclaimpenalty" runat="server" CausesValidation="False" CssClass="buttonCommman"
                                            OnClick="btnsaveclaimpenalty_Click" TabIndex="10" Text="<%$ Resources:Attendance,Save %>"
                                            Width="60px" />
                                        &nbsp;&nbsp;
                                        <asp:Button ID="btncencelpenaltyclaim" runat="server" CausesValidation="False" CssClass="buttonCommman"
                                            OnClick="btncencelpenaltyclaim_Click" TabIndex="9" Text="<%$ Resources:Attendance,Cancel %>"
                                            Width="60px" />
                                    </td>
                                    <caption>
                                        &nbsp;
                                    </caption>
                                </tr>
                            </table>
                        </asp:Panel>
                        <asp:Panel ID="pnlloan" runat="server" Visible="false">
                            <asp:GridView ID="gvloan" runat="server" AutoGenerateColumns="False" PageSize="<%# SystemParameter.GetPageSize() %>"
                                TabIndex="10" Width="100%" AllowPaging="True" OnPageIndexChanging="gvloan_PageIndexChanging">
                                <Columns>
                                    <asp:TemplateField HeaderText="<%$ Resources:Attendance,Loan %>">
                                        <ItemTemplate>
                                            <asp:HiddenField ID="hdnloanId" runat="server" Value='<%# Eval("Loan_Id") %>' />
                                            <asp:Label ID="lblloanname" runat="server" Text='<%# Eval("Loan_Id") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="<%$ Resources:Attendance,Loan Amount %>">
                                        <ItemTemplate>
                                            <asp:HiddenField ID="hdntrnasLoanId" runat="server" Value='<%# Eval("Trans_Id") %>' />
                                            <%--<asp:HiddenField ID="hdnmonth" runat="server" Value='<%# Eval("Month") %>' />--%>
                                            <asp:Label ID="lblloanamt" runat="server" Text='<%# Eval("Montly_Installment") %>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="<%$ Resources:Attendance,Act Loan Amount %>">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtloanamt" runat="server" Text='<%# Eval("Total_Amount") %>' Width="110px"></asp:TextBox>
                                            <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtenderPAidLeave" runat="server"
                                                TargetControlID="txtloanamt" ValidChars="0,1,2,3,4,5,6,7,8,9,.">
                                            </cc1:FilteredTextBoxExtender>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                                <AlternatingRowStyle CssClass="InvgridAltRow" />
                                <HeaderStyle CssClass="Invgridheader" BorderStyle="Solid" BorderColor="#6E6E6E" BorderWidth="1px" />
                                <PagerStyle CssClass="Invgridheader" />
                                <RowStyle CssClass="Invgridrow" HorizontalAlign="Center" />
                            </asp:GridView>
                            <table style="padding-left: 15px;">
                                <tr>
                                    <td align="center">
                                        <asp:Button ID="btnsaveloan" runat="server" CausesValidation="False" CssClass="buttonCommman"
                                            OnClick="btnsaveloan_Click" TabIndex="2" Text="<%$ Resources:Attendance,Save %>"
                                            Width="60px" />
                                        &nbsp;&nbsp;
                                        <asp:Button ID="btncenellaon" runat="server" CausesValidation="False" CssClass="buttonCommman"
                                            TabIndex="3" Text="<%$ Resources:Attendance,Cancel %>" Width="60px" OnClick="btncenellaon_Click" />
                                    </td>
                                </tr>
                            </table>
                        </asp:Panel>
                        <asp:Panel ID="pnlattnd" runat="server" Width="100%" Visible="false">
                            <table width="100%" style="background-image: url(../Images/bgs.png);background-color:#cccccc; background-repeat:repeat-x; padding-left: 80px;
                                padding-right: 0px; padding-bottom: 0px;">
                                <tr>
                                    <td align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                        <asp:Label ID="Label1" runat="server" Text="Worked Min Salary" ForeColor="#666666"
                                            Font-Bold="true" Font-Names="arial" Font-Size="13px" />
                                        <asp:Label ID="lblmonth" runat="server" Visible="false" />
                                        <asp:Label ID="lblyear" runat="server" Visible="false" />
                                        <asp:Label ID="lbltrnsid" runat="server" Visible="false" />
                                    </td>
                                    <td>
                                    </td>
                                    <td>
                                    </td>
                                    <td align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                        <asp:TextBox ID="txtworkedminsal" runat="server" Text='<%# Eval("Worked_Min_Salary") %>'
                                            Width="110px"></asp:TextBox>
                                        <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender3" runat="server" TargetControlID="txtworkedminsal"
                                            ValidChars="0,1,2,3,4,5,6,7,8,9,.">
                                        </cc1:FilteredTextBoxExtender>
                                    </td>
                                </tr>
                                <tr>
                                    <td align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                        <asp:Label ID="Label3" runat="server" Text="Normal OT Min Salary" ForeColor="#666666"
                                            Font-Bold="true" Font-Names="arial" Font-Size="13px" />
                                    </td>
                                    <td>
                                    </td>
                                    <td>
                                    </td>
                                    <td align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                        <asp:TextBox ID="txtnormalOtminsal" runat="server" Text='<%# Eval("Normal_OT_Min_Salary") %>'
                                            Width="110px"></asp:TextBox>
                                        <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender4" runat="server" TargetControlID="txtnormalOtminsal"
                                            ValidChars="0,1,2,3,4,5,6,7,8,9,.">
                                        </cc1:FilteredTextBoxExtender>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="Label5" runat="server" Text=" Week Off OT Min Salary" ForeColor="#666666"
                                            Font-Bold="true" Font-Names="arial" Font-Size="13px" />
                                    </td>
                                    <td>
                                    </td>
                                    <td>
                                    </td>
                                    <td align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                        <asp:TextBox ID="txtWeekoffotminsal" runat="server" Text='<%# Eval("Week_Off_OT_Min_Salary") %>'
                                            Width="110px"></asp:TextBox>
                                        <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender5" runat="server" TargetControlID="txtWeekoffotminsal"
                                            ValidChars="0,1,2,3,4,5,6,7,8,9,-,.">
                                        </cc1:FilteredTextBoxExtender>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="Label2" runat="server" Text="Holiday OT Min Salary " ForeColor="#666666"
                                            Font-Bold="true" Font-Names="arial" Font-Size="13px" />
                                    </td>
                                    <td>
                                    </td>
                                    <td>
                                    </td>
                                    <td align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                        <asp:TextBox ID="txthloidayotminsal" runat="server" Text='<%# Eval("Holiday_OT_Min_Salary") %>'
                                            Width="110px"></asp:TextBox>
                                        <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender6" runat="server" TargetControlID="txthloidayotminsal"
                                            ValidChars="0,1,2,3,4,5,6,7,8,9,.">
                                        </cc1:FilteredTextBoxExtender>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="Label4" runat="server" Text="Leave Days Salary " ForeColor="#666666"
                                            Font-Bold="true" Font-Names="arial" Font-Size="13px" />
                                    </td>
                                    <td>
                                    </td>
                                    <td>
                                    </td>
                                    <td align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                        <asp:TextBox ID="txtleavedayssal" runat="server" Text='<%# Eval("Leave_Days_Salary") %>'
                                            Width="110px"></asp:TextBox>
                                        <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender7" runat="server" TargetControlID="txtleavedayssal"
                                            ValidChars="0,1,2,3,4,5,6,7,8,9,-,.">
                                        </cc1:FilteredTextBoxExtender>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="Label6" runat="server" Text="Week Off Salary " ForeColor="#666666"
                                            Font-Bold="true" Font-Names="arial" Font-Size="13px" />
                                    </td>
                                    <td>
                                    </td>
                                    <td>
                                    </td>
                                    <td align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                        <asp:TextBox ID="txtweekoffsal" runat="server" Text='<%# Eval("Week_Off_Salary") %>'
                                            Width="110px"></asp:TextBox>
                                        <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender8" runat="server" TargetControlID="txtweekoffsal"
                                            ValidChars="0,1,2,3,4,5,6,7,8,9,-,.">
                                        </cc1:FilteredTextBoxExtender>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="Label7" runat="server" Text="Holidays Salary " ForeColor="#666666"
                                            Font-Bold="true" Font-Names="arial" Font-Size="13px" />
                                    </td>
                                    <td>
                                    </td>
                                    <td>
                                    </td>
                                    <td align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                        <asp:TextBox ID="txtholidayssal" runat="server" Text='<%# Eval("Holidays_Salary") %>'
                                            Width="110px"></asp:TextBox>
                                        <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender9" runat="server" TargetControlID="txtholidayssal"
                                            ValidChars="0,1,2,3,4,5,6,7,8,9,-,.">
                                        </cc1:FilteredTextBoxExtender>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="Label8" runat="server" Text="Absent Penalty " ForeColor="#666666"
                                            Font-Bold="true" Font-Names="arial" Font-Size="13px" />
                                    </td>
                                    <td>
                                    </td>
                                    <td>
                                    </td>
                                    <td align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                        <asp:TextBox ID="txtAbsaentsal" runat="server" Text='<%# Eval("Absent_Salary") %>'
                                            Width="110px"></asp:TextBox>
                                        <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender10" runat="server" TargetControlID="txtAbsaentsal"
                                            ValidChars="0,1,2,3,4,5,6,7,8,9,-,.">
                                        </cc1:FilteredTextBoxExtender>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="Label9" runat="server" Text="Late Min Penalty " ForeColor="#666666"
                                            Font-Bold="true" Font-Names="arial" Font-Size="13px" />
                                    </td>
                                    <td>
                                    </td>
                                    <td>
                                    </td>
                                    <td align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                        <asp:TextBox ID="txtlateminsal" runat="server" Text='<%# Eval("Late_Min_Penalty") %>'
                                            Width="110px"></asp:TextBox>
                                        <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender11" runat="server" TargetControlID="txtlateminsal"
                                            ValidChars="0,1,2,3,4,5,6,7,8,9,.">
                                        </cc1:FilteredTextBoxExtender>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="Label10" runat="server" Text="Early Min Penalty " ForeColor="#666666"
                                            Font-Bold="true" Font-Names="arial" Font-Size="13px" />
                                    </td>
                                    <td>
                                    </td>
                                    <td>
                                    </td>
                                    <td align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                        <asp:TextBox ID="txtearlyminsal" runat="server" Text='<%# Eval("Early_Min_Penalty") %>'
                                            Width="110px"></asp:TextBox>
                                        <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender12" runat="server" TargetControlID="txtearlyminsal"
                                            ValidChars="0,1,2,3,4,5,6,7,8,9,.">
                                        </cc1:FilteredTextBoxExtender>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="Label12" runat="server" Text="Patial Violation Penalty " ForeColor="#666666"
                                            Font-Bold="true" Font-Names="arial" Font-Size="13px" />
                                    </td>
                                    <td>
                                    </td>
                                    <td>
                                    </td>
                                    <td align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                        <asp:TextBox ID="txtpatialvoisal" runat="server" Text='<%# Eval("Patial_Violation_Penalty") %>'
                                            Width="110px"></asp:TextBox>
                                        <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender13" runat="server" TargetControlID="txtpatialvoisal"
                                            ValidChars="0,1,2,3,4,5,6,7,8,9,.">
                                        </cc1:FilteredTextBoxExtender>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="4" align="center">
                                        <asp:Button ID="btnsaveattend" runat="server" CausesValidation="False" CssClass="buttonCommman"
                                            OnClick="btnsaveattend_Click" TabIndex="10" Text="<%$ Resources:Attendance,Save %>"
                                            Width="60px" />
                                        &nbsp;&nbsp;
                                        <asp:Button ID="btncencelattend" runat="server" CausesValidation="False" CssClass="buttonCommman"
                                            OnClick="btncencelattend_Click" TabIndex="9" Text="<%$ Resources:Attendance,Cancel %>"
                                            Width="60px" />
                                    </td>
                                    <caption>
                                        &nbsp;
                                    </caption>
                                </tr>
                            </table>
                        </asp:Panel>
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
