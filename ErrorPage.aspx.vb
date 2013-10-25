
Partial Class ErrorPage
    Inherits System.Web.UI.Page



    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        Dim errLoc As String = Session.Item("errLoc").ToString
        Dim err As Exception = CType(Session.Item("err"), Exception)
        Dim errString As String
        errString = err.Message.ToString & vbCrLf & vbCrLf & err.StackTrace.ToString


        Select Case errLoc.ToLower
            Case "getteamid"
                lblMessage.Text = "There was a problem when trying to receive the team id. Please contact support " _
                    & "and provide them with the following information:"
                lblError.Text = errString
            Case "submitteam"
                lblMessage.Text = "There was a problem when trying to add the team information to the database. Please " _
                    & "contact support and provide them with the following information:"
                lblError.Text = errString
            Case "email"
                lblMessage.Text = "There was a problem when trying to send email notification. Please " _
                    & "contact support and provide them with the following information:"
                lblError.Text = errString
        End Select
    End Sub
End Class
