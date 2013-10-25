Imports Data
Imports System.Data
Imports System.Net.Mail


Partial Class _Default
    Inherits System.Web.UI.Page

    Shared count As Integer = 1
    Shared employees() As String
    Shared departments() As String
    Private textboxes() As TextBox

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        'If verified() = False Then
        '    Response.Redirect("NotVerified.aspx")
        'End If
        If Not IsPostBack Then
            employees = CType(Session.Item("employees"), Array)
            departments = CType(Session.Item("departments"), Array)
            calDate.SelectedDate = Today
            lblDate.Text = calDate.SelectedDate.ToShortDateString
        End If
    End Sub

    Protected Sub Page_PreInit(sender As Object, e As System.EventArgs) Handles Me.PreInit
        Dim myControl As Control = GetPostBackControl(Master.Page)
        If (myControl IsNot Nothing) Then
            If (myControl.ClientID.ToString().Contains("btnAddTextBox")) Then
                count = count + 1
            End If
            If (myControl.ClientID.ToString().Contains("btnRemoveTextBox")) Then
                If count > 0 Then
                    count = count - 1
                End If
            End If
        End If
    End Sub

    Protected Overrides Sub OnInit(ByVal e As System.EventArgs)
        MyBase.OnInit(e)
        textboxes = New TextBox(count - 1) {}
        Dim i As Integer
        For i = 0 To (count - 1) Step 1
            'Add textbox
            Dim textBox As TextBox = New TextBox()
            textBox.ID = "tbName" + i.ToString()
            textBox.Width = Unit.Pixel(250)
            textBox.MaxLength = 255
            textBox.Style.Add("margin-bottom", "5px")
            phNames.Controls.Add(textBox)
            textboxes(i) = textBox
            'Add autocomplete
            Dim autoComplete As AjaxControlToolkit.AutoCompleteExtender = New AjaxControlToolkit.AutoCompleteExtender
            autoComplete.ID = "autoComplete" & i.ToString()
            autoComplete.TargetControlID = textBox.ID
            autoComplete.ServiceMethod = "GetEmployeeList"
            autoComplete.Enabled = True
            autoComplete.MinimumPrefixLength = 1
            phNames.Controls.Add(autoComplete)
            'Add break
            Dim literalBreak As LiteralControl = New LiteralControl("<br />")
            phNames.Controls.Add(literalBreak)
        Next
    End Sub

    Public Shared Function GetPostBackControl(ByVal thePage As Page) As Control
        Dim myControl As Control = Nothing
        Dim ctrlName As String = thePage.Request.Params.Get("__EVENTTARGET")
        If ((ctrlName IsNot Nothing) And (ctrlName <> String.Empty)) Then
            myControl = thePage.FindControl(ctrlName)
        Else
            For Each Item As String In thePage.Request.Form
                Dim c As Control = thePage.FindControl(Item)
                If (TypeOf (c) Is System.Web.UI.WebControls.Button) Then
                    myControl = c
                End If
            Next

        End If
        Return myControl
    End Function

    Protected Sub ibDate_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles ibDate.Click
        ibDate.Visible = False
        lblDate.Visible = False
        calDate.Visible = True
    End Sub

    Protected Sub calDate_SelectionChanged(sender As Object, e As System.EventArgs) Handles calDate.SelectionChanged
        ibDate.Visible = True
        lblDate.Visible = True
        lblDate.Text = calDate.SelectedDate
        calDate.Visible = False
    End Sub

    Protected Sub btnAddTextBox_Click(sender As Object, e As System.EventArgs) Handles btnAddTextBox.Click
        'This is taken care of in the pre / init events
    End Sub

    Protected Sub btnSubmit_Click(sender As Object, e As System.EventArgs) Handles btnSubmit.Click
        For Each tb In textboxes
            tb.BorderWidth = Unit.Pixel(0)
            tb.BorderColor = Drawing.Color.Red
        Next
        tbFacilitator.BorderWidth = Unit.Pixel(0)
        tbFacilitator.BorderColor = Drawing.Color.Red
        tbDepartment.BorderWidth = Unit.Pixel(0)
        tbDepartment.BorderColor = Drawing.Color.Red
        Dim caseNumber As Integer = Validation()
        If lblDate.Text = String.Empty Or lblDate.Text = Nothing Then
            caseNumber = 0
        End If
        Select Case caseNumber
            Case 0
                lblCalendar.Visible = True
                lblEmpty.Visible = False
                lblDuplicates.Visible = False
                lblNotEmployee.Visible = False
                lblDepartment.Visible = False
            Case 1
                lblCalendar.Visible = False
                lblEmpty.Visible = False
                lblDuplicates.Visible = True
                lblNotEmployee.Visible = False
                lblDepartment.Visible = False
            Case 2
                lblCalendar.Visible = False
                lblDuplicates.Visible = False
                lblEmpty.Visible = True
                lblNotEmployee.Visible = False
                lblDepartment.Visible = False
            Case 3
                lblNotEmployee.Visible = True
                lblCalendar.Visible = False
                lblDuplicates.Visible = False
                lblEmpty.Visible = False
                lblDepartment.Visible = False
                Dim tb As TextBox = CType(Session.Item("tb"), TextBox)
                tb.BorderColor = Drawing.Color.Red
                tb.BorderWidth = Unit.Pixel(2)
            Case 4
                lblDepartment.Visible = True
                lblNotEmployee.Visible = False
                lblCalendar.Visible = False
                lblDuplicates.Visible = False
                lblEmpty.Visible = False
                tbDepartment.BorderColor = Drawing.Color.Red
                tbDepartment.BorderWidth = Unit.Pixel(2)
            Case 5
                Application.Lock()
                Dim team_id As Integer = CInt(Application.Item("team_id"))
                Try
                    InsertTeamInfo(team_id, calDate.SelectedDate, tbFacilitator.Text, tbDepartment.Text, tbDescription.Text)
                    InsertBucks(tbFacilitator.Text, "Closed Team - " & tbDescription.Text)
                    Dim x As Integer
                    For x = 0 To (count - 1) Step 1
                        InsertMembers(team_id, textboxes(x).Text)
                        InsertBucks(textboxes(x).Text, "Closed Team - " & tbDescription.Text)
                    Next
                Catch ex As Exception
                    Session("errLoc") = "submitTeam"
                    Session("err") = ex
                    Response.Redirect("ErrorPage.aspx")
                Finally
                    Application("team_id") = (team_id + 1)
                    Application.UnLock()
                End Try
                CheckFifteen(tbDepartment.Text)
                Session("team_id") = team_id
                Session("confirm") = "submit"
                Response.Redirect("Confirm.aspx")
        End Select
    End Sub

    '
    Protected Function Validation() As Integer
        Dim caseNumber As Integer = 5
        Dim x, y As Integer
        For x = 0 To (count - 1) Step 1
            For y = (x + 1) To (count - 1) Step 1
                If textboxes(x).Text.Equals(textboxes(y).Text) Then
                    caseNumber = 1
                End If
            Next
            If textboxes(x).Text = String.Empty Or textboxes(x).Text = Nothing Then
                caseNumber = 2
            End If
            If Array.BinarySearch(employees, textboxes(x).Text) < 0 Then
                caseNumber = 3
                Session("tb") = textboxes(x)
            End If
        Next
        If Array.BinarySearch(employees, tbFacilitator.Text) < 0 Then
            caseNumber = 3
            Session("tb") = tbFacilitator
        End If
        If Array.BinarySearch(departments, tbDepartment.Text) < 0 Then
            caseNumber = 4
        End If
        Return caseNumber
    End Function

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

    Protected Sub CheckFifteen(ByVal dept As String)
        Dim count As Integer = GetDeptCount(dept)
        If count Mod 15 = 0 Then
            Email(dept)
        End If
    End Sub

    Protected Sub Email(ByVal dept As String)
        Dim client As New SmtpClient("[...]")
        Dim msg As New MailMessage
        Dim body As String = ""
        msg.From = New MailAddress("[...]")
        msg.To.Add(New MailAddress("[...]"))
        msg.Subject = dept & " has 15 closed teams!"
        msg.IsBodyHtml = True
        'Create body
        body = dept & " has reached the goal of 15 closed teams. It's time for thier prize... Tell them what they've won, Johnny!"
        msg.Body = body
        Try
            client.Send(msg)
        Catch ex As Exception
            Session("errLoc") = "email"
            Session("err") = ex
            Response.Redirect("ErrorPage.aspx")
        End Try
    End Sub
End Class
