<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<Demolition.Models.App>" %>

<asp:Content ContentPlaceHolderID="TitleContent" runat="server">
	<%= Html.Encode(Model.Name) %>
</asp:Content>

<asp:Content ContentPlaceHolderID="LinkContent" runat="server">
  <%= Html.RouteLink("Edit Application", new { action = "Edit", id = Model.Id }) %>
  <%= Html.ActionLink("All Applications", "Index", "Demos") %>
</asp:Content>

<asp:Content ContentPlaceHolderID="MainContent" runat="server">
<div class="app">
  <p>
    <strong>Last Updated:</strong><%= Model.UpdatedAt.ToShortDateString() %>
  </p>
  <p>
    <strong>Created On:</strong><%= Model.CreatedAt.ToShortDateString() %>
  </p>
  <p>
    <strong>Description</strong><%= Html.Encode(Model.Description) %>
  </p>
  <h3>Demos</h3>
  <ul>
    <% foreach(Demolition.Models.Demo demo in Model.Instances.Select(i => i.Demo)) { %>
      <% Html.RenderPartial("~/Views/Demos/Demo.ascx", demo); %>
    <% } %>
  </ul>
</div>
</asp:Content>
