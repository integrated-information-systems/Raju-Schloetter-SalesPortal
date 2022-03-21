Imports SchloetterSalesPortal.Models
Imports System.Globalization
Imports System.Reflection
Imports System.Data.SqlClient
Imports AjaxControlToolkit
Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Shared

Public Class SalesOrder
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
            If SalesPersons.Count > 0 Then
                Dim ResultDataTable As DataTable
                ResultDataTable = AppSpecificFunc.GetActiveCustomersAutoComplete("CardCode", prefixText, SalesPersons)
                If ResultDataTable.Rows.Count > 0 Then
                    CommonFunc.GetStringListFromDataTable(ResultDataTable, "CardCode", CustomerCodes)
                End If
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
            If SalesPersons.Count > 0 Then
                Dim ResultDataTable As DataTable
                ResultDataTable = AppSpecificFunc.GetActiveCustomersAutoComplete("CardName", prefixText, SalesPersons)
                If ResultDataTable.Rows.Count > 0 Then
                    CommonFunc.GetStringListFromDataTable(ResultDataTable, "CardName", CustomerNames)
                End If
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
    <System.Web.Script.Services.ScriptMethod(), _
  System.Web.Services.WebMethod()> _
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
    Private Sub ShipTo_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ShipTo.SelectedIndexChanged
        Try
            If ShipTo.SelectedIndex > 0 Then
                LoadAddressDetails()
            Else
                ClearAddressLines()
            End If
        Catch ex As Exception
            AppSpecificFunc.WriteLog(ex)
        End Try
    End Sub
    Private Sub btnPreview_Click(sender As Object, e As EventArgs) Handles btnPreview.Click
        Try
            If IsNumeric(SONo.Text.Trim) Then
                Dim doc As New ReportDocument()

                Dim SAPIdKey As String = String.Empty

                'Dim IsExists As Boolean = AppSpecificFunc.GetIdkeyBySAPSONo(SONo.Text.Trim, SAPIdKey)

                Dim ReportFileName As String = String.Empty

                ReportFileName = Server.MapPath("~\RptFiles\SO.rpt")


                'Response.Write(ReportFileName)
                doc.Load(ReportFileName)
                doc.SetDatabaseLogon(ConfigurationManager.AppSettings("DB_Username"), ConfigurationManager.AppSettings("DB_Password"), ConfigurationManager.AppSettings("DB_Server"), ConfigurationManager.AppSettings("Company_DB"))
                doc.SetParameterValue("idkey", SONo.Text.Trim)
                'doc.SetParameterValue("idkey", SAPIdKey)



                Dim exportOpts As ExportOptions = doc.ExportOptions

                exportOpts.ExportFormatType = ExportFormatType.PortableDocFormat

                exportOpts.ExportDestinationType = ExportDestinationType.DiskFile

                exportOpts.DestinationOptions = New DiskFileDestinationOptions()

                Dim diskOpts As New DiskFileDestinationOptions()

                Dim origin As New DateTime(1970, 1, 1, 0, 0, 0, 0)
                Dim Diff As New TimeSpan
                Diff = Now - origin
                Dim FileNameGenerated As String = Math.Floor(Diff.TotalSeconds)
                FileNameGenerated = "SalesOrder" & "_" & FileNameGenerated & ".pdf"
                'response.write(Server.MapPath("~/Reports/" & FileNameGenerated))
                CType(doc.ExportOptions.DestinationOptions, DiskFileDestinationOptions).DiskFileName = Server.MapPath("~/Reports/" & FileNameGenerated)
                'CType(doc.ExportOptions.DestinationOptions, DiskFileDestinationOptions).DiskFileName = "C:\inetpub\wwwroot\EarthScape\Reports\" + FileNameGenerated
                'export the report to PDF rather than displaying the report in a viewer

                doc.Export()
                doc.Close()
                doc.Dispose()

                Response.Clear()
                Response.AddHeader("content-disposition", "attachment; filename=" & FileNameGenerated)
                Response.WriteFile(Server.MapPath("~/Reports/" & FileNameGenerated))
                Response.ContentType = ""
                Response.End()
            End If

        Catch ex As Exception
            AppSpecificFunc.WriteLog(ex)
        End Try
    End Sub
    Protected Sub OrderDiscPrsnt_TextChanged(sender As Object, e As EventArgs) Handles OrderDiscPrsnt.TextChanged
        Try
            CalculateOrderDiscount()
        Catch ex As Exception
            AppSpecificFunc.WriteLog(ex)
        End Try
    End Sub
    Protected Sub GSTPrsnt_TextChanged(sender As Object, e As EventArgs) Handles GSTPrsnt.TextChanged
        Try

            CalculateGST()

        Catch ex As Exception
            AppSpecificFunc.WriteLog(ex)
        End Try
    End Sub
    Private Sub SalesOrder_Init(sender As Object, e As System.EventArgs) Handles Me.Init
        Try

            BindPaymentTerms()

        Catch ex As Exception
            AppSpecificFunc.WriteLog(ex)
        End Try
    End Sub
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Not IsPostBack Then
            InitialiseForm()
        Else
            Dim GridDataTable As DataTable = TryCast(ViewState("SalesOrderLines"), DataTable)
            If Not IsNothing(GridDataTable) Then
                If GridDataTable.Rows.Count <= 0 Then
                    AppSpecificFunc.GridNoDataFound(SalesOrderLinesGrid)
                End If
            End If
        End If
        DocStatusBasedButtonRestrictions()
        If Not IsNothing(Me.Context.Items.Item("SONo")) Then
            Dim SONoValue As String = Me.Context.Items.Item("SONo").ToString
            FindDocumentBySysDocNo(SONoValue)
        End If
    End Sub
    Protected Sub CustomerCode_TextChanged(sender As Object, e As EventArgs) Handles CustomerCode.TextChanged
        Try
            Dim CardName As String = String.Empty
            Dim CustomerObj As New OCRD
            CustomerObj = Nothing
            Dim SalesPersons() As String = {}
            If Not IsNothing(HttpContext.Current.Session("SPs")) Then
                SalesPersons = HttpContext.Current.Session("SPs")
            End If
            If SalesPersons.Count > 0 Then
                CustomerObj = AppSpecificFunc.GetActiveCustomerByCodeORName("NameByCode", CustomerCode.Text, SalesPersons)
            End If

            If Not IsNothing(CustomerObj) Then
                BindShipToAddresses()
                CardName = CustomerObj.CardName
                If CardName <> String.Empty Then
                    CustomerName.Text = CardName
                    PaymentTerms.SelectedIndex = PaymentTerms.Items.IndexOf(PaymentTerms.Items.FindByValue(CustomerObj.GroupNum))
                    Dim DocDiscountPercent As Decimal = 0
                    DocDiscountPercent = AppSpecificFunc.GetTaxRateByCustomerTaxInfo(CustomerObj.ECVatGroup)
                    GSTPrsnt.Text = DocDiscountPercent.ToString("F2")

                Else
                    CustomerName.Text = String.Empty
                End If
            Else
                BindShipToAddresses()
                CustomerName.Text = String.Empty
            End If

        Catch ex As Exception
            AppSpecificFunc.WriteLog(ex)
        End Try
    End Sub
    Protected Sub CustomerName_TextChanged(sender As Object, e As EventArgs) Handles CustomerName.TextChanged
        Try
            Dim CardCode As String = String.Empty
            Dim CustomerObj As New OCRD
            CustomerObj = Nothing
            Dim SalesPersons() As String = {}
            If Not IsNothing(HttpContext.Current.Session("SPs")) Then
                SalesPersons = HttpContext.Current.Session("SPs")
            End If

            If SalesPersons.Count > 0 Then
                CustomerObj = AppSpecificFunc.GetActiveCustomerByCodeORName("CodeByName", CustomerName.Text, SalesPersons)
            End If

            If Not IsNothing(CustomerObj) Then

                CardCode = CustomerObj.CardCode
                If CardCode <> String.Empty Then
                    CustomerCode.Text = CardCode
                    BindShipToAddresses()
                    PaymentTerms.SelectedIndex = PaymentTerms.Items.IndexOf(PaymentTerms.Items.FindByValue(CustomerObj.GroupNum))
                    Dim DocDiscountPercent As Decimal = 0
                    DocDiscountPercent = AppSpecificFunc.GetTaxRateByCustomerTaxInfo(CustomerObj.ECVatGroup)
                    GSTPrsnt.Text = DocDiscountPercent.ToString("F2")
                Else
                    CustomerCode.Text = String.Empty
                End If
            Else
                BindShipToAddresses()
                CustomerCode.Text = String.Empty
            End If

        Catch ex As Exception
            AppSpecificFunc.WriteLog(ex)
        End Try
    End Sub
    Protected Sub ItemCode_TextChanged(sender As Object, e As System.EventArgs)
        Try
            Dim txtItemcode As TextBox = TryCast(sender, TextBox)

            If Not IsNothing(txtItemcode) Then
                Dim GridRow As GridViewRow = txtItemcode.Parent.Parent

                If Not IsNothing(GridRow) Then
                    Dim txtDescription As TextBox = TryCast(GridRow.FindControl("Description"), TextBox)
                    Dim txtLastPurPrice As TextBox = TryCast(GridRow.FindControl("LastPurchasePrice"), TextBox)
                    Dim txtUnitPrice As TextBox = TryCast(GridRow.FindControl("Price"), TextBox)
                    ' Dim txtOrigin As TextBox = TryCast(GridRow.FindControl("Origin"), TextBox)
                    Dim txtUOM As TextBox = TryCast(GridRow.FindControl("UOM"), TextBox)

                    Dim txtInStock As TextBox = TryCast(GridRow.FindControl("InStock"), TextBox)
                    Dim txtSalesOrdered As TextBox = TryCast(GridRow.FindControl("SalesOrdered"), TextBox)
                    Dim txtAvailable As TextBox = TryCast(GridRow.FindControl("Available"), TextBox)

                    Dim CommitedTotal As Decimal = 0
                    Dim OnHandTotal As Decimal = 0

                    If Not IsNothing(txtDescription) Then
                        Dim Description As String = String.Empty
                        Dim ItemObj As New OITM
                        ItemObj = AppSpecificFunc.GetActiveItemByCodeORName("NameByCode", txtItemcode.Text)
                        If Not IsNothing(ItemObj) Then
                            Description = ItemObj.ItemName
                            If Description <> String.Empty Then
                                txtDescription.Text = Description
                                Dim Lstprice As Decimal = 0

                                'If Decimal.TryParse(ItemObj.LastPurPrc, Lstprice) Then
                                '    txtLastPurPrice.Text = Lstprice.ToString("F2")
                                'Else
                                '    txtLastPurPrice.Text = 0
                                'End If
                                'txtLastPurPrice.Text = AppSpecificFunc.GetCustmerPriceListPrice(CustomerCode.Text, txtItemcode.Text)
                                'txtUnitPrice.Text = AppSpecificFunc.GetLastSalesPrice(CustomerCode.Text, txtItemcode.Text)
                                txtAvailable.Text = AppSpecificFunc.GetBalanceStockDetails(ItemObj.ItemCode, CommitedTotal, OnHandTotal)
                                txtInStock.Text = OnHandTotal
                                txtSalesOrdered.Text = CommitedTotal

                                txtUnitPrice.Text = AppSpecificFunc.GetCustmerPriceListPrice(CustomerCode.Text, txtItemcode.Text)

                                'Dim CartFactor As Decimal = 0
                                'If Decimal.TryParse(ItemObj.U_origin, CartFactor) Then
                                '    txtOrigin.Text = CartFactor.ToString("F2")
                                'Else
                                '    txtOrigin.Text = 0
                                'End If
                                'txtOrigin.Text = String.Empty
                                'txtOrigin.Text = ItemObj.U_origin
                                txtUOM.Text = ItemObj.SalUnitMsr
                            Else
                                txtDescription.Text = String.Empty
                            End If
                        Else
                            txtDescription.Text = String.Empty
                        End If

                    End If
                End If
            End If
        Catch ex As Exception
            AppSpecificFunc.WriteLog(ex)
        End Try
    End Sub
    Protected Sub ItemName_TextChanged(sender As Object, e As System.EventArgs)
        Try

            Dim txtItemName As TextBox = TryCast(sender, TextBox)

            If Not IsNothing(txtItemName) Then
                Dim GridRow As GridViewRow = txtItemName.Parent.Parent

                If Not IsNothing(GridRow) Then
                    Dim txtItemCode As TextBox = TryCast(GridRow.FindControl("ItemCode"), TextBox)
                    Dim txtLastPurPrice As TextBox = TryCast(GridRow.FindControl("LastPurchasePrice"), TextBox)
                    Dim txtUnitPrice As TextBox = TryCast(GridRow.FindControl("Price"), TextBox)
                    ' Dim txtOrigin As TextBox = TryCast(GridRow.FindControl("Origin"), TextBox)
                    Dim txtUOM As TextBox = TryCast(GridRow.FindControl("UOM"), TextBox)
                    If Not IsNothing(txtItemCode) Then
                        Dim ItemCode As String = String.Empty
                        Dim checking As String = String.Empty
                        Dim ItemObj As New OITM
                        ItemObj = AppSpecificFunc.GetActiveItemByCodeORName("CodeByName", txtItemName.Text)
                        If Not IsNothing(ItemObj) Then
                            ItemCode = ItemObj.ItemCode
                            If ItemCode <> String.Empty Then

                                txtItemCode.Text = ItemCode
                                'Dim Lstprice As Decimal = 0
                                ''If Decimal.TryParse(ItemObj.LastPurPrc, Lstprice) Then
                                ''    txtLastPurPrice.Text = Lstprice.ToString("F2")
                                ''Else
                                ''    txtLastPurPrice.Text = 0
                                ''End If
                                'txtLastPurPrice.Text = AppSpecificFunc.GetCustmerPriceListPrice(CustomerCode.Text, txtItemCode.Text)
                                'txtUnitPrice.Text = AppSpecificFunc.GetLastSalesPrice(CustomerCode.Text, txtItemCode.Text)
                                'Dim CartFactor As Decimal = 0
                                'If Decimal.TryParse(ItemObj.U_origin, CartFactor) Then
                                '    txtOrigin.Text = CartFactor.ToString("F2")
                                'Else
                                '    txtOrigin.Text = 0
                                'End If
                                txtUnitPrice.Text = AppSpecificFunc.GetCustmerPriceListPrice(CustomerCode.Text, txtItemCode.Text)
                                txtUOM.Text = ItemObj.SalUnitMsr
                            Else
                                txtItemCode.Text = String.Empty
                            End If
                        Else
                            txtItemCode.Text = String.Empty
                        End If
                    End If

                End If
            End If
        Catch ex As Exception
            AppSpecificFunc.WriteLog(ex)
        End Try
    End Sub
    Protected Sub Qty_TextChanged(sender As Object, e As System.EventArgs)
        'Try

        '    Dim txtQty As TextBox = TryCast(sender, TextBox)

        '    If Not IsNothing(txtQty) Then
        '        Dim GridRow As GridViewRow = txtQty.Parent.Parent

        '        If Not IsNothing(GridRow) Then

        '            UpdateLineTotal(GridRow)

        '        End If
        '    End If


        'Catch ex As Exception
        '    AppSpecificFunc.WriteLog(ex)
        'End Try
    End Sub
    Protected Sub UnitPrice_TextChanged(sender As Object, e As System.EventArgs)
        'Try

        '    Dim txtUnitPrice As TextBox = TryCast(sender, TextBox)

        '    If Not IsNothing(txtUnitPrice) Then
        '        Dim GridRow As GridViewRow = txtUnitPrice.Parent.Parent

        '        If Not IsNothing(GridRow) Then

        '            UpdateLineTotal(GridRow)

        '        End If
        '    End If


        'Catch ex As Exception
        '    AppSpecificFunc.WriteLog(ex)
        'End Try
    End Sub
    Protected Sub DiscPrsnt_TextChanged(sender As Object, e As System.EventArgs)
        'Try

        '    Dim txtDiscPrsnt As TextBox = TryCast(sender, TextBox)

        '    If Not IsNothing(txtDiscPrsnt) Then
        '        Dim GridRow As GridViewRow = txtDiscPrsnt.Parent.Parent

        '        If Not IsNothing(GridRow) Then

        '            UpdateLineTotal(GridRow)

        '        End If
        '    End If


        'Catch ex As Exception
        '    AppSpecificFunc.WriteLog(ex)
        'End Try
    End Sub
    Private Sub btnSave_Click(sender As Object, e As System.EventArgs) Handles btnSave.Click
        Try
            'btnSave.Enabled = False
            If Page.IsValid Then
                If Not IsNumeric(SONo.Text) Then
                    Dim GotDocNo As String = String.Empty
                    If InsertData(DocStatus.Status_Open, GotDocNo) = True Then
                        ShowMessage(String.Format("System Document No {0}: Successfully saved", GotDocNo))
                        InitialiseForm()
                        DocStatusBasedButtonRestrictions()
                    Else
                        ShowMessage("There is a problem in processing your request, Please contact Web administrator")
                    End If
                Else
                    Dim isDocStatusClosed As Boolean = False
                    If UpdateData(DocStatus.Status_Open, isDocStatusClosed) = True Then
                        ShowMessage("Successfully Updated")
                        InitialiseForm()
                        DocStatusBasedButtonRestrictions()
                    Else
                        If isDocStatusClosed = False Then
                            ShowMessage("There is a problem in processing your request, Please contact Web administrator")
                        Else
                            ShowMessage("This document has been already closed,cannot proceed")
                        End If
                    End If
                End If
            End If
            'btnSave.Enabled = True
        Catch ex As Exception
            AppSpecificFunc.WriteLog(ex)
        End Try
    End Sub
    Private Sub btnSubmitToSAP_Click(sender As Object, e As System.EventArgs) Handles btnSubmitToSAP.Click
        Try

            If Page.IsValid Then
                If Not IsNumeric(SONo.Text) Then
                    Dim GotDocNo As String = String.Empty
                    If InsertData(DocStatus.Status_Waiting_For_Syncing, GotDocNo) = True Then
                        ShowMessage(String.Format("System Document No {0}: Successfully saved", GotDocNo))
                        InitialiseForm()
                        DocStatusBasedButtonRestrictions()
                    Else
                        ShowMessage("There is a problem in processing your request, Please contact Web administrator")
                    End If
                Else
                    Dim isDocStatusClosed As Boolean = False
                    If UpdateData(DocStatus.Status_Waiting_For_Syncing, isDocStatusClosed) = True Then
                        ShowMessage("Successfully Updated")
                        InitialiseForm()
                        DocStatusBasedButtonRestrictions()
                    Else
                        If isDocStatusClosed = False Then
                            ShowMessage("There is a problem in processing your request, Please contact Web administrator")
                        Else
                            ShowMessage("This document has been already closed,cannot proceed")
                        End If
                    End If
                End If
            End If
        Catch ex As Exception
            AppSpecificFunc.WriteLog(ex)
        End Try
    End Sub
    Private Sub btnCancel_Click(sender As Object, e As System.EventArgs) Handles btnCancel.Click
        Try
            Dim isDocStatusClosed As Boolean = False
            If UpdateData(DocStatus.Status_Cancelled, isDocStatusClosed) = True Then
                ShowMessage("Cancelled Successfully")
                InitialiseForm()
                DocStatusBasedButtonRestrictions()
            Else
                If isDocStatusClosed = False Then
                    ShowMessage("There is a problem in processing your request, Please contact Web administrator")
                Else
                    ShowMessage("This document has been already closed,cannot proceed")
                End If
            End If
        Catch ex As Exception
            AppSpecificFunc.WriteLog(ex)
        End Try
    End Sub
    Private Sub btnNew_Click(sender As Object, e As System.EventArgs) Handles btnNew.Click, btnback.Click
        Try
            'Status updated to new, before binding data to gridview status to new so that gridview footer will be shown while initialising form
            Try
                Status.Text = AppSpecificFunc.GetStatusName(1)
                InitialiseForm()
                DocStatusBasedButtonRestrictions()
            Catch ex As Exception
                AppSpecificFunc.WriteLog(ex)
            End Try
        Catch ex As Exception
            AppSpecificFunc.WriteLog(ex)
        End Try
    End Sub
    Private Sub btnFindDocNo_Click(sender As Object, e As System.EventArgs) Handles btnFindDocNo.Click
        Try
            If Page.IsValid Then
                If txtFindDocNo.Text <> String.Empty Then
                    FindDocumentBySysDocNo(txtFindDocNo.Text)
                End If
            End If

        Catch ex As Exception
            AppSpecificFunc.WriteLog(ex)
        End Try
    End Sub
    Private Sub btnFindSAPDocNo_Click(sender As Object, e As System.EventArgs) Handles btnFindSAPDocNo.Click
        Try

            If Page.IsValid Then
                If txtFindSAPDocNo.Text <> String.Empty Then
                    FindDocumentBySAPDocNo(txtFindSAPDocNo.Text)
                End If
            End If

        Catch ex As Exception
            AppSpecificFunc.WriteLog(ex)
        End Try
    End Sub
    Protected Sub btnConfirm_Click(sender As Object, e As EventArgs) Handles btnConfirm.Click
        Try
            Select Case ViewState("ConfirmDialogId").ToString
                Case "PriceAndInsufficentConfirmation"
                    InsertGridLine()
                Case "PriceConfirmation"
                    InsertGridLine()
                Case "InsufficientConfirmation"
                    InsertGridLine()
            End Select
            ViewState("ConfirmDialogId") = String.Empty
        Catch ex As Exception
            AppSpecificFunc.WriteLog(ex)
        End Try
    End Sub
    Protected Sub btnNotConfirm_Click(sender As Object, e As EventArgs) Handles btnNotConfirm.Click
        Try

            ViewState("ConfirmDialogId") = String.Empty
        Catch ex As Exception
            AppSpecificFunc.WriteLog(ex)
        End Try
    End Sub
