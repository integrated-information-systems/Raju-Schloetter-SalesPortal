Imports SchloetterSalesPortal.Models

Public Class _Default
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        loadMessages()
    End Sub
    Private Sub loadMessages()
        Dim message As String = String.Empty

        Dim Msg As New Messages

        Dim SQ As New SelectQuery

        'Where Condition parameter
        Msg.Active = True
        SQ._InputTable = Msg
        SQ._DB = "Custom"
        SQ._HasInBetweenConditions = False
        SQ._HasWhereConditions = True

        'Query Conditions List
        Dim QryConditions As Dictionary(Of String, List(Of String)) = New Dictionary(Of String, List(Of String))

        'Query Condition Groups
        Dim ConditionsGrp1 As List(Of String) = New List(Of String)

        'Query Conditions values
        ConditionsGrp1.Add("Active=@Active")

        QryConditions.Add(" AND ", ConditionsGrp1)

        SQ._Conditions = QryConditions


        Dim ResultDataTable As New DataTable
        ResultDataTable = CURD.SelectAllData(SQ)
        For Each ro As DataRow In ResultDataTable.Rows
            message = message & "<b>" & ro("MessageTitle").ToString & "</b><br/>"
            message = message & ro("Message").ToString & "<br/>"
        Next
        lblMessages.Text = message
        
    End Sub
End Class