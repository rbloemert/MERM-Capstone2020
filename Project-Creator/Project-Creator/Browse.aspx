<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Browse.aspx.cs" Inherits="Project_Creator.Browse" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link rel="stylesheet" href="../StyleSheets/StyleSheetFlickity.css">
    <script src="https://unpkg.com/flickity@2/dist/flickity.pkgd.min.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceholder" runat="server">
    <div style="width:100%;">
        <div id="SearchOptions" class="Basic" style="text-align:left;padding:8px;margin-left:auto;margin-right:auto;width:95%;max-width:1014px;">
            <asp:Button Text="Search" CssClass="ButtonSearch" runat="server" OnClick="Search_Click" />
            <table style="width:80%">
                <tbody>
                    <tr>
                        <td style="width:20%;">
                            <p style="text-overflow:ellipsis;text-wrap:none;white-space:nowrap"><b>Advanced Search:</b></p>
                        </td>
                        <td style="width:70%">
                            <asp:TextBox ID="SearchBox" Width="100%" runat="server"></asp:TextBox>
                        </td>
                        <td style="width:10%;padding-left:12px">
                            <asp:DropDownList ID="DropDownSort" runat="server">
                                <asp:ListItem Value="1">Newest</asp:ListItem>
                                <asp:ListItem Value="2">Oldest</asp:ListItem>
                                <asp:ListItem Selected="True" Value="3">Most Popular</asp:ListItem>
                                <asp:ListItem Value="4">Least Popular</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                    </tr>
                </tbody>
            </table>
        </div>
        <div id="SearchCreator" runat="server" class="Basic" style="text-align:left;padding:8px;margin-left:auto;margin-right:auto;width:95%;max-width:1014px;">
            <div class="gallery js-flickity" data-flickity-options='{ "pageDots": false }' style="height:auto">
                <asp:Repeater ID="RepeaterCreator" ItemType="Project_Creator.Account" runat="server">
                    <ItemTemplate>
                        <div class="gallery-cell" style="width:auto;height:auto;">
                            <a href="/Creators/View?c=<%#:Item.accountID %>">
                                <div class="Basic Browse">
                                    <table>
                                        <tbody>
                                            <tr>
                                                <td style="text-align:center">
                                                    <img src="<%#:Item.account_image_path.Replace("~", "") %>" width="128" height="128" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="text-align:center">
                                                    <h2><%#:Item.username %></h2>
                                                </td>
                                            </tr>
                                        </tbody>
                                    </table>
                                </div>
                            </a>
                        </div>
                    </ItemTemplate>
                </asp:Repeater>
            </div>
        </div>
        <div id="SearchProject" class="basic">
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
    </div>
</asp:Content>
