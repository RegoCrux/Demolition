<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Demolition.Models.App>" %>

<ol>
  <li>
    <div class="number">1.</div>
    <div class="copy">The name of this application.</div>
    <%= Html.LabelFor(model => model.Name) %>
    <%= Html.ValidationMessageFor(model => model.Name) %>
    <%= Html.TextBoxFor(model => model.Name) %>
  </li>
  <li>
    <div class="number">2.</div>
    <div class="copy">A description of this application version, or some text to designate what it does.</div>
    <%= Html.LabelFor(model => model.Description) %>
    <%= Html.ValidationMessageFor(model => model.Description)%>
    <%= Html.TextAreaFor(model => model.Description)%>
  </li>
  <li>   
    <div class="number">3.</div>
    <div class="copy">A zip file that holds the executables for this application.</div>
    <%= Html.LabelFor(model => Model.Zip) %>
    <input id="Zip" name="Zip" type="file" />
    <%= Html.ValidationMessageFor(model => Model.Zip)%>
  </li>
</ol>
<div>
  <%= Html.HiddenFor(model => model.Id) %>
  <input type="submit" value="Submit" />
  <a href="/" class="cancel">Cancel</a>
</div>