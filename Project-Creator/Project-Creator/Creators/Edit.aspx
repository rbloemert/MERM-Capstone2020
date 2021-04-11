<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Edit.aspx.cs" Inherits="Project_Creator.Creators.Edit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="Creators_JavaScript.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder" runat="server">
    <div class="Basic" style="width: 100%; max-width: 1014px;">
        <h1>Account Information</h1>
        <asp:Label ID="editErrorLabel" Visible="False" runat="server" />
        <div class="Basic" style="float: left">
            <table>
                <tr>
                    <td>
                        <h3>ProfilePicture</h3>
                    </td>
                </tr>
                <tr>
                    <div class="imgHolder">
                        <img id="account_image" src="<%=imagePath %>" class="Project" />
                        <input type="file" id="ImageUploader" name="ImageUploader" onchange="previewAccountImage()" hidden />
                        <label for="ImageUploader" class="Project">Upload Image</label>
                    </div>
                </tr>
            </table>
        </div>
        <br />
        <hr />
        <br />
        <table style="width: 60%; margin: auto;">
            <tr>
                <td style="width: 30%">
                    <h3>Username</h3>
                </td>
                <td style="width: 60%; text-align: left;">
                    <asp:Label ID="usernameTextbox" Width="100%" runat="server" />
                </td>
            </tr>
            <tr>
                <td style="width: 30%">
                    <h3>Description</h3>
                </td>
                <td style="width: 60%">
                    <asp:TextBox ID="creatorDescTextbox" Width="100%" runat="server" MaxLength="40" />
                </td>
            </tr>
        </table>
        <br />
        <hr />
        <br />
        <table style="width: 60%; margin: auto;">
            <tr>
                <td style="width: 30%">
                    <h3>Password</h3>
                </td>
                <td style="width: 60%">
                    <asp:TextBox ID="passwordTextbox" TextMode="Password" Width="100%" runat="server" />
                </td>
            </tr>
            <tr>
                <td>
                    <h3>Confirm Password</h3>
                </td>
                <td style="width: 60%">
                    <asp:TextBox ID="passwordConfirmTextbox" TextMode="Password" Width="100%" runat="server" />
                    <asp:CompareValidator ControlToValidate="passwordTextbox" ControlToCompare="passwordConfirmTextbox" runat="server" ErrorMessage="Passwords are not equal." Display="Static" ForeColor="Red"></asp:CompareValidator>
                </td>
            </tr>
        </table>
        <br />
        <hr />
        <br />
        <table style="width: 60%; margin: auto;">
            <tr>
                <td style="width: 30%">
                    <h3>Full Name</h3>
                </td>
                <td style="width: 60%">
                    <asp:TextBox ID="fullNameTextbox" Width="100%" runat="server" />
                </td>
            </tr>
            <tr>
                <td style="width: 30%">
                    <h3>Email</h3>
                </td>
                <td style="width: 60%">
                    <asp:TextBox ID="emailTextbox" Width="100%" runat="server" />
                </td>
            </tr>
        </table>
        <br />
        <hr />
        <br />
        <table style="width: 60%; margin: auto;">
            <tr>
                <td>
                    <asp:CheckBox ID="allowFullnameCheckbox" runat="server" />
                    <asp:Label AssociatedControlID="allowFullnameCheckbox" runat="server">Display Full Name</asp:Label>
                </td>
                <td>
                    <asp:CheckBox ID="allowContactCheckbox" runat="server" />
                    <asp:Label AssociatedControlID="allowContactCheckbox" runat="server">Display Email</asp:Label>
                </td>
            </tr>
        </table>
        <br />
        <hr />
        <br />
        <asp:Button ID="editSubmitButton" Text="Save Changes" OnClick="editSubmitButton_OnClick" runat="server" />
        <br />
    </div>
</asp:Content>
