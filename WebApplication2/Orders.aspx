<%@ Page Title="Orders" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeFile="Orders.aspx.cs" Inherits="RestaurantManagement.Orders" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <div class="card">
        <h2>Orders</h2>
        <asp:GridView ID="gvOrders" runat="server" AutoGenerateColumns="False" CssClass="grid-view" DataKeyNames="ID" OnRowCommand="gvOrders_RowCommand">
            <Columns>
                <asp:BoundField DataField="ID" HeaderText="ID" />
                <asp:BoundField DataField="OrderDate" HeaderText="Order Date" />
                <asp:BoundField DataField="TotalAmount" HeaderText="Total Amount" />
                <asp:TemplateField HeaderText="Actions">
                    <ItemTemplate>
                        <asp:LinkButton ID="lnkEdit" runat="server" CssClass="btn btn-primary" CommandName="EditOrder" CommandArgument='<%# Eval("ID") %>'>Edit</asp:LinkButton>
                        <asp:LinkButton ID="lnkDelete" runat="server" CssClass="btn btn-danger" CommandName="DeleteOrder" CommandArgument='<%# Eval("ID") %>' OnClientClick="return confirm('Are you sure you want to delete this order?');">Delete</asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
    </div>
    <div class="card">
        <h3>Add/Edit Order</h3>
        <div class="form-group">
            <label for="txtOrderDate">Order Date</label>
            <asp:TextBox ID="txtOrderDate" runat="server" CssClass="form-control" placeholder="Order Date" TextMode="Date"></asp:TextBox>
        </div>
        <div class="form-group">
            <label for="txtTotalAmount">Total Amount</label>
            <asp:TextBox ID="txtTotalAmount" runat="server" CssClass="form-control" placeholder="Total Amount"></asp:TextBox>
        </div>
        <asp:Button ID="btnSave" runat="server" Text="Save" CssClass="btn btn-primary" OnClick="btnSave_Click" />
    </div>
    <div id="toast" class="toast" runat="server"></div>
    <div class="card" style="margin-top:2rem;">
        <h3>Orders Per Day</h3>
        <canvas id="ordersChart" style="width:100%;max-width:600px;"></canvas>
    </div>
    <script src="https://cdn.jsdelivr.net/npm/chart.js"></script>
    <script type="text/javascript">
        $(function() {
            $("#<%= txtOrderDate.ClientID %>").datepicker({
                dateFormat: 'mm/dd/yy',
                changeMonth: true,
                changeYear: true
            });
        });
        // Chart data will be injected from code-behind
    </script>
</asp:Content>