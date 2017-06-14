<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Edit App
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <form id="form1" runat="server">

    <h2>Edit App - Name</h2>
    <hr />
    <asp:Panel ID="Panel1" runat="server">
        <asp:Label ID="StatusLabel" runat="server" Text="Status: "></asp:Label>
        <asp:Label ID="Label1" runat="server" Text="Label"></asp:Label>
        <br />
        <asp:Label ID="IndustryLabel" runat="server" Text="Industry: "></asp:Label>
        <asp:Label ID="Label2" runat="server" Text="Label"></asp:Label>
        <br />
        <asp:Label ID="ChangedLabel" runat="server" Text="Changed:"></asp:Label>
        <asp:Label ID="Label3" runat="server" Text="Label"></asp:Label>
        <br />
        <br />
        <asp:Button ID="RemoveAppButton" runat="server" Text="Remove App" />
        <asp:Button ID="ResetDataButton" runat="server" Text="Reset Data" />
        <asp:Button ID="CancelButton" runat="server" Text="Cancel" />
    </asp:Panel>

  

    </form>
</asp:Content>
