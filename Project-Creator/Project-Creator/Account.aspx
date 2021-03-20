<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Account.aspx.cs" Inherits="Project_Creator.Account1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder" runat="server">
    <asp:Table runat="server" Width="100%">
        <asp:TableRow runat="server">
            <asp:TableCell runat="server">
                <div class="Basic">
                    My Account
                    <div><asp:Label ID="MyAccountFullNameLabel" runat="server" /></div>
                    <div><asp:Label ID="MyAccountUsernameLabel" runat="server" /></div>
                    <div><asp:Label ID="MyAccountEmailLabel" runat="server" /></div>
                </div>
            </asp:TableCell>
        </asp:TableRow>
        <asp:TableRow runat="server">
            <asp:TableCell runat="server">
                <div class="Basic">
                    My Projects
                    <div><asp:Label ID="MyAccountProjectsOwnedLabel" runat="server" /></div>
                    <asp:Repeater ID="MyAccountProjectRepeater" ItemType="Project_Creator.Project" runat="server">
                        <ItemTemplate>
                            <asp:Table runat="server">
                                <asp:TableRow>
                                    <asp:TableCell>
                                        <asp:Image ID="ImageProject" ImageUrl="<%#:Item.project_image_path%>" runat="server" />
                                    </asp:TableCell>
                                </asp:TableRow>
                            </asp:Table>
                        </ItemTemplate>
                    </asp:Repeater>
                </div>
            </asp:TableCell>
        </asp:TableRow>
    </asp:Table>
    
    
    
</asp:Content>
