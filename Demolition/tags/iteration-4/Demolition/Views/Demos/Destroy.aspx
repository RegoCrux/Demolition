<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<DemoDestroyPresentor>" %>
<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Delete Demo - <%= Model.Demo.Name%>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
<h2>Current Demos</h2>       
      <%= Html.LabelFor(model => model.Demo.Name)%>
      <%= Html.TextBoxFor(model => model.Demo.Name)%>
      <%= Html.ValidationMessageFor(model => model.Demo.Name)%>
      <%=Html.ActionLink("Delete " + Model.Demo.Id, "Delete", new { id = Model.Demo.EC2Id })%>

   
</asp:Content>
