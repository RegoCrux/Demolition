<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<Demolition.Models.Demo>" %>

<asp:Content ContentPlaceHolderID="TitleContent" runat="server">
	Demo - <%= Model.Name %>
</asp:Content>

<asp:Content ContentPlaceHolderID="MainContent" runat="server">
<ul>
  <% foreach (Demolition.Models.Instance instance in Model.Instances) { %>
    <li>
      <a href="/">
        <h3><%= instance.App.Name %></h3>
      </a>
    </li>
  <% } %>
</ul>
<div class="links">
  <%= Html.RouteLink("Demo Mode", new { action = "SingleSignOn", id = Model.Id }) %>
  <%= Html.RouteLink("Shut Down Demo", new { action = "Destroy", id = Model.Id }) %>
</div>
</asp:Content>
