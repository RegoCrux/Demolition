<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<Demolition.Models.App>" %>

<asp:Content ContentPlaceHolderID="TitleContent" runat="server">
	Edit App - <%= Model.Name %>
</asp:Content>

<asp:Content ContentPlaceHolderID="MainContent" runat="server">
<% using (Html.BeginForm("Edit", "Apps", FormMethod.Post, new { enctype = "multipart/form-data", id = "create" })) {%>
  <% Html.RenderPartial("Form", Model); %>
<% } %>
</asp:Content>