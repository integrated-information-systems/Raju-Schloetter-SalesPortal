Namespace Models
    Public Class SOHeader
        Private _IdKey As String
        Private _SubTotalAmt As String
        Private _OrderDiscPrsnt As String
        Private _OrderDiscAmt As String
        Private _AmtAfterDiscount As String
        Private _GSTPrsnt As String
        Private _GSTAmount As String
        Private _TotalSOAmount As String
        Private _SlpCode As String

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
        Public Property PONo() As String
        Public Property ShipTo() As String
        Public Property CustomerCode() As String
        Public Property CustomerName() As String
        Public Property PaymentTerms() As String
        Public Property SAPSONo() As String
        Public Property Status() As String
        Public Property DocDate() As String
        Public Property DeliveryDate() As String
        Public Property Remarks() As String
        Public Property SubTotalAmt() As String
            Get
                Return _SubTotalAmt
            End Get
            Set(ByVal value As String)
                If value = String.Empty Then
                    _SubTotalAmt = Nothing
                Else
                    _SubTotalAmt = value
                End If
            End Set
        End Property
        Public Property OrderDiscPrsnt() As String
            Get
                Return _OrderDiscPrsnt
            End Get
            Set(ByVal value As String)
                If value = String.Empty Then
                    _OrderDiscPrsnt = Nothing
                Else
                    _OrderDiscPrsnt = value
                End If
            End Set
        End Property
        Public Property OrderDiscAmt() As String
            Get
                Return _OrderDiscAmt
            End Get
            Set(ByVal value As String)
                If value = String.Empty Then
                    _OrderDiscAmt = Nothing
                Else
                    _OrderDiscAmt = value
                End If
            End Set
        End Property
        Public Property AmtAfterDiscount() As String
            Get
                Return _AmtAfterDiscount
            End Get
            Set(ByVal value As String)
                If value = String.Empty Then
                    _AmtAfterDiscount = Nothing
                Else
                    _AmtAfterDiscount = value
                End If
            End Set
        End Property
        Public Property GSTPrsnt() As String
            Get
                Return _GSTPrsnt
            End Get
            Set(ByVal value As String)
                If value = String.Empty Then
                    _GSTPrsnt = Nothing
                Else
                    _GSTPrsnt = value
                End If
            End Set
        End Property
        Public Property GSTAmount() As String
            Get
                Return _GSTAmount
            End Get
            Set(ByVal value As String)
                If value = String.Empty Then
                    _GSTAmount = Nothing
                Else
                    _GSTAmount = value
                End If
            End Set
        End Property
        Public Property TotalSOAmount() As String
            Get
                Return _TotalSOAmount
            End Get
            Set(ByVal value As String)
                If value = String.Empty Then
                    _TotalSOAmount = Nothing
                Else
                    _TotalSOAmount = value
                End If
            End Set
        End Property
        Public Property SlpCode() As String
            Get
                Return _SlpCode
            End Get
            Set(ByVal value As String)
                If value = String.Empty Then
                    _SlpCode = Nothing
                Else
                    _SlpCode = value
                End If
            End Set
        End Property

        Public Property SyncRemarks() As String
        Public Property SubmitToSAP() As String
        Public Property CreatedBy() As String
        Public Property CreatedOn() As String
        Public Property LastUpdateBy() As String
        Public Property LastUpdateOn() As String
    End Class
End Namespace

