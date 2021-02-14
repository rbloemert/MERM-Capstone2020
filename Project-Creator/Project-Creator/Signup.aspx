<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Signup.aspx.cs" Inherits="Project_Creator.Signup" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder" runat="server">
    <div class="Basic">
        <div>
            <h1>Sign Up!</h1>
        </div>
        <table style="margin-left:auto; margin-right:auto;">
            <tr>
                <td>
                    <asp:Label runat="server">Username</asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="TextBoxUsername" runat="server" placeholder="Username..."></asp:TextBox>
                    <asp:RequiredFieldValidator ControlToValidate="TextBoxUsername"  Display="Dynamic" runat="server" ErrorMessage="Username is required."></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label runat="server">Password</asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="TextBoxPassword" TextMode="Password" runat="server" placeholder="Password..."></asp:TextBox>
                    <asp:RequiredFieldValidator ControlToValidate="TextBoxPassword"  Display="Dynamic" runat="server" ErrorMessage="Password is required."></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label runat="server">Confirm</asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="TextBoxPasswordConfirm" TextMode="Password" runat="server" placeholder="Confirm Password..."></asp:TextBox>
                    <asp:CompareValidator ControlToValidate="TextBoxPassword" ControlToCompare="TextBoxPasswordConfirm"  Display="Dynamic" runat="server" ErrorMessage="Passwords do not match."></asp:CompareValidator>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label runat="server">First Name</asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="TextBoxFirstName" runat="server" placeholder="First Name... (Optional)"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label runat="server">Last Name</asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="TextBoxLastName" runat="server" placeholder="Last Name... (Optional)"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label runat="server">Email</asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="TextBoxEmail" runat="server" placeholder="Email..."></asp:TextBox>
                    <asp:RequiredFieldValidator ControlToValidate="TextBoxEmail"  Display="Dynamic" runat="server" ErrorMessage="Email is required."></asp:RequiredFieldValidator>
                    <asp:RegularExpressionValidator ControlToValidate="TextBoxEmail" Display="Dynamic" runat="server" ValidationExpression="\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"  ErrorMessage="Email is not valid."></asp:RegularExpressionValidator>
                </td>
            </tr>
        </table>
        <asp:Button runat="server" Text="Submit" OnClick="Register" />
    </div>
</asp:Content>
