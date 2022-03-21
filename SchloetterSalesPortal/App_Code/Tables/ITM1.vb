Namespace Models
	Public Class ITM1
		 Private _ItemCode As String
		 Private _PriceList As String
		 Private _Price As String
		 Public Property ItemCode() As String
			 Get 
				 if IsNothing(_ItemCode) Then
					 Return String.Empty 
				 else 
					 Return _ItemCode
				 End If 
			 End Get
			 Set(ByVal value As String) 
				_ItemCode = value
			 End Set
		 End Property
		 Public Property PriceList() As String
			 Get 
				 if IsNothing(_PriceList) Then
					 Return String.Empty 
				 else 
					 Return _PriceList
				 End If 
			 End Get
			 Set(ByVal value As String) 
				_PriceList = value
			 End Set
		 End Property
		 Public Property Price() As String
			 Get 
				 if IsNothing(_Price) Then
					 Return String.Empty 
				 else 
					 Return _Price
				 End If 
			 End Get
			 Set(ByVal value As String) 
				_Price = value
			 End Set
		 End Property
	End Class
End Namespace
