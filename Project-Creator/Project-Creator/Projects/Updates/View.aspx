<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="View.aspx.cs" Inherits="Project_Creator.Posts.View" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link rel="stylesheet" href="/StyleSheets/StyleSheetFlickity.css">
    <script src="https://unpkg.com/flickity@2/dist/flickity.pkgd.min.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceholder" runat="server">
    <asp:Table Width="100%" runat="server">
        <asp:TableRow>
            <asp:TableCell>
                <div class="ProjectTable">
                    <div class="ProjectRow">
                        <div class="ProjectColumn" style="width: 100%; max-width: 1014px;">
                            <div class="Basic" style="height: 72px; margin-top: 4px;">
                                <table style="width:100%;">
                                    <tr>
                                        <td>
                                            <h1 class="Project" style="text-align: left"><asp:Label ID="lblUpdate" runat="server" /></h1>
                                        </td>
                                        <td>
                                            <a style="text-align:right;text-decoration:none;" href="../View?p=<%=ProjectID %>"><h3>Back to project</h3></a>
                                        </td>
                                    </tr>
                                </table>
                                <br />
                                <h2 class="Project" style="text-align: left">Creation Date: <span style="color: white;"><asp:Label ID="lblDate" runat="server" /></span></h2>
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
                            <div class="Basic" style="margin:0;">
                                <asp:Image ID="TimelineImage" CssClass="timeline-image" runat="server" />
                                <br />
                            </div>
                            <div class="Basic" style="margin-top:5px;">
                                <p>
                                    <asp:Label ID="lblDesc" runat="server"></asp:Label>
                                </p>
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
                                <div class="gallery js-flickity" data-flickity-options='{ "wrapAround": true }'>

                                    <!-- Timeline carousel repeater. -->
                                    <asp:Repeater ID="RepeaterTimeline" ItemType="Project_Creator.Timeline" runat="server">
                                        <ItemTemplate>
                                            <div class="gallery-cell">
                                                <a style="text-decoration:none;" href="View?p=<%#ProjectID %>&u=<%#Item.timelineID %>">
                                                    <div class="Basic Timeline">
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
                                                                    <p style="height:120px;"><%#Item.timeline_desc %></p>
                                                                    <hr />
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>Date Created: <%#Item.timeline_creation %>
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
                                <asp:Repeater ID="RepeaterComment" ItemType="Project_Creator.Comment2" runat="server">
                                    <ItemTemplate>
                                        <div class="Basic">
                                            <table>
                                                <tr>
                                                    <td class="comment-info">
                                                        <a href="/Home">
                                                            <asp:Image CssClass="comment-img" ImageUrl="<%#Item.account_image_path %>" runat="server" />
                                                        </a>
                                                        <br />
                                                        <%#Item.comment_account_name %>
                                                        <br />
                                                        <%# Item.comment_creation %>
                                                    </td>
                                                    <td class="comment-content">
                                                        <p><%#Item.comment_text %></p>
                                                    </td>
                                                </tr>
                                            </table>
                                        </div>
                                    </ItemTemplate>
                                </asp:Repeater>
                                <div class="Basic">
                                    <table>
                                        <tr>
                                            <td class="comment-info">
                                                <asp:Button ID="btnSubmitComment" CssClass="comment-button" Text="Submit" OnClick="btnSubmitComment_Click" runat="server" />
                                            </td>
                                            <td class="comment-content">
                                                <asp:Textbox id="txtNewComment" CssClass="comment-textbox" runat="server"></asp:Textbox>
                                            </td>
                                        </tr>

                                    </table>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </asp:TableCell>
        </asp:TableRow>
    </asp:Table>
</asp:Content>
