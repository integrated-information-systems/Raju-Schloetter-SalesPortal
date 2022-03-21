Namespace Models
    Public Class OCTG
        Private _GroupNum As String
        Private _PymntGroup As String
        Public Property GroupNum() As String
            Get
                If IsNothing(_GroupNum) Then
                    Return Nothing
                Else
                    Return _GroupNum
                End If

            End Get
            Set(ByVal value As String)
                _GroupNum = value
            End Set
        End Property
        Public Property PymntGroup() As String
            Get
                If IsNothing(_PymntGroup) Then
                    Return Nothing
                Else
                    Return _PymntGroup
                End If

            End Get
            Set(ByVal value As String)
                _PymntGroup = value
            End Set
        End Property
    End Class
End Namespace

