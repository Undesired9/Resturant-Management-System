<%@ Page Title="Staff" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeFile="Staff.aspx.cs" Inherits="RestaurantManagement.Staff" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <h2>Staff</h2>
    <div>
        <asp:GridView ID="gvStaff" runat="server" AutoGenerateColumns="False" DataKeyNames="ID" OnRowCommand="gvStaff_RowCommand">
            <Columns>
                <asp:BoundField DataField="ID" HeaderText="ID" />
                <asp:BoundField DataField="Name" HeaderText="Name" />
                <asp:BoundField DataField="Role" HeaderText="Role" />
                <asp:TemplateField HeaderText="Actions">
                    <ItemTemplate>
                        <asp:LinkButton ID="lnkEdit" runat="server" CommandName="EditStaff" CommandArgument='<%# Eval("ID") %>'>Edit</asp:LinkButton>
                        <asp:LinkButton ID="lnkDelete" runat="server" CommandName="DeleteStaff" CommandArgument='<%# Eval("ID") %>' OnClientClick="return confirm('Are you sure you want to delete this staff member?');">Delete</asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
    </div>
    <div>
        <h3>Add/Edit Staff</h3>
        <asp:TextBox ID="txtName" runat="server" placeholder="Name"></asp:TextBox>
        <asp:TextBox ID="txtRole" runat="server" placeholder="Role"></asp:TextBox>
        <asp:Button ID="btnSave" runat="server" Text="Save" OnClick="btnSave_Click" />
    </div>
</asp:Content> 