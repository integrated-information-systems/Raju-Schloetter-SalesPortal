<%@ Page Title="Home Page" Language="vb" MasterPageFile="~/Site.Master" AutoEventWireup="false"
    CodeBehind="Default.aspx.vb" Inherits="SchloetterSalesPortal._Default" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <h2>
        Announcements
    </h2>   
     
    <div > 
<marquee  style="background-color:#EEEEEE;border-color:#6699CC;border-width:1px;border-style:Solid;" onMouseOver="this.setAttribute('scrollamount', 0, 0);" onMouseOut="this.setAttribute('scrollamount', 2, 0);" bgcolor="#EEEEEE" scrollamount="2" direction="up" loop="true" width="30%" height="150">
<center>
<asp:Label ID="lblMessages" runat="server" Text=""></asp:Label>
</center></marquee>
</div>
</asp:Content>
