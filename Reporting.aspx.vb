Imports Data
Imports System.Data
Imports Microsoft
Imports Microsoft.Office.Interop

Partial Class Reporting
    Inherits System.Web.UI.Page

    Shared employees() As String
    Shared departments() As String
    Shared dt As New DataTable



    Protected Sub ddlQuantity_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles ddlQuantity.SelectedIndexChanged
        lblName.Visible = False
        lblQuantity.Text = "Quantity"
        tbIndividual.Visible = False
        rfvIndividual.Enabled = False
        If ddlQuantity.SelectedValue = "Individual" Then
            tbIndividual.Visible = True
            rfvIndividual.Enabled = True
            lblName.Visible = True
            If ddlType.SelectedIndex = 1 Then
                acIndividual.ServiceMethod = "GetEmployeeList"
                acIndividual.Enabled = True
            End If
            If ddlType.SelectedIndex = 2 Then
                acIndividual.ServiceMethod = "GetDeptList"
                acIndividual.Enabled = True
            End If
        End If
        ddlQuantity.Focus()
    End Sub

    Protected Sub ddlType_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles ddlType.SelectedIndexChanged
        lblQuantity.Text = "Quantity"
        ddlQuantity.Items.Clear()
        ddlQuantity.Items.Add("")
        tbIndividual.Visible = False
        rfvIndividual.Enabled = False
        If ddlType.SelectedIndex = 2 Or ddlType.SelectedIndex = 1 Then
            ddlQuantity.Items.Add("Individual")
        End If
        If ddlType.SelectedIndex = 1 Then
            ddlQuantity.Items.Add("Top 10")
        End If
        If ddlType.SelectedIndex > 0 Then
            ddlQuantity.Items.Add("All")
        End If
    End Sub

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        If verified() = False Then
            Response.Redirect("NotVerified.aspx")
        End If
        employees = CType(Session.Item("employees"), Array)
        departments = CType(Session.Item("departments"), Array)
    End Sub

    <System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()>
    Public Shared Function GetEmployeeList(ByVal prefixText As String, ByVal count As Integer) As String()
        Dim index As Integer = 0
        Dim emps As List(Of String) = New List(Of String)
        For Each e In employees
            If e.ToUpper.Contains(prefixText.ToUpper) Then
                emps.Add(e)
            End If
        Next
        Return emps.ToArray
    End Function

    <System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()>
    Public Shared Function GetDeptList(ByVal prefixText As String, ByVal count As Integer) As String()
        Dim index As Integer = 0
        Dim dept As List(Of String) = New List(Of String)
        For Each d In departments
            If d.ToUpper.Contains(prefixText.ToUpper) Then
                dept.Add(d)
            End If
        Next
        Return dept.ToArray
    End Function

    Protected Sub ddlDuration_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles ddlDuration.SelectedIndexChanged
        If ddlDuration.SelectedIndex = 1 Then
            ddlMonth.Visible = True
            lblMonth.Visible = True
            ddlMonthYear.Visible = True
            lblMonthYear.Visible = True
            ddlQtr.Visible = False
            lblQuarter.Visible = False
            ddlQtrYear.Visible = False
            lblQuarterYear.Visible = False
            ddlYear.Visible = False
            lblYear.Visible = False
        ElseIf ddlDuration.SelectedIndex = 2 Then
            ddlMonth.Visible = False
            lblMonth.Visible = False
            ddlMonthYear.Visible = False
            lblMonthYear.Visible = False
            ddlQtr.Visible = True
            lblQuarter.Visible = True
            ddlQtrYear.Visible = True
            lblQuarterYear.Visible = True
            ddlYear.Visible = False
            lblYear.Visible = False
        ElseIf ddlDuration.SelectedIndex = 3 Then
            ddlMonth.Visible = False
            lblMonth.Visible = False
            ddlMonthYear.Visible = False
            lblMonthYear.Visible = False
            ddlQtr.Visible = False
            lblQuarter.Visible = False
            ddlQtrYear.Visible = False
            lblQuarterYear.Visible = False
            ddlYear.Visible = True
            lblYear.Visible = True
        End If
    End Sub

    Protected Sub btnRun_Click(sender As Object, e As System.EventArgs) Handles btnRun.Click
        panTeamInfo.Visible = False
        lblRangeError.Visible = False
        If (ddlMonth.Visible = True And ddlMonth.SelectedIndex = 0) Or
           (ddlMonthYear.Visible = True And ddlMonthYear.SelectedIndex = 0) Or
           (ddlQtr.Visible = True And ddlQtr.SelectedIndex = 0) Or
           (ddlQtrYear.Visible = True And ddlQtrYear.SelectedIndex = 0) Or
           (ddlYear.Visible = True And ddlYear.SelectedIndex = 0) Then
            lblRangeError.Visible = True
        Else
            Dim dates() As String = GetDates()
            Dim startDate As String = dates(0)
            Dim endDate As String = dates(1)
            If ddlQuantity.SelectedValue = "Top 10" Then
                dt = GetTop10(startDate, endDate)
                gvResults.DataSource = dt
                gvResults.Columns(0).Visible = False
                gvResults.Columns(1).Visible = True
                gvResults.DataBind()
            ElseIf ddlType.SelectedValue = "Participant" And ddlQuantity.SelectedValue = "All" Then
                dt = ParticipantSelect(startDate, endDate)
                gvResults.DataSource = dt
                gvResults.Columns(0).Visible = False
                gvResults.Columns(1).Visible = False
                gvResults.DataBind()
            Else
                dt = QuerySelect(ddlType.SelectedValue, ddlQuantity.SelectedValue, tbIndividual.Text, startDate, endDate)
                dt.Columns(0).ColumnName = "Team ID"
                dt.Columns(1).ColumnName = "Close Date"
                dt.Columns(2).ColumnName = "Department"
                dt.Columns(3).ColumnName = "Description"
                dt.Columns(4).ColumnName = "Facilitator"
                gvResults.DataSource = dt
                gvResults.Columns(0).Visible = True
                gvResults.Columns(1).Visible = False
                gvResults.DataBind()
            End If
            lblTotal.Text = dt.Rows.Count
            panResults.Visible = True
        End If
    End Sub

    Protected Sub gvResults_PageIndexChanging(sender As Object, e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles gvResults.PageIndexChanging
        gvResults.PageIndex = e.NewPageIndex
        gvResults.DataSource = dt
        gvResults.DataBind()
    End Sub

    Protected Function GetDates() As Array
        Dim dates(1) As String
        Dim startDate As String = ""
        Dim endDate As String = ""
        If ddlDuration.SelectedValue = "Month" Then
            Dim month As String = ddlMonth.SelectedValue
            Dim year As String = ddlMonthYear.SelectedValue
            startDate = year & "-" & month & "-01"
            endDate = year & "-" & month & "-" & System.DateTime.DaysInMonth(year, month).ToString
        ElseIf ddlDuration.SelectedValue = "Quarter" Then
            Dim year As String = ddlQtrYear.SelectedValue
            If ddlQtr.SelectedValue = "1st" Then
                startDate = year & "-01-01"
                endDate = year & "-03-" & System.DateTime.DaysInMonth(year, 3).ToString
            ElseIf ddlQtr.SelectedValue = "2nd" Then
                startDate = year & "-04-01"
                endDate = year & "-06-" & System.DateTime.DaysInMonth(year, 6).ToString
            ElseIf ddlQtr.SelectedValue = "3rd" Then
                startDate = year & "-07-01"
                endDate = year & "-09-" & System.DateTime.DaysInMonth(year, 9).ToString
            ElseIf ddlQtr.SelectedValue = "4th" Then
                startDate = year & "-10-01"
                endDate = year & "-12-" & System.DateTime.DaysInMonth(year, 12).ToString
            End If
        ElseIf ddlDuration.SelectedValue = "Year" Then
            Dim year As String = ddlYear.SelectedValue
            startDate = year & "-01-01"
            endDate = year & "-12-" & System.DateTime.DaysInMonth(year, 12).ToString
        End If
        dates(0) = startDate
        dates(1) = endDate
        Return dates
    End Function

    Protected Sub gvResults_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles gvResults.SelectedIndexChanged
        Dim teamID As Integer = CInt(gvResults.SelectedRow.Cells(2).Text)
        Dim teamDT As DataTable = GetTeamInfo(teamID)
        lblTeamID.Text = teamID
        lblCloseDate.Text = teamDT.Rows(0).Item(1).ToString
        lblDept.Text = teamDT.Rows(0).Item(2).ToString
        lblFacilitator.Text = teamDT.Rows(0).Item(3).ToString
        lblDesc.Text = teamDT.Rows(0).Item(4).ToString
        Dim index As Integer = 0
        For Each row In teamDT.Rows
            Dim label As New Label
            label.ID = "lblMember" & index
            label.CssClass = "members"
            label.Text = row.item(5).ToString
            phMembers.Controls.Add(label)
            index += 1
        Next
        panTeamInfo.Visible = True
    End Sub

    Private Sub DatatableToExcel(ByVal dtTemp As DataTable)
        Dim _excel As New Microsoft.Office.Interop.Excel.Application
        Dim wBook As Microsoft.Office.Interop.Excel.Workbook
        Dim wSheet As Microsoft.Office.Interop.Excel.Worksheet

        wBook = _excel.Workbooks.Add()
        wSheet = wBook.ActiveSheet()

        Dim dt As System.Data.DataTable = dtTemp
        Dim dc As System.Data.DataColumn
        Dim dr As System.Data.DataRow
        Dim colIndex As Integer = 0
        Dim rowIndex As Integer = 0

        For Each dc In dt.Columns
            colIndex = colIndex + 1
            _excel.Cells(1, colIndex) = dc.ColumnName
        Next

        For Each dr In dt.Rows
            rowIndex = rowIndex + 1
            colIndex = 0
            For Each dc In dt.Columns
                colIndex = colIndex + 1
                _excel.Cells(rowIndex + 1, colIndex) = dr(dc.ColumnName)
            Next
        Next

        'Will export the report to .xls.  Very quick metod for Admin User to get data without copy nad past or significant changes to code.
        wSheet.Columns.AutoFit()
        Dim strFileName As String = "C:\Report\output_report.xlsx"
        If System.IO.File.Exists(strFileName) Then
            System.IO.File.Delete(strFileName)
        End If

        wBook.SaveAs(strFileName)
        wBook.Close()
        _excel.Quit()
    End Sub
End Class
