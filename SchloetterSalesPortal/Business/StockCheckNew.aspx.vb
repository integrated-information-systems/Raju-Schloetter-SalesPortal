Imports SchloetterSalesPortal.Models

Public Class StockCheckNew
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

            Dim LoadedDataTable As DataTable = New DataTable
            Dim TempDataTable As DataTable = New DataTable

            LoadedDataTable.Columns.Add(New DataColumn("ItemCode"))
            LoadedDataTable.Columns.Add(New DataColumn("Description"))
            LoadedDataTable.Columns.Add(New DataColumn("In Stock"))
            LoadedDataTable.Columns.Add(New DataColumn("Committed"))
            LoadedDataTable.Columns.Add(New DataColumn("Ordered"))
            LoadedDataTable.Columns.Add(New DataColumn("Available"))


            Dim CQ As New CustomQuery
            CQ._DB = "SAP"
            Dim CustomQueryParameters As New Dictionary(Of String, String)
            Dim Conditionlist1 As New List(Of String)
            Dim InputQuery1 As String = "Select T2.*, T3.ItemName from (Select  T1.ItemCode, SUM(T1.onHand) as 'OnHand', SUM(T1.IsCommited) as 'IsCommited', SUM(T1.OnOrder) as 'OnOrder', SUM(T1.onHand)-SUM(T1.IsCommited)+SUM(T1.OnOrder) as 'Available'   from OITW T1 Group By ItemCode)  T2  "
            InputQuery1 = InputQuery1 + " INNER JOIN OITM T3 ON T2.ItemCode=T3.ItemCode "


            If Initialise = False Then
                If ItemCode.Text.Trim <> String.Empty And ToItemCode.Text.Trim <> String.Empty Then
                    Conditionlist1.Add(" (T3.ItemCode>=@FromItemCode and  T3.ItemCode<=@ToItemCode) ")
                    CustomQueryParameters.Add("@FromItemCode", ItemCode.Text.Trim)
                    CustomQueryParameters.Add("@ToItemCode", ToItemCode.Text.Trim)
                ElseIf ItemCode.Text.Trim <> String.Empty Then
                    Conditionlist1.Add("  T3.ItemCode>=@ItemCode ")
                    CustomQueryParameters.Add("@ItemCode", ItemCode.Text.Trim)
                ElseIf ToItemCode.Text.Trim <> String.Empty Then
                    Conditionlist1.Add("  T3.ItemCode<=@ItemCode ")
                    CustomQueryParameters.Add("@ItemCode", ToItemCode.Text.Trim)
                Else
                    Conditionlist1.Add(" T3.ItemCode=@ItemCode ")
                    CustomQueryParameters.Add("@ItemCode", "-1")
                End If
            Else
                Conditionlist1.Add(" T3.ItemCode=@ItemCode ")
                CustomQueryParameters.Add("@ItemCode", "-1")
            End If

            If Conditionlist1.Count > 0 Then
                Dim CondiString1 As String = String.Join(" AND ", Conditionlist1)
                InputQuery1 = InputQuery1 & " WHERE " & CondiString1
            End If
            CQ._InputQuery = InputQuery1 & " ORDER BY T3.ItemCode "
            CQ._Parameters = CustomQueryParameters

            TempDataTable = CURD.CustomQueryGetData(CQ)




            For Each Row As DataRow In TempDataTable.Rows
                Dim DR As DataRow = LoadedDataTable.NewRow
                DR("ItemCode") = Row("ItemCode")
                DR("Description") = Row("ItemName")
                DR("In Stock") = Row("OnHand")
                DR("Committed") = Row("IsCommited")
                DR("Ordered") = Row("OnOrder")
                DR("Available") = Row("Available")
                LoadedDataTable.Rows.Add(DR)
            Next

            ViewState("RowCount") = LoadedDataTable.Rows.Count

            Return LoadedDataTable

        Catch ex As Exception
            AppSpecificFunc.WriteLog(ex)
            Return Nothing
        End Try
    End Function
#End Region
#Region "Event Handlers - Grid View"
    Private Sub ItemInStockList_PageIndexChanging(sender As Object, e As GridViewPageEventArgs) Handles ItemInStockList.PageIndexChanging
        Try
            ItemInStockList.PageIndex = e.NewPageIndex
            Dim DT As DataTable = LoadData(False)
            If Not IsNothing(DT) Then

                AppSpecificFunc.BindGridData(DT, ItemInStockList)

            End If
        Catch ex As Exception
            AppSpecificFunc.WriteLog(ex)
        End Try
    End Sub
#End Region
End Class