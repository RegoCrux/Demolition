<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<IEnumerable<Demolition.Models.App>>" %>

<asp:Content ContentPlaceHolderID="TitleContent" runat="server">
	All Applications
</asp:Content>

<asp:Content ContentPlaceHolderID="LinkContent" runat="server">
  <%= Html.ActionLink("Create New App", "Create", "Apps") %>
  <%= Html.ActionLink("All Demos", "Index", "Demos") %>
</asp:Content>

<asp:Content ContentPlaceHolderID="MainContent" runat="server">
<ul>
  <% foreach (Demolition.Models.App app in Model) { %>
    <li>
      <a href="<%= Url.RouteUrl(new { action = "Details", id = app.Id }) %>">
        <h3><%= app.Name %></h3>
      </a>
    </li>
  <% } %>
</ul>
</asp:Content>
