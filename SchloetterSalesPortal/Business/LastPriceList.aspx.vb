Imports SchloetterSalesPortal.Models

Public Class LastPriceList
    Inherits System.Web.UI.Page

#Region "Helper functions- Page Methods"
    <System.Web.Services.WebMethodAttribute(EnableSession:=True), System.Web.Script.Services.ScriptMethodAttribute()>
    Public Shared Function GetCustomerCodes(ByVal prefixText As String, ByVal count As Integer) As List(Of String)
        Try
            Dim SalesPersons() As String = {}
            If Not IsNothing(HttpContext.Current.Session("SPs")) Then
                SalesPersons = HttpContext.Current.Session("SPs")
            End If


            Dim CustomerCodes As List(Of String) = New List(Of String)
            Dim ResultDataTable As DataTable
            ResultDataTable = AppSpecificFunc.GetActiveCustomersAutoComplete("CardCode", prefixText, SalesPersons)
            If ResultDataTable.Rows.Count > 0 Then
                CommonFunc.GetStringListFromDataTable(ResultDataTable, "CardCode", CustomerCodes)
            End If
            Return CustomerCodes
        Catch ex As Exception
            AppSpecificFunc.WriteLog(ex)
            Return New List(Of String)
        End Try
    End Function
    <System.Web.Script.Services.ScriptMethod(),
   System.Web.Services.WebMethod(EnableSession:=True)>
    Public Shared Function GetCustomerNames(ByVal prefixText As String, ByVal count As Integer) As List(Of String)
        Try
            Dim SalesPersons() As String = {}
            If Not IsNothing(HttpContext.Current.Session("SPs")) Then
                SalesPersons = HttpContext.Current.Session("SPs")
            End If

            Dim CustomerNames As List(Of String) = New List(Of String)
            Dim ResultDataTable As DataTable
            ResultDataTable = AppSpecificFunc.GetActiveCustomersAutoComplete("CardName", prefixText, SalesPersons)
            If ResultDataTable.Rows.Count > 0 Then
                CommonFunc.GetStringListFromDataTable(ResultDataTable, "CardName", CustomerNames)
            End If
            Return CustomerNames
        Catch ex As Exception
            AppSpecificFunc.WriteLog(ex)
            Return New List(Of String)
        End Try
    End Function
    <System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()>
    Public Shared Function GetItemCodes(ByVal prefixText As String, ByVal count As Integer) As List(Of String)
        Try

            Dim ItemCodes As List(Of String) = New List(Of String)
            Dim ResultDataTable As DataTable
            ResultDataTable = AppSpecificFunc.GetActiveInventoryItemsAutoComplete("ItemCode", prefixText)
            If ResultDataTable.Rows.Count > 0 Then
                CommonFunc.GetStringListFromDataTable(ResultDataTable, "ItemCode", ItemCodes)
            End If
            Return ItemCodes
        Catch ex As Exception
            AppSpecificFunc.WriteLog(ex)
            Return New List(Of String)
        End Try
    End Function
    <System.Web.Script.Services.ScriptMethod(),
  System.Web.Services.WebMethod()>
    Public Shared Function GetItemNames(ByVal prefixText As String, ByVal count As Integer) As List(Of String)
        Try

            Dim ItemNames As List(Of String) = New List(Of String)
            Dim ResultDataTable As DataTable
            ResultDataTable = AppSpecificFunc.GetActiveInventoryItemsAutoComplete("ItemName", prefixText)
            If ResultDataTable.Rows.Count > 0 Then
                CommonFunc.GetStringListFromDataTable(ResultDataTable, "ItemName", ItemNames)
            End If
            Return ItemNames
        Catch ex As Exception
            AppSpecificFunc.WriteLog(ex)
            Return New List(Of String)
        End Try
    End Function
