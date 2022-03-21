Imports SchloetterSalesPortal.Models
Imports System.Globalization
Imports System.Threading

Public Class SalesOrderListing
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
    <System.Web.Script.Services.ScriptMethod(), _
   System.Web.Services.WebMethod(EnableSession:=True)> _
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
    <System.Web.Script.Services.ScriptMethod(), _
  System.Web.Services.WebMethod()> _
    Public Shared Function GetDocNos(ByVal prefixText As String, ByVal count As Integer) As List(Of String)
        Try
            Dim SalesPersons() As String = {}
            If Not IsNothing(HttpContext.Current.Session("SPs")) Then
                SalesPersons = HttpContext.Current.Session("SPs")
            End If

            Dim DocumentNos As List(Of String) = New List(Of String)
            Dim ResultDataTable As DataTable
            Dim SalesOrderObj As New SOHeader
            If SalesPersons.Count > 0 Then
                ResultDataTable = AppSpecificFunc.GetActiveDocumentNosAutoComplete(SalesOrderObj, prefixText, SalesPersons)
                If ResultDataTable.Rows.Count > 0 Then
                    CommonFunc.GetStringListFromDataTable(ResultDataTable, "IdKey", DocumentNos)
                End If
            End If

            Return DocumentNos
        Catch ex As Exception
            AppSpecificFunc.WriteLog(ex)
            Return New List(Of String)
        End Try
    End Function
    <System.Web.Script.Services.ScriptMethod(), _
  System.Web.Services.WebMethod()> _
    Public Shared Function GetSAPDocNos(ByVal prefixText As String, ByVal count As Integer) As List(Of String)
        Try
            Dim SalesPersons() As String = {}
            If Not IsNothing(HttpContext.Current.Session("SPs")) Then
                SalesPersons = HttpContext.Current.Session("SPs")
            End If

            Dim DocumentNos As List(Of String) = New List(Of String)
            Dim ResultDataTable As DataTable
            Dim SalesOrderObj As New SOHeader
            If SalesPersons.Count > 0 Then
                ResultDataTable = AppSpecificFunc.GetSAPDocumentNosAutoComplete(SalesOrderObj, prefixText, SalesPersons)
                If ResultDataTable.Rows.Count > 0 Then
                    CommonFunc.GetStringListFromDataTable(ResultDataTable, "SAPSONo", DocumentNos)
                End If
            End If

            Return DocumentNos
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
    Private Sub SalesOrderListing_Init(sender As Object, e As System.EventArgs) Handles Me.Init
        Try
            BindStatusValues()
        Catch ex As Exception
            AppSpecificFunc.WriteLog(ex)
        End Try
    End Sub
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not IsPostBack Then
                Dim DT As DataTable = LoadData(True)
                If Not IsNothing(DT) Then

                    AppSpecificFunc.BindGridData(DT, SalesOrderMasterGrid)

                End If
            Else
                If CType(ViewState("RowCount"), Integer) = 0 Then
                    Dim DT As DataTable = LoadData(True)
                    If Not IsNothing(DT) Then

                        AppSpecificFunc.BindGridData(DT, SalesOrderMasterGrid)

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

                    AppSpecificFunc.BindGridData(DT, SalesOrderMasterGrid)

                End If
            End If
        Catch ex As Exception
            AppSpecificFunc.WriteLog(ex)
        End Try

    End Sub
    Protected Sub btnClear_Click(sender As Object, e As EventArgs) Handles btnClear.Click
        Try
            txtFindDocNo.Text = String.Empty
            FromDate.Text = String.Empty
            ToDate.Text = String.Empty
            Status.SelectedIndex = 0
            CustomerCode.Text = String.Empty
            CustomerName.Text = String.Empty
            Dim DT As DataTable = LoadData(True)
            If Not IsNothing(DT) Then
                If Not IsNothing(DT) Then
                    AppSpecificFunc.BindGridData(DT, SalesOrderMasterGrid)
                End If
            End If
        Catch ex As Exception
            AppSpecificFunc.WriteLog(ex)
        End Try
    End Sub
