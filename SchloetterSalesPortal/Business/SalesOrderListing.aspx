<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="SalesOrderListing.aspx.vb" Inherits="SchloetterSalesPortal.SalesOrderListing" MaintainScrollPositionOnPostback="true"  %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="act" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
 <asp:ScriptManager ID="ScriptManager1" runat="server"
EnablePageMethods = "true">
</asp:ScriptManager>
<h2>Sales Order Listing</h2>
<asp:ValidationSummary ID="HeaderValidationSummary" runat="server" ValidationGroup="HeaderValidation" />
<div class="MainContent">
    <div class="FormHeader"> 
            <div class="twocolleft25" > 
                <div class="Table">
                    <div class="Row">
                        <div class="Cell"><asp:Label ID="Label1" runat="server" Text="Web SO No" AssociatedControlID="txtFindDocNo"></asp:Label></div>
                        <div class="Cell"><asp:TextBox ID="txtFindDocNo" runat="server" MaxLength="50" ValidationGroup="HeaderValidation"></asp:TextBox>
                        <asp:CustomValidator  id="FindDocNoValidator" runat="server"  ValidationGroup="HeaderValidation" OnServerValidate="ValidateFindDocNo"   ErrorMessage="Document no is not valid" Display="Dynamic">*
                            </asp:CustomValidator>            
                            <act:AutoCompleteExtender ServiceMethod="GetDocNos"
                            MinimumPrefixLength="1"
                            CompletionInterval="100" EnableCaching="false" CompletionSetCount="10"  
                            TargetControlID="txtFindDocNo" CompletionListElementID="FindDocNoAutoCompleteContainer" 
                            ID="FindDocNoAutoCompleteExtender" runat="server" FirstRowSelected = "false">
                            </act:AutoCompleteExtender>         
                            <div id="FindDocNoAutoCompleteContainer"></div>
                        </div>
                    </div>
                    <div class="Row">
                        <div class="Cell"><asp:Label ID="Label2"  runat="server" Text="Status" AssociatedControlID="Status"></asp:Label></div>
                        <div class="Cell">
                            <asp:DropDownList ID="Status" runat="server" AppendDataBoundItems="true">
                            <asp:ListItem Text="Select" Value="0">Select</asp:ListItem>
                            </asp:DropDownList>
                        </div>
                    </div>
                                
                </div>
            </div>
            <div class="twocolright70">
                <div class="Table">
                    <div class="Row">
                        <div class="Cell"><asp:Label ID="Label4" runat="server" Text="Customer Code" AssociatedControlID="CustomerCode"></asp:Label></div>
                        <div class="Cell"><asp:TextBox runat="server" ID="CustomerCode" CssClass="textEntry" MaxLength="50" AutoPostBack="true"></asp:TextBox>
                            <act:AutoCompleteExtender ServiceMethod="GetCustomerCodes"
                            MinimumPrefixLength="2"
                            CompletionInterval="100" EnableCaching="false" CompletionSetCount="10"  
                            TargetControlID="CustomerCode" CompletionListElementID="CardCodeAutoCompleteContainer" 
                            ID="AutoCompleteExtenderCustomerCode" runat="server" FirstRowSelected = "false">
                            </act:AutoCompleteExtender>         
                            <div id="CardCodeAutoCompleteContainer"></div></div>
                        </div>
                    <div class="Row">
                        <div class="Cell"><asp:Label ID="Label5"  runat="server" Text="Customer Name" AssociatedControlID="CustomerCode"></asp:Label></div>
                        <div class="Cell"><asp:TextBox ID="CustomerName" runat="server" CssClass="textEntry" AutoPostBack="true" MaxLength="100"></asp:TextBox>
                            <act:AutoCompleteExtender ServiceMethod="GetCustomerNames"
                            MinimumPrefixLength="2"
                            CompletionInterval="100" EnableCaching="false" CompletionSetCount="10"   CompletionListElementID="CardNameAutoCompleteContainer"
                            TargetControlID="CustomerName"
                            ID="AutoCompleteExtenderCustomerName" runat="server" FirstRowSelected = "false">
                            </act:AutoCompleteExtender>
                            <div id="CardNameAutoCompleteContainer"></div>            
                        </div>
                    </div>
                    <div class="Row">
                        <div class="Cell"><asp:Label ID="Label6"  runat="server" Text="From Date"></asp:Label></div>                
                        <div class="Cell">
                        <asp:TextBox ID="FromDate" runat="server" CssClass="textDateEntry" MaxLength="10"></asp:TextBox> <asp:CustomValidator  id="FromDateValidator" runat="server"  ValidationGroup="HeaderValidation" OnServerValidate="ValidateFromDate"   ErrorMessage="From date is not valid" Display="Dynamic">*
                        </asp:CustomValidator> 
                        <act:CalendarExtender ID="FromDateCalendarExtender" TargetControlID="FromDate" runat="server" Format="yyyy-MM-dd" />
                        &nbsp;&nbsp;<asp:Label ID="Label7"  runat="server" CssClass="textDateEntry" Text="To Date" ></asp:Label>
                        &nbsp;&nbsp;<asp:TextBox ID="ToDate" runat="server"  CssClass="textDateEntry"   MaxLength="10" ></asp:TextBox>
                        <asp:CustomValidator  id="ToDateValidator" runat="server"  ValidationGroup="HeaderValidation" OnServerValidate="ValidateToDate"   ErrorMessage="To date is not valid" Display="Dynamic">*
                        </asp:CustomValidator>
                        <act:CalendarExtender ID="CalendarExtender1" TargetControlID="ToDate" runat="server" Format="yyyy-MM-dd" /></div>
                    </div>
                </div>
            </div>
        </div>
    <div class="FormHeader">
            <div class="Table">
                <div class="Row">
                    <div class="Cell">
                        <asp:Button ID="btnSearch" runat="server" Text="Search"  CssClass="Action-Button"   ValidationGroup="HeaderValidation" />
                        <asp:Button ID="btnClear" runat="server" Text="Clear"  CssClass="Action-Button"  /></div>
                </div>
            </div>
        </div>
    <div class="GridViewItems">
        <center>
         <asp:GridView ID="SalesOrderMasterGrid"
             AllowSorting="True"
             AllowPaging="True" 
             runat="server"
             CellPadding="4"
             ForeColor="#333333"
             AutoGenerateColumns="False"
             ShowHeaderWhenEmpty="True"
             GridLines="None"
             EmptyDataText="No Record found"
             CssClass="ItemGrid"     
              PageSize="10"
             >
                <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" HorizontalAlign="Left" />
                <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                <SortedAscendingCellStyle BackColor="#E9E7E2" />
                <SortedAscendingHeaderStyle BackColor="#506C8C" />
                <SortedDescendingCellStyle BackColor="#FFFDF8" />
                <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
                <Columns>
                    <asp:BoundField DataField="DocDate" HeaderText="Doc Date" ReadOnly="True" DataFormatString="{0:yyyy/MM/dd}"  />
                    <asp:BoundField DataField="IdKey" HeaderText="Web SO No"  ReadOnly="True" SortExpression="IdKey" />       
                    <asp:BoundField DataField="SAPSONo" HeaderText="SAP SO No"  ReadOnly="True" SortExpression="SAPSONo" />                    
                    <asp:BoundField DataField="Status" HeaderText="Status" ReadOnly="True" />
                   <%-- <asp:BoundField DataField="Status" HeaderText="Converted to Invoice" ReadOnly="True" />       --%>                 
                    <asp:BoundField DataField="CustomerCode" HeaderText="Customer Code" ReadOnly="True" />
                    <asp:BoundField DataField="CustomerName" HeaderText="Customer Name" ReadOnly="True"  />   
                    <asp:BoundField DataField="AmtAfterDiscount" HeaderText="Total After Discount" ReadOnly="True"  />                                         
                    <asp:TemplateField>
                        <ItemTemplate>
                            <asp:Button ID="btnView" runat="server" CommandArgument='<%# Eval("IdKey")%>' CommandName="View" Text="View" />
                        </ItemTemplate>
                    </asp:TemplateField>                
                </Columns>
                </asp:GridView>
                <% If SalesOrderMasterGrid.Rows(0).Cells.Count > 1 Then%>
                <i>You are viewing page
                <%=SalesOrderMasterGrid.PageIndex + 1%>
                of
                <%=SalesOrderMasterGrid.PageCount%>
                </i>
                <% End If%>
                </center>
</div>
</div>
</asp:Content>
