<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Users
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
<form id="form1" runat="server">
    <h2>User List</h2>
    <ul>
          <% foreach (Demolition.Models.User u in (IEnumerable)ViewData.Model)
          { %>
               <li> <%= Html.ActionLink(u.Name, "Edit", "Users") %> <%= u.Role %></li>
               <br />
          <% } %>
     </ul>
    <asp:Button ID="DemoModeButton" runat="server" Text="Demo Mode" />
    <asp:Button ID="AddNewAppButton" runat="server" Text="Add New App" />
    <asp:Button ID="ShutDownDemoButton" runat="server" Text="Shut Down Demo" />
    </form>
</asp:Content>
