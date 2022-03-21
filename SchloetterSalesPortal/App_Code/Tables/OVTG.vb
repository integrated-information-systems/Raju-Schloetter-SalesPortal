Namespace Models
    Public Class OVTG
        Private _Code As String
        Private _Rate As String
        Public Property Code() As String
            Get
                If IsNothing(_Code) Then
                    Return Nothing
                Else
                    Return _Code
                End If

            End Get
            Set(ByVal value As String)
                _Code = value
            End Set
        End Property
        Public Property Rate() As String
            Get
                If IsNothing(_Rate) Then
                    Return Nothing
                Else
                    Return _Rate
                End If

            End Get
            Set(ByVal value As String)
                _Rate = value
            End Set
        End Property
    End Class
End Namespace

