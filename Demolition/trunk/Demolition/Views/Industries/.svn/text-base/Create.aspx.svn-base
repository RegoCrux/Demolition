<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<Demolition.Models.Industry>" %>

<asp:Content ContentPlaceHolderID="TitleContent" runat="server">
	Add New Industry
</asp:Content>

<asp:Content ContentPlaceHolderID="MainContent" runat="server">
<% using (Html.BeginForm("Create", "Industries", FormMethod.Post, new { enctype = "multipart/form-data" })) {%>
  <ol>
    <li>
      <%= Html.LabelFor(model => model.Name) %>
      <%= Html.TextBoxFor(model => model.Name) %>
      <%= Html.ValidationMessageFor(model => model.Name) %>
    </li>
    <li>
      <%= Html.LabelFor(model => model.Description) %>
      <%= Html.TextBoxFor(model => model.Description)%>
      <%= Html.ValidationMessageFor(model => model.Description)%>
    </li>
    <li>
      <%= Html.LabelFor(model => model.Xml) %>
      <input id="Xml" name="Xml" type="file" />
      <%= Html.ValidationMessageFor(model => model.Xml)%>
    </li>
    <li>
      <%= Html.HiddenFor(model => model.Id) %>
      <input type="submit" value="Submit" />
      <a href="/" class="cancel">Cancel</a>
    </li>
  </ol>
<% } %>
</asp:Content>