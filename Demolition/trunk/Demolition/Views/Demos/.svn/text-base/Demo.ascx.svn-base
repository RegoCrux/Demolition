<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Demolition.Models.Demo>" %>

<li>
  <a href="<%= Url.RouteUrl(new { controller = "Demos", action = "Details", id = Model.Id }) %>">
    <h3><%= Model.Name %></h3>
    <span class="user"><%= Model.User.Name %></span>
    <span class="<%= Model.StatusColor() %>"><%= Model.State %></span>
  </a>
</li>