Imports Microsoft.VisualBasic
Namespace Models
    Public Class OSLP
        Private _SlpCode As String
        Private _SlpName As String
        Public Property SlpCode() As String
            Get
                If IsNothing(_SlpCode) Then
                    Return Nothing
                Else
                    Return _SlpCode
                End If

            End Get
            Set(ByVal value As String)
                _SlpCode = value
            End Set
        End Property
        Public Property SlpName() As String
            Get
                If IsNothing(_SlpName) Then
                    Return Nothing
                Else
                    Return _SlpName
                End If

            End Get
            Set(ByVal value As String)
                _SlpName = value
            End Set
        End Property
    End Class
End Namespace

