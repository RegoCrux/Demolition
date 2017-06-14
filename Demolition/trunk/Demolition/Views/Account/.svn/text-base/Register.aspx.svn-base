<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<Demolition.Models.RegisterModel>" %>

<asp:Content ContentPlaceHolderID="TitleContent" runat="server">
  Register
</asp:Content>

<asp:Content ContentPlaceHolderID="MainContent" runat="server">
<% using (Html.BeginForm("Register", "Account", FormMethod.Post, new { id = "signin" })) {%>
  <%= Html.ValidationSummary(true, "Account creation was unsuccessful. Please correct the errors and try again.") %>
  <ol>
    <li>
      <%= Html.LabelFor(m => m.UserName) %>
      <%= Html.TextBoxFor(m => m.UserName) %>
      <%= Html.ValidationMessageFor(m => m.UserName) %>
    </li>
    <li>
      <%= Html.LabelFor(m => m.Email) %>
      <%= Html.TextBoxFor(m => m.Email) %>
      <%= Html.ValidationMessageFor(m => m.Email) %>
    </li>
    <li>
      <%= Html.LabelFor(m => m.Password) %>
      <%= Html.PasswordFor(m => m.Password) %>
      <%= Html.ValidationMessageFor(m => m.Password) %>
    </li>
    <li>
      <%= Html.LabelFor(m => m.ConfirmPassword) %>
      <%= Html.PasswordFor(m => m.ConfirmPassword) %>
      <%= Html.ValidationMessageFor(m => m.ConfirmPassword) %>
    </li>
    <li>
      <input type="submit" value="Register" />
      <a href="/" class="cancel">Cancel</a>
    </li>
  </ol>
<% } %>
</asp:Content>
