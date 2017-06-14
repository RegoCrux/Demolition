<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<Demolition.Models.Demo>" %>

<asp:Content ContentPlaceHolderID="TitleContent" runat="server">
	Single Sign On - <%= Model.Name %>
</asp:Content>

<asp:Content ContentPlaceHolderID="MainContent" runat="server">
<% using (Html.BeginForm("Showcase", "Demos", new { id = Model.Id }, FormMethod.Get, new { id = "signin" })) {%>
  <ol>
    <li>
      <%= Html.Label("Username") %>
      <%= Html.TextBox("Username") %>
    </li>
    <li>
      <%= Html.Label("Password")%>
      <%= Html.Password("Password")%>
    </li>
    <li>
      <input type="submit" value="Sign In" />
      <a href="/" class="cancel">Cancel</a>
    </li>
  </ol>
<% } %>
</asp:Content>