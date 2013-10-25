Imports Data

Partial Class Bucks
    Inherits System.Web.UI.Page

    Shared employees() As String
    Shared tb()() As TextBox

    Protected Overrides Sub OnInit(ByVal e As System.EventArgs)
        MyBase.OnInit(e)
        tb = New TextBox(24)() {}
        Dim tbl As New Table
        Dim label As Label
        Dim tbName As TextBox
        Dim tbDescription As TextBox
        Dim tbAmount As TextBox
        Dim break As LiteralControl
        Dim row As New TableRow
        Dim col1, col2, col3, col4 As New TableCell
        Dim lbl1, lbl2, lbl3 As New Label
        lbl1.ID = "lbl1"
        lbl1.Text = "Name"
        lbl1.CssClass = "tbBucks"
        lbl2.ID = "lbl2"
        lbl2.Text = "Description"
        lbl2.CssClass = "tbBucks"
        lbl3.ID = "lbl3"
        lbl3.Text = "Amount"
        lbl3.CssClass = "tbBucks"
        col2.Controls.Add(lbl1)
        col3.Controls.Add(lbl2)
        col4.Controls.Add(lbl3)
        row.Cells.Add(col1)
        row.Cells.Add(col2)
        row.Cells.Add(col3)
        row.Cells.Add(col4)
        tbl.Rows.Add(row)
        For x As Integer = 0 To 24 Step 1
            Dim r As New TableRow
            Dim c1, c2, c3, c4 As New TableCell
            label = New Label
            tbName = New TextBox
            tbDescription = New TextBox
            tbAmount = New TextBox
            break = New LiteralControl("<br />")
            label.Text = x + 1
            label.ID = "label" & x
            tbName.ID = "tbName" & x
            tbDescription.ID = "tbDescription" & x
            tbAmount.ID = "tbAmount" & x
            tbName.CssClass = "tbBucks"
            tbDescription.CssClass = "tbBucks"
            tbAmount.CssClass = "tbBucks"
            label.Width = Unit.Pixel(20)
            tbName.Width = Unit.Pixel(175)
            tbDescription.Width = Unit.Pixel(240)
            tbAmount.Width = Unit.Pixel(40)
            Dim autoComplete As AjaxControlToolkit.AutoCompleteExtender = New AjaxControlToolkit.AutoCompleteExtender
            Dim rev As New RegularExpressionValidator
            rev.ControlToValidate = tbAmount.ID
            rev.ValidationExpression = "-?[0-9]{1,5}"
            rev.Text = "Must be a number."
            autoComplete.ID = "autoComplete" & x.ToString()
            autoComplete.TargetControlID = tbName.ID
            autoComplete.ServiceMethod = "GetEmployeeList"
            autoComplete.Enabled = True
            autoComplete.MinimumPrefixLength = 1
            c1.Controls.Add(label)
            c2.Controls.Add(tbName)
            c2.Controls.Add(autoComplete)
            c3.Controls.Add(tbDescription)
            c4.Controls.Add(tbAmount)
            c4.Controls.Add(rev)
            r.Cells.Add(c1)
            r.Cells.Add(c2)
            r.Cells.Add(c3)
            r.Cells.Add(c4)
            tbl.Rows.Add(r)
            tb(x) = New TextBox(2) {}
            tb(x)(0) = tbName
            tb(x)(1) = tbDescription
            tb(x)(2) = tbAmount
        Next
        phBuckEntry.Controls.Add(tbl)
    End Sub

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        employees = CType(Session.Item("employees"), Array)
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

    Protected Sub btnAdd_Click(sender As Object, e As System.EventArgs) Handles btnAdd.Click
        Dim cont As Boolean = True
        For x As Integer = 0 To 24 Step 1
            tb(x)(0).BorderColor = Nothing
            tb(x)(1).BorderColor = Nothing
            tb(x)(2).BorderColor = Nothing
            Select Case EntryValidation(x)
                Case 0
                    tb(x)(0).BorderColor = Drawing.Color.Red
                    cont = False
                Case 1
                    For y As Integer = 0 To 2 Step 1
                        If tb(x)(y).Text = "" Then
                            tb(x)(y).BorderColor = Drawing.Color.Red
                        End If
                    Next
                    cont = False
            End Select
        Next
        If cont Then
            For x As Integer = 0 To 24 Step 1
                Select Case EntryValidation(x)
                    Case 2
                        InsertBucksEntry(tb(x)(0).Text, tb(x)(1).Text, tb(x)(2).Text)
                    Case 3
                        'Ignore blank entries
                End Select
            Next
            Response.Redirect("Bucks.aspx")
        End If
    End Sub

    Protected Function EntryValidation(ByVal index As Integer) As Integer
        Dim caseNumber As Integer = -1
        Dim name As String = tb(index)(0).Text
        Dim description As String = tb(index)(1).Text
        Dim amount As String = tb(index)(2).Text
        If Array.BinarySearch(employees, name) < 0 And name <> "" Then
            caseNumber = 0
        ElseIf name <> "" And description <> "" And amount <> "" Then
            caseNumber = 2
        ElseIf name = "" And description = "" And amount = "" Then
            caseNumber = 3
        Else
            caseNumber = 1
        End If
        Return caseNumber
    End Function
End Class
