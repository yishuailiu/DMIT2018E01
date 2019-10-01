<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ListViewCRUDODS.aspx.cs" Inherits="WebApp.SamplePages.ListViewCRUDODS" %>

<%@ Register Src="~/UserControls/MessageUserControl.ascx" TagPrefix="uc1" TagName="MessageUserControl" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h1>CRUD using ODS</h1>
    <blockquote class="alert alert-info">
        Crud process with ODS
    </blockquote>
    <uc1:MessageUserControl runat="server" id="MessageUserControl" />

    <br />
    <asp:ValidationSummary ID="ValidationSummaryEdit" runat="server" ValidationGroup="EditGroup" HeaderText="Correct the following problems on Edit Record" />
    <asp:ValidationSummary ID="ValidationSummaryInsert" runat="server" ValidationGroup="InsertGroup" HeaderText="Correct the following problems on Insert Record" />
    <br />


    <asp:ListView ID="AlbumList" runat="server" DataSourceID="AlbumListODS" InsertItemPosition="LastItem" DataKeyNames="AlbumId">
        <AlternatingItemTemplate>
            <tr style="background-color: #FFFFFF; color: #284775;">
                <td>
                    <asp:Button runat="server" CommandName="Delete" Text="Delete3" ID="DeleteButton" OnClientClick="return confirm('Are you sure you want to remove?')" />
                    <asp:Button runat="server" CommandName="Edit" Text="Edit" ID="EditButton" />
                </td>
                <td>
                    <asp:Label Text='<%# Eval("AlbumId") %>' runat="server" ID="AlbumIdLabel" Width="50px" Enabled="false" /></td>
                <td>
                    <asp:Label Text='<%# Eval("Title") %>' runat="server" ID="TitleLabel" /></td>
                <td>                    
                    <asp:DropDownList ID="ArtistList" runat="server" Width="300px" Enabled="false" DataSourceID="ArtistListODS" DataTextField="Name" DataValueField="ArtistId" SelectedValue='<%# Eval("ArtistId") %>'></asp:DropDownList>
                    </td>
                <td>
                    <asp:Label Text='<%# Eval("ReleaseYear") %>' runat="server" ID="ReleaseYearLabel"
                        Width="50px" /></td>
                <td>
                    <asp:Label Text='<%# Eval("ReleaseLabel") %>' runat="server" ID="ReleaseLabelLabel" /></td>
            </tr>
        </AlternatingItemTemplate>
        <EditItemTemplate>
            <asp:RegularExpressionValidator ID="RegularExTitleTextBoxE" runat="server" ErrorMessage="Title is limited to 160 characteres" Display="None" ControlToValidate="TitleTextBoxE" ValidationGroup="EditGroup"
             ValidationExpression="^.{1,160}$" ></asp:RegularExpressionValidator>
            <asp:RegularExpressionValidator ID="RegularExReleaseLabelE" runat="server" ErrorMessage="Label is limited to 50 characteres" Display="None" ControlToValidate="ReleaseYearTextBoxE" ValidationGroup="EditGroup"
                ValidationExpression="^.{0,50}$" ></asp:RegularExpressionValidator>
            <%--<asp:RequiredFieldValidator ID="RequiredReleaseYearE" runat="server" ErrorMessage="Year is required" ControlToValidate="ReleaseYearTextBoxE" Display="None" ValidationGroup="EditGroup"></asp:RequiredFieldValidator>--%>
            <%--<asp:RangeValidator ID="RangeReleaseYearTextBoxE" runat="server" Type="Integer" ErrorMessage="Year range is 1950-2050" ControlToValidate="ReleaseYearTextBoxE" Display="None" MaximumValue='<%# DateTime.Today.Year %>' MinimumValue="2010" ValidationGroup="EditGroup"></asp:RangeValidator>--%>
            <asp:RequiredFieldValidator ID="RequiredTitleTextBoxE" runat="server" ErrorMessage="Title is required" ControlToValidate="TitleTextBoxE" Display="None" ValidationGroup="EditGroup"></asp:RequiredFieldValidator>
            <tr style="background-color: #999999; color:red;">
                <td>
                    <asp:Button runat="server" CommandName="Update" Text="Update" ID="UpdateButton" ValidationGroup="EditGroup" />
                    <asp:Button runat="server" CommandName="Cancel" Text="Cancel" ID="CancelButton" />
                </td>
                <td>
                    <asp:TextBox Text='<%# Bind("AlbumId") %>'  Width="50px" Enabled="false" runat="server" ID="AlbumIdTextBox" /></td>
                <td>
                    <asp:TextBox Text='<%# Bind("Title") %>' runat="server" ID="TitleTextBoxE" /></td>
                <td>
                    <asp:DropDownList ID="ArtistList" runat="server" Width="300px" DataSourceID="ArtistListODS" DataTextField="Name" DataValueField="ArtistId" SelectedValue='<%# Bind("ArtistId") %>'></asp:DropDownList>

                <td>
                    <asp:TextBox Text='<%# Bind("ReleaseYear") %>'  Width="50px" runat="server" ID="ReleaseYearTextBoxE" /></td>
                <td>
                    <asp:TextBox Text='<%# Bind("ReleaseLabel") %>' runat="server" ID="ReleaseLabelTextBoxE" /></td>
                
            </tr>
        </EditItemTemplate>
        <EmptyDataTemplate>
            <table runat="server" style="background-color: #FFFFFF; border-collapse: collapse; border-color: #999999; border-style: none; border-width: 1px;">
                <tr>
                    <td>No data was returned.</td>
                </tr>
            </table>
        </EmptyDataTemplate>
        <InsertItemTemplate>
            <%--<asp:RangeValidator ID="RangeReleaseYearTextBoxI" runat="server" Type="Integer" ErrorMessage="Year range is 1951-2049" ControlToValidate="ReleaseYearTextBoxI" Display="None" MaximumValue='<%# DateTime.Today.Year %>' MinimumValue="1950" ValidationGroup="InsertGroup"></asp:RangeValidator>--%>
            <asp:RegularExpressionValidator ID="RegularExReleaseLabelI" runat="server" ErrorMessage="Label is limited to 50 characteres" Display="None" ControlToValidate="ReleaseYearTextBoxI" ValidationGroup="InsertGroup"
                ValidationExpression="^.{0,50}$" ></asp:RegularExpressionValidator>
            <asp:RegularExpressionValidator ID="RegularExTitleTextBoxI" runat="server" ErrorMessage="Title is limited to 160 characteres" Display="None" ControlToValidate="TitleTextBoxI" ValidationGroup="InsertGroup"
                ValidationExpression="^.{1,160}$" ></asp:RegularExpressionValidator>
            <%--<asp:RequiredFieldValidator ID="RequiredFieldReleaseYearTextBoxI" runat="server" ErrorMessage="Year is required" ControlToValidate="ReleaseYearTextBoxI" Display="None" ValidationGroup="InsertGroup"></asp:RequiredFieldValidator>--%>

            <asp:RequiredFieldValidator ID="RequiredTitleTextBoxI" runat="server" ErrorMessage="Title is required" ControlToValidate="TitleTextBoxI" Display="None" ValidationGroup="InsertGroup"></asp:RequiredFieldValidator>
            <tr style="">
                <td>
                    <asp:Button runat="server" CommandName="Insert" Text="Insert" ID="InsertButton" ValidationGroup="InsertGroup"/>
                    <asp:Button runat="server" CommandName="Cancel" Text="Clear" ID="CancelButton" />
                </td>
                <td>
                    <asp:TextBox Text='<%# Bind("AlbumId") %>'  Width="50px" Enabled="false" runat="server" ID="AlbumIdTextBox" /></td>
                <td>
                    <asp:TextBox Text='<%# Bind("Title") %>' runat="server" ID="TitleTextBoxI" /></td>
                <td>
                    <asp:DropDownList ID="ArtistList" runat="server" Width="300px" DataSourceID="ArtistListODS" DataTextField="Name" DataValueField="ArtistId" SelectedValue='<%# Bind("ArtistId") %>'></asp:DropDownList>

                <td>
                    <asp:TextBox Text='<%# Bind("ReleaseYear") %>'  Width="50px" runat="server" ID="ReleaseYearTextBoxI" /></td>
                <td>
                    <asp:TextBox Text='<%# Bind("ReleaseLabel") %>' runat="server" ID="ReleaseLabelTextBoxI" /></td>
                
            </tr>
        </InsertItemTemplate>
        <ItemTemplate>
            <tr style="background-color: #E0FFFF; color: #333333;">
                <td>
                    <asp:Button runat="server" CommandName="Delete" Text="Delete1" ID="DeleteButton"  OnClientClick="return confirm('Are you sure you want to remove?')"/>
                    <asp:Button runat="server" CommandName="Edit" Text="Edit" ID="EditButton" />
                </td>
                <td>
                    <asp:Label Text='<%# Eval("AlbumId") %>' runat="server" ID="AlbumIdLabel" Width="50px" Enabled="false" /></td>
                <td>
                    <asp:Label Text='<%# Eval("Title") %>' runat="server" ID="TitleLabel" /></td>
                <td>
                    <asp:DropDownList ID="ArtistList" runat="server" Width="300px" Enabled="false" DataSourceID="ArtistListODS" DataTextField="Name" DataValueField="ArtistId" SelectedValue='<%# Eval("ArtistId") %>'></asp:DropDownList>

                <td>
                    <asp:Label Text='<%# Eval("ReleaseYear") %>'  Width="50px" runat="server" ID="ReleaseYearLabel" /></td>
                <td>
                    <asp:Label Text='<%# Eval("ReleaseLabel") %>' runat="server" ID="ReleaseLabelLabel" /></td>
                
            </tr>
        </ItemTemplate>
        <LayoutTemplate>
            <table runat="server">
                <tr runat="server">
                    <td runat="server">
                        <table runat="server" id="itemPlaceholderContainer" style="background-color: #FFFFFF; border-collapse: collapse; border-color: #999999; border-style: none; border-width: 1px; font-family: Verdana, Arial, Helvetica, sans-serif;" border="1">
                            <tr runat="server" style="background-color: #E0FFFF; color: #333333;">
                                <th runat="server"></th>
                                <th runat="server">Id</th>
                                <th runat="server">Title</th>
                                <th runat="server">Artist</th>
                                <th runat="server">Year</th>
                                <th runat="server">Label</th>                                
                            </tr>
                            <tr runat="server" id="itemPlaceholder"></tr>
                        </table>
                    </td>
                </tr>
                <tr runat="server">
                    <td runat="server" style="text-align: center; background-color: #5D7B9D; font-family: Verdana, Arial, Helvetica, sans-serif; color: #333333" >
                        <asp:DataPager runat="server" ID="DataPager1">
                            <Fields >
                                <asp:NextPreviousPagerField ButtonCssClass="btn btn-info"  ButtonType="Button"  ShowFirstPageButton="True" ShowLastPageButton="True"></asp:NextPreviousPagerField>
                            </Fields>
                        </asp:DataPager>
                    </td>
                </tr>
            </table>
        </LayoutTemplate>
        <SelectedItemTemplate>
            <tr style="background-color: #E2DED6; font-weight: bold; color: #333333;">
                <td>
                    <asp:Button runat="server" CommandName="Delete" Text="Delete2" ID="DeleteButton" OnClientClick="return confirm('Are you sure you want to remove?')" />
                    <asp:Button runat="server" CommandName="Edit" Text="Edit" ID="EditButton" />
                </td>
                <td>
                    <asp:Label Text='<%# Eval("AlbumId") %>'  Width="50px" Enabled="false" runat="server" ID="AlbumIdLabel" /></td>
                <td>
                    <asp:Label Text='<%# Eval("Title") %>' runat="server" ID="TitleLabel" /></td>
                <td>
                    <asp:DropDownList ID="ArtistList" runat="server" Width="300px" Enabled="false" DataSourceID="ArtistListODS" DataTextField="Name" DataValueField="ArtistId" SelectedValue='<%# Eval("ArtistId") %>'></asp:DropDownList>

                </td>
                <td>
                    <asp:Label Text='<%# Eval("ReleaseYear") %>'  Width="50px" runat="server" ID="ReleaseYearLabel" /></td>
                <td>
                    <asp:Label Text='<%# Eval("ReleaseLabel") %>' runat="server" ID="ReleaseLabelLabel" /></td>                
            </tr>
        </SelectedItemTemplate>
    </asp:ListView>
    <asp:ObjectDataSource ID="AlbumListODS" runat="server" 
        DataObjectTypeName="ChinookSystem.Data.Entities.Album" 
        DeleteMethod="Album_Delete" 
        InsertMethod="Album_Add" 
        OldValuesParameterFormatString="original_{0}" 
        SelectMethod="Album_List" 
        TypeName="ChinookSystem.BLL.AlbumController" 
        UpdateMethod="Album_Update"
        
        OnDeleted="CheckForException"
        OnInserted="CheckForException"
        OnUpdated="CheckForException"
        OnSelected="CheckForException">
        
    </asp:ObjectDataSource>
    <asp:ObjectDataSource ID="ArtistListODS" runat="server" OldValuesParameterFormatString="original_{0}"
        SelectMethod="Artist_List" TypeName="ChinookSystem.BLL.ArtistController" OnSelected="CheckForException"></asp:ObjectDataSource>
</asp:Content>
