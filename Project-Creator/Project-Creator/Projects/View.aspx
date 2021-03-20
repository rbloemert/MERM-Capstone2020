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
                                <asp:Image ID="ProjectIcon" CssClass="Project" runat="server" />
                            </div>
                        </div>
                        <div class="ProjectColumn" style="width: 100%;max-width: 800px;">
                            <div class="Basic" style="height:90px;">
                                <h1 style="text-align: left"><asp:Label ID="lblTitle" runat="server" /></h1>
                                <br />
                                <h2 style="text-align: left"><asp:Label ID="lblDescription" runat="server" /></h2>
                            </div>
                            <div class="Basic" style="height:16px;margin-top:4px;">
                                <table style="margin:0;width:100%;">
                                    <tr>
                                        <td>
                                            <h2 class="Project" style="text-align: left">Project Creator: <a><span style="color: white;">
                                            <asp:Label ID="lblAuthor" runat="server" /></span></a></h2>
                                        </td>
                                        <td>
                                            <h2 class="Project" style="text-align: left">Creation Date: <span style="color: white;">
                                            <asp:Label ID="lblDate" runat="server" /></span></h2>
                                        </td>
                                    </tr>
                                </table>
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
                        <div class="ProjectColumn" style="width: 100%; max-width: 1014px;">
                            <div class="Basic" style="margin-top:0;min-height:474px;">
                                <div class="gallery js-flickity" data-flickity-options='{ "pageDots": false, "initialIndex": <%=TimelineIndex %> }'>

                                    <!-- Timeline carousel repeater. -->
                                    <asp:Repeater ID="RepeaterTimeline" ItemType="Project_Creator.Timeline" runat="server">
                                        <ItemTemplate>
                                            <div class="gallery-cell">
                                                <a style="text-decoration:none;" href="Updates/View?p=<%#ProjectID %>&u=<%#Item.timelineID %>">
                                                    <div class="Basic Timeline">
                                                        <table>
                                                            <tr>
                                                                <td>
                                                                    <asp:Image CssClass="gallery-image" ImageUrl="<%#Item.timeline_image_path %>" runat="server" />
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    <h2 class="TimelineTitle"><%#Item.timeline_name %></h2>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    <hr />
                                                                    <p style="height:116px;"><%#Item.timeline_desc %></p>
                                                                    <hr />
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>Date Created: <%#Item.timeline_creation.Value.ToString("yyyy-MM-dd") %>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </div>
                                                </a>
                                            </div>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                    <!-- Timeline carousel repeater. -->

                                </div>
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
                        <div class="ProjectColumn" style="width: 100%; max-width: 1014px;">
                            <div class="Basic" style="margin-top: 0">
                                <div class="related-panel">
                                    <asp:Repeater ID="RepeaterRelated" ItemType="Project_Creator.Project" runat="server">
                                        <ItemTemplate>
                                            <div class="timeline-content">
                                                <div class="Basic" style="width: 90%; height: 250px; margin: 0; padding: 8px;">
                                                    <table>
                                                        <tr>
                                                            <td>
                                                                <a href="View?p=<%#Item.projectID %>">
                                                                    <asp:Image CssClass="related-img" ImageUrl="<%#Item.project_image_path %>" runat="server" />
                                                                </a>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <h2><%#Item.project_name %></h2>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <hr />
                                                                <p><%#Item.project_desc %></p>
                                                                <hr />
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
