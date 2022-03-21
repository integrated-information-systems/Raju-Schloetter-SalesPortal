Imports Microsoft.VisualBasic
Namespace Models
    Public Class CRD1
        Private _CardCode As String
        Private _Address As String
        Private _Street As String
        Private _Block As String
        Private _ZipCode As String
        Private _City As String
        Private _County As String
        Private _Country As String
        Private _State As String
        Private _AdresType As String
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
        Public Property Address() As String
            Get
                If IsNothing(_Address) Then
                    Return Nothing
                Else
                    Return _Address
                End If

            End Get
            Set(ByVal value As String)
                _Address = value
            End Set
        End Property
        Public Property Street() As String
            Get
                If IsNothing(_Street) Then
                    Return Nothing
                Else
                    Return _Street
                End If

            End Get
            Set(ByVal value As String)
                _Street = value
            End Set
        End Property
        Public Property Block() As String
            Get
                If IsNothing(_Block) Then
                    Return Nothing
                Else
                    Return _Block
                End If

            End Get
            Set(ByVal value As String)
                _Block = value
            End Set
        End Property
        Public Property ZipCode() As String
            Get
                If IsNothing(_ZipCode) Then
                    Return Nothing
                Else
                    Return _ZipCode
                End If

            End Get
            Set(ByVal value As String)
                _ZipCode = value
            End Set
        End Property
        Public Property City() As String
            Get
                If IsNothing(_City) Then
                    Return Nothing
                Else
                    Return _City
                End If

            End Get
            Set(ByVal value As String)
                _City = value
            End Set
        End Property
        Public Property County() As String
            Get
                If IsNothing(_County) Then
                    Return Nothing
                Else
                    Return _County
                End If

            End Get
            Set(ByVal value As String)
                _County = value
            End Set
        End Property
        Public Property Country() As String
            Get
                If IsNothing(_Country) Then
                    Return Nothing
                Else
                    Return _Country
                End If

            End Get
            Set(ByVal value As String)
                _Country = value
            End Set
        End Property
        Public Property State() As String
            Get
                If IsNothing(_State) Then
                    Return Nothing
                Else
                    Return _State
                End If

            End Get
            Set(ByVal value As String)
                _State = value
            End Set
        End Property
        Public Property AdresType() As String
            Get
                If IsNothing(_AdresType) Then
                    Return Nothing
                Else
                    Return _AdresType
                End If

            End Get
            Set(ByVal value As String)
                _AdresType = value
            End Set
        End Property
    End Class
End Namespace

