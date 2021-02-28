<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="View.aspx.cs" Inherits="Project_Creator.Projects.View" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link rel="stylesheet" href="../StyleSheets/StyleSheetFlickity.css">
    <script src="https://unpkg.com/flickity@2/dist/flickity.pkgd.min.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceholder" runat="server">
    <asp:Table Width="100%" runat="server">
        <asp:TableRow>
            <asp:TableCell>
                <div class="ProjectTable">
                    <div class="ProjectRow">
                        <div class="ProjectColumn">
                            <div class="Basic">
                                <asp:Image CssClass="Project" runat="server" />
                            </div>
                        </div>
                        <div class="ProjectColumn" style="width:100%;max-width:800px;">
                            <div class="Basic">
                                <h1 style="text-align:left">Sample Project</h1>
                            </div>
                            <div class="Basic" style="height:72px;margin-top:4px;">
                                <h2 class="Project" style="text-align:left">Project Creator: <span style="color:white;">Kupoapo</span></h2>
                                <br />
                                <h2 class="Project" style="text-align:left">Creation Date: <span style="color:white;">2021-02-24</span></h2>
                            </div>
                        </div>
                    </div>
                </div>
            </asp:TableCell>
        </asp:TableRow>
        <asp:TableRow>
            <asp:TableCell>
                <div class="ProjectTable">
                    <div class="ProjectRow">
                        <div class="ProjectColumn" style="width:100%;max-width:1014px;">
                            <div class="Basic" style="margin-top:0">
                                <div class="gallery js-flickity" data-flickity-options='{ "wrapAround": true }'>
                                    <asp:Repeater ID="RepeaterTimeline" ItemType="Project_Creator.Timeline" runat="server">
                                        <ItemTemplate>
                                            <div class="gallery-cell">
                                                <div class="Basic" style="width:90%;height:360px;margin:0;padding:8px;">
                                                    <table>
                                                        <tr>
                                                            <td>
                                                                <asp:Image CssClass="gallery-image" ImageUrl="<%#Item.timeline_image_path %>" runat="server" />
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <h2><%#Item.timeline_name %></h2>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <hr />
                                                                <p><%#Item.timeline_desc %></p>
                                                                <hr />
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                Date Created: <%#Item.timeline_creation %>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </div>
                                            </div>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </asp:TableCell>
        </asp:TableRow>
    </asp:Table>
</asp:Content>
