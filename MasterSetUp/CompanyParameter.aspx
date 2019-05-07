<%@ Page Language="C#" MasterPageFile="~/ERPMaster.master" AutoEventWireup="true"
    CodeFile="CompanyParameter.aspx.cs" Inherits="MasterSetUp_CompanyParameter"  %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <link href="../CSS/InvStyle.css" rel="stylesheet" type="text/css" />
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <Triggers>
            <asp:PostBackTrigger ControlID="btnTime" />
           
            <asp:PostBackTrigger ControlID="btnWork" />
            
             <asp:PostBackTrigger ControlID="Button7" />

<asp:PostBackTrigger ControlID="btnSMSEmail" />
<asp:PostBackTrigger ControlID="btnKeyPref" />

<asp:PostBackTrigger ControlID="Button12" />
<asp:PostBackTrigger ControlID="Button15" />
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
                                                <asp:Label ID="lblHeader" runat="server" CssClass="LableHeaderTitle"></asp:Label>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                                <td align="right">
                                    <table cellpadding="0" cellspacing="0">
                                        <tr>
                                            <td>
                                                <asp:Panel ID="pnlTimetableList" runat="server" CssClass="a">
                                                    <asp:Button ID="btnTime" runat="server" Text="<%$ Resources:Attendance,Time Table %>"
                                                        Width="90px" BorderStyle="none" BackColor="Transparent" OnClick="btnTime_Click"
                                                        Style="padding-left: 10px; padding-top: 3px; background-repeat: no-repeat; height: 49px;
                                                        background-position: 5px 15px; font: bold 14px Trebuchet MS; color: #000000;" />
                                                </asp:Panel>
                                            </td>
                                            
                                            <td>
                                                <asp:Panel ID="pnlWorkCal" runat="server" CssClass="a" Width="100%">
                                                    <asp:Button ID="btnWork" runat="server" Text="<%$ Resources:Attendance,Work Calculation %>"
                                                        Width="125px" BorderStyle="none" BackColor="Transparent" OnClick="btnWorkCalc_Click"
                                                        Style="padding-left: 10px; padding-top: 3px; background-repeat: no-repeat; height: 49px;
                                                        background-position: 5px 15px; font: bold 14px Trebuchet MS; color: #000000;" />
                                                </asp:Panel>
                                            </td>
                                            <td>
                                                <asp:Panel ID="pnlSMSEmailB" runat="server" CssClass="a" Width="100%">
                                                    <asp:Button ID="btnSMSEmail" runat="server" Text="<%$ Resources:Attendance,SMS/Email %>"
                                                        Width="90px" BorderStyle="none" BackColor="Transparent" OnClick="btnSMSEmail_Click"
                                                        Style="padding-left: 10px; padding-top: 3px; background-repeat: no-repeat; height: 49px;
                                                        background-position: 5px 15px; font: bold 14px Trebuchet MS; color: #000000;" />
                                                </asp:Panel>
                                            </td>
                                            <td>
                                                <asp:Panel ID="pnlPartialLeave" runat="server" CssClass="a" Width="100%">
                                                    <asp:Button ID="Button7" runat="server" Text="<%$ Resources:Attendance,OT/PL %>"
                                                        Width="70px" BorderStyle="none" BackColor="Transparent" OnClick="btnPartialLeave_Click"
                                                        Style="padding-left: 10px; padding-top: 3px; background-repeat: no-repeat; height: 49px;
                                                        background-position: 5px 15px; font: bold 14px Trebuchet MS; color: #000000;" />
                                                </asp:Panel>
                                            </td>
                                            <td>
                                                <asp:Panel ID="pnlKeyPref" runat="server" CssClass="a" Width="100%">
                                                    <asp:Button ID="btnKeyPref" runat="server" Text="<%$ Resources:Attendance,Keys %>"
                                                        Width="60px" BorderStyle="none" BackColor="Transparent" OnClick="btnKeyPref_Click"
                                                        Style="padding-left: 10px; padding-top: 3px; background-repeat: no-repeat; height: 49px;
                                                        background-position: 5px 15px; font: bold 14px Trebuchet MS; color: #000000;" />
                                                </asp:Panel>
                                            </td>
                                            <td>
                                                <asp:Panel ID="pnlColorCode" runat="server" CssClass="a" Width="100%">
                                                    <asp:Button ID="Button12" runat="server" Text="<%$ Resources:Attendance,Color Code  %>"
                                                        Width="100px" BorderStyle="none" BackColor="Transparent" OnClick="btnColorCode_Click"
                                                        Style="padding-left: 10px; padding-top: 3px; background-repeat: no-repeat; height: 49px;
                                                        background-position: 5px 15px; font: bold 14px Trebuchet MS; color: #000000;" />
                                                </asp:Panel>
                                            </td>
                                            <td>
                                                <asp:Panel ID="pnlPanelty" runat="server" CssClass="a" Width="100%">
                                                    <asp:Button ID="Button15" runat="server" Text="<%$ Resources:Attendance,Penalty  %>"
                                                        Width="70px" BorderStyle="none" BackColor="Transparent" OnClick="btnPanelty_Click"
                                                        Style="padding-left: 10px; padding-top: 3px; background-repeat: no-repeat; height: 49px;
                                                        background-position: 5px 15px; font: bold 14px Trebuchet MS; color: #000000;" />
                                                </asp:Panel>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td bgcolor="#ccddee" colspan="4" width="100%" height="500px" valign="top">
                                    <asp:Panel ID="PnlTime" runat="server">
                                        <table width="100%" style="padding-left: 43px">
                                            <tr>
                                                <td>
                                                    <asp:HiddenField ID="hdnCompanyId" runat="server" />
                                                    <br />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td width="160px" align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                                    <asp:Label ID="Label52" runat="server" CssClass="labelComman" Text="<%$ Resources:Attendance,Employee Sychronization %>"></asp:Label>
                                                </td>
                                                <td width="1px">
                                                    :
                                                </td>
                                                <td align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                                    <asp:DropDownList ID="ddlEmpSync" Width="162px" runat="server" CssClass="DropdownSearch">
                                                        <asp:ListItem Text="Location Level" Value="Location"></asp:ListItem>
                                                        <asp:ListItem Text="Company Level" Value="Company"></asp:ListItem>
                                                    </asp:DropDownList>
                                                </td>
                                                <td width="160px" align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                                    <asp:Label ID="Label53" runat="server" CssClass="labelComman" Text="<%$ Resources:Attendance,Financial Year Month %>"></asp:Label>
                                                </td>
                                                <td width="1px">
                                                    :
                                                </td>
                                                <td align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                                    <asp:DropDownList ID="ddlFinancialYear" Width="162px" runat="server" CssClass="DropdownSearch">
                                                        <asp:ListItem Text="January" Value="1"></asp:ListItem>
                                                        <asp:ListItem Text="February" Value="2"></asp:ListItem>
                                                        <asp:ListItem Text="March" Value="3"></asp:ListItem>
                                                        <asp:ListItem Text="April" Value="4"></asp:ListItem>
                                                        <asp:ListItem Text="May" Value="5"></asp:ListItem>
                                                        <asp:ListItem Text="June" Value="6"></asp:ListItem>
                                                        <asp:ListItem Text="July" Value="7"></asp:ListItem>
                                                        <asp:ListItem Text="August" Value="8"></asp:ListItem>
                                                        <asp:ListItem Text="September" Value="9"></asp:ListItem>
                                                        <asp:ListItem Text="October" Value="10"></asp:ListItem>
                                                        <asp:ListItem Text="November" Value="11"></asp:ListItem>
                                                        <asp:ListItem Text="December" Value="12"></asp:ListItem>
                                                    </asp:DropDownList>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td width="160px" align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                                    <asp:Label ID="lblCompanyName" runat="server" CssClass="labelComman" Text="<%$ Resources:Attendance,Shortest Time %>"></asp:Label>
                                                </td>
                                                <td width="1px">
                                                    :
                                                </td>
                                                <td align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                                    <asp:TextBox ID="txtShortestTime" Width="150px" runat="server" CssClass="textComman" />
                                                    <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender5" runat="server" Enabled="True"
                                                        TargetControlID="txtShortestTime" ValidChars="1,2,3,4,5,6,7,8,9,0">
                                                    </cc1:FilteredTextBoxExtender>
                                                </td>
                                                <td width="160px" align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                                    <asp:Label ID="Label1" runat="server" CssClass="labelComman" Text="<%$ Resources:Attendance,Work Day Minutes %>"></asp:Label>
                                                </td>
                                                <td width="1px">
                                                    :
                                                </td>
                                                <td align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                                    <asp:TextBox ID="txtWorkDayMin" Width="150px" runat="server" CssClass="textComman" />
                                                    <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" Enabled="True"
                                                        TargetControlID="txtWorkDayMin" ValidChars="1,2,3,4,5,6,7,8,9,0">
                                                    </cc1:FilteredTextBoxExtender>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td width="250px" align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                                    <asp:Label ID="Label2" runat="server" CssClass="labelComman" Text="<%$ Resources:Attendance,Difference Between Time Table In Shift %>"></asp:Label>
                                                </td>
                                                <td width="1px">
                                                    :
                                                </td>
                                                <td align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                                    <asp:TextBox ID="txtMinDiffTime" Width="150px" runat="server" CssClass="textComman" />
                                                    <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server" Enabled="True"
                                                        TargetControlID="txtMinDiffTime" ValidChars="1,2,3,4,5,6,7,8,9,0">
                                                    </cc1:FilteredTextBoxExtender>
                                                </td>
                                                <td align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                                    <asp:Label ID="Label3" runat="server" CssClass="labelComman" Text="<%$ Resources:Attendance,Week Off Days %>"></asp:Label>
                                                </td>
                                                <td width="1px">
                                                    :
                                                </td>
                                                <td rowspan="5"  valign="bottom" align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                                    <table height="100px" width="205px" style="border-style: solid; border-width: 1px;
                                                        border-color: #ABADB3; background-color: White">
                                                        <tr>
                                                            <td>
                                                                <asp:CheckBoxList ID="ChkWeekOffList" runat="server" RepeatColumns="2" AutoPostBack="True"
                                                                    Font-Names="Trebuchet MS" Font-Size="Small" ForeColor="Gray" OnSelectedIndexChanged="ChkWeekOffList_OnSelectedIndexChanged">
                                                                    <asp:ListItem Text="Sunday" Value="Sunday"></asp:ListItem>
                                                                    <asp:ListItem Text="Monday" Value="Monday"></asp:ListItem>
                                                                    <asp:ListItem Text="Tuesday" Value="Tuesday"></asp:ListItem>
                                                                    <asp:ListItem Text="Wednesday" Value="Wednesday"></asp:ListItem>
                                                                    <asp:ListItem Text="Thursday" Value="Thursday"></asp:ListItem>
                                                                    <asp:ListItem Text="Friday" Value="Friday"></asp:ListItem>
                                                                    <asp:ListItem Text="Saturday" Value="Saturday"></asp:ListItem>
                                                                </asp:CheckBoxList>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                            <tr id="trAllBrand" visible="false" runat="server">
                                                <td align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                                    <asp:Label ID="Label4" runat="server" CssClass="labelComman" Text="<%$ Resources:Attendance,Display Time Table In All Brand %>"></asp:Label>
                                                </td>
                                                <td width="1px">
                                                    :
                                                </td>
                                                <td align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                                    <asp:RadioButton ID="rbtnBrandYes" GroupName="Brand" Text="<%$ Resources:Attendance,Yes %>"
                                                        runat="server" CssClass="labelComman" />
                                                    &nbsp;&nbsp;
                                                    <asp:RadioButton ID="rbtnBrandNo" GroupName="Brand" Text="<%$ Resources:Attendance,No %>"
                                                        runat="server" CssClass="labelComman" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                                    <asp:Label ID="Label5" runat="server" CssClass="labelComman" Text="<%$ Resources:Attendance,Exclude Day As %>"></asp:Label>
                                                </td>
                                                <td width="1px">
                                                    :
                                                </td>
                                                <td align='<%= Common.ChangeTDForDefaultLeft()%>' style="padding-top: 5px">
                                                    <asp:DropDownList ID="ddlExculeDay" Width="162px" runat="server" CssClass="DropdownSearch">
                                                        <asp:ListItem Text="Absent" Value="Absent"></asp:ListItem>
                                                        <asp:ListItem Text="IsOff" Value="IsOff"></asp:ListItem>
                                                    </asp:DropDownList>
                                                </td>
                                            </tr>
                                            
                                             <tr>
                                                <td align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                                    <asp:Label ID="Label74" runat="server" CssClass="labelComman" Text="<%$ Resources:Attendance,Default Shift %>"></asp:Label>
                                                </td>
                                                <td width="1px">
                                                    :
                                                </td>
                                                <td align='<%= Common.ChangeTDForDefaultLeft()%>' style="padding-top: 5px">
                                                    <asp:DropDownList ID="ddlDefaultShift" Width="162px" runat="server" CssClass="DropdownSearch">
                                                        
                                                    </asp:DropDownList>
                                                </td>
                                            </tr>
                                            
                                            <tr>
                                                <td align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                                    <asp:Label ID="Label83" runat="server" CssClass="labelComman" Text="<%$ Resources:Attendance,Service Run Time %>"></asp:Label>
                                                </td>
                                                <td width="1px">
                                                    :
                                                </td>
                                                <td align='<%= Common.ChangeTDForDefaultLeft()%>' style="padding-top: 5px">
                                                    <asp:TextBox ID="txtServiceRunTime" Width="150px" runat="server" CssClass="textComman" />
                                                  <cc1:MaskedEditExtender ID="MaskedEditExtender6" runat="server" CultureAMPMPlaceholder=""
                                                        CultureCurrencySymbolPlaceholder="" CultureDateFormat="" CultureDatePlaceholder=""
                                                        CultureDecimalPlaceholder="" CultureThousandsPlaceholder="" CultureTimePlaceholder=""
                                                        Enabled="True" Mask="99:99" MaskType="Time" TargetControlID="txtServiceRunTime">
                                                    </cc1:MaskedEditExtender>
                                                </td>
                                            </tr>
                                            
                                            
                                            <tr>
                                                <td>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                </td>
                                                <td>
                                                </td>
                                                <td align='<%= Common.ChangeTDForDefaultLeft()%>' style="padding-left: 12PX;">
                                                    <asp:Button ID="btnSaveTime" runat="server" CssClass="buttonCommman" OnClick="btnSaveTime_Click"
                                                        TabIndex="8" Text="<%$ Resources:Attendance,Save %>" Width="75px" />
                                                    &nbsp; &nbsp;
                                                    <asp:Button ID="btnCancelTime" runat="server" CausesValidation="False" CssClass="buttonCommman"
                                                        OnClick="btnCancelTime_Click" TabIndex="10" Text="<%$ Resources:Attendance,Close %>"
                                                        Width="75px" />
                                                </td>
                                            </tr>
                                        </table>
                                    </asp:Panel>
                                    <asp:Panel ID="PnlOT" runat="server">
                                        <table width="100%" style="padding-left: 43px">
                                          
                                           
                                            <tr>
                                                <td colspan="2">
                                                </td>
                                                <td align='<%= Common.ChangeTDForDefaultLeft()%>' style="padding-left: 12PX;">
                                                    <asp:Button ID="Button1" runat="server" CssClass="buttonCommman" OnClick="btnSaveOverTime_Click"
                                                        TabIndex="8" Text="<%$ Resources:Attendance,Save %>" Width="75px" />
                                                    &nbsp; &nbsp;
                                                    <asp:Button ID="Button2" runat="server" CausesValidation="False" CssClass="buttonCommman"
                                                        OnClick="btnCancelOverTime_Click" TabIndex="10" Text="<%$ Resources:Attendance,Close %>"
                                                        Width="75px" />
                                                </td>
                                            </tr>
                                        </table>
                                    </asp:Panel>
                                    <asp:Panel ID="pnlWork" runat="server">
                                        <table width="100%" style="padding-left: 43px">
                                            <tr>
                                                <td>
                                                    <br />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                                    <asp:Label ID="Label10" runat="server" CssClass="labelComman" Text="<%$ Resources:Attendance,Work Calculation Method %>"></asp:Label>
                                                </td>
                                                <td width="1px">
                                                    :
                                                </td>
                                                <td align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                                    <asp:DropDownList ID="ddlWorkCal" Width="155px" runat="server" CssClass="DropdownSearch">
                                                        <asp:ListItem Text="PairWise" Value="PairWise"></asp:ListItem>
                                                        <asp:ListItem Text="InOut" Value="InOut"></asp:ListItem>
                                                    </asp:DropDownList>
                                                </td>
                                                <td align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                                    <asp:Label ID="Label11" runat="server" CssClass="labelComman" Text="<%$ Resources:Attendance,Pay Salary According To %>"></asp:Label>
                                                </td>
                                                <td width="1px">
                                                    :
                                                </td>
                                                <td width="300px" align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                                    <asp:DropDownList ID="ddlPaySal" Width="155px" runat="server" CssClass="DropdownSearch"
                                                        AutoPostBack="true" OnSelectedIndexChanged="ddlPaySal_OnSelectedIndexChanged">
                                                        <asp:ListItem Text="Work Hour" Value="Work Hour"></asp:ListItem>
                                                        <asp:ListItem Text="Ref Hour" Value="Ref Hour"></asp:ListItem>
                                                    </asp:DropDownList>
                                                </td>
                                            </tr>
                                            <tr>
                                                <tr>
                                                    <td align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                                        <asp:Label ID="Label12" runat="server" CssClass="labelComman" Text="<%$ Resources:Attendance,Without Shift Preference %>"></asp:Label>
                                                    </td>
                                                    <td width="1px">
                                                        :
                                                    </td>
                                                    <td align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                                        <asp:DropDownList ID="ddlShiftPref" Width="155px" runat="server" CssClass="DropdownSearch">
                                                            <asp:ListItem Text="Log Sequence" Value="Log Sequence"></asp:ListItem>
                                                            <asp:ListItem Text="Function Key" Value="Function Key"></asp:ListItem>
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td>
                                                    </td>
                                                    <td>
                                                    </td>
                                                    <td align="left" rowspan="4" valign="bottom">
                                                        <asp:Panel ID="pnlWorkPercent" runat="server" Height="130px" Width="280px" BorderStyle="Solid"
                                                            BorderWidth="1px" BorderColor="#ABADB3" BackColor="White" ScrollBars="Auto">
                                                            <table>
                                                                <tr>
                                                                    <td align="<%= Common.ChangeTDForDefaultLeft()%>">
                                                                        <asp:Label ID="lblWorkpercent1" CssClass="labelComman" runat="server" Text="<%$ Resources:Attendance,From %>"></asp:Label>
                                                                    </td>
                                                                    <td>
                                                                    </td>
                                                                    <td align="<%= Common.ChangeTDForDefaultLeft()%>">
                                                                        <asp:TextBox ID="txtWorkPercentFrom1" runat="server" Width="30px" Text="0" CssClass="textComman"
                                                                            Enabled="False"></asp:TextBox>
                                                                    </td>
                                                                    <td>
                                                                        % &nbsp;
                                                                        <asp:Label ID="Label14" CssClass="labelComman" runat="server" Text="To"></asp:Label>
                                                                    </td>
                                                                    <td align="<%= Common.ChangeTDForDefaultLeft()%>">
                                                                        <asp:TextBox ID="txtWorkPercentTo1" AutoPostBack="True" CssClass="textComman" OnTextChanged="txtWorkPercentTo1_TextChanged"
                                                                            runat="server" Width="30px"></asp:TextBox>
                                                                        <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender7" runat="server" Enabled="True"
                                                                            TargetControlID="txtWorkPercentTo1" ValidChars="1,2,3,4,5,6,7,8,9,0">
                                                                        </cc1:FilteredTextBoxExtender>
                                                                    </td>
                                                                    <td>
                                                                        % &nbsp;
                                                                        <asp:Label ID="Label15" CssClass="labelComman" runat="server" Text="="></asp:Label>
                                                                    </td>
                                                                    <td align="<%= Common.ChangeTDForDefaultLeft()%>">
                                                                        <asp:TextBox ID="txtValue1" runat="server" Width="30px" CssClass="textComman"></asp:TextBox>
                                                                        <cc1:FilteredTextBoxExtender ID="ftbtxtValue1" runat="server" Enabled="True" TargetControlID="txtValue1"
                                                                            ValidChars="1,2,3,4,5,6,7,8,9,0">
                                                                        </cc1:FilteredTextBoxExtender>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="<%= Common.ChangeTDForDefaultLeft()%>">
                                                                        <asp:Label ID="Label16" CssClass="labelComman" runat="server" Text="<%$ Resources:Attendance,From %>"></asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        <b>:</b>
                                                                    </td>
                                                                    <td align="<%= Common.ChangeTDForDefaultLeft()%>">
                                                                        <asp:TextBox ID="txtWorkPercentFrom2" Enabled="False" CssClass="textComman" runat="server"
                                                                            Width="30px"></asp:TextBox>
                                                                    </td>
                                                                    <td>
                                                                        % &nbsp;
                                                                        <asp:Label ID="Label17" CssClass="labelComman" runat="server" Text=" To"></asp:Label>
                                                                    </td>
                                                                    <td align="<%= Common.ChangeTDForDefaultLeft()%>">
                                                                        <asp:TextBox ID="txtWorkPercentTo2" AutoPostBack="True" CssClass="textComman" OnTextChanged="txtWorkPercentTo2_TextChanged"
                                                                            runat="server" Width="30px"></asp:TextBox>
                                                                        <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender8" runat="server" Enabled="True"
                                                                            TargetControlID="txtWorkPercentTo2" ValidChars="1,2,3,4,5,6,7,8,9,0">
                                                                        </cc1:FilteredTextBoxExtender>
                                                                    </td>
                                                                    <td>
                                                                        % &nbsp;
                                                                        <asp:Label ID="Label18" CssClass="labelComman" runat="server" Text="="></asp:Label>
                                                                    </td>
                                                                    <td align="<%= Common.ChangeTDForDefaultLeft()%>">
                                                                        <asp:TextBox ID="txtValue2" runat="server" Width="30px" CssClass="textComman"></asp:TextBox>
                                                                        <cc1:FilteredTextBoxExtender ID="ftbtxtValue2" runat="server" Enabled="True" TargetControlID="txtValue2"
                                                                            ValidChars="1,2,3,4,5,6,7,8,9,0">
                                                                        </cc1:FilteredTextBoxExtender>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="<%= Common.ChangeTDForDefaultLeft()%>">
                                                                        <asp:Label ID="lblWorkper1" CssClass="labelComman" runat="server" Text="<%$ Resources:Attendance,From %>"></asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        <b>:</b>
                                                                    </td>
                                                                    <td align="<%= Common.ChangeTDForDefaultLeft()%>">
                                                                        <asp:TextBox ID="txtWorkPercentFrom3" CssClass="textComman" Enabled="False" runat="server"
                                                                            Width="30px"></asp:TextBox>
                                                                    </td>
                                                                    <td>
                                                                        % &nbsp;
                                                                        <asp:Label ID="Label19" runat="server" CssClass="labelComman" Text="To"></asp:Label>
                                                                    </td>
                                                                    <td align="<%= Common.ChangeTDForDefaultLeft()%>">
                                                                        <asp:TextBox ID="txtWorkPercentTo3" CssClass="textComman" runat="server" Width="30px"
                                                                            Enabled="False" Text="100"></asp:TextBox>
                                                                    </td>
                                                                    <td>
                                                                        % &nbsp;
                                                                        <asp:Label ID="Label20" runat="server" CssClass="labelComman" Text="="></asp:Label>
                                                                    </td>
                                                                    <td align="<%= Common.ChangeTDForDefaultLeft()%>">
                                                                        <asp:TextBox ID="txtValue3" runat="server" Width="30px" CssClass="textComman"></asp:TextBox>
                                                                        <cc1:FilteredTextBoxExtender ID="ftbTxtValue3" runat="server" Enabled="True" TargetControlID="txtValue3"
                                                                            ValidChars="1,2,3,4,5,6,7,8,9,0">
                                                                        </cc1:FilteredTextBoxExtender>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </asp:Panel>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                                        <asp:Label ID="Label13" runat="server" CssClass="labelComman" Text="<%$ Resources:Attendance,Salary Calculate According To %>"></asp:Label>
                                                    </td>
                                                    <td width="1px">
                                                        :
                                                    </td>
                                                    <td width="300px" align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                                        <asp:DropDownList ID="ddlSalCal" Width="155px" AutoPostBack="true" OnSelectedIndexChanged="ddlSalCal_OnSelectedIndexChanged"
                                                            runat="server" CssClass="DropdownSearch">
                                                            <asp:ListItem Text="Monthly" Value="Monthly"></asp:ListItem>
                                                            <asp:ListItem Text="Fixed Days" Value="Fixed Days"></asp:ListItem>
                                                        </asp:DropDownList>
                                                        &nbsp;&nbsp;
                                                         <asp:TextBox ID="txtFixedDays" Width="50px" Height="12px" runat="server" CssClass="textComman" />
                                                        <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender6" runat="server" Enabled="True"
                                                            TargetControlID="txtFixedDays" ValidChars="1,2,3,4,5,6,7,8,9,0">
                                                        </cc1:FilteredTextBoxExtender>
                                                        <asp:Label ID="lblDay" Text="Days" runat="server" CssClass="labelComman" />
                                                    </td>
                                                   
                                                   
                                                   
                                                    
                                                    
                                                </tr>
                                                
                                                
                                                <tr>
                                                    <td align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                                            <asp:Label ID="Label79" runat="server" CssClass="labelComman" Text="<%$ Resources:Attendance,Employee PF %>"></asp:Label>
                                              
                                                    </td>
                                                    <td width="1px">
                                                    :
                                                    
                                                    </td>
                                                    <td align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                                    
                                                     <asp:TextBox ID="txtEmpPF" Height="12px" Width="143px" runat="server" CssClass="textComman" />
                                                    <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender19" runat="server" Enabled="True"
                                                        TargetControlID="txtEmpPF" ValidChars="1,2,3,4,5,6,7,8,9,0,.">
                                                    </cc1:FilteredTextBoxExtender>
                                                    </td>
                                                </tr>
                                                
                                                   <tr>
                                                    <td align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                                            <asp:Label ID="Label80" runat="server" CssClass="labelComman" Text="<%$ Resources:Attendance,Employer PF %>"></asp:Label>
                                              
                                                    </td>
                                                    <td width="1px">
                                                    :
                                                    
                                                    </td>
                                                    <td align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                                    
                                                     <asp:TextBox ID="txtEmployerPf" Height="12px" Width="143px" runat="server" CssClass="textComman" />
                                                    <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender20" runat="server" Enabled="True"
                                                        TargetControlID="txtEmployerPf" ValidChars="1,2,3,4,5,6,7,8,9,0,.">
                                                    </cc1:FilteredTextBoxExtender>
                                                    </td>
                                                </tr>
                                                
                                                <tr>
                                                    <td align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                                            <asp:Label ID="Label82" runat="server" CssClass="labelComman" Text="<%$ Resources:Attendance,Employee ESIC %>"></asp:Label>
                                              
                                                    </td>
                                                    <td width="1px">
                                                    :
                                                    
                                                    </td>
                                                    <td align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                                    
                                                     <asp:TextBox ID="txtEmpESIC" Height="12px" Width="143px" runat="server" CssClass="textComman" />
                                                    <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender22" runat="server" Enabled="True"
                                                        TargetControlID="txtEmpESIC" ValidChars="1,2,3,4,5,6,7,8,9,0,.">
                                                    </cc1:FilteredTextBoxExtender>
                                                    </td>
                                                </tr>
                                                
                                               
                                                   <tr>
                                                    <td align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                                            <asp:Label ID="Label81" runat="server" CssClass="labelComman" Text="<%$ Resources:Attendance,Employer ESIC %>"></asp:Label>
                                              
                                                    </td>
                                                    <td width="1px">
                                                    :
                                                    
                                                    </td>
                                                    <td align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                                    
                                                     <asp:TextBox ID="txtEmployerESIC" Height="12px" Width="143px" runat="server" CssClass="textComman" />
                                                    <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender21" runat="server" Enabled="True"
                                                        TargetControlID="txtEmployerESIC" ValidChars="1,2,3,4,5,6,7,8,9,0,.">
                                                    </cc1:FilteredTextBoxExtender>
                                                    </td>
                                                </tr>
                                                
                                                
                                                
                                              
                                                
                                                <tr>
                                                    <td>
                                                    </td>
                                                    <td>
                                                    </td>
                                                    <td align='<%= Common.ChangeTDForDefaultLeft()%>' style="padding-left: 12PX;">
                                                        <asp:Button ID="Button3" runat="server" CssClass="buttonCommman" OnClick="btnSaveWork_Click"
                                                            TabIndex="8" Text="<%$ Resources:Attendance,Save %>" Width="75px" />
                                                        &nbsp; &nbsp;
                                                        <asp:Button ID="Button4" runat="server" CausesValidation="False" CssClass="buttonCommman"
                                                            OnClick="btnCancelWork_Click" TabIndex="10" Text="<%$ Resources:Attendance,Close %>"
                                                            Width="75px" />
                                                    </td>
                                                </tr>
                                    </asp:Panel>
                                    <asp:Panel ID="pnlSMSEmail" runat="server">
                                        <table width="100%" style="padding-left: 43px">
                                            <tr>
                                                <td style="padding-top: 8PX;">
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                                    <fieldset style="height: 230px">
                                                        <legend>
                                                            <asp:Label ID="Label21" Font-Bold="true" Font-Size="13px" Font-Names="Arial" runat="server"
                                                                Text="<%$ Resources:Attendance,SMS Setup %>"></asp:Label></legend>
                                                        <table width="100%">
                                                            <tr>
                                                                <td align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                                                    <asp:Label ID="Label23" runat="server" CssClass="labelComman" Text="<%$ Resources:Attendance,SMS Functionality %>"></asp:Label>
                                                                </td>
                                                                <td width="1px">
                                                                    :
                                                                </td>
                                                                <td align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                                                    <asp:RadioButton ID="rbtnSMSEnable" GroupName="SMS" Text="<%$ Resources:Attendance,Enable %>"
                                                                        runat="server" CssClass="labelComman" AutoPostBack="true" OnCheckedChanged="rbtSMS_OnCheckedChanged" />
                                                                    &nbsp;&nbsp;
                                                                    <asp:RadioButton ID="rbtnSMSDisable" GroupName="SMS" Text="<%$ Resources:Attendance,Disable %>"
                                                                        runat="server" CssClass="labelComman" AutoPostBack="true" OnCheckedChanged="rbtSMS_OnCheckedChanged" />
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                                                    <asp:Label ID="Label24" runat="server" CssClass="labelComman" Text="<%$ Resources:Attendance,SMS_API %>"></asp:Label>
                                                                </td>
                                                                <td width="1px">
                                                                    :
                                                                </td>
                                                                <td align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                                                    <asp:TextBox ID="txtSMSAPI" Width="150px" runat="server" CssClass="textComman" />
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                                                    <asp:Label ID="Label25" runat="server" CssClass="labelComman" Text="<%$ Resources:Attendance,Sender_Id %>"></asp:Label>
                                                                </td>
                                                                <td width="1px">
                                                                    :
                                                                </td>
                                                                <td align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                                                    <asp:TextBox ID="txtSenderId" Width="150px" runat="server" CssClass="textComman" />
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                                                    <asp:Label ID="Label26" runat="server" CssClass="labelComman" Text="<%$ Resources:Attendance,SMS_User_Id %>"></asp:Label>
                                                                </td>
                                                                <td width="1px">
                                                                    :
                                                                </td>
                                                                <td align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                                                    <asp:TextBox ID="txtUserId" Width="150px" runat="server" CssClass="textComman" />
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                                                    <asp:Label ID="Label27" runat="server" CssClass="labelComman" Text="<%$ Resources:Attendance,SMS_User_Password %>"></asp:Label>
                                                                </td>
                                                                <td width="1px">
                                                                    :
                                                                </td>
                                                                <td align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                                                    <asp:TextBox ID="txtSmsPassword" Width="150px" TextMode="Password" runat="server"
                                                                        CssClass="textComman" />
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </fieldset>
                                                </td>
                                                <td align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                                    <fieldset style="height: 230px">
                                                        <legend>
                                                            <asp:Label ID="Label22" Font-Bold="true" Font-Size="13px" Font-Names="Arial" runat="server"
                                                                Text="<%$ Resources:Attendance,Email Setup %>"></asp:Label></legend>
                                                        <table width="100%">
                                                            <tr>
                                                                <td align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                                                    <asp:Label ID="Label28" runat="server" CssClass="labelComman" Text="<%$ Resources:Attendance,Email Functionality %>"></asp:Label>
                                                                </td>
                                                                <td width="1px">
                                                                    :
                                                                </td>
                                                                <td align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                                                    <asp:RadioButton ID="rbtnEmailEnable" GroupName="Email" Text="<%$ Resources:Attendance,Enable %>"
                                                                        runat="server" CssClass="labelComman" AutoPostBack="true" OnCheckedChanged="rbtEmail_OnCheckedChanged" />
                                                                    &nbsp;&nbsp;
                                                                    <asp:RadioButton ID="rbtnEmailDisable" GroupName="Email" Text="<%$ Resources:Attendance,Disable %>"
                                                                        runat="server" CssClass="labelComman" AutoPostBack="true" OnCheckedChanged="rbtEmail_OnCheckedChanged" />
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                                                    <asp:Label ID="Label29" runat="server" CssClass="labelComman" Text="<%$ Resources:Attendance,Email %>"></asp:Label>
                                                                </td>
                                                                <td width="1px">
                                                                    :
                                                                </td>
                                                                <td align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                                                    <asp:TextBox ID="txtEmail" Width="150px" runat="server" CssClass="textComman" />
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                                                    <asp:Label ID="Label30" runat="server" CssClass="labelComman" Text="<%$ Resources:Attendance,Password %>"></asp:Label>
                                                                </td>
                                                                <td width="1px">
                                                                    :
                                                                </td>
                                                                <td align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                                                    <asp:TextBox ID="txtPasswordEmail" Width="150px" TextMode="Password" runat="server"
                                                                        CssClass="textComman" />
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                                                    <asp:Label ID="Label31" runat="server" CssClass="labelComman" Text="<%$ Resources:Attendance,SMTP %>"></asp:Label>
                                                                </td>
                                                                <td width="1px">
                                                                    :
                                                                </td>
                                                                <td align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                                                    <asp:TextBox ID="txtSMTP" Width="150px" runat="server" CssClass="textComman" />
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                                                    <asp:Label ID="Label32" runat="server" CssClass="labelComman" Text="<%$ Resources:Attendance,Port %>"></asp:Label>
                                                                </td>
                                                                <td width="1px">
                                                                    :
                                                                </td>
                                                                <td align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                                                    <asp:TextBox ID="txtPort" Width="150px" runat="server" CssClass="textComman" />
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                                                    <asp:Label ID="Label33" runat="server" CssClass="labelComman" Text="<%$ Resources:Attendance,EnableSSL %>"></asp:Label>
                                                                </td>
                                                                <td width="1px">
                                                                    :
                                                                </td>
                                                                <td align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                                                    <asp:CheckBox ID="chkEnableSSL" CssClass="labelComman" runat="server" />
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </fieldset>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="2" align="center">
                                                    <asp:Button ID="Button5" runat="server" CssClass="buttonCommman" OnClick="btnSaveSMSEmail_Click"
                                                        TabIndex="8" Text="<%$ Resources:Attendance,Save %>" Width="75px" />
                                                    &nbsp; &nbsp;
                                                    <asp:Button ID="Button6" runat="server" CausesValidation="False" CssClass="buttonCommman"
                                                        OnClick="btnCancelSMSEmail_Click" TabIndex="10" Text="<%$ Resources:Attendance,Close %>"
                                                        Width="75px" />
                                                </td>
                                            </tr>
                                        </table>
                                    </asp:Panel>
                                    <asp:Panel ID="pnlPartialLeave1" runat="server">
                                        <table width="100%" style="padding-left: 43px">
                                            <tr>
                                                <td>
                                                    <br />
                                                </td>
                                            </tr>
                                            <tr>
                                            <td align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                           
                                             <fieldset style="height: 200px">
                                                        <legend>
                                                            <asp:Label ID="Label70" Font-Bold="true" Font-Size="13px" Font-Names="Arial" runat="server"
                                                                Text="<%$ Resources:Attendance,Partial Leave %>"></asp:Label></legend>
                       
                                            <table>
                                            
                                              <tr>
                                                <td width="230px" align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                                    <asp:Label ID="Label34" runat="server" CssClass="labelComman" Text="<%$ Resources:Attendance,Partial Leave Functionality %>"></asp:Label>
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
                                                    <asp:Label ID="Label35" runat="server" CssClass="labelComman" Text="<%$ Resources:Attendance,Total Minutes %>"></asp:Label>
                                                </td>
                                                <td width="1px">
                                                    :
                                                </td>
                                                <td align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                                    <asp:TextBox ID="txtTotalMinutes" Width="123px" runat="server" CssClass="textComman" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                                    <asp:Label ID="Label36" runat="server" CssClass="labelComman" Text="<%$ Resources:Attendance,Minute Use in a Day %>"></asp:Label>
                                                </td>
                                                <td width="1px">
                                                    :
                                                </td>
                                                <td align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                                    <asp:TextBox ID="txtMinuteday" Width="123px" runat="server" CssClass="textComman" />
                                                </td>
                                            </tr>
                                            <tr runat="server" visible="false" >
                                                <td align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                                    <asp:Label ID="Label37" runat="server" CssClass="labelComman" Text="<%$ Resources:Attendance,Carry Forward Minutes %>"></asp:Label>
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
                                            
                                             <td align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                            
                                              <fieldset style="height: 200px">
                                                        <legend>
                                                            <asp:Label ID="Label71" Font-Bold="true" Font-Size="13px" Font-Names="Arial" runat="server"
                                                                Text="<%$ Resources:Attendance,Over Time %>"></asp:Label></legend>
                       
                                            
                                            
                                           
                                          
                                            
                                            
                                            
                                            <table>
                                             <tr>
                                                <td width="230px" align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                                    <asp:Label ID="Label6" runat="server" CssClass="labelComman" Text="<%$ Resources:Attendance,Over Time Functionality %>"></asp:Label>
                                                </td>
                                                <td width="1px">
                                                    :
                                                </td>
                                                <td align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                                    <asp:RadioButton ID="rbtnOtEnable" GroupName="OT" Text="<%$ Resources:Attendance,Enable %>"
                                                        runat="server" CssClass="labelComman" AutoPostBack="true" OnCheckedChanged="rbtOT_OnCheckedChanged" />
                                                    &nbsp;&nbsp;
                                                    <asp:RadioButton ID="rbtnOtDisable" GroupName="OT" Text="<%$ Resources:Attendance,Disable %>"
                                                        runat="server" CssClass="labelComman" AutoPostBack="true" OnCheckedChanged="rbtOT_OnCheckedChanged" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td width="160px" align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                                    <asp:Label ID="Label7" runat="server" CssClass="labelComman" Text="<%$ Resources:Attendance,Max Over Time Minutes %>"></asp:Label>
                                                </td>
                                                <td width="1px">
                                                    :
                                                </td>
                                                <td align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                                    <asp:TextBox ID="txtMaxOTMint" Width="123px" runat="server" CssClass="textComman" />
                                                    <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender4" runat="server" Enabled="True"
                                                        TargetControlID="txtMaxOTMint" ValidChars="1,2,3,4,5,6,7,8,9,0">
                                                    </cc1:FilteredTextBoxExtender>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td width="160px" align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                                    <asp:Label ID="Label8" runat="server" CssClass="labelComman" Text="<%$ Resources:Attendance,Min Over Time Minutes %>"></asp:Label>
                                                </td>
                                                <td width="1px">
                                                    :
                                                </td>
                                                <td align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                                    <asp:TextBox ID="txtMinOTMint" Width="123px" runat="server" CssClass="textComman" />
                                                    <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender3" runat="server" Enabled="True"
                                                        TargetControlID="txtMinOTMint" ValidChars="1,2,3,4,5,6,7,8,9,0">
                                                    </cc1:FilteredTextBoxExtender>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td width="160px" align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                                    <asp:Label ID="Label9" runat="server" CssClass="labelComman" Text="<%$ Resources:Attendance,Calculation Method%>"></asp:Label>
                                                </td>
                                                <td width="1px">
                                                    :
                                                </td>
                                                <td align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                                    <asp:DropDownList ID="ddlCalculationMethod" Width="135px" runat="server" CssClass="DropdownSearch">
                                                        <asp:ListItem Text="In" Value="In"></asp:ListItem>
                                                        <asp:ListItem Text="Out" Value="Out"></asp:ListItem>
                                                        <asp:ListItem Text="Both" Value="Both"></asp:ListItem>
                                                        <asp:ListItem Text="Work Hour" Value="Work Hour"></asp:ListItem>
                                                    </asp:DropDownList>
                                                </td>
                                            </tr>
                                            
                                            </table>
                                            </fieldset>
                                             </td>
                                            </tr>
                                            
                                            
                                            <tr>
                                                
                                                <td colspan="2" align="center" >
                                                    <asp:Button ID="Button8" runat="server" CssClass="buttonCommman" OnClick="btnSavePartial_Click"
                                                        TabIndex="8" Text="<%$ Resources:Attendance,Save %>" Width="75px" />
                                                    &nbsp; &nbsp;
                                                    <asp:Button ID="Button9" runat="server" CausesValidation="False" CssClass="buttonCommman"
                                                        OnClick="btnCancelPartial_Click" TabIndex="10" Text="<%$ Resources:Attendance,Close %>"
                                                        Width="75px" />
                                                </td>
                                            </tr>
                                        </table>
                                    </asp:Panel>
                                    <asp:Panel ID="pnlKeyPreference" runat="server">
                                        <table width="100%" style="padding-left: 43px">
                                            <tr>
                                                <td>
                                                    <br />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td width="200px" align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                                    <asp:Label ID="Label41" runat="server" CssClass="labelComman" Text="<%$ Resources:Attendance,Key Preference Functionality %>"></asp:Label>
                                                </td>
                                                <td width="1px">
                                                    :
                                                </td>
                                                <td align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                                    <asp:RadioButton ID="rbtnKeyEnable" GroupName="KeyPref" Text="<%$ Resources:Attendance,Enable %>"
                                                        runat="server" CssClass="labelComman" AutoPostBack="true" OnCheckedChanged="rbtKeyPref_OnCheckedChanged" />
                                                    &nbsp;&nbsp;
                                                    <asp:RadioButton ID="rbtnKeyDisable" GroupName="KeyPref" Text="<%$ Resources:Attendance,Disable %>"
                                                        runat="server" CssClass="labelComman" AutoPostBack="true" OnCheckedChanged="rbtKeyPref_OnCheckedChanged" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                                    <asp:Label ID="Label39" runat="server" CssClass="labelComman" Text="<%$ Resources:Attendance,IN Key %>"></asp:Label>
                                                </td>
                                                <td width="1px">
                                                    :
                                                </td>
                                                <td align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                                    <asp:TextBox ID="txtInKey" Width="150px" runat="server" CssClass="textComman" />
                                                </td>
                                                <td width="160px" align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                                    <asp:Label ID="Label40" runat="server" CssClass="labelComman" Text="<%$ Resources:Attendance,Partial Leave IN Key %>"></asp:Label>
                                                </td>
                                                <td width="1px">
                                                    :
                                                </td>
                                                <td align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                                    <asp:TextBox ID="txtPartialInKey" Width="150px" runat="server" CssClass="textComman" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                                    <asp:Label ID="Label48" runat="server" CssClass="labelComman" Text="<%$ Resources:Attendance,Out Key %>"></asp:Label>
                                                </td>
                                                <td width="1px">
                                                    :
                                                </td>
                                                <td align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                                    <asp:TextBox ID="txtOutKey" Width="150px" runat="server" CssClass="textComman" />
                                                </td>
                                                <td width="160px" align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                                    <asp:Label ID="Label49" runat="server" CssClass="labelComman" Text="<%$ Resources:Attendance,Partial Leave Out Key %>"></asp:Label>
                                                </td>
                                                <td width="1px">
                                                    :
                                                </td>
                                                <td align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                                    <asp:TextBox ID="txtPartialOutKey" Width="150px" runat="server" CssClass="textComman" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <br />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                                    <asp:Label ID="Label42" runat="server" CssClass="labelComman" Text="<%$ Resources:Attendance,Break In Key %>"></asp:Label>
                                                </td>
                                                <td width="1px">
                                                    :
                                                </td>
                                                <td align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                                    <asp:TextBox ID="txtBreakInKey" Width="150px" runat="server" CssClass="textComman" />
                                                </td>
                                                <td runat="server" visible="false"  width="160px" align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                                    <asp:Label ID="Label43" runat="server" CssClass="labelComman" Text="<%$ Resources:Attendance,Consider Next Day Log %>"></asp:Label>
                                                </td>
                                                <td width="1px">
                                                   
                                                </td>
                                                <td runat="server" visible="false" align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                                    <asp:CheckBox ID="ChkNextDayLog" runat="server" CssClass="labelComman" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                                    <asp:Label ID="Label44" runat="server" CssClass="labelComman" Text="<%$ Resources:Attendance,Break Out Key %>"></asp:Label>
                                                </td>
                                                <td width="1px">
                                                    :
                                                </td>
                                                <td align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                                    <asp:TextBox ID="txtBreakOutKey" Width="150px" runat="server" CssClass="textComman" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                </td>
                                                <td>
                                                </td>
                                                <td align='<%= Common.ChangeTDForDefaultLeft()%>' style="padding-left: 12PX;">
                                                    <asp:Button ID="Button10" runat="server" CssClass="buttonCommman" OnClick="btnSaveKeyPreference_Click"
                                                        TabIndex="8" Text="<%$ Resources:Attendance,Save %>" Width="75px" />
                                                    &nbsp; &nbsp;
                                                    <asp:Button ID="Button11" runat="server" CausesValidation="False" CssClass="buttonCommman"
                                                        OnClick="btnCancelKeyPreference_Click" TabIndex="10" Text="<%$ Resources:Attendance,Close %>"
                                                        Width="75px" />
                                                </td>
                                            </tr>
                                        </table>
                                    </asp:Panel>
                                    <asp:Panel ID="pnlColor" runat="server">
                                        <table width="100%" style="padding-left: 43px">
                                            <tr>
                                                <td>
                                                    <br />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                                    <asp:Label ID="Label46" runat="server" CssClass="labelComman" Text="<%$ Resources:Attendance,Present Color Code %>"></asp:Label>
                                                </td>
                                                <td width="1px">
                                                    :
                                                </td>
                                                <td align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                                    <asp:TextBox ID="txtPresentColorCode" Width="150px" runat="server" CssClass="textComman" />
                                                    <cc1:ColorPickerExtender ID="txtPresentColorCode_ColorPickerExtender" runat="server"
                                                        Enabled="True" TargetControlID="txtPresentColorCode" SampleControlID="txtPresentColorCode">
                                                    </cc1:ColorPickerExtender>
                                                </td>
                                                <td width="160px" align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                                    <asp:Label ID="Label47" runat="server" CssClass="labelComman" Text="<%$ Resources:Attendance,Absent Color Code %>"></asp:Label>
                                                </td>
                                                <td width="1px">
                                                    :
                                                </td>
                                                <td align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                                    <asp:TextBox ID="txtAbsentColorCode" Width="150px" runat="server" CssClass="textComman" />
                                                    <cc1:ColorPickerExtender ID="ColorPickerExtender1" runat="server" Enabled="True"
                                                        TargetControlID="txtAbsentColorCode" SampleControlID="txtAbsentColorCode">
                                                    </cc1:ColorPickerExtender>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                                    <asp:Label ID="Label50" runat="server" CssClass="labelComman" Text="<%$ Resources:Attendance,Late Color Code %>"></asp:Label>
                                                </td>
                                                <td width="1px">
                                                    :
                                                </td>
                                                <td align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                                    <asp:TextBox ID="txtLateColorCode" Width="150px" runat="server" CssClass="textComman" />
                                                    <cc1:ColorPickerExtender ID="ColorPickerExtender2" runat="server" Enabled="True"
                                                        TargetControlID="txtLateColorCode" SampleControlID="txtLateColorCode">
                                                    </cc1:ColorPickerExtender>
                                                </td>
                                                <td width="160px" align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                                    <asp:Label ID="Label51" runat="server" CssClass="labelComman" Text="<%$ Resources:Attendance,Early Color Code %>"></asp:Label>
                                                </td>
                                                <td width="1px">
                                                    :
                                                </td>
                                                <td align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                                    <asp:TextBox ID="txtEarlyColorCode" Width="150px" runat="server" CssClass="textComman" />
                                                    <cc1:ColorPickerExtender ID="ColorPickerExtender3" runat="server" Enabled="True"
                                                        TargetControlID="txtEarlyColorCode" SampleControlID="txtEarlyColorCode">
                                                    </cc1:ColorPickerExtender>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                                    <asp:Label ID="Label54" runat="server" CssClass="labelComman" Text="<%$ Resources:Attendance,Leave Color Code %>"></asp:Label>
                                                </td>
                                                <td width="1px">
                                                    :
                                                </td>
                                                <td align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                                    <asp:TextBox ID="txtLeaveColorCode" Width="150px" runat="server" CssClass="textComman" />
                                                    <cc1:ColorPickerExtender ID="ColorPickerExtender4" runat="server" Enabled="True"
                                                        TargetControlID="txtLeaveColorCode" SampleControlID="txtLeaveColorCode">
                                                    </cc1:ColorPickerExtender>
                                                </td>
                                                <td align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                                    <asp:Label ID="Label45" runat="server" CssClass="labelComman" Text="<%$ Resources:Attendance,Holiday Color Code %>"></asp:Label>
                                                </td>
                                                <td width="1px">
                                                    :
                                                </td>
                                                <td align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                                    <asp:TextBox ID="txtHolidayColorCode" Width="150px" runat="server" CssClass="textComman" />
                                                    <cc1:ColorPickerExtender ID="ColorPickerExtender5" runat="server" Enabled="True"
                                                        TargetControlID="txtHolidayColorCode" SampleControlID="txtHolidayColorCode">
                                                    </cc1:ColorPickerExtender>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                </td>
                                                <td>
                                                </td>
                                                <td align='<%= Common.ChangeTDForDefaultLeft()%>' style="padding-left: 12PX;">
                                                    <asp:Button ID="Button13" runat="server" CssClass="buttonCommman" OnClick="btnSaveColorCode_Click"
                                                        TabIndex="8" Text="<%$ Resources:Attendance,Save %>" Width="75px" />
                                                    &nbsp; &nbsp;
                                                    <asp:Button ID="Button14" runat="server" CausesValidation="False" CssClass="buttonCommman"
                                                        OnClick="btnCancelColorCode_Click" TabIndex="10" Text="<%$ Resources:Attendance,Close %>"
                                                        Width="75px" />
                                                </td>
                                            </tr>
                                        </table>
                                    </asp:Panel>
                                    <asp:Panel ID="pnlPaneltyCalc" runat="server">
                                        <table width="100%" style="padding-left: 43px">
                                            <tr>
                                                <td>
                                                    <br />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                                    <fieldset style="height: 230px;width:450px">
                                                        <legend>
                                                            <asp:Label ID="Label55" Font-Bold="true" Font-Size="13px" Font-Names="Arial" runat="server"
                                                                Text="<%$ Resources:Attendance,Late In %>"></asp:Label></legend>
                                                        <table width="100%">
                                                            <tr>
                                                                <td width="230px" align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                                                    <asp:Label ID="Label60" runat="server" CssClass="labelComman" Text="<%$ Resources:Attendance,Late In Functionality %>"></asp:Label>
                                                                </td>
                                                                <td width="1px">
                                                                    :
                                                                </td>
                                                                <td align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                                                    <asp:RadioButton ID="rbtLateInEnable" GroupName="Late" Text="<%$ Resources:Attendance,Enable %>"
                                                                        runat="server" CssClass="labelComman" AutoPostBack="true" OnCheckedChanged="rbtLateIn_OnCheckedChanged" />
                                                                    &nbsp;&nbsp;
                                                                    <asp:RadioButton ID="rbtLateInDisable" GroupName="Late" Text="<%$ Resources:Attendance,Disable %>"
                                                                        runat="server" CssClass="labelComman" AutoPostBack="true" OnCheckedChanged="rbtLateIn_OnCheckedChanged" />
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td colspan="3" align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                                                    <asp:RadioButton ID="rbtnLateSalary" GroupName="LateType" Text="<%$ Resources:Attendance,Salary  %>"
                                                                        runat="server" CssClass="labelComman" AutoPostBack="true" OnCheckedChanged="rbtLateType_OnCheckedChanged" />
                                                                    &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp;
                                                                    <asp:RadioButton ID="rbtnLateMinutes" GroupName="LateType" Text="<%$ Resources:Attendance,Minutes %>"
                                                                        runat="server" CssClass="labelComman" AutoPostBack="true" OnCheckedChanged="rbtLateType_OnCheckedChanged" />
                                                                </td>
                                                                 </tr>
                                                                 <tr>
                                                                <td colspan="3" align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                                                    <asp:Panel ID="pnlLateSal" runat="server">
                                                                    <table>
                                                                    <tr>
                                                                    <td width="230px" align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                                                            <asp:Label ID="Label61" runat="server" CssClass="labelComman" Text="<%$ Resources:Attendance,Relaxation Minute %>"></asp:Label>
                                                           
                                                                    </td>
                                                                    <td width="1px">
                                                                    :
                                                                </td>
                                                                    <td  align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                                                     <asp:TextBox ID="txtLateRelaxMin" Width="70px" runat="server" CssClass="textComman" />
                                                                    <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender11" runat="server" Enabled="True"
                                                                        TargetControlID="txtLateRelaxMin" ValidChars="1,2,3,4,5,6,7,8,9,0,.">
                                                                    </cc1:FilteredTextBoxExtender>
                                                                    </td>
                                                                    </tr>
                                                                
                                                                    <tr>
                                                                    <td  align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                                                            <asp:Label ID="Label62" runat="server" CssClass="labelComman" Text="<%$ Resources:Attendance,Late Count %>"></asp:Label>
                                                           
                                                                    </td>
                                                                    <td width="1px">
                                                                    :
                                                                </td>
                                                                    <td  align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                                                     <asp:TextBox ID="txtLateCount" Width="70px" runat="server" CssClass="textComman" />
                                                                    <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender12" runat="server" Enabled="True"
                                                                        TargetControlID="txtLateCount" ValidChars="1,2,3,4,5,6,7,8,9,0,.">
                                                                    </cc1:FilteredTextBoxExtender>
                                                                    </td>
                                                                    </tr>
                                                                       <tr>
                                                                    <td  align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                                                              <asp:Label ID="Label63" runat="server" CssClass="labelComman" Text="<%$ Resources:Attendance,Value %>"></asp:Label>
                                                           
                                                                    </td>
                                                                    <td width="1px">
                                                                    :
                                                                </td>
                                                                    <td  align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                                                      <asp:TextBox ID="txtLateValue" Width="70px" runat="server" CssClass="textComman" />
                                                                    <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender9" runat="server" Enabled="True"
                                                                        TargetControlID="txtLateValue" ValidChars="1,2,3,4,5,6,7,8,9,0,.">
                                                                    </cc1:FilteredTextBoxExtender>
                                                                    <asp:DropDownList ID="ddlLateType" Width="70px" runat="server" CssClass="DropdownSearch">
                                                                        <asp:ListItem Text="Fixed" Value="1"></asp:ListItem>
                                                                        <asp:ListItem Text="%" Value="2"></asp:ListItem>
                                                                    </asp:DropDownList>
                                                                    </td>
                                                                    
                                                                    
                                                                    </tr>
                                                                    
                                                                    
                                                                    
                                                                    </table>
                                                                    
                                                                    </asp:Panel>
                                                                    <asp:Panel ID="pnlLateMin" runat="server">
                                                                    
                                                                    <table>
                                                                    <tr>
                                                                    <td width="230px" align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                                                            <asp:Label ID="Label64" runat="server" CssClass="labelComman" Text="<%$ Resources:Attendance,Minute Times %>"></asp:Label>
                                                           
                                                                    </td>
                                                                    <td width="1px">
                                                                    :
                                                                </td>
                                                                    <td  align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                                                     <asp:DropDownList ID="ddlLateMinTime" Width="82px" runat="server" CssClass="DropdownSearch">
                                                                        <asp:ListItem Text="2 Times" Value="2"></asp:ListItem>
                                                                        <asp:ListItem Text="3 Times" Value="3"></asp:ListItem>
                                                                         <asp:ListItem Text="4 Times" Value="4"></asp:ListItem>
                                                                    </asp:DropDownList>
                                                                    </td>
                                                                    </tr>
                                                                    <tr>
                                                                    <td width="230px" align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                                                            <asp:Label ID="Label75" runat="server" CssClass="labelComman" Text="<%$ Resources:Attendance,Relaxation Minute %>"></asp:Label>
                                                           
                                                                    </td>
                                                                    <td width="1px">
                                                                    :
                                                                </td>
                                                                    <td  align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                                                     <asp:TextBox ID="txtLateRelaxMinWithMTimes" Width="70px" runat="server" CssClass="textComman" />
                                                                    <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender16" runat="server" Enabled="True"
                                                                        TargetControlID="txtLateRelaxMinWithMTimes" ValidChars="1,2,3,4,5,6,7,8,9,0,.">
                                                                    </cc1:FilteredTextBoxExtender>
                                                                    </td>
                                                                    </tr>
                                                                    </table>
                                                                    </asp:Panel>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </fieldset>
                                                </td>
                                                <td align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                                    <fieldset style="height: 230px;width:450px">
                                                        <legend>
                                                            <asp:Label ID="Label56" Font-Bold="true" Font-Size="13px" Font-Names="Arial" runat="server"
                                                                Text="<%$ Resources:Attendance,Early Out %>"></asp:Label></legend>
                                                       <table width="100%">
                                                            <tr>
                                                                <td width="230px" align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                                                    <asp:Label ID="Label65" runat="server" CssClass="labelComman" Text="<%$ Resources:Attendance,Early Out Functionality %>"></asp:Label>
                                                                </td>
                                                                <td width="1px">
                                                                    :
                                                                </td>
                                                                <td align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                                                    <asp:RadioButton ID="rbtEarlyOutEnable" GroupName="Early" Text="<%$ Resources:Attendance,Enable %>"
                                                                        runat="server" CssClass="labelComman" AutoPostBack="true" OnCheckedChanged="rbtEarlyOut_OnCheckedChanged" />
                                                                    &nbsp;&nbsp;
                                                                    <asp:RadioButton ID="rbtEarlyOutDisable" GroupName="Early" Text="<%$ Resources:Attendance,Disable %>"
                                                                        runat="server" CssClass="labelComman" AutoPostBack="true" OnCheckedChanged="rbtEarlyOut_OnCheckedChanged" />
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td colspan="3" align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                                                    <asp:RadioButton ID="rbtnEarlySalary" GroupName="EarlyType" Text="<%$ Resources:Attendance,Salary  %>"
                                                                        runat="server" CssClass="labelComman" AutoPostBack="true" OnCheckedChanged="rbtEarlyType_OnCheckedChanged" />
                                                                    &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp;
                                                                    <asp:RadioButton ID="rbtnEarlyMinutes" GroupName="EarlyType" Text="<%$ Resources:Attendance,Minutes %>"
                                                                        runat="server" CssClass="labelComman" AutoPostBack="true" OnCheckedChanged="rbtEarlyType_OnCheckedChanged" />
                                                                </td>
                                                                 </tr>
                                                                 <tr>
                                                                <td colspan="3" align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                                                    <asp:Panel ID="pnlEarlySal" runat="server">
                                                                    <table>
                                                                    <tr>
                                                                    <td width="230px" align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                                                            <asp:Label ID="Label66" runat="server" CssClass="labelComman" Text="<%$ Resources:Attendance,Relaxation Minute %>"></asp:Label>
                                                           
                                                                    </td>
                                                                    <td width="1px">
                                                                    :
                                                                </td>
                                                                    <td  align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                                                     <asp:TextBox ID="txtEarlyRelaxMin" Width="70px" runat="server" CssClass="textComman" />
                                                                    <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender13" runat="server" Enabled="True"
                                                                        TargetControlID="txtEarlyRelaxMin" ValidChars="1,2,3,4,5,6,7,8,9,0,.">
                                                                    </cc1:FilteredTextBoxExtender>
                                                                    </td>
                                                                    </tr>
                                                                
                                                                    <tr>
                                                                    <td  align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                                                            <asp:Label ID="Label67" runat="server" CssClass="labelComman" Text="<%$ Resources:Attendance,Early Count %>"></asp:Label>
                                                           
                                                                    </td>
                                                                    <td width="1px">
                                                                    :
                                                                </td>
                                                                    <td  align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                                                     <asp:TextBox ID="txtEarlyCount" Width="70px" runat="server" CssClass="textComman" />
                                                                    <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender14" runat="server" Enabled="True"
                                                                        TargetControlID="txtEarlyCount" ValidChars="1,2,3,4,5,6,7,8,9,0,.">
                                                                    </cc1:FilteredTextBoxExtender>
                                                                    </td>
                                                                    </tr>
                                                                       <tr>
                                                                    <td  align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                                                              <asp:Label ID="Label68" runat="server" CssClass="labelComman" Text="<%$ Resources:Attendance,Value %>"></asp:Label>
                                                           
                                                                    </td>
                                                                    <td width="1px">
                                                                    :
                                                                </td>
                                                                    <td  align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                                                      <asp:TextBox ID="txtEarlyValue" Width="70px" runat="server" CssClass="textComman" />
                                                                    <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender15" runat="server" Enabled="True"
                                                                        TargetControlID="txtEarlyValue" ValidChars="1,2,3,4,5,6,7,8,9,0,.">
                                                                    </cc1:FilteredTextBoxExtender>
                                                                    <asp:DropDownList ID="ddlEarlyType" Width="70px" runat="server" CssClass="DropdownSearch">
                                                                        <asp:ListItem Text="Fixed" Value="1"></asp:ListItem>
                                                                        <asp:ListItem Text="%" Value="2"></asp:ListItem>
                                                                    </asp:DropDownList>
                                                                    </td>
                                                                    
                                                                    
                                                                    </tr>
                                                                    
                                                                    
                                                                    
                                                                    </table>
                                                                    
                                                                    </asp:Panel>
                                                                    <asp:Panel ID="pnlEarlyMin" runat="server">
                                                                    
                                                                    <table>
                                                                    <tr>
                                                                    <td width="230px" align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                                                            <asp:Label ID="Label69" runat="server" CssClass="labelComman" Text="<%$ Resources:Attendance,Minute Times %>"></asp:Label>
                                                           
                                                                    </td>
                                                                    <td width="1px">
                                                                    :
                                                                </td>
                                                                    <td  align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                                                     <asp:DropDownList ID="ddlEarlyMinTime" Width="82px" runat="server" CssClass="DropdownSearch">
                                                                        <asp:ListItem Text="2 Times" Value="2"></asp:ListItem>
                                                                        <asp:ListItem Text="3 Times" Value="3"></asp:ListItem>
                                                                         <asp:ListItem Text="4 Times" Value="4"></asp:ListItem>
                                                                    </asp:DropDownList>
                                                                    </td>
                                                                    </tr>
                                                                    <tr>
                                                                    <td width="230px" align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                                                            <asp:Label ID="Label76" runat="server" CssClass="labelComman" Text="<%$ Resources:Attendance,Relaxation Minute %>"></asp:Label>
                                                           
                                                                    </td>
                                                                    <td width="1px">
                                                                    :
                                                                </td>
                                                                    <td  align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                                                     <asp:TextBox ID="txtEarlyRelaxMinWithMinTimes" Width="70px" runat="server" CssClass="textComman" />
                                                                    <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender17" runat="server" Enabled="True"
                                                                        TargetControlID="txtEarlyRelaxMinWithMinTimes" ValidChars="1,2,3,4,5,6,7,8,9,0,.">
                                                                    </cc1:FilteredTextBoxExtender>
                                                                    </td>
                                                                    </tr>
                                                                    
                                                                    </table>
                                                                    </asp:Panel>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </fieldset>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                                    <fieldset style="height: 130px;width:450px">
                                                        <legend>
                                                            <asp:Label ID="Label57" Font-Bold="true" Font-Size="13px" Font-Names="Arial" runat="server"
                                                                Text="<%$ Resources:Attendance,Absent  %>"></asp:Label></legend>
                                                        <table width="100%">
                                                            <tr>
                                                                <td width="230px" align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                                                    <asp:Label ID="Label59" runat="server" CssClass="labelComman" Text="<%$ Resources:Attendance,Deduction Value %>"></asp:Label>
                                                                </td>
                                                                <td width="1px">
                                                                    :
                                                                </td>
                                                                <td align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                                                    <asp:TextBox ID="txtAbsentDeduc" Width="70px" runat="server" CssClass="textComman" />
                                                                    <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender10" runat="server" Enabled="True"
                                                                        TargetControlID="txtAbsentDeduc" ValidChars="1,2,3,4,5,6,7,8,9,0,.">
                                                                    </cc1:FilteredTextBoxExtender>
                                                                    <asp:DropDownList ID="ddlAbsentType" Width="70px" runat="server" CssClass="DropdownSearch">
                                                                        <asp:ListItem Text="Fixed" Value="1"></asp:ListItem>
                                                                        <asp:ListItem Text="%" Value="2"></asp:ListItem>
                                                                    </asp:DropDownList>
                                                                </td>
                                                                <td align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                                                </td>
                                                            </tr>
                                                            
                                                            
                                                            
                                                                <tr>
                                                                <td width="230px" align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                                                    <asp:Label ID="Label77" runat="server" CssClass="labelComman" Text="<%$ Resources:Attendance,No Clock In Count As Absent %>"></asp:Label>
                                                                </td>
                                                                <td width="1px">
                                                                    :
                                                                </td>
                                                                <td align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                                                    <asp:CheckBox ID="chkNoClockIn" runat="server" CssClass="labelComman"  />
                                                                </td>
                                                               
                                                            </tr>
                                                            
                                                            
                                                             <tr>
                                                                <td width="230px" align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                                                    <asp:Label ID="Label78" runat="server" CssClass="labelComman" Text="<%$ Resources:Attendance,No Clock Out Count As Absent %>"></asp:Label>
                                                                </td>
                                                                <td width="1px">
                                                                    :
                                                                </td>
                                                                <td align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                                                   <asp:CheckBox ID="chkNoClockOut" runat="server" CssClass="labelComman"  />
                                                                </td>
                                                               
                                                            </tr>
                                                            
                                                        </table>
                                                    </fieldset>
                                                </td>
                                                <td align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                                    <fieldset style="height: 130px;width:450px;">
                                                        <legend>
                                                            <asp:Label ID="Label58" Font-Bold="true" Font-Size="13px" Font-Names="Arial" runat="server"
                                                                Text="<%$ Resources:Attendance,Partial Leave Violation %>"></asp:Label></legend>
                                                        <table width="100%">
                                                           
                                                           
                                                           <tr>
                                                                <td colspan="3" align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                                                    <asp:RadioButton ID="rbtnPartialSalary" GroupName="PartialType" Text="<%$ Resources:Attendance,Salary  %>"
                                                                        runat="server" CssClass="labelComman" AutoPostBack="true" OnCheckedChanged="rbtPartialType_OnCheckedChanged" />
                                                                    &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp;
                                                                    <asp:RadioButton ID="rbtnPartialMinutes" GroupName="PartialType" Text="<%$ Resources:Attendance,Minutes %>"
                                                                        runat="server" CssClass="labelComman" AutoPostBack="true" OnCheckedChanged="rbtPartialType_OnCheckedChanged" />
                                                                </td>
                                                                 </tr>
                                                                 <tr>
                                                                <td colspan="3" align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                                                    <asp:Panel ID="pnlPartialSal" runat="server">
                                                                    <table>
                                                                 
                                                                       <tr>
                                                                    <td  width="230px" align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                                                              <asp:Label ID="Label72" runat="server" CssClass="labelComman" Text="<%$ Resources:Attendance,Value %>"></asp:Label>
                                                           
                                                                    </td>
                                                                    <td width="1px">
                                                                    :
                                                                </td>
                                                                    <td  align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                                                      <asp:TextBox ID="txtPartialValue" Width="70px" runat="server" CssClass="textComman" />
                                                                    <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender18" runat="server" Enabled="True"
                                                                        TargetControlID="txtPartialValue" ValidChars="1,2,3,4,5,6,7,8,9,0,.">
                                                                    </cc1:FilteredTextBoxExtender>
                                                                    <asp:DropDownList ID="ddlPartialType" Width="70px" runat="server" CssClass="DropdownSearch">
                                                                        <asp:ListItem Text="Fixed" Value="1"></asp:ListItem>
                                                                        <asp:ListItem Text="%" Value="2"></asp:ListItem>
                                                                    </asp:DropDownList>
                                                                    </td>
                                                                    
                                                                    
                                                                    </tr>
                                                                    
                                                                    
                                                                    
                                                                    </table>
                                                                    
                                                                    </asp:Panel>
                                                                    <asp:Panel ID="pnlPartialMin" runat="server">
                                                                    
                                                                    <table>
                                                                    <tr>
                                                                    <td width="230px" align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                                                            <asp:Label ID="Label73" runat="server" CssClass="labelComman" Text="<%$ Resources:Attendance,Minute Times %>"></asp:Label>
                                                           
                                                                    </td>
                                                                    <td width="1px">
                                                                    :
                                                                </td>
                                                                    <td  align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                                                     <asp:DropDownList ID="ddlPartialMinTime" Width="82px" runat="server" CssClass="DropdownSearch">
                                                                        <asp:ListItem Text="2 Times" Value="2"></asp:ListItem>
                                                                        <asp:ListItem Text="3 Times" Value="3"></asp:ListItem>
                                                                         <asp:ListItem Text="4 Times" Value="4"></asp:ListItem>
                                                                    </asp:DropDownList>
                                                                    </td>
                                                                    </tr>
                                                                    </table>
                                                                    </asp:Panel>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                <td  align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                                    <asp:Label ID="Label38" runat="server" CssClass="labelComman" Text="<%$ Resources:Attendance,Violation Minutes %>"></asp:Label>
                                                </td>
                                                <td  width="1px">
                                                    :
                                                </td>
                                                <td style="padding-right:5px"  align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                                    <asp:TextBox ID="txtViolation" Width="70px" runat="server" CssClass="textComman" />
                                                </td>
                                            </tr>
                                                        </table>
                                                    </fieldset>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="center" colspan="2">
                                                    <asp:Button ID="Button16" runat="server" CssClass="buttonCommman" OnClick="btnSavePanelty_Click"
                                                        TabIndex="8" Text="<%$ Resources:Attendance,Save %>" Width="75px" />
                                                    &nbsp; &nbsp;
                                                    <asp:Button ID="Button17" runat="server" CausesValidation="False" CssClass="buttonCommman"
                                                        OnClick="btnCancelPanelty_Click" TabIndex="10" Text="<%$ Resources:Attendance,Close %>"
                                                        Width="75px" />
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
