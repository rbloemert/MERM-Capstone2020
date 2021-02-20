<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Creator.aspx.cs" Inherits="Project_Creator.Creator" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>



<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder" runat="server">
    <div >
        <div class="split left">
            <div class="centered">
                <br /><br />
                <h1>Creator: [CREATOR NAME]</h1>
        
                <div>
                    <br />
                    <p>lmao this is text</p>
                </div>
            </div>
        </div>

        <div class="split right">
            <div class="centered">
                <p>lmao this is text</p>

                
            </div>
        </div>

        <div>
            <br /><br />
            <asp:GridView ID="creatorProjectGrid" autoGenerateColumns="False" DataKeyNames="id" GridLines="Horizontal" runat="server"  style="margin-left:auto; margin-right:auto; width:80%; text-align:Center;" OnRowCommand="btnSelectProject_Clicked"> 
                <Columns>
                    <asp:ImageField dataimageurlfield="icon" HeaderText="Icon"    InsertVisible="False" ReadOnly="true" SortExpression="id" HeaderStyle-Width="10%" HeaderStyle-HorizontalAlign="Center" ItemStyle-Width="5%" /> 
                    <asp:BoundField DataField="title"     HeaderText="Title"   InsertVisible="False" ReadOnly="true" SortExpression="id" HeaderStyle-Width="10%" HeaderStyle-HorizontalAlign="Center" ItemStyle-Width="5%" /> 
                    <asp:BoundField DataField="author"    HeaderText="Author"  InsertVisible="False" ReadOnly="true" SortExpression="id" HeaderStyle-Width="10%" HeaderStyle-HorizontalAlign="Center" ItemStyle-Width="5%" /> 
                    <asp:ButtonField buttontype="Button" commandname="Select"   text="Select" InsertVisible="False" SortExpression="id" HeaderStyle-Width="10%" HeaderStyle-HorizontalAlign="Center" /> 
                </Columns>
            </asp:GridView>

        
        </div>
    </div>    
    
    


</asp:Content>