#End Region
#Region "Form Event Handlers"
    Protected Sub CustomerCode_TextChanged(sender As Object, e As EventArgs) Handles CustomerCode.TextChanged
        Try
            Dim CustomerObj As OCRD = Nothing
            Dim CardName As String = String.Empty
            Dim SalesPersons() As String = {}
            If Not IsNothing(HttpContext.Current.Session("SPs")) Then
                SalesPersons = HttpContext.Current.Session("SPs")
            End If
            If SalesPersons.Count > 0 Then
                CustomerObj = AppSpecificFunc.GetActiveCustomerByCodeORName("NameByCode", CustomerCode.Text, SalesPersons)
            End If
            If Not IsNothing(CustomerObj) Then
                CardName = CustomerObj.CardName
            End If
            If CardName <> String.Empty Then
                CustomerName.Text = CardName
            Else
                CustomerCode.Text = String.Empty
                CustomerName.Text = String.Empty
            End If
        Catch ex As Exception
            AppSpecificFunc.WriteLog(ex)
        End Try
    End Sub

    Protected Sub CustomerName_TextChanged(sender As Object, e As EventArgs) Handles CustomerName.TextChanged
        Try
            Dim CustomerObj As OCRD = Nothing
            Dim CardCode As String = String.Empty
            Dim SalesPersons() As String = {}
            If Not IsNothing(HttpContext.Current.Session("SPs")) Then
                SalesPersons = HttpContext.Current.Session("SPs")
            End If

            If SalesPersons.Count > 0 Then
                CustomerObj = AppSpecificFunc.GetActiveCustomerByCodeORName("CodeByName", CustomerName.Text, SalesPersons)
            End If
            If Not IsNothing(CustomerObj) Then
                CardCode = CustomerObj.CardCode
            End If
            If CardCode <> String.Empty Then
                CustomerCode.Text = CardCode
            Else
                CustomerName.Text = String.Empty
                CustomerCode.Text = String.Empty
            End If
        Catch ex As Exception
            AppSpecificFunc.WriteLog(ex)
        End Try
    End Sub

    Protected Sub ToItemCode_TextChanged(sender As Object, e As System.EventArgs) Handles ToItemCode.TextChanged
        Try
            Dim ItemObj As OITM = Nothing
            Dim ItmCode As String = String.Empty

            ItemObj = AppSpecificFunc.GetActiveItemByCodeORName("NameByCode", ToItemCode.Text)

            If Not IsNothing(ItemObj) Then
                ToItemName.Text = ItemObj.ItemName
                ToItemCode.Text = ItemObj.ItemCode
            Else
                ToItemName.Text = String.Empty
                ToItemCode.Text = String.Empty
            End If
        Catch ex As Exception
            AppSpecificFunc.WriteLog(ex)
        End Try

    End Sub
    Protected Sub ItemCode_TextChanged(sender As Object, e As System.EventArgs) Handles ItemCode.TextChanged
        Try
            Dim ItemObj As OITM = Nothing
            Dim ItmCode As String = String.Empty

            ItemObj = AppSpecificFunc.GetActiveItemByCodeORName("NameByCode", ItemCode.Text)

            If Not IsNothing(ItemObj) Then
                ItemName.Text = ItemObj.ItemName
                ItemCode.Text = ItemObj.ItemCode
            Else
                ItemName.Text = String.Empty
                ItemCode.Text = String.Empty
            End If
        Catch ex As Exception
            AppSpecificFunc.WriteLog(ex)
        End Try

    End Sub
    Protected Sub ItemName_TextChanged(sender As Object, e As System.EventArgs) Handles ItemName.TextChanged

        Try
            Dim ItemObj As OITM = Nothing
            Dim ItmCode As String = String.Empty

            ItemObj = AppSpecificFunc.GetActiveItemByCodeORName("CodeByName", ItemName.Text)

            If Not IsNothing(ItemObj) Then
                ItemName.Text = ItemObj.ItemName
                ItemCode.Text = ItemObj.ItemCode
            Else
                ItemName.Text = String.Empty
                ItemCode.Text = String.Empty
            End If
        Catch ex As Exception
            AppSpecificFunc.WriteLog(ex)
        End Try
    End Sub
    Protected Sub ToItemName_TextChanged(sender As Object, e As System.EventArgs) Handles ToItemName.TextChanged

        Try
            Dim ItemObj As OITM = Nothing
            Dim ItmCode As String = String.Empty

            ItemObj = AppSpecificFunc.GetActiveItemByCodeORName("CodeByName", ToItemName.Text)

            If Not IsNothing(ItemObj) Then
                ToItemName.Text = ItemObj.ItemName
                ToItemCode.Text = ItemObj.ItemCode
            Else
                ToItemName.Text = String.Empty
                ToItemCode.Text = String.Empty
            End If
        Catch ex As Exception
            AppSpecificFunc.WriteLog(ex)
        End Try
    End Sub
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not IsPostBack Then
                Dim DT As DataTable = LoadData(True)
                If Not IsNothing(DT) Then

                    AppSpecificFunc.BindGridData(DT, LastPriceListGrid)

                End If
            Else
                If CType(ViewState("RowCount"), Integer) = 0 Then
                    Dim DT As DataTable = LoadData(True)
                    If Not IsNothing(DT) Then

                        AppSpecificFunc.BindGridData(DT, LastPriceListGrid)

                    End If
                End If
            End If
        Catch ex As Exception
            AppSpecificFunc.WriteLog(ex)
        End Try
    End Sub
    Protected Sub btnSearch_Click(sender As Object, e As EventArgs) Handles btnSearch.Click
        Try
            If Page.IsValid Then


                Dim DT As DataTable = LoadData(False)
                If Not IsNothing(DT) Then

                    AppSpecificFunc.BindGridData(DT, LastPriceListGrid)

                End If
            End If
        Catch ex As Exception
            AppSpecificFunc.WriteLog(ex)
        End Try

    End Sub
    Protected Sub btnClear_Click(sender As Object, e As EventArgs) Handles btnClear.Click
        Try
            ItemCode.Text = String.Empty
            ItemName.Text = String.Empty
            CustomerCode.Text = String.Empty
            CustomerName.Text = String.Empty
            ToItemCode.Text = String.Empty
            ToItemName.Text = String.Empty

            Dim DT As DataTable = LoadData(True)
            If Not IsNothing(DT) Then
                If Not IsNothing(DT) Then
                    AppSpecificFunc.BindGridData(DT, LastPriceListGrid)
                End If
            End If
        Catch ex As Exception
            AppSpecificFunc.WriteLog(ex)
        End Try
    End Sub
