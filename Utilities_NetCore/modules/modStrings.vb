Public Module modStrings
    Public Function RemoveCharacters(pi_Input As String, pi_RemoveCharacters As List(Of Char)) As String
        Dim result As String = ""
        For Each ch As Char In pi_Input
            If Not pi_RemoveCharacters.Contains(ch) Then
                result &= ch
            End If
        Next
        Return result
    End Function
End Module
