<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<Demolition.Models.LogOnModel>" %>

<asp:Content ContentPlaceHolderID="TitleContent" runat="server">
  Log On
</asp:Content>

<asp:Content ContentPlaceHolderID="MainContent" runat="server">
<% using (Html.BeginForm("LogOn", "Account", FormMethod.Post, new { id = "signin" })) {%>
  <%= Html.ValidationSummary("Login was unsuccessful. Please correct the errors and try again.", new { id = "flash_error" })%>
  <ol>
    <li>
      <%= Html.LabelFor(m => m.UserName) %>
      <%= Html.TextBoxFor(m => m.UserName) %>
      <%= Html.ValidationMessageFor(m => m.UserName) %>
    </li>
    <li>
      <%= Html.LabelFor(m => m.Password) %>
      <%= Html.PasswordFor(m => m.Password) %>
      <%= Html.ValidationMessageFor(m => m.Password) %>
    </li>
    <li>
      <input type="submit" value="Log On" />
      <%= Html.ActionLink("Register New Account", "Register", null, new { @class = "cancel" }) %>
    </li>
  </ol>
<% } %>
</asp:Content>
