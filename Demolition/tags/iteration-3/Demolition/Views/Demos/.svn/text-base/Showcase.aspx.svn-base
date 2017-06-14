<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<Demolition.Models.Demo>" %>

<asp:Content ContentPlaceHolderID="TitleContent" runat="server">
	<%= Model.Name %>
</asp:Content>

<asp:Content ContentPlaceHolderID="MainContent" runat="server">
<div class="demos">
  <% foreach (Demolition.Models.Instance instance in Model.Instances) { %>
    <a href="<%= instance.EC2Url %>">
      <%= instance.App.Name %>
    </a>
  <% } %>
</div>
<div class="links">
  <%= Html.RouteLink("Cancel", new { action = "Details", id = Model.Id }) %>
</div>
</asp:Content>