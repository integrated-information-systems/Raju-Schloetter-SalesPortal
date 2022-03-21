Namespace Models
    Public Class ORDR
        Private _CardCode As String
        Private _DocEntry As String

        Public Property CardCode() As String
            Get
                If IsNothing(_CardCode) Then
                    Return Nothing
                Else
                    Return _CardCode
                End If

            End Get
            Set(ByVal value As String)
                _CardCode = value
            End Set
        End Property
        Public Property DocEntry() As String
            Get
                If IsNothing(_DocEntry) Then
                    Return Nothing
                Else
                    Return _DocEntry
                End If

            End Get
            Set(ByVal value As String)
                _DocEntry = value
            End Set
        End Property
    End Class
End Namespace

