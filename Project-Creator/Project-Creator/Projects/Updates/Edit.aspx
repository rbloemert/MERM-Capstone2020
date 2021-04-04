<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Edit.aspx.cs" Inherits="Project_Creator.Projects.Updates.Edit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link rel="stylesheet" href="/StyleSheets/StyleSheetFlickity.css">
    <script src="https://unpkg.com/flickity@2/dist/flickity.pkgd.min.js"></script>
    <script src="JavaScript.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceholder" runat="server">
    <asp:ScriptManager ID="ScriptManager2" runat="server" EnablePageMethods="true"/>
    <asp:Table Width="100%" runat="server">
        <asp:TableRow>
            <asp:TableCell>
                <div class="ProjectTable">
                    <div class="ProjectRow">
                        <div class="ProjectColumn" style="width: 100%; max-width: 1014px;">
                            <div class="Basic" style="height: 72px; margin-top: 4px; text-align: left">
                                <asp:TextBox runat="server" Width="100%" ID="TextBoxUpdate" Font-Size="X-Large" placeholder="Title..."></asp:TextBox>
                                <br />
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
                            <div class="Basic" style="margin: 0;">
                                Timeline Image:
                                    <div class="timeline-image-container-edit" style="margin-top: 5px;">
                                        <asp:Image ID="TimelineImage" CssClass="timeline-image" runat="server" />
                                    </div>
                                    <div class="Basic" style="margin-top: 5px;">
                                        <asp:FileUpload ID="ImageUploader" onchange="FileUpload()" runat="server" />
                                        <asp:Button ID="btnFileSubmit" OnClick="btnNewFile_Click" runat="server" hidden></asp:Button>
                                        <script type="text/javascript">
                                            function FileUpload() {
                                                document.getElementById("<%=btnFileSubmit.ClientID %>").click();
                                            }
                                        </script>
                                    </div>
                                </div>
                            </div>
                            <div class="Basic" style="text-align:left;margin-top: 5px;">
                                Description:
                                <p>
                                    <asp:TextBox Height="160px" Width="99%" TextMode="MultiLine" Wrap="true" ID="txtDesc" CssClass="update-edit-textbox" runat="server" MaxLength="600"></asp:TextBox>
                                </p>
                            </div>
                            <div class="Basic" style="text-align:left;margin-top: 5px;">
                                Attatched File:
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
                                        <p>
                                            <a href="http://docs.google.com/gview?url=<%=FileLink %>">Click here to view the Pdf Document</a>
                                        </p>
                                    </div>
                                    <div id="FileText" runat="server" style="display: none;text-align:left;white-space: pre-wrap;">
                                        <asp:Label ID="FileTextContent" runat="server">

                                        </asp:Label>
                                    </div>
                                </div>
                                <div class="Basic" style="margin-top: 5px;">
                                    <asp:FileUpload ID="ContentUploader" runat="server" />
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
                                        <td style="text-align:left;padding-left:12px">
                                            <asp:Button ID="btnSubmit" runat="server" OnClick="btnSubmit_Click" Text="Save Changes" />
                                        </td>
                                        <td style="text-align:left;padding-left:12px">
                                            <asp:Button ID="btnDelete" runat="server" OnClick="btnDelete_Click" Text="Delete Update" />
                                        </td>
                                        <td style="text-align:right;padding-left:12px">
                                            <asp:Button ID="btnCancel" runat="server" OnClick="btnCancel_Click" Text="Cancel" />
                                        </td>
                                        <td style="text-align:left;padding-left:12px">
                                            <asp:RadioButton runat="server" id="RadioPublic" GroupName="RadioVisibility" Text="Send Notifications" Checked="true" />
                                        </td>
                                        <td style="text-align:left;padding-left:12px">
                                            <asp:RadioButton runat="server" id="RadioPrivate" GroupName="RadioVisibility" Text="Don't Send Notifications" />
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
