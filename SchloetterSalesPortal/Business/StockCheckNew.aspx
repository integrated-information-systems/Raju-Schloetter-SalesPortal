<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="StockCheckNew.aspx.vb" Inherits="SchloetterSalesPortal.StockCheckNew" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="act" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"
EnablePageMethods = "true">
</asp:ScriptManager>
    <h2>Stock Check</h2>
<asp:ValidationSummary ID="HeaderValidationSummary" runat="server" ValidationGroup="HeaderValidation" />
    <div class="MainContent">
        <div class="FormHeader"> 
            <div class="twocolleft50" > 
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
            <div class="twocolright70">
                <div class="Table">
                  
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
         <asp:GridView ID="ItemInStockList"
             AllowSorting="False"
             AllowPaging="True" 
             runat="server"
             CellPadding="4"
             ForeColor="#333333"
             AutoGenerateColumns="True"
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
                </asp:GridView>                
                </center>
</div>
    </div>
</asp:Content>