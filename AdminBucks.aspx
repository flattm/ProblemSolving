<%@ Page Title="" Language="VB" MasterPageFile="~/Site.master" AutoEventWireup="false" CodeFile="AdminBucks.aspx.vb" Inherits="AdminBucks" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </asp:ToolkitScriptManager>
    <table width="100%">
        <tr>
            <td align="center" valign="middle">
                <div>
                    <h2>
                        View Bucks</h2>
                </div>
                <br />
                <div>
                    <h5>
                        <asp:Label ID="Label1" runat="server" Text="Select Employee:"></asp:Label></h5>
                    <asp:TextBox ID="tbEmp" runat="server"></asp:TextBox>
                    <br />
                    <br />
                    <asp:Label ID="lblError" runat="server" Visible="False"></asp:Label>
                    <br />
                    <br />
                    <asp:Button ID="btnGo" runat="server" Text="Go" CssClass="button" />
                </div>
                <br />
                <div>
                    <table>
                        <tr>
                            <td>
                                    <asp:Label ID="lblTotalHeader" runat="server" Text="Total:" Visible="False" 
                                        Font-Bold="True" ForeColor="#0E273A"></asp:Label>
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
    <asp:AutoCompleteExtender ID="acEmployee" runat="server" 
        BehaviorID="acEmployee" MinimumPrefixLength="1" ServiceMethod="GetEmployeeList" 
        TargetControlID="tbEmp">
    </asp:AutoCompleteExtender>
</asp:Content>

