<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Edit.aspx.cs" Inherits="Project_Creator.Projects.Edit" %>
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
                                <asp:FileUpload ID="FileImage" runat="server" hidden/>
                                <asp:Label CssClass="Project" AssociatedControlID="FileImage" runat="server">Upload Image</asp:Label>
                            </div>
                        </div>
                        <div class="ProjectColumn" style="width: 100%; max-width: 800px;">
                            <div class="Basic" style="padding:16px">
                                <asp:TextBox Font-Size="X-Large" Width="98%" ID="TextBoxTitle" runat="server" placeholder="Project Title..."></asp:TextBox>
                                <asp:RequiredFieldValidator ControlToValidate="TextBoxTitle" Display="Static" ErrorMessage="A project title is required." runat="server"></asp:RequiredFieldValidator>
                                <asp:TextBox Font-Size="Medium" Width="98%" ID="TextBoxDescription" runat="server" placeholder="Project Description..."></asp:TextBox>
                                <asp:RequiredFieldValidator ControlToValidate="TextBoxDescription" Display="Static" ErrorMessage="A project title is required." runat="server"></asp:RequiredFieldValidator>
                            </div>
                            <div class="Basic" style="height:16px;margin-top:4px;">
                                <table style="margin:0;width:100%;">
                                    <tr>
                                        <td>
                                            <h2 class="Project" style="text-align: left">Project Creator: <span style="color: white;">
                                            <asp:Label ID="lblAuthor" runat="server" /></span></h2>
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
                            <div class="Basic" style="margin:0;min-height:474px">
                                <div style="text-align:right">
                                    <asp:ImageButton CssClass="Icon" runat="server" ImageUrl="~/Images/Add.png" OnClick="AddUpdate_Click" />
                                </div>
                                <div class="gallery js-flickity" data-flickity-options='{ "pageDots": false, "initialIndex": <%=TimelineIndex %> }'>

                                    <!-- Timeline carousel repeater. -->
                                    <asp:Repeater ID="RepeaterTimeline" ItemType="Project_Creator.Timeline" runat="server">
                                        <ItemTemplate>
                                            <div class="gallery-cell">
                                                <a style="text-decoration:none;" href="Updates/Edit?p=<%#ProjectID %>&u=<%#Item.timelineID %>">
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
                            <div class="Basic" style="margin:0">
                                <table>
                                    <tr>
                                        <td style="text-align:right;padding-left:12px">
                                            <asp:Button runat="server" Text="Save Changes" CausesValidation="true" OnClick="Save_Click" />
                                        </td>
                                        <td style="text-align:left;padding-left:12px">
                                            <asp:Button runat="server" Text="Delete Project" CausesValidation="true" OnClick="Delete_Click" />
                                        </td>
                                        <td style="text-align:left;padding-left:12px">
                                            <asp:RadioButton runat="server" id="RadioPrivate" GroupName="RadioVisibility" Text="Private" Checked="true" />
                                        </td>
                                        <td style="text-align:left;padding-left:12px">
                                            <asp:RadioButton runat="server" id="RadioPublic" GroupName="RadioVisibility" Text="Public" />
                                        </td>
                                    </tr>
                                </table>
                            </div>
                        </div>
                    </div>
                </div>
            </asp:TableCell>
        </asp:TableRow>
    </asp:Table>
</asp:Content>