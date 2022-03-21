<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="SalesOrder.aspx.vb" Inherits="SchloetterSalesPortal.SalesOrder" MaintainScrollPositionOnPostback="true" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="act" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
   <asp:ScriptManager ID="ScriptManager1" runat="server"
EnablePageMethods = "true">
</asp:ScriptManager>
<h2>Sales Order</h2>
    <asp:ValidationSummary ID="HeaderValidationSummary" runat="server" ValidationGroup="HeaderValidation" />
    <asp:ValidationSummary ID="FindDocNoValiadationSummary" runat="server" ValidationGroup="FindDocNoValidation" />
    <asp:ValidationSummary ID="FindSysDocNoValiadationSummary" runat="server" ValidationGroup="FindSAPDocNoValidation" />
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
                    <div class="Cell"><asp:Label ID="lblForPaymentTerms" runat="server" Text="Payment Terms:" AssociatedControlID="PaymentTerms"></asp:Label></div>
                    <div class="Cell">
                        <asp:DropDownList ID="PaymentTerms" runat="server">
                        </asp:DropDownList>
                    </div>
                </div>
                <div class="Row">
                    <div class="Cell"><asp:Label ID="Label1" runat="server" Text="PO No:" AssociatedControlID="PONo"></asp:Label></div>
                    <div class="Cell"><asp:TextBox ID="PONo" runat="server" MaxLength="100" CssClass="textEntry"></asp:TextBox>                                                              
                     </div>
                </div>
                <div class="Row">
                    <div class="Cell"><asp:Label ID="lblForShipAddressID" runat="server" Text="Delivery Address ID:" AssociatedControlID="ShipTo"></asp:Label></div>
                    <div class="Cell">
                        <asp:DropDownList ID="ShipTo" runat="server" AutoPostBack="true">
                        </asp:DropDownList>
                        <asp:CustomValidator  id="ValidShipToRequired"  runat="server"  ValidationGroup="HeaderValidation" OnServerValidate="ShipToValidator"   ErrorMessage="Valid Delivery Address ID required" Display="Dynamic">*</asp:CustomValidator>
                    </div>
                </div>
                <div class="Row">
                    <div class="Cell"><asp:Label ID="lblForAddressLine1" runat="server" Text="Address Line 1:" AssociatedControlID="AddressLine1"></asp:Label></div>
                    <div class="Cell">
                        <asp:TextBox runat="server" ID="AddressLine1" ReadOnly="true" CssClass="textEntry"></asp:TextBox>
                    </div>
                </div>
                <div class="Row">
                    <div class="Cell"><asp:Label ID="lblForAddressLine2" runat="server" Text="Address Line 2:" AssociatedControlID="AddressLine2"></asp:Label></div>
                    <div class="Cell">
                        <asp:TextBox runat="server" ID="AddressLine2" ReadOnly="true" CssClass="textEntry"></asp:TextBox>
                    </div>
                </div>
                <div class="Row">
                    <div class="Cell"><asp:Label ID="lblForAddressLine3" runat="server" Text="Address Line 3:" AssociatedControlID="AddressLine3"></asp:Label></div>
                    <div class="Cell">
                        <asp:TextBox runat="server" ID="AddressLine3" ReadOnly="true" CssClass="textEntry"></asp:TextBox>
                    </div>
                </div>
                <div class="Row">
                    <div class="Cell"><asp:Label ID="lblForAddressLine4" runat="server" Text="Address Line 4:" AssociatedControlID="AddressLine4"></asp:Label></div>
                    <div class="Cell">
                        <asp:TextBox runat="server" ID="AddressLine4" ReadOnly="true" CssClass="textEntry"></asp:TextBox>
                    </div>
                </div>
                <div class="Row">
                    <div class="Cell"><asp:Label ID="lblForAddressLine5" runat="server" Text="Address Line 5:" AssociatedControlID="AddressLine5"></asp:Label></div>
                    <div class="Cell">
                        <asp:TextBox runat="server" ID="AddressLine5" ReadOnly="true" CssClass="textEntry"></asp:TextBox>
                    </div>
                </div>
            </div>
        </div>
        <div class="twocolright30">
            <div class="Table">
                            
                <div class="Row">
                    <div class="Cell"><asp:Button ID="btnFindDocNo" runat="server" Text="Find by Web SO No" ValidationGroup="FindDocNoValidation" /></div>
                    <div class="Cell"><asp:TextBox ID="txtFindDocNo" CssClass="textHeaderDecimalEntry" runat="server" MaxLength="50"></asp:TextBox>
                        <asp:RequiredFieldValidator runat="server" ID="FindDocNoRequired" Display="Dynamic" ControlToValidate="txtFindDocNo" ErrorMessage="Enter a valid Document No" ValidationGroup="FindDocNoValidation">*</asp:RequiredFieldValidator>                                      
                        <act:AutoCompleteExtender ServiceMethod="GetDocNos"
                    MinimumPrefixLength="1"
                    CompletionInterval="100" EnableCaching="false" CompletionSetCount="10"  
                    TargetControlID="txtFindDocNo" CompletionListElementID="FindDONoAutoCompleteContainer"
                    ID="FindDONoAutoCompleteExtender" runat="server" FirstRowSelected = "false"> </act:AutoCompleteExtender>
                        <div id="FindDONoAutoCompleteContainer"></div>  
                     </div>                       
                </div>    
                <div class="Row">
                    <div class="Cell">
                        <asp:Button ID="btnFindSAPDocNo" runat="server" Text="Find by SAP SO No" ValidationGroup="FindSAPDocNoValidation" />
                    </div>
                    <div class="Cell"><asp:TextBox ID="txtFindSAPDocNo" runat="server" MaxLength="20" CssClass="textHeaderDecimalEntry"></asp:TextBox>
                     <asp:RequiredFieldValidator runat="server" ID="FindSAPDocNoRequired" Display="Dynamic" ControlToValidate="txtFindSAPDocNo" ErrorMessage="Enter a valid SAP Document No" ValidationGroup="FindSAPDocNoValidation">*</asp:RequiredFieldValidator>
                     <act:AutoCompleteExtender ServiceMethod="GetSAPDocNos"
                    MinimumPrefixLength="1"
                    CompletionInterval="100" EnableCaching="false" CompletionSetCount="10"  
                    TargetControlID="txtFindSAPDocNo" CompletionListElementID="FindSAPDocNoAutoCompleteContainer" 
                    ID="FindSAPDocNoAutoCompleteExtender" runat="server" FirstRowSelected = "false"> </act:AutoCompleteExtender>         
                    <div id="FindSAPDocNoAutoCompleteContainer"></div>
                    </div>                   
                </div>                  
                <div class="Row">
                    <div class="Cell"><asp:Label ID="lblForIdKey" runat="server" Text="Web SO No:" AssociatedControlID="SONo"></asp:Label></div>
                    <div class="Cell"><asp:Label ID="SONo" runat="server" Text=""></asp:Label></div>
                </div>
                <div class="Row">
                    <div class="Cell"><asp:Label ID="lblForSAPSONo" runat="server" Text="SAP SO No:" AssociatedControlID="SAPSONo"></asp:Label></div>
                    <div class="Cell"><asp:Label ID="SAPSONo" runat="server" Text=""></asp:Label></div>
                </div>
                <div class="Row">
                    <div class="Cell"><asp:Label ID="lblForStatus" runat="server" Text="Status:" AssociatedControlID="Status"></asp:Label></div>
                    <div class="Cell"><asp:Label ID="Status" runat="server" Text=""></asp:Label></div>
                </div>
                <div class="Row">
                    <div class="Cell"><asp:Label ID="lblForDocDate" runat="server" Text="Document Date:" AssociatedControlID="DocDate"></asp:Label></div>
                    <div class="Cell"><asp:TextBox ID="DocDate" runat="server" CssClass="textDateEntry" MaxLength="10"></asp:TextBox>
                    <asp:CustomValidator  id="DocDateRequired"  runat="server"  ValidationGroup="HeaderValidation" OnServerValidate="DocDateValidator"   ErrorMessage="Document Date is not valid" Display="Dynamic">*</asp:CustomValidator>
                        <act:CalendarExtender ID="DocDateCalendarExtender" TargetControlID="DocDate" runat="server" Format="yyyy-MM-dd" /></div>
                </div>
                <div class="Row">
                    <div class="Cell"><asp:Label ID="lblForDeliveryDate" runat="server" Text="Delivery Date:" AssociatedControlID="DeliveryDate" CssClass="bold"></asp:Label></div>
                    <div class="Cell"><asp:TextBox ID="DeliveryDate" runat="server" CssClass="textDateEntry" MaxLength="10"></asp:TextBox>
                    <asp:CustomValidator  id="DeliveryDateRequired"  runat="server"  ValidationGroup="HeaderValidation" OnServerValidate="DeliveryDateValidator"   ErrorMessage="Delivery Date is not valid" Display="Dynamic">*</asp:CustomValidator>
                        <act:CalendarExtender ID="DeliveryDateCalendarExtender" TargetControlID="DeliveryDate" runat="server" Format="yyyy-MM-dd" /></div>
                </div>
            </div>
        </div>
    </div>
    <div class="GridViewItems">
        <asp:ValidationSummary ID="ValidationSummaryItemsAddValidation" ValidationGroup="ItemsAddValidation" runat="server" />
        <asp:ValidationSummary ID="ValidationSummaryItemsUpdateValidation" ValidationGroup="ItemsUpdateValidation" runat="server" />
        <center>
            <asp:GridView ID="SalesOrderLinesGrid"
             runat="server"
             CellPadding="4"
             ForeColor="#333333"
             AutoGenerateColumns="false"
             ShowFooter="true"
             DataKeyNames=""
             ShowHeaderWhenEmpty="true"
             GridLines="None"
             EmptyDataText="No Record found"
             CssClass="ItemGrid"                             
             >
            <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
            <EditRowStyle BackColor="#999999" />
     
            <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
            <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
            <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
            <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
            <SortedAscendingCellStyle BackColor="#E9E7E2" />
            <SortedAscendingHeaderStyle BackColor="#506C8C" />
            <SortedDescendingCellStyle BackColor="#FFFDF8" />
            <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
            <Columns>                            
                <asp:TemplateField HeaderText="Line No" HeaderStyle-HorizontalAlign="Left">
                    <EditItemTemplate>
                        <asp:Label ID="lblLineNum" runat="server" Text='<%# Bind("LineNum") %>'   ></asp:Label>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="lblLineNum" runat="server" Text='<%# Bind("LineNum") %>'   ></asp:Label> 
                    </ItemTemplate>
                    <FooterTemplate>
                    </FooterTemplate>
                </asp:TemplateField>
                
                <asp:TemplateField HeaderText="Item Code" HeaderStyle-HorizontalAlign="Left">
                    <EditItemTemplate>
                        <asp:TextBox ID="ItemCode" runat="server" Text='<%# Bind("ItemCode") %>' MaxLength="50" AutoPostBack="true" OnTextChanged="ItemCode_TextChanged"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="ItemCodeRequiredFieldValidator" runat="server" ErrorMessage="Item code Required" ControlToValidate="ItemCode" ValidationGroup="ItemsUpdateValidation">*</asp:RequiredFieldValidator>
                        <asp:CustomValidator  id="ValidItemCodeRequired"  runat="server"  ValidationGroup="ItemsUpdateValidation" OnServerValidate="ItemCodeValidator"   ErrorMessage="Valid Itemcode required" Display="Dynamic">*
