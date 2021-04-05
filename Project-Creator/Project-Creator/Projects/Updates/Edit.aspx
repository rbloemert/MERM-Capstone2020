<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Edit.aspx.cs" Inherits="Project_Creator.Projects.Updates.Edit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link rel="stylesheet" href="/StyleSheets/StyleSheetFlickity.css">
    <script src="https://unpkg.com/flickity@2/dist/flickity.pkgd.min.js"></script>
    <script src="Update_JavaScript.js"></script>
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
    <asp:ScriptManager ID='ScriptManager1' runat='server' EnablePageMethods='true' EnableCdn="true" />
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
                                <div class="timeline-image-container" style="margin-top: 5px;">
                                    <img id="timeline_image" src="<%=TimelineObject.timeline_image_path %>" class="timeline-image" />
                                </div>
                                <div class="Basic" style="margin-top: 5px;">
                                    <input type="file" id="ImageUploader" name="ImageUploader" onchange="previewTimelineImage()" />
                                </div>
                            </div>
                            <div class="Basic" style="text-align: left; margin-top: 5px;">
                                Description:
                                <p>
                                    <asp:TextBox Height="160px" Width="99%" TextMode="MultiLine" Wrap="true" ID="txtDesc" CssClass="update-edit-textbox" runat="server" MaxLength="600"></asp:TextBox>
                                </p>
                            </div>
                            <div class="Basic" style="text-align: left; margin-top: 5px;">
                                Attatched File:
                                <div class="Basic" style="margin-top: 5px;">
                                    <div id="FileImage" runat="server" style="display: none">
                                        <img id="image_upload" src="<%=FileLink %>" class="file-image" />
                                    </div>
                                    <div id="FileVideo" runat="server" style="display: none">
                                        <video id="video_upload" width="960" height="540" src="<%=FileLink %>" controls autoplay>
                                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                            <script>
                                                var video = document.currentScript.parentElement;
                                                video.volume = 0.3;
                                            </script>
                                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
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
                                    <div id="FileZip" runat="server" style="display: none; text-align: left">
                                        <input id="zip_download" type="button" style="margin: 0; float: right"  value="Download" />
                                        <table style="height: 29px">
                                            <tbody>
                                                <tr>
                                                    <td>
                                                        <i class="gg-folder"></i>
                                                    </td>
                                                    <td>
                                                        <p style="padding-left: 12px">Attached Downloadable <%=FileType %></p>
                                                    </td>
                                                </tr>
                                            </tbody>
                                        </table>
                                    </div>
                                </div>
                                <div class="Basic" style="margin-top: 5px;">
                                    <input type="file" id="ContentUploader" name="ContentUploader" onchange="previewFile()" />
                                    <script type="text/javascript">
                                        var btn_listener = null;

                                        document.querySelector('#zip_download').addEventListener('click', btn_listener = () => {
                                            window.location.href = '<%=FileLink %>';
                                        });

                                        function previewFile() {
                                            var preview = null;
                                            const file = document.getElementById('ContentUploader').files[0];
                                            const reader = new FileReader();

                                            reader.addEventListener("load", function () {
                                                // convert image file to base64 string
                                                preview.src = reader.result;
                                            }, false);

                                            if (file) {
                                                document.getElementById("<%=FileImage.ClientID %>").style.display = "none";
                                                document.getElementById("<%=FileVideo.ClientID %>").style.display = "none";
                                                document.getElementById("<%=FilePDF.ClientID %>").style.display = "none";
                                                document.getElementById("<%=FileText.ClientID %>").style.display = "none";
                                                document.getElementById("<%=FileZip.ClientID %>").style.display = "none";
                                                var complex = false;
                                                switch (file.type) {
                                                    case ("image/jpeg"):
                                                    case ("image/png"):
                                                    case ("image/bmp"):
                                                        document.getElementById("<%=FileImage.ClientID %>").style.display = "block";
                                                        preview = document.getElementById("image_upload");
                                                        break;
                                                    case ("application/pdf"):
                                                        document.getElementById("<%=FilePDF.ClientID %>").style.display = "block";
                                                        preview = document.getElementById("viewer");
                                                        reader.readAsDataURL(file);
                                                        document.getElementById("wrapper").data = reader.result;
                                                        complex = true;
                                                        break;
                                                    case ("video/mp4"):
                                                        document.getElementById("<%=FileVideo.ClientID %>").style.display = "block";
                                                        preview = document.getElementById("video_upload");
                                                        break;
                                                    case ("text/plain"):
                                                        document.getElementById("<%=FileText.ClientID %>").style.display = "block";
                                                        preview = document.getElementById("PDF_upload");
                                                        break;
                                                    case ("application/x-zip-compressed"):
                                                        document.getElementById("<%=FileZip.ClientID %>").style.display = "block";
                                                        preview = document.createElement("BUTTON");
                                                        reader.readAsDataURL(file);
                                                        document.querySelector('#zip_download').removeEventListener("click", btn_listener);
                                                        document.querySelector('#zip_download').addEventListener('click', btn_listener = () => {
                                                            downloadToFile(reader.result, 'upload.zip', 'application/x-zip-compressed');
                                                        });
                                                        complex = true;
                                                        break;
                                                }

                                                if (complex == false) {
                                                    reader.readAsDataURL(file);
                                                }

                                            }
                                        }
                                    </script>
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
                            <div class="Basic" style="margin: 0">
                                <table>
                                    <tr>
                                        <td style="text-align: left; padding-left: 12px">
                                            <asp:Button ID="btnSubmit" runat="server" OnClick="btnSubmit_Click" Text="Save Changes" />
                                        </td>
                                        <td style="text-align: left; padding-left: 12px">
                                            <asp:Button ID="btnDelete" runat="server" OnClick="btnDelete_Click" Text="Delete Update" />
                                        </td>
                                        <td style="text-align: right; padding-left: 12px">
                                            <asp:Button ID="btnCancel" runat="server" OnClick="btnCancel_Click" Text="Cancel" />
                                        </td>
                                        <td style="text-align: left; padding-left: 12px">
                                            <asp:RadioButton runat="server" ID="RadioPublic" GroupName="RadioVisibility" Text="Send Notifications" Checked="true" />
                                        </td>
                                        <td style="text-align: left; padding-left: 12px">
                                            <asp:RadioButton runat="server" ID="RadioPrivate" GroupName="RadioVisibility" Text="Don't Send Notifications" />
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
