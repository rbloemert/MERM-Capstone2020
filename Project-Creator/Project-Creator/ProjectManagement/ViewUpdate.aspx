<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ViewUpdate.aspx.cs" Inherits="Project_Creator.ProjectManagement.ViewUpdate" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder" runat="server">
    <nav class=".w3-padding-top-64 w3-sidebar w3-hide-medium w3-hide-small " style="width:40%;margin-top:100px">
        <div class="bgimg"></div>
    </nav>
    <div class="w3-main w3-padding-large" style="margin-left:40%;">
        <header class="w3-container w3-center" style="padding:128px 16px" id="home">
            <h1 class="w3-jumbo" id="title"><b>Project Update Title</b></h1>
            <p id="details">Update details go here in paragraph form. We haven't decided what kind of editor we will use yet. although i guess if we save it as fromatted html then it doesn't matter</p>
        </header>
        <div class="w3-panel w3-center">
            <h3>Updates</h3>
            <asp:Panel ID="RelatedPanel" runat="server"></asp:Panel>
        </div>
    </div>
    
</asp:Content>
