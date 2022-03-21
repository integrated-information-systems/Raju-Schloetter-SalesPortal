Namespace Models
    Public Class RDR1
        Private _DocEntry As String
        Private _DocDate As String
        Private _ItemCode As String
        Private _price As String
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
        Public Property DocDate() As String
            Get
                If IsNothing(_DocDate) Then
                    Return Nothing
                Else
                    Return _DocDate
                End If

            End Get
            Set(ByVal value As String)
                _DocDate = value
            End Set
        End Property
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
        Public Property price() As String
            Get
                If IsNothing(_price) Then
                    Return Nothing
                Else
                    Return _price
                End If

            End Get
            Set(ByVal value As String)
                _price = value
            End Set
        End Property
    End Class
End Namespace

