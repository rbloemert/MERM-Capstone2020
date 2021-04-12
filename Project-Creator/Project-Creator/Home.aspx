<%@ Page Title="Home" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Home.aspx.cs" Inherits="Project_Creator.Home" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link rel="stylesheet" href="/StyleSheets/StyleSheetFlickity.css">
    <script src="https://unpkg.com/flickity@2/dist/flickity.pkgd.min.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder" runat="server">
    
    <div style="width: 100%;padding-top:20px;" id="WelcomeMessage" runat="server">
        <table style="margin:auto;">
            <tr>
                <td>
                    <img src="/Images/Icon_Project.png" style="width:200px;height:200px;"/>
                </td>
                <td style="padding-left:40px;text-align:left">
                    <h1>Welcome to Project Creator!</h1>
                    <p style="color:lightgrey;width:400px;word-break:break-word"> 
                        Project Creator is a website that lets you organize your projects
                        in timelines of updates which other users can follow. Give your audience
                        the choice of what they want to see from you!
                    </p>
                </td>
            </tr>
        </table>
        <br />
        <table style="margin:auto;">
            <tr>
                <td>
                    <input style="padding:12px;width:300px;" type="button" onclick="window.location.href='/Login'" value="Login">
                </td>
                <td style="padding-left:40px;">
                    <input style="padding:12px;width:300px;" type="button" onclick="window.location.href='/Signup'" value="Signup">
                </td>
            </tr>
        </table>
        <br />
        <table style="margin:auto;">
            <tr>
                <td style="text-align:center;width:300px;">
                    <img src="/Images/Icon_Folder.png" style="width:200px;height:200px;"/>
                    <h1>Organize!</h1>
                    <br />
                    <p style="color:lightgrey;word-break:break-word;text-align:center;text-justify:none"> 
                        Are you a Jack of all trades? 
                        Keep your different skills seperate and easy to find.
                        Someone may not like your smithing videos but love your wood carving!
                    </p>
                </td>
                <td style="text-align:center;width:300px;padding-left:100px;">
                    <img src="/Images/Icon_Video.png" style="width:200px;height:200px;"/>
                    <h1>Upload!</h1>
                    <br />
                    <p style="color:lightgrey;word-break:break-word;text-align:center;text-justify:none"> 
                        Upload many different multimedia files which works for your kind of project.
                        Do you like creating videos? Do you draw artwork? Do you program videogames?
                        Upload videos, images, and ZIP files to share with your audience.
                    </p>
                </td>
                <td style="text-align:center;width:300px;padding-left:100px;">
                    <img src="/Images/Icon_Search.png" style="width:200px;height:200px;"/>
                    <h1>Browse!</h1>
                    <br />
                    <p style="color:lightgrey;word-break:break-word;text-align:center;text-justify:none"> 
                        Search through projects that may interest or inspire you. Find and follow
                        projects that show potential and get updates about its progress.
                    </p>
                </td>
            </tr>
        </table>
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
                                                            <td id="divEmail" runat="server">
                                                                <div style="margin-right:12px;">
                                                                    <h2 class="Project" style="text-align: left">Email: <a><span style="color: white;">
                                                                    <asp:Label ID="lblEmail" runat="server" /></span></a></h2>
                                                                </div>
                                                            </td>
                                                            <td>
                                                                <h2 class="Project" style="text-align: left">Creation Date: <span style="color: white;">
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
                                    <div class="Basic" style="position:relative;margin-top:0;min-height:474px;">
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
                                        <h1 id="msgEmpty" runat="server" class="popup" style="color:gray;width:100%;">Sorry, we have no updates to show...</h1>
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
