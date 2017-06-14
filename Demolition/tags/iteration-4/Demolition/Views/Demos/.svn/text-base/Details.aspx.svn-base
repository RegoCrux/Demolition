<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<Demolition.Models.Demo>" %>

<asp:Content ContentPlaceHolderID="TitleContent" runat="server">
  Demo - <%= Model.Name %>
</asp:Content>

<asp:Content ContentPlaceHolderID="HeadContent" runat="server">
  <script type="text/javascript">
    function deleteComplete() {
      window.location = "/Demos/Index";
    }
  </script>
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
  <% if (Model.IsReady()) { %>
  <%= Html.RouteLink("Demo Mode", new { action = "SingleSignOn", id = Model.Id }) %>
  <%= Ajax.RouteLink("Shut Down Demo", new { action = "Destroy", id = Model.Id }, new AjaxOptions
      {
         Confirm = "Are you sure you want to shut down this demo?",
         OnComplete = "deleteComplete",
         HttpMethod = "DELETE"
      })%>
  <% } %>
</div>
</asp:Content>
