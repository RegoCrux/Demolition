<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<List<Amazon.EC2.Model.RunningInstance>>" %>
<%@ Import Namespace="Demolition" %>

<asp:Content ContentPlaceHolderID="TitleContent" runat="server">
  EC2 Monitor
</asp:Content>

<asp:Content ContentPlaceHolderID="LinkContent" runat="server">
  <%= Html.ActionLink("All Running Demos", "Index", "Demos") %>
</asp:Content>

<asp:Content ContentPlaceHolderID="MainContent" runat="server">
<ul>
  <% foreach (var instance in Model) { %>
  <li>
    <a href="http://<%= instance.PublicDnsName %>">
      <h3><%= instance.InstanceId %></h3>
      <span class="<%= Html.EC2Color(instance) %>"><%= instance.InstanceState.Name %></span>
      <span><%= instance.PublicDnsName %></span>
    </a>
  </li>
  <%} %>
</ul>
</asp:Content>