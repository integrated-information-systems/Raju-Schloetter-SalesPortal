Imports Microsoft.VisualBasic
Imports System.Data
Imports System.Diagnostics
Imports System.Reflection
Namespace Models
    Public Class AppSpecificFunc
        Public Shared Sub PageControlsToObject(ByVal FooterRow As Control, ByRef Obj As Object)
            Try


                Dim props As Type = Obj.GetType

                For Each member As PropertyInfo In props.GetProperties
                    If Not IsNothing(FooterRow.FindControl(member.Name)) Then
                        Select Case (FooterRow.FindControl(member.Name).GetType())
                            Case GetType(TextBox)
                                Dim TBox As TextBox = TryCast(FooterRow.FindControl(member.Name), TextBox)
                                member.SetValue(Obj, TBox.Text, Nothing)
                            Case GetType(DropDownList)
                                Dim DDL As DropDownList = TryCast(FooterRow.FindControl(member.Name), DropDownList)

                                member.SetValue(Obj, DDL.SelectedValue, Nothing)
                            Case GetType(Label)
                                Dim Lbl As Label = TryCast(FooterRow.FindControl(member.Name), Label)
                                member.SetValue(Obj, Lbl.Text, Nothing)
                            Case GetType(RadioButtonList)
                                Dim RdoList As RadioButtonList = TryCast(FooterRow.FindControl(member.Name), RadioButtonList)
                                If RdoList.SelectedIndex > -1 Then
                                    member.SetValue(Obj, RdoList.SelectedItem.Value, Nothing)
                                End If
                        End Select
                    End If
                Next
            Catch ex As Exception
                AppSpecificFunc.WriteLog(ex)
            End Try
        End Sub
        Public Shared Sub MakeFormReadOnly(ByRef InputForm As Control, ByVal Obj As Object, ByVal SetReadOnly As Boolean)
            Try


                Dim props As Type = Obj.GetType

                For Each member As PropertyInfo In props.GetProperties
                    If Not IsNothing(InputForm.FindControl(member.Name)) Then
                        If SetReadOnly = True Then
                            Select Case (InputForm.FindControl(member.Name).GetType())
                                Case GetType(TextBox)
                                    Dim TBox As TextBox = TryCast(InputForm.FindControl(member.Name), TextBox)
                                    TBox.Enabled = False
                                Case GetType(DropDownList)
                                    Dim DDL As DropDownList = TryCast(InputForm.FindControl(member.Name), DropDownList)
                                    DDL.Enabled = False
                                Case GetType(Label)
                                    Dim Lbl As Label = TryCast(InputForm.FindControl(member.Name), Label)
                                    Lbl.Enabled = False
                                Case GetType(RadioButtonList)
                                    Dim RdoList As RadioButtonList = TryCast(InputForm.FindControl(member.Name), RadioButtonList)
                                    RdoList.Enabled = False
                            End Select
                        Else
                            Select Case (InputForm.FindControl(member.Name).GetType())
                                Case GetType(TextBox)
                                    Dim TBox As TextBox = TryCast(InputForm.FindControl(member.Name), TextBox)
                                    TBox.Enabled = True
                                Case GetType(DropDownList)
                                    Dim DDL As DropDownList = TryCast(InputForm.FindControl(member.Name), DropDownList)
                                    DDL.Enabled = True
                                Case GetType(Label)
                                    Dim Lbl As Label = TryCast(InputForm.FindControl(member.Name), Label)
                                    Lbl.Enabled = True
                                Case GetType(RadioButtonList)
                                    Dim RdoList As RadioButtonList = TryCast(InputForm.FindControl(member.Name), RadioButtonList)
                                    RdoList.Enabled = True
                            End Select
                        End If

                    End If
                Next
            Catch ex As Exception
                AppSpecificFunc.WriteLog(ex)
            End Try
        End Sub
        Public Shared Function GetStatusValue(ByVal InputStatus As String) As Integer
            Dim ReturnResult As Integer = -1
            Try
                InputStatus = InputStatus.Replace(" ", "_")
                InputStatus = "Status_" & InputStatus
                For Each Status As DocStatus In System.Enum.GetValues(GetType(DocStatus))
                    If Status.ToString.IndexOf(InputStatus) > -1 Then
                        ReturnResult = Status
                    End If
                Next
                Return ReturnResult
            Catch ex As Exception
                AppSpecificFunc.WriteLog(ex)
                Return ReturnResult

            End Try

        End Function
        Public Shared Function GetStatusName(ByRef InputValue As DocStatus) As String
            Dim ReturnResult As String = String.Empty
            Try
                ReturnResult = InputValue.ToString.Replace("Status_", "")
                ReturnResult = ReturnResult.Replace("_", " ")
                Return ReturnResult
            Catch ex As Exception
                AppSpecificFunc.WriteLog(ex)
                Return ReturnResult
            End Try
        End Function
        Public Shared Sub ObjectToPageControls(ByRef FooterRow As Control, ByVal Obj As Object)
            Try


                Dim props As Type = Obj.GetType

                For Each member As PropertyInfo In props.GetProperties
                    If Not IsNothing(FooterRow.FindControl(member.Name)) Then
                        If Not IsNothing(member.GetValue(Obj, Nothing)) Then
                            Select Case (FooterRow.FindControl(member.Name).GetType())
                                Case GetType(TextBox)
                                    Dim TBox As TextBox = TryCast(FooterRow.FindControl(member.Name), TextBox)
                                    TBox.Text = member.GetValue(Obj, Nothing).ToString
                                Case GetType(DropDownList)
                                    Dim DDL As DropDownList = TryCast(FooterRow.FindControl(member.Name), DropDownList)
                                    DDL.SelectedIndex = DDL.Items.IndexOf(DDL.Items.FindByValue(member.GetValue(Obj, Nothing).ToString))
                                Case GetType(Label)
                                    Dim Lbl As Label = TryCast(FooterRow.FindControl(member.Name), Label)
                                    Lbl.Text = member.GetValue(Obj, Nothing).ToString
                                Case GetType(RadioButtonList)
                                    Dim RdoList As RadioButtonList = TryCast(FooterRow.FindControl(member.Name), RadioButtonList)
                                    RdoList.SelectedIndex = RdoList.Items.IndexOf(RdoList.Items.FindByValue(member.GetValue(Obj, Nothing).ToString))
                            End Select
                        Else
                            Select Case (FooterRow.FindControl(member.Name).GetType())
                                Case GetType(TextBox)
                                    Dim TBox As TextBox = TryCast(FooterRow.FindControl(member.Name), TextBox)
                                    TBox.Text = String.Empty
                                Case GetType(DropDownList)
                                    Dim DDL As DropDownList = TryCast(FooterRow.FindControl(member.Name), DropDownList)
                                    DDL.SelectedIndex = 0
                                Case GetType(Label)
                                    Dim Lbl As Label = TryCast(FooterRow.FindControl(member.Name), Label)
                                    Lbl.Text = String.Empty
                                Case GetType(RadioButtonList)
                                    Dim RdoList As RadioButtonList = TryCast(FooterRow.FindControl(member.Name), RadioButtonList)
                                    RdoList.SelectedIndex = -1
                            End Select
                        End If
                    End If
                Next
            Catch ex As Exception
                AppSpecificFunc.WriteLog(ex)
            End Try
        End Sub
        Public Shared Function GetLastPurchasePrice(ByRef ItemCode As String) As Decimal
            Try
                Dim ReturnResult As Decimal = 0
                Dim ResultDataTable As New DataTable
                '**********************Query Builder Function *****************
                Dim ItemTable As New OITM

                'Filter Values


                ItemTable.ItemCode = ItemCode

                ItemTable.LastPurPrc = "?"


                'Query Conditions List
                Dim QryConditions As Dictionary(Of String, List(Of String)) = New Dictionary(Of String, List(Of String))

                'Query Condition Groups
                Dim ConditionsGrp1 As List(Of String) = New List(Of String)

                'Query Conditions values
                
                ConditionsGrp1.Add("ItemCode=@ItemCode")
                 

                QryConditions.Add(" AND ", ConditionsGrp1)



                ' INPUT FOR Query Builder
                Dim SQB As New SelectQuery
                SQB._InputTable = ItemTable
                SQB._DB = "SAP"
                SQB._HasInBetweenConditions = False
                SQB._HasWhereConditions = True
                SQB._Conditions = QryConditions

                ResultDataTable = CURD.SelectSpecificFieldsData(SQB)
                '**********************Query Builder Function *****************
                If ResultDataTable.Rows.Count > 0 Then
                    Decimal.TryParse(ResultDataTable.Rows(0).Item(0).ToString, ReturnResult)
                    ReturnResult = ReturnResult.ToString("F2")
                End If
                Return ReturnResult
            Catch ex As Exception
                AppSpecificFunc.WriteLog(ex)
                Return 0
            End Try
        End Function
        Public Shared Function GetAllSalesPersons() As DataTable
            Try
                Dim ResultDataTable As New DataTable
                Dim SalesPersonsTable As New OSLP
                SalesPersonsTable.SlpCode = "-1"

                'Query Conditions List
                Dim QryConditions As Dictionary(Of String, List(Of String)) = New Dictionary(Of String, List(Of String))

                'Query Condition Groups
                Dim ConditionsGrp1 As List(Of String) = New List(Of String)

                ConditionsGrp1.Add("SlpCode<>@SlpCode")

                QryConditions.Add(" AND ", ConditionsGrp1)

                Dim SQB As New SelectQuery
                SQB._InputTable = SalesPersonsTable
                SQB._DB = "SAP"
                SQB._HasInBetweenConditions = False
                SQB._HasWhereConditions = True
                SQB._Conditions = QryConditions
                SQB._OrderBy = " SlpCode ASC"
                ResultDataTable = CURD.SelectAllData(SQB)

                Return ResultDataTable
            Catch ex As Exception
                AppSpecificFunc.WriteLog(ex)
                Return Nothing
            End Try
        End Function
        Public Shared Function GetAllDefaultCustomerAddress(ByVal AddressCode As String, ByVal CustomerCode As String, Optional ByVal AddressType As String = "") As String
            Try

                Dim ReturnAddress As String = String.Empty
                Dim ResultDataTable As New DataTable
                '**********************Query Builder Function *****************
                Dim CustomerAddressTable As New CRD1

                'Filter Values
                If AddressType <> String.Empty Then
                    If AddressType = "Shipping" Then
                        CustomerAddressTable.AdresType = "S"
                    ElseIf AddressType = "Billing" Then
                        CustomerAddressTable.AdresType = "B"
                    End If
                End If


                CustomerAddressTable.CardCode = CustomerCode
                CustomerAddressTable.Address = AddressCode

                'Query Conditions List
                Dim QryConditions As Dictionary(Of String, List(Of String)) = New Dictionary(Of String, List(Of String))

                'Query Condition Groups
                Dim ConditionsGrp1 As List(Of String) = New List(Of String)

                'Query Conditions values
                If AddressType <> String.Empty Then
                    ConditionsGrp1.Add("AdresType=@AdresType")
                End If


                ConditionsGrp1.Add("CardCode=@CardCode")
                ConditionsGrp1.Add("Address=@Address")

                QryConditions.Add(" AND ", ConditionsGrp1)



                ' INPUT FOR Query Builder
                Dim SQB As New SelectQuery
                SQB._InputTable = CustomerAddressTable
                SQB._DB = "SAP"
                SQB._HasInBetweenConditions = False
                SQB._HasWhereConditions = True
                SQB._Conditions = QryConditions

                ResultDataTable = CURD.SelectAllData(SQB)
                '**********************Query Builder Function *****************
                If ResultDataTable.Rows.Count > 0 Then
                    Dim AddressObj As New CRD1
                    CommonFunc.DataTableToObject(AddressObj, ResultDataTable)
                    If Not IsNothing(AddressObj) Then


                        
                        If Not IsNothing(AddressObj.Street) Then
                            If AddressObj.Street.ToString <> String.Empty Then
                                'ReturnAddress = ReturnAddress & AddressObj.Street & "," & vbCrLf
                                ReturnAddress = ReturnAddress & AddressObj.Street & vbCrLf
                            End If
                        End If

                        If Not IsNothing(AddressObj.Block) Then
                            If AddressObj.Block.ToString <> String.Empty Then
                                'ReturnAddress = ReturnAddress & AddressObj.Block & "," & vbCrLf
                                ReturnAddress = ReturnAddress & AddressObj.Block & vbCrLf
                            End If
                        End If

                        If Not IsNothing(AddressObj.City) Then
                            If AddressObj.City.ToString <> String.Empty Then
                                'ReturnAddress = ReturnAddress & AddressObj.City & "," & vbCrLf
                                ReturnAddress = ReturnAddress & AddressObj.City & vbCrLf
                            End If
                        End If
                        If Not IsNothing(AddressObj.County) Then
                            If AddressObj.County.ToString <> String.Empty Then
                                'ReturnAddress = ReturnAddress & AddressObj.County & "," & vbCrLf
                                ReturnAddress = ReturnAddress & AddressObj.County & vbCrLf
                            End If
                        End If
                        'If Not IsNothing(AddressObj.Country) Then
                        '    If AddressObj.Country.ToString <> String.Empty Then
                        '        ReturnAddress = ReturnAddress & AddressObj.Country & "," & vbCrLf
                        '    End If
                        'End If
                        'ReturnAddress = ReturnAddress & AddressObj.Block & "," & vbCrLf
                        'ReturnAddress = ReturnAddress & AddressObj.Street & "," & vbCrLf
                        'ReturnAddress = ReturnAddress & AddressObj.ZipCode & "," & vbCrLf
                        'ReturnAddress = ReturnAddress & AddressObj.County & "," & vbCrLf
                        'ReturnAddress = ReturnAddress & AddressObj.Country & "," & vbCrLf


                    End If
                End If
                Return ReturnAddress
            Catch ex As Exception
                AppSpecificFunc.WriteLog(ex)
                Return String.Empty
            End Try
        End Function
        Public Shared Function GetCustomerHeaderInfo(ByVal Type As String, ByVal CardCodeORCardName As String) As Object
            Try
                Dim ReturnResult As New OCRD
                Dim ResultDataTable As New DataTable
                '**********************Query Builder Function *****************
                Dim CustomerTable As New OCRD



                If Type = "DetailsByName" Then
                    CustomerTable.CardName = CardCodeORCardName
                ElseIf Type = "DetailsByCode" Then
                    CustomerTable.CardCode = CardCodeORCardName
                End If

                CustomerTable.CardType = "C"

                'Query Conditions List
                Dim QryConditions As Dictionary(Of String, List(Of String)) = New Dictionary(Of String, List(Of String))

                'Query Condition Groups
                Dim ConditionsGrp1 As List(Of String) = New List(Of String)

                'Query Conditions values

                If Type = "DetailsByCode" Then
                    ConditionsGrp1.Add("CardCode=@CardCode")
                ElseIf Type = "DetailsByName" Then
                    ConditionsGrp1.Add("CardName=@CardName")
                End If

                ConditionsGrp1.Add("CardType=@CardType")

                QryConditions.Add(" AND ", ConditionsGrp1)



                ' INPUT FOR Query Builder
                Dim SQB As New SelectQuery
                SQB._InputTable = CustomerTable
                SQB._DB = "SAP"
                SQB._HasInBetweenConditions = False
                SQB._HasWhereConditions = True
                SQB._Conditions = QryConditions

                ResultDataTable = CURD.SelectAllData(SQB)
                '**********************Query Builder Function *****************
                If ResultDataTable.Rows.Count > 0 Then
                    DataTableToObject(ReturnResult, ResultDataTable)
                End If

                Return ReturnResult
            Catch ex As Exception
                AppSpecificFunc.WriteLog(ex)
                Return Nothing
            End Try
        End Function
        Public Shared Function GetActiveCustomersAutoComplete(ByVal Type As String, ByVal prefix As String, ByVal SalesPersons() As String) As DataTable
            Try


                Dim ResultDataTable As New DataTable
                '**********************Query Builder Function *****************
                Dim CustomerTable() As OCRD = Nothing
                ReDim Preserve CustomerTable(0)
                'Filter Values
                CustomerTable(0) = New OCRD
                CustomerTable(0).ValidFor = "Y"

                If SalesPersons.Count > 0 Then                    
                    CustomerTable(0).SlpCode = SalesPersons(0)
                End If

                If Type = "CardCode" Then
                    CustomerTable(0).CardCode = prefix
                ElseIf Type = "CardName" Then
                    CustomerTable(0).CardName = prefix
                End If

                CustomerTable(0).CardType = "C"

                'Query Conditions List
                Dim QryConditions As Dictionary(Of String, List(Of String)) = New Dictionary(Of String, List(Of String))

                'Query Condition Groups
                Dim ConditionsGrp1 As List(Of String) = New List(Of String)

                'Query Conditions values
                ConditionsGrp1.Add("ValidFor=@ValidFor")

                If Type = "CardCode" Then
                    ConditionsGrp1.Add("CardCode LIKE '%'+ @CardCode + '%' ")
                ElseIf Type = "CardName" Then
                    ConditionsGrp1.Add("CardName LIKE '%'+ @CardName + '%' ")
                End If
                If SalesPersons.Count > 0 Then
                    If SalesPersons.Count > 1 Then
                        Dim ConditionString As New List(Of String)

                        For i = 0 To SalesPersons.Count - 1
                            If i = 0 Then
                                ConditionString.Add("@SlpCode")
                            Else
                                ReDim Preserve CustomerTable(i)

                                ConditionString.Add("@SlpCode" & i)
                                CustomerTable(i) = New OCRD
                                CustomerTable(i).SlpCode = SalesPersons(i)
                            End If
                        Next
                        ConditionsGrp1.Add("SlpCode IN (" & String.Join(",", ConditionString.ToArray) & ")")
                    Else
                        ConditionsGrp1.Add("SlpCode IN  (@SlpCode)")
                    End If
 
                End If
                ConditionsGrp1.Add("CardType=@CardType")

                QryConditions.Add(" AND ", ConditionsGrp1)



                ' INPUT FOR Query Builder
                Dim SQB As New ComplexSelectQuery
                SQB._InputTable = CustomerTable
                SQB._DB = "SAP"
                SQB._HasInBetweenConditions = False
                SQB._HasWhereConditions = True
                SQB._Conditions = QryConditions

                ResultDataTable = CURD.SelectAllDataComplexCondition(SQB)
                '**********************Query Builder Function *****************
                Return ResultDataTable
            Catch ex As Exception
                WriteLog(ex)
                Return New DataTable
            End Try
        End Function
        Public Shared Function GetTaxRateByCustomerTaxInfo(ByVal TaxCode As String) As Decimal
            Try
                If TaxCode <> String.Empty Then
                    Return GetTaxRateByCode(TaxCode)
                Else
                    TaxCode = GetSAPDefaultTaxCode()
                    Return GetTaxRateByCode(TaxCode)
                End If
            Catch ex As Exception
                AppSpecificFunc.WriteLog(ex)
                Return 0
            End Try
        End Function
        Public Shared Function GetSAPDefaultTaxCode() As String
            Try
                Dim Result As String = String.Empty
                Dim ResultDataTable As New DataTable
                '**********************Query Builder Function *****************
                Dim SAPMasterTable As New OADM

                'Filter Values
 
                SAPMasterTable.DfsVatItem = "?"

                ' INPUT FOR Query Builder
                Dim SQB As New SelectQuery
                SQB._InputTable = SAPMasterTable
                SQB._DB = "SAP"
                SQB._HasInBetweenConditions = False
                SQB._HasWhereConditions = False


                ResultDataTable = CURD.SelectSpecificFieldsData(SQB)
                '**********************Query Builder Function *****************
                If ResultDataTable.Rows.Count > 0 Then
                    Result = ResultDataTable.Rows(0).Item(0).ToString
                End If
                Return Result
            Catch ex As Exception
                AppSpecificFunc.WriteLog(ex)
                Return String.Empty
            End Try
        End Function
        Public Shared Function GetTaxRateByCode(ByVal TaxCode As String) As Decimal
            Try

                Dim ReturnResult As Decimal = 0
                Dim ResultDataTable As New DataTable
                '**********************Query Builder Function *****************
                Dim TaxRateTable As New OVTG

                'Filter Values


                TaxRateTable.Code = TaxCode

                TaxRateTable.Rate = "?"


                'Query Conditions List
                Dim QryConditions As Dictionary(Of String, List(Of String)) = New Dictionary(Of String, List(Of String))

                'Query Condition Groups
                Dim ConditionsGrp1 As List(Of String) = New List(Of String)

                'Query Conditions values

                ConditionsGrp1.Add("Code=@Code")


                QryConditions.Add(" AND ", ConditionsGrp1)



                ' INPUT FOR Query Builder
                Dim SQB As New SelectQuery
                SQB._InputTable = TaxRateTable
                SQB._DB = "SAP"
                SQB._HasInBetweenConditions = False
                SQB._HasWhereConditions = True
                SQB._Conditions = QryConditions

                ResultDataTable = CURD.SelectSpecificFieldsData(SQB)
                '**********************Query Builder Function *****************
                If ResultDataTable.Rows.Count > 0 Then
                    Decimal.TryParse(ResultDataTable.Rows(0).Item(0).ToString, ReturnResult)
                    ReturnResult = ReturnResult.ToString("F2")
                End If
                Return ReturnResult
            Catch ex As Exception
                AppSpecificFunc.WriteLog(ex)
                Return 0
            End Try
        End Function
        Public Shared Function GetActiveCustomerByCodeORName(ByVal Type As String, ByVal CardCodeORCardName As String, ByVal SalesPersons() As String) As Object
            Try
                Dim Result As Object = Nothing
                Dim ResultDataTable As New DataTable
                '**********************Query Builder Function *****************
                Dim CustomerTable() As OCRD = Nothing
                ReDim Preserve CustomerTable(0)
                'Filter Values
                CustomerTable(0) = New OCRD
                CustomerTable(0).ValidFor = "Y"

                If SalesPersons.Count > 0 Then
                    CustomerTable(0).SlpCode = SalesPersons(0)
                End If


                If Type = "CodeByName" Then
                    CustomerTable(0).CardName = CardCodeORCardName
                    'CustomerTable.CardCode = "?"
                ElseIf Type = "NameByCode" Then
                    CustomerTable(0).CardCode = CardCodeORCardName
                    'CustomerTable.CardName = "?"
                End If

                CustomerTable(0).CardType = "C"

                'Query Conditions List
                Dim QryConditions As Dictionary(Of String, List(Of String)) = New Dictionary(Of String, List(Of String))

                'Query Condition Groups
                Dim ConditionsGrp1 As List(Of String) = New List(Of String)

                'Query Conditions values
                ConditionsGrp1.Add("ValidFor=@ValidFor")
                If Type = "NameByCode" Then
                    ConditionsGrp1.Add("CardCode=@CardCode")
                ElseIf Type = "CodeByName" Then
                    ConditionsGrp1.Add("CardName=@CardName")
                End If
                ConditionsGrp1.Add("CardType=@CardType")

                If SalesPersons.Count > 0 Then
                    If SalesPersons.Count > 1 Then
                        Dim ConditionString As New List(Of String)

                        For i = 0 To SalesPersons.Count - 1
                            If i = 0 Then
                                ConditionString.Add("@SlpCode")
                            Else
                                ReDim Preserve CustomerTable(i)

                                ConditionString.Add("@SlpCode" & i)
                                CustomerTable(i) = New OCRD
                                CustomerTable(i).SlpCode = SalesPersons(i)
                            End If
                        Next
                        ConditionsGrp1.Add("SlpCode IN (" & String.Join(",", ConditionString.ToArray) & ")")
                    Else
                        ConditionsGrp1.Add("SlpCode IN  (@SlpCode)")
                    End If

                End If
                ConditionsGrp1.Add("CardType=@CardType")

                QryConditions.Add(" AND ", ConditionsGrp1)



                ' INPUT FOR Query Builder
                Dim SQB As New ComplexSelectQuery
                SQB._InputTable = CustomerTable
                SQB._DB = "SAP"
                SQB._HasInBetweenConditions = False
                SQB._HasWhereConditions = True
                SQB._Conditions = QryConditions

                'ResultDataTable = CURD.SelectSpecificFieldsData(SQB)
                ResultDataTable = CURD.SelectAllDataComplexCondition(SQB)
                '**********************Query Builder Function *****************
                Dim ResultCustomerTable As New OCRD
                If ResultDataTable.Rows.Count > 0 Then
                    AppSpecificFunc.DataTableToObject(ResultCustomerTable, ResultDataTable)
                    Result = ResultCustomerTable

                End If
                Return Result
            Catch ex As Exception
                WriteLog(ex)
                Return Nothing
            End Try
        End Function
        Public Shared Function GetItemByCodeORName(ByVal Type As String, ByVal ItemCodeORItemName As String) As String
            Try
                Dim Result As String = String.Empty
                Dim ResultDataTable As New DataTable
                '**********************Query Builder Function *****************
                Dim ItemTable As New OITM

                'Filter Values

                If Type = "CodeByName" Then
                    ItemTable.ItemName = ItemCodeORItemName
                    ItemTable.ItemCode = "?"
                ElseIf Type = "NameByCode" Then
                    ItemTable.ItemCode = ItemCodeORItemName
                    ItemTable.ItemName = "?"
                End If



                'Query Conditions List
                Dim QryConditions As Dictionary(Of String, List(Of String)) = New Dictionary(Of String, List(Of String))

                'Query Condition Groups
                Dim ConditionsGrp1 As List(Of String) = New List(Of String)

                'Query Conditions values

                If Type = "NameByCode" Then
                    ConditionsGrp1.Add("ItemCode=@ItemCode")
                ElseIf Type = "CodeByName" Then
                    ConditionsGrp1.Add("ItemName=@ItemName")
                End If

                QryConditions.Add(" AND ", ConditionsGrp1)



                ' INPUT FOR Query Builder
                Dim SQB As New SelectQuery
                SQB._InputTable = ItemTable
                SQB._DB = "SAP"
                SQB._HasInBetweenConditions = False
                SQB._HasWhereConditions = True
                SQB._Conditions = QryConditions

                ResultDataTable = CURD.SelectSpecificFieldsData(SQB)
                '**********************Query Builder Function *****************
                If ResultDataTable.Rows.Count > 0 Then
                    Result = ResultDataTable.Rows(0).Item(0).ToString
                End If
                Return Result
            Catch ex As Exception
                WriteLog(ex)
                Return String.Empty
            End Try
        End Function
        Public Shared Function GetActiveItemByCodeORName(ByVal Type As String, ByVal CardCodeORCardName As String) As Object
            Try
                Dim Result As Object = Nothing
                Dim ResultDataTable As New DataTable
                '**********************Query Builder Function *****************
                Dim ItemTable As New OITM

                'Filter Values
                ItemTable.frozenFor = "N"

                If Type = "CodeByName" Then
                    ItemTable.ItemName = CardCodeORCardName
                    'ItemTable.ItemCode = "?"
                ElseIf Type = "NameByCode" Then
                    ItemTable.ItemCode = CardCodeORCardName
                    'ItemTable.ItemName = "?"
                End If

                'ItemTable.InvntItem = "Y"

                'Query Conditions List
                Dim QryConditions As Dictionary(Of String, List(Of String)) = New Dictionary(Of String, List(Of String))

                'Query Condition Groups
                Dim ConditionsGrp1 As List(Of String) = New List(Of String)

                'Query Conditions values
                ConditionsGrp1.Add("frozenFor=@frozenFor")
                If Type = "NameByCode" Then
                    ConditionsGrp1.Add("ItemCode=@ItemCode")
                ElseIf Type = "CodeByName" Then
                    ConditionsGrp1.Add("ItemName=@ItemName")
                End If
                'ConditionsGrp1.Add("InvntItem=@InvntItem")
                QryConditions.Add(" AND ", ConditionsGrp1)



                ' INPUT FOR Query Builder
                Dim SQB As New SelectQuery
                SQB._InputTable = ItemTable
                SQB._DB = "SAP"
                SQB._HasInBetweenConditions = False
                SQB._HasWhereConditions = True
                SQB._Conditions = QryConditions

                'ResultDataTable = CURD.SelectSpecificFieldsData(SQB)
                ResultDataTable = CURD.SelectAllData(SQB)
                '**********************Query Builder Function *****************
                If ResultDataTable.Rows.Count > 0 Then
                    AppSpecificFunc.DataTableToObject(ItemTable, ResultDataTable)
                    Result = ItemTable
                End If
                Return Result
            Catch ex As Exception
                WriteLog(ex)
                Return Nothing
            End Try
        End Function
        Public Shared Function GetActiveInventoryItemsAutoComplete(ByVal Type As String, ByVal prefix As String) As DataTable
            Try


                Dim ResultDataTable As New DataTable
                '**********************Query Builder Function *****************
                Dim ItemTable As New OITM

                'Filter Values
                ItemTable.frozenFor = "N"

                If Type = "ItemCode" Then
                    ItemTable.ItemCode = prefix
                ElseIf Type = "ItemName" Then
                    ItemTable.ItemName = prefix
                End If

                ' ItemTable.InvntItem = "Y"

                'Query Conditions List
                Dim QryConditions As Dictionary(Of String, List(Of String)) = New Dictionary(Of String, List(Of String))

                'Query Condition Groups
                Dim ConditionsGrp1 As List(Of String) = New List(Of String)

                'Query Conditions values
                ConditionsGrp1.Add("frozenFor=@frozenFor")
                If Type = "ItemCode" Then
                    ConditionsGrp1.Add("ItemCode LIKE '%'+ @ItemCode + '%' ")
                ElseIf Type = "ItemName" Then
                    ConditionsGrp1.Add("ItemName LIKE '%'+ @ItemName + '%' ")
                End If
                'ConditionsGrp1.Add("InvntItem=@InvntItem")
                QryConditions.Add(" AND ", ConditionsGrp1)



                ' INPUT FOR Query Builder
                Dim SQB As New SelectQuery
                SQB._InputTable = ItemTable
                SQB._DB = "SAP"
                SQB._HasInBetweenConditions = False
                SQB._HasWhereConditions = True
                SQB._Conditions = QryConditions

                ResultDataTable = CURD.SelectAllData(SQB)
                '**********************Query Builder Function *****************
                Return ResultDataTable
            Catch ex As Exception
                WriteLog(ex)
                Return New DataTable
            End Try
        End Function

        Public Shared Function GetActiveInventoryItemCodesBetween(ByVal FromItemCode As String, ByVal ToItemCode As String) As DataTable
            Try


                Dim ResultDataTable As New DataTable
                '**********************Query Builder Function *****************

                Dim CQ As New CustomQuery
                CQ._DB = "SAP"
                Dim CustomQueryParameters As New Dictionary(Of String, String)
                Dim Conditionlist1 As New List(Of String)

                Dim InputQuery1 As String = "SELECT * FROM OITM"

                Conditionlist1.Add(" frozenFor=@frozenFor  ")
                CustomQueryParameters.Add("@frozenFor", "N")

                'Conditionlist1.Add(" InvntItem=@InvntItem ")
                'CustomQueryParameters.Add("@InvntItem", "Y")

                If Not FromItemCode.Equals(String.Empty) And Not ToItemCode.Equals(String.Empty) Then
                    Conditionlist1.Add(" (ItemCode>=@FromItemCode AND ItemCode<=@ToItemCode) ")
                    CustomQueryParameters.Add("@FromItemCode", FromItemCode)
                    CustomQueryParameters.Add("@ToItemCode", ToItemCode)
                ElseIf Not FromItemCode.Equals(String.Empty) Then
                    Conditionlist1.Add(" ItemCode>=@FromItemCode  ")
                    CustomQueryParameters.Add("@FromItemCode", FromItemCode)
                ElseIf Not ToItemCode.Equals(String.Empty) Then
                    Conditionlist1.Add(" ItemCode<=@ToItemCode ")
                    CustomQueryParameters.Add("@ToItemCode", ToItemCode)
                End If

                If Conditionlist1.Count > 0 Then
                    Dim CondiString1 As String = String.Join(" AND ", Conditionlist1)
                    InputQuery1 = InputQuery1 & " WHERE " & CondiString1
                End If
                CQ._InputQuery = InputQuery1 & " ORDER BY ItemCode Desc"
                CQ._Parameters = CustomQueryParameters

                ResultDataTable = CURD.CustomQueryGetData(CQ)

                '**********************Query Builder Function *****************
                Return ResultDataTable
            Catch ex As Exception
                WriteLog(ex)
                Return New DataTable
            End Try
        End Function
        Public Shared Function GetActiveDocumentNosAutoComplete(ByVal InputObj As Object, ByVal prefix As String, ByVal SalesPersons() As String, Optional ByVal CreatedBy As String = "") As DataTable
            Try


                Dim ResultDataTable As New DataTable
                '**********************Query Builder Function *****************
                Dim MasterObj() As Object = Nothing
                ReDim Preserve MasterObj(0)
                'Filter Values

                MasterObj(0) = Activator.CreateInstance(InputObj.GetType)

                If SalesPersons.Count > 0 Then
                    MasterObj(0).SlpCode = SalesPersons(0)
                End If

                'Filter Values
                If CreatedBy <> String.Empty Then
                    MasterObj(0).CreatedBy = CreatedBy
                End If

                MasterObj(0).IdKey = prefix

                'Query Conditions List
                Dim QryConditions As Dictionary(Of String, List(Of String)) = New Dictionary(Of String, List(Of String))

                'Query Condition Groups
                Dim ConditionsGrp1 As List(Of String) = New List(Of String)

                'Query Conditions values

                If CreatedBy <> String.Empty Then
                    ConditionsGrp1.Add("CreatedBy=@CreatedBy")
                End If

                ConditionsGrp1.Add("IdKey LIKE '%'+ @IdKey + '%' ")

                If SalesPersons.Count > 0 Then
                    If SalesPersons.Count > 1 Then
                        Dim ConditionString As New List(Of String)

                        For i = 0 To SalesPersons.Count - 1
                            If i = 0 Then
                                ConditionString.Add("@SlpCode")
                            Else
                                ReDim Preserve MasterObj(i)

                                ConditionString.Add("@SlpCode" & i)
                                MasterObj(i) = Activator.CreateInstance(InputObj.GetType)
                                MasterObj(i).SlpCode = SalesPersons(i)
                            End If
                        Next
                        ConditionsGrp1.Add("SlpCode IN (" & String.Join(",", ConditionString.ToArray) & ")")
                    Else
                        ConditionsGrp1.Add("SlpCode IN  (@SlpCode)")
                    End If

                End If

                QryConditions.Add(" AND ", ConditionsGrp1)



                ' INPUT FOR Query Builder
                Dim SQB As New ComplexSelectQuery
                SQB._InputTable = MasterObj
                SQB._DB = "Custom"
                SQB._HasInBetweenConditions = False
                SQB._HasWhereConditions = True
                SQB._Conditions = QryConditions

                ResultDataTable = CURD.SelectAllDataComplexCondition(SQB)
                '**********************Query Builder Function *****************
                Return ResultDataTable
            Catch ex As Exception
                WriteLog(ex)
                Return New DataTable
            End Try
        End Function
        Public Shared Function GetSAPDocumentNosAutoComplete(ByVal InputObj As Object, ByVal prefix As String, ByVal SalesPersons() As String, Optional ByVal CreatedBy As String = "") As DataTable
            Try


                Dim ResultDataTable As New DataTable
                '**********************Query Builder Function *****************
                Dim MasterObj() As Object = Nothing
                ReDim Preserve MasterObj(0)
                'Filter Values

                MasterObj(0) = Activator.CreateInstance(InputObj.GetType)

                If SalesPersons.Count > 0 Then
                    MasterObj(0).SlpCode = SalesPersons(0)
                End If

                'Filter Values
                If CreatedBy <> String.Empty Then
                    MasterObj(0).CreatedBy = CreatedBy
                End If

                MasterObj(0).SAPSONo = prefix

                'Query Conditions List
                Dim QryConditions As Dictionary(Of String, List(Of String)) = New Dictionary(Of String, List(Of String))

                'Query Condition Groups
                Dim ConditionsGrp1 As List(Of String) = New List(Of String)

                'Query Conditions values

                If CreatedBy <> String.Empty Then
                    ConditionsGrp1.Add("CreatedBy=@CreatedBy")
                End If

                ConditionsGrp1.Add("SAPSONo LIKE '%'+ @SAPSONo + '%' ")

                If SalesPersons.Count > 0 Then
                    If SalesPersons.Count > 1 Then
                        Dim ConditionString As New List(Of String)

                        For i = 0 To SalesPersons.Count - 1
                            If i = 0 Then
                                ConditionString.Add("@SlpCode")
                            Else
                                ReDim Preserve MasterObj(i)

                                ConditionString.Add("@SlpCode" & i)
                                MasterObj(i) = Activator.CreateInstance(InputObj.GetType)
                                MasterObj(i).SlpCode = SalesPersons(i)
                            End If
                        Next
                        ConditionsGrp1.Add("SlpCode IN (" & String.Join(",", ConditionString.ToArray) & ")")
                    Else
                        ConditionsGrp1.Add("SlpCode IN  (@SlpCode)")
                    End If

                End If


                QryConditions.Add(" AND ", ConditionsGrp1)



                ' INPUT FOR Query Builder
                Dim SQB As New ComplexSelectQuery
                SQB._InputTable = MasterObj
                SQB._DB = "Custom"
                SQB._HasInBetweenConditions = False
                SQB._HasWhereConditions = True
                SQB._Conditions = QryConditions

                ResultDataTable = CURD.SelectAllDataComplexCondition(SQB)
                '**********************Query Builder Function *****************
                Return ResultDataTable
            Catch ex As Exception
                WriteLog(ex)
                Return New DataTable
            End Try
        End Function
        Public Shared Function UserCanAccessDocumentNo(ByVal InputObj As Object, ByVal DocumentNo As String, ByVal SalesPersons() As String, ByVal DocNoType As String, Optional ByVal CreatedBy As String = "") As Boolean
            Try

                Dim Result As Boolean = False
                Dim ResultDataTable As New DataTable
                '**********************Query Builder Function *****************
                Dim MasterObj() As Object = Nothing
                ReDim Preserve MasterObj(0)
                'Filter Values

                MasterObj(0) = Activator.CreateInstance(InputObj.GetType)

                If SalesPersons.Count > 0 Then
                    MasterObj(0).SlpCode = SalesPersons(0)
                End If

                'Filter Values
                If CreatedBy <> String.Empty Then
                    MasterObj(0).CreatedBy = CreatedBy
                End If

                If DocNoType = "SAP" Then
                    MasterObj(0).SAPSONo = DocumentNo
                Else
                    MasterObj(0).IdKey = DocumentNo
                End If


                'Query Conditions List
                Dim QryConditions As Dictionary(Of String, List(Of String)) = New Dictionary(Of String, List(Of String))

                'Query Condition Groups
                Dim ConditionsGrp1 As List(Of String) = New List(Of String)

                'Query Conditions values

                If CreatedBy <> String.Empty Then
                    ConditionsGrp1.Add("CreatedBy=@CreatedBy")
                End If

                If DocNoType = "SAP" Then
                    ConditionsGrp1.Add("SAPSONo=@SAPSONo")
                Else
                    ConditionsGrp1.Add("IdKey=@IdKey")                    
                End If


                If SalesPersons.Count > 0 Then
                    If SalesPersons.Count > 1 Then
                        Dim ConditionString As New List(Of String)

                        For i = 0 To SalesPersons.Count - 1
                            If i = 0 Then
                                ConditionString.Add("@SlpCode")
                            Else
                                ReDim Preserve MasterObj(i)

                                ConditionString.Add("@SlpCode" & i)
                                MasterObj(i) = Activator.CreateInstance(InputObj.GetType)
                                MasterObj(i).SlpCode = SalesPersons(i)
                            End If
                        Next
                        ConditionsGrp1.Add("SlpCode IN (" & String.Join(",", ConditionString.ToArray) & ")")
                    Else
                        ConditionsGrp1.Add("SlpCode IN  (@SlpCode)")
                    End If

                End If

                QryConditions.Add(" AND ", ConditionsGrp1)



                ' INPUT FOR Query Builder
                Dim SQB As New ComplexSelectQuery
                SQB._InputTable = MasterObj
                SQB._DB = "Custom"
                SQB._HasInBetweenConditions = False
                SQB._HasWhereConditions = True
                SQB._Conditions = QryConditions

                ResultDataTable = CURD.SelectAllDataComplexCondition(SQB)
                '**********************Query Builder Function *****************
                If ResultDataTable.Rows.Count > 0 Then
                    Result = True
                End If
                Return Result
            Catch ex As Exception
                WriteLog(ex)
                Return False
            End Try
        End Function
        Public Shared Function GetLastSalesPrice(ByVal CardCode As String, ByVal ItemCode As String) As Decimal
            Try
                Dim ReturnResult As Decimal = 0
                Dim ResultDataTable As New DataTable
                Dim OrderTable As New ORDR
                Dim OrderLineTable As New RDR1
                Dim ObjArray(1) As JoinObj

                OrderTable.CardCode = CardCode
                OrderLineTable.ItemCode = ItemCode

                Dim CSJQ As New ComplexSelectJoinQuery
                ObjArray(0) = New JoinObj
                ObjArray(0).InputObj = OrderTable
                ObjArray(1) = New JoinObj
                ObjArray(1).InputObj = OrderLineTable
                ObjArray(1).Join = " INNER JOIN "
                ObjArray(1).Condition = " T1.DOCENTRY=T2.DOCENTRY "

                Dim QryConditions As Dictionary(Of String, List(Of String)) = New Dictionary(Of String, List(Of String))

                'Query Condition Groups
                Dim ConditionsGrp1 As List(Of String) = New List(Of String)

                'Query Conditions values
                ConditionsGrp1.Add("T1.CardCode=@CardCode1")
                ConditionsGrp1.Add("T2.ItemCode=@ItemCode2")
                QryConditions.Add(" AND ", ConditionsGrp1)

                CSJQ._InputTable = ObjArray
                CSJQ._DB = "SAP"
                CSJQ._HasInBetweenConditions = False
                CSJQ._HasWhereConditions = True
                CSJQ._Conditions = QryConditions
                CSJQ._TopRecord = 1
                CSJQ._OrderBy = " T2.DocDate DESC "

                ResultDataTable = CURD.SelectAllDataComplexCondition(CSJQ)
                If ResultDataTable.Rows.Count > 0 Then
                    If Not IsDBNull(ResultDataTable.Rows(0).Item("price")) Then
                        Decimal.TryParse(ResultDataTable.Rows(0).Item("price").ToString, ReturnResult)
                        ReturnResult = ReturnResult.ToString("F4")
                    End If

                End If
                Return ReturnResult
            Catch ex As Exception
                AppSpecificFunc.WriteLog(ex)
                Return 0
            End Try
        End Function
        Public Shared Function GetCustmerPriceListPrice(ByVal CardCode As String, ByVal ItemCode As String) As Decimal
            Try
                Dim ReturnResult As Decimal = 0
                Dim ResultDataTable As New DataTable
                Dim CustomerTable As New OCRD
                Dim ItemLineTable As New ITM1
                Dim ObjArray(1) As JoinObj

                CustomerTable.CardCode = CardCode
                ItemLineTable.ItemCode = ItemCode

                Dim CSJQ As New ComplexSelectJoinQuery
                ObjArray(0) = New JoinObj
                ObjArray(0).InputObj = ItemLineTable
                ObjArray(1) = New JoinObj
                ObjArray(1).InputObj = CustomerTable
                ObjArray(1).Join = " INNER JOIN "
                ObjArray(1).Condition = " T1.PriceList=T2.ListNum "

                Dim QryConditions As Dictionary(Of String, List(Of String)) = New Dictionary(Of String, List(Of String))

                'Query Condition Groups
                Dim ConditionsGrp1 As List(Of String) = New List(Of String)

                'Query Conditions values
                ConditionsGrp1.Add("T2.CardCode=@CardCode2")
                ConditionsGrp1.Add("T1.ItemCode=@ItemCode1")
                QryConditions.Add(" AND ", ConditionsGrp1)

                CSJQ._InputTable = ObjArray
                CSJQ._DB = "SAP"
                CSJQ._HasInBetweenConditions = False
                CSJQ._HasWhereConditions = True
                CSJQ._Conditions = QryConditions
                CSJQ._TopRecord = 1
                'CSJQ._OrderBy = " T2.DocDate DESC "

                ResultDataTable = CURD.SelectAllDataComplexCondition(CSJQ)
                If ResultDataTable.Rows.Count > 0 Then
                    If Not IsDBNull(ResultDataTable.Rows(0).Item("price")) Then
                        Decimal.TryParse(ResultDataTable.Rows(0).Item("price").ToString, ReturnResult)
                        ReturnResult = ReturnResult.ToString("F4")
                    End If

                End If
                Return ReturnResult
            Catch ex As Exception
                AppSpecificFunc.WriteLog(ex)
                Return 0
            End Try
        End Function
        Public Shared Function GetAllPaymentTerms() As DataTable
            Try

                Dim Result As String = String.Empty
                Dim ResultDataTable As New DataTable
                '**********************Query Builder Function *****************
                Dim PaymentTermsTable As New OCTG

                ' INPUT FOR Query Builder
                Dim SQB As New SelectQuery
                SQB._InputTable = PaymentTermsTable
                SQB._DB = "SAP"
                SQB._HasInBetweenConditions = False
                SQB._HasWhereConditions = False


                ResultDataTable = CURD.SelectAllData(SQB)
                '**********************Query Builder Function *****************

                Return ResultDataTable
            Catch ex As Exception
                WriteLog(ex)
                Return New DataTable
            End Try
        End Function
        Public Shared Function GetShipAddressIDsByCardCode(CardCode As String) As DataTable
            Try

                Dim Result As String = String.Empty
                Dim ResultDataTable As New DataTable
                '**********************Query Builder Function *****************
                Dim CRD1Table As New CRD1
                CRD1Table.AdresType = "S"
                CRD1Table.CardCode = CardCode

                Dim QryConditions As Dictionary(Of String, List(Of String)) = New Dictionary(Of String, List(Of String))

                'Query Condition Groups
                Dim ConditionsGrp1 As List(Of String) = New List(Of String)

                'Query Conditions values
                ConditionsGrp1.Add("CardCode=@CardCode")
                ConditionsGrp1.Add("AdresType=@AdresType")
                QryConditions.Add(" AND ", ConditionsGrp1)

                ' INPUT FOR Query Builder
                Dim SQB As New SelectQuery
                SQB._InputTable = CRD1Table
                SQB._DB = "SAP"
                SQB._HasInBetweenConditions = False
                SQB._HasWhereConditions = True
                SQB._Conditions = QryConditions

                ResultDataTable = CURD.SelectAllData(SQB)
                '**********************Query Builder Function *****************

                Return ResultDataTable
            Catch ex As Exception
                WriteLog(ex)
                Return New DataTable
            End Try
        End Function
        Public Shared Function GetShipAddressDetailsByAddressID(AddressID As String, CardCode As String) As CRD1
            Try

                Dim Result As String = String.Empty
                Dim ResultDataTable As New DataTable
                '**********************Query Builder Function *****************
                Dim CRD1Table As New CRD1
                CRD1Table.AdresType = "S"
                CRD1Table.Address = AddressID
                CRD1Table.CardCode = CardCode
                Dim QryConditions As Dictionary(Of String, List(Of String)) = New Dictionary(Of String, List(Of String))

                'Query Condition Groups
                Dim ConditionsGrp1 As List(Of String) = New List(Of String)

                'Query Conditions values
                ConditionsGrp1.Add("Address=@Address")
                ConditionsGrp1.Add("AdresType=@AdresType")
                ConditionsGrp1.Add("CardCode=@CardCode")
                QryConditions.Add(" AND ", ConditionsGrp1)

                ' INPUT FOR Query Builder
                Dim SQB As New SelectQuery
                SQB._InputTable = CRD1Table
                SQB._DB = "SAP"
                SQB._HasInBetweenConditions = False
                SQB._HasWhereConditions = True
                SQB._Conditions = QryConditions

                ResultDataTable = CURD.SelectAllData(SQB)
                '**********************Query Builder Function *****************
                If ResultDataTable.Rows.Count > 0 Then
                    AppSpecificFunc.DataTableToObject(CRD1Table, ResultDataTable)
                End If
                Return CRD1Table
            Catch ex As Exception
                WriteLog(ex)
                Return New CRD1()
            End Try
        End Function
        Public Shared Function GetPaymentTermNameByCode(ByVal PaymentCode As String) As String

            Try

                Dim Result As String = String.Empty
                Dim ResultDataTable As New DataTable
                '**********************Query Builder Function *****************
                Dim PaymentTermsTable As New OCTG

                PaymentTermsTable.GroupNum = PaymentCode
                PaymentTermsTable.PymntGroup = "?"

                Dim QryConditions As Dictionary(Of String, List(Of String)) = New Dictionary(Of String, List(Of String))

                'Query Condition Groups
                Dim ConditionsGrp1 As List(Of String) = New List(Of String)

                'Query Conditions values
                ConditionsGrp1.Add("GroupNum=@GroupNum")
                QryConditions.Add(" AND ", ConditionsGrp1)

                ' INPUT FOR Query Builder
                Dim SQB As New SelectQuery
                SQB._InputTable = PaymentTermsTable
                SQB._DB = "SAP"
                SQB._HasInBetweenConditions = False
                SQB._HasWhereConditions = True
                SQB._Conditions = QryConditions

                ResultDataTable = CURD.SelectSpecificFieldsData(SQB)
                '**********************Query Builder Function *****************
                If ResultDataTable.Rows.Count > 0 Then
                    Result = ResultDataTable.Rows(0).Item(0).ToString
                End If
                Return Result
            Catch ex As Exception
                WriteLog(ex)
                Return String.Empty
            End Try
        End Function
        Public Shared Sub BindGridData(ByVal GridDataTable As DataTable, ByRef InputGridView As GridView)
            Try


                If GridDataTable.Rows.Count > 0 Then
                    InputGridView.DataSource = GridDataTable
                    InputGridView.DataBind()
                Else

                    Dim TempDataTable As DataTable = GridDataTable.Clone
                    TempDataTable.Rows.Add(TempDataTable.NewRow())
                    InputGridView.DataSource = TempDataTable
                    InputGridView.DataBind()

                    AppSpecificFunc.GridNoDataFound(InputGridView)
                End If

            Catch ex As Exception
                AppSpecificFunc.WriteLog(ex)
            End Try
        End Sub
        Public Shared Sub GridNoDataFound(ByRef InputGridView As GridView)
            Try

                Dim TotalColumns As Integer = InputGridView.Columns.Count
                InputGridView.Rows(0).Cells.Clear()
                InputGridView.Rows(0).Cells.Add(New TableCell())
                InputGridView.Rows(0).Cells(0).ColumnSpan = TotalColumns
                InputGridView.Rows(0).Cells(0).Text = "No Record Found"
            Catch ex As Exception
                AppSpecificFunc.WriteLog(ex)
            End Try
        End Sub
        Public Shared Sub GridRowToObject(ByVal FooterRow As GridViewRow, ByRef Obj As Object)
            Try


                Dim props As Type = Obj.GetType

                For Each member As PropertyInfo In props.GetProperties
                    If Not IsNothing(FooterRow.FindControl(member.Name)) Then
                        Select Case (FooterRow.FindControl(member.Name).GetType())
                            Case GetType(TextBox)
                                Dim TBox As TextBox = TryCast(FooterRow.FindControl(member.Name), TextBox)
                                member.SetValue(Obj, TBox.Text, Nothing)
                            Case GetType(DropDownList)
                                Dim DDL As DropDownList = TryCast(FooterRow.FindControl(member.Name), DropDownList)
                                member.SetValue(Obj, DDL.SelectedValue, Nothing)
                            Case GetType(RadioButton)
                                Dim RDO As RadioButton = TryCast(FooterRow.FindControl(member.Name), RadioButton)
                                If RDO.Checked = True Then
                                    member.SetValue(Obj, "Y", Nothing)
                                Else
                                    member.SetValue(Obj, "N", Nothing)
                                End If
                            Case GetType(Label)
                                Dim Lbl As Label = TryCast(FooterRow.FindControl(member.Name), Label)
                                member.SetValue(Obj, Lbl.Text, Nothing)
                        End Select
                    End If
                Next
            Catch ex As Exception
                AppSpecificFunc.WriteLog(ex)
            End Try
        End Sub
        Public Shared Sub ObjectToDataRow(ByVal Obj As Object, ByRef DRow As DataRow)
            Try


                Dim props As Type = Obj.GetType

                For Each member As PropertyInfo In props.GetProperties
                    If Not IsNothing(member.GetValue(Obj, Nothing)) Then
                        DRow(member.Name) = member.GetValue(Obj, Nothing)
                    End If
                Next
            Catch ex As Exception
                AppSpecificFunc.WriteLog(ex)
            End Try
        End Sub
        Public Shared Sub DataTableToObject(ByRef Obj As Object, ByVal DT As DataTable, Optional ByVal Index As Integer = 0)
            Try
                Dim props As Type = Obj.GetType
                If DT.Rows.Count > 0 Then
                    Dim Drow As DataRow = DT.Rows(Index)
                    For Each member As PropertyInfo In props.GetProperties
                        If DT.Columns.Contains(member.Name) Then
                            If Not IsDBNull(Drow(member.Name)) Then
                                Dim ValueType As Type = Type.GetType(member.PropertyType.FullName.ToString)
                                Dim Value = CTypeDynamic(Drow(member.Name).ToString, ValueType)
                                member.SetValue(Obj, Value, Nothing)
                            End If
                        End If
                    Next
                End If
            Catch ex As Exception
                AppSpecificFunc.WriteLog(ex)
            End Try
        End Sub
        Public Shared Function GetIdkeyBySAPSONo(ByVal SAPSONo As String, ByRef Idkey As String) As Boolean
            Try
                Dim ReturnResult As Boolean = False

                Dim ResultDataTable As New DataTable

                '**********************Query Builder Function *****************
                Dim SOHeaderTable As New Models.SOHeader

                'Filter Values
                SOHeaderTable.SAPSONo = SAPSONo
                SOHeaderTable.IdKey = "?"

                'Query Conditions List
                Dim QryConditions As Dictionary(Of String, List(Of String)) = New Dictionary(Of String, List(Of String))

                'Query Condition Groups
                Dim ConditionsGrp1 As List(Of String) = New List(Of String)

                'Query Conditions values
                ConditionsGrp1.Add("SAPSONo=@SAPSONo")
                QryConditions.Add(" AND ", ConditionsGrp1)



                ' INPUT FOR Query Builder
                Dim SQB As New SelectQuery
                SQB._InputTable = SOHeaderTable
                SQB._DB = "Custom"
                SQB._HasInBetweenConditions = False
                SQB._HasWhereConditions = True
                SQB._Conditions = QryConditions

                ResultDataTable = CURD.SelectSpecificFieldsData(SQB)
                '**********************Query Builder Function *****************
                If ResultDataTable.Rows.Count > 0 Then
                    DataTableToObject(SOHeaderTable, ResultDataTable)
                    Idkey = SOHeaderTable.IdKey
                    ReturnResult = True
                End If

                Return ReturnResult
            Catch ex As Exception
                AppSpecificFunc.WriteLog(ex)
                Return False
            End Try
        End Function
        Public Shared Function GetAllDocStatus() As DataTable
            Try
                Dim DT As New DataTable
                DT.Columns.Add("StatusId", GetType(Integer))
                DT.Columns.Add("StatusName", GetType(String))

                For Each Status As DocStatus In System.Enum.GetValues(GetType(DocStatus))
                    If Status.ToString <> "Status_New" Then
                        DT.Rows.Add(Status, GetStatusName(Status))
                    End If
                Next
                Return (DT)
            Catch ex As Exception
                AppSpecificFunc.WriteLog(ex)
                Return Nothing
            End Try

        End Function
        Public Shared Function isThereEnoughStock(ByVal ItemCodeORName As String, ByVal Qty As Decimal, Optional ByVal Type As String = "Code") As Boolean
            Try
                Dim WarhouseCode As String = String.Empty
                Dim Result As Boolean = False
                Dim AvailStock As Decimal = 0
                Dim ResultDataTable As New DataTable
                Dim ItemObj As New OITM
                If Type = "Code" Then
                    ItemObj.ItemCode = ItemCodeORName
                Else
                    ItemObj.ItemName = ItemCodeORName
                End If



                Dim QryConditions As Dictionary(Of String, List(Of String)) = New Dictionary(Of String, List(Of String))

                'Query Condition Groups
                Dim ConditionsGrp1 As List(Of String) = New List(Of String)

                'Query Conditions values
                If Type = "Code" Then
                    ConditionsGrp1.Add("ItemCode=@ItemCode")
                Else
                    ConditionsGrp1.Add("ItemName=@ItemName")
                End If

                QryConditions.Add(" AND ", ConditionsGrp1)

                ' INPUT FOR Query Builder
                Dim SQB As New SelectQuery
                SQB._InputTable = ItemObj
                SQB._DB = "SAP"
                SQB._HasInBetweenConditions = False
                SQB._HasWhereConditions = True
                SQB._Conditions = QryConditions


                ResultDataTable = CURD.SelectAllData(SQB)
                '**********************Query Builder Function *****************
                If ResultDataTable.Rows.Count > 0 Then
                    If Not IsDBNull(ResultDataTable.Rows(0).Item("DfltWH")) Then
                        WarhouseCode = ResultDataTable.Rows(0).Item("DfltWH").ToString
                    End If
                End If

                If WarhouseCode = String.Empty Then
                    WarhouseCode = GetSAPDefaultWarehouse()
                    AvailStock = GetBalanceStock(ItemObj.ItemCode, WarhouseCode)
                Else
                    AvailStock = GetBalanceStock(ItemObj.ItemCode, WarhouseCode)
                End If

                If AvailStock >= Qty Then
                    Result = True
                End If

                Return Result

            Catch ex As Exception
                AppSpecificFunc.WriteLog(ex)
                Return False
            End Try
        End Function
        Public Shared Function GetBalanceStock(ByVal ItemCode As String, ByVal WarehouseCode As String) As Decimal
            Try
                Dim Result As Decimal = 0
                Dim Commited As Decimal = 0
                Dim OnHand As Decimal = 0

                Dim ResultDataTable As New DataTable
                Dim OITWObj As New OITW

                OITWObj.ItemCode = ItemCode
                OITWObj.WhsCode = WarehouseCode

                Dim QryConditions As Dictionary(Of String, List(Of String)) = New Dictionary(Of String, List(Of String))

                'Query Condition Groups
                Dim ConditionsGrp1 As List(Of String) = New List(Of String)

                'Query Conditions values

                ConditionsGrp1.Add("ItemCode=@ItemCode")
                ConditionsGrp1.Add("WhsCode=@WhsCode")

                QryConditions.Add(" AND ", ConditionsGrp1)

                ' INPUT FOR Query Builder
                Dim SQB As New SelectQuery
                SQB._InputTable = OITWObj
                SQB._DB = "SAP"
                SQB._HasInBetweenConditions = False
                SQB._HasWhereConditions = True
                SQB._Conditions = QryConditions

                ResultDataTable = CURD.SelectAllData(SQB)
                '**********************Query Builder Function *****************
                If ResultDataTable.Rows.Count > 0 Then
                    Decimal.TryParse(ResultDataTable.Rows(0).Item("IsCommited"), Commited)
                    Decimal.TryParse(ResultDataTable.Rows(0).Item("OnHand"), OnHand)
                End If
                Result = OnHand - Commited
                Return Result
            Catch ex As Exception
                AppSpecificFunc.WriteLog(ex)
                Return 0
            End Try
        End Function
        Public Shared Function GetBalanceStockDetails(ByVal ItemCode As String, ByRef CommitedTotal As Decimal, ByRef OnHandTotal As Decimal) As Decimal
            Try
                Dim Result As Decimal = 0
                Dim Commited As Decimal = 0
                Dim OnHand As Decimal = 0

                Dim ResultDataTable As New DataTable
                Dim OITWObj As New OITW

                OITWObj.ItemCode = ItemCode


                Dim QryConditions As Dictionary(Of String, List(Of String)) = New Dictionary(Of String, List(Of String))

                'Query Condition Groups
                Dim ConditionsGrp1 As List(Of String) = New List(Of String)

                'Query Conditions values

                ConditionsGrp1.Add("ItemCode=@ItemCode")


                QryConditions.Add(" AND ", ConditionsGrp1)

                ' INPUT FOR Query Builder
                Dim SQB As New SelectQuery
                SQB._InputTable = OITWObj
                SQB._DB = "SAP"
                SQB._HasInBetweenConditions = False
                SQB._HasWhereConditions = True
                SQB._Conditions = QryConditions

                ResultDataTable = CURD.SelectAllData(SQB)
                '**********************Query Builder Function *****************
                If ResultDataTable.Rows.Count > 0 Then
                    For Each row As DataRow In ResultDataTable.Rows
                        Decimal.TryParse(row.Item("IsCommited"), Commited)
                        Decimal.TryParse(row.Item("OnHand"), OnHand)
                        CommitedTotal = CommitedTotal + Commited
                        OnHandTotal = OnHandTotal + OnHand
                    Next


                End If
                Result = OnHandTotal - CommitedTotal
                Return Result
            Catch ex As Exception
                AppSpecificFunc.WriteLog(ex)
                Return 0
            End Try
        End Function
        Public Shared Function GetSAPDefaultWarehouse() As String
            Try
                Dim Result As String = String.Empty
                Dim ResultDataTable As New DataTable
                '**********************Query Builder Function *****************
                Dim SAPMasterTable As New OADM

                'Filter Values

                SAPMasterTable.dfltwhs = "?"

                ' INPUT FOR Query Builder
                Dim SQB As New SelectQuery
                SQB._InputTable = SAPMasterTable
                SQB._DB = "SAP"
                SQB._HasInBetweenConditions = False
                SQB._HasWhereConditions = False


                ResultDataTable = CURD.SelectSpecificFieldsData(SQB)
                '**********************Query Builder Function *****************
                If ResultDataTable.Rows.Count > 0 Then
                    Result = ResultDataTable.Rows(0).Item(0).ToString
                End If
                Return Result
            Catch ex As Exception
                AppSpecificFunc.WriteLog(ex)
                Return String.Empty
            End Try
        End Function
        Public Shared Function WriteLog(ByRef ex As Exception) As ErrLog
            Dim EL As New ErrLog
            Dim St As StackTrace = New StackTrace(ex, True)
            EL.errMSg = ex.Message
            If Not IsNothing(ex.InnerException) Then
                EL.InnerException = ex.InnerException.Message
            End If
            Dim FrameCount As Integer = St.FrameCount
            For i As Integer = 0 To FrameCount - 1
                If Not IsNothing(St.GetFrame(i).GetFileName) Then
                    EL.FileName = St.GetFrame(i).GetFileName.ToString
                    EL.LineNumber = St.GetFrame(i).GetFileLineNumber.ToString
                End If

            Next
            EL.CreatedOn = Format(CDate(DateTime.Now), "yyyy-MM-dd HH:mm:ss")
            CURD.InsertData(EL, True)
            Return EL
        End Function
    End Class
End Namespace

