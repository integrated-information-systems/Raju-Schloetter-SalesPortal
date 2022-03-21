Imports SchloetterSalesPortal.Models

Public Class CustomerEnquiry
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

#End Region
#Region "Form Event Handlers"
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try

        Catch ex As Exception
            AppSpecificFunc.WriteLog(ex)
        End Try
    End Sub
    Protected Sub btnSearch_Click(sender As Object, e As EventArgs) Handles btnSearch.Click
        Try

            If Page.IsValid Then

                Dim CustomerHeader As OCRD = Nothing
                If CustomerCode.Text <> String.Empty Then
                    CustomerHeader = AppSpecificFunc.GetCustomerHeaderInfo("DetailsByCode", CustomerCode.Text)
                ElseIf CustomerName.Text <> String.Empty Then
                    CustomerHeader = AppSpecificFunc.GetCustomerHeaderInfo("DetailsByName", CustomerName.Text)
                End If
                If Not IsNothing(CustomerHeader) Then
                    ClearForm()
                    FoundCustomerCode.Text = CustomerHeader.CardCode
                    FoundCustomerName.Text = CustomerHeader.CardName
                    Tel1.Text = CustomerHeader.Phone1
                    Tel2.Text = CustomerHeader.Phone2
                    Cellular.Text = CustomerHeader.Cellular
                    Email.Text = CustomerHeader.E_Mail
                    Fax.Text = CustomerHeader.Fax
                    If CustomerHeader.ValidFor = "Y" Then
                        Active.Text = "Yes"
                    Else
                        Active.Text = "No"
                    End If
                    DefaultCntPerson.Text = CustomerHeader.CntctPrsn
                    If CustomerHeader.GroupNum <> String.Empty Then
                        PaymentTerms.Text = AppSpecificFunc.GetPaymentTermNameByCode(CustomerHeader.GroupNum)
                    End If

                    If CustomerHeader.BillToDef <> String.Empty Then
                        BillingAddress.Text = AppSpecificFunc.GetAllDefaultCustomerAddress(CustomerHeader.BillToDef, CustomerHeader.CardCode, "Billing")
                    Else
                        BillingAddress.Text = String.Empty
                    End If
                    If CustomerHeader.Shiptodef <> String.Empty Then
                        DeliveryAddress.Text = AppSpecificFunc.GetAllDefaultCustomerAddress(CustomerHeader.Shiptodef, CustomerHeader.CardCode, "Shipping")
                    Else
                        DeliveryAddress.Text = String.Empty
                    End If
                End If
            End If

        Catch ex As Exception
            AppSpecificFunc.WriteLog(ex)
        End Try
    End Sub
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
    Protected Sub btnClear_Click(sender As Object, e As EventArgs) Handles btnClear.Click
        Try
            ClearForm(True)
        Catch ex As Exception
            AppSpecificFunc.WriteLog(ex)
        End Try
    End Sub
#End Region
#Region "Form Related functions"
    Protected Sub ClearForm(Optional ByVal NeedClearSearchField As Boolean = False)
        If (NeedClearSearchField = True) Then
            CustomerCode.Text = String.Empty
            CustomerName.Text = String.Empty
        End If

        FoundCustomerCode.Text = String.Empty
        FoundCustomerName.Text = String.Empty
        BillingAddress.Text = String.Empty
        DeliveryAddress.Text = String.Empty
        PaymentTerms.Text = String.Empty
        DefaultCntPerson.Text = String.Empty
        Cellular.Text = String.Empty
        Tel1.Text = String.Empty
        Tel2.Text = String.Empty
        Email.Text = String.Empty
        Fax.Text = String.Empty
        Active.Text = String.Empty
    End Sub
#End Region



End Class