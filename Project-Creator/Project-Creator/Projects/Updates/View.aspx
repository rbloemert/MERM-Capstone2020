<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="View.aspx.cs" Inherits="Project_Creator.Posts.View" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link rel="stylesheet" href="/StyleSheets/StyleSheetFlickity.css">
    <script src="https://unpkg.com/flickity@2/dist/flickity.pkgd.min.js"></script>
    <script src="JavaScript.js"></script>
    <script type="text/javascript">
        function maybeDont() {
            var fileName = "<%=FileLink %>";
            var fileExtension = fileName.substr((fileName.lastIndexOf('.') + 1));
            console.log(fileExtension);
            if (fileExtension != "pdf") {
                fileName = "";
            }
            document.write('<div><object data="' + fileName + '" id="wrapper" type="application/pdf" width="500" height="678"><iframe src="' + fileName + '" id="viewer" width="500" height="678"><p>This browser does not support PDF!</p></iframe></object></div>');
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceholder" runat="server">
    <asp:Table Width="100%" runat="server">
        <asp:TableRow>
            <asp:TableCell>
                <div class="ProjectTable">
                    <div class="ProjectRow">
                        <div class="ProjectColumn" style="width: 100%; max-width: 1014px;">
                            <div class="Basic" style="height: 72px; margin-top: 4px;">
                                <table style="width: 100%;">
                                    <tr>
                                        <td>
                                            <h1 class="Project" style="text-align: left">
                                                <asp:Label ID="lblUpdate" runat="server" /></h1>
                                        </td>
                                        <td>
                                            <div style="float: right" id="ButtonEdit" runat="server">
                                                <a style="display: flex; width: 32px; height: 32px; align-items: center; justify-content: center" href="/Projects/Updates/Edit?p=<%=ProjectID %>&u=<%=UpdateID %>">
                                                    <i style="color: gray;" class="gg-pen"></i>
                                                </a>
                                            </div>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <h2 class="Project" style="text-align: left">Creation Date: <span style="color: white;">
                                                <asp:Label ID="lblDate" runat="server" /></span></h2>
                                        </td>
                                        <td>
                                            <a style="text-align: right; text-decoration: none;" href="../View?p=<%=ProjectID %>">
                                                <h3>Back to project</h3>
                                            </a>
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
                            <div class="Basic" style="margin: 0;">
                                <div class="timeline-image-container">
                                    <asp:Image ID="TimelineImage" CssClass="timeline-image" runat="server" />
                                </div>
                            </div>
                            <div class="Basic" style="margin-top: 5px;">
                                <p>
                                    <asp:Label ID="lblDesc" runat="server"></asp:Label>
                                </p>
                            </div>
                            <div class="Basic" style="margin-top: 5px;">
                                <div id="FileImage" runat="server" style="display: none">
                                    <img src="<%=FileLink %>" class="file-image" />
                                </div>
                                <div id="FileVideo" runat="server" style="display: none">
                                    <video width="960" height="540" src="<%=FileLink %>" controls autoplay>
                                        <script>
                                            var video = document.currentScript.parentElement;
                                            video.volume = 0.3;
                                        </script>
                                    </video>
                                </div>
                                <div id="FilePDF" runat="server" style="display: none">
                                    <!-- <a href="http://docs.google.com/gview?url=<%=FileLink %>">Click here to view the Pdf Document</a> -->
                                    <script type="text/javascript">
                                        maybeDont();
                                    </script>
                                </div>
                                <div id="FileText" runat="server" style="display: none; text-align: left; white-space: pre-wrap;">
                                    <asp:Label Height="256" Width="100%" Style="overflow-y: scroll;" ID="FileTextContent" runat="server"></asp:Label>
                                </div>
                                <div id="FileZip" runat="server" style="display:none;text-align:left">
                                    <input type="button" style="margin:0;float:right" onclick="location.href='<%=FileLink %>';" value="Download" />
                                    <table style="height:29px">
                                        <tbody>
                                            <tr>
                                                <td>
                                                    <i class="gg-folder"></i>
                                                </td>
                                                <td>
                                                    <p style="padding-left:12px">Attached Downloadable <%=FileType %></p>
                                                </td>
                                            </tr>
                                        </tbody>
                                    </table>
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
                                <div class="gallery js-flickity" data-flickity-options='{ "pageDots": false, "initialIndex": <%=TimelineIndex %> }'>

                                    <!-- Timeline carousel repeater. -->
                                    <asp:Repeater ID="RepeaterTimeline" ItemType="Project_Creator.Timeline" runat="server">
                                        <ItemTemplate>
                                            <div class="gallery-cell">
                                                <a style="text-decoration: none;" href="View?p=<%#ProjectID %>&u=<%#Item.timelineID %>">
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
                                                                    <p style="height: 116px;"><%#Item.timeline_desc %></p>
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
                                <asp:Repeater ID="RepeaterComment" ItemType="Project_Creator.Comment2" OnItemCommand="RepeaterComment_ItemCommand" runat="server">
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
                                                        <asp:Button ClientIDMode="AutoID" ID="btnEditThisItem" CommandName="delete_comment" CssClass="comment-delete-button" CommandArgument='<%# Item.commentID + "," + Item.comment_owner_accountID %>' Text="Delete" runat="server" />
                                                    </td>
                                                </tr>
                                            </table>
                                        </div>
                                    </ItemTemplate>
                                </asp:Repeater>
                                <div class="Basic">
                                    <table style="width: 100%">
                                        <tr>
                                            <td class="comment-info">
                                                <asp:Image ID="LoggedInUserImage" CssClass="comment-img" runat="server" />
                                                <asp:Label ID="lblNewCommentUser" runat="server"></asp:Label>
                                            </td>
                                            <td class="comment-content">
                                                <p>
                                                    <asp:TextBox ID="txtNewComment" CssClass="comment-textbox" TextMode="MultiLine" runat="server" onKeyUp="TextChanged()" MaxLength="255" ClientIDMode="Static" Style="overflow: hidden;"></asp:TextBox>
                                                    <script>
                                                        function TextChanged() {
                                                            val = document.getElementById("txtNewComment").value;
                                                            document.getElementById("lblDescCounter").innerHTML = val.length + " of 255";
                                                        }
                                                    </script>
                                                    <br />
                                                    <asp:Label ID="lblDescCounter" runat="server" ClientIDMode="Static">0 of 255</asp:Label>
                                                </p>
                                            </td>
                                            <td class="comment-submit">
                                                <asp:Button ID="btnSubmitComment" CssClass="comment-button" Text="Submit" OnClick="btnSubmitComment_Click" runat="server" />
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
