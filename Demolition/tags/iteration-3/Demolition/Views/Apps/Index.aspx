<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Apps
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <form id="form1" runat="server">
    <h2>Apps</h2>
    <ul>
          <% foreach (Demolition.Models.App m in (IEnumerable)ViewData.Model)
          { %>
               <li> <%= m.Name %> </li>
          <% } %>
     </ul>
     
     <%= Html.ActionLink("Create New", "Create", "Apps") %>
     </form>

</asp:Content>
