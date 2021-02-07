<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="Project_Creator.Login" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>




<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder" runat="server">
    <div >
        <h1>Login</h1>
        <div>
            <asp:Label ID="lblLoginUsername" Text="Username/Email:" AssociatedControlID= "lblLoginUsername" runat="server" ></asp:Label>
            <asp:TextBox ID="txtLoginUsername" CssClass="form-control" runat="server" ></asp:TextBox>
        </div>
        <div>
            <asp:Label ID="lblLoginPassword" Text="Password:" AssociatedControlID= "lblLoginPassword" runat="server" ></asp:Label>
            <asp:TextBox ID="txtLoginPassword" CssClass="form-control" runat="server" ></asp:TextBox>
        </div>
        <hr />
        <div>
            <asp:Button ID="btnLogin" Text="Login" runat="server" CssClass="btn btn-primary" OnClick="btnLogin_Clicked" ></asp:Button>
            <asp:Button ID="btnClear" Text="Clear" runat="server" CssClass="btn btn-primary" OnClick="btnClear_Clicked" ></asp:Button>
        </div>
        <div>
            <asp:Label ID="lblLoginFeedback" AssociatedControlID= "lblLoginFeedback" runat="server" style="color:red" ></asp:Label>
        </div>
    </div>    
    
    


</asp:Content>


