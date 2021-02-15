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
                </td>
            </tr>
            <tr>
                <td></td>
                <td>
                    <div class="Error">
                        <asp:RequiredFieldValidator ControlToValidate="TextBoxUsername"  Display="Dynamic" runat="server" ErrorMessage="Username is required." ForeColor="Red"></asp:RequiredFieldValidator>
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
            <tr>
                <td>
                    <asp:Label runat="server">Confirm</asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="TextBoxPasswordConfirm" TextMode="Password" runat="server" placeholder="Confirm Password..."></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td></td>
                <td>
                    <div class="Error">
                        <asp:CompareValidator ControlToValidate="TextBoxPassword" ControlToCompare="TextBoxPasswordConfirm"  Display="Dynamic" runat="server" ErrorMessage="Passwords do not match." ForeColor="Red"></asp:CompareValidator>
                    </div>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label runat="server">Full Name</asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="TextBoxFullName" runat="server" placeholder="Full Name... (Optional)"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td></td>
                <td>
                    <div class="Error">
                        <asp:RegularExpressionValidator ControlToValidate="TextBoxFullName" Display="Dynamic" runat="server" ValidationExpression="^[A-Z][a-z]*(\s[A-Z][a-z]*)+$"  ErrorMessage="Full name is not valid." ForeColor="Red"></asp:RegularExpressionValidator>
                    </div>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label runat="server">Email</asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="TextBoxEmail" runat="server" placeholder="Email..."></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td></td>
                <td>
                    <div class="Error">
                        <asp:RequiredFieldValidator ControlToValidate="TextBoxEmail"  Display="Dynamic" runat="server" ErrorMessage="Email is required." ForeColor="Red"></asp:RequiredFieldValidator>
                        <asp:RegularExpressionValidator ControlToValidate="TextBoxEmail" Display="Dynamic" runat="server" ValidationExpression="\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"  ErrorMessage="Email is not valid." ForeColor="Red"></asp:RegularExpressionValidator>
                    </div>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label runat="server">Creator?</asp:Label>
                </td>
                <td>
                    <table style="width:100%;">
                        <tr>
                            <td>
                                <asp:RadioButton ID="RadioCreator" Text="Yes" Checked="False" GroupName="RadioType" runat="server" />
                            </td>
                            <td>
                                <asp:RadioButton ID="RadioUser" Text="No" Checked="True" GroupName="RadioType" runat="server" />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td></td>
                <td><div class="Error"></div></td>
            </tr>
        </table>
        <asp:Button runat="server" Text="Submit" OnClick="Register" />
    </div>
</asp:Content>
