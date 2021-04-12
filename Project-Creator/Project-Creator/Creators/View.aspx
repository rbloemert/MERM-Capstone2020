<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="View.aspx.cs" Inherits="Project_Creator.Creators.View" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder" runat="server">
    <asp:Table Width="100%" runat="server">
        <asp:TableRow>
            <asp:TableCell>
                <div class="ProjectTable">
                    <div class="ProjectRow">
                        <div class="ProjectColumn">
                            <div class="Basic">
                                <asp:Image ID="CreatorIcon" Width="80" Height="80" CssClass="Project" runat="server" />
                            </div>
                        </div>
                        <div class="ProjectColumn" style="width: 100%;max-width: 900px;">
                            <div class="Basic" style="padding-top:12px;height:92px;">
                                <table style="margin:0;width:100%;">
                                    <tr>
                                        <td>
                                            <div style="float:right" id="ButtonEdit" runat="server">
                                                <a style="display:flex;width:32px;height:32px;align-items:center;justify-content:center" href="/Creators/Edit">
                                                    <i style="color:gray;" class="gg-pen">

                                                    </i>
                                                </a>
                                            </div>
                                            <table>
                                                <tbody>
                                                    <tr>
                                                        <td style="text-align:left;">
                                                            <table>
                                                                <tbody>
                                                                    <tr>
                                                                        <td>
                                                                            <h1 style="text-align: left"><asp:Label ID="lblUsername" runat="server" /></h1>
                                                                        </td>
                                                                        <td>
                                                                            <h2 style="margin-left:12px;text-align: left"><asp:Label ForeColor="Gray" ID="lblFullname" runat="server" /></h2>
                                                                        </td>
                                                                    </tr>
                                                                </tbody>
                                                            </table>
                                                            <asp:Label ID="lblDescription" Font-Size="22px" runat="server">Hello</asp:Label>
                                                        </td>
                                                    </tr>
                                                </tbody>
                                            </table>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <table>
                                                <tbody>
                                                    <tr>
                                                        <td id="divEmail" style="text-align:left;" runat="server">
                                                            <h2 class="Project" style="text-align:left;margin-right:12px;">Email: <a><span style="color: white;">
                                                            <asp:Label ID="lblEmail" runat="server" /></span></a></h2>
                                                        </td>
                                                        <td style="text-align:left;">
                                                            <h2 class="Project" style="text-align:left">Creation Date: <span style="color: white;">
                                                            <asp:Label ID="lblDate" runat="server" /></span></h2>
                                                        </td>
                                                    </tr>
                                                </tbody>
                                            </table>
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
                <div id="SearchOptions" class="Basic" style="text-align:left;margin-top:4px;padding:8px;margin-left:auto;margin-right:auto;width:95%;max-width:1014px;">
                    <asp:Button Text="Search" CssClass="ButtonSearch" runat="server" OnClick="Search_Click"/>
                    <table style="width:80%">
                        <tbody>
                            <tr>
                                <td style="width:20%;">
                                    <p style="text-overflow:ellipsis;text-wrap:none;white-space:nowrap"><b>Advanced Search:</b></p>
                                </td>
                                <td style="width:70%">
                                    <asp:TextBox ID="SearchBox" Width="100%" runat="server"></asp:TextBox>
                                </td>
                                <td style="width:10%;padding-left:12px">
                                    <asp:DropDownList ID="DropDownSort" runat="server">
                                        <asp:ListItem Value="1">Newest</asp:ListItem>
                                        <asp:ListItem Value="2">Oldest</asp:ListItem>
                                        <asp:ListItem Selected="True" Value="3">Most Popular</asp:ListItem>
                                        <asp:ListItem Value="4">Least Popular</asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                            </tr>
                        </tbody>
                    </table>
                </div>
            </asp:TableCell>
        </asp:TableRow>
        <asp:TableRow>
            <asp:TableCell>
                <div id="ButtonAddProject" style="text-align:left;padding:8px;margin-left:auto;margin-right:auto;width:95%;max-width:1014px;" runat="server">
                    <div style="text-align:right">
                        <asp:ImageButton CssClass="Icon" runat="server" ImageUrl="~/Images/Add.png" OnClick="AddProject_Click" />
                    </div>
                </div>
            </asp:TableCell>
        </asp:TableRow>
        <asp:TableRow>
            <asp:TableCell>
                <asp:Repeater ID="RepeaterProject" ItemType="Project_Creator.Project" runat="server">
                    <ItemTemplate>
                        <div class="Basic Browse" style="margin-top:0;margin-bottom:20px;text-align:left;padding:8px;margin-left:auto;margin-right:auto;width:95%;max-width:1014px;">
                            <a href="/Projects/View?p=<%#:Item.projectID%>">
                                <asp:Table runat="server" Width="100%">
                                    <asp:TableRow>
                                        <asp:TableCell Width="160px">
                                            <asp:Image ID="ImageProject" CssClass="Project" ImageUrl="<%#:Item.project_image_path %>" runat="server" />
                                        </asp:TableCell>
                                        <asp:TableCell>
                                            <table style="width:100%;padding-left:12px">
                                                <tr>
                                                    <td>
                                                        <h1 style="text-align:left"><%#:Item.project_name %></h1>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <h2 style="text-align:left"><%#:Item.project_desc %></h2>
                                                        <hr style="margin-top:8px;" />
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <table>
                                                            <tr>
                                                                <td>
                                                                    <div style="text-align:left">
                                                                        <span style="color:dimgray">Creator: </span><span style="color:white"><%#:Item.project_author %></span>
                                                                    </div>
                                                                </td>
                                                                <td>
                                                                    <div style="margin-left:12px;text-align:left">
                                                                        <span style="color:dimgray">Created: </span><span style="color:white"><%#:Item.project_creation.Value.ToString("yyyy-MM-dd") %></span>
                                                                    </div>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                            </table>
                                        </asp:TableCell>
                                    </asp:TableRow>
                                </asp:Table>
                            </a>
                        </div>
                    </ItemTemplate>
                </asp:Repeater>
            </asp:TableCell>
        </asp:TableRow>
    </asp:Table>
</asp:Content>
