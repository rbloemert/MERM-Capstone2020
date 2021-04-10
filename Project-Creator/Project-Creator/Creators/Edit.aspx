<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Edit.aspx.cs" Inherits="Project_Creator.Creators.Edit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder" runat="server">
    <asp:Panel ID="myAccountPanel" class="Basic" Width="80%" runat="server">
        <h1>My Account</h1><br/>
        <asp:Label ID="MyAccountFullNameLabel" runat="server"/><br/>
        <asp:Label ID="MyAccountDescLabel" runat="server"/><br/>
        <asp:Label ID="MyAccountUsernameLabel" runat="server"/><br/>
        <asp:Label ID="MyAccountEmailLabel" runat="server"/><br/>
        <asp:Button ID="MyAccountEditButton" Text="Edit Account" OnClick="MyAccountEditButton_OnClick" runat="server"/><br/>
        <asp:Panel ID="myAccountEditPanel" Visible="False" runat="server">
            <%-- all edit panel items --%>
            Full Name <asp:TextBox ID="fullNameTextbox" Width="20%" runat="server"/><br/>
            Description <asp:TextBox ID="creatorDescTextbox" Width="20%" runat="server"/><br/>
            Email <asp:TextBox ID="emailTextbox" Width="20%" runat="server"/><br/>
            Username <asp:TextBox ID="usernameTextbox" Width="20%" runat="server"/><br/>
            Password <asp:TextBox ID="passwordTextbox" Width="20%" runat="server"/><br/>
            Display Full Name <asp:CheckBox ID="allowFullnameCheckbox" runat="server"/><br/>
            Allow Contact by Email <asp:CheckBox ID="allowContactCheckbox" runat="server"/><br/>
            <asp:Button ID="editSubmitButton" Text="Submit" OnClick="editSubmitButton_OnClick" runat="server"/>
            <asp:Button ID="editCancelButton" Text="Cancel" OnClick="editCancelButton_OnClick" runat="server"/>
            <br/>
            <asp:Label ID="editErrorLabel" Visible="False" runat="server"/>
        </asp:Panel>
    </asp:Panel>
</asp:Content>