#End Region
#Region "Form Related functions"
    Protected Sub FindDocumentBySAPDocNo(ByVal SAPSONoValue As String)
        Try
            Dim SalesPersons() As String = {}
            If Not IsNothing(HttpContext.Current.Session("SPs")) Then
                SalesPersons = HttpContext.Current.Session("SPs")
            End If

            Dim SOHeaderObj As New SOHeader
            Dim IdKey As String = String.Empty
            Dim ErrMsg As String = String.Empty
            If CURD.CheckFieldValueExist("SOHeader", "SAPSONo", SAPSONoValue, ErrMsg) = True And SalesPersons.Count > 0 Then
                If AppSpecificFunc.UserCanAccessDocumentNo(SOHeaderObj, SAPSONoValue, SalesPersons, "SAP") = True Then
                    If AppSpecificFunc.GetIdkeyBySAPSONo(SAPSONoValue, IdKey) Then
                        If LoadHeaderFromDataBase(IdKey) Then
                            LoadItemsFromDataBase(IdKey)
                            DocStatusBasedButtonRestrictions()
                        End If
                    End If
                Else
                    InitialiseForm()
                    DocStatusBasedButtonRestrictions()
                    MakeGridReadyOnlyBasedOnStatusandRole()
                    ShowMessage("Document not found")
                End If

            Else
                InitialiseForm()
                DocStatusBasedButtonRestrictions()
                MakeGridReadyOnlyBasedOnStatusandRole()
                ShowMessage("Document not found")
            End If

        Catch ex As Exception
            AppSpecificFunc.WriteLog(ex)
        End Try
    End Sub
    Protected Sub FindDocumentBySysDocNo(ByVal IdKey As String)
        Try
            Dim SalesPersons() As String = {}
            If Not IsNothing(HttpContext.Current.Session("SPs")) Then
                SalesPersons = HttpContext.Current.Session("SPs")
            End If

            Dim SOHeaderObj As New SOHeader
            Dim ErrMsg As String = String.Empty
            If CURD.CheckFieldValueExist("SOHeader", "IdKey", IdKey, ErrMsg) = True And SalesPersons.Count > 0 Then

                If AppSpecificFunc.UserCanAccessDocumentNo(SOHeaderObj, IdKey, SalesPersons, "DocNo") = True Then
                    If LoadHeaderFromDataBase(IdKey) Then
                        LoadItemsFromDataBase(IdKey)
                        DocStatusBasedButtonRestrictions()
                    End If
                Else
                    InitialiseForm()
                    DocStatusBasedButtonRestrictions()
                    MakeGridReadyOnlyBasedOnStatusandRole()
                    ShowMessage("Document not found")
                End If
            Else
                InitialiseForm()
                DocStatusBasedButtonRestrictions()
                MakeGridReadyOnlyBasedOnStatusandRole()
                ShowMessage("Document not found")
            End If

        Catch ex As Exception
            AppSpecificFunc.WriteLog(ex)
        End Try
    End Sub
    Protected Sub MakeGridReadyOnlyBasedOnStatusandRole()
        Try
            Dim DocStatus As DocStatus = GetStatus()
            Select Case DocStatus
                Case DocStatus.Status_New, DocStatus.Status_Open, DocStatus.Status_Sync_Failed
                    SalesOrderLinesGrid.ShowFooter = True
                    Dim GridViewColumnCount As Integer = SalesOrderLinesGrid.Columns.Count
                    ' Hide Delete and Edit buttons for the grid
                    SalesOrderLinesGrid.Columns(GridViewColumnCount - 1).Visible = True
                    SalesOrderLinesGrid.Columns(GridViewColumnCount - 2).Visible = True
                Case DocStatus.Status_Closed, DocStatus.Status_Waiting_For_Syncing, DocStatus.Status_Cancelled
                    SalesOrderLinesGrid.ShowFooter = False
                    Dim GridViewColumnCount As Integer = SalesOrderLinesGrid.Columns.Count
                    ' Hide Delete and Edit buttons for the grid
                    SalesOrderLinesGrid.Columns(GridViewColumnCount - 1).Visible = False
                    SalesOrderLinesGrid.Columns(GridViewColumnCount - 2).Visible = False
            End Select

        Catch ex As Exception
            AppSpecificFunc.WriteLog(ex)
        End Try
    End Sub
    Protected Function GridLinesToObjectsPrepareData(Optional ByVal PrimaryKey As String = "") As Object()
        Try
            Dim SalesOrderLines() As SOLines = Nothing

            Dim GridDataTable As DataTable = TryCast(ViewState("SalesOrderLines"), DataTable)
            If Not IsNothing(GridDataTable) Then
                Dim i As Integer = 0
                For Each DRow As DataRow In GridDataTable.Rows
                    ReDim Preserve SalesOrderLines(i)
                    SalesOrderLines(i) = New SOLines
                    Dim props As Type = SalesOrderLines(i).GetType
                    For Each member As PropertyInfo In props.GetProperties
                        If Not IsDBNull(DRow(member.Name)) Then
                            member.SetValue(SalesOrderLines(i), DRow(member.Name).ToString, Nothing)
                        End If
                        If member.Name.ToLower = "linenum" Then
                            member.SetValue(SalesOrderLines(i), (i + 1).ToString, Nothing)
                        End If
                        If PrimaryKey <> String.Empty Then
                            If member.Name.ToLower = "idkey" Then
                                member.SetValue(SalesOrderLines(i), PrimaryKey, Nothing)
                            End If
                        End If
                    Next
                    i = i + 1
                Next
            End If
            Return SalesOrderLines
        Catch ex As Exception
            AppSpecificFunc.WriteLog(ex)
            Return Nothing
        End Try

    End Function
    Protected Function FormToObjectPrepareData(ByVal InsertORUpdate As String) As Object
        Try

            Dim SOHeaderMaster As New SOHeader
            AppSpecificFunc.PageControlsToObject(Me.Master.FindControl("MainContent"), SOHeaderMaster)
            Dim props As Type = SOHeaderMaster.GetType
            For Each member As PropertyInfo In props.GetProperties

                If Not IsNothing(member.GetValue(SOHeaderMaster, Nothing)) Then
                    Select Case member.Name
                        Case "Status"
                            Dim Status As String = member.GetValue(SOHeaderMaster, Nothing).ToString
                            member.SetValue(SOHeaderMaster, AppSpecificFunc.GetStatusValue(Status).ToString, Nothing)
                    End Select
                Else
                    If InsertORUpdate = "Insert" Then
                        Select Case member.Name
                            Case "CreatedBy"
                                member.SetValue(SOHeaderMaster, User.Identity.Name, Nothing)
                        End Select
                    ElseIf InsertORUpdate = "Update" Then
                        Select Case member.Name
                            Case "LastUpdateBy"
                                member.SetValue(SOHeaderMaster, User.Identity.Name, Nothing)
                        End Select
                    End If
                End If

                'If IsNothing(member.GetValue(ItemReqMaster, Nothing)) Then
                '    Response.Write(member.Name & " - <br/>")                 
                'Else
                '    Response.Write(member.Name & " - " & member.GetValue(ItemReqMaster, Nothing).ToString & "<br/>")
                'End If
            Next
            Return SOHeaderMaster
        Catch ex As Exception
            AppSpecificFunc.WriteLog(ex)
            Return Nothing
        End Try
    End Function
    Protected Function InsertData(ByVal DocumentStatus As DocStatus, ByRef DocuNo As String) As Boolean

        Try
            Dim ReturnResult As Boolean = True
            Dim HeaderObject As SOHeader = TryCast(FormToObjectPrepareData("Insert"), SOHeader)
            If DocumentStatus = DocStatus.Status_Waiting_For_Syncing Then
                HeaderObject.SubmitToSAP = "Y"
            Else
                HeaderObject.SubmitToSAP = "N"
            End If
            'Insert SlpCode
            Dim CustomerObj As New OCRD
            CustomerObj = Nothing
            Dim SalesPersons() As String = {}
            If Not IsNothing(HttpContext.Current.Session("SPs")) Then
                SalesPersons = HttpContext.Current.Session("SPs")
            End If
            If SalesPersons.Count > 0 Then
                CustomerObj = AppSpecificFunc.GetActiveCustomerByCodeORName("NameByCode", HeaderObject.CustomerCode, SalesPersons)
            End If
            If Not IsNothing(CustomerObj) Then
                HeaderObject.SlpCode = CustomerObj.SlpCode
            End If

            Using SqlCon As New SqlConnection(CURD.GetConnectionString)

                SqlCon.Open()

                Dim SqlTrans As SqlTransaction = SqlCon.BeginTransaction()

                Try
                    Dim ProceedNext As Boolean = True
                    Dim PrimaryKey As String = String.Empty

                    ' Insert Header Line
                    If ProceedNext = True Then
                        'Set Headerobject Document Number

                        HeaderObject.Status = DocumentStatus
                        ProceedNext = CURD.InsertDataTransaction(HeaderObject, SqlCon, SqlTrans, PrimaryKey, True)
                        DocuNo = PrimaryKey
                    Else
                        Throw New System.Exception("Header Object insertion error in Sales Order page")
                    End If

                    If ProceedNext = True Then
                        ' Insert Line Items
                        Dim LineItemsObject() As SOLines = TryCast(GridLinesToObjectsPrepareData(PrimaryKey), SOLines())
                        For Each Obj As SOLines In LineItemsObject
                            ProceedNext = CURD.InsertDataTransaction(Obj, SqlCon, SqlTrans, PrimaryKey, False)
                            If ProceedNext = False Then
                                Throw New System.Exception("Line Item insertion error in Sales Order page")
                            End If
                        Next
                    Else
                        Throw New System.Exception("Line Item insertion error in Sales Order page")
                    End If


                    SqlTrans.Commit()

                Catch ex As Exception
                    SqlTrans.Rollback()
                    SqlTrans.Dispose()
                    AppSpecificFunc.WriteLog(ex)
                    ReturnResult = False
                End Try

            End Using
            Return ReturnResult
        Catch ex As Exception
            AppSpecificFunc.WriteLog(ex)
            Return False
        End Try
    End Function
    Protected Function UpdateData(ByVal DocumentStatus As DocStatus, ByRef isDocStatusClosed As Boolean) As Boolean

        Try
            Dim ReturnResult As Boolean = True
            Dim HeaderObject As SOHeader = TryCast(FormToObjectPrepareData("Update"), SOHeader)
            HeaderObject.Status = DocumentStatus
            If DocumentStatus = DocStatus.Status_Waiting_For_Syncing Then
                HeaderObject.SubmitToSAP = "Y"
            Else
                HeaderObject.SubmitToSAP = "N"
            End If
            Dim IdKey As String = String.Empty

            If IsNumeric(SONo.Text) Then
                IdKey = SONo.Text
            End If


            Using SqlCon As New SqlConnection(CURD.GetConnectionString)

                SqlCon.Open()

                Dim SqlTrans As SqlTransaction = SqlCon.BeginTransaction()

                Try
                    Dim ProceedNext As Boolean = True
                    Dim PrimaryKey As String = String.Empty


                    ' Update Header Line
                    If ProceedNext = True Then
                        Dim UQ As New UpdateQuery
                        UQ._InputTable = HeaderObject
                        Dim FilterTable As New SOHeader
                        FilterTable.IdKey = IdKey
                        FilterTable.Status = DocStatus.Status_Closed
                        UQ._FilterTable = FilterTable
                        UQ._DB = "Custom"
                        UQ._HasInBetweenConditions = False
                        UQ._HasWhereConditions = True
                        'Query Conditions List
                        Dim QryConditions As Dictionary(Of String, List(Of String)) = New Dictionary(Of String, List(Of String))

                        'Query Condition Groups
                        Dim ConditionsGrp1 As List(Of String) = New List(Of String)

                        'Query Conditions values
                        ConditionsGrp1.Add("IdKey=@Filter_IdKey")
                        ConditionsGrp1.Add("Status<>@Filter_Status")
                        QryConditions.Add(" AND ", ConditionsGrp1)
                        UQ._Conditions = QryConditions

                        ProceedNext = CURD.UpdateDataTransaction(UQ, SqlCon, SqlTrans, PrimaryKey, True)
                    Else
                        Throw New System.Exception("Header object update error in Sales order page")
                    End If

                    If PrimaryKey = String.Empty Then
                        isDocStatusClosed = True
                        Throw New System.Exception("Header object update error DocStatus closed in Sales order page")
                    End If

                    If ProceedNext = True Then
                        Dim DQ As New DeleteQuery
                        Dim FilterTable As New SOLines
                        FilterTable.IdKey = PrimaryKey
                        'Where Condition parameter


                        DQ._InputTable = FilterTable
                        DQ._DB = "Custom"
                        DQ._HasInBetweenConditions = False
                        DQ._HasWhereConditions = True

                        'Query Conditions List
                        Dim QryConditions As Dictionary(Of String, List(Of String)) = New Dictionary(Of String, List(Of String))

                        'Query Condition Groups
                        Dim ConditionsGrp1 As List(Of String) = New List(Of String)

                        'Query Conditions values
                        ConditionsGrp1.Add("IdKey=@IdKey")

                        QryConditions.Add(" AND ", ConditionsGrp1)

                        DQ._Conditions = QryConditions


                        ProceedNext = CURD.DeleteDataTransaction(DQ, SqlCon, SqlTrans, PrimaryKey, False)

                    Else
                        Throw New System.Exception("Line Item deletion error in Sales order page")
                    End If
                    If ProceedNext = True Then
                        ' Insert Line Items
                        Dim LineItemsObject() As SOLines = TryCast(GridLinesToObjectsPrepareData(PrimaryKey), SOLines())
                        For Each Obj As SOLines In LineItemsObject
                            ProceedNext = CURD.InsertDataTransaction(Obj, SqlCon, SqlTrans, PrimaryKey, False)
                            If ProceedNext = False Then
                                Throw New System.Exception("Line Item insertion error in  Sales order page")
                            End If



                        Next
                    Else
                        Throw New System.Exception("Line Item insertion error in  Sales order page")
                    End If


                    SqlTrans.Commit()

                Catch ex As Exception
                    SqlTrans.Rollback()
                    SqlTrans.Dispose()
                    AppSpecificFunc.WriteLog(ex)
                    ReturnResult = False
                End Try

            End Using
            Return ReturnResult
        Catch ex As Exception
            AppSpecificFunc.WriteLog(ex)
            Return False
        End Try
    End Function
    Protected Sub ShowMessage(ByVal Message As String)
        Try
            LblShowMsg.Text = Message
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "GPMS", "$(document).ready(function () { ShowMessage(); });", True)
        Catch ex As Exception
            AppSpecificFunc.WriteLog(ex)
        End Try
    End Sub
    Protected Sub ShowConfirmDialog(ByVal Message As String)
        Try
            LblConfirmMsg.Text = Message
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "GPMS", "$(document).ready(function () { ShowConfirmDialog(); });", True)
        Catch ex As Exception
            AppSpecificFunc.WriteLog(ex)
        End Try
    End Sub
    Protected Function GetStatus() As DocStatus
        Try
            If Status.Text = String.Empty Then
                Return DocStatus.Status_New
            Else
                Return AppSpecificFunc.GetStatusValue(Status.Text)
            End If
        Catch ex As Exception
            AppSpecificFunc.WriteLog(ex)
            Return DocStatus.Status_New
        End Try
    End Function
    Protected Sub DocStatusBasedButtonRestrictions()
        Try
            Dim DocStatus As DocStatus = GetStatus()
            Select Case DocStatus
                Case DocStatus.Status_New
                    btnNew.Enabled = True
                    btnSave.Enabled = True
                    btnSubmitToSAP.Enabled = True
                    btnCancel.Enabled = False
                    btnPreview.Enabled = False
                    MakeFormReadonlyBasedOnStatusandRole(False)
                Case DocStatus.Status_Open
                    btnNew.Enabled = True
                    btnSave.Enabled = True
                    btnSubmitToSAP.Enabled = True
                    btnCancel.Enabled = True
                    btnPreview.Enabled = True
                    MakeFormReadonlyBasedOnStatusandRole(False)
                Case DocStatus.Status_Closed
                    btnNew.Enabled = True
                    btnSave.Enabled = False
                    btnSubmitToSAP.Enabled = False
                    btnCancel.Enabled = False
                    btnPreview.Enabled = True
                    MakeFormReadonlyBasedOnStatusandRole(True)
                Case DocStatus.Status_Waiting_For_Syncing
                    btnNew.Enabled = True
                    btnSave.Enabled = False
                    btnSubmitToSAP.Enabled = False
                    btnCancel.Enabled = False
                    btnPreview.Enabled = True
                    MakeFormReadonlyBasedOnStatusandRole(True)
                Case DocStatus.Status_Sync_Failed
                    btnNew.Enabled = True
                    btnSave.Enabled = True
                    btnSubmitToSAP.Enabled = True
                    btnCancel.Enabled = True
                    btnPreview.Enabled = True
                    MakeFormReadonlyBasedOnStatusandRole(False)
                Case DocStatus.Status_Cancelled
                    btnNew.Enabled = True
                    btnSave.Enabled = False
                    btnSubmitToSAP.Enabled = False
                    btnCancel.Enabled = False
                    btnPreview.Enabled = True
                    MakeFormReadonlyBasedOnStatusandRole(True)
            End Select
            'RoleBasedButtonRestriction()
        Catch ex As Exception
            AppSpecificFunc.WriteLog(ex)
        End Try
    End Sub
    Protected Sub MakeFormReadonlyBasedOnStatusandRole(ByVal SetReadOnly As Boolean)
        Try
            Dim HeaderObject As New SOHeader
            AppSpecificFunc.MakeFormReadOnly(Me.Master.FindControl("MainContent"), HeaderObject, SetReadOnly)
        Catch ex As Exception
            AppSpecificFunc.WriteLog(ex)
        End Try
    End Sub
    Protected Sub InitialiseForm()
        Try
            LoadItemsFromDataBase(-1)
            LoadHeaderFromDataBase(-1)
            Status.Text = AppSpecificFunc.GetStatusName(1)
            SONo.Text = String.Empty
            SAPSONo.Text = String.Empty
            CustomerCode.Text = String.Empty
            CustomerName.Text = String.Empty
            Remarks.Text = String.Empty
            txtFindDocNo.Text = String.Empty
            txtFindSAPDocNo.Text = String.Empty
            DocDate.Text = Format(CDate(DateTime.Now), "yyyy-MM-dd")
            DeliveryDate.Text = Format(CDate(DateTime.Now), "yyyy-MM-dd")
            PaymentTerms.SelectedIndex = 0
            SubTotalAmt.Text = 0
            OrderDiscPrsnt.Text = 0
            OrderDiscAmt.Text = 0
            AmtAfterDiscount.Text = 0
            GSTPrsnt.Text = 0
            GSTAmount.Text = 0
            TotalSOAmount.Text = 0
            SalesOrderLinesGrid.EditIndex = -1
            ClearAddressLines()
        Catch ex As Exception
            AppSpecificFunc.WriteLog(ex)
        End Try
    End Sub
    Protected Function LoadHeaderFromDataBase(ByVal IdKey As String) As Boolean

        Try
            Dim ReturnResult As Boolean = False
            Dim SOHeaderObj As New SOHeader
            Dim ErrMsg As String = String.Empty
            Dim SQ As New SelectQuery

            'Where Condition parameter
            SOHeaderObj.IdKey = IdKey

            SQ._InputTable = SOHeaderObj
            SQ._DB = "Custom"
            SQ._HasInBetweenConditions = False
            SQ._HasWhereConditions = True

            'Query Conditions List
            Dim QryConditions As Dictionary(Of String, List(Of String)) = New Dictionary(Of String, List(Of String))

            'Query Condition Groups
            Dim ConditionsGrp1 As List(Of String) = New List(Of String)

            'Query Conditions values
            ConditionsGrp1.Add("IdKey=@IdKey")

            QryConditions.Add(" AND ", ConditionsGrp1)

            SQ._Conditions = QryConditions


            Dim ResultDataTable As New DataTable
            ResultDataTable = CURD.SelectAllData(SQ)

            AppSpecificFunc.DataTableToObject(SOHeaderObj, ResultDataTable)
            ObjectToFormHeadearPrepareData(SOHeaderObj)

            If ResultDataTable.Rows.Count > 0 Then
                ReturnResult = True
            Else
                ReturnResult = False
            End If
            Return ReturnResult
        Catch ex As Exception
            AppSpecificFunc.WriteLog(ex)
            Return False
        End Try
    End Function
    Protected Sub LoadItemsFromDataBase(ByVal IdKey As String)

        Try

            Dim SOLinesObj As New SOLines
            Dim ErrMsg As String = String.Empty
            Dim SQB As New SelectQuery

            'Where Condition parameter
            SOLinesObj.IdKey = IdKey

            SQB._InputTable = SOLinesObj
            SQB._DB = "Custom"
            SQB._HasInBetweenConditions = False
            SQB._HasWhereConditions = True

            'Query Conditions List
            Dim QryConditions As Dictionary(Of String, List(Of String)) = New Dictionary(Of String, List(Of String))

            'Query Condition Groups
            Dim ConditionsGrp1 As List(Of String) = New List(Of String)

            'Query Conditions values
            ConditionsGrp1.Add("IdKey=@IdKey")

            QryConditions.Add(" AND ", ConditionsGrp1)

            SQB._Conditions = QryConditions

            SQB._OrderBy = " LineNum ASC"


            Dim ResultDataTable As New DataTable
            ResultDataTable = CURD.SelectAllData(SQB)

            If Not IsNothing(ResultDataTable) Then
                ViewState("SalesOrderLines") = ResultDataTable.Copy()
                AppSpecificFunc.BindGridData(ResultDataTable, SalesOrderLinesGrid)
            End If


        Catch ex As Exception
            AppSpecificFunc.WriteLog(ex)
        End Try
    End Sub
    Protected Sub ObjectToFormHeadearPrepareData(ByVal Obj As Object)
        Try
            AppSpecificFunc.ObjectToPageControls(Me.Master.FindControl("MainContent"), Obj)
            BindShipToAddresses()
            ShipTo.SelectedIndex = ShipTo.Items.IndexOf(ShipTo.Items.FindByValue(Obj.ShipTo))
            If ShipTo.SelectedIndex > 0 Then
                LoadAddressDetails()
            End If
            If IsNumeric(Status.Text) Then
                Status.Text = AppSpecificFunc.GetStatusName(CType(Status.Text, Integer))
            End If

            If Obj.IdKey <> -1 Then
                SONo.Text = Obj.IdKey
            End If

            If DocDate.Text <> String.Empty Then
                Dim DocDateValue As New Date
                If Date.TryParse(DocDate.Text, DocDateValue) Then
                    DocDate.Text = Format(DocDateValue, "yyyy-MM-dd")
                End If
            End If
            If DeliveryDate.Text <> String.Empty Then
                Dim DeliveryDateValue As New Date
                If Date.TryParse(DeliveryDate.Text, DeliveryDateValue) Then
                    DeliveryDate.Text = Format(DeliveryDateValue, "yyyy-MM-dd")
                End If
            End If
        Catch ex As Exception
            AppSpecificFunc.WriteLog(ex)
        End Try
    End Sub
    Protected Sub ClearAddressLines()
        Try
            AddressLine1.Text = String.Empty
            AddressLine2.Text = String.Empty
            AddressLine3.Text = String.Empty
            AddressLine4.Text = String.Empty
            AddressLine5.Text = String.Empty
        Catch ex As Exception
            AppSpecificFunc.WriteLog(ex)
        End Try
    End Sub
    Protected Sub LoadAddressDetails()
        Try
            Dim AddressDetails As CRD1 = AppSpecificFunc.GetShipAddressDetailsByAddressID(ShipTo.SelectedValue, CustomerCode.Text)

            AddressLine1.Text = AddressDetails.Street
            AddressLine2.Text = AddressDetails.Block
            AddressLine3.Text = AddressDetails.City
            AddressLine4.Text = AddressDetails.ZipCode
            AddressLine5.Text = AddressDetails.County
        Catch ex As Exception
            AppSpecificFunc.WriteLog(ex)
        End Try
    End Sub
    Protected Sub BindShipToAddresses()
        Try
            Dim CardCode As String = CustomerCode.Text.Trim
            If CardCode.Equals(String.Empty) Then
                CardCode = "-1"
            End If
            Dim AddressesTable As DataTable = AppSpecificFunc.GetShipAddressIDsByCardCode(CardCode)
            ShipTo.DataSource = AddressesTable
            ShipTo.DataTextField = "Address"
            ShipTo.DataValueField = "Address"
            ShipTo.DataBind()

            ShipTo.Items.Insert(0, New ListItem("Select", ""))
            ClearAddressLines()
        Catch ex As Exception
            AppSpecificFunc.WriteLog(ex)
        End Try
    End Sub
    Protected Sub BindPaymentTerms()
        Try
            Dim PaymentTermsTable As DataTable = AppSpecificFunc.GetAllPaymentTerms
            PaymentTerms.DataSource = PaymentTermsTable
            PaymentTerms.DataTextField = "PymntGroup"
            PaymentTerms.DataValueField = "GroupNum"
            PaymentTerms.DataBind()
            PaymentTerms.Items.Insert(0, New ListItem("Select", ""))
        Catch ex As Exception
            AppSpecificFunc.WriteLog(ex)
        End Try
    End Sub
    Protected Sub UpdateLineTotal(ByRef GvRow As GridViewRow)
        Dim Qty As Decimal = 0
        Dim UnitPrice As Decimal = 0
        Dim DiscountPercent As Decimal = 0
        Dim AmountAfterDiscountPercent As Decimal = 0
        Dim txtQty As New TextBox
        Dim txtUnitPrice As New TextBox
        Dim txtDiscountPercent As New TextBox
        Dim txtAmountAfterDiscountPercent As New TextBox
        txtQty = TryCast(GvRow.FindControl("Qty"), TextBox)
        txtUnitPrice = TryCast(GvRow.FindControl("Price"), TextBox)
        txtDiscountPercent = TryCast(GvRow.FindControl("LineLevelDiscount"), TextBox)
        txtAmountAfterDiscountPercent = TryCast(GvRow.FindControl("LineTotal"), TextBox)

        If txtQty.Text = String.Empty Then
            Qty = 0
        ElseIf Decimal.TryParse(txtQty.Text, Qty) Then
            Qty = txtQty.Text
        End If

        If txtUnitPrice.Text = String.Empty Then
            UnitPrice = 0
        ElseIf Decimal.TryParse(txtUnitPrice.Text, UnitPrice) Then
            UnitPrice = txtUnitPrice.Text
        End If

        If txtDiscountPercent.Text = String.Empty Then
            DiscountPercent = 0
        ElseIf Decimal.TryParse(txtDiscountPercent.Text, DiscountPercent) Then
            DiscountPercent = txtDiscountPercent.Text
        End If

        If txtAmountAfterDiscountPercent.Text = String.Empty Then
            AmountAfterDiscountPercent = 0
        ElseIf Decimal.TryParse(txtAmountAfterDiscountPercent.Text, AmountAfterDiscountPercent) Then
            AmountAfterDiscountPercent = txtAmountAfterDiscountPercent.Text
        End If


        Dim LineTotal As Decimal = 0
        Dim DiscountAmount As Decimal = 0
        Dim LineTotalAfterDiscount As Decimal = 0
        LineTotal = (Qty * UnitPrice)
        DiscountAmount = ((LineTotal * DiscountPercent) / 100)
        LineTotalAfterDiscount = LineTotal - DiscountAmount

        txtAmountAfterDiscountPercent.Text = Math.Round(LineTotalAfterDiscount, 2, MidpointRounding.AwayFromZero)

    End Sub
    Protected Sub CalculateOrderDiscount()
        Try


            Dim SubTotalAmount As Decimal = 0
            Dim DiscountPercent As Decimal = 0
            Dim DiscountAmount As Decimal = 0
            Dim AmountAfterDiscount As Decimal = 0

            If OrderDiscPrsnt.Text = String.Empty Then
                OrderDiscPrsnt.Text = 0
            End If

            If Decimal.TryParse(SubTotalAmt.Text, SubTotalAmount) And Decimal.TryParse(OrderDiscPrsnt.Text, DiscountPercent) Then

                DiscountAmount = (SubTotalAmount * DiscountPercent) / 100

                DiscountAmount = Math.Round(DiscountAmount, 2, MidpointRounding.AwayFromZero)

                OrderDiscAmt.Text = DiscountAmount

                AmountAfterDiscount = SubTotalAmount - DiscountAmount

                AmtAfterDiscount.Text = Math.Round(AmountAfterDiscount, 2, MidpointRounding.AwayFromZero)

            End If

            CalculateGST()

        Catch ex As Exception
            AppSpecificFunc.WriteLog(ex)
        End Try
    End Sub
    Protected Sub CalculateGST()
        Dim GSTPercentValue As Decimal = 0
        Dim GSTAmountValue As Decimal = 0
        Dim AmountAfterDiscount As Decimal = 0
        Dim AmountAfterGSTAmount As Decimal = 0
        Dim TotalInvoiceAmount As Decimal = 0

        If GSTPrsnt.Text = String.Empty Then
            GSTPrsnt.Text = 0
        End If

        If GSTAmount.Text = String.Empty Then
            GSTAmount.Text = 0
        End If

        If Decimal.TryParse(GSTPrsnt.Text, GSTPercentValue) And Decimal.TryParse(AmtAfterDiscount.Text, AmountAfterDiscount) Then
            GSTAmountValue = (AmountAfterDiscount * GSTPercentValue) / 100
            GSTAmountValue = Math.Round(GSTAmountValue, 2, MidpointRounding.AwayFromZero)
            GSTAmount.Text = GSTAmountValue
            AmountAfterDiscount = AmtAfterDiscount.Text
            TotalInvoiceAmount = AmountAfterDiscount + GSTAmountValue
            TotalInvoiceAmount = Math.Round(TotalInvoiceAmount, 2, MidpointRounding.AwayFromZero)
            TotalSOAmount.Text = TotalInvoiceAmount
        End If
    End Sub
    Protected Sub InsertGridLine()
        Try
            If Page.IsValid Then
                'Update Line Total
                UpdateLineTotal(SalesOrderLinesGrid.FooterRow)

                Dim LineItems As New SOLines
                Dim GridDataTable As DataTable = TryCast(ViewState("SalesOrderLines"), DataTable)

                If Not IsNothing(GridDataTable) Then
                    Dim GridDataRow As DataRow = GridDataTable.NewRow
                    AppSpecificFunc.GridRowToObject(SalesOrderLinesGrid.FooterRow, LineItems)

                    AppSpecificFunc.ObjectToDataRow(LineItems, GridDataRow)
                    GridDataRow("LineNum") = GridDataTable.Rows.Count + 1
                    GridDataTable.Rows.Add(GridDataRow)

                    AppSpecificFunc.BindGridData(GridDataTable, SalesOrderLinesGrid)
                End If
            End If
        Catch ex As Exception
            AppSpecificFunc.WriteLog(ex)
        End Try
    End Sub
