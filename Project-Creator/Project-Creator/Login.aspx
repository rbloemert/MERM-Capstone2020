<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="Project_Creator.Login" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder" runat="server">
    <h1>Login</h1>
    <div class="sectionContainer">
        <asp:Label ID="lblLoginUsername" Text="Username/Email:" AssociatedControlID= "lblLoginUsername" runat="server" ></asp:Label>
        <asp:TextBox ID="txtSearchItem" CssClass="form-control" runat="server"></asp:TextBox>
        <asp:TextBox ID="TextBox1" CssClass="form-control" runat="server"></asp:TextBox>

        <hr />

         <div>
            <asp:Button ID="btnLogin" Text="Login" runat="server" CssClass="btn btn-primary" OnClick="btnLogin_Clicked"></asp:Button>
        </div>
    </div>    
    
    


</asp:Content>


