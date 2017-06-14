<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<IEnumerable<Demolition.Models.Demo>>" %>

<asp:Content ContentPlaceHolderID="TitleContent" runat="server">
	All Running Demos
</asp:Content>

<asp:Content ContentPlaceHolderID="MainContent" runat="server">
<ul>
  <% foreach (Demolition.Models.Demo demo in Model) { %>
    <li>
      <a href="<%= Url.RouteUrl(new { action = "Details", id = demo.Id }) %>">
        <h3><%= demo.Name %></h3>
        <span class="user"><%= demo.User.Name%></span>
        <span class="<%=demo.StatusColor() %>"><%= demo.State%></span>
      </a>
    </li>
  <% } %>
</ul>
<div class="links">
  <% if(User.IsInRole("Administrator")) { %>
    <%= Html.ActionLink("Create New App", "Create", "Apps") %>
  <% } %>
  <%= Html.ActionLink("Run New Demo", "Create", "Demos") %>
</div>
</asp:Content>
