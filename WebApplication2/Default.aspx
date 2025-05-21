<%@ Page Title="Dashboard" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="RestaurantManagement.Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <div class="dashboard-cards">
        <div class="dashboard-card">
            <h3>Today's Orders</h3>
            <div class="stats">
                <div class="stat-item">
                    <div class="stat-value"><asp:Label ID="lblTotalOrders" runat="server">0</asp:Label></div>
                    <div class="stat-label">Total Orders</div>
                </div>
                <div class="stat-item">
                    <div class="stat-value">$<asp:Label ID="lblTotalRevenue" runat="server">0</asp:Label></div>
                    <div class="stat-label">Revenue</div>
                </div>
            </div>
        </div>

        <div class="dashboard-card">
            <h3>Menu Items</h3>
            <div class="stats">
                <div class="stat-item">
                    <div class="stat-value"><asp:Label ID="lblTotalItems" runat="server">0</asp:Label></div>
                    <div class="stat-label">Total Items</div>
                </div>
                <div class="stat-item">
                    <div class="stat-value"><asp:Label ID="lblPopularItem" runat="server">-</asp:Label></div>
                    <div class="stat-label">Most Popular</div>
                </div>
            </div>
        </div>

        <div class="dashboard-card">
            <h3>Tables</h3>
            <div class="stats">
                <div class="stat-item">
                    <div class="stat-value"><asp:Label ID="lblAvailableTables" runat="server">0</asp:Label></div>
                    <div class="stat-label">Available</div>
                </div>
                <div class="stat-item">
                    <div class="stat-value"><asp:Label ID="lblOccupiedTables" runat="server">0</asp:Label></div>
                    <div class="stat-label">Occupied</div>
                </div>
            </div>
        </div>
    </div>

    <div class="card">
        <h3>Recent Orders</h3>
        <div class="search-bar">
            <asp:TextBox ID="txtSearch" runat="server" CssClass="form-control" placeholder="Search orders..."></asp:TextBox>
            <asp:DropDownList ID="ddlFilter" runat="server" CssClass="filter-dropdown">
                <asp:ListItem Text="All Orders" Value="all" />
                <asp:ListItem Text="Today" Value="today" />
                <asp:ListItem Text="This Week" Value="week" />
                <asp:ListItem Text="This Month" Value="month" />
            </asp:DropDownList>
        </div>
        <asp:GridView ID="gvRecentOrders" runat="server" AutoGenerateColumns="False" CssClass="grid-view">
            <Columns>
                <asp:BoundField DataField="ID" HeaderText="Order ID" />
                <asp:BoundField DataField="OrderDate" HeaderText="Date" DataFormatString="{0:MM/dd/yyyy HH:mm}" />
                <asp:BoundField DataField="TotalAmount" HeaderText="Amount" DataFormatString="{0:C}" />
                <asp:BoundField DataField="Status" HeaderText="Status" />
                <asp:TemplateField HeaderText="Actions">
                    <ItemTemplate>
                        <asp:LinkButton ID="lnkView" runat="server" CssClass="btn btn-primary" CommandName="ViewOrder" CommandArgument='<%# Eval("ID") %>'>View</asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
    </div>

    <div class="card">
        <h3>Quick Actions</h3>
        <div class="quick-actions">
            <asp:Button ID="btnNewOrder" runat="server" Text="New Order" CssClass="btn btn-primary" OnClick="btnNewOrder_Click" />
            <asp:Button ID="btnAddMenuItem" runat="server" Text="Add Menu Item" CssClass="btn btn-primary" OnClick="btnAddMenuItem_Click" />
            <asp:Button ID="btnManageTables" runat="server" Text="Manage Tables" CssClass="btn btn-primary" OnClick="btnManageTables_Click" />
            <asp:Button ID="btnViewReports" runat="server" Text="View Reports" CssClass="btn btn-primary" OnClick="btnViewReports_Click" />
        </div>
    </div>
</asp:Content> 