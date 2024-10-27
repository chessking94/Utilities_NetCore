Public Module modLogging
    ''' <summary>
    ''' Perform actions related to process logging
    ''' </summary>
    ''' <param name="piProgram"></param>
    ''' <param name="piLanguage"></param>
    ''' <param name="piFunction"></param>
    ''' <param name="piLevel"></param>
    ''' <param name="piMessage"></param>
    ''' <param name="piLogMethod"></param>
    ''' <returns>
    ''' 0 if written to console, inserted database key if written to database, full name of file if written to file
    ''' </returns>
    Public Function AddLog(piProgram As String, piLanguage As String, piFunction As String, piLevel As eLogLevel, piMessage As String, piLogMethod As eLogMethod) As Object
        Select Case piLogMethod
            Case eLogMethod.CONSOLE
                Return LogConsole(piFunction, piLevel, piMessage)

            Case eLogMethod.DATABASE
                Return LogDatabase(piProgram, piLanguage, piFunction, piLevel, piMessage)

            Case eLogMethod.FILE
                'Return LogFile()
                Throw New Exception("This method is not currently supported")

            Case Else
                Throw New Exception("Invalid Log Method")

        End Select
    End Function

    ''' <summary>
    ''' Write a log method to the console, with the Date, Function, Level, and Message being tab-delimited
    ''' </summary>
    ''' <param name="piFunction"></param>
    ''' <param name="piLevel"></param>
    ''' <param name="piMessage"></param>
    ''' <returns>Always 0</returns>
    Private Function LogConsole(piFunction As String, piLevel As eLogLevel, piMessage As String) As Integer
        Dim strl_Line = $"{Date.Now:yyyy-MM-dd HH:mm:ss,fff}\t{piFunction}\t{piLevel}\t{piMessage}"
        Console.WriteLine(strl_Line)

        Return 0
    End Function

    ''' <summary>
    ''' Write a record to a database for process logging
    ''' </summary>
    ''' <param name="piProgram"></param>
    ''' <param name="piLanguage"></param>
    ''' <param name="piFunction"></param>
    ''' <param name="piLevel"></param>
    ''' <param name="piMessage"></param>
    ''' <returns>
    ''' Table key if record was inserted, -1 if process failed
    ''' </returns>
    Private Function LogDatabase(piProgram As String, piLanguage As String, piFunction As String, piLevel As eLogLevel, piMessage As String) As Long
        Try
#If DEBUG Then
            Dim strl_ConnectionString = Environment.GetEnvironmentVariable("ConnectionStringDebug")
#Else
            Dim strl_ConnectionString = Environment.GetEnvironmentVariable("ConnectionStringRelease")
#End If

            Dim objl_CMD As New Microsoft.Data.SqlClient.SqlCommand
            objl_CMD.Connection = modDatabase.Connection(strl_ConnectionString)
            objl_CMD.CommandType = Data.CommandType.Text

            objl_CMD.CommandText =
                "
                    INSERT INTO HuntHome.Logs.Entries (ScriptName, FileDate, ScriptType, LogDate, LogTime, [Function], LevelID, Message)
                    VALUES (@ProgramName, GETDATE(), @Language, CAST(GETDATE() AS DATE), CAST(GETDATE() AS TIME(3)), @Function, @LevelID, @Message)

                    SELECT @@IDENTITY
                "

            objl_CMD.Parameters.AddWithValue("@ProgramName", piProgram)
            objl_CMD.Parameters.AddWithValue("@Language", piLanguage)
            objl_CMD.Parameters.AddWithValue("@Function", piFunction)
            objl_CMD.Parameters.AddWithValue("@LevelID", piLevel)
            objl_CMD.Parameters.AddWithValue("@Message", piMessage)

            Dim lngl_Return As Long = objl_CMD.ExecuteScalar

            objl_CMD.Connection.Close()
            objl_CMD.Connection.Dispose()
            objl_CMD.Dispose()

            Select Case piLevel
                Case eLogLevel.ERROR, eLogLevel.CRITICAL
                    modNotifications.SendTelegramMessage(piMessage)  'no need to handle exceptions here, that is handled in the method
            End Select

            Return lngl_Return

        Catch ex As Exception
            Return -1

        End Try
    End Function

    Private Function LogFile() As String
        'TODO
        Return ""
    End Function

    ''' <summary>
    ''' Mirror of logging levels found in Python's logging module
    ''' </summary>
    Public Enum eLogLevel
        DEBUG = 1
        INFO = 2
        WARNING = 3
        [ERROR] = 4
        CRITICAL = 5
    End Enum

    Public Enum eLogMethod
        CONSOLE
        DATABASE
        FILE
    End Enum
End Module
