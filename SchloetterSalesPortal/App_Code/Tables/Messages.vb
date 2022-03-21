Namespace Models
    Public Class Messages
        Private _IdKey As String
        Private _LastUpdateOn As String
        Public Property IdKey() As String
            Get
                Return _IdKey
            End Get
            Set(ByVal value As String)
                If value = String.Empty Then
                    _IdKey = Nothing
                Else
                    _IdKey = value
                End If
            End Set
        End Property
        Public Property MessageTitle() As String
        Public Property Message() As String
        Public Property Active() As Boolean
        Public Property CreatedBy() As String
        Public Property CreatedOn() As String
        Public Property LastUpdateBy() As String
        Public Property LastUpdateOn() As String
            Get
                Return _LastUpdateOn
            End Get
            Set(ByVal value As String)
                _LastUpdateOn = value
            End Set
        End Property
    End Class
End Namespace

