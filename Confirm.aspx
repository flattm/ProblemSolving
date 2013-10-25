<%@ Page Title="" Language="VB" MasterPageFile="~/Site.master" AutoEventWireup="false" CodeFile="Confirm.aspx.vb" Inherits="Confirm" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
<div>
<h2>The team information was successfully entered</h2>
<br />
    <asp:Label ID="lblID" runat="server" Text="" class="filedheader"></asp:Label>
    <br />
    <table class="options" align="center">
    <tr>
    <td>
        <asp:Button ID="btnBack" runat="server" Text="Back" CssClass="button" />
    </td>
    </tr>
    </table>
</div>
</asp:Content>

