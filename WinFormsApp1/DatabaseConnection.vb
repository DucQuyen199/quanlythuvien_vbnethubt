Imports System.Data.SqlClient

Public Module DatabaseConnection
    Public ConnectionString As String = "Data Source=localhost;Initial Catalog=quanlythuvien;User ID=sa;Password=123456aA@$"
    
    Public Function GetConnection() As SqlConnection
        Dim connection As New SqlConnection(ConnectionString)
        Return connection
    End Function
    
    Public Function ExecuteQuery(query As String) As DataTable
        Dim connection As SqlConnection = GetConnection()
        Dim command As New SqlCommand(query, connection)
        Dim adapter As New SqlDataAdapter(command)
        Dim dataTable As New DataTable()
        
        Try
            connection.Open()
            adapter.Fill(dataTable)
            Return dataTable
        Catch ex As Exception
            MessageBox.Show("Error: " & ex.Message)
            Return Nothing
        Finally
            connection.Close()
        End Try
    End Function
    
    Public Function ExecuteNonQuery(query As String, Optional parameters As List(Of SqlParameter) = Nothing) As Integer
        Dim connection As SqlConnection = GetConnection()
        Dim command As New SqlCommand(query, connection)
        
        If parameters IsNot Nothing Then
            command.Parameters.AddRange(parameters.ToArray())
        End If
        
        Try
            connection.Open()
            Return command.ExecuteNonQuery()
        Catch ex As Exception
            MessageBox.Show("Error: " & ex.Message)
            Return -1
        Finally
            connection.Close()
        End Try
    End Function
End Module 