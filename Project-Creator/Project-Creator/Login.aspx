<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="Project_Creator.Login" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder" runat="server">
    <div class="Basic">
        <div>
            <h1 style="padding-top:4px;padding-bottom:4px;">Login!</h1>
        </div>
        <table style="margin-left:auto; margin-right:auto;">
            <tr>
                <td>
                    <asp:Label runat="server">Username</asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="TextBoxUsername" runat="server" placeholder="Username..."></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td></td>
                <td>
                    <div class="Error">
                        <asp:RequiredFieldValidator ControlToValidate="TextBoxUsername"  Display="Dynamic" runat="server" ErrorMessage="Username is required." ForeColor="Red"></asp:RequiredFieldValidator>
                        <asp:CustomValidator ControlToValidate="TextBoxUsername" OnServerValidate="ValidateUsername" Display="Dynamic" runat="server" ErrorMessage="Username already in use." ForeColor="Red"></asp:CustomValidator>
                    </div>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label runat="server">Password</asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="TextBoxPassword" TextMode="Password" runat="server" placeholder="Password..."></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td></td>
                <td>
                    <div class="Error">
                        <asp:RequiredFieldValidator ControlToValidate="TextBoxPassword"  Display="Dynamic" runat="server" ErrorMessage="Password is required." ForeColor="Red"></asp:RequiredFieldValidator>
                    </div>
                </td>
            </tr>
        </table>
        <asp:Button runat="server" Text="Login" OnClick="Access" />
    </div>
</asp:Content>


