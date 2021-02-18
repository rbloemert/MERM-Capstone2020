<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Creator.aspx.cs" Inherits="Project_Creator.Creator" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>



<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder" runat="server">
    <div >
        <h1>Creator</h1>
        <div>
            <asp:GridView ID="creatorProjectGrid" autoGenerateColumns="False" DataKeyNames="id" GridLines="Horizontal" runat="server" > 
                <Columns>
                    <asp:ImageField dataimageurlfield="icon" AlternateText="Icon" HeaderText="Icon"    InsertVisible="False" ReadOnly="true" SortExpression="id" HeaderStyle-Width="10%" HeaderStyle-HorizontalAlign="Left" /> 
                    <asp:BoundField DataField="title"     HeaderText="Title"   InsertVisible="False" ReadOnly="true" SortExpression="id" HeaderStyle-Width="10%" HeaderStyle-HorizontalAlign="Left" /> 
                    <asp:BoundField DataField="author"    HeaderText="Author"  InsertVisible="False" ReadOnly="true" SortExpression="id" HeaderStyle-Width="10%" HeaderStyle-HorizontalAlign="Left" /> 
                    <asp:ButtonField buttontype="Button" commandname="Select"  HeaderText="Select Project" text="Select" InsertVisible="False" SortExpression="id" HeaderStyle-Width="10%" HeaderStyle-HorizontalAlign="Left" /> 
                </Columns>
            </asp:GridView>

        
        </div>
    </div>    
    
    


</asp:Content>
