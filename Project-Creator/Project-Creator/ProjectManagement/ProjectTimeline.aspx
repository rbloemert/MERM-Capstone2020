<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ProjectTimeline.aspx.cs" Inherits="Project_Creator.ProjectManagement.ProjectTimeline" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder" runat="server">
    <div class="w3-main w3-content w3-padding" style="max-width:1200px;margin-top:100px">
        <table>
            <tr>
                <td><asp:Image ID="projectIcon" runat="server" /> </td>
                <td>
                    <div class="w3-panel">
                        <h1>
                            <asp:Label ID="lblTitle" runat="server" Text="Project Title"></asp:Label>
                            &nbsp;<asp:Label ID="Label2" runat="server" Text=" | "></asp:Label>
                            <asp:Label ID="lblAuthor" runat="server" Text="Author Name"></asp:Label>
                        </h1>
                    </div>
                </td>
            </tr>
        </table>
        <div class="w3-row-padding w3-padding-16 w3-center " >
            <asp:Panel ID="UpdatePanel" runat="server"></asp:Panel>
        </div>
        <div class="w3-panel w3-center">
            <h3>Related Projects</h3>
            <asp:Panel ID="RelatedPanel" runat="server"></asp:Panel>
        </div>
    </div>
</asp:Content>