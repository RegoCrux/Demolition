<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<Demolition.Models.Demo>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Create and Run Demo
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
<% using (Html.BeginForm("Create", "Demos")) {%>
  <ol>
    <li>
      <%= Html.LabelFor(model => model.Name) %>
      <%= Html.TextBoxFor(model => model.Name) %>
      <%= Html.ValidationMessageFor(model => model.Name) %>
    </li>
    <li>
      <label>Apps to Start</label>
      <%= Html.ValidationMessageFor(model => model.AppsToStart) %>
      <ol>
        <% for(int i = 0; i < Model.AppsToStart.Count; i++) { %>
          <li>
            <%= Model.AppsToStart[i].Name %>
            <%= Html.Hidden(string.Format("AppsToStart[{0}].Id", i), Model.AppsToStart[i].Id) %>
            <%= Html.Hidden(string.Format("AppsToStart[{0}].Name", i), Model.AppsToStart[i].Name)%>
            <%= Html.CheckBox(string.Format("AppsToStart[{0}].Selected", i), Model.AppsToStart[i].Selected) %>
          </li>
        <% } %>
      </ol>
    </li>
    <li>
      <input type="submit" value="Launch Demo" />
      <a href="/" class="cancel">Cancel</a>
    </li>
  </ol>
<% } %>
</asp:Content>