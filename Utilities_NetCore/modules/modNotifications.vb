Imports System.Net.Http

Public Module modNotifications
    Public Function SendTelegramMessage(piMessage As String) As Boolean
        Try
#If DEBUG Then
            Dim apiKey As String = Environment.GetEnvironmentVariable("TelegramAPIKeyDebug")
            Dim chatID As String = Environment.GetEnvironmentVariable("TelegramChatIDDebug")
#Else
            Dim apiKey As String = Environment.GetEnvironmentVariable("TelegramAPIKeyRelease")
            Dim chatID As String = Environment.GetEnvironmentVariable("TelegramChatIDRelease")
#End If

            If apiKey Is Nothing Then
                Console.WriteLine("Missing TelegramAPIKey environment variable")
                Return False
            End If

            If chatID Is Nothing Then
                Console.WriteLine("Missing TelegramChatID environment variable")
                Return False
            End If

            Dim url As String = $"https://api.telegram.org/bot{apiKey}/sendMessage"

            Using client As New HttpClient()
                Dim parameters As New Dictionary(Of String, String) From {
                        {"chat_id", chatID},
                        {"text", piMessage}
                    }

                Dim content As New FormUrlEncodedContent(parameters)
                Dim response As HttpResponseMessage = client.PostAsync(url, content).Result

                Return response.IsSuccessStatusCode
            End Using

        Catch ex As Exception
            Console.WriteLine($"{ex.Message}: {ex.StackTrace}")
            Return False

        End Try
    End Function
End Module
