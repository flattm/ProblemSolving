<%@ Page Title="" Language="VB" MasterPageFile="~/Site.master" AutoEventWireup="false" CodeFile="ErrorPage.aspx.vb" Inherits="ErrorPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
<div>
<h2>
The application has encountered an error.</h2>
<br />
    <asp:Label ID="lblMessage" runat="server" Text="" class="filedheader" ></asp:Label>
    <br /><br />
    <asp:Label ID="lblError" runat="server" Text="" class="filedheader"></asp:Label>
</div>
</asp:Content>

