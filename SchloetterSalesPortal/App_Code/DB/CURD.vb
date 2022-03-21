﻿Imports Microsoft.VisualBasic
Imports System.Data.SqlClient
Imports System.Reflection
Imports System.Data
Imports System.Diagnostics
Namespace Models
    Public Class CURD
        Protected Shared ConString As String = ConfigurationManager.ConnectionStrings("Custom_CRM_DB_ConnectionString").ToString
        Protected Shared SAPConString As String = ConfigurationManager.ConnectionStrings("SAP_DB_ConnectionString").ToString
        Public Shared Function GetConnectionString(Optional ByVal Type As String = "Custom") As String

            Dim ReturnResult As String = String.Empty
            Try
                Try
                    If Type = "Custom" Then
                        ReturnResult = ConString
                    ElseIf Type = "SAP" Then
                        ReturnResult = SAPConString
                    End If
                Catch ex As Exception
                    AppSpecificFunc.WriteLog(ex)
                End Try
                Return ReturnResult
            Catch ex As Exception
                Return ReturnResult
            End Try

        End Function
        Public Shared Function UpdateData(ByVal UQ As UpdateQuery) As Boolean
            Dim ReturnResult As Boolean = True
            Try
                Dim ConnectionStringToUse As String = String.Empty
                If UQ._DB = "SAP" Then
                    ConnectionStringToUse = SAPConString
                Else
                    ConnectionStringToUse = ConString
                End If
                Using SqlCon As New SqlConnection(ConnectionStringToUse)

                    SqlCon.Open()

                    Dim TableName As String = UQ._InputTable.GetType().Name
                    Dim UpdateFields As New List(Of String)
                    Dim UpdateParams As New List(Of String)
                    Dim UpdateParamsValues As New List(Of String)
                    Dim FilterParams As New List(Of String)
                    Dim FilterParamsValues As New List(Of String)
                    Dim Qry As String = "UPDATE  " & TableName & " SET "


                    Dim InputProps As Type = UQ._InputTable.GetType
                    For Each member As PropertyInfo In InputProps.GetProperties
                        If Not IsNothing(member.GetValue(UQ._InputTable, Nothing)) Then
                            Dim MemberValue As String = member.GetValue(UQ._InputTable, Nothing).ToString
                            If Not IsNothing(UQ._InputTableFieldsOperation) Then
                                Dim OperatorValue As String = member.GetValue(UQ._InputTableFieldsOperation, Nothing).ToString
                                Select Case OperatorValue
                                    Case "-"
                                        UpdateFields.Add(member.Name & "=" & member.Name & "-@" & member.Name)
                                    Case "+"
                                        UpdateFields.Add(member.Name & "=" & member.Name & "+@" & member.Name)
                                    Case Else
                                        UpdateFields.Add(member.Name & "=@" & member.Name)
                                End Select
                            Else
                                UpdateFields.Add(member.Name & "=@" & member.Name)
                            End If

                            UpdateParams.Add("@" & member.Name)
                            UpdateParamsValues.Add(member.GetValue(UQ._InputTable, Nothing))
                        ElseIf member.Name.ToLower = "lastupdateon" Then
                            UpdateFields.Add(member.Name & "=@" & member.Name)
                            UpdateParams.Add("@" & member.Name)
                            UpdateParamsValues.Add(Format(CDate(DateTime.Now), "yyyy-MM-dd HH:mm:ss"))
                        End If
                    Next
                    If UQ._HasWhereConditions = True Then

                        Dim FilterProps As Type = UQ._FilterTable.GetType
                        For Each member As PropertyInfo In FilterProps.GetProperties
                            If Not IsNothing(member.GetValue(UQ._FilterTable, Nothing)) Then
                                Dim MemberValue As String = member.GetValue(UQ._FilterTable, Nothing).ToString
                                FilterParams.Add("@Filter_" & member.Name)
                                FilterParamsValues.Add(member.GetValue(UQ._FilterTable, Nothing))
                            End If
                        Next

                        Qry = Qry & String.Join(",", UpdateFields.ToArray)

                        Qry = Qry & " WHERE "


                        If UQ._Conditions.Count > 0 Then
                            Dim ConditionsCount As Integer = 0
                            Dim pair As KeyValuePair(Of String, List(Of String))
                            For Each pair In UQ._Conditions
                                ' Display Key and Value.
                                If ConditionsCount > 0 And UQ._HasInBetweenConditions = True Then
                                    Qry = Qry & UQ._InBetweenCondition
                                End If

                                If pair.Value.Count > 0 Then
                                    Select Case pair.Key.ToString
                                        Case " AND ", " OR "
                                            Qry = Qry & " ( " & String.Join(pair.Key, pair.Value.ToArray) & " )"
                                    End Select
                                End If

                                ConditionsCount = ConditionsCount + 1

                            Next
                        End If


                    End If




                    Using cmd As New SqlCommand(Qry)
                        cmd.Connection = SqlCon
                        Dim i As Integer = 0
                        For Each value As String In FilterParams
                            cmd.Parameters.AddWithValue(value, FilterParamsValues.Item(i))
                            i = i + 1
                        Next
                        i = 0
                        For Each value As String In UpdateParams
                            cmd.Parameters.AddWithValue(value, UpdateParamsValues.Item(i))
                            i = i + 1
                        Next
                        cmd.ExecuteNonQuery()
                    End Using

                    Return ReturnResult
                End Using
            Catch ex As Exception
                AppSpecificFunc.WriteLog(ex)
                Return False
            End Try
        End Function
        Public Shared Function CustomQueryGetData(ByVal CQ As CustomQuery) As DataTable
            Dim ReturnResult As New DataTable
            Try
                Dim ConnectionStringToUse As String = String.Empty
                If CQ._DB = "SAP" Then
                    ConnectionStringToUse = SAPConString
                Else
                    ConnectionStringToUse = ConString
                End If
                Using SqlCon As New SqlConnection(ConnectionStringToUse)
                    SqlCon.Open()
                    Dim Qry As String = CQ._InputQuery
                    Using cmd As New SqlCommand(Qry)
                        cmd.Connection = SqlCon
                        If Not IsNothing(CQ._Parameters) Then
                            Dim Params = CQ._Parameters
                            For Each iKey As String In Params.Keys
                                Dim ParamValue As String = Params(iKey)
                                cmd.Parameters.AddWithValue(iKey, ParamValue)
                            Next
                        End If

                        Dim SqlAdap As New SqlDataAdapter(cmd)
                        SqlAdap.Fill(ReturnResult)
                        SqlAdap.Dispose()
                    End Using
                End Using

                Return ReturnResult
            Catch ex As Exception
                AppSpecificFunc.WriteLog(ex)
                Return Nothing
            End Try
        End Function
        Public Shared Function InsertDataAvoidDuplicate(ByVal UQ As ComplexInsertQuery, ByRef PrimaryKeyValue As String, Optional ByVal HasPrimaryKey As Boolean = False) As Boolean
            Dim ReturnResult As Boolean = True
            Try
                Dim ConnectionStringToUse As String = String.Empty
                If UQ._DB = "SAP" Then
                    ConnectionStringToUse = SAPConString
                Else
                    ConnectionStringToUse = ConString
                End If
                Using SqlCon As New SqlConnection(ConnectionStringToUse)

                    SqlCon.Open()

                    Dim TableName As String = UQ._InputTable.GetType().Name
                    Dim InsertFields As New List(Of String)
                    Dim InsertParams As New List(Of String)
                    Dim InsertParamsValues As New List(Of String)
                    Dim FilterParams As New List(Of String)
                    Dim FilterParamsValues As New List(Of String)
                    Dim Qry As String = " INSERT INTO  " & TableName


                    Dim InputProps As Type = UQ._InputTable.GetType
                    For Each member As PropertyInfo In InputProps.GetProperties
                        If Not IsNothing(member.GetValue(UQ._InputTable, Nothing)) Then
                            Dim MemberValue As String = member.GetValue(UQ._InputTable, Nothing).ToString
                            InsertFields.Add(member.Name)
                            InsertParams.Add("@" & member.Name)
                            InsertParamsValues.Add(member.GetValue(UQ._InputTable, Nothing))
                        ElseIf member.Name.ToLower = "createdon" Then
                            InsertFields.Add(member.Name)
                            InsertParams.Add("@" & member.Name)
                            InsertParamsValues.Add(Format(CDate(DateTime.Now), "yyyy-MM-dd HH:mm:ss"))
                        End If
                    Next
                    Qry = Qry & " (" & String.Join(",", InsertFields.ToArray) & ") "

                    If HasPrimaryKey Then
                        Qry = Qry & " OUTPUT INSERTED." & "IdKey "
                    End If


                    Qry = Qry & " SELECT "

                    Qry = Qry & " " & String.Join(",", InsertParams.ToArray) & " "

                    If UQ._HasWhereConditions = True Then

                        Qry = Qry & " WHERE   NOT EXISTS ( SELECT 1 FROM " & TableName

                        Dim FilterProps As Type = UQ._FilterTable.GetType
                        For Each member As PropertyInfo In FilterProps.GetProperties
                            If Not IsNothing(member.GetValue(UQ._FilterTable, Nothing)) Then
                                Dim MemberValue As String = member.GetValue(UQ._FilterTable, Nothing).ToString
                                FilterParams.Add("@Filter_" & member.Name)
                                FilterParamsValues.Add(member.GetValue(UQ._FilterTable, Nothing))
                            End If
                        Next


                        Qry = Qry & " WHERE "


                        If UQ._Conditions.Count > 0 Then
                            Dim ConditionsCount As Integer = 0
                            Dim pair As KeyValuePair(Of String, List(Of String))
                            For Each pair In UQ._Conditions
                                ' Display Key and Value.
                                If ConditionsCount > 0 And UQ._HasInBetweenConditions = True Then
                                    Qry = Qry & UQ._InBetweenCondition
                                End If

                                If pair.Value.Count > 0 Then
                                    Select Case pair.Key.ToString
                                        Case " AND ", " OR "
                                            Qry = Qry & " ( " & String.Join(pair.Key, pair.Value.ToArray) & " )"
                                    End Select
                                End If

                                ConditionsCount = ConditionsCount + 1

                            Next
                        End If

                        Qry = Qry & " )"
                    End If




                    Using cmd As New SqlCommand(Qry)
                        cmd.Connection = SqlCon
                        Dim i As Integer = 0
                        For Each value As String In FilterParams
                            cmd.Parameters.AddWithValue(value, FilterParamsValues.Item(i))
                            i = i + 1
                        Next
                        i = 0
                        For Each value As String In InsertParams
                            cmd.Parameters.AddWithValue(value, InsertParamsValues.Item(i))
                            i = i + 1
                        Next

                        If HasPrimaryKey Then
                            Dim PrimaryKeyValueReader As SqlDataReader = cmd.ExecuteReader()
                            If PrimaryKeyValueReader.Read Then
                                PrimaryKeyValue = PrimaryKeyValueReader("IdKey").ToString
                            End If
                            PrimaryKeyValueReader.Close()
                        Else
                            cmd.ExecuteNonQuery()
                        End If
                    End Using

                    Return ReturnResult
                End Using
            Catch ex As Exception
                AppSpecificFunc.WriteLog(ex)
                Return False
            End Try
        End Function
        Public Shared Function InsertData(ByVal InputTable As Object, Optional ByVal HasPrimaryKey As Boolean = False, Optional ByVal PassedTableName As String = "") As Boolean
            Dim ReturnResult As Boolean = False
            Try

                Using SqlCon As New SqlConnection(ConString)

                    SqlCon.Open()
                    'Query Building - starts here

                    Dim TableName As String = InputTable.GetType().Name


                    If PassedTableName <> String.Empty Then
                        TableName = PassedTableName
                    End If

                    Dim Qry As String = "Insert into "
                    Qry = Qry & TableName
                    Dim QueryParams As New List(Of String)
                    Dim SqlParams As New List(Of String)
                    Dim SqlParamsValues As New List(Of String)


                    Dim props As Type = InputTable.GetType
                    For Each member As PropertyInfo In props.GetProperties
                        If member.Name <> "IdKey" Or HasPrimaryKey = False Then
                            If Not IsNothing(member.GetValue(InputTable, Nothing)) Then
                                QueryParams.Add(member.Name)
                                SqlParams.Add("@" & member.Name)
                                SqlParamsValues.Add(member.GetValue(InputTable, Nothing).ToString)
                            ElseIf member.Name.ToLower = "createdon" Then
                                QueryParams.Add(member.Name)
                                SqlParams.Add("@" & member.Name)
                                SqlParamsValues.Add(Format(CDate(DateTime.Now), "yyyy-MM-dd HH:mm:ss"))
                            End If
                        End If
                    Next

                    Qry = Qry & " (" & String.Join(",", QueryParams.ToArray) & ") "

                    Qry = Qry & " Values "

                    Qry = Qry & " (" & String.Join(",", SqlParams.ToArray) & ") "




                    'Query Building - ends here


                    Using cmd As New SqlCommand(Qry)
                        cmd.Connection = SqlCon
                        Dim i As Integer = 0
                        For Each Value As String In SqlParamsValues
                            cmd.Parameters.AddWithValue(SqlParams(i), Value)
                            i = i + 1
                        Next
                        cmd.ExecuteNonQuery()

                    End Using

                    ReturnResult = True


                End Using

                Return ReturnResult
            Catch ex As Exception
                AppSpecificFunc.WriteLog(ex)
                Return ReturnResult
            End Try
        End Function
        Public Shared Function InsertDataTransaction(ByVal InputTable As Object, ByRef Connection As SqlConnection, ByRef SqlTrans As SqlTransaction, ByRef PrimaryKeyValue As String, Optional ByVal HasPrimaryKey As Boolean = False) As Boolean
            Dim ReturnResult As Boolean = False
            Try




                'Query Building - starts here
                Dim TableName As String = InputTable.GetType().Name

                Dim Qry As String = "Insert into "
                Qry = Qry & TableName
                Dim QueryParams As New List(Of String)
                Dim SqlParams As New List(Of String)
                Dim SqlParamsValues As New List(Of String)


                Dim props As Type = InputTable.GetType
                For Each member As PropertyInfo In props.GetProperties
                    If member.Name <> "IdKey" Or HasPrimaryKey = False Then
                        If Not IsNothing(member.GetValue(InputTable, Nothing)) Then
                            QueryParams.Add(member.Name)
                            SqlParams.Add("@" & member.Name)
                            SqlParamsValues.Add(member.GetValue(InputTable, Nothing).ToString)
                        ElseIf member.Name.ToLower = "createdon" Then
                            QueryParams.Add(member.Name)
                            SqlParams.Add("@" & member.Name)
                            SqlParamsValues.Add(Format(CDate(DateTime.Now), "yyyy-MM-dd HH:mm:ss"))
                        End If
                    End If
                Next

                Qry = Qry & " (" & String.Join(",", QueryParams.ToArray) & ") "

                If HasPrimaryKey Then
                    Qry = Qry & " OUTPUT INSERTED." & "IdKey "
                End If

                Qry = Qry & " Values "

                Qry = Qry & " (" & String.Join(",", SqlParams.ToArray) & ") "




                'Query Building - ends here


                Using cmd As New SqlCommand(Qry)
                    cmd.Connection = Connection
                    cmd.Transaction = SqlTrans

                    Dim i As Integer = 0
                    For Each Value As String In SqlParamsValues
                        cmd.Parameters.AddWithValue(SqlParams(i), Value)
                        i = i + 1
                    Next
                    If HasPrimaryKey Then
                        Dim PrimaryKeyValueReader As SqlDataReader = cmd.ExecuteReader()
                        If PrimaryKeyValueReader.Read Then
                            PrimaryKeyValue = PrimaryKeyValueReader("IdKey").ToString
                        End If
                        PrimaryKeyValueReader.Close()
                    Else
                        cmd.ExecuteNonQuery()
                    End If

                End Using

                ReturnResult = True




                Return ReturnResult
            Catch ex As Exception
                AppSpecificFunc.WriteLog(ex)
                Return ReturnResult
            End Try
        End Function
        Public Shared Function UpdateDataTransaction(ByVal UQ As UpdateQuery, ByRef Connection As SqlConnection, ByRef SqlTrans As SqlTransaction, ByRef PrimaryKeyValue As String, Optional ByVal HasPrimaryKey As Boolean = False) As Boolean
            Dim ReturnResult As Boolean = True
            Try

                Dim TableName As String = UQ._InputTable.GetType().Name
                Dim UpdateFields As New List(Of String)
                Dim UpdateParams As New List(Of String)
                Dim UpdateParamsValues As New List(Of String)
                Dim FilterParams As New List(Of String)
                Dim FilterParamsValues As New List(Of String)
                Dim Qry As String = "UPDATE  " & TableName & " SET "


                Dim InputProps As Type = UQ._InputTable.GetType
                For Each member As PropertyInfo In InputProps.GetProperties
                    If Not IsNothing(member.GetValue(UQ._InputTable, Nothing)) Then
                        Dim MemberValue As String = member.GetValue(UQ._InputTable, Nothing).ToString
                        If Not IsNothing(UQ._InputTableFieldsOperation) Then
                            Dim OperatorValue As String = member.GetValue(UQ._InputTableFieldsOperation, Nothing).ToString
                            Select Case OperatorValue
                                Case "-"
                                    UpdateFields.Add(member.Name & "=" & member.Name & "-@" & member.Name)
                                Case "+"
                                    UpdateFields.Add(member.Name & "=" & member.Name & "+@" & member.Name)
                                Case Else
                                    UpdateFields.Add(member.Name & "=@" & member.Name)
                            End Select
                        Else
                            UpdateFields.Add(member.Name & "=@" & member.Name)
                        End If


                        UpdateParams.Add("@" & member.Name)
                        UpdateParamsValues.Add(member.GetValue(UQ._InputTable, Nothing))
                    ElseIf member.Name.ToLower = "lastupdateon" Then
                        UpdateFields.Add(member.Name & "=@" & member.Name)
                        UpdateParams.Add("@" & member.Name)
                        UpdateParamsValues.Add(Format(CDate(DateTime.Now), "yyyy-MM-dd HH:mm:ss"))
                    End If
                Next
                If UQ._HasWhereConditions = True Then

                    Dim FilterProps As Type = UQ._FilterTable.GetType
                    For Each member As PropertyInfo In FilterProps.GetProperties
                        If Not IsNothing(member.GetValue(UQ._FilterTable, Nothing)) Then
                            Dim MemberValue As String = member.GetValue(UQ._FilterTable, Nothing).ToString
                            FilterParams.Add("@Filter_" & member.Name)
                            FilterParamsValues.Add(member.GetValue(UQ._FilterTable, Nothing))
                        End If
                    Next

                    Qry = Qry & String.Join(",", UpdateFields.ToArray)

                    If HasPrimaryKey Then
                        Qry = Qry & " OUTPUT INSERTED." & "IdKey "
                    End If

                    Qry = Qry & " WHERE "


                    If UQ._Conditions.Count > 0 Then
                        Dim ConditionsCount As Integer = 0
                        Dim pair As KeyValuePair(Of String, List(Of String))
                        For Each pair In UQ._Conditions
                            ' Display Key and Value.
                            If ConditionsCount > 0 And UQ._HasInBetweenConditions = True Then
                                Qry = Qry & UQ._InBetweenCondition
                            End If

                            If pair.Value.Count > 0 Then
                                Select Case pair.Key.ToString
                                    Case " AND ", " OR "
                                        Qry = Qry & " ( " & String.Join(pair.Key, pair.Value.ToArray) & " )"
                                End Select
                            End If

                            ConditionsCount = ConditionsCount + 1

                        Next
                    End If


                End If




                Using cmd As New SqlCommand(Qry)
                    cmd.Connection = Connection
                    cmd.Transaction = SqlTrans
                    Dim i As Integer = 0
                    For Each value As String In FilterParams
                        cmd.Parameters.AddWithValue(value, FilterParamsValues.Item(i))
                        i = i + 1
                    Next
                    i = 0
                    For Each value As String In UpdateParams
                        cmd.Parameters.AddWithValue(value, UpdateParamsValues.Item(i))
                        i = i + 1
                    Next
                    If HasPrimaryKey Then
                        Dim PrimaryKeyValueReader As SqlDataReader = cmd.ExecuteReader()
                        If PrimaryKeyValueReader.Read Then
                            PrimaryKeyValue = PrimaryKeyValueReader("IdKey").ToString
                        End If
                        PrimaryKeyValueReader.Close()
                    Else
                        cmd.ExecuteNonQuery()
                    End If

                End Using

                Return ReturnResult

            Catch ex As Exception
                AppSpecificFunc.WriteLog(ex)
                Return False
            End Try
        End Function
        Public Shared Function DeleteDataTransaction(ByVal DQ As DeleteQuery, ByRef Connection As SqlConnection, ByRef SqlTrans As SqlTransaction, ByRef PrimaryKeyValue As String, Optional ByVal HasPrimaryKey As Boolean = False) As Boolean
            Dim ReturnResult As Boolean = True
            Try
                Dim TableName As String = DQ._InputTable.GetType().Name

                Dim QueryParams As New List(Of String)
                Dim FilterParams As New List(Of String)
                Dim FilterParamsValues As New List(Of String)
                Dim Qry As String = "DELETE  "


                Dim props As Type = DQ._InputTable.GetType
                For Each member As PropertyInfo In props.GetProperties


                    If Not IsNothing(member.GetValue(DQ._InputTable, Nothing)) Then
                        FilterParams.Add("@" & member.Name)
                        FilterParamsValues.Add(member.GetValue(DQ._InputTable, Nothing).ToString)
                    End If
                Next



                Qry = Qry & " FROM  " & TableName

                If HasPrimaryKey Then
                    Qry = Qry & " OUTPUT DELETED." & "IdKey "
                End If

                If DQ._HasWhereConditions = True Then
                    Qry = Qry & " WHERE "


                    If DQ._Conditions.Count > 0 Then
                        Dim ConditionsCount As Integer = 0
                        Dim pair As KeyValuePair(Of String, List(Of String))
                        For Each pair In DQ._Conditions
                            ' Display Key and Value.
                            If ConditionsCount > 0 And DQ._HasInBetweenConditions = True Then
                                Qry = Qry & DQ._InBetweenCondition
                            End If

                            If pair.Value.Count > 0 Then
                                Select Case pair.Key.ToString
                                    Case " AND ", " OR "
                                        Qry = Qry & " ( " & String.Join(pair.Key, pair.Value.ToArray) & " )"
                                End Select
                            End If

                            ConditionsCount = ConditionsCount + 1

                        Next
                    End If


                End If




                Using cmd As New SqlCommand(Qry)
                    cmd.Connection = Connection
                    cmd.Transaction = SqlTrans
                    Dim i As Integer = 0
                    For Each value As String In FilterParams
                        cmd.Parameters.AddWithValue(value, FilterParamsValues.Item(i))
                        i = i + 1
                    Next
                    If HasPrimaryKey Then
                        Dim PrimaryKeyValueReader As SqlDataReader = cmd.ExecuteReader()
                        If PrimaryKeyValueReader.Read Then
                            PrimaryKeyValue = PrimaryKeyValueReader("IdKey").ToString
                        End If
                        PrimaryKeyValueReader.Close()
                    Else
                        cmd.ExecuteNonQuery()
                    End If
                End Using

                Return ReturnResult

            Catch ex As Exception
                AppSpecificFunc.WriteLog(ex)
                Return False
            End Try
        End Function
        Public Shared Function SelectAllData(ByVal SQ As SelectQuery) As DataTable
            Dim ReturnResult As New DataTable
            Try
                Dim ConnectionStringToUse As String = String.Empty
                If SQ._DB = "SAP" Then
                    ConnectionStringToUse = SAPConString
                Else
                    ConnectionStringToUse = ConString
                End If
                Using SqlCon As New SqlConnection(ConnectionStringToUse)

                    SqlCon.Open()
                    Dim TableName As String = SQ._InputTable.GetType().Name

                    Dim QueryParams As New List(Of String)
                    Dim FilterParams As New List(Of String)
                    Dim FilterParamsValues As New List(Of String)
                    Dim Qry As String = "SELECT "

                    If Not IsNothing(SQ._TopRecord) Then
                        If IsNumeric(SQ._TopRecord) Then
                            If SQ._TopRecord > 0 Then
                                Qry = Qry & " Top " & SQ._TopRecord & "  "
                            End If
                        End If
                    End If

                    Dim props As Type = SQ._InputTable.GetType
                    For Each member As PropertyInfo In props.GetProperties
                        QueryParams.Add(member.Name)


                        If Not IsNothing(member.GetValue(SQ._InputTable, Nothing)) Then
                            FilterParams.Add("@" & member.Name)
                            FilterParamsValues.Add(member.GetValue(SQ._InputTable, Nothing).ToString)
                        End If
                    Next

                    Qry = Qry & String.Join(",", QueryParams.ToArray)

                    Qry = Qry & " FROM  " & TableName

                    If SQ._HasWhereConditions = True Then
                        Qry = Qry & " WHERE "


                        If SQ._Conditions.Count > 0 Then
                            Dim ConditionsCount As Integer = 0
                            Dim pair As KeyValuePair(Of String, List(Of String))
                            For Each pair In SQ._Conditions
                                ' Display Key and Value.
                                If ConditionsCount > 0 And SQ._HasInBetweenConditions = True Then
                                    Qry = Qry & SQ._InBetweenCondition
                                End If

                                If pair.Value.Count > 0 Then
                                    Select Case pair.Key.ToString
                                        Case " AND ", " OR "
                                            Qry = Qry & " ( " & String.Join(pair.Key, pair.Value.ToArray) & " )"
                                    End Select
                                End If

                                ConditionsCount = ConditionsCount + 1

                            Next
                        End If


                    End If

                    If Not IsNothing(SQ._OrderBy) Then
                        If SQ._OrderBy <> String.Empty Then
                            Qry = Qry & " ORDER BY " & SQ._OrderBy
                        End If
                    End If


                    Using cmd As New SqlCommand(Qry)
                        cmd.Connection = SqlCon
                        Dim i As Integer = 0
                        For Each value As String In FilterParams
                            cmd.Parameters.AddWithValue(value, FilterParamsValues.Item(i))
                            i = i + 1
                        Next
                        Dim SqlAdap As New SqlDataAdapter(cmd)
                        SqlAdap.Fill(ReturnResult)
                        SqlAdap.Dispose()
                    End Using

                    Return ReturnResult
                End Using
            Catch ex As Exception
                AppSpecificFunc.WriteLog(ex)
                Return ReturnResult
            End Try
        End Function
        Public Shared Function DeleteData(ByVal DQ As DeleteQuery) As Boolean
            Dim ReturnResult As Boolean = True
            Try
                Dim ConnectionStringToUse As String = String.Empty
                If DQ._DB = "SAP" Then
                    ConnectionStringToUse = SAPConString
                Else
                    ConnectionStringToUse = ConString
                End If
                Using SqlCon As New SqlConnection(ConnectionStringToUse)

                    SqlCon.Open()
                    Dim TableName As String = DQ._InputTable.GetType().Name


                    Dim FilterParams As New List(Of String)
                    Dim FilterParamsValues As New List(Of String)
                    Dim Qry As String = "DELETE "


                    Dim props As Type = DQ._InputTable.GetType
                    For Each member As PropertyInfo In props.GetProperties
                        If Not IsNothing(member.GetValue(DQ._InputTable, Nothing)) Then
                            FilterParams.Add("@" & member.Name)
                            FilterParamsValues.Add(member.GetValue(DQ._InputTable, Nothing).ToString)
                        End If
                    Next



                    Qry = Qry & " FROM  " & TableName

                    If DQ._HasWhereConditions = True Then
                        Qry = Qry & " WHERE "


                        If DQ._Conditions.Count > 0 Then
                            Dim ConditionsCount As Integer = 0
                            Dim pair As KeyValuePair(Of String, List(Of String))
                            For Each pair In DQ._Conditions
                                ' Display Key and Value.
                                If ConditionsCount > 0 And DQ._HasInBetweenConditions = True Then
                                    Qry = Qry & DQ._InBetweenCondition
                                End If

                                If pair.Value.Count > 0 Then
                                    Select Case pair.Key.ToString
                                        Case " AND ", " OR "
                                            Qry = Qry & " ( " & String.Join(pair.Key, pair.Value.ToArray) & " )"
                                    End Select
                                End If

                                ConditionsCount = ConditionsCount + 1

                            Next
                        End If


                    End If




                    Using cmd As New SqlCommand(Qry)
                        cmd.Connection = SqlCon
                        Dim i As Integer = 0
                        For Each value As String In FilterParams
                            cmd.Parameters.AddWithValue(value, FilterParamsValues.Item(i))
                            i = i + 1
                        Next
                        cmd.ExecuteNonQuery()
                    End Using

                    Return ReturnResult
                End Using
            Catch ex As Exception
                AppSpecificFunc.WriteLog(ex)
                Return False
            End Try
        End Function
        Public Shared Function DeleteDataComplexCondition(ByVal DQ As ComplexDeleteQuery) As Boolean
            Dim ReturnResult As Boolean = True
            Try
                Dim ConnectionStringToUse As String = String.Empty
                If DQ._DB = "SAP" Then
                    ConnectionStringToUse = SAPConString
                Else
                    ConnectionStringToUse = ConString
                End If
                Using SqlCon As New SqlConnection(ConnectionStringToUse)

                    SqlCon.Open()
                    Dim TableName As String = DQ._InputTable(0).GetType().Name


                    Dim FilterParams As New List(Of String)
                    Dim FilterParamsValues As New List(Of String)
                    Dim Qry As String = "DELETE "


                    Dim props As Type = DQ._InputTable(0).GetType
                    Dim j As Integer = 0
                    For Each Obj In DQ._InputTable
                        For Each member As PropertyInfo In props.GetProperties
                            If j = 0 Then
                                If Not IsNothing(member.GetValue(DQ._InputTable(j), Nothing)) Then
                                    FilterParams.Add("@" & member.Name)
                                    FilterParamsValues.Add(member.GetValue(DQ._InputTable(j), Nothing).ToString)
                                End If
                            Else
                                If Not IsNothing(member.GetValue(DQ._InputTable(j), Nothing)) Then
                                    FilterParams.Add("@" & member.Name & j)
                                    FilterParamsValues.Add(member.GetValue(DQ._InputTable(j), Nothing).ToString)
                                End If
                            End If

                        Next
                        j = j + 1
                    Next



                    Qry = Qry & " FROM  " & TableName

                    If DQ._HasWhereConditions = True Then
                        Qry = Qry & " WHERE "


                        If DQ._Conditions.Count > 0 Then
                            Dim ConditionsCount As Integer = 0
                            Dim pair As KeyValuePair(Of String, List(Of String))
                            For Each pair In DQ._Conditions
                                ' Display Key and Value.
                                If ConditionsCount > 0 And DQ._HasInBetweenConditions = True Then
                                    Qry = Qry & DQ._InBetweenCondition
                                End If

                                If pair.Value.Count > 0 Then
                                    Select Case pair.Key.ToString
                                        Case " AND ", " OR "
                                            Qry = Qry & " ( " & String.Join(pair.Key, pair.Value.ToArray) & " )"
                                    End Select
                                End If

                                ConditionsCount = ConditionsCount + 1

                            Next
                        End If


                    End If




                    Using cmd As New SqlCommand(Qry)
                        cmd.Connection = SqlCon
                        Dim i As Integer = 0
                        For Each value As String In FilterParams
                            cmd.Parameters.AddWithValue(value, FilterParamsValues.Item(i))
                            i = i + 1
                        Next
                        cmd.ExecuteNonQuery()
                    End Using

                    Return ReturnResult
                End Using
            Catch ex As Exception
                AppSpecificFunc.WriteLog(ex)
                Return False
            End Try
        End Function
        Public Shared Function SelectSpecificFieldsData(ByVal SQ As SelectQuery) As DataTable
            Dim ReturnResult As New DataTable
            Try
                Dim ConnectionStringToUse As String = String.Empty
                If SQ._DB = "SAP" Then
                    ConnectionStringToUse = SAPConString
                Else
                    ConnectionStringToUse = ConString
                End If
                Using SqlCon As New SqlConnection(ConnectionStringToUse)

                    SqlCon.Open()

                    Dim TableName As String = SQ._InputTable.GetType().Name

                    Dim QueryParams As New List(Of String)
                    Dim FilterParams As New List(Of String)
                    Dim FilterParamsValues As New List(Of String)
                    Dim Qry As String = "SELECT "

                    If Not IsNothing(SQ._TopRecord) Then
                        If IsNumeric(SQ._TopRecord) Then
                            If SQ._TopRecord > 0 Then
                                Qry = Qry & " Top " & SQ._TopRecord & "  "
                            End If
                        End If
                    End If

                    Dim props As Type = SQ._InputTable.GetType
                    For Each member As PropertyInfo In props.GetProperties



                        If Not IsNothing(member.GetValue(SQ._InputTable, Nothing)) Then
                            Dim MemberValue As String = member.GetValue(SQ._InputTable, Nothing).ToString
                            If MemberValue <> "?" Then
                                FilterParams.Add("@" & member.Name)
                                FilterParamsValues.Add(member.GetValue(SQ._InputTable, Nothing).ToString)
                            Else
                                QueryParams.Add(member.Name)
                            End If

                        End If
                    Next

                    Qry = Qry & String.Join(",", QueryParams.ToArray)

                    Qry = Qry & " FROM  " & TableName

                    If SQ._HasWhereConditions = True Then
                        Qry = Qry & " WHERE "


                        If SQ._Conditions.Count > 0 Then
                            Dim ConditionsCount As Integer = 0
                            Dim pair As KeyValuePair(Of String, List(Of String))
                            For Each pair In SQ._Conditions
                                ' Display Key and Value.
                                If ConditionsCount > 0 And SQ._HasInBetweenConditions = True Then
                                    Qry = Qry & SQ._InBetweenCondition
                                End If

                                If pair.Value.Count > 0 Then
                                    Select Case pair.Key.ToString
                                        Case " AND ", " OR "
                                            Qry = Qry & " ( " & String.Join(pair.Key, pair.Value.ToArray) & " )"
                                    End Select
                                End If

                                ConditionsCount = ConditionsCount + 1

                            Next
                        End If


                    End If

                    If Not IsNothing(SQ._OrderBy) Then
                        If SQ._OrderBy <> String.Empty Then
                            Qry = Qry & " ORDER BY " & SQ._OrderBy
                        End If
                    End If

                    Using cmd As New SqlCommand(Qry)
                        cmd.Connection = SqlCon
                        Dim i As Integer = 0
                        For Each value As String In FilterParams
                            cmd.Parameters.AddWithValue(value, FilterParamsValues.Item(i))
                            i = i + 1
                        Next
                        Dim SqlAdap As New SqlDataAdapter(cmd)
                        SqlAdap.Fill(ReturnResult)
                        SqlAdap.Dispose()
                    End Using

                End Using
                Return ReturnResult
            Catch ex As Exception
                AppSpecificFunc.WriteLog(ex)
                Return ReturnResult
            End Try
        End Function
        Public Shared Function SelectAllDataComplexCondition(ByVal SQ As ComplexSelectQuery) As DataTable
            Dim ReturnResult As New DataTable
            Try
                Dim ConnectionStringToUse As String = String.Empty
                If SQ._DB = "SAP" Then
                    ConnectionStringToUse = SAPConString
                Else
                    ConnectionStringToUse = ConString
                End If
                Using SqlCon As New SqlConnection(ConnectionStringToUse)

                    SqlCon.Open()
                    Dim TableName As String = SQ._InputTable(0).GetType().Name

                    Dim QueryParams As New List(Of String)
                    Dim FilterParams As New List(Of String)
                    Dim FilterParamsValues As New List(Of String)
                    Dim Qry As String = "SELECT "

                    If Not IsNothing(SQ._TopRecord) Then
                        If IsNumeric(SQ._TopRecord) Then
                            If SQ._TopRecord > 0 Then
                                Qry = Qry & " Top " & SQ._TopRecord & "  "
                            End If
                        End If
                    End If

                    Dim props As Type = SQ._InputTable(0).GetType
                    Dim j As Integer = 0
                    For Each Obj In SQ._InputTable
                        For Each member As PropertyInfo In props.GetProperties
                            If j = 0 Then
                                QueryParams.Add(member.Name)
                                If Not IsNothing(member.GetValue(SQ._InputTable(j), Nothing)) Then
                                    FilterParams.Add("@" & member.Name)
                                    FilterParamsValues.Add(member.GetValue(SQ._InputTable(j), Nothing).ToString)
                                End If
                            Else
                                If Not IsNothing(member.GetValue(SQ._InputTable(j), Nothing)) Then
                                    FilterParams.Add("@" & member.Name & j)
                                    FilterParamsValues.Add(member.GetValue(SQ._InputTable(j), Nothing).ToString)
                                End If
                            End If

                        Next
                        j = j + 1
                    Next


                    Qry = Qry & String.Join(",", QueryParams.ToArray)

                    Qry = Qry & " FROM  " & TableName

                    If SQ._HasWhereConditions = True Then
                        Qry = Qry & " WHERE "


                        If SQ._Conditions.Count > 0 Then
                            Dim ConditionsCount As Integer = 0
                            Dim pair As KeyValuePair(Of String, List(Of String))
                            For Each pair In SQ._Conditions
                                ' Display Key and Value.
                                If ConditionsCount > 0 And SQ._HasInBetweenConditions = True Then
                                    Qry = Qry & SQ._InBetweenCondition
                                End If

                                If pair.Value.Count > 0 Then
                                    Select Case pair.Key.ToString
                                        Case " AND ", " OR "
                                            Qry = Qry & " ( " & String.Join(pair.Key, pair.Value.ToArray) & " )"
                                    End Select
                                End If

                                ConditionsCount = ConditionsCount + 1

                            Next
                        End If


                    End If

                    If Not IsNothing(SQ._OrderBy) Then
                        If SQ._OrderBy <> String.Empty Then
                            Qry = Qry & " ORDER BY " & SQ._OrderBy
                        End If
                    End If


                    Using cmd As New SqlCommand(Qry)
                        cmd.Connection = SqlCon
                        Dim i As Integer = 0
                        For Each value As String In FilterParams
                            cmd.Parameters.AddWithValue(value, FilterParamsValues.Item(i))
                            i = i + 1
                        Next
                        Dim SqlAdap As New SqlDataAdapter(cmd)
                        SqlAdap.Fill(ReturnResult)
                        SqlAdap.Dispose()
                    End Using

                    Return ReturnResult
                End Using
            Catch ex As Exception
                AppSpecificFunc.WriteLog(ex)
                Return ReturnResult
            End Try
        End Function
        Public Shared Function SelectAllDataComplexCondition(ByVal SQ As ComplexSelectJoinQuery) As DataTable
            Dim ReturnResult As New DataTable
            Try
                Dim ConnectionStringToUse As String = String.Empty
                If SQ._DB = "SAP" Then
                    ConnectionStringToUse = SAPConString
                Else
                    ConnectionStringToUse = ConString
                End If
                Using SqlCon As New SqlConnection(ConnectionStringToUse)

                    SqlCon.Open()
                    

                    Dim QueryParams As New List(Of String)
                    Dim FilterParams As New List(Of String)
                    Dim FilterParamsValues As New List(Of String)
                    Dim TableNames As New List(Of String)
                    Dim TableJoins As New List(Of String)


                    Dim Qry As String = "SELECT "

                    If Not IsNothing(SQ._TopRecord) Then
                        If IsNumeric(SQ._TopRecord) Then
                            If SQ._TopRecord > 0 Then
                                Qry = Qry & " Top " & SQ._TopRecord & "  "
                            End If
                        End If
                    End If

                    




                    Dim BaseTableName As String = String.Empty
                    Dim j As Integer = 1

                    For Each Obj As JoinObj In SQ._InputTable
                        Dim InputObj As New Object
                        

                        Dim props As Type = Obj.InputObj.GetType

                        TableNames.Add(props.Name & " T" & j & "  ")
                        If Not IsNothing(Obj.Join) Then
                            TableJoins.Add(Obj.Join & props.Name & " T" & j & "  ON " & Obj.Condition)
                        End If
                        InputObj = Obj.InputObj
                        For Each member As PropertyInfo In props.GetProperties
                            QueryParams.Add("T" & j & "." & member.Name)
                             
                            If Not IsNothing(member.GetValue(InputObj, Nothing)) Then
                                FilterParams.Add("@" & member.Name & j)
                                FilterParamsValues.Add(member.GetValue(InputObj, Nothing).ToString)
                            End If

                        Next
                        j = j + 1
                    Next
                    '_InputTable = JoinObject.InputObj
                    'Dim props As Type = _InputTable.GetType
                    'Dim j As Integer = 0
                    'For Each Obj In _InputTable
                    '    For Each member As PropertyInfo In props.GetProperties
                    '        If j = 0 Then
                    '            QueryParams.Add(member.Name)
                    '            If Not IsNothing(member.GetValue(_InputTable, Nothing)) Then
                    '                FilterParams.Add("@" & member.Name)
                    '                FilterParamsValues.Add(member.GetValue(SQ._InputTable(j), Nothing).ToString)
                    '            End If
                    '        Else
                    '            If Not IsNothing(member.GetValue(SQ._InputTable(j), Nothing)) Then
                    '                FilterParams.Add("@" & member.Name & j)
                    '                FilterParamsValues.Add(member.GetValue(SQ._InputTable(j), Nothing).ToString)
                    '            End If
                    '        End If

                    '    Next
                    '    j = j + 1
                    'Next


                    Qry = Qry & String.Join(",", QueryParams.ToArray)

                    Qry = Qry & " FROM  " & TableNames.Item(0).ToString

                    Qry = Qry & String.Join("  ", TableJoins.ToArray)

                    If SQ._HasWhereConditions = True Then
                        Qry = Qry & " WHERE "


                        If SQ._Conditions.Count > 0 Then
                            Dim ConditionsCount As Integer = 0
                            Dim pair As KeyValuePair(Of String, List(Of String))
                            For Each pair In SQ._Conditions
                                ' Display Key and Value.
                                If ConditionsCount > 0 And SQ._HasInBetweenConditions = True Then
                                    Qry = Qry & SQ._InBetweenCondition
                                End If

                                If pair.Value.Count > 0 Then
                                    Select Case pair.Key.ToString
                                        Case " AND ", " OR "
                                            Qry = Qry & " ( " & String.Join(pair.Key, pair.Value.ToArray) & " )"
                                    End Select
                                End If

                                ConditionsCount = ConditionsCount + 1

                            Next
                        End If


                    End If

                    If Not IsNothing(SQ._OrderBy) Then
                        If SQ._OrderBy <> String.Empty Then
                            Qry = Qry & " ORDER BY " & SQ._OrderBy
                        End If
                    End If


                    Using cmd As New SqlCommand(Qry)
                        cmd.Connection = SqlCon
                        Dim i As Integer = 0
                        For Each value As String In FilterParams
                            cmd.Parameters.AddWithValue(value, FilterParamsValues.Item(i))
                            i = i + 1
                        Next
                        Dim SqlAdap As New SqlDataAdapter(cmd)
                        SqlAdap.Fill(ReturnResult)
                        SqlAdap.Dispose()
                    End Using

                    Return ReturnResult
                End Using
            Catch ex As Exception
                AppSpecificFunc.WriteLog(ex)
                Return ReturnResult
            End Try
        End Function
