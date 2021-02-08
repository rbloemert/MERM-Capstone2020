<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Signup.aspx.cs" Inherits="Project_Creator.Signup" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder" runat="server">

    Username<asp:TextBox ID="TextBoxUsername" runat="server"></asp:TextBox>
    <br />
    Password<asp:TextBox ID="TextBoxPassword" TextMode="Password" runat="server"></asp:TextBox>
    <br />
    Confirm<asp:TextBox ID="TextBoxPasswordConfirm" runat="server"></asp:TextBox>
    <br />
    First Name<asp:TextBox ID="TextBoxFirstName" runat="server"></asp:TextBox>
    <br />
    Last Name<asp:TextBox ID="TextBoxLastName" runat="server"></asp:TextBox>
    <br />
    Email<asp:TextBox ID="TextBoxEmail" runat="server"></asp:TextBox>
    <br />
    <asp:Button runat="server" Text="Submit" OnClick="Register" />

</asp:Content>
