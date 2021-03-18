<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Creator.aspx.cs" Inherits="Project_Creator.Creator" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>



<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder" runat="server">
    <asp:Table Width="100%" runat="server">
        <asp:TableRow>
            <asp:TableCell>
                <div class="CreatorTable">
                    <div class="CreationRow" style="margin-left:50px;">
                        <div class="CreatorColumn" style="width:75%; float:left;">
                            <div class="Basic" >
                                <asp:Label ID="CreatorUsernameLabel" style="h1" runat="server" />
                                <br/><br/><br/>
                                <asp:Label ID="CreatorDescriptionTextBox" runat="server" ReadOnly="true" wrap="true" style="width: 100%; height: 500px;" />
                                <br/><br/>
                                <asp:Button ID="CreatorFollowButton" Text="Follow" runat="server" />
                                <asp:Button ID="CreatorContactButton" Text="Contact" runat="server" />
                            </div>
                        </div>
                        <div class="CreatorColumn" style="width:25%; float:right;">
                            <asp:Image ID="CreatorIcon" runat="server" ImageAlign="Right" Width="80%" Height="90%" style="margin-left:50px; margin-top:25px; margin-right:50px; "/>
                        </div>
                    </div>

                </div>
            </asp:TableCell>
            <asp:TableCell runat="server">
                <div class="Basic" style="margin-top: 0">
                        <div class="related-panel">
                            <asp:Repeater ID="RepeaterRelated" ItemType="Project_Creator.Project" runat="server">
                                <ItemTemplate>
                                    <div class="timeline-content">
                                        <div class="Basic" style="width: 90%; height: 250px; margin: 0; padding: 8px;">
                                            <table>
                                                <tr>
                                                    <td>
                                                        <a href="View?p=<%#Item.projectID %>">
                                                            <asp:Image CssClass="related-img" ImageUrl="<%#Item.project_image_path %>" runat="server" />
                                                        </a>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <h2><%#Item.project_name %></h2>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <hr />
                                                        <p><%#Item.project_desc %></p>
                                                        <hr />
                                                    </td>
                                                </tr>
                                            </table>
                                        </div>
                                    </div>
                                </ItemTemplate>
                            </asp:Repeater>
                        </div>
                    </div>
            </asp:TableCell>
        </asp:TableRow>


    </asp:Table>


    
    
    


</asp:Content>
