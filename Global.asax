<%@ Application Language="VB" %>

<script runat="server">

    Sub Application_Start(ByVal sender As Object, ByVal e As EventArgs)
        ' Code that runs on application startup
        'Sets the next team id from data entry
        Try
            Application.Add("team_id", Data.GetTeamId() + 1)
        Catch ex As InvalidCastException
            Application.Add("team_id", 1)
        Catch ex As Exception
            Session("errLoc") = "getTeamID"
            Session("err") = ex
            Response.Redirect("ErrorPage.aspx")
        End Try
    End Sub
    
    Sub Application_End(ByVal sender As Object, ByVal e As EventArgs)
        ' Code that runs on application shutdown
    End Sub
        
    Sub Application_Error(ByVal sender As Object, ByVal e As EventArgs)
        ' Code that runs when an unhandled error occurs
    End Sub

    Sub Session_Start(ByVal sender As Object, ByVal e As EventArgs)
        'Get a current listing of the Employees and Departments
        Dim dte = Data.GetEmpNames()
        Dim dtd = Data.GetDeptNames()
        Dim employees(dte.Rows.Count - 1) As String
        Dim departments(dtd.Rows.Count - 1) As String
        Dim count As Integer = 0
        'populate employees array with dte table
        For Each row In dte.Rows
            employees(count) = dte.Rows(count).Item(0).ToString & " " & dte.Rows(count).Item(1).ToString
            count += 1
        Next
        'Reset count
        count = 0
        'populate department array with dtd table
        For Each row In dtd.Rows
            departments(count) = dtd.Rows(count).Item(0)
            count += 1
        Next
        '*******************************************
        Array.Sort(employees)
        Array.Sort(departments)
        Session("employees") = employees
        Session("departments") = departments
    End Sub

    Sub Session_End(ByVal sender As Object, ByVal e As EventArgs)
        ' Code that runs when a session ends. 
        ' Note: The Session_End event is raised only when the sessionstate mode
        ' is set to InProc in the Web.config file. If session mode is set to StateServer 
        ' or SQLServer, the event is not raised.
    End Sub
       
</script>