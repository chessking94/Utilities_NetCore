Imports Utilities_NetCore
Imports Microsoft.VisualStudio.TestTools.UnitTesting

Namespace Testing
    <TestClass>
    Public Class Test_modStrings
#Region "RemoveCharacters"
        <TestMethod>
        Public Sub Test_RemoveCharacters_InInput()
            Dim strl_Input As String = "Hello, World!"
            Dim objl_CharactersToRemove = New List(Of Char) From {"e", "o"}
            Dim strl_Output As String = "Hll, Wrld!"

            Assert.AreEqual(modStrings.RemoveCharacters(strl_Input, objl_CharactersToRemove), strl_Output)
        End Sub

        <TestMethod>
        Sub Test_RemoveCharacters_NotInInput()
            Dim strl_Input As String = "Hello, World!"
            Dim objl_CharactersToRemove = New List(Of Char) From {"b", "c"}
            Dim strl_Output As String = "Hello, World!"

            Assert.AreEqual(modStrings.RemoveCharacters(strl_Input, objl_CharactersToRemove), strl_Output)
        End Sub

        <TestMethod>
        Sub Test_RemoveCharacters_None()
            Dim strl_Input As String = "Hello, World!"
            Dim objl_CharactersToRemove = New List(Of Char)
            Dim strl_Output As String = "Hello, World!"

            Assert.AreEqual(modStrings.RemoveCharacters(strl_Input, objl_CharactersToRemove), strl_Output)
        End Sub
#End Region

#Region "AppendText"
        <TestMethod>
        Sub Test_AppendText_WithDelimiter()
            Dim strl_Input As String = "Hello"
            Dim strl_Append As String = "World!"
            Dim strl_Delimiter As String = ", "
            Dim strl_Output As String = "Hello, World!"

            Assert.AreEqual(modStrings.AppendText(strl_Input, strl_Append, strl_Delimiter), strl_Output)
        End Sub

        <TestMethod>
        Sub Test_AppendText_WithoutDelimiter()
            Dim strl_Input As String = "Hello"
            Dim strl_Append As String = "World!"
            Dim strl_Output As String = "Hello" + vbCrLf + "World!"

            Assert.AreEqual(modStrings.AppendText(strl_Input, strl_Append), strl_Output)
        End Sub

        <TestMethod>
        Sub Test_AppendText_EmptyInput()
            Dim strl_Input As String = ""
            Dim strl_Append As String = "World!"
            Dim strl_Output As String = "World!"

            Assert.AreEqual(modStrings.AppendText(strl_Input, strl_Append), strl_Output)
        End Sub

        <TestMethod>
        Sub Test_AppendText_EmptyAppend()
            Dim strl_Input As String = "Hello"
            Dim strl_Append As String = ""
            Dim strl_Output As String = "Hello"

            Assert.AreEqual(modStrings.AppendText(strl_Input, strl_Append), strl_Output)
        End Sub
#End Region
    End Class
End Namespace

