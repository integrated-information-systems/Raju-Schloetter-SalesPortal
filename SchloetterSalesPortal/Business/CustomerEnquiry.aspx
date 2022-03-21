<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="CustomerEnquiry.aspx.vb" Inherits="SchloetterSalesPortal.CustomerEnquiry" MaintainScrollPositionOnPostback="true" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="act" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
 <asp:ScriptManager ID="ScriptManager1" runat="server"
EnablePageMethods = "true">
</asp:ScriptManager>
<h2>Customer Enquiry</h2>
<div class="MainContent">
    <div class="FormHeader">
        <div class="twocolleft">
            <div class="Table">
                <div class="Row">
                    <div class="Cell"><asp:Label ID="lblForCustomerCode" runat="server" Text="Customer Code:" AssociatedControlID="CustomerCode"></asp:Label></div>
                    <div class="Cell"><asp:TextBox ID="CustomerCode" runat="server" MaxLength="15" CssClass="textEntry" AutoPostBack="true"></asp:TextBox>
                                      <asp:RequiredFieldValidator ID="CustomerCoderRequired" runat="server" ControlToValidate="CustomerCode" ErrorMessage="Customer code required" ValidationGroup="HeaderValidation" >*</asp:RequiredFieldValidator> 
                                      <act:AutoCompleteExtender ServiceMethod="GetCustomerCodes"
                                MinimumPrefixLength="2"  
                                CompletionInterval="100" EnableCaching="false" CompletionSetCount="10"  
                                TargetControlID="CustomerCode" CompletionListElementID="CardCodeAutoCompleteContainer" 
                                ID="AutoCompleteExtenderCustomerCode" runat="server" FirstRowSelected = "false">
                        </act:AutoCompleteExtender>         
                         <div id="CardCodeAutoCompleteContainer"></div>

                     </div>
                </div>
                <div class="Row">
                    <div class="Cell"><asp:Label ID="lblForCustomerName" runat="server" Text="Customer Name:" AssociatedControlID="CustomerName"></asp:Label></div>
                    <div class="Cell">
                        <asp:TextBox ID="CustomerName" runat="server" MaxLength="100" CssClass="textEntry" AutoPostBack="true"></asp:TextBox>
                                      <asp:RequiredFieldValidator ID="CustomerNameRequired" runat="server" ControlToValidate="CustomerName" ErrorMessage="Customer name  required" ValidationGroup="HeaderValidation" >*</asp:RequiredFieldValidator> 
                                      <act:AutoCompleteExtender ServiceMethod="GetCustomerNames"
                                MinimumPrefixLength="2"  
                                CompletionInterval="100" EnableCaching="false" CompletionSetCount="10"  
                                TargetControlID="CustomerName" CompletionListElementID="CardNameAutoCompleteContainer" 
                                ID="AutoCompleteExtenderCustomerName" runat="server" FirstRowSelected = "false">
                        </act:AutoCompleteExtender>         
                         <div id="CardNameAutoCompleteContainer"></div>
                    </div>
                </div>
                <div class="Row">
                    <div class="Cell"></div>
                    <div class="Cell">
                        <asp:Button ID="btnSearch" runat="server" Text="Search" /> &nbsp;<asp:Button ID="btnClear" runat="server" Text="Clear" />
                    </div>
                </div>
                <div class="Row">
                    <div class="Cell"><asp:Label ID="lblInformation" runat="server" Text="Customer Information" Font-Bold='true'></asp:Label></div>
                </div>
                <div class="Row">
                    <div class="Cell"><asp:Label ID="lblForFoundCustomerCode" runat="server" Text="Customer Code:" AssociatedControlID="FoundCustomerCode"></asp:Label></div>
                    <div class="Cell"><asp:TextBox ID="FoundCustomerCode" runat="server" ReadOnly="true" CssClass="textEntry" ></asp:TextBox></div>
                </div>
                <div class="Row">
                    <div class="Cell"><asp:Label ID="lblForFoundCustomerName" runat="server" Text="Customer Name:" AssociatedControlID="FoundCustomerName"></asp:Label></div>
                    <div class="Cell"><asp:TextBox ID="FoundCustomerName" runat="server" ReadOnly="true" CssClass="textEntry" ></asp:TextBox></div>
                </div>
                <div class="Row">
                    <div class="Cell" style="vertical-align:top"><asp:Label ID="lblForBillingAddress" runat="server" Text="Billing Address:" AssociatedControlID="BillingAddress"></asp:Label></div>
                    <div class="Cell"><asp:TextBox ID="BillingAddress" runat="server" ReadOnly="true" Columns="35" Rows="5" TextMode="MultiLine"  CssClass="FixedWithMultilineTexBox, NoResize"></asp:TextBox></div>
                </div>
                <div class="Row">
                    <div class="Cell" style="vertical-align:top"><asp:Label ID="lblForDeliveryAddress" runat="server" Text="Delivery Address:" AssociatedControlID="DeliveryAddress"></asp:Label></div>
                    <div class="Cell"><asp:TextBox ID="DeliveryAddress" runat="server" ReadOnly="true" Columns="35" Rows="5" TextMode="MultiLine"  CssClass="FixedWithMultilineTexBox, NoResize"></asp:TextBox></div>
                </div>
                <div class="Row">
                    <div class="Cell"><asp:Label ID="lblForPaymentTerms" runat="server" Text="Payment Terms:" AssociatedControlID="PaymentTerms"></asp:Label></div>
                    <div class="Cell"><asp:TextBox ID="PaymentTerms" runat="server" ReadOnly="true" CssClass="textEntry" ></asp:TextBox></div>
                </div>
                <div class="Row">
                    <div class="Cell"><asp:Label ID="lblForDefaultCntPerson" runat="server" Text="Default Contact Person:" AssociatedControlID="DefaultCntPerson"></asp:Label></div>
                    <div class="Cell"><asp:TextBox ID="DefaultCntPerson" runat="server" ReadOnly="true" CssClass="textEntry" ></asp:TextBox></div>
                </div>
                <div class="Row">
                    <div class="Cell"><asp:Label ID="lblForTel1" runat="server" Text="Tel1:" AssociatedControlID="Tel1"></asp:Label></div>
                    <div class="Cell"><asp:TextBox ID="Tel1" runat="server" ReadOnly="true" CssClass="textEntry" ></asp:TextBox></div>
                </div>
                <div class="Row">
                    <div class="Cell"><asp:Label ID="lblForTel2" runat="server" Text="Tel2:" AssociatedControlID="Tel2"></asp:Label></div>
                    <div class="Cell"><asp:TextBox ID="Tel2" runat="server" ReadOnly="true" CssClass="textEntry" ></asp:TextBox></div>
                </div>
                <div class="Row">
                    <div class="Cell"><asp:Label ID="lblForCellular" runat="server" Text="Mobile:" AssociatedControlID="Cellular"></asp:Label></div>
                    <div class="Cell"><asp:TextBox ID="Cellular" runat="server" ReadOnly="true" CssClass="textEntry" ></asp:TextBox></div>
                </div>
                <div class="Row">
                    <div class="Cell"><asp:Label ID="lblEmail" runat="server" Text="Email:" AssociatedControlID="Email"></asp:Label></div>
                    <div class="Cell"><asp:TextBox ID="Email" runat="server" ReadOnly="true" CssClass="textEntry" ></asp:TextBox></div>
                </div>
                <div class="Row">
                    <div class="Cell"><asp:Label ID="lblForFax" runat="server" Text="Fax:" AssociatedControlID="Fax"></asp:Label></div>
                    <div class="Cell"><asp:TextBox ID="Fax" runat="server" ReadOnly="true" CssClass="textEntry" ></asp:TextBox></div>
                </div>
                <div class="Row">
                    <div class="Cell"><asp:Label ID="lblForActive" runat="server" Text="Active:" AssociatedControlID="Active"></asp:Label></div>
                    <div class="Cell"><asp:TextBox ID="Active" runat="server" ReadOnly="true" CssClass="textEntry" ></asp:TextBox></div>
                </div>
            </div>
        </div>
        
    </div>
</div>
</asp:Content>
