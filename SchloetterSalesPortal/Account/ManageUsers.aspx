﻿<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="ManageUsers.aspx.vb" Inherits="SchloetterSalesPortal.ManageUsers" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
    <style type="text/css">
        .style1
        {
            width: 100%;
        }
        .style2
        {
            width: 594px;
        }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
    <h2>Manage Users</h2>
                        <div class="MainContent">
                    <div class="FormHeader">  
                   
<div id="content">
<div id="left" style="float:left;width:40%;border:1px; padding-right: 20px;">
 
         
                <asp:ValidationSummary ID="RegisterUserValidationSummary" runat="server" 
                    CssClass="failureNotification" ValidationGroup="RegisterUserValidationGroup" />
                    <asp:Label ID="lblMsg" runat="server" Visible="False"></asp:Label>
          <div >
                        <fieldset class="register">
                         <legend>Update Account Information</legend>
                         <p>
                             <asp:Label ID="Label1" runat="server" AssociatedControlID="UserName">User Name:</asp:Label>
                              <asp:TextBox ID="UserName" runat="server" ReadOnly="True" CssClass="textEntry"  MaxLength="20"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="UserNameRequired" runat="server" ControlToValidate="UserName" CssClass="failureNotification" 
                            ErrorMessage="Please Select User." ToolTip="Please Select User." ValidationGroup="RegisterUserValidationGroup">*</asp:RequiredFieldValidator>
                         </p>
                         <p>
                          <asp:Label ID="EmailLabel" runat="server" AssociatedControlID="Email">E-mail:</asp:Label>
                          <asp:TextBox ID="Email" runat="server" CssClass="textEntry"  MaxLength="50"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="EmailRequired" runat="server" 
                                            ControlToValidate="Email" CssClass="failureNotification" 
                                            ErrorMessage="E-mail is required." ToolTip="E-mail is required." 
                                            ValidationGroup="RegisterUserValidationGroup">*</asp:RequiredFieldValidator>
                         </p>
                         <p>
                          <asp:Label ID="PasswordLabel" runat="server" AssociatedControlID="Password">
                        Password:</asp:Label>
                            <asp:TextBox ID="Password" runat="server" TextMode="Password" CssClass="textEntry"></asp:TextBox><asp:RegularExpressionValidator    CssClass="failureNotification" Display = "static"  ControlToValidate = "Password" ID="RegularExpressionValidator3" ValidationGroup="RegisterUserValidationGroup" ValidationExpression = "^[\s\S]{6,}$" runat="server" ErrorMessage="Min 6 and characters required.">*</asp:RegularExpressionValidator>
                         </p>
                         <p>
                         <asp:Label ID="ConfirmPasswordLabel" runat="server" AssociatedControlID="ConfirmPassword">
                        Confirm Password:</asp:Label>
                        <asp:TextBox ID="ConfirmPassword" runat="server" TextMode="Password" 
                      CssClass="textEntry" ></asp:TextBox><asp:CompareValidator ID="PasswordCompare" runat="server" ControlToCompare="Password"
                        ControlToValidate="ConfirmPassword"    CssClass="failureNotification" Display="Dynamic" ErrorMessage="confirm password doesn't match with Password"
                        ValidationGroup="RegisterUserValidationGroup">*</asp:CompareValidator>
                         </p>
                         <p>
                          <asp:Label ID="SalesPersonLabel" runat="server" AssociatedControlID="SalesPersonLst">Sales Persons:</asp:Label>
                              <asp:ListBox ID="SalesPersonLst" runat="server" AppendDataBoundItems="true" SelectionMode="Multiple"></asp:ListBox>
                              <asp:RequiredFieldValidator ID="SalesPersonRequired" runat="server" ControlToValidate="SalesPersonLst" CssClass="failureNotification" 
                            ErrorMessage="Please select Sales Person." ToolTip="Please select  Sales Person." ValidationGroup="RegisterUserValidationGroup">*</asp:RequiredFieldValidator>
                         </p>
                         </fieldset>
                         <p class="submitButton"><asp:Button ID="btnSubmit" runat="server" Text="Update"  ValidationGroup="RegisterUserValidationGroup" /> </p>
            </div>    
            
              
</div>
<div id="right" style="float:right;width:55%;">
<div >
                        <fieldset class="register">
                         <legend>List of Users</legend>
 <asp:GridView ID="UsersList" runat="server" AutoGenerateColumns="false"  Width="100%" HeaderStyle-HorizontalAlign="Left"
                    AutoGenerateDeleteButton="True" AutoGenerateSelectButton="True" CellPadding="2" 
                    ForeColor="#333333" GridLines="None" onrowdeleting="UsersList_RowDeleting">
                    <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                    <EditRowStyle BackColor="#999999" />
                    <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                    <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                    <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                    <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                    <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                    <SortedAscendingCellStyle BackColor="#E9E7E2" />
                    <SortedAscendingHeaderStyle BackColor="#506C8C" />
                    <SortedDescendingCellStyle BackColor="#FFFDF8" />
                    <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
                    <Columns>
                        <asp:BoundField DataField="Username" HeaderText="Username" ReadOnly="true" />
                        <asp:BoundField DataField="Email" HeaderText="Email" />
                    </Columns>
                </asp:GridView>
                </fieldset>
</div>
</div>
</div>    
 </div>
</div>              
<div id="dialog" title="Message"><asp:Label runat="server" Text="" ID="LblShowMsg"></asp:Label></div>         
</asp:Content>

