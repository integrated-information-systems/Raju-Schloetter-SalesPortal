Imports Microsoft.VisualBasic
Namespace Models
    Public Enum DocStatus As Integer
        Status_New = 1
        Status_Open = 2
        Status_Closed = 3
        Status_Waiting_For_Syncing = 4
        Status_Sync_Failed = 5
        Status_Cancelled = 6
    End Enum
End Namespace

