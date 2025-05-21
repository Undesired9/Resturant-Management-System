<%@ Page Title="Login" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeFile="Login.aspx.cs" Inherits="RestaurantManagement.Login" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <div class="login-container">
        <h2>Login</h2>
        <div class="form-group">
            <asp:Label ID="lblUsername" runat="server" Text="Username:" AssociatedControlID="txtUsername"></asp:Label>
            <asp:TextBox ID="txtUsername" runat="server" CssClass="form-control" placeholder="Enter username"></asp:TextBox>
            <asp:RequiredFieldValidator ID="rfvUsername" runat="server" 
                ControlToValidate="txtUsername" 
                ErrorMessage="Username is required" 
                Display="Dynamic" 
                CssClass="error-message">
            </asp:RequiredFieldValidator>
        </div>
        <div class="form-group">
            <asp:Label ID="lblPassword" runat="server" Text="Password:" AssociatedControlID="txtPassword"></asp:Label>
            <asp:TextBox ID="txtPassword" runat="server" TextMode="Password" CssClass="form-control" placeholder="Enter password"></asp:TextBox>
            <asp:RequiredFieldValidator ID="rfvPassword" runat="server" 
                ControlToValidate="txtPassword" 
                ErrorMessage="Password is required" 
                Display="Dynamic" 
                CssClass="error-message">
            </asp:RequiredFieldValidator>
        </div>
        <div class="form-group">
            <asp:Button ID="btnLogin" runat="server" Text="Login" OnClick="btnLogin_Click" CssClass="btn btn-primary" />
            <asp:Button ID="btnRegister" runat="server" Text="Register" OnClick="btnRegister_Click" CssClass="btn" />
        </div>
        <div>
            <asp:Label ID="lblMessage" runat="server" CssClass="error-message"></asp:Label>
        </div>
    </div>
</asp:Content> 