#End Region
#Region "Form Related functions"
    'Protected Function LoadData(Optional ByVal Initialise As Boolean = True) As DataTable

    '    Try



    '        Dim ResultDataTable As New DataTable

    '        Dim CQ As New CustomQuery
    '        CQ._DB = "SAP"
    '        Dim CustomQueryParameters As New Dictionary(Of String, String)
    '        Dim Conditionlist1 As New List(Of String)
    '        '            Dim InputQuery1 As String = "SELECT top 25 * FROM (SELECT OQUT.DocDate, QUT1.ItemCode, OQUT.CardCode, OQUT.DocNum,'SQ' as 'Type', QUT1.DiscPrcnt ,(QUT1.Price-((QUT1.Price*QUT1.DiscPrcnt)/100)) as 'Price After Discount' FROM QUT1 INNER JOIN OQUT ON OQUT.DocEntry=QUT1.DocEntry
    '        'UNION SELECT ORDR.DocDate, RDR1.ItemCode, ORDR.CardCode,  ORDR.DocNum,'SO' as 'Type', RDR1.DiscPrcnt,(RDR1.Price-((RDR1.Price*RDR1.DiscPrcnt)/100)) as 'Price After Discount' FROM RDR1 INNER JOIN ORDR ON ORDR.DocEntry=RDR1.DocEntry
    '        'UNION SELECT OINV.DocDate, INV1.ItemCode, OINV.CardCode,  OINV.DocNum,'IN' as 'Type', INV1.DiscPrcnt,(INV1.Price-((INV1.Price*INV1.DiscPrcnt)/100)) as 'Price After Discount' FROM INV1 INNER JOIN OINV ON OINV.DocEntry=INV1.DocEntry
    '        ' ) AS Results "
    '        Dim InputQuery1 As String = "SELECT OINV.DocDate, INV1.ItemCode, OINV.CardCode,  OINV.DocNum,'IN' as 'Type', INV1.DiscPrcnt,(INV1.Price-((INV1.Price*INV1.DiscPrcnt)/100)) as 'Price After Discount' FROM INV1 INNER JOIN OINV ON OINV.DocEntry=INV1.DocEntry "

    '        Conditionlist1.Add(" CardCode=@CardCode  ")
    '        If CustomerCode.Text.Trim <> String.Empty Then
    '            CustomQueryParameters.Add("@CardCode", CustomerCode.Text.Trim)
    '        Else
    '            CustomQueryParameters.Add("@CardCode", "-1")
    '        End If


    '        Conditionlist1.Add(" ItemCode=@ItemCode ")
    '        If Initialise = False Then
    '            If ItemCode.Text.Trim <> String.Empty Then
    '                CustomQueryParameters.Add("@ItemCode", ItemCode.Text.Trim)
    '            Else
    '                CustomQueryParameters.Add("@ItemCode", "-1")
    '            End If
    '        Else
    '            CustomQueryParameters.Add("@ItemCode", "-1")
    '        End If

    '        If Conditionlist1.Count > 0 Then
    '            Dim CondiString1 As String = String.Join(" AND ", Conditionlist1)
    '            InputQuery1 = InputQuery1 & " WHERE " & CondiString1
    '        End If
    '        CQ._InputQuery = InputQuery1 & "ORDER BY DocDate Desc"
    '        CQ._Parameters = CustomQueryParameters

    '        ResultDataTable = CURD.CustomQueryGetData(CQ)

    '        ViewState("RowCount") = ResultDataTable.Rows.Count

    '        Return ResultDataTable

    '    Catch ex As Exception
    '        AppSpecificFunc.WriteLog(ex)
    '        Return Nothing
    '    End Try
    'End Function
    Protected Function LoadData(Optional ByVal Initialise As Boolean = True) As DataTable

        Try

            Dim LoadedDataTable As DataTable = New DataTable
            LoadedDataTable.Columns.Add(New DataColumn("ItemCode"))
            LoadedDataTable.Columns.Add(New DataColumn("ItemName"))

            LoadedDataTable.Columns.Add(New DataColumn("Type"))
            LoadedDataTable.Columns.Add(New DataColumn("DiscPrcnt1", GetType(Decimal)))
            LoadedDataTable.Columns.Add(New DataColumn("PriceAfterDiscount1", GetType(Decimal)))
            LoadedDataTable.Columns.Add(New DataColumn("Uom1"))
            LoadedDataTable.Columns.Add(New DataColumn("DiscPrcnt2", GetType(Decimal)))
            LoadedDataTable.Columns.Add(New DataColumn("PriceAfterDiscount2", GetType(Decimal)))
            LoadedDataTable.Columns.Add(New DataColumn("Uom2"))
            LoadedDataTable.Columns.Add(New DataColumn("DiscPrcnt3", GetType(Decimal)))
            LoadedDataTable.Columns.Add(New DataColumn("PriceAfterDiscount3", GetType(Decimal)))
            LoadedDataTable.Columns.Add(New DataColumn("Uom3"))

            Dim SalesPersons() As String = {}
            If Not IsNothing(HttpContext.Current.Session("SPs")) Then
                SalesPersons = HttpContext.Current.Session("SPs")
            End If

            Dim prefixText As String = "%"


            Dim ItemCodes As List(Of OITM) = New List(Of OITM)

            If Initialise = False Then


                Dim ResultDataTable As DataTable
                ResultDataTable = AppSpecificFunc.GetActiveInventoryItemCodesBetween(ItemCode.Text.Trim, ToItemCode.Text.Trim)
                If ResultDataTable.Rows.Count > 0 Then
                    For index = 0 To ResultDataTable.Rows.Count - 1
                        Dim obj As New OITM
                        CommonFunc.DataTableToObject(obj, ResultDataTable, index)
                        ItemCodes.Add(obj)
                    Next
                    'CommonFunc.GetStringListFromDataTable(ResultDataTable, "ItemCode", ItemCodes)
                End If

            End If


            For Each Item As OITM In ItemCodes
                Dim NewRow As DataRow = LoadedDataTable.NewRow
                NewRow("ItemCode") = Item.ItemCode

                NewRow("ItemName") = Item.ItemName
                NewRow("Type") = "IN"
                Dim ResultDataTable As New DataTable

                Dim CQ As New CustomQuery
                CQ._DB = "SAP"
                Dim CustomQueryParameters As New Dictionary(Of String, String)
                Dim Conditionlist1 As New List(Of String)

                Dim InputQuery1 As String = "SELECT TOP 3 OINV.DocDate, INV1.ItemCode, INV1.unitmsr, OINV.CardCode,  OINV.DocNum,'IN' as 'Type', INV1.DiscPrcnt,INV1.Price as 'PriceAfterDiscount' FROM INV1 INNER JOIN OINV ON OINV.DocEntry=INV1.DocEntry "

                Conditionlist1.Add(" CardCode=@CardCode  ")
                CustomQueryParameters.Add("@CardCode", CustomerCode.Text.Trim)

                Conditionlist1.Add(" ItemCode=@ItemCode ")
                CustomQueryParameters.Add("@ItemCode", Item.ItemCode)



                If Conditionlist1.Count > 0 Then
                    Dim CondiString1 As String = String.Join(" AND ", Conditionlist1)
                    InputQuery1 = InputQuery1 & " WHERE " & CondiString1
                End If
                CQ._InputQuery = InputQuery1 & " ORDER BY DocDate Desc"
                CQ._Parameters = CustomQueryParameters

                ResultDataTable = CURD.CustomQueryGetData(CQ)

                NewRow("DiscPrcnt1") = 0
                NewRow("PriceAfterDiscount1") = 0
                NewRow("DiscPrcnt2") = 0
                NewRow("PriceAfterDiscount2") = 0
                NewRow("DiscPrcnt3") = 0
                NewRow("PriceAfterDiscount3") = 0
                NewRow("Uom1") = String.Empty
                NewRow("Uom2") = String.Empty
                NewRow("Uom3") = String.Empty

                If ResultDataTable.Rows.Count > 0 Then
                    Dim i As Integer = 1
                    For Each Row As DataRow In ResultDataTable.Rows

                        Select Case i
                            Case 1
                                If IsDBNull(Row("DiscPrcnt")) Then
                                    NewRow("DiscPrcnt1") = 0
                                Else
                                    NewRow("DiscPrcnt1") = Row("DiscPrcnt")
                                End If
                                If IsDBNull(Row("PriceAfterDiscount")) Then
                                    NewRow("PriceAfterDiscount1") = 0
                                Else
                                    NewRow("PriceAfterDiscount1") = Row("PriceAfterDiscount")
                                End If
                                If IsDBNull(Row("unitmsr")) Then
                                    NewRow("Uom1") = ""
                                Else
                                    NewRow("Uom1") = Row("unitmsr")
                                End If
                            Case 2
                                If IsDBNull(Row("DiscPrcnt")) Then
                                    NewRow("DiscPrcnt2") = 0
                                Else
                                    NewRow("DiscPrcnt2") = Row("DiscPrcnt")
                                End If
                                If IsDBNull(Row("PriceAfterDiscount")) Then
                                    NewRow("PriceAfterDiscount2") = 0
                                Else
                                    NewRow("PriceAfterDiscount2") = Row("PriceAfterDiscount")
                                End If
                                If IsDBNull(Row("unitmsr")) Then
                                    NewRow("Uom2") = ""
                                Else
                                    NewRow("Uom2") = Row("unitmsr")
                                End If
                            Case 3
                                If IsDBNull(Row("DiscPrcnt")) Then
                                    NewRow("DiscPrcnt3") = 0
                                Else
                                    NewRow("DiscPrcnt3") = Row("DiscPrcnt")
                                End If
                                If IsDBNull(Row("PriceAfterDiscount")) Then
                                    NewRow("PriceAfterDiscount3") = 0
                                Else
                                    NewRow("PriceAfterDiscount3") = Row("PriceAfterDiscount")
                                End If
                                If IsDBNull(Row("unitmsr")) Then
                                    NewRow("Uom3") = ""
                                Else
                                    NewRow("Uom3") = Row("unitmsr")
                                End If
                        End Select


                        i = i + 1
                    Next
                End If
                If NewRow("PriceAfterDiscount3") > 0 Or NewRow("PriceAfterDiscount2") > 0 Or NewRow("PriceAfterDiscount1") > 0 Then
                    LoadedDataTable.Rows.Add(NewRow)
                End If

            Next
            Dim dataView As New DataView(LoadedDataTable)
            dataView.Sort = " ItemCode ASC "
            Dim SortedDataTable As DataTable = dataView.ToTable()
            Return SortedDataTable

        Catch ex As Exception
            AppSpecificFunc.WriteLog(ex)
            Return Nothing
        End Try
    End Function

#End Region
#Region "Event Handlers - Grid View"
    Private Sub LastPriceListGrid_PageIndexChanging(sender As Object, e As GridViewPageEventArgs) Handles LastPriceListGrid.PageIndexChanging
        Try
            LastPriceListGrid.PageIndex = e.NewPageIndex
            Dim DT As DataTable = LoadData(False)
            If Not IsNothing(DT) Then

                AppSpecificFunc.BindGridData(DT, LastPriceListGrid)

            End If
        Catch ex As Exception
            AppSpecificFunc.WriteLog(ex)
        End Try
    End Sub
#End Region

End Class