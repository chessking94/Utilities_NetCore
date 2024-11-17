Imports Microsoft.Data.SqlClient

Public Module modDatabase
    Public Function ConnectionString(piServer As String, piDatabase As String, piUsername As String, piPassword As String, Optional piApplication As String = "Unknown") As String
        Return _
            "Server=" & piServer &
            ";Database=" & piDatabase &
            ";Application Name=" & piApplication &
            ";MultipleActiveResultSets=True" &
            ";UID=" & piUsername &
            ";PWD=" & piPassword
    End Function

    Public Function Connection(piServer As String, piDatabase As String, piUsername As String, piPassword As String, Optional piApplication As String = "Unknown") As SqlConnection
        Dim objl_Connection As New SqlConnection(ConnectionString(piServer, piDatabase, piUsername, piPassword, piApplication))
        objl_Connection.Open()
        Return objl_Connection
    End Function

    Public Function Connection(piConnectionString As String) As SqlConnection
        Dim objl_Connection As New SqlConnection(piConnectionString)
        objl_Connection.Open()
        Return objl_Connection
    End Function

    Public Function Connection() As SqlConnection
#If DEBUG Then
        Dim strl_ConnectionString = Environment.GetEnvironmentVariable("ConnectionStringDebug")
#Else
        Dim strl_ConnectionString = Environment.GetEnvironmentVariable("ConnectionStringRelease")
#End If
        Dim objl_Connection As New SqlConnection(strl_ConnectionString)
        objl_Connection.Open()
        Return objl_Connection
    End Function
End Module
