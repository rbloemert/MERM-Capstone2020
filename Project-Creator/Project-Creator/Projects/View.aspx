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
                                <div style="float:right" id="ButtonEdit" runat="server">
                                    <a style="display:flex;width:32px;height:32px;align-items:center;justify-content:center" href="/Projects/Edit?p=<%=ProjectID %>">
                                        <i style="color:gray;" class="gg-pen">

                                        </i>
                                    </a>
                                </div>
                                <h1 style="text-align: left"><asp:Label ID="lblTitle" runat="server" /></h1>
                                <h2 style="text-align: left"><asp:Label ID="lblDescription" runat="server" /></h2>
                                <table>
                                    <tr>
                                        <td>
                                            <div style="margin-top: 8px;text-align: left">
                                                <asp:Button ID="ButtonFollow" Text="Follow" Width="200px" runat="server" OnClick="Follow_Click" />
                                            </div>
                                        </td>
                                        <td>
                                            <div style="padding-left:6px;padding-top:6px;text-align:left;">
                                                <asp:Label ID="lblFollowers" runat="server" />
                                            </div>
                                        </td>
                                        <td>
                                            <div style="padding-left:6px;padding-top:6px;text-align:left;">
                                                <asp:Label CssClass="Label" ID="lblFollowing" Text="(You follow this project)" runat="server" Visible="false"/>
                                            </div>
                                        </td>
                                    </tr>
                                </table>
                            </div>
                            <div class="Basic" style="height:16px;margin-top:4px;">
                                <table style="margin:0;width:100%;">
                                    <tr>
                                        <td>
                                            <h2 class="Project" style="text-align: left">Project Creator: <a href="../Creators/View.aspx?c=<%=CreatorID%>"><span style="color: white;">

                                            <asp:Label ID="lblAuthor" runat="server" />
                                            </span></a></h2>
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
    </asp:Table>
</asp:Content>
