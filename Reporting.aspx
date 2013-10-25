<%@ Page Title="Reporting - Continuous Improvement" Language="VB" MasterPageFile="~/Site.master"
    AutoEventWireup="false" CodeFile="Reporting.aspx.vb" Inherits="Reporting" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <style type="text/css">
        .style1
        {
            height: 41px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </asp:ToolkitScriptManager>
    <div class="query">
        <h2>
            Impact Team Reporting</h2>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <table align="center" class="options">
                    <tr>
                        <td class="style1">
                            <h3>
                                Type</h3>
                        </td>
                        <td class="style1" colspan="2">
                            <h3>
                                <asp:Label ID="lblQuantity" runat="server" Text="Quantity"></asp:Label></h3>
                        </td>
                        <td class="style1" colspan="6">
                            <h3>
                                Duration</h3>
                        </td>
                    </tr>
                    <tr>
                        <td>
                        </td>
                        <td>
                        </td>
                        <td>
                            <asp:Label ID="lblName" runat="server" Text="Name" 
                                Visible="False" CssClass="subCat"></asp:Label>
                        </td>
                        <td>
                        </td>
                        <td>
                            <asp:Label ID="lblMonth" runat="server" Text="Month" Visible="False" 
                                CssClass="subCat"></asp:Label>
                        </td>
                        <td>
                        <asp:Label ID="lblMonthYear" runat="server" Text="Year" Visible="False" 
                                CssClass="subCat"></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="lblQuarter" runat="server" Text="Quarter" Visible="False" 
                                CssClass="subCat"></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="lblQuarterYear" runat="server" Text="Year" Visible="False" 
                                CssClass="subCat"></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="lblYear" runat="server" Text="Year" Visible="False" 
                                CssClass="subCat"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:DropDownList ID="ddlType" runat="server" AutoPostBack="True">
                                <asp:ListItem></asp:ListItem>
                                <asp:ListItem>Participant</asp:ListItem>
                                <asp:ListItem>Department</asp:ListItem>
                                <asp:ListItem>Team</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlQuantity" runat="server" AutoPostBack="True">
                                <asp:ListItem></asp:ListItem>
                            </asp:DropDownList>
                        </td>
                        <td>
                            <asp:TextBox ID="tbIndividual" runat="server" CssClass="individual" Visible="False"></asp:TextBox>
                            <asp:AutoCompleteExtender ID="acIndividual" TargetControlID="tbIndividual" runat="server"
                                MinimumPrefixLength="1">
                            </asp:AutoCompleteExtender>
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlDuration" runat="server" AutoPostBack="True">
                                <asp:ListItem></asp:ListItem>
                                <asp:ListItem>Month</asp:ListItem>
                                <asp:ListItem>Quarter</asp:ListItem>
                                <asp:ListItem>Year</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlMonth" runat="server" Visible="False">
                                <asp:ListItem></asp:ListItem>
                                <asp:ListItem Value="01">Jan</asp:ListItem>
                                <asp:ListItem Value="02">Feb</asp:ListItem>
                                <asp:ListItem Value="03">Mar</asp:ListItem>
                                <asp:ListItem Value="04">Apr</asp:ListItem>
                                <asp:ListItem Value="05">May</asp:ListItem>
                                <asp:ListItem Value="06">Jun</asp:ListItem>
                                <asp:ListItem Value="07">Jul</asp:ListItem>
                                <asp:ListItem Value="08">Aug</asp:ListItem>
                                <asp:ListItem Value="09">Sep</asp:ListItem>
                                <asp:ListItem Value="10">Oct</asp:ListItem>
                                <asp:ListItem Value="11">Nov</asp:ListItem>
                                <asp:ListItem Value="12">Dec</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlMonthYear" AppendDataBoundItems="true" runat="server" DataSourceID="Year"
                                DataTextField="close_date" DataValueField="close_date" Visible="False">
                                <asp:ListItem Text="" Value="" />
                            </asp:DropDownList>
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlQtr" AppendDataBoundItems="true" runat="server" DataSourceID="Quarters"
                                DataTextField="quarter" DataValueField="quarter" Visible="False">
                                <asp:ListItem Text="" Value="" />
                            </asp:DropDownList>
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlQtrYear" AppendDataBoundItems="true" runat="server" DataSourceID="Year"
                                DataTextField="close_date" DataValueField="close_date" Visible="False">
                                <asp:ListItem Text="" Value="" />
                            </asp:DropDownList>
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlYear" AppendDataBoundItems="true" runat="server" DataSourceID="Year"
                                DataTextField="close_date" DataValueField="close_date" Visible="False">
                                <asp:ListItem Text="" Value="" />
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="9">
                            <asp:ValidationSummary ID="ValidationSummary1" runat="server" DisplayMode="List" />
                            <asp:Label ID="lblRangeError" runat="server" Text="Select date range." Visible="False"></asp:Label>
                        </td>
                    </tr>
                </table>
            </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="ddlType" EventName="SelectedIndexChanged" />
                <asp:AsyncPostBackTrigger ControlID="ddlQuantity" EventName="SelectedIndexChanged" />
                <asp:AsyncPostBackTrigger ControlID="ddlQuantity" EventName="SelectedIndexChanged" />
                <asp:AsyncPostBackTrigger ControlID="ddlDuration" EventName="SelectedIndexChanged" />
            </Triggers>
        </asp:UpdatePanel>
        <table class="options" align="center">
            <asp:Button class="button" ID="btnRun" runat="server" Text="Run Query" />
        </table>
        <br />
        <asp:RequiredFieldValidator ID="rfvType" runat="server" ErrorMessage="Enter Type"
            Display="None" ControlToValidate="ddlType"></asp:RequiredFieldValidator>
        <asp:RequiredFieldValidator ID="rfvQuantity" runat="server" ErrorMessage="Enter Quantity"
            Display="None" ControlToValidate="ddlQuantity"></asp:RequiredFieldValidator>
        <asp:RequiredFieldValidator ID="rfvIndividual" runat="server" ErrorMessage="Enter Name"
            Display="None" ControlToValidate="tbIndividual"></asp:RequiredFieldValidator>
        <asp:RequiredFieldValidator ID="rfvDuration" runat="server" ErrorMessage="Enter Duration"
            Display="None" ControlToValidate="ddlDuration"></asp:RequiredFieldValidator>
        <br />
        <asp:Panel ID="panTeamInfo" runat="server" Visible="false">
            <hr />
            <h2>
                Team Info</h2>
            <table class="options" align="center">
                <tr>
                    <td>
                        <h3>
                            Team ID</h3>
                    </td>
                    <td>
                        <h3>
                            Close Date</h3>
                    </td>
                    <td>
                        <h3>
                            Department</h3>
                    </td>
                    <td>
                        <h3>
                            Facilitator</h3>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="lblTeamID" runat="server"></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="lblCloseDate" runat="server"></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="lblDept" runat="server"></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="lblFacilitator" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td valign="top">
                        <h4>
                            Description</h4>
                    </td>
                    <tr>
                        <td colspan="4" align="left">
                            <asp:Label ID="lblDesc" runat="server" CssClass="members"></asp:Label>
                        </td>
                    </tr>
                </tr>
                <tr>
                    <td valign="top">
                        <h4>
                            Members</h4>
                    </td>
                    <tr>
                        <td colspan="4" align="left">
                            <asp:PlaceHolder ID="phMembers" runat="server"></asp:PlaceHolder>
                        </td>
                    </tr>
                </tr>
            </table>
            <hr />
        </asp:Panel>
        <br />
        <asp:Panel ID="panResults" runat="server" Visible="False">
            <h2>
                Results</h2>
            <table class="options" align="center">
                <tr>
                    <td>
                        <h3>
                            Total:
                            <asp:Label ID="lblTotal" runat="server" Font-Bold="False"></asp:Label></h3>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:GridView ID="gvResults" runat="server" AllowPaging="True" PageSize="25" CellPadding="3"
                            ForeColor="Black" BorderStyle="Solid" BorderWidth="2px">
                            <AlternatingRowStyle BackColor="#EEEEEE" />
                            <Columns>
                                <asp:CommandField SelectText="View" ShowSelectButton="True">
                                    <ItemStyle ForeColor="#0E273A" />
                                </asp:CommandField>
                                <asp:TemplateField HeaderText="Position">
                                    <ItemTemplate>
                                        <%#Container.DataItemIndex + 1%>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                            <HeaderStyle Font-Names="Century Gothic" ForeColor="#0E273A" BorderColor="#0E273A" />
                            <PagerSettings Mode="NumericFirstLast" PageButtonCount="25" />
                        </asp:GridView>
                    </td>
                </tr>
            </table>
        </asp:Panel>
    </div>
    <asp:SqlDataSource ID="Year" runat="server" ConnectionString="<%$ ConnectionStrings:ImpactBucksConnectionString %>"
        SelectCommand="SELECT DISTINCT YEAR(close_date) AS close_date FROM TeamInfo ORDER BY close_date DESC">
    </asp:SqlDataSource>
    <asp:SqlDataSource ID="Quarters" runat="server" ConnectionString="<%$ ConnectionStrings:ImpactBucksConnectionString %>"
        SelectCommand="SELECT DISTINCT [quarter] FROM [Quarters] ORDER BY [quarter]">
    </asp:SqlDataSource>
</asp:Content>
