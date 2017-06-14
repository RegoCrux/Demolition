<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<Demolition.Models.Demo>" %>

<asp:Content ContentPlaceHolderID="TitleContent" runat="server">
	Create and Run Demo
</asp:Content>

<asp:Content ContentPlaceHolderID="MainContent" runat="server">
<% using (Html.BeginForm("Create", "Demos", FormMethod.Post, new { id = "create" })) {%>
<ol>
  <li>
    <div class="number">1.</div>
    <div class="copy">The client, company, or person you're giving the demo to.</div>
    <%= Html.LabelFor(model => model.Name) %>
    <%= Html.ValidationMessageFor(model => model.Name) %>
    <%= Html.TextBoxFor(model => model.Name) %>
  </li>
  <li>
    <div class="number">2.</div>
    <div class="copy">Choose the Paychex applications your demo will include.</div>
    <label>Apps to Start</label>
    <%= Html.ValidationMessageFor(model => model.AppsToStart) %>
    <% var i = 0; %>
    <% foreach(var app in Model.AppsToStart) { %>
      <div>
        <%= Html.Hidden(string.Format("AppsToStart[{0}].Key", i), app.Key) %>
        <%= Html.CheckBox(string.Format("AppsToStart[{0}].Value", i), app.Value) %>
        <%= app.Key %>
      </div>
    <% i++; } %>
  </li>
  <li>   
    <div class="number">3.</div>
    <div class="copy">Select which industry your demo is tailored for.</div>
    <label>Industry</label>
    <%= Html.ValidationMessageFor(model => model.IndustrySelected)%>
    <% var j = 0; %>
    <% foreach(var ind in Model.IndustrySelected) { %>
      <div>
        <%= Html.Hidden(string.Format("IndustrySelected[{0}].Key", j), ind.Key) %>
        <%= Html.RadioButton(string.Format("IndustrySelected[{0}].Value", j), ind.Value) %>
        <%= ind.Key %>
      </div>
    <% j++; } %>
  </li>
</ol>
<div>
  <input type="submit" value="Launch Demo" />
  <a href="/" class="cancel">Cancel</a>
</div>
<% } %>
</asp:Content>