<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Edit Industry - [Industry Name]
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Edit Industry</h2>
    <hr />
    <asp:Label ID="IndustryNameLabel" runat="server" Text="Name: "></asp:Label>
    <asp:TextBox ID="IndustryTextBox" runat="server"></asp:TextBox>
    <br />
    <asp:Label ID="DataLabel" runat="server" Text="Data: "></asp:Label>
    <asp:TextBox ID="BrowseForData" runat="server" Text="Browse..."></asp:TextBox>
    <br />
    <asp:Label ID="DescriptionLabel" runat="server" Text="Description: "></asp:Label>
    <asp:TextBox ID="DescriptionText" runat="server" Height="68px" MaxLength="1000" 
        Rows="5" Width="243px"></asp:TextBox>
    <br />
    <br />
    <asp:Button ID="SaveChangesButton" runat="server" Text="Save Changes" />
    <asp:Button ID="CancelButton" runat="server" Text="Cancel" />
</asp:Content>
