Imports Data
Imports System.Data

Partial Class EmpBucks
    Inherits System.Web.UI.Page

    Dim dt As New DataTable

    Protected Sub btnBucks_Click(sender As Object, e As System.EventArgs) Handles btnBucks.Click

        gvBucks.DataSource = Nothing
        gvBucks.DataBind()

        lblError.Visible = False
        lblTotalHeader.Visible = True
        Try
            Dim name As String = GetEmpByNum(CInt(tbEmpNum.Text))
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
            lblError.Text = "There was a problem retrieveing your information.  Please ensure your ID was entered correctly."
            lblError.Visible = True
        End Try

    End Sub

    Protected Sub btnTeams_Click(sender As Object, e As System.EventArgs) Handles btnTeams.Click

        gvBucks.DataSource = Nothing
        gvBucks.DataBind()

        lblError.Visible = False
        lblTotalHeader.Visible = True
        Try
            Dim name As String = GetEmpByNum(CInt(tbEmpNum.Text))
            Dim endDate As String = Today().Year.ToString & "-" & Today.Month.ToString & "-" & Today.Day.ToString
            dt = QuerySelect("Participant", "Individual", name, "2012-1-1", endDate)
            dt.Columns.RemoveAt(0)
            dt.Columns(0).ColumnName = "Close Date"
            dt.Columns(1).ColumnName = "Department"
            dt.Columns(2).ColumnName = "Description"
            dt.Columns(3).ColumnName = "Facilitator"
            gvBucks.DataSource = dt
            gvBucks.DataBind()
            lblTotal.Text = dt.Rows.Count
        Catch ex As Exception
            lblError.Text = "There was a problem retrieveing your information.  Please ensure your ID was entered correctly."
            lblError.Visible = True
        End Try

    End Sub
End Class
