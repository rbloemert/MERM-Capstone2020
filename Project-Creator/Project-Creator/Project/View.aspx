<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="View.aspx.cs" Inherits="Project_Creator.Projects.View" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceholder" runat="server">
    <asp:Table Width="100%" runat="server">
        <asp:TableRow>
            <asp:TableCell>
                <div class="ProjectTable">
                    <div class="ProjectRow">
                        <div class="ProjectColumn">
                            <div class="Basic">
                                <asp:Image CssClass="Project" runat="server" />
                            </div>
                        </div>
                        <div class="ProjectColumn" style="width:100%;max-width:800px;">
                            <div class="Basic">
                                <h1 style="text-align:left">Sample Project</h1>
                            </div>
                            <div class="Basic" style="height:72px;margin-top:4px;">
                                <h2 class="Project" style="text-align:left">Project Creator: <span style="color:white;">Kupoapo</span></h2>
                                <br />
                                <h2 class="Project" style="text-align:left">Creation Date: <span style="color:white;">2021-02-24</span></h2>
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
                        <div class="ProjectColumn" style="width:100%;max-width:1014px;">
                            <div class="Basic" style="margin-top:0">

                            </div>
                        </div>
                    </div>
                </div>
            </asp:TableCell>
        </asp:TableRow>
    </asp:Table>
    
</asp:Content>