#Region "App Specific functions- Custom DB"

        Public Shared Function IncrementDocnum(ByVal InputTableName As String, ByRef NextNo As String, ByRef Connection As SqlConnection, ByRef SqlTrans As SqlTransaction) As Boolean
            Try
                Dim ReturnResult As Boolean = True


                Dim Qry As String = "UPDATE " & InputTableName & "  SET NextNo=NextNo+1 OUTPUT DELETED.NextNo WHERE SetDefault='Y' "
                Using cmd As New SqlCommand(Qry)
                    Dim DR As SqlDataReader
                    cmd.Connection = Connection
                    cmd.Transaction = SqlTrans
                    DR = cmd.ExecuteReader()
                    If DR.Read Then
                        If Not IsDBNull(DR("NextNo")) Then
                            NextNo = DR("NextNo").ToString
                        End If
                    End If
                    DR.Close()
                End Using


                Return ReturnResult
            Catch ex As Exception
                AppSpecificFunc.WriteLog(ex)
                Return False
            End Try
        End Function
        Public Shared Sub GetNextandLastNum(ByVal InputTableName As String, ByRef NextNo As String, ByRef LastNo As String)
            Try

                Using SqlCon As New SqlConnection(ConString)
                    Try

                        SqlCon.Open()
                        Dim Qry As String = "SELECT NextNo, LastNo FROM " & InputTableName & " WHERE SetDefault='Y' "

                        Using cmd As New SqlCommand(Qry)
                            Dim DR As SqlDataReader
                            cmd.Connection = SqlCon
                            DR = cmd.ExecuteReader()
                            If DR.Read Then
                                If Not IsDBNull(DR("NextNo")) Then
                                    NextNo = DR("NextNo").ToString
                                End If
                                If Not IsDBNull(DR("LastNo")) Then
                                    LastNo = DR("LastNo").ToString
                                End If
                            End If
                            DR.Close()
                        End Using
                    Catch ex As Exception
                        AppSpecificFunc.WriteLog(ex)
                    End Try
                End Using


            Catch ex As Exception
                AppSpecificFunc.WriteLog(ex)
            End Try
        End Sub
        Public Shared Function CheckFieldValueExist(ByVal TableName As String, ByVal ColumnName As String, ByVal Value As String, ByRef ErrMsg As String) As Boolean
            Dim ReturnResult As Boolean = False
            Try


                Using SqlCon As New SqlConnection(ConString)


                    SqlCon.Open()
                    Dim Qry As String = "SELECT " & ColumnName & " FROM  " & TableName & " WHERE LOWER(" & ColumnName & ")=@Value"

                    Using cmd As New SqlCommand(Qry)
                        Dim DR As SqlDataReader
                        cmd.Connection = SqlCon
                        cmd.Parameters.AddWithValue("@Value", Value.ToLower)
                        DR = cmd.ExecuteReader()
                        If DR.Read Then
                            ReturnResult = True
                        Else
                            ReturnResult = False
                        End If
                        DR.Close()
                    End Using

                End Using

                Return ReturnResult
            Catch ex As Exception
                ErrMsg = ex.Message
                AppSpecificFunc.WriteLog(ex)
                Return ReturnResult
            End Try
        End Function
        Public Shared Function CheckFieldValueExistSAP(ByVal TableName As String, ByVal ColumnName As String, ByVal Value As String, ByRef ErrMsg As String) As Boolean
            Dim ReturnResult As Boolean = False
            Try


                Using SqlCon As New SqlConnection(SAPConString)


                    SqlCon.Open()
                    Dim Qry As String = "SELECT " & ColumnName & " FROM  " & TableName & " WHERE LOWER(" & ColumnName & ")=@Value"

                    Using cmd As New SqlCommand(Qry)
                        Dim DR As SqlDataReader
                        cmd.Connection = SqlCon
                        cmd.Parameters.AddWithValue("@Value", Value.ToLower)
                        DR = cmd.ExecuteReader()
                        If DR.Read Then
                            ReturnResult = True
                        Else
                            ReturnResult = False
                        End If
                        DR.Close()
                    End Using

                End Using

                Return ReturnResult
            Catch ex As Exception
                ErrMsg = ex.Message
                AppSpecificFunc.WriteLog(ex)
                Return ReturnResult
            End Try
        End Function
        Public Shared Function GetTableSchemeIntoDataTable(ByVal TableName As String, ByRef InputDataTable As DataTable) As Boolean
            Try
                Dim ReturnResult As Boolean = True
                Using SqlCon As New SqlConnection(ConString)


                    SqlCon.Open()


                    Using adapter As New SqlDataAdapter("SELECT TOP 0 * FROM " & TableName, SqlCon)
                        adapter.FillSchema(InputDataTable, SchemaType.Mapped)
                    End Using

                End Using
                Return ReturnResult
            Catch ex As Exception
                AppSpecificFunc.WriteLog(ex)
                Return False
            End Try
        End Function
        Public Shared Function GetTableSchemeDataTableDetails(ByVal TableName As String, ByRef InputDataTable As DataTable) As Boolean
            ' Can retrive table schema information return by this function
            ' using the below code
            ' CURD.GetTableSchemeIntoDataTable("DOFileImportList", inputTable)
            ' For Each row As DataRow In inputTable.Rows
            '    Dim name = row("ColumnName")
            '    Dim size = row("ColumnSize")
            '    'append these to a string or StringBuilder for writing out later...
            '    Dim dataType = row("DataTypeName")
            'Next
            Try
                Dim ReturnResult As Boolean = True
                Using SqlCon As New SqlConnection(ConString)

                    SqlCon.Open()
                    Dim Qry As String = "SELECT TOP 0 * FROM " & TableName
                    Using cmd As New SqlCommand(Qry)
                        Dim DR As SqlDataReader
                        cmd.Connection = SqlCon
                        DR = cmd.ExecuteReader()
                        InputDataTable = DR.GetSchemaTable
                    End Using

                End Using
                Return ReturnResult
            Catch ex As Exception
                AppSpecificFunc.WriteLog(ex)
                Return False
            End Try
        End Function
#End Region

    End Class
End Namespace