#End Region
#Region "Event Handlers - Grid View"
    Private Sub SalesOrderMasterGrid_PageIndexChanging(sender As Object, e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles SalesOrderMasterGrid.PageIndexChanging
        Try

            SalesOrderMasterGrid.PageIndex = e.NewPageIndex
            Dim DT As DataTable = LoadData(False)
            If Not IsNothing(DT) Then

                AppSpecificFunc.BindGridData(DT, SalesOrderMasterGrid)

            End If
        Catch ex As Exception
            AppSpecificFunc.WriteLog(ex)
        End Try
    End Sub
    Private Sub SalesOrderMasterGrid_RowCommand(sender As Object, e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles SalesOrderMasterGrid.RowCommand
        Try
            If e.CommandName = "View" Then
                Me.Context.Items.Add("SONo", e.CommandArgument)
                Server.Transfer("~/Business/SalesOrder.aspx", True)
            End If

        Catch ex As Exception When Not TypeOf ex Is ThreadAbortException
            AppSpecificFunc.WriteLog(ex)
        End Try
    End Sub
    Private Sub SalesOrderMasterGrid_RowDataBound(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles SalesOrderMasterGrid.RowDataBound
        Try
            If e.Row.RowType = DataControlRowType.DataRow Then
                'Change Status value
                If IsNumeric(e.Row.Cells(3).Text) Then
                    e.Row.Cells(3).Text = AppSpecificFunc.GetStatusName(e.Row.Cells(3).Text)
                End If

            End If
        Catch ex As Exception
            AppSpecificFunc.WriteLog(ex)
        End Try
    End Sub
    Private Sub SalesOrderMasterGrid_Sorting(sender As Object, e As System.Web.UI.WebControls.GridViewSortEventArgs) Handles SalesOrderMasterGrid.Sorting
        Try
            Dim DT As DataTable = LoadData(False)
            If Not IsNothing(DT) Then
                Dim DV As DataView = New DataView(DT)

                DV.Sort = e.SortExpression & " " & GetSorting()
                DT = DV.ToTable
                AppSpecificFunc.BindGridData(DT, SalesOrderMasterGrid)
            End If
        Catch ex As Exception
            AppSpecificFunc.WriteLog(ex)
        End Try
    End Sub
#End Region
#Region "Form Related functions"
    Protected Function GetSorting() As String
        Try
            If IsNothing(ViewState("Sorting")) Then
                ViewState("Sorting") = "ASC"
            Else

                If ViewState("Sorting").ToString = "DESC" Then
                    ViewState("Sorting") = "ASC"
                Else
                    ViewState("Sorting") = "DESC"
                End If
            End If
            Return ViewState("Sorting").ToString
        Catch ex As Exception
            AppSpecificFunc.WriteLog(ex)
            Return "ASC"
        End Try
    End Function
    Protected Function LoadData(Optional ByVal Initialise As Boolean = True) As DataTable

        Try

            

            Dim SOHeaderObj() As SOHeader = Nothing
            Dim CSQ As New ComplexSelectQuery

            'Query Conditions List
            Dim QryConditions As Dictionary(Of String, List(Of String)) = New Dictionary(Of String, List(Of String))

            'Query Condition Groups
            Dim ConditionsGrp1 As List(Of String) = New List(Of String)


            If Initialise = True Then
                ReDim Preserve SOHeaderObj(0)
                SOHeaderObj(0) = New SOHeader
                SOHeaderObj(0).IdKey = -1

                'Query Conditions values
                ConditionsGrp1.Add("IdKey=@IdKey")

            Else

                Dim SalesPersons() As String = {}
                If Not IsNothing(HttpContext.Current.Session("SPs")) Then
                    SalesPersons = HttpContext.Current.Session("SPs")
                End If

                'First Obejct
                ReDim Preserve SOHeaderObj(0)
                SOHeaderObj(0) = New SOHeader
                'Where Condition parameter
                If txtFindDocNo.Text <> String.Empty Then
                    SOHeaderObj(0).IdKey = txtFindDocNo.Text
                End If
                If CustomerCode.Text <> String.Empty Then
                    SOHeaderObj(0).CustomerCode = CustomerCode.Text
                End If
                If CustomerName.Text <> String.Empty Then
                    SOHeaderObj(0).CustomerName = CustomerName.Text
                End If
                If Status.SelectedIndex > 0 Then
                    SOHeaderObj(0).Status = Status.SelectedItem.Value
                End If

                If FromDate.Text <> String.Empty Then
                    SOHeaderObj(0).DocDate = FromDate.Text
                End If

                If SalesPersons.Count > 0 Then
                    SOHeaderObj(0).SlpCode = SalesPersons(0)
                End If


                'Second Obejct
                If ToDate.Text <> String.Empty Then
                    ReDim Preserve SOHeaderObj(1)
                    SOHeaderObj(1) = New SOHeader
                    SOHeaderObj(1).DocDate = ToDate.Text
                End If




                'Query Conditions values
                If txtFindDocNo.Text <> String.Empty Then
                    ConditionsGrp1.Add("IdKey=@IdKey")
                End If
                If CustomerCode.Text <> String.Empty Then
                    ConditionsGrp1.Add("CustomerCode=@CustomerCode")
                End If
                If CustomerName.Text <> String.Empty Then
                    ConditionsGrp1.Add("CustomerName=@CustomerName")
                End If
                If Status.SelectedIndex > 0 Then
                    ConditionsGrp1.Add("Status=@Status")
                End If

                If FromDate.Text <> String.Empty Then
                    ConditionsGrp1.Add("DocDate>=@DocDate")
                End If


                If ToDate.Text <> String.Empty Then
                    ConditionsGrp1.Add("DocDate<=@DocDate1")
                End If


                If SalesPersons.Count > 0 Then
                    If SalesPersons.Count > 1 Then
                        Dim ConditionString As New List(Of String)

                        For i = 0 To SalesPersons.Count - 1
                            If i = 0 Then
                                ConditionString.Add("@SlpCode")
                            Else
                                ReDim Preserve SOHeaderObj(i)

                                ConditionString.Add("@SlpCode" & i)
                                SOHeaderObj(i) = New SOHeader
                                SOHeaderObj(i).SlpCode = SalesPersons(i)
                            End If
                        Next
                        ConditionsGrp1.Add("SlpCode IN (" & String.Join(",", ConditionString.ToArray) & ")")
                    Else
                        ConditionsGrp1.Add("SlpCode IN  (@SlpCode)")
                    End If

                End If

            End If

            If ConditionsGrp1.Count > 0 Then
                QryConditions.Add(" AND ", ConditionsGrp1)
            End If



            CSQ._InputTable = SOHeaderObj
            CSQ._DB = "Custom"
            CSQ._HasInBetweenConditions = False
            If QryConditions.Count > 0 Then
                CSQ._HasWhereConditions = True
                CSQ._Conditions = QryConditions
            Else
                CSQ._HasWhereConditions = False
            End If

            CSQ._OrderBy = " Idkey DESC"
            'CSQ._TopRecord = 1
            Dim ResultDataTable As New DataTable
            ResultDataTable = CURD.SelectAllDataComplexCondition(CSQ)

            ViewState("RowCount") = ResultDataTable.Rows.Count

            Return ResultDataTable

        Catch ex As Exception
            AppSpecificFunc.WriteLog(ex)
            Return Nothing
        End Try
    End Function
    Protected Sub BindStatusValues()
        Try
            Dim DocStatusTable As DataTable = AppSpecificFunc.GetAllDocStatus
            Status.DataSource = DocStatusTable
            Status.DataTextField = "StatusName"
            Status.DataValueField = "StatusId"
            Status.DataBind()
        Catch ex As Exception
            AppSpecificFunc.WriteLog(ex)
        End Try
    End Sub
#End Region
#Region "Custom Validation function"
    Protected Sub ValidateFindDocNo(ByVal source As System.Object, ByVal args As System.Web.UI.WebControls.ServerValidateEventArgs)
        Try
            Dim SalesPersons() As String = {}
            If Not IsNothing(HttpContext.Current.Session("SPs")) Then
                SalesPersons = HttpContext.Current.Session("SPs")
            End If

            Dim SOHeaderObj As New SOHeader
            Dim CustomValidatorControl As CustomValidator = TryCast(source, CustomValidator)
            If txtFindDocNo.Text <> String.Empty Then
                Dim ErrMsg As String = String.Empty
                If CURD.CheckFieldValueExist("SOHeader", "IdKey", txtFindDocNo.Text, ErrMsg) = False Then                    
                    args.IsValid = False
                ElseIf SalesPersons.Count <= 0 Then
                    args.IsValid = False
                ElseIf AppSpecificFunc.UserCanAccessDocumentNo(SOHeaderObj, txtFindDocNo.Text, SalesPersons, "DocNo") = False Then
                    args.IsValid = False
                End If
            End If

        Catch ex As Exception
            AppSpecificFunc.WriteLog(ex)
        End Try
    End Sub
    Protected Sub ValidateFromDate(ByVal source As System.Object, ByVal args As System.Web.UI.WebControls.ServerValidateEventArgs) Handles FromDateValidator.ServerValidate
        Try
            Dim FromDateValue As Date
            If FromDate.Text <> String.Empty Then
                If DateTime.TryParseExact(FromDate.Text, "yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture, DateTimeStyles.None, FromDateValue) = False Then
                    args.IsValid = False
                End If
            End If

        Catch ex As Exception
            AppSpecificFunc.WriteLog(ex)
        End Try
    End Sub
    Protected Sub ValidateToDate(ByVal source As System.Object, ByVal args As System.Web.UI.WebControls.ServerValidateEventArgs) Handles ToDateValidator.ServerValidate
        Try
            Dim ToDateValue As Date
            If ToDate.Text <> String.Empty Then
                If Date.TryParseExact(ToDate.Text, "yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture, DateTimeStyles.None, ToDateValue) = False Then
                    args.IsValid = False
                End If
            Else
                args.IsValid = True
            End If

        Catch ex As Exception
            AppSpecificFunc.WriteLog(ex)
        End Try
    End Sub
#End Region


    
End Class