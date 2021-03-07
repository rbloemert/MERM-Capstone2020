<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Edit.aspx.cs" Inherits="Project_Creator.Projects.Edit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
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
                        <div class="ProjectColumn" style="width: 100%; max-width: 800px;">
                            <div class="Basic">
                                <div class="Basic">
                                    <asp:TextBox ID="TextBoxTitle" runat="server"></asp:TextBox>
                                </div>
                            </div>
                            <div class="Basic" style="height: 72px; margin-top: 4px;">
                                <h2 class="Project" style="text-align: left">Project Creator: <span style="color: white;">
                                    <asp:Label ID="lblAuthor" runat="server" /></span></h2>
                                <br />
                                <h2 class="Project" style="text-align: left">Creation Date: <span style="color: white;">
                                    <asp:Label ID="lblDate" runat="server" /></span></h2>
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
    </asp:Table>
</asp:Content>