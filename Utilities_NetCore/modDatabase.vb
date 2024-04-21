﻿Public Module modDatabase
    Public Function ConnectionString_Local(str_Database As String, Optional str_Application As String = "Unknown") As String
        Return _
            "Server=localhost" &
            ";Database=" & str_Database &
            ";Integrated Security=SSPI" &
            ";Application Name=" & str_Application &
            ";MultipleActiveResultSets=True"
    End Function

    Public Function ConnectionLocal(str_Database As String, Optional str_Application As String = "Unknown") As Microsoft.Data.SqlClient.SqlConnection
        Dim objl_Connection As New Microsoft.Data.SqlClient.SqlConnection(ConnectionString_Local(str_Database, str_Application))
        objl_Connection.Open()
        Return objl_Connection
    End Function
End Module
