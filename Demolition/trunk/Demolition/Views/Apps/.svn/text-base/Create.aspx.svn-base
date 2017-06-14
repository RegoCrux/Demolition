<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<Demolition.Models.App>" %>

<asp:Content ContentPlaceHolderID="TitleContent" runat="server">
  Create New App
</asp:Content>

<asp:Content ContentPlaceHolderID="MainContent" runat="server">
<% using (Html.BeginForm("Create", "Apps", FormMethod.Post, new { enctype = "multipart/form-data", id = "create" })) {%>
  <% Html.RenderPartial("Form", Model); %>
<% } %>
</asp:Content>
