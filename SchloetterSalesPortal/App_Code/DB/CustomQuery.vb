Imports Microsoft.VisualBasic
Namespace Models
    Public Class CustomQuery
        Private DB As String
        Public Property _InputQuery() As String
        Public Property _Parameters() As Dictionary(Of String, String)
        Public Property _DB() As String
            Get
                Return DB
            End Get
            Set(ByVal value As String)
                DB = value
            End Set
        End Property
    End Class

End Namespace

