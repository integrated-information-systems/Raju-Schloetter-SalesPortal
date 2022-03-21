Imports SchloetterSalesPortal.Models

Public Class StockCheck
    Inherits System.Web.UI.Page

#Region "Helper functions- Page Methods"
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
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not IsPostBack Then
                Dim DT As DataTable = LoadData(True)
                If Not IsNothing(DT) Then

                    AppSpecificFunc.BindGridData(DT, ItemInStockList)

                End If
            Else
                If CType(ViewState("RowCount"), Integer) = 0 Then
                    Dim DT As DataTable = LoadData(True)
                    If Not IsNothing(DT) Then

                        AppSpecificFunc.BindGridData(DT, ItemInStockList)

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

                    AppSpecificFunc.BindGridData(DT, ItemInStockList)

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

            Dim DT As DataTable = LoadData(True)
            If Not IsNothing(DT) Then
                If Not IsNothing(DT) Then
                    AppSpecificFunc.BindGridData(DT, ItemInStockList)
                End If
            End If
        Catch ex As Exception
            AppSpecificFunc.WriteLog(ex)
        End Try
    End Sub
#End Region
#Region "Form Related functions"
    Protected Function LoadData(Optional ByVal Initialise As Boolean = True) As DataTable

        Try
            Dim WhsArray As New Dictionary(Of String, String)
            Dim ItemsArray As New Dictionary(Of String, String)
            Dim LoadedDataTable As DataTable = New DataTable
            Dim TempDataTable As DataTable = New DataTable

            LoadedDataTable.Columns.Add(New DataColumn("Warehouse"))
            LoadedDataTable.Columns.Add(New DataColumn("In Stock"))
            LoadedDataTable.Columns.Add(New DataColumn("Committed"))
            LoadedDataTable.Columns.Add(New DataColumn("In Stock - Committed"))
            LoadedDataTable.Columns.Add(New DataColumn("Ordered"))
            LoadedDataTable.Columns.Add(New DataColumn("Available"))
            LoadedDataTable.Columns.Add(New DataColumn("UOM"))

            Dim CQ As New CustomQuery
            CQ._DB = "SAP"
            Dim CustomQueryParameters As New Dictionary(Of String, String)
            Dim Conditionlist1 As New List(Of String)
            Dim InputQuery1 As String = "SELECT OITM.ItemCode, OITM.InvntryUOM, OITW.WhsCode,OITW.OnHand, OITW.IsCommited,OITW.OnOrder, (OITW.OnHand-OITW.IsCommited+OITW.OnOrder) AS 'Available', OWHS.WhsName FROM OITW INNER JOIN OITM ON OITM.ItemCode=OITW.ItemCode   INNER JOIN OWHS ON OWHS.WhsCode=OITW.WhsCode  "


            Conditionlist1.Add(" OITM.ItemCode=@ItemCode ")
            If Initialise = False Then
                If ItemCode.Text.Trim <> String.Empty Then
                    CustomQueryParameters.Add("@ItemCode", ItemCode.Text.Trim)
                Else
                    CustomQueryParameters.Add("@ItemCode", "-1")
                End If
            Else
                CustomQueryParameters.Add("@ItemCode", "-1")
            End If

            If Conditionlist1.Count > 0 Then
                Dim CondiString1 As String = String.Join(" AND ", Conditionlist1)
                InputQuery1 = InputQuery1 & " WHERE " & CondiString1
            End If
            CQ._InputQuery = InputQuery1 & " ORDER BY OWHS.WhsName "
            CQ._Parameters = CustomQueryParameters

            TempDataTable = CURD.CustomQueryGetData(CQ)

            For Each row As DataRow In TempDataTable.Rows
                If WhsArray.ContainsKey(row("WhsCode").ToString()) = False Then
                    WhsArray.Add(row("WhsCode").ToString(), row("WhsName").ToString())
                    ' LoadedDataTable.Columns.Add(New DataColumn(row("WhsCode").ToString() & "-" & row("WhsName").ToString()))
                End If
                If ItemsArray.ContainsKey(row("ItemCode").ToString()) = False Then
                    ItemsArray.Add(row("ItemCode").ToString(), row("ItemCode").ToString())
                End If
            Next

            Dim WhsCodePair As KeyValuePair(Of String, String)
            For Each WhsCodePair In WhsArray
                Dim DR As DataRow = LoadedDataTable.NewRow
                'DR("Warehouse") = WhsCodePair.Key & "-" & WhsCodePair.Value
                DR("Warehouse") = WhsCodePair.Value
                Dim ItemCodePair As KeyValuePair(Of String, String)
                For Each ItemCodePair In ItemsArray
                    Dim resultRows As DataRow = TempDataTable.Select("ItemCode='" & ItemCodePair.Key & "'  AND WhsCode='" & WhsCodePair.Key & "'").First
                    If Not resultRows Is Nothing Then
                        'Response.Write(ItemCodePair.Key & " - " & WhsCodePair.Key & "-" & WhsCodePair.Key & "-" & WhsCodePair.Value & "<BR/>")
                        Dim InStockValue As Decimal = 0
                        If IsDBNull(resultRows.Item("OnHand")) Then
                            DR("In Stock") = "0.00"
                            InStockValue = DR("In Stock")
                        Else
                            DR("In Stock") = Math.Round(resultRows.Item("OnHand"), 2)
                            InStockValue = DR("In Stock")
                        End If
                        Dim CommittedValue As Decimal = 0
                        If IsDBNull(resultRows.Item("IsCommited")) Then
                            DR("Committed") = "0.00"
                            CommittedValue = DR("Committed")
                        Else
                            DR("Committed") = Math.Round(resultRows.Item("IsCommited"), 2)
                            CommittedValue = DR("Committed")
                        End If

                        DR("In Stock - Committed") = Math.Round(InStockValue - CommittedValue, 2)

                        If IsDBNull(resultRows.Item("OnOrder")) Then
                            DR("Ordered") = "0.00"
                        Else
                            DR("Ordered") = Math.Round(resultRows.Item("OnOrder"), 2)
                        End If

                        If IsDBNull(resultRows.Item("Available")) Then
                            DR("Available") = "0.00"
                        Else
                            DR("Available") = Math.Round(resultRows.Item("Available"), 2)
                        End If
                        If IsDBNull(resultRows.Item("InvntryUOM")) Then
                            DR("UOM") = "-"
                        Else
                            DR("UOM") = resultRows.Item("InvntryUOM")
                        End If
                        'DR("In Stock") = resultRows.Item("OnHand")
                        'DR("Committed") = resultRows.Item("IsCommited")
                        '    DR("Ordered") = resultRows.Item("OnOrder")
                        '    DR("Available") = resultRows.Item("Available")
                    End If
                Next
                LoadedDataTable.Rows.Add(DR)
            Next

            Dim DR_Total As DataRow = LoadedDataTable.NewRow

            Dim InStockValueTotal As Decimal = 0
            Dim CommittedValueTotal As Decimal = 0

            DR_Total("Warehouse") = "Total"
            If Not IsDBNull(TempDataTable.Compute("Sum(OnHand)", "")) Then
                DR_Total("In Stock") = Math.Round((TempDataTable.Compute("Sum(OnHand)", "")), 2)
                InStockValueTotal = DR_Total("In Stock")
            Else
                DR_Total("In Stock") = "0.00"
                InStockValueTotal = DR_Total("In Stock")
            End If
            If Not IsDBNull(TempDataTable.Compute("Sum(IsCommited)", "")) Then
                DR_Total("Committed") = Math.Round((TempDataTable.Compute("Sum(IsCommited)", "")), 2)
                CommittedValueTotal = DR_Total("Committed")
            Else
                DR_Total("Committed") = "0.00"
                CommittedValueTotal = DR_Total("Committed")
            End If
            DR_Total("In Stock - Committed") = Math.Round(InStockValueTotal - CommittedValueTotal, 2)
            If Not IsDBNull(TempDataTable.Compute("Sum(OnOrder)", "")) Then
                DR_Total("Ordered") = Math.Round((TempDataTable.Compute("Sum(OnOrder)", "")), 2)
            Else
                DR_Total("Ordered") = "0.00"
            End If
            If Not IsDBNull(TempDataTable.Compute("Sum(Available)", "")) Then
                DR_Total("Available") = Math.Round((TempDataTable.Compute("Sum(Available)", "")), 2)
            Else
                DR_Total("Available") = "0.00"
            End If




            LoadedDataTable.Rows.Add(DR_Total)

            ViewState("RowCount") = LoadedDataTable.Rows.Count

            Return LoadedDataTable

        Catch ex As Exception
            AppSpecificFunc.WriteLog(ex)
            Return Nothing
        End Try
    End Function

#End Region

End Class