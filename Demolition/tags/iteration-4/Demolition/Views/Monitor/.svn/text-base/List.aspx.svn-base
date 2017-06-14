<%@ Page Title="Running Instances" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage" %>
<%@ Import Namespace="Amazon.EC2.Model" %>
<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	
</asp:Content>

   
            
            <asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
            
            
    <form id="form1" runat="server">
    
<br />
    <h2>Current Instances</h2>
    <ul>
          <% foreach (RunningInstance item in (List<RunningInstance>)ViewData["MyList"])
          { %>
          <hr width="50%" size="3" />
               <li>Public DNS Name: <%= item.PublicDnsName %></li>
            <li>Instance ID: <%= item.InstanceId %></li>
            <li>Current state: <%= item.InstanceState.Name %></li>
            <%if(item.InstanceState.Name.Equals("running")) { %>
            
            <%=Html.ActionLink("Delete " + item.InstanceId, "Delete", new {id = item.InstanceId}) %>

            <% } %>
            
          <% } %>
     </ul>
     
     
     
     </form>
     
     

</asp:Content>
  
      