#End Region

#Region "Custom Validation function"

    Protected Sub ItemCodeValidator(ByVal source As System.Object, ByVal args As System.Web.UI.WebControls.ServerValidateEventArgs)
        Try



            Dim CustomValidatorControl As CustomValidator = TryCast(source, CustomValidator)
            'Is Customer added checking
            If CustomerCode.Text = String.Empty Or CustomerName.Text = String.Empty Then
                args.IsValid = False
                CustomValidatorControl.ErrorMessage = "Please add valid customer information before adding items"

            Else
                If Not IsNothing(CustomValidatorControl) Then
                    Dim GridRow As GridViewRow = CustomValidatorControl.Parent.Parent

                    If Not IsNothing(GridRow) Then

                        Dim txtItemCode As TextBox = TryCast(GridRow.FindControl("ItemCode"), TextBox)
                        Dim txtQty As TextBox = TryCast(GridRow.FindControl("Qty"), TextBox)
                        Dim ItemName As String = String.Empty
                        If Not IsNothing(txtItemCode) Then


                            Dim ErrMsg As String = String.Empty
                            Dim Description As String = String.Empty
                            Dim ItemObj As New OITM
                            ItemObj = AppSpecificFunc.GetActiveItemByCodeORName("NameByCode", txtItemCode.Text)



                            If Not IsNothing(ItemObj) Then
                                Description = ItemObj.ItemName
                            End If
                            If Description <> String.Empty Then
                                args.IsValid = True
                            Else
                                args.IsValid = False
                                CustomValidatorControl.ErrorMessage = "Valid Itemcode required"
                            End If


                            'If args.IsValid = True Then
                            '    If txtQty.Text <> String.Empty Then
                            '        Dim Qty As Decimal = 0
                            '        If Decimal.TryParse(txtQty.Text, Qty) Then
                            '            args.IsValid = AppSpecificFunc.isThereEnoughStock(ItemObj.ItemCode, Qty)
                            '            CustomValidatorControl.ErrorMessage = "Not enough stock available"
                            '        End If
                            '    End If
                            'End If

                        End If
                    End If

                Else
                    args.IsValid = True
                End If

            End If

        Catch ex As Exception
            AppSpecificFunc.WriteLog(ex)
        End Try
    End Sub
    Protected Sub QtyValidator(ByVal source As System.Object, ByVal args As System.Web.UI.WebControls.ServerValidateEventArgs)
        Try
            Dim CustomValidatorControl As CustomValidator = TryCast(source, CustomValidator)

            If Not IsNothing(CustomValidatorControl) Then
                Dim GridRow As GridViewRow = CustomValidatorControl.Parent.Parent

                If Not IsNothing(GridRow) Then

                    Dim txtQty As TextBox = TryCast(GridRow.FindControl("Qty"), TextBox)
                    Dim Qty As Decimal
                    If Not IsNothing(txtQty) Then
                        If txtQty.Text = String.Empty Then
                            txtQty.Text = 0

                        ElseIf Not Decimal.TryParse(txtQty.Text, Qty) Then
                            args.IsValid = False
                            'ElseIf Qty < 0 Then
                            '    args.IsValid = False
                        Else

                        End If
                    End If
                End If

            Else
                args.IsValid = True
            End If

        Catch ex As Exception
            AppSpecificFunc.WriteLog(ex)
        End Try
    End Sub
    Protected Sub UnitPriceValidator(ByVal source As System.Object, ByVal args As System.Web.UI.WebControls.ServerValidateEventArgs)
        Try
            Dim CustomValidatorControl As CustomValidator = TryCast(source, CustomValidator)

            If Not IsNothing(CustomValidatorControl) Then
                Dim GridRow As GridViewRow = CustomValidatorControl.Parent.Parent

                If Not IsNothing(GridRow) Then

                    Dim txtUnitPrice As TextBox = TryCast(GridRow.FindControl("Price"), TextBox)
                    Dim UnitPrice As Decimal
                    If Not IsNothing(txtUnitPrice) Then
                        If txtUnitPrice.Text = String.Empty Then
                            txtUnitPrice.Text = 0
                        ElseIf Not Decimal.TryParse(txtUnitPrice.Text, UnitPrice) Then
                            args.IsValid = False
                        ElseIf UnitPrice < 0 Then
                            'args.IsValid = False
                        End If
                    End If
                End If

            Else
                args.IsValid = True
            End If

        Catch ex As Exception
            AppSpecificFunc.WriteLog(ex)
        End Try
    End Sub
    Protected Sub DiscPrsntValidator(ByVal source As System.Object, ByVal args As System.Web.UI.WebControls.ServerValidateEventArgs)
        Try
            Dim CustomValidatorControl As CustomValidator = TryCast(source, CustomValidator)

            If Not IsNothing(CustomValidatorControl) Then
                Dim GridRow As GridViewRow = CustomValidatorControl.Parent.Parent

                If Not IsNothing(GridRow) Then

                    Dim txtDiscPrsnt As TextBox = TryCast(GridRow.FindControl("LineLevelDiscount"), TextBox)
                    Dim DiscPrsnt As Decimal
                    If Not IsNothing(txtDiscPrsnt) Then
                        If txtDiscPrsnt.Text = String.Empty Then
                            txtDiscPrsnt.Text = 0
                        ElseIf Not Decimal.TryParse(txtDiscPrsnt.Text, DiscPrsnt) Then
                            args.IsValid = False
                        ElseIf DiscPrsnt < 0 Then
                            args.IsValid = False
                        End If
                    End If
                End If

            Else
                args.IsValid = True
            End If

        Catch ex As Exception
            AppSpecificFunc.WriteLog(ex)
        End Try
    End Sub
    Protected Sub LineTotalValidator(ByVal source As System.Object, ByVal args As System.Web.UI.WebControls.ServerValidateEventArgs)
        Try
            Dim CustomValidatorControl As CustomValidator = TryCast(source, CustomValidator)

            If Not IsNothing(CustomValidatorControl) Then
                Dim GridRow As GridViewRow = CustomValidatorControl.Parent.Parent

                If Not IsNothing(GridRow) Then

                    Dim txtAmtAftLineDisc As TextBox = TryCast(GridRow.FindControl("LineTotal"), TextBox)
                    Dim AmtAftLineDisc As Decimal
                    If Not IsNothing(txtAmtAftLineDisc) Then
                        If txtAmtAftLineDisc.Text = String.Empty Then
                            txtAmtAftLineDisc.Text = 0
                        ElseIf Not Decimal.TryParse(txtAmtAftLineDisc.Text, AmtAftLineDisc) Then
                            args.IsValid = False
                            'ElseIf AmtAftLineDisc < 0 Then
                            '    args.IsValid = False
                        End If
                    End If
                End If

            Else
                args.IsValid = True
            End If

        Catch ex As Exception
            AppSpecificFunc.WriteLog(ex)
        End Try
    End Sub
    Protected Sub ShipToValidator(ByVal source As System.Object, ByVal args As System.Web.UI.WebControls.ServerValidateEventArgs)
        Try
            If ShipTo.SelectedIndex <= 0 Then
                args.IsValid = False
            End If
        Catch ex As Exception
            AppSpecificFunc.WriteLog(ex)
        End Try
    End Sub
    Protected Sub DocDateValidator(ByVal source As System.Object, ByVal args As System.Web.UI.WebControls.ServerValidateEventArgs)
        Dim DocuDate As New Date
        If DocDate.Text <> String.Empty Then
            If Date.TryParseExact(DocDate.Text, "yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture, DateTimeStyles.None, DocuDate) = False Then
                args.IsValid = False
            End If
        Else
            DocDateRequired.ErrorMessage = "Document Date is Required"
            args.IsValid = False
        End If

    End Sub
    Protected Sub DeliveryDateValidator(ByVal source As System.Object, ByVal args As System.Web.UI.WebControls.ServerValidateEventArgs)
        Dim DeliDate As New Date
        If DeliveryDate.Text <> String.Empty Then
            If Date.TryParseExact(DeliveryDate.Text, "yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture, DateTimeStyles.None, DeliDate) = False Then
                args.IsValid = False
            End If
        Else
            DeliveryDateRequired.ErrorMessage = "Delivery Date is Required"
            args.IsValid = False
        End If

    End Sub
    Protected Sub SubTotalAmtValidator(ByVal source As System.Object, ByVal args As System.Web.UI.WebControls.ServerValidateEventArgs)
        Try
            Dim SubTotalAmtValue As Decimal = 0
            If SubTotalAmt.Text = String.Empty Then
                args.IsValid = False
            ElseIf Not Decimal.TryParse(SubTotalAmt.Text, SubTotalAmtValue) Then
                args.IsValid = False
            End If
        Catch ex As Exception
            AppSpecificFunc.WriteLog(ex)
        End Try
    End Sub
    Protected Sub OrderDiscPrsntValidator(ByVal source As System.Object, ByVal args As System.Web.UI.WebControls.ServerValidateEventArgs)
        Try
            Dim oOrderDiscPercentValue As Decimal = 0
            If OrderDiscPrsnt.Text = String.Empty Then
                args.IsValid = False
            ElseIf Not Decimal.TryParse(OrderDiscPrsnt.Text, oOrderDiscPercentValue) Then
                args.IsValid = False
            End If
        Catch ex As Exception
            AppSpecificFunc.WriteLog(ex)
        End Try
    End Sub
    Protected Sub OrderDiscAmtValidator(ByVal source As System.Object, ByVal args As System.Web.UI.WebControls.ServerValidateEventArgs)
        Try
            Dim oOrderDiscAmountValue As Decimal = 0
            If OrderDiscAmt.Text = String.Empty Then
                args.IsValid = False
            ElseIf Not Decimal.TryParse(OrderDiscAmt.Text, oOrderDiscAmountValue) Then
                args.IsValid = False
            End If
        Catch ex As Exception
            AppSpecificFunc.WriteLog(ex)
        End Try
    End Sub
    Protected Sub AmtAfterDiscountValidator(ByVal source As System.Object, ByVal args As System.Web.UI.WebControls.ServerValidateEventArgs)
        Try
            Dim AmtAfterDiscountValue As Decimal = 0
            If AmtAfterDiscount.Text = String.Empty Then
                args.IsValid = False
            ElseIf Not Decimal.TryParse(AmtAfterDiscount.Text, AmtAfterDiscountValue) Then
                args.IsValid = False
            End If
        Catch ex As Exception
            AppSpecificFunc.WriteLog(ex)
        End Try
    End Sub
    Protected Sub GSTPrsntValidator(ByVal source As System.Object, ByVal args As System.Web.UI.WebControls.ServerValidateEventArgs)
        Try
            Dim GSTPrsntValue As Decimal = 0
            If GSTPrsnt.Text = String.Empty Then
                GSTPrsnt.Text = 0
            ElseIf Not Decimal.TryParse(GSTPrsnt.Text, GSTPrsntValue) Then
                args.IsValid = False
            End If
        Catch ex As Exception
            AppSpecificFunc.WriteLog(ex)
        End Try
    End Sub
    Protected Sub GSTAmountValidator(ByVal source As System.Object, ByVal args As System.Web.UI.WebControls.ServerValidateEventArgs)
        Try
            Dim GSTAmtValue As Decimal = 0
            If GSTAmount.Text = String.Empty Then
                GSTAmount.Text = 0
            ElseIf Not Decimal.TryParse(GSTAmount.Text, GSTAmtValue) Then
                args.IsValid = False
            End If
        Catch ex As Exception
            AppSpecificFunc.WriteLog(ex)
        End Try
    End Sub
    Protected Sub TotalInvAmountValidator(ByVal source As System.Object, ByVal args As System.Web.UI.WebControls.ServerValidateEventArgs)
        Try
            Dim TotalInvAmountValue As Decimal = 0
            If TotalSOAmount.Text = String.Empty Then
                args.IsValid = False
            ElseIf Not Decimal.TryParse(TotalSOAmount.Text, TotalInvAmountValue) Then
                args.IsValid = False
            End If
        Catch ex As Exception
            AppSpecificFunc.WriteLog(ex)
        End Try
    End Sub

#End Region
#Region "Event Handlers - Grid View"

    Private Sub SalesOrderLinesGrid_DataBinding(sender As Object, e As System.EventArgs) Handles SalesOrderLinesGrid.DataBinding

        MakeGridReadyOnlyBasedOnStatusandRole()
    End Sub
    Private Sub SalesOrderLinesGrid_RowCancelingEdit(sender As Object, e As System.Web.UI.WebControls.GridViewCancelEditEventArgs) Handles SalesOrderLinesGrid.RowCancelingEdit
        Try
            SalesOrderLinesGrid.EditIndex = -1

            Dim GridDataTable As DataTable = TryCast(ViewState("SalesOrderLines"), DataTable)
            If Not IsNothing(GridDataTable) Then
                AppSpecificFunc.BindGridData(GridDataTable, SalesOrderLinesGrid)
            End If
        Catch ex As Exception
            AppSpecificFunc.WriteLog(ex)
        End Try
    End Sub
    Private Sub SalesOrderLinesGrid_RowCommand(sender As Object, e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles SalesOrderLinesGrid.RowCommand
        Try
            If e.CommandName = "Insert" Then

                If Page.IsValid Then

                    'Price is less than or equal to zero
                    Dim FooterRow As GridViewRow = SalesOrderLinesGrid.FooterRow
                    Dim txtItemCode As TextBox = TryCast(FooterRow.FindControl("ItemCode"), TextBox)
                    Dim txtUnitPrice As TextBox = TryCast(FooterRow.FindControl("Price"), TextBox)
                    Dim txtQty As TextBox = TryCast(FooterRow.FindControl("Qty"), TextBox)
                    Dim Qty As Decimal = 0
                    Decimal.TryParse(txtQty.Text, Qty)
                    Dim NotEnoughStock As Boolean = False
                    NotEnoughStock = AppSpecificFunc.isThereEnoughStock(txtItemCode.Text, Qty)

                    If txtUnitPrice.Text.Trim = "0" And NotEnoughStock = False Then
                        ViewState("ConfirmDialogId") = "PriceAndInsufficentConfirmation"
                        ShowConfirmDialog("Not enough stock is available and Is this an FOC item? Can add this item")
                    ElseIf txtUnitPrice.Text.Trim = "0" Then
                        ViewState("ConfirmDialogId") = "PriceConfirmation"
                        ShowConfirmDialog("Is this an FOC item, Can add this item?")
                    ElseIf NotEnoughStock = False Then
                        ViewState("ConfirmDialogId") = "InsufficientConfirmation"
                        ShowConfirmDialog("Not enought stock available, Can add this item?")
                    Else
                        InsertGridLine()
                    End If
                End If
            Else

            End If





        Catch ex As Exception
            AppSpecificFunc.WriteLog(ex)
        End Try
    End Sub
    Private Sub SalesOrderLinesGrid_RowDataBound(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles SalesOrderLinesGrid.RowDataBound
        Try
            'If SalesOrderLinesGrid.Rows.Count > 0 Then
            '    CustomerCode.ReadOnly = True
            '    CustomerName.ReadOnly = True
            'Else
            '    CustomerCode.ReadOnly = False
            '    CustomerName.ReadOnly = False
            'End If

            Dim SubTotalAmount As Decimal = 0
            For Each GRV As GridViewRow In SalesOrderLinesGrid.Rows
                Dim LblAmountAftLineDisc As Label = TryCast(GRV.FindControl("lblLineTotal"), Label)
                If Not IsNothing(LblAmountAftLineDisc) Then

                    Dim AmountAftLineDisc As Decimal = 0

                    If LblAmountAftLineDisc.Text <> String.Empty Then
                        If Decimal.TryParse(LblAmountAftLineDisc.Text, AmountAftLineDisc) Then
                            SubTotalAmount = SubTotalAmount + AmountAftLineDisc
                        End If
                    End If

                End If

            Next
            SubTotalAmt.Text = SubTotalAmount
            CalculateOrderDiscount()
            CalculateGST()


        Catch ex As Exception
            AppSpecificFunc.WriteLog(ex)
        End Try
    End Sub
    Private Sub SalesOrderLinesGrid_RowDeleting(sender As Object, e As System.Web.UI.WebControls.GridViewDeleteEventArgs) Handles SalesOrderLinesGrid.RowDeleting
        Try

            SalesOrderLinesGrid.EditIndex = -1
            Dim GridDataTable As DataTable = TryCast(ViewState("SalesOrderLines"), DataTable)
            If Not IsNothing(GridDataTable) Then
                GridDataTable.Rows(e.RowIndex).Delete()
                GridDataTable.AcceptChanges()
                For i As Integer = 1 To GridDataTable.Rows.Count
                    Dim GridDataRow As DataRow = GridDataTable.Rows(i - 1)
                    GridDataRow("LineNum") = i
                Next
                AppSpecificFunc.BindGridData(GridDataTable, SalesOrderLinesGrid)
            End If
        Catch ex As Exception
            AppSpecificFunc.WriteLog(ex)
        End Try
    End Sub
    Private Sub SalesOrderLinesGrid_RowEditing(sender As Object, e As System.Web.UI.WebControls.GridViewEditEventArgs) Handles SalesOrderLinesGrid.RowEditing
        Try
            SalesOrderLinesGrid.EditIndex = e.NewEditIndex

            Dim GridDataTable As DataTable = TryCast(ViewState("SalesOrderLines"), DataTable)
            If Not IsNothing(GridDataTable) Then
                AppSpecificFunc.BindGridData(GridDataTable, SalesOrderLinesGrid)
            End If
        Catch ex As Exception
            AppSpecificFunc.WriteLog(ex)
        End Try
    End Sub
    Private Sub SalesOrderLinesGrid_RowUpdating(sender As Object, e As System.Web.UI.WebControls.GridViewUpdateEventArgs) Handles SalesOrderLinesGrid.RowUpdating
        Try



            Dim LineItems As New SOLines
            SalesOrderLinesGrid.EditIndex = -1
            Dim GridDataTable As DataTable = TryCast(ViewState("SalesOrderLines"), DataTable)
            If Not IsNothing(GridDataTable) Then

                Dim GridDataRow As DataRow = GridDataTable.Rows(e.RowIndex)
                Dim GridRow As GridViewRow = SalesOrderLinesGrid.Rows(e.RowIndex)
                'Update Line Total
                UpdateLineTotal(GridRow)

                AppSpecificFunc.GridRowToObject(SalesOrderLinesGrid.Rows(e.RowIndex), LineItems)
                AppSpecificFunc.ObjectToDataRow(LineItems, GridDataRow)

                AppSpecificFunc.BindGridData(GridDataTable, SalesOrderLinesGrid)
            End If
        Catch ex As Exception
            AppSpecificFunc.WriteLog(ex)
        End Try
    End Sub





#End Region









End Class