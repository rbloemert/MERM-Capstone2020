<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ProjectTimeline.aspx.cs" Inherits="Project_Creator.ProjectManagement.ProjectTimeline" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder" runat="server">
    <div class="timeline-body" style="max-width:1200px;">
        <table>
            <tr>
                <td><asp:Image ID="projectIcon" runat="server" /> </td>
                <td>
                    <div class="timeline-info">
                        <h1>
                            <asp:Label ID="lblTitle" runat="server" Text="Project Title"></asp:Label>
                            &nbsp;<asp:Label ID="Label2" runat="server" Text=" | "></asp:Label>
                            <asp:Label ID="lblDesc" runat="server" Text="Small Decription"></asp:Label>
                        </h1>
                    </div>
                </td>
            </tr>
        </table>
        <div class="timeline-updates timeline-centre">
            <asp:Panel ID="UpdatePanel" runat="server"></asp:Panel>
        </div>
        <div class="timeline-related timeline-centre">
            <h3>Related Projects</h3>
            <asp:Panel ID="RelatedPanel" runat="server"></asp:Panel>
        </div>
    </div>
</asp:Content>