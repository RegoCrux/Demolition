<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Industries
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
<form id="form1" runat="server">
    <h2>Industries</h2>
    <ul>
          <% foreach (Demolition.Models.Industry i in (IEnumerable)ViewData.Model)
          { %>
               <li> <%= i.Name %> </li>
          <% } %>
     </ul>
    <asp:Button ID="AddNewIndustryButton" runat="server" Text="Add New Industry" />
    </form>
</asp:Content>
