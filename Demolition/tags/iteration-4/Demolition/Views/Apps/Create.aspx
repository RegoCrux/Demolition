<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<Demolition.Models.App>" %>

<asp:Content ContentPlaceHolderID="TitleContent" runat="server">
  Create New App
</asp:Content>

<asp:Content ContentPlaceHolderID="MainContent" runat="server">
<% using (Html.BeginForm("Create", "Apps", FormMethod.Post, new { enctype = "multipart/form-data" })) {%>
  <ol>
    <li>
      <%= Html.LabelFor(model => model.Name) %>
      <%= Html.TextBoxFor(model => model.Name) %>
      <%= Html.ValidationMessageFor(model => model.Name) %>
    </li>
    <li>
      <%= Html.LabelFor(model => model.Zip) %>
      <input id="Zip" name="Zip" type="file" />
      <%= Html.ValidationMessageFor(model => model.Zip)%>
    </li>
    <li>
      <%= Html.HiddenFor(model => model.Id) %>
      <input type="submit" value="Submit" />
      <a href="/" class="cancel">Cancel</a>
    </li>
  </ol>
<% } %>
</asp:Content>
