<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Creator.aspx.cs" Inherits="Project_Creator.Creator" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>



<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder" runat="server">
    <div>
        <asp:Table ID="CreatorTable" runat="server" style="width:90%" border="1"  HorizontalAlign="Center" >
            <asp:TableRow>
                <asp:TableCell>
                    <div>
                        <asp:Label ID="CreatorUsernameLabel" runat="server" ></asp:Label>
                    </div>
                    <div>
                        <asp:Label ID="CreatorDescriptionLabel" runat="server" Width="300px" Height="300px" ></asp:Label>
                    </div>
                    <div>
                        <br />
                        <p>lmao this is text</p>
                    </div>

                </asp:TableCell>
                <asp:TableCell>
                    <asp:Image id="creatorProfilePicture" runat="server" alt="CreatorProfilePicture" />
                </asp:TableCell>
            </asp:TableRow>


        </asp:Table>
        
        <div>
            <div>
                <p>lmao this is text</p>

                
            </div>
        </div>
        <div>
            <br /><br />
            <asp:GridView ID="creatorProjectGrid" autoGenerateColumns="False" DataKeyNames="projectID" GridLines="Horizontal" runat="server"  style="margin-left:auto; margin-right:auto; width:80%; text-align:Center;" OnRowCommand="btnSelectProject_Clicked"> 
                <Columns>
                    <asp:ImageField dataimageurlfield="project_icon_path" HeaderText="Icon"    InsertVisible="False" ReadOnly="true" SortExpression="projectID" HeaderStyle-Width="10%" HeaderStyle-HorizontalAlign="Center" ItemStyle-Width="5%" /> 
                    <asp:BoundField DataField="project_name"     HeaderText="Title"   InsertVisible="False" ReadOnly="true" SortExpression="projectID" HeaderStyle-Width="10%" HeaderStyle-HorizontalAlign="Center" ItemStyle-Width="5%" /> 
                    <asp:BoundField DataField="project_desc"    HeaderText="Description"  InsertVisible="False" ReadOnly="true" SortExpression="projectID" HeaderStyle-Width="10%" HeaderStyle-HorizontalAlign="Center" ItemStyle-Width="5%" /> 
                    <asp:ButtonField buttontype="Button" commandname="Select"   text="Select" InsertVisible="False" SortExpression="projectID" HeaderStyle-Width="10%" HeaderStyle-HorizontalAlign="Center" /> 
                </Columns>
            </asp:GridView>

        
        </div>
    </div>    
    
    


</asp:Content>
