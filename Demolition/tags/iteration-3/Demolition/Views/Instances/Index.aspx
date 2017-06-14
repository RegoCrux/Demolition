<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Instances
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
<form id="form1" runat="server">
    <h2>Instances IDs</h2>
    <ul>
          <% foreach (Demolition.Models.Instance i in (IEnumerable)ViewData.Model)
          { %>
               <li> <%= i.Id %> </li>
          <% } %>
     </ul>
     </form>

</asp:Content>
