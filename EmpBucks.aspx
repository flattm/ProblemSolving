<%@ Page Language="VB" AutoEventWireup="false" CodeFile="EmpBucks.aspx.vb" Inherits="EmpBucks" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Irwin Bucks</title>
    <link href="~/Styles/Site.css" rel="stylesheet" type="text/css" />
    </head>
<body>
    <form id="form1" runat="server">
    <table width="100%">
        <tr>
            <td align="center" valign="middle">
                <div>
                    <h3>
                        &nbsp;Bucks</h3>
                </div>
                <br />
                <div>
                    <h5>
                        <asp:Label ID="Label1" runat="server" Text="Please enter your Employee ID:"></asp:Label></h5>
                    <asp:TextBox ID="tbEmpNum" runat="server" CssClass="tbBucks"></asp:TextBox>
                    <br />
                    <br />
                    <asp:Label ID="lblError" runat="server" Visible="False"></asp:Label>
                    <br />
                    <br />
                    <table>
                        <tr>
                            <td style="padding-right: 10px">
                                <asp:Button ID="btnBucks" runat="server" Text="View Bucks" 
                                    CssClass="button" Width="175px" />
                            </td>
                            <td style="padding-left: 10px">
                            <asp:Button ID="btnTeams" runat="server" Text="View Completed Teams" 
                                    CssClass="button" Width="175px" />
                            </td>
                        </tr>
                    </table>
                </div>
                <br />
                <div>
                    <table>
                        <tr>
                            <td>
                                <asp:Label ID="lblTotalHeader" runat="server" Text="Total:" Visible="False" Font-Bold="True"
                                    ForeColor="#0E273A"></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="lblTotal" runat="server"></asp:Label>
                            </td>
                        </tr>
                    </table>
                </div>
                <div>
                    <asp:GridView ID="gvBucks" runat="server" BorderStyle="Solid" BorderWidth="2px" CellPadding="3"
                        ForeColor="Black">
                        <AlternatingRowStyle BackColor="#EEEEEE" />
                        <HeaderStyle BorderColor="#0E273A" Font-Names="Century Gothic" ForeColor="#0E273A" />
                    </asp:GridView>
                </div>
            </td>
        </tr>
    </table>
    </form>
</body>
</html>
