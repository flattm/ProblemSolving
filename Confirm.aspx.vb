
Partial Class Confirm
    Inherits System.Web.UI.Page


    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        Dim team_id As Integer = CInt(Session.Item("team_id"))
        lblID.Text = "The team id for the entry was " & team_id.ToString & ". Please keep this number with the paper submission for your records."
    End Sub

    Protected Sub btnBack_Click(sender As Object, e As System.EventArgs) Handles btnBack.Click
        Response.Redirect("Default.aspx")
    End Sub
End Class
