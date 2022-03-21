Namespace Models
	Public Class OCRD
		 Private _CardType As String
		 Private _CntctPrsn As String
		 Private _ECVatGroup As String
		 Private _ShipToDef As String
		 Private _Phone2 As String
		 Private _E_Mail As String
		 Private _Territory As String
		 Private _GroupNum As String
		 Private _Phone1 As String
		 Private _CardCode As String
		 Private _BillToDef As String
		 Private _CardName As String
		 Private _Fax As String
		 Private _SlpCode As String
		 Private _validFor As String
        Private _ListNum As String
        Private _Cellular As String
        Public Property CardType() As String
			 Get 
				 if IsNothing(_CardType) Then
					 Return String.Empty 
				 else 
					 Return _CardType
				 End If 
			 End Get
			 Set(ByVal value As String) 
				_CardType = value
			 End Set
		 End Property
		 Public Property CntctPrsn() As String
			 Get 
				 if IsNothing(_CntctPrsn) Then
					 Return String.Empty 
				 else 
					 Return _CntctPrsn
				 End If 
			 End Get
			 Set(ByVal value As String) 
				_CntctPrsn = value
			 End Set
		 End Property
		 Public Property ECVatGroup() As String
			 Get 
				 if IsNothing(_ECVatGroup) Then
					 Return String.Empty 
				 else 
					 Return _ECVatGroup
				 End If 
			 End Get
			 Set(ByVal value As String) 
				_ECVatGroup = value
			 End Set
		 End Property
		 Public Property ShipToDef() As String
			 Get 
				 if IsNothing(_ShipToDef) Then
					 Return String.Empty 
				 else 
					 Return _ShipToDef
				 End If 
			 End Get
			 Set(ByVal value As String) 
				_ShipToDef = value
			 End Set
		 End Property
		 Public Property Phone2() As String
			 Get 
				 if IsNothing(_Phone2) Then
					 Return String.Empty 
				 else 
					 Return _Phone2
				 End If 
			 End Get
			 Set(ByVal value As String) 
				_Phone2 = value
			 End Set
		 End Property
		 Public Property E_Mail() As String
			 Get 
				 if IsNothing(_E_Mail) Then
					 Return String.Empty 
				 else 
					 Return _E_Mail
				 End If 
			 End Get
			 Set(ByVal value As String) 
				_E_Mail = value
			 End Set
		 End Property
		 Public Property Territory() As String
			 Get 
				 if IsNothing(_Territory) Then
					 Return String.Empty 
				 else 
					 Return _Territory
				 End If 
			 End Get
			 Set(ByVal value As String) 
				_Territory = value
			 End Set
		 End Property
		 Public Property GroupNum() As String
			 Get 
				 if IsNothing(_GroupNum) Then
					 Return String.Empty 
				 else 
					 Return _GroupNum
				 End If 
			 End Get
			 Set(ByVal value As String) 
				_GroupNum = value
			 End Set
		 End Property
		 Public Property Phone1() As String
			 Get 
				 if IsNothing(_Phone1) Then
					 Return String.Empty 
				 else 
					 Return _Phone1
				 End If 
			 End Get
			 Set(ByVal value As String) 
				_Phone1 = value
			 End Set
		 End Property
		 Public Property CardCode() As String
			 Get 
				 if IsNothing(_CardCode) Then
					 Return String.Empty 
				 else 
					 Return _CardCode
				 End If 
			 End Get
			 Set(ByVal value As String) 
				_CardCode = value
			 End Set
		 End Property
		 Public Property BillToDef() As String
			 Get 
				 if IsNothing(_BillToDef) Then
					 Return String.Empty 
				 else 
					 Return _BillToDef
				 End If 
			 End Get
			 Set(ByVal value As String) 
				_BillToDef = value
			 End Set
		 End Property
		 Public Property CardName() As String
			 Get 
				 if IsNothing(_CardName) Then
					 Return String.Empty 
				 else 
					 Return _CardName
				 End If 
			 End Get
			 Set(ByVal value As String) 
				_CardName = value
			 End Set
		 End Property
		 Public Property Fax() As String
			 Get 
				 if IsNothing(_Fax) Then
					 Return String.Empty 
				 else 
					 Return _Fax
				 End If 
			 End Get
			 Set(ByVal value As String) 
				_Fax = value
			 End Set
		 End Property
		 Public Property SlpCode() As String
			 Get 
				 if IsNothing(_SlpCode) Then
					 Return String.Empty 
				 else 
					 Return _SlpCode
				 End If 
			 End Get
			 Set(ByVal value As String) 
				_SlpCode = value
			 End Set
		 End Property
		 Public Property validFor() As String
			 Get 
				 if IsNothing(_validFor) Then
					 Return String.Empty 
				 else 
					 Return _validFor
				 End If 
			 End Get
			 Set(ByVal value As String) 
				_validFor = value
			 End Set
		 End Property
        Public Property ListNum() As String
            Get
                If IsNothing(_ListNum) Then
                    Return String.Empty
                Else
                    Return _ListNum
                End If
            End Get
            Set(ByVal value As String)
                _ListNum = value
            End Set
        End Property
        Public Property Cellular() As String
            Get
                If IsNothing(_Cellular) Then
                    Return String.Empty
                Else
                    Return _Cellular
                End If
            End Get
            Set(ByVal value As String)
                _Cellular = value
            End Set
        End Property

    End Class
End Namespace