</asp:CustomValidator>
                        <act:AutoCompleteExtender ServiceMethod="GetItemCodes"
                                        MinimumPrefixLength="1"
                                        CompletionInterval="100" EnableCaching="false" CompletionSetCount="10"  
                                        TargetControlID="ItemCode" CompletionListElementID="ItemCodeAutoCompleteEditContainer" 
                                        ID="AutoCompleteExtenderItemCode" runat="server" FirstRowSelected = "false"></act:AutoCompleteExtender>
                                      <div id="ItemCodeAutoCompleteEditContainer"></div>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="lblItemCode" runat="server" Text='<%# Bind("ItemCode") %>'   ></asp:Label> 
                    </ItemTemplate>
                    <FooterTemplate>
                        <asp:TextBox ID="ItemCode" runat="server" Text='<%# Bind("ItemCode") %>' MaxLength="50" AutoPostBack="true"  OnTextChanged="ItemCode_TextChanged"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="ItemCodeRequiredFieldValidator" runat="server" ErrorMessage="Item code Required" ControlToValidate="ItemCode" ValidationGroup="ItemsAddValidation">*</asp:RequiredFieldValidator>
                        <asp:CustomValidator  id="ValidItemCodeRequired"  runat="server"  ValidationGroup="ItemsAddValidation" OnServerValidate="ItemCodeValidator"   ErrorMessage="Valid Itemcode required" Display="Dynamic">*
