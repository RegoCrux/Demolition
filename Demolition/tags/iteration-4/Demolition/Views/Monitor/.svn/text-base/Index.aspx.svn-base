<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage" %>
<%@ Import Namespace="System.Collections.Generic" %>
<%@ Import Namespace="Demolition.Controllers" %>
<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Monitor
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>AWS EC2 Monitor Utility </h2>
    <p>Number of EC2 instances running: <%= Html.Encode(ViewData["EC2Instances"]) %></p>
    <p>View Instances running: <%= Html.ActionLink("View Instances", "List","Monitor") %></p>
    <p>Number of simple DB domains running: <%= Html.Encode(ViewData["SimpleDBDomains"])%></p>
    <p>Number of EC2 buckets: <%= Html.Encode(ViewData["EC2Buckets"])%></p>
    
    <!-- any error mesages -->
    <p>Errors:<br /><%= Html.Encode(ViewData["ErrorData"]) %></p>
    <hr />
    <p><%= Html.Encode(ViewData["SOMESTUFF"]) %></p>
   
</asp:Content>
