<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="FilterSearchCrud.aspx.cs" Inherits="WebApp.SamplePages.FilterSearch" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h1>ReviewCrud</h1>
    <div class="row">
        <div class="col-sm-offset-3">
            <asp:Label Text="Select an artist to view album" ID="label1" runat="server" />&nbsp;
            <asp:DropDownList runat="server" ID="ArtistList">                
            </asp:DropDownList>
        </div>
    </div>
</asp:Content>