</asp:CustomValidator>
                        <act:AutoCompleteExtender ServiceMethod="GetItemCodes"
                                        MinimumPrefixLength="1"
                                        CompletionInterval="100" EnableCaching="false" CompletionSetCount="10"  
                                        TargetControlID="ItemCode" CompletionListElementID="ItemCodeAutoCompleteAddContainer" 
                                        ID="AutoCompleteExtenderItemCode" runat="server" FirstRowSelected = "false"></act:AutoCompleteExtender>
                                      <div id="ItemCodeAutoCompleteAddContainer"></div>
                    </FooterTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Description" HeaderStyle-HorizontalAlign="Left" > 
                    <EditItemTemplate> 
                        <asp:TextBox ID="Description" runat="server" Text='<%# Bind("Description") %>' MaxLength="100" AutoPostBack="true" OnTextChanged="ItemName_TextChanged"></asp:TextBox> 
                        <act:AutoCompleteExtender ServiceMethod="GetItemNames"
                            MinimumPrefixLength="2"
                            CompletionInterval="100" EnableCaching="false" CompletionSetCount="10"   CompletionListElementID="ItemNameEditCompleteContainer"
                            TargetControlID="Description"
                            ID="AutoCompleteExtenderDescription" runat="server" FirstRowSelected = "false"></act:AutoCompleteExtender>
                            <div id="ItemNameEditCompleteContainer"></div>  
                    </EditItemTemplate> 
                    <FooterTemplate> 
                        <asp:TextBox ID="Description" runat="server" MaxLength="100" AutoPostBack="true" OnTextChanged="ItemName_TextChanged"></asp:TextBox>
                        <act:AutoCompleteExtender ServiceMethod="GetItemNames"
                            MinimumPrefixLength="2"
                            CompletionInterval="100" EnableCaching="false" CompletionSetCount="10"   CompletionListElementID="ItemNameAddCompleteContainer"
                            TargetControlID="Description"
                            ID="AutoCompleteExtenderDescription" runat="server" FirstRowSelected = "false"></act:AutoCompleteExtender>
                            <div id="ItemNameAddCompleteContainer"></div>  
                    </FooterTemplate> 
                    <ItemTemplate> 
                        <asp:Label ID="lblDescription" runat="server" Text='<%# Bind("Description") %>' CssClass="FixedWithLabel140" ></asp:Label> 
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="In Stock" HeaderStyle-HorizontalAlign="Left" >
                    <EditItemTemplate>
                        <asp:TextBox ID="InStock" runat="server" Text='<%# Bind("InStock") %>' MaxLength="20"  CssClass="textDecimalEntry"  ReadOnly="true"  ></asp:TextBox>                        
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="lblInStock" runat="server" Text='<%# Bind("InStock") %>'  ></asp:Label> 
                    </ItemTemplate>
                    <FooterTemplate>
                        <asp:TextBox ID="InStock" runat="server" Text='<%# Bind("InStock") %>' MaxLength="20" CssClass="textDecimalEntry" ReadOnly="true"></asp:TextBox>                        
                    </FooterTemplate>
                </asp:TemplateField> 
                <asp:TemplateField HeaderText="Sales Ordered" HeaderStyle-HorizontalAlign="Left" >
                    <EditItemTemplate>
                        <asp:TextBox ID="SalesOrdered" runat="server" Text='<%# Bind("SalesOrdered") %>' MaxLength="20"  CssClass="textDecimalEntry"  ReadOnly="true"  ></asp:TextBox>                        
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="lblSalesOrdered" runat="server" Text='<%# Bind("SalesOrdered") %>'  ></asp:Label> 
                    </ItemTemplate>
                    <FooterTemplate>
                        <asp:TextBox ID="SalesOrdered" runat="server" Text='<%# Bind("SalesOrdered") %>' MaxLength="20" CssClass="textDecimalEntry" ReadOnly="true"></asp:TextBox>                        
                    </FooterTemplate>
                </asp:TemplateField> 
                <asp:TemplateField HeaderText="Available" HeaderStyle-HorizontalAlign="Left" >
                    <EditItemTemplate>
                        <asp:TextBox ID="Available" runat="server" Text='<%# Bind("Available") %>' MaxLength="20"  CssClass="textDecimalEntry"  ReadOnly="true"  ></asp:TextBox>                        
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="lblAvailable" runat="server" Text='<%# Bind("Available") %>'  ></asp:Label> 
                    </ItemTemplate>
                    <FooterTemplate>
                        <asp:TextBox ID="Available" runat="server" Text='<%# Bind("Available") %>' MaxLength="20" CssClass="textDecimalEntry" ReadOnly="true"></asp:TextBox>                        
                    </FooterTemplate>
                </asp:TemplateField> 
                <asp:TemplateField HeaderText="Qty" HeaderStyle-HorizontalAlign="Left" >
                    <EditItemTemplate>
                        <asp:TextBox ID="Qty" runat="server" Text='<%# Bind("Qty") %>' MaxLength="20" CssClass="textQtyEntry" AutoPostBack="false" OnTextChanged="Qty_TextChanged"></asp:TextBox>
                        <asp:CustomValidator  id="ValidQtyRequired"  runat="server"  ValidationGroup="ItemsUpdateValidation" OnServerValidate="QtyValidator"   ErrorMessage="Valid Qty required" Display="Dynamic">*</asp:CustomValidator>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="lblQty" runat="server" Text='<%# Bind("Qty") %>'  ></asp:Label> 
                    </ItemTemplate>
                    <FooterTemplate>
                        <asp:TextBox ID="Qty" runat="server" Text='<%# Bind("Qty") %>' MaxLength="20" CssClass="textQtyEntry" AutoPostBack="false" OnTextChanged="Qty_TextChanged"></asp:TextBox>
                        
                    </FooterTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="UOM" HeaderStyle-HorizontalAlign="Left" >
                    <EditItemTemplate>
                        <asp:TextBox ID="UOM" runat="server" Text='<%# Bind("UOM") %>' MaxLength="100" CssClass="textQtyEntry" ReadOnly="true"></asp:TextBox>                        
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="lblUOM" runat="server" Text='<%# Bind("UOM") %>'  ></asp:Label> 
                    </ItemTemplate>
                    <FooterTemplate>
                        <asp:TextBox ID="UOM" runat="server" Text='<%# Bind("UOM") %>' MaxLength="100" CssClass="textQtyEntry" ReadOnly="true" ></asp:TextBox>                        
                    </FooterTemplate>
                </asp:TemplateField>
               <%-- <asp:TemplateField HeaderText="Origin" HeaderStyle-HorizontalAlign="Left" >
                    <EditItemTemplate>
                        <asp:TextBox ID="Origin" runat="server" Text='<%# Bind("Origin") %>' MaxLength="50" CssClass="textUOMEntry" ></asp:TextBox>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="lblOrigin" runat="server" Text='<%# Bind("Origin") %>'  ></asp:Label> 
                    </ItemTemplate>
                    <FooterTemplate>
                        <asp:TextBox ID="Origin" runat="server" Text='<%# Bind("Origin") %>' MaxLength="50"  CssClass="textUOMEntry"></asp:TextBox>
                    </FooterTemplate>
                </asp:TemplateField>--%>
                <asp:TemplateField HeaderText="Price" HeaderStyle-HorizontalAlign="Left" >
                    <EditItemTemplate>
                        <asp:TextBox ID="Price" runat="server" Text='<%# Bind("Price") %>' MaxLength="20"  CssClass="textDecimalEntry" AutoPostBack="false" OnTextChanged="UnitPrice_TextChanged"></asp:TextBox>
                        <asp:CustomValidator  id="ValidUnitPriceRequired"  runat="server"  ValidationGroup="ItemsUpdateValidation" OnServerValidate="UnitPriceValidator"   ErrorMessage="Valid Unit Price required" Display="Dynamic">*</asp:CustomValidator>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="lblPrice" runat="server" Text='<%# Bind("Price") %>'  ></asp:Label> 
                    </ItemTemplate>
                    <FooterTemplate>
                        <asp:TextBox ID="Price" runat="server" Text='<%# Bind("Price") %>' MaxLength="20" CssClass="textDecimalEntry" AutoPostBack="false" OnTextChanged="UnitPrice_TextChanged"></asp:TextBox>
                        <asp:CustomValidator  id="ValidUnitPriceRequired"  runat="server"  ValidationGroup="ItemsAddValidation" OnServerValidate="UnitPriceValidator"   ErrorMessage="Valid Unit Price required" Display="Dynamic">*</asp:CustomValidator>
                    </FooterTemplate>
                </asp:TemplateField>
               <%-- <asp:TemplateField HeaderText="Price List Price" HeaderStyle-HorizontalAlign="Left" >
                    <EditItemTemplate>
                        <asp:TextBox ID="LastPurchasePrice" runat="server" Text='<%# Bind("LastPurchasePrice") %>' MaxLength="20"  CssClass="textDecimalEntry"  ReadOnly="true"  ></asp:TextBox>                        
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="lblLastPurchasePrice" runat="server" Text='<%# Bind("LastPurchasePrice") %>'  ></asp:Label> 
                    </ItemTemplate>
                    <FooterTemplate>
                        <asp:TextBox ID="LastPurchasePrice" runat="server" Text='<%# Bind("LastPurchasePrice") %>' MaxLength="20" CssClass="textDecimalEntry" ReadOnly="true"></asp:TextBox>                        
                    </FooterTemplate>
                </asp:TemplateField>--%>
                <asp:TemplateField HeaderText="Discount Percent" HeaderStyle-HorizontalAlign="Left" >
                    <EditItemTemplate>
                        <asp:TextBox ID="LineLevelDiscount" runat="server" Text='<%# Bind("LineLevelDiscount") %>' MaxLength="20" CssClass="textDecimalEntry" AutoPostBack="false" OnTextChanged="DiscPrsnt_TextChanged"></asp:TextBox>
                        <asp:CustomValidator  id="ValidDiscPrsntRequired"  runat="server"  ValidationGroup="ItemsUpdateValidation" OnServerValidate="DiscPrsntValidator"   ErrorMessage="Valid line discount percent required" Display="Dynamic">*</asp:CustomValidator>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="lblLineLevelDiscount" runat="server" Text='<%# Bind("LineLevelDiscount") %>'  ></asp:Label> 
                    </ItemTemplate>
                    <FooterTemplate>
                        <asp:TextBox ID="LineLevelDiscount" runat="server" Text='<%# Bind("LineLevelDiscount") %>' MaxLength="20" CssClass="textDecimalEntry"  AutoPostBack="false" OnTextChanged="DiscPrsnt_TextChanged"></asp:TextBox>
                        <asp:CustomValidator  id="ValidDiscPrsntRequired"  runat="server"  ValidationGroup="ItemsAddValidation" OnServerValidate="DiscPrsntValidator"   ErrorMessage="Valid line discount percent required" Display="Dynamic">*</asp:CustomValidator>
                    </FooterTemplate>
                </asp:TemplateField>
                 <asp:TemplateField HeaderText="Line Total" HeaderStyle-HorizontalAlign="Left" >
                    <EditItemTemplate>
                        <asp:TextBox ID="LineTotal" runat="server" Text='<%# Bind("LineTotal") %>' MaxLength="20" CssClass="textDecimalEntry" ReadOnly="true"></asp:TextBox>
                        <asp:CustomValidator  id="ValidLineTotalRequired"  runat="server"  ValidationGroup="ItemsUpdateValidation" OnServerValidate="LineTotalValidator"   ErrorMessage="Valid Line Total required" Display="Dynamic">*</asp:CustomValidator>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="lblLineTotal" runat="server" Text='<%# Bind("LineTotal") %>'  ></asp:Label> 
                    </ItemTemplate>
                    <FooterTemplate>
                        <asp:TextBox ID="LineTotal" runat="server" Text='<%# Bind("LineTotal") %>' MaxLength="20" CssClass="textDecimalEntry" ReadOnly="true"></asp:TextBox>
                        <asp:CustomValidator  id="ValidLineTotalRequired"  runat="server"  ValidationGroup="ItemsAddValidation" OnServerValidate="LineTotalValidator"   ErrorMessage="Valid Line Total required" Display="Dynamic">*</asp:CustomValidator>
                    </FooterTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Action" >
                    <ItemTemplate>
                        <asp:LinkButton ID="lnkRemove"  runat="server" CommandName="Delete" CommandArgument = '<%# Eval("LineNum")%>'  OnClientClick = "return confirm('Are your sure, want to delete?')" Text = "Delete" ></asp:LinkButton>
                        
                    </ItemTemplate>
                    <FooterTemplate>
                        <%--<asp:Button ID="btnAdd" runat="server" Text="Insert" CommandName="Insert" CausesValidation="true" ValidationGroup="ItemsAddValidation"  OnClientClick="if ( ! QtyConfirmation()) return false; " />--%>
                        <asp:Button ID="btnAdd" runat="server" Text="Insert" CommandName="Insert" CausesValidation="true" ValidationGroup="ItemsAddValidation"  />                     
                    </FooterTemplate>
                </asp:TemplateField>
                <asp:CommandField ShowEditButton="true" ValidationGroup="ItemsUpdateValidation" />
            </Columns>
            </asp:GridView>
        </center>
    </div>
    <div class="FormHeader"> 
        <div class="twocolleft" > 
            <div class="Table">
                <div class="Row">
                    <div class="Cell" style="vertical-align:top"><asp:Label ID="Label2" Text="Remarks:" runat="server" AssociatedControlID="Remarks"></asp:Label></div>
                    <div class="Cell"><asp:TextBox ID="Remarks" runat="server" Columns="35" Rows="5" TextMode="MultiLine" MaxLength="150" MaxSize="150" CssClass="FixedWithMultilineTexBox, NoResize" ></asp:TextBox></div>
                </div>
            </div>
        </div>
        <div class="twocolright">
            <div class="Table">                
                <div class="Row">
                    <div class="Cell"><asp:Label ID="lblForSubTotalAmt" runat="server" Text="Sub Total Amount:" AssociatedControlID="SubTotalAmt"></asp:Label></div>
                    <div class="Cell"><asp:TextBox ID="SubTotalAmt" runat="server" MaxLength="20" 
                            CssClass="textHeaderDecimalEntry" ReadOnly="True"></asp:TextBox>
                    <asp:CustomValidator  id="ValidSubTotalAmtRequired"  runat="server"  ValidationGroup="HeaderValidation" OnServerValidate="SubTotalAmtValidator"   ErrorMessage="Valid Sub Total Amount required" Display="Dynamic">*</asp:CustomValidator>
                    </div>

                </div>
                <div class="Row">
                    <div class="Cell"><asp:Label ID="lblForOrderDiscPrsnt" runat="server" Text="Order Discount Percent:" AssociatedControlID="OrderDiscPrsnt"></asp:Label></div>
                    <div class="Cell"><asp:TextBox ID="OrderDiscPrsnt" runat="server" MaxLength="20" AutoPostBack="true" CssClass="textPercentEntry"></asp:TextBox>%
                    <asp:CustomValidator  id="ValidOrderDiscPrsntRequired"  runat="server"  ValidationGroup="HeaderValidation" OnServerValidate="OrderDiscPrsntValidator"   ErrorMessage="Valid Order discount percent required" Display="Dynamic">*</asp:CustomValidator>
                    </div>
                </div>
                <div class="Row">
                    <div class="Cell"><asp:Label ID="lblForOrderDiscAmt" runat="server" Text="Order Discount Amount:" AssociatedControlID="OrderDiscAmt"></asp:Label></div>
                    <div class="Cell"><asp:TextBox ID="OrderDiscAmt" runat="server" MaxLength="20" 
                            CssClass="textHeaderDecimalEntry" ReadOnly="True"></asp:TextBox>
                    <asp:CustomValidator  id="ValidOrderDiscAmtRequired"  runat="server"  ValidationGroup="HeaderValidation" OnServerValidate="OrderDiscAmtValidator"   ErrorMessage="Valid Order discount amount required" Display="Dynamic">*</asp:CustomValidator>
                    </div>
                </div>
                <div class="Row">
                    <div class="Cell"><asp:Label ID="lblForAmtAfterDiscount" runat="server" Text="Sub Total Amount After Discount:" AssociatedControlID="AmtAfterDiscount"></asp:Label></div>
                    <div class="Cell"><asp:TextBox ID="AmtAfterDiscount" runat="server" MaxLength="20" 
                            CssClass="textHeaderDecimalEntry" ReadOnly="True"></asp:TextBox>
                    <asp:CustomValidator  id="ValidAmtAfterDiscountRequired"  runat="server"  ValidationGroup="HeaderValidation" OnServerValidate="AmtAfterDiscountValidator"   ErrorMessage="Valid Sub total amount after discount required" Display="Dynamic">*</asp:CustomValidator>
                    </div>
                </div>
                <div class="Row">
                    <div class="Cell"><asp:Label ID="lblForGSTPrsnt" runat="server" Text="GST Percent:" AssociatedControlID="GSTPrsnt"></asp:Label></div>
                    <div class="Cell">
                        <asp:TextBox ID="GSTPrsnt" runat="server" MaxLength="20" 
                            AutoPostBack="true" CssClass="textQtyEntry" ReadOnly="True"></asp:TextBox>%
                    <asp:CustomValidator  id="ValidGSTPrsntRequired"  runat="server"  ValidationGroup="HeaderValidation" OnServerValidate="GSTPrsntValidator"   ErrorMessage="Valid GST percent required" Display="Dynamic">*</asp:CustomValidator></div>
                </div>
                <div class="Row">
                    <div class="Cell"><asp:Label ID="lblForGSTAmount" runat="server" Text="GST Amount:" AssociatedControlID="GSTAmount"></asp:Label></div>
                    <div class="Cell"><asp:TextBox ID="GSTAmount" runat="server" MaxLength="20" 
                            CssClass="textHeaderDecimalEntry" ReadOnly="True"></asp:TextBox>
                    <asp:CustomValidator  id="ValidGSTAmountRequired"  runat="server"  ValidationGroup="HeaderValidation" OnServerValidate="GSTAmountValidator"   ErrorMessage="Valid GST amount required" Display="Dynamic">*</asp:CustomValidator>
                    </div>
                </div>                
                <div class="Row">
                    <div class="Cell"><asp:Label ID="lblForTotalInvAmount" runat="server" Text="Total invoice amount:" AssociatedControlID="TotalSOAmount"></asp:Label></div>
                    <div class="Cell"><asp:TextBox ID="TotalSOAmount" runat="server" MaxLength="20" 
                            CssClass="textHeaderDecimalEntry" ReadOnly="True"></asp:TextBox>
                    <asp:CustomValidator  id="ValidTotalInvAmountRequired"  runat="server"  ValidationGroup="HeaderValidation" OnServerValidate="TotalInvAmountValidator"   ErrorMessage="Valid Total invoice amount required" Display="Dynamic">*</asp:CustomValidator>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <div class="FormHeader">
       <div class="Table">
            <div class="Row">
                <div class="Cell">
                    <asp:Button ID="btnNew" runat="server" Enabled="false" Text="New" CssClass="Action-Button" />
                    <asp:Button ID="btnSave" runat="server" Text="Save" Enabled="false" UseSubmitBehavior="false"
      OnClientClick="this.disabled='true';"  ValidationGroup="HeaderValidation" CssClass="Action-Button" />
                    <asp:Button ID="btnSubmitToSAP" runat="server" Text="Save & Submit to SAP" UseSubmitBehavior="false"
      OnClientClick="this.disabled='true';"  ValidationGroup="HeaderValidation"
                    Enabled="false" CssClass="Action-Button"  />
                    <asp:Button ID="btnPreview" runat="server" Text="Preview" ValidationGroup="HeaderValidation" Enabled="false" CssClass="Action-Button"  />
                    <asp:Button ID="btnCancel" runat="server" Text="Cancel" Enabled="false" 
                        CssClass="Action-Button" 
                        onclientclick="return confirm('Are you sure you want to cancel the SO, cancellation is irreversible?');"  />
                    <asp:Button ID="btnback" runat="server" Text="Back"  
                        CssClass="Action-Button" 
                         /><!-- Because of customer aske to have this button to clear the form we are adding it -->
                </div>
            </div>
        </div>
   </div>
</div>
    <div id="dialog" title="Message"><asp:Label runat="server" Text="" ID="LblShowMsg"></asp:Label></div>    
    <div id="ConfirmDialog" title="Confirm"><asp:Label runat="server" Text="24234" ID="LblConfirmMsg"></asp:Label><br /><br />
      <center>  <asp:Button ID="btnConfirm" runat="server" Text="Yes"/>
        <asp:Button ID="btnNotConfirm" runat="server" Text="No" CausesValidation="true" /></center></div>   
</asp:Content>
