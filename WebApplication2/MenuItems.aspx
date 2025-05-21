<%@ Page Title="Menu Items" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeFile="MenuItems.aspx.cs" Inherits="RestaurantManagement.MenuItems" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <div class="card">
        <h2>Menu Items</h2>
        <asp:GridView ID="gvMenuItems" runat="server" AutoGenerateColumns="False" CssClass="grid-view" OnRowCommand="gvMenuItems_RowCommand">
            <Columns>
                <asp:BoundField DataField="ID" HeaderText="ID" />
                <asp:BoundField DataField="Name" HeaderText="Name" />
                <asp:BoundField DataField="Price" HeaderText="Price" DataFormatString="{0:C}" />
                <asp:TemplateField HeaderText="Actions">
                    <ItemTemplate>
                        <asp:LinkButton ID="lnkEdit" runat="server" CssClass="btn btn-primary" CommandName="EditRow" CommandArgument='<%# Eval("ID") %>'>Edit</asp:LinkButton>
                        <asp:LinkButton ID="lnkDelete" runat="server" CssClass="btn btn-danger" CommandName="DeleteRow" CommandArgument='<%# Eval("ID") %>' OnClientClick="return confirm('Are you sure you want to delete this item?');">Delete</asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
    </div>
    <div class="card">
        <h3><asp:Label ID="lblFormTitle" runat="server" Text="Add/Edit Menu Item"></asp:Label></h3>
        <div class="form-group">
            <label for="txtName">Name</label>
            <asp:TextBox ID="txtName" runat="server" CssClass="form-control" />
        </div>
        <div class="form-group">
            <label for="txtPrice">Price</label>
            <asp:TextBox ID="txtPrice" runat="server" CssClass="form-control" />
        </div>
        <asp:Button ID="btnSave" runat="server" Text="Save" CssClass="btn btn-primary" OnClick="btnSave_Click" />
        <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="btn btn-danger" OnClick="btnCancel_Click" />
    </div>
    <div id="toast" class="toast" runat="server"></div>
</asp:Content> 