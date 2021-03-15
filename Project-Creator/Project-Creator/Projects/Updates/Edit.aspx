<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Edit.aspx.cs" Inherits="Project_Creator.Projects.Updates.Edit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link rel="stylesheet" href="../StyleSheets/StyleSheetFlickity.css">
    <script src="https://unpkg.com/flickity@2/dist/flickity.pkgd.min.js"></script>
    <script src="JavaScript.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceholder" runat="server">
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
                            <div class="Basic" style="margin: 0;">
                                Update Image:
                                <br />
                                <asp:Image ID="TimelineImage" CssClass="timeline-image" runat="server" />
                                <br />
                                <asp:FileUpload ID="ImageUploader" runat="server" />
                                <asp:Button ID="btnNewImage" runat="server" OnClick="btnNewImage_Click" Text="Upload New Image" />
                            </div>
                            <div class="Basic" style="margin-top: 5px;">
                                Update Description:
                                <p>
                                    <asp:TextBox ID="txtDesc" CssClass="update-edit-textbox" runat="server" onKeyUp="txtDesc_TextChanged()" MaxLength="255" ClientIDMode="Static"></asp:TextBox>
                                    <asp:Label ID="lblDescCounter" runat="server" ClientIDMode="Static">0 of 255</asp:Label>
                                </p>
                            </div>
                            <div class="Basic" style="margin-top: 5px;">
                                Attatched File:
                                <p>
                                    <asp:TextBox ID="txtContent" CssClass="update-edit-textbox" runat="server" Text="Put attatched content here"></asp:TextBox>
                                </p>
                                <asp:FileUpload ID="ContentUploader" runat="server" />
                                <asp:Button ID="btnNewFile" runat="server" OnClick="btnNewFile_Click" Text="Upload New File" />
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
                            <div class="Basic" style="margin: 0; text-align: right">
                                <asp:Button ID="btnCancel" runat="server" OnClick="btnCancel_Click" Text="Cancel" />
                                <asp:Button ID="btnSubmit" runat="server" OnClick="btnSubmit_Click" Text="Submit" />
                            </div>
                        </div>
                    </div>
                </div>
            </asp:TableCell>
        </asp:TableRow>
    </asp:Table>
</asp:Content>
