<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Creator.aspx.cs" Inherits="Project_Creator.Creator" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>



<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder" runat="server">
    <asp:Table Width="100%" runat="server">
        <asp:TableRow>
            <asp:TableCell>
                <div class="CreatorTable">
                    <div class="CreationRow" >
                        <div class="CreatorColumn" style="width:75%; float:left;">
                            <div class="Basic" >
                                <asp:Label ID="CreatorUsernameLabel" runat="server" />
                            </div>
                            <div class="Basic">
                                <asp:Label ID="CreatorDescriptionTextBox" runat="server" ReadOnly="true" wrap="true" style="width: 100%; height: 500px;" />
                            </div>
                            <div class="Basic">
                                <asp:Button ID="CreatorFollowButton" Text="Follow" runat="server" />
                                <asp:Button ID="CreatorContactButton" Text="Contact" runat="server" />
                            </div>
                        </div>
                        <div class="CreatorColumn" style="width:25%; float:right;">
                            <asp:Image ID="CreatorIcon" runat="server" style="margin-left:50px; margin-top:50px; margin-right:50px;"/>
                        </div>
                    </div>

                    <div class="CreationRow">
                        <asp:GridView ID="creatorProjectGrid" autoGenerateColumns="False" DataKeyNames="projectID" GridLines="Horizontal" runat="server"  style="margin-left:auto; margin-right:auto; width:80%; text-align:Center;" OnRowCommand="btnSelectProject_Clicked"> 
                            <Columns>
                                <asp:BoundField DataField="projectID" HeaderText="ID" InsertVisible="false" ReadOnly="true" SortExpression="projectID" HeaderStyle-Width="5%" HeaderStyle-HorizontalAlign="Center" />
                                
                                <asp:BoundField DataField="project_name"     HeaderText="Title"   InsertVisible="False" ReadOnly="true" SortExpression="projectID" HeaderStyle-Width="10%" HeaderStyle-HorizontalAlign="Center" ItemStyle-Width="5%" /> 
                                <asp:BoundField DataField="project_desc"    HeaderText="Description"  InsertVisible="False" ReadOnly="true" SortExpression="projectID" HeaderStyle-Width="10%" HeaderStyle-HorizontalAlign="Center" ItemStyle-Width="5%" /> 
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:Button Text="Select" runat="server" CommandName="Select" CommandArgument="<%# Container.DataItemIndex %>" Width="10%" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>

                    </div>
                </div>
            </asp:TableCell>
        </asp:TableRow>


    </asp:Table>


    
    
    


</asp:Content>
