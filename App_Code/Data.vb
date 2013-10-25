Imports Microsoft.VisualBasic
Imports System.ComponentModel
Imports System.Data.SqlClient
Imports System.Data

Public Class Data
    '****************************************************************************************************************************************************************
    'Only members of the ContImproveTracking security group will have access
    'This is added to the page_load event of each page
    '****************************************************************************************************************************************************************
    Public Shared Function verified() As Boolean
        Dim user As New ApplicationServices.User
        Return user.IsInRole("[...]")
    End Function

    Private Shared Function GetConnectionString() As String
        Return ConfigurationManager.ConnectionStrings("[..]ConnectionString").ConnectionString
    End Function

    Private Shared Function GetConnectionStringEdu() As String
        Return ConfigurationManager.ConnectionStrings("[..]ConnectionString").ConnectionString
    End Function

    <DataObjectMethod(DataObjectMethodType.Select)> _
    Public Shared Function GetEmpNames() As DataTable
        Dim con As New SqlConnection(GetConnectionStringEdu)
        Dim sel As String = "SELECT FirstName, LastName " _
                            & "FROM Employee " _
                            & "WHERE Active = 1 " _
                            & "ORDER BY FirstName ASC"
        Dim cmd As New SqlCommand(sel, con)
        Dim dt As DataTable = New DataTable
        Try
            con.Open()
            Dim adapter As SqlDataAdapter = New SqlDataAdapter(cmd)
            adapter.Fill(dt)
        Catch ex As Exception
            Throw ex
        End Try
        Return dt
    End Function

    <DataObjectMethod(DataObjectMethodType.Select)> _
    Public Shared Function GetDeptNames() As DataTable
        Dim con As New SqlConnection(GetConnectionString)
        Dim sel As String = "SELECT name " _
                            & "FROM Department " _
                            & "ORDER BY name ASC"
        Dim cmd As New SqlCommand(sel, con)
        Dim dt As DataTable = New DataTable
        Try
            con.Open()
            Dim adapter As SqlDataAdapter = New SqlDataAdapter(cmd)
            adapter.Fill(dt)
        Catch ex As Exception
            Throw ex
        End Try
        Return dt
    End Function

    <DataObjectMethod(DataObjectMethodType.Select)> _
    Public Shared Function GetEmpByNum(ByVal number As Integer) As String
        Dim con As New SqlConnection(GetConnectionStringEdu)
        Dim sel As String = "SELECT FirstName, LastName " _
                            & "FROM Employee " _
                            & "WHERE HRFILE_EmployeeID = @EmployeeID"
        Dim cmd As New SqlCommand(sel, con)
        cmd.Parameters.AddWithValue("EmployeeID", number)
        Dim dt As DataTable = New DataTable
        Try
            con.Open()
            Dim adapter As SqlDataAdapter = New SqlDataAdapter(cmd)
            adapter.Fill(dt)
        Catch ex As Exception
            Throw ex
        End Try
        Dim first As String = dt.Rows(0).Item(0).ToString.Trim
        Dim last As String = dt.Rows(0).Item(1).ToString.Trim
        Dim name As String = first & " " & last
        Return name
    End Function

    <DataObjectMethod(DataObjectMethodType.Select)> _
    Public Shared Function GetBuckEntries(ByVal name As String) As DataTable
        Dim con As New SqlConnection(GetConnectionString)
        Dim sel As String = "SELECT convert(varchar, date, 107), description, amount " _
                            & "FROM Bucks " _
                            & "WHERE name = @name " _
                            & "ORDER BY date DESC"
        Dim cmd As New SqlCommand(sel, con)
        cmd.Parameters.AddWithValue("name", name)
        Dim dt As DataTable = New DataTable
        Try
            con.Open()
            Dim adapter As SqlDataAdapter = New SqlDataAdapter(cmd)
            adapter.Fill(dt)
        Catch ex As Exception
            Throw ex
        End Try
        Return dt
    End Function

    <DataObjectMethod(DataObjectMethodType.Select)> _
    Public Shared Function GetTeamId() As Integer
        Dim con As New SqlConnection(GetConnectionString)
        Dim sel As String = "SELECT MAX(team_id)" _
                            & " FROM TeamInfo"
        Dim cmd As New SqlCommand(sel, con)
        Dim dt As DataTable = New DataTable
        Try
            con.Open()
            Dim adapter As SqlDataAdapter = New SqlDataAdapter(cmd)
            adapter.Fill(dt)
            con.Close()
        Catch ex As Exception
            Throw ex
        End Try
        Dim team_id As Integer = dt.Rows.Item(0).Item(0)
        Return team_id
    End Function

    <DataObjectMethod(DataObjectMethodType.Insert)> _
    Public Shared Sub InsertTeamInfo(ByVal team_id As Integer, ByVal close_date As Date, ByVal facilitator As String,
                                     ByVal dept As String, ByVal team_description As String)
        Dim con As New SqlConnection(GetConnectionString)
        Dim ins As String = "INSERT INTO TeamInfo " _
                            & "(team_id, close_date, facilitator, dept, team_description) " _
                            & "VALUES(@team_id, @close_date, @facilitator, @dept, @team_description)"
        Dim cmd As New SqlCommand(ins, con)
        cmd.Parameters.AddWithValue("team_id", team_id)
        cmd.Parameters.AddWithValue("close_date", close_date)
        cmd.Parameters.AddWithValue("facilitator", facilitator)
        cmd.Parameters.AddWithValue("dept", dept)
        cmd.Parameters.AddWithValue("team_description", team_description)
        Try
            con.Open()
            cmd.ExecuteNonQuery()
            con.Close()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    <DataObjectMethod(DataObjectMethodType.Insert)> _
    Public Shared Sub InsertMembers(ByVal team_id As Integer, ByVal member As String)
        Dim con As New SqlConnection(GetConnectionString)
        Dim ins As String = "INSERT INTO Members " _
                            & "(team_id, member) " _
                            & "VALUES(@team_id, @member)"
        Dim cmd As New SqlCommand(ins, con)
        cmd.Parameters.AddWithValue("team_id", team_id)
        cmd.Parameters.AddWithValue("member", member)
        Try
            con.Open()
            cmd.ExecuteNonQuery()
            con.Close()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub


    'Combine these at some point
    <DataObjectMethod(DataObjectMethodType.Insert)> _
    Public Shared Sub InsertBucks(ByVal name As String, ByVal description As String)
        Dim entryDate As String = Today.ToShortDateString
        Dim con As New SqlConnection(GetConnectionString)
        Dim ins As String = "INSERT INTO Bucks " _
                            & "(name, date, description, amount) " _
                            & "VALUES(@name, @date, @description, 3)"
        Dim cmd As New SqlCommand(ins, con)
        cmd.Parameters.AddWithValue("name", name)
        cmd.Parameters.AddWithValue("date", entryDate)
        cmd.Parameters.AddWithValue("description", description)
        Try
            con.Open()
            cmd.ExecuteNonQuery()
            con.Close()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    <DataObjectMethod(DataObjectMethodType.Insert)> _
    Public Shared Sub InsertBucksEntry(ByVal name As String, ByVal description As String, ByVal amount As Integer)
        Dim entryDate As String = Today.ToShortDateString
        Dim con As New SqlConnection(GetConnectionString)
        Dim ins As String = "INSERT INTO Bucks " _
                            & "(name, date, description, amount) " _
                            & "VALUES(@name, @date, @description, @amount)"
        Dim cmd As New SqlCommand(ins, con)
        cmd.Parameters.AddWithValue("name", name)
        cmd.Parameters.AddWithValue("date", entryDate)
        cmd.Parameters.AddWithValue("description", description)
        cmd.Parameters.AddWithValue("amount", amount)
        Try
            con.Open()
            cmd.ExecuteNonQuery()
            con.Close()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    <DataObjectMethod(DataObjectMethodType.Select)> _
    Public Shared Function GetTop10(ByVal startdate As String, ByVal endDate As String) As DataTable
        Dim con As New SqlConnection(GetConnectionString)
        Dim sel As String = "USE [ImpactBucks] " _
                            & "DECLARE @return_value int " _
                            & "EXEC @return_value = [dbo].[GetTop10] " _
                            & "@startDate = '" & startdate.ToString & "', " _
                            & "@endDate = '" & endDate.ToString & "' " _
                            & "SELECT 'Return Value' = @return_value"
        Dim cmd As New SqlCommand(sel, con)
        Dim dt As New DataTable
        Try
            con.Open()
            Dim adapter As SqlDataAdapter = New SqlDataAdapter(cmd)
            adapter.Fill(dt)
            con.Close()
        Catch ex As Exception
            Throw ex
        End Try
        Return dt
    End Function

    <DataObjectMethod(DataObjectMethodType.Select)> _
    Public Shared Function QuerySelect(ByVal type As String, ByVal quantity As String, ByVal name As String,
                                            ByVal startDate As String, ByVal endDate As String) As DataTable
        Dim con As New SqlConnection(GetConnectionString)
        Dim sel As String = "SELECT DISTINCT t.team_id, convert(varchar, close_date, 107) as short_date, dept, team_description, facilitator " _
                            & "FROM TeamInfo t "
        If type = "Participant" Then
            sel &= "FULL JOIN Members m " _
                & "ON m.team_id = t.team_id "
        End If
        sel &= "WHERE close_date BETWEEN '" & startDate & "' AND '" & endDate & "' "
        If quantity = "Individual" Then
            If type = "Participant" Then
                sel &= "AND member = '" & name & "' OR facilitator = '" & name & "' "
            ElseIf type = "Department" Then
                sel &= "AND dept = '" & name & "' "
            End If
        End If
        sel &= "ORDER BY short_date DESC"
        Dim cmd As New SqlCommand(sel, con)
        Dim dt As New DataTable
        Try
            con.Open()
            Dim adapter As SqlDataAdapter = New SqlDataAdapter(cmd)
            adapter.Fill(dt)
            con.Close()
        Catch ex As Exception
            Throw ex
        End Try
        Return dt
    End Function

    <DataObjectMethod(DataObjectMethodType.Select)> _
    Public Shared Function ParticipantSelect(ByVal startDate As String, ByVal endDate As String) As DataTable
        Dim con As New SqlConnection(GetConnectionString)
        Dim sel As String = "SELECT member AS Name, m.team_id As 'Team ID' FROM Members m " _
                            & "FULL JOIN TeamInfo t ON t.team_id = m.team_id " _
                            & "WHERE close_date BETWEEN '" & startDate & "' AND '" & endDate & "' " _
                            & "UNION ALL " _
                            & "SELECT facilitator AS Name, team_id AS 'Team ID' FROM TeamInfo " _
                            & "WHERE close_date BETWEEN '" & startDate & "' AND '" & endDate & "' " _
                            & "ORDER BY Name ASC"
        Dim cmd As New SqlCommand(sel, con)
        Dim dt As New DataTable
        Try
            con.Open()
            Dim adapter As SqlDataAdapter = New SqlDataAdapter(cmd)
            adapter.Fill(dt)
            con.Close()
        Catch ex As Exception
            Throw ex
        End Try
        Return dt
    End Function

    <DataObjectMethod(DataObjectMethodType.Select)> _
    Public Shared Function GetTeamInfo(ByVal teamID As Integer) As DataTable
        Dim con As New SqlConnection(GetConnectionString)
        Dim sel As String = "SELECT t.team_id, convert(varchar, t.close_date, 107), t.dept, t.facilitator, t.team_description, m.member FROM TeamInfo t " _
                            & "JOIN Members m ON t.team_id = m.team_id " _
                            & "WHERE t.team_id = " & teamID
        Dim cmd As New SqlCommand(sel, con)
        Dim dt As New DataTable
        Try
            con.Open()
            Dim adapter As SqlDataAdapter = New SqlDataAdapter(cmd)
            adapter.Fill(dt)
            con.Close()
        Catch ex As Exception
            Throw ex
        End Try
        Return dt
    End Function

    <DataObjectMethod(DataObjectMethodType.Select)> _
    Public Shared Function GetDeptCount(ByVal dept As String) As Integer
        Dim con As New SqlConnection(GetConnectionString)
        Dim sel As String = "SELECT * FROM TeamInfo " _
                            & "WHERE dept = '" & dept & "'"
        Dim cmd As New SqlCommand(sel, con)
        Dim dt As New DataTable
        Try
            con.Open()
            Dim adapter As SqlDataAdapter = New SqlDataAdapter(cmd)
            adapter.Fill(dt)
            con.Close()
        Catch ex As Exception
            Throw ex
        End Try
        Dim count As Integer = dt.Rows.Count
        Return count
    End Function
End Class

