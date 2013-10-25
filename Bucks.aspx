<%@ Page Title="" Language="VB" MasterPageFile="~/Site.master" AutoEventWireup="false" CodeFile="Bucks.aspx.vb" Inherits="Bucks" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </asp:ToolkitScriptManager>
<div>
<table align="center">
<tr>
<td>
<h2>
&nbsp;Bucks Entry</h2>
<br />
</td>
</tr>
<tr>
<td>
    <asp:PlaceHolder ID="phBuckEntry" runat="server"></asp:PlaceHolder>
</td>
</tr>
<tr>
<td>
<br />
    <asp:Button ID="btnAdd" class="button" runat="server" Text="Submit" 
        PostBackUrl="~/Bucks.aspx" />
</td>
</tr>
</table>
</div>
</asp:Content>

