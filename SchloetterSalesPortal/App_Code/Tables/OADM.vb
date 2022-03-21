Namespace Models
    Public Class OADM
        Private _DfsVatItem As String
        Private _dfltwhs As String

        Public Property DfsVatItem() As String
            Get
                If IsNothing(_DfsVatItem) Then
                    Return Nothing
                Else
                    Return _DfsVatItem
                End If

            End Get
            Set(ByVal value As String)
                _DfsVatItem = value
            End Set
        End Property
        Public Property dfltwhs() As String
            Get
                If IsNothing(_dfltwhs) Then
                    Return Nothing
                Else
                    Return _dfltwhs
                End If

            End Get
            Set(ByVal value As String)
                _dfltwhs = value
            End Set
        End Property
    End Class
End Namespace
