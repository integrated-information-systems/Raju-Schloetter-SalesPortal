Public Class Login
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        'RegisterHyperLink.NavigateUrl = "Register.aspx?ReturnUrl=" + HttpUtility.UrlEncode(Request.QueryString("ReturnUrl"))
        If User.Identity.Name <> Nothing Then
            Response.Redirect("~")
        End If
    End Sub

     

    Private Sub LoginUser_LoggedIn(sender As Object, e As System.EventArgs) Handles LoginUser.LoggedIn
        'Get User tagged SalesPersons
        Dim UserProfile As ProfileBase = System.Web.Profile.ProfileBase.Create(LoginUser.UserName)
        Dim SalesPersonList As String = UserProfile.GetPropertyValue("SalesPersons").ToString
        Dim SplittedSalesPersons() As String = SalesPersonList.Split("|")
        Session("SPs") = SplittedSalesPersons

    End Sub

    Private Sub LoginUser_LoggingIn(sender As Object, e As System.Web.UI.WebControls.LoginCancelEventArgs) Handles LoginUser.LoggingIn
        
       
    End Sub
End Class