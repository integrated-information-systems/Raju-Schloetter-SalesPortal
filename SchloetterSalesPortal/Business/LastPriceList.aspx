<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="LastPriceList.aspx.vb" Inherits="SchloetterSalesPortal.LastPriceList" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="act" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
       <asp:ScriptManager ID="ScriptManager1" runat="server"
EnablePageMethods = "true">
</asp:ScriptManager>
    <h2>Last Price List</h2>
<asp:ValidationSummary ID="HeaderValidationSummary" runat="server" ValidationGroup="HeaderValidation" />
    <div class="MainContent">
        <div class="FormHeader"> 
            <div class="twocolleft25" > 
                <div class="Table">
                    <div class="Row">
                        <div class="Cell"><asp:Label ID="Label4" runat="server" Text="Customer Code" AssociatedControlID="CustomerCode"></asp:Label></div>
                        <div class="Cell"><asp:TextBox runat="server" ID="CustomerCode" CssClass="textEntry" MaxLength="50" AutoPostBack="true"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="CustomerCoderRequired" runat="server" ControlToValidate="CustomerCode" ErrorMessage="Customer code required" ValidationGroup="HeaderValidation" >*</asp:RequiredFieldValidator> 
                            <act:AutoCompleteExtender ServiceMethod="GetCustomerCodes"
                            MinimumPrefixLength="2"
                            CompletionInterval="100" EnableCaching="false" CompletionSetCount="10"  
                            TargetControlID="CustomerCode" CompletionListElementID="CardCodeAutoCompleteContainer" 
                            ID="AutoCompleteExtenderCustomerCode" runat="server" FirstRowSelected = "false">
                            </act:AutoCompleteExtender>         
                            <div id="CardCodeAutoCompleteContainer"></div></div>
                        </div>
                    <div class="Row">
                        <div class="Cell"><asp:Label ID="Label5"  runat="server" Text="Customer Name" AssociatedControlID="CustomerName"></asp:Label></div>
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
                </div>
            </div>
            <div class="twocolright70">
                <div class="Table">
                    <div class="Row">
                        <div class="Cell"><asp:Label ID="Label1" runat="server" Text="From Item Code" AssociatedControlID="ItemCode"></asp:Label></div>
                        <div class="Cell"><asp:TextBox runat="server" ID="ItemCode" CssClass="textEntry" MaxLength="50" AutoPostBack="true"></asp:TextBox>
                            <act:AutoCompleteExtender ServiceMethod="GetItemCodes"
                            MinimumPrefixLength="2"
                            CompletionInterval="100" EnableCaching="false" CompletionSetCount="10"  
                            TargetControlID="ItemCode" CompletionListElementID="ItemCodeAutoCompleteContainer" 
                            ID="AutoCompleteExtenderItemCode" runat="server" FirstRowSelected = "false">
                            </act:AutoCompleteExtender>         
                            <div id="ItemCodeAutoCompleteContainer"></div></div>
                        </div>
                    <div class="Row">
                        <div class="Cell"><asp:Label ID="Label2"  runat="server" Text="From Item Name" AssociatedControlID="ItemName"></asp:Label></div>
                        <div class="Cell"><asp:TextBox ID="ItemName" runat="server" CssClass="textEntry" AutoPostBack="true" MaxLength="100"></asp:TextBox>
                            <act:AutoCompleteExtender ServiceMethod="GetItemNames"
                            MinimumPrefixLength="2"
                            CompletionInterval="100" EnableCaching="false" CompletionSetCount="10"   CompletionListElementID="ItemNameAutoCompleteContainer"
                            TargetControlID="ItemName"
                            ID="AutoCompleteExtenderItemName" runat="server" FirstRowSelected = "false">
                            </act:AutoCompleteExtender>
                            <div id="ItemNameAutoCompleteContainer"></div>            
                        </div>
                    </div>    
                    <div class="Row">
                        <div class="Cell"><asp:Label ID="Label3" runat="server" Text="To Item Code" AssociatedControlID="ItemCode"></asp:Label></div>
                        <div class="Cell"><asp:TextBox runat="server" ID="ToItemCode" CssClass="textEntry" MaxLength="50" AutoPostBack="true"></asp:TextBox>
                            <act:AutoCompleteExtender ServiceMethod="GetItemCodes"
                            MinimumPrefixLength="2"
                            CompletionInterval="100" EnableCaching="false" CompletionSetCount="10"  
                            TargetControlID="ToItemCode" CompletionListElementID="ToItemCodeAutoCompleteContainer" 
                            ID="AutoCompleteExtenderToItemCode" runat="server" FirstRowSelected = "false">
                            </act:AutoCompleteExtender>         
                            <div id="ToItemCodeAutoCompleteContainer"></div></div>
                        </div>
                    <div class="Row">
                        <div class="Cell"><asp:Label ID="Label6"  runat="server" Text="To Item Name" AssociatedControlID="ItemName"></asp:Label></div>
                        <div class="Cell"><asp:TextBox ID="ToItemName" runat="server" CssClass="textEntry" AutoPostBack="true" MaxLength="100"></asp:TextBox>
                            <act:AutoCompleteExtender ServiceMethod="GetItemNames"
                            MinimumPrefixLength="2"
                            CompletionInterval="100" EnableCaching="false" CompletionSetCount="10"   CompletionListElementID="ToItemNameAutoCompleteContainer"
                            TargetControlID="ToItemName"
                            ID="AutoCompleteExtenderToItemName" runat="server" FirstRowSelected = "false">
                            </act:AutoCompleteExtender>
                            <div id="ToItemNameAutoCompleteContainer"></div>            
                        </div>
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
         <asp:GridView ID="LastPriceListGrid"
             AllowSorting="False"
             AllowPaging="True" 
             runat="server"
             CellPadding="4"
             ForeColor="#333333"
             AutoGenerateColumns="False"
             ShowHeaderWhenEmpty="True"
             GridLines="None"
             EmptyDataText="No Record found"
             CssClass="ItemGrid"     
              PageSize="100"
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
                   <%-- <asp:BoundField DataField="DocDate" HeaderText="Doc Date" ReadOnly="True" DataFormatString="{0:dd/MM/yyyy}"  />
                    <asp:BoundField DataField="ItemCode" HeaderText="Item Code"  ReadOnly="True" SortExpression="IdKey" />       
                    <asp:BoundField DataField="CardCode" HeaderText="Customer Code"  ReadOnly="True" SortExpression="SAPSQNo" />                    
                    <asp:BoundField DataField="DocNum" HeaderText="Doc Num" ReadOnly="True" />
                    <asp:BoundField DataField="Type" HeaderText="Type" ReadOnly="True" />                        
                    <asp:BoundField DataField="DiscPrcnt" HeaderText="Discount Percent" ReadOnly="True" DataFormatString="{0:0.00}" />
                    <asp:BoundField DataField="Price After Discount" HeaderText="Price After Discount" ReadOnly="True" DataFormatString="{0:0.00}"  />   --%>
                     
                     
                    <asp:BoundField DataField="ItemCode" HeaderText="Item Code"  ReadOnly="True" SortExpression="IdKey" HeaderStyle-HorizontalAlign="Justify" ItemStyle-HorizontalAlign="Justify" />  
                                      
                    <asp:BoundField DataField="ItemName" HeaderText="Item Name"  ReadOnly="True" SortExpression="SAPSQNo" HeaderStyle-HorizontalAlign="Justify" ItemStyle-HorizontalAlign="Justify"/>                    
                   
                    <asp:BoundField DataField="Type" HeaderText="Type" ReadOnly="True" HeaderStyle-HorizontalAlign="Justify" ItemStyle-HorizontalAlign="Justify" />                        
                    <asp:BoundField DataField="DiscPrcnt1" HeaderText="Discount Percent 1" ReadOnly="True" DataFormatString="{0:0.00}" HeaderStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right" />
                    <asp:BoundField DataField="PriceAfterDiscount1" HeaderText="Price After Discount 1" ReadOnly="True" DataFormatString="{0:0.00}" HeaderStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right" />   
                    <asp:BoundField DataField="Uom1" HeaderText="UOM1" ReadOnly="True" ItemStyle-HorizontalAlign="Justify" />        
                    <asp:BoundField DataField="DiscPrcnt2" HeaderText="Discount Percent 2" ReadOnly="True" DataFormatString="{0:0.00}" HeaderStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right" />
                    <asp:BoundField DataField="PriceAfterDiscount2" HeaderText="Price After Discount 2 " ReadOnly="True" DataFormatString="{0:0.00}" HeaderStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right"  />   
                    <asp:BoundField DataField="Uom2" HeaderText="UOM2" ReadOnly="True" ItemStyle-HorizontalAlign="Justify" />     
                    <asp:BoundField DataField="DiscPrcnt3" HeaderText="Discount Percent 3" ReadOnly="True" DataFormatString="{0:0.00}" HeaderStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right" />
                    <asp:BoundField DataField="PriceAfterDiscount3" HeaderText="Price After Discount 3" ReadOnly="True" DataFormatString="{0:0.00}" HeaderStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right"  />   
                    <asp:BoundField DataField="Uom3" HeaderText="UOM3" ReadOnly="True" ItemStyle-HorizontalAlign="Justify" />     
                </Columns>
                </asp:GridView>        
                 <% If LastPriceListGrid.Rows(0).Cells.Count > 1 Then%>
                <i>You are viewing page
                <%=LastPriceListGrid.PageIndex + 1%>
                of
                <%=LastPriceListGrid.PageCount%>
                </i>
                <% End If%>
                </center>
</div>
    </div>
</asp:Content>

