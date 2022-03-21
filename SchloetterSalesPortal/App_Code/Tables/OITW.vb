Public Class OITW
    Private _OnHand As String
    Private _IsCommited As String
    Private _ItemCode As String
    Private _WhsCode As String
    Public Property ItemCode() As String
        Get
            If IsNothing(_ItemCode) Then
                Return Nothing
            Else
                Return _ItemCode
            End If

        End Get
        Set(ByVal value As String)
            _ItemCode = value
        End Set
    End Property
    Public Property WhsCode() As String
        Get
            If IsNothing(_WhsCode) Then
                Return Nothing
            Else
                Return _WhsCode
            End If

        End Get
        Set(ByVal value As String)
            _WhsCode = value
        End Set
    End Property
    Public Property OnHand() As String
        Get
            If IsNothing(_OnHand) Then
                Return Nothing
            Else
                Return _OnHand
            End If

        End Get
        Set(ByVal value As String)
            _OnHand = value
        End Set
    End Property
    Public Property IsCommited() As String
        Get
            If IsNothing(_IsCommited) Then
                Return Nothing
            Else
                Return _IsCommited
            End If

        End Get
        Set(ByVal value As String)
            _IsCommited = value
        End Set
    End Property
End Class
