<%@ Page Language="C#" MasterPageFile="~/ERPMaster.master" AutoEventWireup="true"
    CodeFile="StudentAdmissionMaster.aspx.cs" Inherits="Academic_StudentAdmissionMaster"
    Title="Student Admission Master" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <Triggers>
            <asp:PostBackTrigger ControlID="btnList" />
            <asp:PostBackTrigger ControlID="btnNew" />
            <asp:PostBackTrigger ControlID="btnBin" />
            <asp:PostBackTrigger ControlID="gvStudentMaster" />
        </Triggers>
        <ContentTemplate>
         <link href="../CSS/InvStyle.css" rel="stylesheet" type="text/css" />
            <table width="100%">
                <tr>
                    <td>
                        <table width="100%" cellpadding="0" cellspacing="0" style="border-color: #F0F0F0">
                            <tr style="background-color:#90BDE9";>
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
                                                <asp:Label ID="lblHeader" runat="server" Text="Student SetUp" CssClass="LableHeaderTitle"></asp:Label>
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
                                <td style="background-color:#ccddee;width:100%;" colspan="4" height="500px" valign="top">
                                    <asp:Panel ID="PnlList" runat="server">
                                        <asp:Panel ID="pnlSearchRecords" runat="server" DefaultButton="btnbind">
                                            <div  style="background-image: url(../Images/bg_repeat.jpg); background-repeat: repeat;width:100%;">
                                                <table width="100%" style="padding-left: 20px; height: 38px">
                                                    <tr>
                                                        <td width="90px">
                                                            <asp:Label ID="lblSelectField" runat="server" Text="<%$ Resources:Attendance,Select Field %>"
                                                                CssClass="labelComman"></asp:Label>
                                                        </td>
                                                        <td style="width:180px">
                                                            <asp:DropDownList ID="ddlFieldName" runat="server" CssClass="DropdownSearch" Height="25px"
                                                                Width="170px">
                                                                <asp:ListItem Selected="True" Text="Student_Name" Value="Student_Name"></asp:ListItem>
                                                                <asp:ListItem Text="FathereName" Value="Father_Name"></asp:ListItem>
                                                                <asp:ListItem Text="Class" Value="Class"></asp:ListItem>
                                                                <asp:ListItem Text="Sr No" Value="Sr_No"></asp:ListItem>
                                                                <asp:ListItem Text="Student Id " Value="Student_Id"></asp:ListItem>
                                                            </asp:DropDownList>
                                                        </td>
                                                        <td style="width:135px">
                                                            <asp:DropDownList ID="ddlOption" runat="server" CssClass="DropdownSearch" Height="25px"
                                                                Width="120px">
                                                                <asp:ListItem Text="<%$ Resources:Attendance,--Select-- %>"></asp:ListItem>
                                                                <asp:ListItem Text="<%$ Resources:Attendance,Equal %>"></asp:ListItem>
                                                                <asp:ListItem Selected="True" Text="<%$ Resources:Attendance,Contains %>"></asp:ListItem>
                                                                <asp:ListItem Text="<%$ Resources:Attendance,Like %>"></asp:ListItem>
                                                            </asp:DropDownList>
                                                        </td>
                                                        <td style="width:24%">
                                                            <asp:TextBox ID="txtValue" runat="server" CssClass="textCommanSearch" Height="14px"
                                                                Width="100%"></asp:TextBox>
                                                        </td>
                                                        <td style="width:50px" align="center">
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
                                        <asp:GridView ID="gvStudentMaster" PageSize="<%# SystemParameter.GetPageSize() %>"
                                            runat="server" AutoGenerateColumns="False" Width="100%" AllowPaging="True" AllowSorting="True"
                                            CssClass="grid" OnPageIndexChanging="gvStudentMaster_PageIndexChanging" OnSorting="gvStudentMaster_OnSorting">
                                            <Columns>
                                                <asp:TemplateField HeaderText="<%$ Resources:Attendance,Edit %>">
                                                    <ItemTemplate>
                                                        <asp:ImageButton ID="btnEdit" runat="server" CommandArgument='<%# Eval("Student_Id") %>'
                                                            ImageUrl="~/Images/edit.png" CausesValidation="False" OnCommand="btnEdit_Command"
                                                            Visible="false" ToolTip="<%$ Resources:Attendance,Edit %>" /></ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Center" />
                                                    <ItemStyle CssClass="grid" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="<%$ Resources:Attendance,Delete %>">
                                                    <ItemTemplate>
                                                        <asp:ImageButton ID="IbtnDelete" runat="server" CausesValidation="False" CommandArgument='<%# Eval("Student_Id") %>'
                                                            ImageUrl="~/Images/Erase.png" OnCommand="IbtnDelete_Command" Visible="false"
                                                            ToolTip="<%$ Resources:Attendance,Delete %>" />
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Center" />
                                                    <ItemStyle CssClass="grid" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Student Id" SortExpression="Student_Id">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblStudentId1" runat="server" Text='<%# Eval("Student_Id") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle CssClass="grid" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Student Name" SortExpression="Student_Name">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lbStudentName" runat="server" Text='<%# Eval("Student_Name") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle CssClass="grid" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Father Name" SortExpression="Father_Name">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblStudentNameL" runat="server" Text='<%# Eval("Father_Name") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle CssClass="grid" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Mother Name" SortExpression="Mother_Namee">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblMother_Name" runat="server" Text='<%# Eval("Mother_Name") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle CssClass="grid" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Sr_No" SortExpression="Sr_No">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblSr_No" runat="server" Text='<%# Eval("Sr_No") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle CssClass="grid" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Form No" SortExpression="FormNo">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblFormNo" runat="server" Text='<%# Eval("FormNo") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle CssClass="grid" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Class" SortExpression="Class">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblClass" runat="server" Text='<%# Eval("Class") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle CssClass="grid" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Mobile" SortExpression="Mobile1">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblMobile1" runat="server" Text='<%# Eval("Mobile1") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle CssClass="grid" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Student Code" SortExpression="Student_Code">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblStudentcode" runat="server" Text='<%# Eval("Student_Code") %>'></asp:Label>
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
                                    <asp:Panel ID="PnlNewEdit" runat="server" DefaultButton="btnSave">
                                        <table width="100%" style="padding-left: 43px">
                                            <tr>
                                                <td>
                                                    <asp:Label ID="lblStudent" runat="server" CssClass="labelComman" Text="Student code"></asp:Label>
                                                </td>
                                                <td>
                                                    :
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtStudent_Code" runat="server" CssClass="textComman" />
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblRegistrationNo" runat="server" CssClass="labelComman" Text=" Registration No."></asp:Label>
                                                </td>
                                                <td>
                                                    :
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtReg_No" runat="server" CssClass="textComman" />
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblCourses1" runat="server" CssClass="labelComman" Text="Course"></asp:Label>
                                                </td>
                                                <td>
                                                    :
                                                </td>
                                                <td>
                                                    <asp:DropDownList ID="ddlCourse" CssClass="textComman" runat="server" Height="28px"
                                                        Width="260px">
                                                        <asp:ListItem>....Select....</asp:ListItem>
                                                        <asp:ListItem>Arts</asp:ListItem>
                                                        <asp:ListItem>Commerce</asp:ListItem>
                                                        <asp:ListItem>Science</asp:ListItem>
                                                    </asp:DropDownList>
                                                </td>
                                                <td> <asp:Label ID="lblFormNo" runat="server" CssClass="labelComman" Text="Form No"></asp:Label></td>
                                                <td>:</td>
                                                <td> <asp:TextBox ID="txtFormNo" runat="server" CssClass="textComman" /></td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:Label ID="lblClass" CssClass="labelComman" runat="server" Text="Class"></asp:Label>
                                                </td>
                                                <td>
                                                    :
                                                </td>
                                                <td>
                                                    <asp:DropDownList ID="ddlclass" runat="server" CssClass="textComman" Height="28px"
                                                        Width="260px">
                                                        <asp:ListItem>....Select....</asp:ListItem>
                                                        <asp:ListItem>B.A Ist</asp:ListItem>
                                                        <asp:ListItem>B.A IInd</asp:ListItem>
                                                        <asp:ListItem>B.A IIIrd</asp:ListItem>
                                                        <asp:ListItem>B.Com Ist</asp:ListItem>
                                                        <asp:ListItem>B.Com IInd</asp:ListItem>
                                                        <asp:ListItem>B.Com IIInd</asp:ListItem>
                                                        <asp:ListItem>B.Sc Ist</asp:ListItem>
                                                        <asp:ListItem>B.Sc IInd</asp:ListItem>
                                                        <asp:ListItem>B.Sc IIIrd</asp:ListItem>
                                                    </asp:DropDownList>
                                                </td>
                                                <td>
                                                    <asp:Label ID="LblDoa" CssClass="labelComman" runat="server" Text="Date Of Admission"></asp:Label>
                                                </td>
                                                <td>
                                                    :
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtDoa" CssClass="textComman" runat="server" Height="28px"></asp:TextBox>
                                                    <cc1:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd/MMM/yyyy"
                                                        TargetControlID="txtDoa" />
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblsrno" CssClass="labelComman" runat="server" Text="S R No."></asp:Label>
                                                </td>
                                                <td>
                                                    :
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtSr_No" runat="server" CssClass="textComman" Height="28px"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:Label ID="lblcategory" CssClass="labelComman" runat="server" Text="Category"></asp:Label>
                                                </td>
                                                <td>
                                                    :
                                                </td>
                                                <td>
                                                    <asp:DropDownList ID="ddlCategory" runat="server" CssClass="textComman" Height="28px"
                                                        Width="260px">
                                                        <asp:ListItem>...Select...</asp:ListItem>
                                                        <asp:ListItem>Gen</asp:ListItem>
                                                        <asp:ListItem>ST</asp:ListItem>
                                                        <asp:ListItem>OBC</asp:ListItem>
                                                        <asp:ListItem>SC</asp:ListItem>
                                                        <asp:ListItem>Minority</asp:ListItem>
                                                    </asp:DropDownList>
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblenroll" runat="server" CssClass="labelComman" Text="Enrolment No."></asp:Label>
                                                </td>
                                                <td>
                                                    :
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtEnroll_No" CssClass="textComman" runat="server" Height="28px"></asp:TextBox>
                                                </td>
                                                <td>
                                                    <asp:Label ID="LblPermit" runat="server" CssClass="labelComman" Text="Permitted By"></asp:Label>
                                                </td>
                                                <td>
                                                    :
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtPermittedBy" CssClass="textComman" runat="server" Height="28px"></asp:TextBox>
                                                </td>
                                                <td width="140px">
                                                    <asp:Label ID="lblSig2" runat="server" Text="Father Signature"></asp:Label>
                                                </td>
                                                <td width="1px" valign="top">
                                                    :
                                                </td>
                                                <td>
                                                    <cc1:AsyncFileUpload ID="Sigs2" TabIndex="102" Width="200px" runat="server" OnUploadedComplete="Sigs2_UploadedComplete"
                                                        FailedValidation="False" />
                                                </td>
                                                <td>
                                                    <asp:Image ID="imgLogo2" runat="server" Width="90px" Height="20px" />
                                                    <br />
                                                    &nbsp;&nbsp;
                                                    <asp:Button ID="btnUpload2" TabIndex="103" runat="server" Text="Uplode" CssClass="buttonCommman"
                                                        OnClick="btnUpload3_Click" Height="15px" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:Label ID="lblName" CssClass="labelComman" runat="server" Text="Student Name"></asp:Label>
                                                </td>
                                                <td>
                                                    :
                                                </td>
                                                <td colspan="4">
                                                    <asp:TextBox ID="txtStudent_Name" runat="server" CssClass="textComman" Width="713px" />
                                                </td>
                                                <td>
                                                    <asp:Label ID="Label15" runat="server" CssClass="labelComman" Text="Student Name L"></asp:Label>
                                                </td>
                                                <td>
                                                    :
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtStudent_Name_L" runat="server" CssClass="textComman" />
                                                </td>
                                                <td width="140px" align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                                    <asp:Label ID="lblEmpLogo" runat="server" CssClass="labelComman" Text="<%$ Resources:Attendance,Image  %>"></asp:Label>
                                                </td>
                                                <td style="width:1px"  valign="top">
                                                    :
                                                </td>
                                                <td align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                                    <cc1:AsyncFileUpload ID="Photos" TabIndex="102" Width="250px" runat="server" OnUploadedComplete="Photos_UploadedComplete"
                                                        FailedValidation="False" />
                                                </td>
                                                <td align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                                    <asp:Image ID="imgLogo" runat="server" Width="90px" Height="90px" />
                                                    <br />
                                                    &nbsp;&nbsp;
                                                    <asp:Button ID="btnUpload" TabIndex="103" runat="server" Text="<%$ Resources:Attendance,Load %>"
                                                        CssClass="buttonCommman" OnClick="btnUpload1_Click" Height="15px" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:Label ID="lblemail" CssClass="labelComman" runat="server" Text="Email"></asp:Label>
                                                </td>
                                                <td>
                                                    :
                                                </td>
                                                <td align='<%= Common.ChangeTDForDefaultLeft()%>' colspan="4">
                                                    <asp:TextBox ID="txtEmail" runat="server" CssClass="textComman" Width="713px" />
                                                </td>
                                                <td>
                                                  <asp:Label ID="lblPlace" CssClass="labelComman" runat="server" Text="Place"></asp:Label>
                                                </td>
                                                <td>:
                                                </td>
                                                <td>
                                                  <asp:TextBox ID="txtPlace" runat="server" CssClass="textComman"></asp:TextBox>
                                                </td>
                                                <td width="140px">
                                                    <asp:Label ID="lblSig1" runat="server" Text="Student Signature"></asp:Label>
                                                </td>
                                                <td style="width:1px" valign="top">
                                                    :
                                                </td>
                                                <td>
                                                    <cc1:AsyncFileUpload ID="Sigs1" TabIndex="102" Width="200px" runat="server" OnUploadedComplete="Sigs1_UploadedComplete"
                                                        FailedValidation="False" />
                                                </td>
                                                <td>
                                                    <asp:Image ID="imgLogo1" runat="server" Width="90px" Height="20px" />
                                                    <br />
                                                    &nbsp;&nbsp;
                                                    <asp:Button ID="btnUpload1" TabIndex="103" runat="server" Text="Uplode" CssClass="buttonCommman"
                                                        OnClick="btnUpload2_Click" Height="15px" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:Label ID="lblBirth1" CssClass="labelComman" runat="server" Text="Date Of Birth"></asp:Label>
                                                </td>
                                                <td>
                                                    :
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtDob" runat="server" CssClass="textComman"></asp:TextBox>
                                                    <cc1:CalendarExtender ID="Calender" runat="server" Format="dd/MMM/yyyy" TargetControlID="txtDob" />
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblCaste" CssClass="labelComman" runat="server" Text="Caste"></asp:Label>
                                                </td>
                                                <td>
                                                    :
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtCaste" runat="server" CssClass="textComman"></asp:TextBox>
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblStreet" runat="server" CssClass="labelComman" Text="Street" />
                                                </td>
                                                <td align="center">
                                                    :
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtStreet" runat="server" CssClass="textComman"></asp:TextBox>
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblVillage" runat="server" CssClass="labelComman" Text="Village" />
                                                </td>
                                                <td align="center">
                                                    :
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtVillage" runat="server" CssClass="textComman"></asp:TextBox>
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblGrampanchayat" runat="server" CssClass="labelComman" Text="Grampanchayat" />
                                                </td>
                                                <td align="center">
                                                    :
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtGrampanchayat" runat="server" CssClass="textComman"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:Label ID="lblTehsil" runat="server" CssClass="labelComman" Text="Tehsil" />
                                                </td>
                                                <td align="center">
                                                    :
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtTehsil" runat="server" CssClass="textComman"></asp:TextBox>
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblDistrict" runat="server" CssClass="labelComman" Text="District" />
                                                </td>
                                                <td align="center">
                                                    :
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtDistrict" runat="server" CssClass="textComman"></asp:TextBox>
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblState" runat="server" CssClass="labelComman" Text="State" />
                                                </td>
                                                <td align="center">
                                                    :
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtState" runat="server" CssClass="textComman"></asp:TextBox>
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblPost" runat="server" CssClass="labelComman" Text="Post" />
                                                </td>
                                                <td align="center">
                                                    :
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtPost" runat="server" CssClass="textComman"></asp:TextBox>
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblPinCode" runat="server" CssClass="labelComman" Text="PinCode" />
                                                </td>
                                                <td align="center">
                                                    :
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtPinCode" runat="server" CssClass="textComman"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:Label ID="lblPhoneNo" runat="server" CssClass="labelComman" Text="Phone No" />
                                                </td>
                                                <td align="center">
                                                    :
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtPhoneNo" runat="server" CssClass="textComman"></asp:TextBox>
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblMobile1" runat="server" CssClass="labelComman" Text="Mobile No" />
                                                </td>
                                                <td align="center">
                                                    :
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtMobile1" runat="server" CssClass="textComman"></asp:TextBox>
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblMobile2" runat="server" CssClass="labelComman" Text="Mobile No2" />
                                                </td>
                                                <td align="center">
                                                    :
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtMobile2" runat="server" CssClass="textComman"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:Label ID="lblBankName" runat="server" CssClass="labelComman" Text="Bank Name" />
                                                </td>
                                                <td>
                                                    :
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtBankName" runat="server" CssClass="textComman"></asp:TextBox>
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblBranch" runat="server" CssClass="labelComman" Text="Branch" />
                                                </td>
                                                <td>
                                                    :
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtBranch" runat="server" CssClass="textComman"></asp:TextBox>
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblAccountNo" runat="server" CssClass="labelComman" Text="Account Number" />
                                                </td>
                                                <td>
                                                    :
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtAccountNo" runat="server" CssClass="textComman"></asp:TextBox>
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblStatus" runat="server" Text="Married Status" CssClass="labelComman"></asp:Label>
                                                </td>
                                                <td>
                                                    :
                                                </td>
                                                <td>
                                                    <asp:DropDownList ID="ddlStatus" CssClass="textComman" runat="server">
                                                        <asp:ListItem>------Select------</asp:ListItem>
                                                        <asp:ListItem>Married</asp:ListItem>
                                                        <asp:ListItem>Unmarried</asp:ListItem>
                                                        <asp:ListItem>Widow</asp:ListItem>
                                                    </asp:DropDownList>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:Label ID="lblmothertongue" runat="server" Text="Mother Tongue" CssClass="labelComman"></asp:Label>
                                                </td>
                                                <td align="left" valign="bottom">
                                                    :
                                                </td>
                                                <td>
                                                    <asp:DropDownList ID="ddlmothertongue" CssClass="textComman" runat="server">
                                                        <asp:ListItem>-----Select-----</asp:ListItem>
                                                        <asp:ListItem>Hindi</asp:ListItem>
                                                        <asp:ListItem>English</asp:ListItem>
                                                        <asp:ListItem>Rajasthani</asp:ListItem>
                                                    </asp:DropDownList>
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblNationality" CssClass="labelComman" runat="server" Text="Nationality"></asp:Label>
                                                </td>
                                                <td>
                                                    :
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtNationality" runat="server" CssClass="textComman"></asp:TextBox>
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblPH" CssClass="labelComman" runat="server" Text="If you are P.H."></asp:Label>
                                                </td>
                                                <td>
                                                    :
                                                </td>
                                                <td>
                                                    <asp:DropDownList ID="ddlPH" runat="server" Width="260px" CssClass="textComman">
                                                        <asp:ListItem>----Select----</asp:ListItem>
                                                        <asp:ListItem>Yes</asp:ListItem>
                                                        <asp:ListItem>No</asp:ListItem>
                                                    </asp:DropDownList>
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblReligion" runat="server" CssClass="labelComman" Text="<%$ Resources:Attendance,Religion %>" />
                                                </td>
                                                <td>
                                                    :
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtReligion" runat="server" CssClass="textComman"></asp:TextBox>
                                                </td>
                                                <tr>
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="lblFather_Name" runat="server" CssClass="labelComman" Text="Father's Name" />
                                                        </td>
                                                        <td>
                                                            :
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtFather_Name" runat="server" CssClass="textComman"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="lblpb" runat="server" CssClass="labelComman" Text="Professional/Bussiness" />
                                                        </td>
                                                        <td>
                                                            :
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtProfession" runat="server" CssClass="textComman"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="lblPosition" runat="server" CssClass="labelComman" Text="Position Held/Nature Of Business" />
                                                        </td>
                                                        <td>
                                                            :
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtPosition" runat="server" CssClass="textComman"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="lblincome" runat="server" CssClass="labelComman" Text="Total Annual Income" />
                                                        </td>
                                                        <td>
                                                            :
                                                        </td>
                                                        <td>
                                                         <asp:TextBox ID="txtToatalIncome" runat="server" CssClass="textComman"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="lblOfficePhoneNo" runat="server" CssClass="labelComman" Text="Office Phone Number" />
                                                        </td>
                                                        <td>
                                                            :
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtOfficePhoneNo" runat="server" CssClass="textComman"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="lblMobileNo3" runat="server" CssClass="labelComman" Text="Mobile Number" />
                                                        </td>
                                                        <td>
                                                            :
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtMobileNo3" runat="server" CssClass="textComman"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="lblOfficeAddr" runat="server" CssClass="labelComman" Text="Office Address" />
                                                        </td>
                                                        <td>
                                                            :
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtOfficeAddr" runat="server" CssClass="textComman" TextMode="multiline"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="lblMother_Name" runat="server" CssClass="labelComman" Text="Mother's Name" />
                                                        </td>
                                                        <td>
                                                            :
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtMother_Name" runat="server" CssClass="textComman"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="lblProfession1" runat="server" CssClass="labelComman" Text="Profession/Bussiness" />
                                                        </td>
                                                        <td>
                                                            :
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtProfession1" runat="server" CssClass="textComman"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="lblPosition1" runat="server" CssClass="labelComman" Text="Position Held/Nature Of Business" />
                                                        </td>
                                                        <td>
                                                            :
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtPosition1" runat="server" CssClass="textComman"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="lblTotalIncome1" runat="server" CssClass="labelComman" Text="Total Annual Income" />
                                                        </td>
                                                        <td>
                                                            :
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtToatalIncome1" runat="server" CssClass="textComman"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="lblOfficePhoneNo1" runat="server" CssClass="labelComman" Text="Office Phone Number" />
                                                        </td>
                                                        <td>
                                                            :
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtOfficePhoneNo1" runat="server" CssClass="textComman"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="lblMobileNo4" runat="server" CssClass="labelComman" Text="Mobile Number" />
                                                        </td>
                                                        <td>
                                                            :
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtMobileNo4" runat="server" CssClass="textComman"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="lblOfficeAddr1" runat="server" CssClass="labelComman" Text="Office Address" />
                                                        </td>
                                                        <td>
                                                            :
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtOfficeAddr1" runat="server" CssClass="textComman" TextMode="multiline"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="lblLocalGuardian" runat="server" CssClass="labelComman" Text="Local Guardian's Name" />
                                                        </td>
                                                        <td>
                                                            :
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtLocalGuardian" runat="server" CssClass="textComman"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="lblMobileNo5" runat="server" CssClass="labelComman" Text="Mobile Number" />
                                                        </td>
                                                        <td>
                                                            :
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtMobileNo5" runat="server" CssClass="textComman"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="lblVillage1" runat="server" CssClass="labelComman" Text="Village/Town" />
                                                        </td>
                                                        <td>
                                                            :
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtVillage1" runat="server" CssClass="textComman"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="lblAddress" runat="server" CssClass="labelComman" Text="Address" />
                                                        </td>
                                                        <td>
                                                            :
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtAddress" runat="server" CssClass="textComman" TextMode="multiline"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="lblDoctor" runat="server" CssClass="labelComman" Text="Name Of Family Doctor" />
                                                        </td>
                                                        <td>
                                                            :
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtDocotor" runat="server" CssClass="textComman"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="lblMobileNo6" runat="server" CssClass="labelComman" Text="Mobile Number" />
                                                        </td>
                                                        <td>
                                                            :
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtMobileNo6" runat="server" CssClass="textComman"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="lblBloodGroup" runat="server" CssClass="labelComman" Text="Blood Group" />
                                                        </td>
                                                        <td>
                                                            :
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtBloodGroup" runat="server" CssClass="textComman"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="lblExam" runat="server" CssClass="labelComman" Text="Qualifying Exam" />
                                                        </td>
                                                        <td>
                                                            :
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtExam" runat="server" CssClass="textComman"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="lblYear" runat="server" CssClass="labelComman" Text="Year" />
                                                        </td>
                                                        <td>
                                                            :
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtYear" runat="server" CssClass="textComman"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="lblRollNo" runat="server" CssClass="labelComman" Text="Roll Number" />
                                                        </td>
                                                        <td>
                                                            :
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtRollNo" runat="server" CssClass="textComman"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="lblEnrNo" runat="server" CssClass="labelComman" Text="Enrolment Number" />
                                                        </td>
                                                        <td>
                                                            :
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtEnrNo" runat="server" CssClass="textComman"></asp:TextBox>
                                                        </td>
                                                    </tr>






							 <table width="100%"> <tr>
                                                                <td>
                                                                    <asp:CheckBox ID="txtMq1" runat="server" Text="MarkSheet" Checked="true"></asp:CheckBox>
                                                                </td>
                                                                <td>
                                                                    <asp:CheckBox ID="txtTc" runat="server" Text="Transfer Certificate" Checked="true"></asp:CheckBox>
                                                                </td>
                                                                <td>
                                                                    <asp:CheckBox ID="txtMig" runat="server" Text="Migration Certificate" Checked="true"></asp:CheckBox>
                                                                </td>
                                                                <td>
                                                                    <asp:CheckBox ID="txtsc" runat="server" Text="SC/ST/OBC/PH Certificate" Checked="true"></asp:CheckBox>
                                                                </td>
                                                                <td>
                                                                    <asp:CheckBox ID="txtSports" runat="server" Text="Sports Certificate" Checked="true"></asp:CheckBox>
                                                                </td>
                                                                <td>
                                                                    <asp:CheckBox ID="txtBonafide" runat="server" Text="Bonafide Certificate" Checked="true"></asp:CheckBox>
                                                                </td>
                                                                <td>
                                                                    <asp:CheckBox ID="txtSE" runat="server" Text="Secondary Exam Certificate" Checked="true"></asp:CheckBox>
                                                                </td>
                                                                <td>
                                                                    <asp:CheckBox ID="txtCC" runat="server" Text="Character Certificate" Checked="true"></asp:CheckBox>
                                                                </td>
                                                                <td>
                                                                    <asp:CheckBox ID="txtPP" runat="server" Text="Passport Size Photo" Checked="true"></asp:CheckBox>
                                                                </td>
                                                                <td>
                                                                    <asp:CheckBox ID="txtPAR" runat="server" Text="Personality Assesment Report" Checked="true"></asp:CheckBox>
                                                                </td>
                                                                <td>
                                                                    <asp:CheckBox ID="txtNCC" runat="server" Text="NCC/NSS/SCOUTS Certificate" Checked="true"></asp:CheckBox>
                                                                </td>
                                                            </tr>





















                                                    <tr>
                                                        <table width="100%">
                                                            <tr>
                                                                <td>
                                                                </td>
                                                                <td>
                                                                    <asp:Label ID="Label3" runat="server" CssClass="labelComman" Text="Board/University" />
                                                                </td>
                                                                <td>
                                                                    <asp:Label ID="Label4" runat="server" CssClass="labelComman" Text="Name of Institute And City" />
                                                                </td>
                                                                <td>
                                                                    <asp:Label ID="Label6" runat="server" CssClass="labelComman" Text="Year1" />
                                                                </td>
                                                                <td>
                                                                    <asp:Label ID="Label7" runat="server" CssClass="labelComman" Text="Max. Marks" />
                                                                </td>
                                                                <td>
                                                                    <asp:Label ID="Label8" runat="server" CssClass="labelComman" Text="Marks Obt." />
                                                                </td>
                                                                <td>
                                                                    <asp:Label ID="Label9" runat="server" CssClass="labelComman" Text="% of Marks" />
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    <asp:Label ID="Label10" runat="server" CssClass="labelComman" Text="X Standard" />
                                                                </td>
                                                                <td>
                                                                    <asp:TextBox ID="txtX_Board" runat="server" CssClass="textComman"></asp:TextBox>
                                                                </td>
                                                                <td>
                                                                    <asp:TextBox ID="txtX_Inst" runat="server" CssClass="textComman"></asp:TextBox>
                                                                </td>
                                                                <td>
                                                                    <asp:TextBox ID="txtX_Year" runat="server" CssClass="textComman"></asp:TextBox>
                                                                </td>
                                                                <td>
                                                                    <asp:TextBox ID="txtX_MM" runat="server" CssClass="textComman"></asp:TextBox>
                                                                </td>
                                                                <td>
                                                                    <asp:TextBox ID="txtX_Obt" runat="server" CssClass="textComman"></asp:TextBox>
                                                                </td>
                                                                <td>
                                                                    <asp:TextBox ID="txtX_Per" runat="server" CssClass="textComman"></asp:TextBox>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    <asp:Label ID="Label11" runat="server" CssClass="labelComman" Text="XII Standard" />
                                                                </td>
                                                                <td>
                                                                    <asp:TextBox ID="txtXII_Board" runat="server" CssClass="textComman"></asp:TextBox>
                                                                </td>
                                                                <td>
                                                                    <asp:TextBox ID="txtXII_Inst" runat="server" CssClass="textComman"></asp:TextBox>
                                                                </td>
                                                                <td>
                                                                    <asp:TextBox ID="txtXII_Year" runat="server" CssClass="textComman"></asp:TextBox>
                                                                </td>
                                                                <td>
                                                                    <asp:TextBox ID="txtXII_MM" runat="server" CssClass="textComman"></asp:TextBox>
                                                                </td>
                                                                <td>
                                                                    <asp:TextBox ID="txtXII_Obt" runat="server" CssClass="textComman"></asp:TextBox>
                                                                </td>
                                                                <td>
                                                                    <asp:TextBox ID="txtXII_Per" runat="server" CssClass="textComman"></asp:TextBox>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    <asp:Label ID="Label12" runat="server" CssClass="labelComman" Text="Graduation" />
                                                                </td>
                                                                <td>
                                                                    <asp:TextBox ID="txtGraducation" runat="server" CssClass="textComman"></asp:TextBox>
                                                                </td>
                                                                <td>
                                                                    <asp:TextBox ID="txtGrd_Inst" runat="server" CssClass="textComman"></asp:TextBox>
                                                                </td>
                                                                <td>
                                                                    <asp:TextBox ID="txtGrd_Year" runat="server" CssClass="textComman"></asp:TextBox>
                                                                </td>
                                                                <td>
                                                                    <asp:TextBox ID="txtGrd_MM" runat="server" CssClass="textComman"></asp:TextBox>
                                                                </td>
                                                                <td>
                                                                    <asp:TextBox ID="txtGrd_Obt" runat="server" CssClass="textComman"></asp:TextBox>
                                                                </td>
                                                                <td>
                                                                    <asp:TextBox ID="txtGrd_Per" runat="server" CssClass="textComman"></asp:TextBox>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    <asp:Label ID="Label13" runat="server" CssClass="labelComman" Text="Post Graduation" />
                                                                </td>
                                                                <td>
                                                                    <asp:TextBox ID="txtPostGraducation" runat="server" CssClass="textComman"></asp:TextBox>
                                                                </td>
                                                                <td>
                                                                    <asp:TextBox ID="txtPost_Inst" runat="server" CssClass="textComman"></asp:TextBox>
                                                                </td>
                                                                <td>
                                                                    <asp:TextBox ID="txtPost_Year" runat="server" CssClass="textComman"></asp:TextBox>
                                                                </td>
                                                                <td>
                                                                    <asp:TextBox ID="txtPost_MM" runat="server" CssClass="textComman"></asp:TextBox>
                                                                </td>
                                                                <td>
                                                                    <asp:TextBox ID="txtPost_Obt" runat="server" CssClass="textComman"></asp:TextBox>
                                                                </td>
                                                                <td>
                                                                    <asp:TextBox ID="txtPost_Per" runat="server" CssClass="textComman"></asp:TextBox>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    <asp:Label ID="Label14" runat="server" CssClass="labelComman" Text="Other Qualification" />
                                                                </td>
                                                                <td>
                                                                    <asp:TextBox ID="txtOtherBoard" runat="server" CssClass="textComman"></asp:TextBox>
                                                                </td>
                                                                <td>
                                                                    <asp:TextBox ID="txtOther_Inst" runat="server" CssClass="textComman"></asp:TextBox>
                                                                </td>
                                                                <td>
                                                                    <asp:TextBox ID="txtOther_Year" runat="server" CssClass="textComman"></asp:TextBox>
                                                                </td>
                                                                <td>
                                                                    <asp:TextBox ID="txtOther_MM" runat="server" CssClass="textComman"></asp:TextBox>
                                                                </td>
                                                                <td>
                                                                    <asp:TextBox ID="txtOther_Obt" runat="server" CssClass="textComman"></asp:TextBox>
                                                                </td>
                                                                <td>
                                                                    <asp:TextBox ID="txtOther_Per" runat="server" CssClass="textComman"></asp:TextBox>
                                                                </td>
                                                            </tr>
								<tr>
                                                        <td>
                                                            <asp:Label ID="lblcomsub" runat="server" CssClass="labelComman" Text="Compulsory Subjects" />
                                                        </td>
                                                        
                                                        <td>
                                                            <asp:TextBox ID="txtCompSub1" runat="server" CssClass="textComman"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtCompSub2" runat="server" CssClass="textComman"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtCompSub3" runat="server" CssClass="textComman"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtCompSub4" runat="server" CssClass="textComman"></asp:TextBox>
                                                        </td>
                                                    </tr>
							<table align="left" width="100%"><tr>
                                                        <td>
                                                            <asp:Label ID="lblPreference" runat="server" CssClass="labelComman" Text="Preference" />
                                                        </td>
                                                        <td colspan="5" align="center">
                                                            <asp:Label ID="lblPreference1" runat="server" CssClass="labelComman" Text=" Optional Subject(s) Combination" />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="lblOpSub1" runat="server" CssClass="labelComman" Text="1." />
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtOpSub1" runat="server" CssClass="textComman"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtOpSub2" runat="server" CssClass="textComman"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtOpSu3" runat="server" CssClass="textComman"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtOpSub4" runat="server" CssClass="textComman"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtOpSub5" runat="server" CssClass="textComman"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="lblOpSub2" runat="server" CssClass="labelComman" Text="2." />
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtOpSub6" runat="server" CssClass="textComman"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtOpSub7" runat="server" CssClass="textComman"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtOpSub8" runat="server" CssClass="textComman"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtOpSub9" runat="server" CssClass="textComman"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtOpSub10" runat="server" CssClass="textComman"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="lblOpSub3" runat="server" CssClass="labelComman" Text="3." />.
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtOpSub11" runat="server" CssClass="textComman"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtOpSub12" runat="server" CssClass="textComman"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtOpSub13" runat="server" CssClass="textComman"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtOpSub14" runat="server" CssClass="textComman"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtOpSu15" runat="server" CssClass="textComman"></asp:TextBox>
                                                        </td>
                                                    </tr>
							
							</table>
                                                        
							<table width="100%"><tr>
                                                        <td>
                                                            <asp:Label ID="lblDC" runat="server" CssClass="labelComman" Text="Specify Certificate Programme/Training Opted along with degree courses :" />
                                                        </td>
                                                        
                                                        <td>
                                                            <asp:TextBox ID="txtDC" runat="server" CssClass="textComman"></asp:TextBox>
                                                        </td>
                                                    </tr></table>
							<table width="100%">
                                                            <tr>
                                                                <td>
                                                                    <asp:Label ID="lblDComSub" runat="server" CssClass="labelComman" Text="Compulsory Subjects" />
                                                                </td>
                                                                <td>
                                                                    :
                                                                </td>
                                                                <td>
                                                                    <asp:TextBox ID="txtDCompSub1" runat="server" CssClass="textComman"></asp:TextBox>
                                                                </td>
                                                                <td>
                                                                    <asp:TextBox ID="txtDCompSub2" runat="server" CssClass="textComman"></asp:TextBox>
                                                                </td>
                                                                <td>
                                                                    <asp:TextBox ID="txtDCompSub3" runat="server" CssClass="textComman"></asp:TextBox>
                                                                </td>
                                                                <td>
                                                                    <asp:TextBox ID="txtDCompSub4" runat="server" CssClass="textComman"></asp:TextBox>
                                                                </td>
                                                            </tr>
									<tr>
                                                                <td>
                                                                    <asp:Label ID="lblDOpSub" runat="server" CssClass="labelComman" Text="Optional Subjects" />
                                                                </td>
                                                                <td>
                                                                    :
                                                                </td>
                                                                <td>
                                                                    <asp:TextBox ID="txtDOpsub1" runat="server" CssClass="textComman"></asp:TextBox>
                                                                </td>
                                                                <td>
                                                                    <asp:TextBox ID="txtDOpsub2" runat="server" CssClass="textComman"></asp:TextBox>
                                                                </td>
                                                                <td>
                                                                    <asp:TextBox ID="txtDOpsub3" runat="server" CssClass="textComman"></asp:TextBox>
                                                                </td>
                                                            </tr>
								<tr>
                                                                <table width="100%">
                                                                    <tr>
                                                                        <td>
                                                                            <asp:Label ID="lblnexam" runat="server" CssClass="labelComman" Text="Name Of Examination" />
                                                                        </td>
                                                                        <td>
                                                                            <asp:Label ID="lblInstitution" runat="server" CssClass="labelComman" Text="Name Of Institution" />
                                                                        </td>
                                                                        <td>
                                                                            <asp:Label ID="lblUniversity" runat="server" CssClass="labelComman" Text="Board/University" />
                                                                        </td>
                                                                        <td>
                                                                            <asp:Label ID="lblYear1" runat="server" CssClass="labelComman" Text="Year" />
                                                                        </td>
                                                                        <td>
                                                                            <asp:Label ID="lblroll" runat="server" CssClass="labelComman" Text="Roll No" />
                                                                        </td>
                                                                        <td>
                                                                            <asp:Label ID="lblres" runat="server" CssClass="labelComman" Text="Result/Reason" />
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td>
                                                                            <asp:TextBox ID="txtNoe1" runat="server" CssClass="textComman"></asp:TextBox>
                                                                        </td>
                                                                        <td>
                                                                            <asp:TextBox ID="txtNoi1" runat="server" CssClass="textComman"></asp:TextBox>
                                                                        </td>
                                                                        <td>
                                                                            <asp:TextBox ID="txtBoard1" runat="server" CssClass="textComman"></asp:TextBox>
                                                                        </td>
                                                                        <td>
                                                                            <asp:TextBox ID="txtYera1" runat="server" CssClass="textComman"></asp:TextBox>
                                                                        </td>
                                                                        <td>
                                                                            <asp:TextBox ID="txtRollNo1" runat="server" CssClass="textComman"></asp:TextBox>
                                                                        </td>
                                                                        <td>
                                                                            <asp:TextBox ID="txtResult1" runat="server" CssClass="textComman"></asp:TextBox>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td>
                                                                            <asp:TextBox ID="txtNoe2" runat="server" CssClass="textComman"></asp:TextBox>
                                                                        </td>
                                                                        <td>
                                                                            <asp:TextBox ID="txtNoi2" runat="server" CssClass="textComman"></asp:TextBox>
                                                                        </td>
                                                                        <td>
                                                                            <asp:TextBox ID="txtBoard2" runat="server" CssClass="textComman"></asp:TextBox>
                                                                        </td>
                                                                        <td>
                                                                            <asp:TextBox ID="txtYear2" runat="server" CssClass="textComman"></asp:TextBox>
                                                                        </td>
                                                                        <td>
                                                                            <asp:TextBox ID="txtRollNo2" runat="server" CssClass="textComman"></asp:TextBox>
                                                                        </td>
                                                                        <td>
                                                                            <asp:TextBox ID="txtResult2" runat="server" CssClass="textComman"></asp:TextBox>
                                                                        </td>
                                                                    </tr>
							</table>
                                                    </tr>
                                                    
                                                      
                                                           
                                                             
                                                                </table>
									<tr>
                                
                               <%--<td align='<%= Common.ChangeTDForDefaultLeft()%>'>--%>
                                    <table style="padding-left: 8px">
                                        <%--<tr>--%>
                                           <td style="width:80px">
                                                <asp:Button ID="btnSave" runat="server" TabIndex="107" Text="<%$ Resources:Attendance,Save %>"
                                                    Visible="false" CssClass="buttonCommman" ValidationGroup="a" OnClick="btnSave_Click" />
                                            </td>
                                            <td style="width:80px">
                                                <asp:Button ID="btnReset" runat="server" TabIndex="108" Text="<%$ Resources:Attendance,Reset %>"
                                                    CssClass="buttonCommman" CausesValidation="False" OnClick="btnReset_Click" />
                                            </td>
                                            <td style="width:80px">
                                                <asp:Button ID="btnCancel" runat="server" TabIndex="109" Text="<%$ Resources:Attendance,Cancel %>"
                                                    CssClass="buttonCommman" CausesValidation="False" OnClick="btnCancel_Click" />
                                            </td>
                                       <%--</tr>--%>
                                    </table>
                                    <asp:HiddenField ID="editid" runat="server" />
                                <%--</td>--%>
                            </tr>
                                                        </table>
                                    </asp:Panel>
                                </td>
                            </tr>
                            
                        </table>
                      
                        <asp:Panel ID="PnlBin" runat="server">
                            <asp:Panel ID="pnlbinsearch" runat="server" DefaultButton="btnbinbind">
                                <div  style="background-image: url(../Images/bg_repeat.jpg); background-repeat: repeat;width:100%;">
                                    <table width="100%" style="padding-left: 20px; height: 38px">
                                        <tr>
                                            <td style="width:90px">
                                                <asp:Label ID="lblbinSelectField" runat="server" Text="<%$ Resources:Attendance,Select Field %>"
                                                    CssClass="labelComman"></asp:Label>
                                            </td>
                                            <td style="width:180px">
                                                <asp:DropDownList ID="ddlbinFieldName" runat="server" CssClass="DropdownSearch" Height="25px"
                                                    Width="170px">
                                                    <asp:ListItem Selected="True" Text="Student_Name" Value="Student_Name"></asp:ListItem>
                                                    <asp:ListItem Text="FathereName" Value="Father_Name"></asp:ListItem>
                                                    <asp:ListItem Text="Class" Value="Class"></asp:ListItem>
                                                    <asp:ListItem Text="Sr No" Value="Sr_No"></asp:ListItem>
                                                    <asp:ListItem Text="Student Id " Value="Student_Id"></asp:ListItem>
                                                </asp:DropDownList>
                                            </td>
                                            <td style="width:135px">
                                                <asp:DropDownList ID="ddlbinOption" runat="server" CssClass="DropdownSearch" Height="25px"
                                                    Width="120px">
                                                    <asp:ListItem Text="<%$ Resources:Attendance,--Select-- %>"></asp:ListItem>
                                                    <asp:ListItem Text="<%$ Resources:Attendance,Equal %>"></asp:ListItem>
                                                    <asp:ListItem Selected="True" Text="<%$ Resources:Attendance,Contains %>"></asp:ListItem>
                                                    <asp:ListItem Text="<%$ Resources:Attendance,Like %>"></asp:ListItem>
                                                </asp:DropDownList>
                                            </td>
                                            <td style="width:24%">
                                                <asp:TextBox ID="txtbinValue" runat="server" CssClass="textCommanSearch" Height="14px"
                                                    Width="100%"></asp:TextBox>
                                            </td>
                                            <td style="width:50px" align="center">
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
                                            <td style="width:30px" align="center">
                                                <asp:Panel ID="pnlimgBtnRestore" runat="server" DefaultButton="imgBtnRestore">
                                                    <asp:ImageButton ID="imgBtnRestore" Height="25px" Width="25px" CausesValidation="False"
                                                        Visible="false" runat="server" ImageUrl="~/Images/active.png" OnClick="imgBtnRestore_Click"
                                                        ToolTip="<%$ Resources:Attendance, Active %>" />
                                                </asp:Panel>
                                            </td>
                                            <td>
                                                <asp:Panel ID="pnlImgbtnSelectAll" runat="server" DefaultButton="ImgbtnSelectAll">
                                                    <asp:ImageButton ID="ImgbtnSelectAll" runat="server" OnClick="ImgbtnSelectAll_Click"
                                                        Visible="false" ToolTip="<%$ Resources:Attendance, Select All %>" AutoPostBack="true"
                                                        ImageUrl="~/Images/selectAll.png" />
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
                            <asp:GridView ID="gvStudentMasterBin" PageSize="<%# SystemParameter.GetPageSize() %>"
                                runat="server" AutoGenerateColumns="False" Width="100%" AllowPaging="True" OnPageIndexChanging="gvStudentMasterBin_PageIndexChanging"
                                OnSorting="gvStudentMasterBin_OnSorting" AllowSorting="true" CssClass="grid">
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
                                    <asp:TemplateField HeaderText="Student Id" SortExpression="Student_Id">
                                        <ItemTemplate>
                                            <asp:Label ID="lblStudentId1" runat="server" Text='<%# Eval("Student_Id") %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle CssClass="grid" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Student Name" SortExpression="Student_Name">
                                        <ItemTemplate>
                                            <asp:Label ID="lbStudentName" runat="server" Text='<%# Eval("Student_Name") %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle CssClass="grid" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Father Name" SortExpression="Father_Name">
                                        <ItemTemplate>
                                            <asp:Label ID="lblStudentNameL" runat="server" Text='<%# Eval("Father_Name") %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle CssClass="grid" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Mother Name" SortExpression="Mother_Namee">
                                        <ItemTemplate>
                                            <asp:Label ID="lblMother_Name" runat="server" Text='<%# Eval("Mother_Name") %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle CssClass="grid" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Sr_No" SortExpression="Sr_No">
                                        <ItemTemplate>
                                            <asp:Label ID="lblSr_No" runat="server" Text='<%# Eval("Sr_No") %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle CssClass="grid" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Form No" SortExpression="FormNo">
                                        <ItemTemplate>
                                            <asp:Label ID="lblFormNo" runat="server" Text='<%# Eval("FormNo") %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle CssClass="grid" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Class" SortExpression="Class">
                                        <ItemTemplate>
                                            <asp:Label ID="lblClass" runat="server" Text='<%# Eval("Class") %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle CssClass="grid" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Mobile" SortExpression="Mobile1">
                                        <ItemTemplate>
                                            <asp:Label ID="lblMobile1" runat="server" Text='<%# Eval("Mobile1") %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle CssClass="grid" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Student Code" SortExpression="Student_Code">
                                        <ItemTemplate>
                                            <asp:Label ID="lblStudentcode" runat="server" Text='<%# Eval("Student_Code") %>'></asp:Label>
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
            </td> </tr> </table>
        </ContentTemplate>
    </asp:UpdatePanel>
    <asp:UpdateProgress ID="UpdateProgress1" runat="server">
        <ProgressTemplate>
            <div id="Background">
            </div>
            <div id="Progress">
                <center>
                    <asp:Image ID="Image1" runat="server" ImageUrl="../Images/ajax-loader2.gif" style="vertical-align: middle" />
                </center>
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>
</asp:Content>
