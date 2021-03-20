<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Browse.aspx.cs" Inherits="Project_Creator.Browse" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceholder" runat="server">
    <div style="width:100%;">
        <asp:Repeater ID="RepeaterProject" ItemType="Project_Creator.Project" runat="server">
            <ItemTemplate>
                    <div class="Basic Browse" style="text-align:left;padding:8px;margin-left:auto;margin-right:auto;width:95%;max-width:1014px;">
                        <a href="/Projects/View?p=<%#:Item.projectID%>">
                            <asp:Table runat="server" Width="100%">
                                <asp:TableRow>
                                    <asp:TableCell Width="160px">
                                        <asp:Image ID="ImageProject" CssClass="Project" ImageUrl="<%#:Item.project_image_path %>" runat="server" />
                                    </asp:TableCell>
                                    <asp:TableCell>
                                        <table style="width:100%;padding-left:12px">
                                            <tr>
                                                <td>
                                                    <h1 style="text-align:left"><%#:Item.project_name %></h1>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <h2 style="text-align:left"><%#:Item.project_desc %></h2>
                                                    <hr style="margin-top:8px;" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <table>
                                                        <tr>
                                                            <td>
                                                                <div style="text-align:left">
                                                                    <span style="color:dimgray">Creator: </span><span style="color:white"><%#:Item.project_author %></span>
                                                                </div>
                                                            </td>
                                                            <td>
                                                                <div style="margin-left:12px;text-align:left">
                                                                    <span style="color:dimgray">Created: </span><span style="color:white"><%#:Item.project_creation.Value.ToString("yyyy-MM-dd") %></span>
                                                                </div>
                                                            </td>
                                                        </tr>
                                                    </table>
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
