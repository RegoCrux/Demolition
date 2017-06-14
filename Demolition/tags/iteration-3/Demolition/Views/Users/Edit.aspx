<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Edit User - Name
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Edit User</h2>
    <hr />
    <asp:Panel ID="Panel1" runat="server">
        <asp:Label ID="NameLabel" runat="server" Text="Name: "></asp:Label>
        <asp:Label ID="ActualNameLabel" runat="server" Text="[Name]"></asp:Label>
        <br />
        <asp:Label ID="AdminLabel" runat="server" Text="Admin: "></asp:Label>
        <asp:Label ID="ActualAdminLabel" runat="server" Text="[Name]"></asp:Label>
        <br />
        <asp:Label ID="LastLoginLabel" runat="server" Text="Last Login: "></asp:Label>
        <asp:Label ID="ActualLastLogin" runat="server" Text="[Label]"></asp:Label>
        <br />
        <asp:Label ID="RunningDemosLabel" runat="server" Text="Running Demos: "></asp:Label>
        <!-- <% foreach %> -->
        <br />
        <br />
        <asp:Button ID="SaveChangesButton" runat="server" Text="Save Changes" />
        <asp:Button ID="ResetPasswordButton" runat="server" Text="Reset Password" />
        <asp:Button ID="CancelButton" runat="server" Text="Cancel" />
    </asp:Panel>

</asp:Content>
