<%@ Page Title="Add Team - Continuous Improvement Tracking" Language="VB" MasterPageFile="~/Site.Master"
    AutoEventWireup="false" CodeFile="Default.aspx.vb" Inherits="_Default" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
    <style type="text/css">
        .style1
        {
            width: auto;
        }
        .style2
        {
            height: 36px;
        }
        .style3
        {
            width: 164px;
        }
        .style4
        {
            width: 164px;
            height: 21px;
        }
        .style5
        {
            height: 21px;
        }
    </style>
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </asp:ToolkitScriptManager>
    <div>
        <h2>
            Add Impact Team</h2>
        <table align="center" class="optionsleft">
            <tr>
                <td class="style4" valign="top">
                    <asp:Label ID="Label1" runat="server" CssClass="filedheader" Text="Date:"></asp:Label>
                </td>
                <td class="style5">
                    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                        <ContentTemplate>
                            <asp:ImageButton ID="ibDate" runat="server" CssClass="calbutton" ImageUrl="~/Images/cal_ico.png" />
                            <asp:Label ID="lblDate" runat="server" CssClass="datelabel"></asp:Label>
                            <asp:Calendar ID="calDate" runat="server" BackColor="White" BorderColor="#999999"
                                CellPadding="4" DayNameFormat="Shortest" Font-Names="Verdana" Font-Size="8pt"
                                ForeColor="Black" Height="180px" Visible="False" Width="200px">
                                <DayHeaderStyle BackColor="#2B4559" Font-Bold="True" Font-Size="7pt" ForeColor="White" />
                                <NextPrevStyle VerticalAlign="Bottom" />
                                <OtherMonthDayStyle ForeColor="#808080" />
                                <SelectedDayStyle BackColor="#666666" Font-Bold="True" ForeColor="White" />
                                <SelectorStyle BackColor="#CCCCCC" />
                                <TitleStyle BackColor="#0E273A" BorderColor="Black" Font-Bold="True" ForeColor="White" />
                                <TodayDayStyle BackColor="#CCCCCC" ForeColor="Black" />
                                <WeekendDayStyle BackColor="#7B97AC" />
                            </asp:Calendar>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </td>
            </tr>
            <tr>
                <td class="style3" valign="top">
                    <asp:Label ID="Label2" runat="server" CssClass="filedheader" Text="Facilitator:"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="tbFacilitator" runat="server" Width="250px" MaxLength="255"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="style3" valign="top">
                    <asp:Label ID="Label3" runat="server" CssClass="filedheader" Text="Department:"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="tbDepartment" runat="server" Width="250px" MaxLength="255"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="style3" valign="top">
                    <asp:Label ID="Label4" runat="server" CssClass="filedheader" Text="Team Description:"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="tbDescription" runat="server" MaxLength="255" Width="375px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="style3" valign="top">
                    <asp:Label ID="Label5" runat="server" CssClass="filedheader" Text="Team Members:"></asp:Label>
                    <br />
                </td>
                <td>
                    <div>
                        <asp:UpdatePanel ID="upTeams" runat="server">
                            <ContentTemplate>
                                <asp:PlaceHolder runat="server" ID="phNames"></asp:PlaceHolder>
                            </ContentTemplate>
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="btnAddTextBox" EventName="Click" />
                            </Triggers>
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="btnRemoveTextBox" EventName="Click" />
                            </Triggers>
                        </asp:UpdatePanel>
                    </div>
                    <asp:Button ID="btnAddTextBox" runat="server" Text="Add" class="button" />
                    <asp:Button ID="btnRemoveTextBox" runat="server" Text="Remove" class="button" />
                    <br />
                    <br />
                    <asp:Label ID="lblDuplicates" runat="server" Text="Cannot have duplicate names."
                        Visible="false" class="errorMsg"></asp:Label>
                    <asp:Label ID="lblEmpty" runat="server" Text="Cannot have empty text boxes." Visible="false"
                        class="errorMsg"></asp:Label>
                    <asp:Label ID="lblCalendar" runat="server" Text="Select a date first." Visible="false"
                        class="errorMsg"></asp:Label>
                    <asp:Label ID="lblNotEmployee" runat="server" Text="Employee name is not recognized."
                        Visible="false" class="errorMsg"></asp:Label>
                    <asp:Label ID="lblDepartment" runat="server" Text="Department name is not recognized."
                        Visible="false" class="errorMsg"></asp:Label>
                    <asp:ValidationSummary ID="ValidationSummary1" runat="server" DisplayMode="List"
                        Font-Bold="True" ForeColor="Red" ValidationGroup="default" />
                </td>
            </tr>
            <tr>
                <td class="style3" valign="top">
                    <asp:Button ID="btnSubmit" runat="server" Text="Submit" class="submit button" ValidationGroup="default" />
                </td>
                <td>
                    &nbsp;
                </td>
            </tr>
        </table>
    </div>
    <asp:RequiredFieldValidator ID="rfvFacilitator" runat="server" Display="None" ErrorMessage="Must enter a facilitator first."
        class="validator" ControlToValidate="tbFacilitator" ValidationGroup="default"></asp:RequiredFieldValidator>
    <asp:RequiredFieldValidator ID="rfvDept" runat="server" Display="None" ErrorMessage="Must enter a department first."
        class="validator" ControlToValidate="tbDepartment" ValidationGroup="default"></asp:RequiredFieldValidator>
    <asp:RequiredFieldValidator ID="rfvDescription" runat="server" Display="None" ErrorMessage="Must enter a team description first."
        class="validator" ControlToValidate="tbDescription" ValidationGroup="default"></asp:RequiredFieldValidator>
    <asp:AutoCompleteExtender ID="acFacilitator" TargetControlID="tbFacilitator" runat="server"
        ServiceMethod="GetEmployeeList" MinimumPrefixLength="1">
    </asp:AutoCompleteExtender>
    <asp:AutoCompleteExtender ServiceMethod="GetDeptList" TargetControlID="tbDepartment"
        ID="acDepartment" runat="server" MinimumPrefixLength="1">
    </asp:AutoCompleteExtender>
</asp:Content>
