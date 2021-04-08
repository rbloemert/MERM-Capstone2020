<%@ Page Title="Home" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Home.aspx.cs" Inherits="Project_Creator.Home" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link rel="stylesheet" href="/StyleSheets/StyleSheetFlickity.css">
    <script src="https://unpkg.com/flickity@2/dist/flickity.pkgd.min.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder" runat="server">
    
    <div class="centered" style="width: 100%; height: 50%;" id="WelcomeMessage" runat="server">
        <h1 ID="lblWelcomeMessage">Welcome to Project Creator</h1><br/><br/>
        <h2 ID="lblPleaseLogin">Please <a href="Login.aspx"><u>Log In</u></a> to view your account</h2>
    </div>

    <div style="width:100%" id="Notifications" runat="server">
        <asp:Table Width="100%" runat="server">
            <asp:TableRow>
                <asp:TableCell>
                    <div class="ProjectTable">
                        <div class="ProjectRow">
                            <div class="ProjectColumn">
                                <div class="Basic">
                                    <asp:Image ID="AccountIcon" Width="80" Height="80" CssClass="Project" runat="server" />
                                </div>
                            </div>
                            <div class="ProjectColumn" style="width: 100%;max-width: 880px;">
                                <div class="Basic" style="height:80px;">
                                    <table style="margin:0;width:100%;">
                                        <tr>
                                            <td>
                                                <table>
                                                    <tbody>
                                                        <tr>
                                                            <td>
                                                                <h1 style="text-align: left"><asp:Label ID="lblUsername" runat="server" /></h1>
                                                            </td>
                                                            <td>
                                                                <h2 style="margin-left:12px;text-align: left"><asp:Label ForeColor="Gray" ID="lblFullname" runat="server" /></h2>
                                                            </td>
                                                        </tr>
                                                    </tbody>
                                                </table>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <table>
                                                    <tbody>
                                                        <tr>
                                                            <td>
                                                                <h2 class="Project" style="text-align: left">Email: <a><span style="color: white;">
                                                                <asp:Label ID="lblEmail" runat="server" /></span></a></h2>
                                                            </td>
                                                            <td>
                                                                <h2 class="Project" style="margin-left:12px;text-align: left">Creation Date: <span style="color: white;">
                                                                <asp:Label ID="lblDate" runat="server" /></span></h2>
                                                            </td>
                                                        </tr>
                                                    </tbody>
                                                </table>
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
                                <div style="width: 100%; max-width: 1014px;">
                                    <div class="Basic" style="margin-top:0;min-height:474px;">
                                        <div class="Basic" style="margin:0">
                                            <h1 style="color:navajowhite">
                                                New Project Updates:
                                            </h1>
                                        </div>
                                        <div class="gallery js-flickity" data-flickity-options='{ "pageDots": false, "initialIndex": <%=TimelineIndex %> }'>

                                            <!-- Timeline carousel repeater. -->
                                            <asp:Repeater ID="RepeaterTimeline" ItemType="Project_Creator.Timeline" runat="server">
                                                <ItemTemplate>
                                                    <div class="gallery-cell">
                                                        <a style="text-decoration:none;" href="/Projects/Updates/View?p=<%#Item.timeline_project %>&u=<%#Item.timelineID %>">
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
                    </div>
                </asp:TableCell>
            </asp:TableRow>
        </asp:Table>
    </div>
</asp:Content>
