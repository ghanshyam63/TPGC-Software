<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CompareQuatation.aspx.cs"
    Inherits="Purchase_CompareQuatation" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Compare Quotation</title>

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
            <table cellpadding="0" cellspacing="0">
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
                        <asp:Label ID="lblHeader" runat="server" Text="<%$ Resources:Attendance,Quotation Comparison %>"
                            CssClass="LableHeaderTitle"></asp:Label>
                    </td>
                    <td>
                        <img src="../Images/print.png" width="31" height="30" alt="D" />
                    </td>
                </tr>
                <tr style="background-color: #e7e7e7">
                    <td>
                    </td>
                    <td>
                        <asp:DataList ID="datalistProduct" runat="server" OnDataBinding="datalistProduct_DataBinding"
                            OnItemDataBound="datalistProduct_ItemDataBound">
                            <ItemTemplate>
                                <br />
                                <table>
                                    <tr>
                                        <td width="100px" align="left">
                                            <asp:Label ID="lblProductName" runat="server" CssClass="labelComman" Text="<%$ Resources:Attendance,Product Name %>"></asp:Label>
                                        </td>
                                        <td width="10px" align="left">
                                            :
                                        </td>
                                        <td align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                            <asp:Label ID="txtProductName" runat="server" CssClass="labelComman" Font-Bold="true"
                                                Text='<%# ProductName(Eval("Product_Id").ToString(),Eval("Product_description").ToString()) %>'></asp:Label>
                                            <asp:Label ID="lblProductId" Visible="false" runat="server" CssClass="labelComman"
                                                Font-Bold="true" Text='<%# Eval("Product_Id") %>'></asp:Label>
                                        </td>
                                    </tr>
                                </table>
                                <asp:GridView ID="gvSupplier" runat="server" BackColor="White" CellPadding="4" ForeColor="Black"
                                    GridLines="Horizontal" AutoGenerateColumns="false" Width="700px">
                                    <Columns>
                                        <asp:TemplateField HeaderText="<%$ Resources:Attendance,Serial No %>" ItemStyle-Width="70px"
                                            ItemStyle-BorderStyle="Solid" ItemStyle-BorderWidth="1px">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvSupplierName" Font-Size="13px" ForeColor="#474646" Font-Names="Trebuchet MS"
                                                    runat="server" Text="<%#Container.DataItemIndex+1 %>"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle BorderStyle="Solid" BorderWidth="1px" Font-Size="13px" ForeColor="#474646"
                                                Font-Names="Trebuchet MS" Font-Bold="true" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="<%$ Resources:Attendance,Supplier Name %>" ItemStyle-Width="120px"
                                            ItemStyle-BorderStyle="Solid" ItemStyle-BorderWidth="1px">
                                            <ItemTemplate>
                                                <asp:HiddenField ID="hdngvSupplierId" runat="server" Value='<%#Eval("Supplier_Id") %>' />
                                                <asp:Label ID="lblgvSupplierName" runat="server" Font-Size="13px" ForeColor="#474646"
                                                    Font-Names="Trebuchet MS" Text='<%#GetSupplierName(Eval("Supplier_Id").ToString()) %>' />
                                            </ItemTemplate>
                                            <HeaderStyle BorderStyle="Solid" BorderWidth="1px" Font-Size="13px" Font-Bold="true"
                                                ForeColor="#474646" Font-Names="Trebuchet MS" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="<%$ Resources:Attendance,Terms & Conditions %>" ItemStyle-Width="400px"
                                            ItemStyle-BorderStyle="Solid" ItemStyle-BorderWidth="1px">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvTermCondition" runat="server" Font-Size="13px" ForeColor="#474646"
                                                    Font-Names="Trebuchet MS" Text='<%# Eval("TermsCondition") %>' />
                                            </ItemTemplate>
                                            <HeaderStyle BorderStyle="Solid" BorderWidth="1px" Font-Size="13px" Font-Bold="true"
                                                ForeColor="#474646" Font-Names="Trebuchet MS" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="<%$ Resources:Attendance,Amount %>" ItemStyle-Width="80px"
                                            ItemStyle-BorderStyle="Solid" ItemStyle-BorderWidth="1px">
                                            <ItemTemplate>
                                                <asp:Label ID="lblAmmount" runat="server" Font-Size="13px" ForeColor="#474646" Font-Names="Trebuchet MS"
                                                    Text='<%# Eval("Amount") %>' />
                                            </ItemTemplate>
                                            <HeaderStyle BorderStyle="Solid" BorderWidth="1px" Font-Size="13px" ForeColor="#474646"
                                                Font-Names="Trebuchet MS" Font-Bold="true" />
                                        </asp:TemplateField>
                                    </Columns>
                                    <FooterStyle BackColor="#CCCC99" ForeColor="Black" />
                                    <PagerStyle BackColor="White" ForeColor="Black" HorizontalAlign="Right" />
                                    <SelectedRowStyle BackColor="#CC3333" Font-Bold="True" ForeColor="White" />
                                    <HeaderStyle Font-Bold="True" ForeColor="Black" />
                                </asp:GridView>
                            </ItemTemplate>
                        </asp:DataList>
                        <br />
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
