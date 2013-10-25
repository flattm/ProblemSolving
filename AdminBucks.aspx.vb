Imports System.Data
Imports Data

Partial Class AdminBucks
    Inherits System.Web.UI.Page

    Shared employees() As String
    Dim dt As DataTable

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            employees = CType(Session.Item("employees"), Array)
        End If
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

    Protected Sub btnGo_Click(sender As Object, e As System.EventArgs) Handles btnGo.Click

        lblError.Visible = False
        lblTotalHeader.Visible = True

        'If Array.BinarySearch(employees, tbEmp.Text) > 0 Then
        Try
            Dim name As String = tbEmp.Text
            dt = GetBuckEntries(name)
            dt.Columns(0).ColumnName = "Date"
            dt.Columns(1).ColumnName = "Description"
            dt.Columns(2).ColumnName = "Amount ($)"
            Dim total As Integer
            For x As Integer = 0 To (dt.Rows.Count - 1)
                total += dt.Rows(x).Item(2)
            Next
            gvBucks.DataSource = dt
            gvBucks.DataBind()
            lblTotal.Text = FormatCurrency(total)
        Catch ex As Exception
            lblError.Text = "There was a problem retrieveing your information.  Check that the name was spelled correctly and try again."
            lblError.Visible = True
        End Try
        'End If
    End Sub
End Class
