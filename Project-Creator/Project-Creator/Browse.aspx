<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Browse.aspx.cs" Inherits="Project_Creator.Browse" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceholder" runat="server">
    <div class="Basic">
        <asp:Repeater ID="RepeaterProject" ItemType="Project_Creator.Project" runat="server">
            <ItemTemplate>
                <asp:Table runat="server">
                    <asp:TableRow>
                        <asp:TableCell>
                            <asp:Image ID="ImageProject" ImageUrl="<%#:Item.project_icon_path%>" runat="server" />
                        </asp:TableCell>
                    </asp:TableRow>
                </asp:Table>
            </ItemTemplate>
        </asp:Repeater>
    </div>
</asp:Content>
