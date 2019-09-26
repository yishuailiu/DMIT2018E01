<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="DisplayArtistAlbums.aspx.cs" Inherits="WebApp.SamplePages.DisplayArtistAlbums" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <asp:DropDownList ID="ArtistList" runat="server" DataSourceID="ObjectDataSource1" DataTextField="Name" DataValueField="Name"></asp:DropDownList>
    <asp:Button ID="Button1" runat="server" Text="Search" />
    <asp:GridView ID="ArtistAlbumsList" runat="server" AutoGenerateColumns="False" DataSourceID="ArtistAlbumsListODS" PageSize="5" AllowPaging="true">
        <Columns>
            <asp:BoundField DataField="Title" HeaderText="Title" SortExpression="Title"></asp:BoundField>
            <asp:BoundField DataField="ArtistName" HeaderText="ArtistName" SortExpression="ArtistName"></asp:BoundField>
            <asp:BoundField DataField="Year" HeaderText="Year" SortExpression="Year"></asp:BoundField>
            <asp:BoundField DataField="Rlabel" HeaderText="Rlabel" SortExpression="Rlabel"></asp:BoundField>
        </Columns>
    </asp:GridView>
    <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" OldValuesParameterFormatString="original_{0}" SelectMethod="Artist_List" TypeName="ChinookSystem.BLL.ArtistController"></asp:ObjectDataSource>
    <asp:ObjectDataSource ID="ArtistAlbumsListODS" runat="server" OldValuesParameterFormatString="original_{0}" SelectMethod="Album_AlbumsOfArtist" TypeName="ChinookSystem.BLL.AlbumController">
        <SelectParameters>
            <asp:ControlParameter ControlID="ArtistList" PropertyName="SelectedValue" Name="artistname" Type="String"></asp:ControlParameter>
        </SelectParameters>
    </asp:ObjectDataSource>
</asp:Content>
