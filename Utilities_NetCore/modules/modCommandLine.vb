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
End Module
