<%@ Page Title="Tables" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeFile="Tables.aspx.cs" Inherits="RestaurantManagement.Tables" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <h2>Tables</h2>
    <div>
        <asp:GridView ID="gvTables" runat="server" AutoGenerateColumns="False" DataKeyNames="ID" OnRowCommand="gvTables_RowCommand">
            <Columns>
                <asp:BoundField DataField="ID" HeaderText="ID" />
                <asp:BoundField DataField="TableNumber" HeaderText="Table Number" />
                <asp:BoundField DataField="Capacity" HeaderText="Capacity" />
                <asp:TemplateField HeaderText="Actions">
                    <ItemTemplate>
                        <asp:LinkButton ID="lnkEdit" runat="server" CommandName="EditTable" CommandArgument='<%# Eval("ID") %>'>Edit</asp:LinkButton>
                        <asp:LinkButton ID="lnkDelete" runat="server" CommandName="DeleteTable" CommandArgument='<%# Eval("ID") %>' OnClientClick="return confirm('Are you sure you want to delete this table?');">Delete</asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
    </div>
    <div>
        <h3>Add/Edit Table</h3>
        <asp:TextBox ID="txtTableNumber" runat="server" placeholder="Table Number"></asp:TextBox>
        <asp:TextBox ID="txtCapacity" runat="server" placeholder="Capacity"></asp:TextBox>
        <asp:Button ID="btnSave" runat="server" Text="Save" OnClick="btnSave_Click" />
    </div>
</asp:Content> 