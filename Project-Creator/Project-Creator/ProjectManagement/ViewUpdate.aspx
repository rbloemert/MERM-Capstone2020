<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ViewUpdate.aspx.cs" Inherits="Project_Creator.ProjectManagement.ViewUpdate" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder" runat="server">
    <nav class="update-sidebar-img update-sidebar-hide-medium  update-sidebar-hide-small " style="width:40%">
        <asp:Panel CssClass="bgimg" id="sidebarImage" runat="server"></asp:Panel>
    </nav>
    <div style="margin-left:40%;">
        <br />
        <a href="ProjectTimeline.aspx">&lt-- Back to timeline</a>
        <div class="update-info update-centre" style="padding:128px 16px" id="home">
            <asp:Panel ID="updateData" runat="server"></asp:Panel>
        </div>
        <div class="update-related update-centre">
            <h3>Updates</h3>
            <asp:Panel ID="RelatedPanel" runat="server"></asp:Panel>
        </div>
    </div>
    
</asp:Content>
