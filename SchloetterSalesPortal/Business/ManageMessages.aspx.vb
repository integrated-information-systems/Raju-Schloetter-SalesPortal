Imports SchloetterSalesPortal.Models

Public Class ManageMessages
    Inherits System.Web.UI.Page
#Region "Form Related functions"
    Private Sub ClearForm()
        Try
            MessageTitle.Text = String.Empty
            Message.Text = String.Empty
            MessagesListGrid.SelectedIndex = -1
            LoadData()
        Catch ex As Exception
            AppSpecificFunc.WriteLog(ex)
        End Try
    End Sub
    Protected Function LoadData(Optional ByVal Initialise As Boolean = True) As DataTable

        Try

            Dim MessagesObj As New Messages
            Dim CSQ As New SelectQuery

            'Query Conditions List
            Dim QryConditions As Dictionary(Of String, List(Of String)) = New Dictionary(Of String, List(Of String))

            'Query Condition Groups
            Dim ConditionsGrp1 As List(Of String) = New List(Of String)


            If Initialise = True Then

                'MessagesObj.IdKey = -1
                ''Query Conditions values
                'ConditionsGrp1.Add("IdKey=@IdKey")

            Else

            End If

            If ConditionsGrp1.Count > 0 Then
                QryConditions.Add(" AND ", ConditionsGrp1)
            End If

            CSQ._InputTable = MessagesObj
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
            ResultDataTable = CURD.SelectAllData(CSQ)

            ViewState("RowCount") = ResultDataTable.Rows.Count
            AppSpecificFunc.BindGridData(ResultDataTable, MessagesListGrid)
            Return ResultDataTable

        Catch ex As Exception
            AppSpecificFunc.WriteLog(ex)
            Return Nothing
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
#End Region
#Region "Form Event Handlers"
    Private Sub btnNew_Click(sender As Object, e As System.EventArgs) Handles btnNew.Click
        Try
            ClearForm()
        Catch ex As Exception
            AppSpecificFunc.WriteLog(ex)
        End Try
    End Sub

    Private Sub MessagesListGrid_PageIndexChanging(sender As Object, e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles MessagesListGrid.PageIndexChanging
        Try
            MessagesListGrid.PageIndex = e.NewPageIndex
            LoadData()
        Catch ex As Exception
            AppSpecificFunc.WriteLog(ex)
        End Try
    End Sub

    Private Sub MessagesListGrid_RowCommand(sender As Object, e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles MessagesListGrid.RowCommand
        Try

            Dim RIndex = CInt(e.CommandArgument) - CInt((MessagesListGrid.PageIndex * MessagesListGrid.PageSize))
            Dim MessageID As String = MessagesListGrid.DataKeys(RIndex).Value

            Dim MsgObj As New Messages
            MsgObj.IdKey = MessageID
            Select Case e.CommandName
                Case "Select"
                    Dim QryConditions As Dictionary(Of String, List(Of String)) = New Dictionary(Of String, List(Of String))

                    'Query Condition Groups
                    Dim ConditionsGrp1 As List(Of String) = New List(Of String)
                    'Query Conditions values
                    ConditionsGrp1.Add("Idkey=@Idkey")
                    'ConditionsGrp1.Add("ItemName=@ItemName")
                    QryConditions.Add(" AND ", ConditionsGrp1)

                    Dim SQ As New SelectQuery
                    SQ._InputTable = MsgObj
                    SQ._DB = "Custom"
                    SQ._HasInBetweenConditions = False
                    SQ._HasWhereConditions = True
                    SQ._Conditions = QryConditions
                    Dim ResultDataTable As New DataTable
                    ResultDataTable = CURD.SelectAllData(SQ)
                    If ResultDataTable.Rows.Count > 0 Then
                        AppSpecificFunc.DataTableToObject(MsgObj, ResultDataTable)
                        MessageTitle.Text = MsgObj.MessageTitle
                        Message.Text = MsgObj.Message
                        Active.Checked = CBool(MsgObj.Active)
                    End If
                Case "DeleteItem"
                    Dim DQ As New DeleteQuery
                    Dim FilterTable As New Messages
                    FilterTable.IdKey = MessageID
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


                    CURD.DeleteData(DQ)
                    ClearForm()
            End Select
          
        Catch ex As Exception
            AppSpecificFunc.WriteLog(ex)
        End Try
    End Sub
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not IsPostBack Then
                LoadData()
            End If

        Catch ex As Exception
            AppSpecificFunc.WriteLog(ex)
        End Try
    End Sub

    Private Sub btnAddOrUpdate_Click(sender As Object, e As System.EventArgs) Handles btnAddOrUpdate.Click
        Try
            If Page.IsValid Then

                Dim MsgObj As New Messages
                MsgObj.Message = Message.Text
                MsgObj.MessageTitle = MessageTitle.Text
                MsgObj.Active = Active.Checked
                If MessagesListGrid.SelectedIndex > -1 Then
                    Dim MsgID As String = MessagesListGrid.DataKeys(MessagesListGrid.SelectedIndex).Value
                    MsgObj.LastUpdateBy = User.Identity.Name
                    Dim FilterMessages As New Messages
                    FilterMessages.IdKey = MsgID
                    Dim Result As Boolean = False
                    Dim UQ As New UpdateQuery
                    UQ._InputTable = MsgObj
                    UQ._FilterTable = FilterMessages
                    UQ._DB = "Custom"
                    UQ._HasInBetweenConditions = False
                    UQ._HasWhereConditions = True
                    'Query Conditions List
                    Dim QryConditions As Dictionary(Of String, List(Of String)) = New Dictionary(Of String, List(Of String))

                    'Query Condition Groups
                    Dim ConditionsGrp1 As List(Of String) = New List(Of String)

                    'Query Conditions values
                    ConditionsGrp1.Add("Idkey=@Filter_Idkey")
                    'ConditionsGrp1.Add("ItemName=@ItemName")


                    QryConditions.Add(" AND ", ConditionsGrp1)
                    UQ._Conditions = QryConditions

                    Result = CURD.UpdateData(UQ)
                    ShowMessage("Successfully Updated")
                    ClearForm()

                Else
                    MsgObj.CreatedBy = User.Identity.Name
                    CURD.InsertData(MsgObj)
                    ShowMessage("Successfully Added")
                    ClearForm()
                End If
                LoadData()
            End If

        Catch ex As Exception
            AppSpecificFunc.WriteLog(ex)
        End Try
    End Sub
#End Region

    
End Class