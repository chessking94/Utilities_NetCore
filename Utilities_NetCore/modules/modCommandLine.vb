Imports System.IO

Public Module modCommandLine
    Public Function ParseCommandLineArguments(args As String()) As Dictionary(Of String, String)
        Dim parameters As New Dictionary(Of String, String)
        Dim currentKey As String = Nothing

        For Each arg As String In args
            If arg.StartsWith("-"c) Then
                'found a hyphen, this is a key. Technically this doesn't allow an actual argument to start with a "-", but oh well
                'TODO: this also does not support -tTEST style there has to be a space between the positional name and the argument itself
                currentKey = arg.TrimStart("-"c)
                If parameters.ContainsKey(currentKey) Then
                    Throw New Exception($"Found multiple instances of argument '{currentKey}'")
                Else
                    parameters(currentKey) = Nothing  'set default value
                End If
            Else
                'no hyphen, it is the value for the most recent key found
                If currentKey Is Nothing Then
                    Throw New Exception($"Argument '{arg}' passed with no positional identifier")
                Else
                    parameters(currentKey) = arg
                    currentKey = Nothing
                End If
            End If
        Next

        Return parameters
    End Function

    Public Function RunCommand(pi_command As String, Optional pi_workingDir As String = Nothing, Optional pi_arguments As String = Nothing, Optional pi_permanent As Boolean = False) As Integer
        'based on https://stackoverflow.com/a/10263144
        Dim process As Process = New Process()
        Dim processInfo As ProcessStartInfo = New ProcessStartInfo()
        processInfo.CreateNoWindow = True

        If Not Directory.Exists(pi_workingDir) Then
            Throw New DirectoryNotFoundException
        End If

        If Not String.IsNullOrWhiteSpace(pi_workingDir) Then processInfo.WorkingDirectory = pi_workingDir
        processInfo.Arguments = " " + If(pi_permanent = True, "/K", "/C") + " " + pi_command

        If Not String.IsNullOrWhiteSpace(pi_arguments) Then
            processInfo.Arguments += " "
            processInfo.Arguments += pi_arguments
        End If
        processInfo.FileName = "cmd.exe"
        process.StartInfo = processInfo
        process.Start()
        process.WaitForExit()

        Return process.ExitCode
    End Function
End Module
