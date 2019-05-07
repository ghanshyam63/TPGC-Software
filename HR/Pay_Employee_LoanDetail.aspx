<%@ Page Language="C#" MasterPageFile="~/ERPMaster.master" AutoEventWireup="true" CodeFile="Pay_Employee_LoanDetail.aspx.cs" Inherits="HR_Pay_Employee_LoanDetail" Title="Untitled Page" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<link href="../CSS/InvStyle.css" rel="stylesheet" type="text/css" />
    <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">
        <Triggers>
            <asp:PostBackTrigger ControlID="GridViewLoan" />
            <asp:PostBackTrigger ControlID="GridViewLoanDetailrecord" />
        </Triggers>
        <ContentTemplate>
            <table width="100%">
                <tr align='<%= Common.ChangeTDForDefaultLeft()%>'>
                    <td>
                        <table width="100%" cellpadding="0" cellspacing="0" bordercolor="#F0F0F0">
                            <tr bgcolor="#90BDE9">
                                <td style="width: 30%;">
                                    <table>
                                        <tr>
                                            <td>
                                                <img src="../Images/product_icon.png" width="31" height="30" alt="D" />
                                            </td>
                                            <td>
                                                <img src="../Images/seperater.png" width="2" height="43" alt="SS" />
                                            </td>
                                            <td width="150px" style="padding-left: 5px">
                                                <asp:Label ID="lblHeader" runat="server" Text="<%$ Resources:Attendance,Loan Detail %>"
                                                    CssClass="LableHeaderTitle"></asp:Label>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                                <td align="right" style="width: 70%;">
                                    <table cellpadding="0" cellspacing="0">
                                        <tr>
                                            <td>
                                                <%--<asp:Panel ID="pnlLeave" runat="server" CssClass="a" Width="100%">
                                                    <asp:Button ID="btnLoan" runat="server" Text="<%$ Resources:Attendance,Loan %>" Width="100px"
                                                        BorderStyle="none" BackColor="Transparent" Style="padding-top: 3px; padding-left: 15px;
                                                        background-image: url('../Images/Bin.png' ); background-repeat: no-repeat; height: 49px;
                                                        background-position: 5px 15px; font: bold 14px Trebuchet MS; color: #000000;"
                                                        OnClick="btnLoan_Click" />
                                                </asp:Panel>
                                            --%></td>
                                            <td>
                                                <%--<asp:Panel ID="PanelList" runat="server" CssClass="a" Width="100%">
                                                    <asp:Button ID="btnList" runat="server" Text="<%$ Resources:Attendance,List %>" Width="80px"
                                                        BorderStyle="none" BackColor="Transparent" Style="padding-top: 3px; padding-left: 10px;
                                                        background-image: url('../Images/List.png' ); background-repeat: no-repeat; height: 49px;
                                                        background-position: 5px 15px; font: bold 14px Trebuchet MS; color: #000000;"
                                                        OnClick="btList_Click" />
                                                </asp:Panel>
                                           --%> </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td bgcolor="#ccddee" colspan="4" width="100%" height="500px" valign="top">
     <asp:Panel ID="PanelUpdateLoan" runat="server" Width="100%">
                                        <asp:GridView ID="GridViewLoan" runat="server" AllowPaging="True" 
                                            AllowSorting="true" AutoGenerateColumns="False"
                                            CssClass="grid" Width="100%" 
                                            PageSize="<%# SystemParameter.GetPageSize() %>" 
                                            onpageindexchanging="GridViewLoan_PageIndexChanging" 
                                            onsorting="GridViewLoan_Sorting">
                                            <Columns>
                                                <asp:TemplateField HeaderText="<%$ Resources:Attendance,Edit %>">
                                                    <ItemTemplate>
                                                        <asp:ImageButton ID="btnEdit" runat="server" CommandArgument='<%# Eval("Loan_Id") %>'
                                                            ImageUrl="~/Images/edit.png" CausesValidation="False" OnCommand="btnEdit_command" 
                                                            ToolTip="<%$ Resources:Attendance,Edit %>" Width="16px" /></ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Center" />
                                                    <ItemStyle CssClass="grid" />
                                                </asp:TemplateField>
                                               <%-- <asp:TemplateField HeaderText="<%$ Resources:Attendance,Delete %>">
                                                    <ItemTemplate>
                                                        <asp:ImageButton ID="IbtnDelete" runat="server" CausesValidation="False" CommandArgument='<%# Eval("Loan_Id") %>'
                                                            ImageUrl="~/Images/Erase.png"  ToolTip="<%$ Resources:Attendance,Delete %>" />
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Center" />
                                                    <ItemStyle CssClass="grid" />
                                                </asp:TemplateField>
                                               --%> <asp:TemplateField HeaderText="<%$ Resources:Attendance,Loan Id %>" SortExpression="Loan_Id">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblEmpIdList" runat="server" Text='<%# Eval("Loan_Id") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle CssClass="grid" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="<%$ Resources:Attendance,Employee Name %>" SortExpression="Emp_Name">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblEmpnameList" runat="server" Text='<%# Eval("Emp_Name") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle CssClass="grid" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="<%$ Resources:Attendance,Loan Name %>" SortExpression="Loan_Name">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblPenaltynameList" runat="server" Text='<%# Eval("Loan_Name") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle CssClass="grid" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="<%$ Resources:Attendance,Loan Amount %>" SortExpression="Loan_Amount">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblvaluetype" runat="server" Text='<%# Eval("Loan_Amount") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle CssClass="grid" />
                                                </asp:TemplateField>
                                            </Columns>
                                            <AlternatingRowStyle CssClass="InvgridAltRow" />
                                            <HeaderStyle CssClass="Invgridheader" BorderStyle="Solid" BorderColor="#6E6E6E" BorderWidth="1px" />
                                            <PagerStyle CssClass="Invgridheader" />
                                            <RowStyle CssClass="Invgridrow" HorizontalAlign="Center" />
                                        </asp:GridView>
                                          <asp:HiddenField ID="HDFSort" runat="server" />
                                    </asp:Panel>
                                    <br />
                                    <br />
                                     <asp:Label ID="Lblemployeeid" runat="server" Font-Bold="true" CssClass="labelComman" Text="<%$ Resources:Attendance,Employee Id %>" Visible="false"></asp:Label>
                                     : <asp:Label ID="SetEmployeeId" runat="server" CssClass="labelComman" Visible="false"></asp:Label>
                                     &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                     <asp:Label ID="Lblemployeename" runat="server" Font-Bold="true" CssClass="labelComman" Text="<%$ Resources:Attendance,Employee Name %>" Visible="false"></asp:Label>
                                     : <asp:Label ID="setemployeename" runat="server" CssClass="labelComman" Visible="false"></asp:Label>
                                     
                                     
                                                
      <asp:Panel ID="Panel1" runat="server" Width="100%">
                                        <asp:GridView ID="GridViewLoanDetailrecord" runat="server"  
                                            AllowSorting="true" AutoGenerateColumns="False"
                                            CssClass="grid" Width="100%" 
                                            PageSize="<%# SystemParameter.GetPageSize() %>" 
                                            onsorting="GridViewLoanDetailrecord_Sorting" 
                                            >
                                            
                                            <Columns>
                                              <%--  <asp:TemplateField HeaderText="<%$ Resources:Attendance,Edit %>">
                                                    <ItemTemplate>
                                                        <asp:ImageButton ID="btnEdit" runat="server" CommandArgument='<%# Eval("Loan_Id") %>'
                                                            ImageUrl="~/Images/edit.png" CausesValidation="False" 
                                                            ToolTip="<%$ Resources:Attendance,Edit %>" Width="16px" /></ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Center" />
                                                    <ItemStyle CssClass="grid" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="<%$ Resources:Attendance,Delete %>">
                                                    <ItemTemplate>
                                                        <asp:ImageButton ID="IbtnDelete" runat="server" CausesValidation="False" CommandArgument='<%# Eval("Loan_Id") %>'
                                                            ImageUrl="~/Images/Erase.png"  ToolTip="<%$ Resources:Attendance,Delete %>" />
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Center" />
                                                    <ItemStyle CssClass="grid" />
                                                </asp:TemplateField>--%>
                                                <asp:TemplateField HeaderText="<%$ Resources:Attendance,Month %>"  >
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblEmpIdList" runat="server" Text='<%# GetType(Eval("Month").ToString()) %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle CssClass="grid" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="<%$ Resources:Attendance,Year %>" >
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblEmpnameList" runat="server" Text='<%# Eval("Year") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle CssClass="grid" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="<%$ Resources:Attendance,Previous Balance %>" >
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblPenaltynameList" runat="server" Text='<%# Eval("Previous_Balance") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle CssClass="grid" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="<%$ Resources:Attendance,Monthly Installment %>" >
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblvaluetype1" runat="server" Text='<%# Eval("Montly_Installment") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle CssClass="grid" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="<%$ Resources:Attendance,Total Amount %>" >
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblvaluetype2" runat="server" Text='<%# Eval("Total_Amount") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle CssClass="grid" />
                                                </asp:TemplateField>
                                           
                                           <asp:TemplateField HeaderText="<%$ Resources:Attendance,Employee Paid %>" >
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblvaluetype3" runat="server" Text='<%# Eval("Employee_Paid") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle CssClass="grid" />
                                                </asp:TemplateField>
                                                 <asp:TemplateField HeaderText="<%$ Resources:Attendance,status %>" >
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblvaluetype4" runat="server" Text='<%# Eval("Is_Status") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle CssClass="grid" />
                                                </asp:TemplateField>
                                          
                                           
                                            </Columns>
                                            <AlternatingRowStyle CssClass="InvgridAltRow" />
                                            <HeaderStyle CssClass="Invgridheader" BorderStyle="Solid" BorderColor="#6E6E6E" BorderWidth="1px" />
                                            <PagerStyle CssClass="Invgridheader" />
                                            <RowStyle CssClass="Invgridrow" HorizontalAlign="Center" />
                                        </asp:GridView>
                                          <asp:HiddenField ID="HdfSortDetail" runat="server" />
                                          <asp:HiddenField ID="hiddenid" runat="server" />
                                    </asp:Panel>
     
                                   </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>
    <asp:UpdateProgress runat="server" AssociatedUpdatePanelID="UpdatePanel2" ID="updategvprogress">
        <ProgressTemplate>
            <div id="progressBackgroundFilter">
            </div>
            <div id="processMessainfoge" class="progressBackgroundFilter">
                Loading…<br />
                <br />
                <img alt="Loading" src="../Images/ajax-loader2.gif" class="processMessage" />
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>



</asp:Content>

