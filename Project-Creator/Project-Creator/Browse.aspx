<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Browse.aspx.cs" Inherits="Project_Creator.Browse" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceholder" runat="server">
    <div style="width:100%;">
        <asp:Repeater ID="RepeaterProject" ItemType="Project_Creator.Project" runat="server">
            <ItemTemplate>
                    <div class="Basic Browse" style="padding:8px;margin-left:auto;margin-right:auto;width:100%;max-width:1014px;">
                        <a href="/Projects/View?p=<%#:Item.projectID%>">
                            <asp:Table runat="server">
                                <asp:TableRow>
                                    <asp:TableCell>
                                        <asp:Image ID="ImageProject" CssClass="Project" ImageUrl="<%#:Item.project_image_path %>" runat="server" />
                                    </asp:TableCell>
                                    <asp:TableCell>
                                        <table style="padding-left:12px">
                                            <tr>
                                                <td>
                                                    <h1 style="text-align:left"><%#:Item.project_name %></h1>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <h2 style="text-align:left"><%#:Item.project_desc %></h2>
                                                </td>
                                            </tr>
                                        </table>
                                    </asp:TableCell>
                                </asp:TableRow>
                            </asp:Table>
                        </a>
                    </div>
            </ItemTemplate>
        </asp:Repeater>
    </div>
</asp:Content>
