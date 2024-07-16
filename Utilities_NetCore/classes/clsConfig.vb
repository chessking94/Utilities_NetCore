Imports Microsoft.Extensions.Configuration

Public Class clsConfig
    'TODO: add support for YAML

    Public Property configFile As String
        Get
            Return strl_configFile
        End Get
        Set(value As String)
            strl_configFile = value
        End Set
    End Property

    Private strl_configFile As String
    Private _configuration As IConfiguration = Nothing

    Public Sub buildConfig()
        'TODO: consider validating the existence of strl_configFile
        Dim builder = New ConfigurationBuilder().AddJsonFile(strl_configFile)
        _configuration = builder.Build()
    End Sub

    Public Function getConfig(key As String) As String
        If _configuration Is Nothing Then buildConfig()
        Return _configuration(key)
    End Function
End Class