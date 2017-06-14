<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<IEnumerable<Demolition.Models.Demo>>" %>

<asp:Content ContentPlaceHolderID="TitleContent" runat="server">
  All Running Demos
</asp:Content>

<asp:Content ContentPlaceHolderID="LinkContent" runat="server">
  <%= Html.ActionLink("Run New Demo", "Create", "Demos") %>
  <% if(Page.User.IsInRole("Administrator")) { %>
    <%= Html.ActionLink("All Applications", "Index", "Apps") %>
    <%= Html.ActionLink("Add New Industry", "Create", "Industries") %>
    <%= Html.ActionLink("EC2 Monitor", "Index", "Monitor") %>
  <% } %>
</asp:Content>

<asp:Content ContentPlaceHolderID="MainContent" runat="server">
<ul>
  <% foreach (Demolition.Models.Demo demo in Model) { %>
    <% Html.RenderPartial("Demo", demo); %>
  <% } %>
</ul>
</asp:Content>