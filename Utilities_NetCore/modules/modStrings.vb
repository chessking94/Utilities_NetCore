Public Module modStrings
    ''' <summary>
    ''' Remove all occurrences of a list of characters from a string
    ''' </summary>
    ''' <param name="pi_Input"></param>
    ''' <param name="pi_RemoveCharacters"></param>
    ''' <returns>
    ''' String without characters present in pi_RemoveCharacters
    ''' </returns>
    Public Function RemoveCharacters(pi_Input As String, pi_RemoveCharacters As List(Of Char)) As String
        Dim result As String = ""
        For Each ch As Char In pi_Input
            If Not pi_RemoveCharacters.Contains(ch) Then
                result &= ch
            End If
        Next
        Return result
    End Function

    ''' <summary>
    ''' Combine two strings and separate them by a provided delimiter
    ''' </summary>
    ''' <param name="pi_Input"></param>
    ''' <param name="pi_Append"></param>
    ''' <param name="pi_Delimiter"></param>
    ''' <returns>
    ''' Merged strings separated by the provided delimiter
    ''' </returns>
    Public Function AppendText(pi_Input As String, pi_Append As String, Optional pi_Delimiter As String = vbCrLf) As String
        If String.IsNullOrWhiteSpace(pi_Input) Then
            Return pi_Append
        Else
            If pi_Append = "" Then
                Return pi_Input
            Else
                pi_Input += pi_Delimiter
                pi_Input += pi_Append
                Return pi_Input
            End If
        End If
    End Function
End Module
