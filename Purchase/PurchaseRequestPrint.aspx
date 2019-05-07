<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PurchaseRequestPrint.aspx.cs"
    Inherits="Purchase_PurchaseRequestPrint" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Print</title>

    <script language="javascript" type="text/javascript">
  
  function setprint()
  {     
     window.print();     
  }
    </script>

    <link href="../CSS/InvStyle.css" rel="stylesheet" type="text/css" />
</head>
<body style="background-color: #4e4a4a" onload="setprint()">
    <form id="form1" runat="server">
    <div>
        <center>
            <table width="70%" cellpadding="0" cellspacing="0">
                <tr style="background-color: #90BDE9">
                    <td>
                        <table>
                            <tr>
                                <td>
                                    <img src="../Images/compare.png" width="31" height="30" alt="D" />
                                </td>
                                <td>
                                    <img src="../Images/seperater.png" width="2" height="43" alt="SS" />
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td style="padding-left: 5px" align="left">
                        <asp:Label ID="lblHeader" runat="server" Text="<%$ Resources:Attendance,Purchase Request %>"
                            CssClass="LableHeaderTitle"></asp:Label>
                    </td>
                    <td>
                        <img src="../Images/print.png" width="31" height="30" alt="D" />
                    </td>
                </tr>
                <tr style="background-color: #fff">
                    <td>
                    </td>
                    <td>
                        <table width="100%">
                            <tr>
                                <td align="right" colspan="6">
                                    <table>
                                        <tr>
                                            <td>
                                                <asp:Label ID="lblCompanyName" runat="server" Font-Bold="true" CssClass="labelComman"
                                                    ForeColor="Black" Font-Size="16px"></asp:Label>
                                            </td>
                                            <td rowspan="3">
                                                <asp:Literal ID="litImage" runat="server"></asp:Literal>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:Label ID="lblAddress" runat="server" Font-Bold="true" CssClass="labelComman"
                                                    ForeColor="Black"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:Label ID="lblPhone" runat="server" Font-Bold="true" CssClass="labelComman" ForeColor="Black"></asp:Label>
                                            </td>
                                        </tr>
                                    </table>
                                    <hr />
                                    <br />
                                </td>
                            </tr>
                            <tr>
                                <td width="50px" align="left">
                                    <asp:Label ID="lblRequestdate" runat="server" Text="<%$ Resources:Attendance,Request Date %>"
                                        CssClass="labelComman" ForeColor="Black" Font-Bold="true"></asp:Label>
                                </td>
                                <td width="1px">
                                    :
                                </td>
                                <td width="150px" align="left">
                                    <asp:Label ID="txtRequestdate" runat="server" Font-Bold="true" CssClass="labelComman"
                                        ForeColor="Black"></asp:Label>
                                </td>
                                <td width="90px" align="left">
                                    <asp:Label ID="lblRequestNo" runat="server" Font-Bold="true" Text="<%$ Resources:Attendance,Request No %>"
                                        CssClass="labelComman" ForeColor="Black"></asp:Label>
                                </td>
                                <td width="1px">
                                    :
                                </td>
                                <td>
                                    <asp:Label ID="txtRequestNo" runat="server" Font-Bold="true" CssClass="labelComman"
                                        ForeColor="Black"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td align="left" colspan="6">
                                    <asp:GridView ID="gvProductRequestDetails" runat="server" AutoGenerateColumns="False"
                                        Width="100%" BorderColor="Black" BorderStyle="Solid" BorderWidth="1px">
                                        <Columns>
                                            <asp:TemplateField HeaderText="<%$ Resources:Attendance,Serial No %>">
                                                <HeaderStyle CssClass="labelComman" ForeColor="Black" BorderColor="Black" BorderStyle="Solid"
                                                    BorderWidth="1px" />
                                                <ItemTemplate>
                                                    <asp:Label ID="lblSerialNO" runat="server" Text='<%#Container.DataItemIndex+1 %>'
                                                        CssClass="labelComman" ForeColor="Black"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle BorderColor="Black" BorderStyle="Solid" BorderWidth="1px" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="<%$ Resources:Attendance,Product Name %>">
                                                <HeaderStyle CssClass="labelComman" ForeColor="Black" BorderColor="Black" BorderStyle="Solid" BorderWidth="1px" />
                                                <ItemTemplate>
                                                    <asp:Label ID="lblProductId" runat="server" Text='<%# ProductName(Eval("Product_Id").ToString()) %>'
                                                        CssClass="labelComman" ForeColor="Black"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle BorderColor="Black" BorderStyle="Solid" BorderWidth="1px" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="<%$ Resources:Attendance,Unit Name %>">
                                                <HeaderStyle CssClass="labelComman" ForeColor="Black" BorderColor="Black" BorderStyle="Solid" BorderWidth="1px" />
                                                <ItemTemplate>
                                                    <asp:Label ID="lblUnit" runat="server" Text='<%# UnitName(Eval("UnitId").ToString()) %>'
                                                        CssClass="labelComman" ForeColor="Black"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle BorderColor="Black" BorderStyle="Solid" BorderWidth="1px" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="<%$ Resources:Attendance,Quantity %>">
                                                <HeaderStyle CssClass="labelComman" ForeColor="Black" BorderColor="Black" BorderStyle="Solid" BorderWidth="1px" />
                                                <ItemTemplate>
                                                    <asp:Label ID="lblReqQty" runat="server" Text='<%# Eval("ReqQty") %>' CssClass="labelComman"
                                                        ForeColor="Black"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle BorderColor="Black" BorderStyle="Solid" BorderWidth="1px" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="<%$ Resources:Attendance,Description %>" ItemStyle-Width="50%">
                                                <HeaderStyle CssClass="labelComman" ForeColor="Black" BorderColor="Black" BorderStyle="Solid" BorderWidth="1px" />
                                                <ItemTemplate>
                                                    <asp:Label ID="lblDescription" runat="server" Text='<%# Eval("ProductDescription") %>'
                                                        CssClass="labelComman" ForeColor="Black"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Width="30%" BorderColor="Black" BorderStyle="Solid" BorderWidth="1px" />
                                            </asp:TemplateField>
                                        </Columns>
                                        <HeaderStyle CssClass="labelComman" ForeColor="Black" BorderColor="Black" BorderStyle="None" BorderWidth="1px" />
                                    </asp:GridView>
                                </td>
                            </tr>
                            <tr>
                                <td align="left">
                                    <asp:Label ID="lblRemark" runat="server" Text="<%$ Resources:Attendance,Description %>"
                                        Font-Bold="true" CssClass="labelComman" ForeColor="Black"></asp:Label>
                                </td>
                                <td width="1px">
                                    :
                                </td>
                                <td align="left" colspan="4">
                                    <asp:Label ID="txtRemark" runat="server" CssClass="labelComman" ForeColor="Black"
                                        Font-Bold="true"></asp:Label>
                                </td>
                            </tr>
                        </table>
                        <br />
                    </td>
                    <td>
                    </td>
                </tr>
                <tr style="background-color: #90BDE9">
                    <td>
                        <br />
                    </td>
                    <td style="padding-left: 5px" align="left">
                    </td>
                    <td>
                    </td>
                </tr>
            </table>
        </center>
    </div>
    </form>
</body>
</html>
