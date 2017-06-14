<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<Demolition.Models.Industry>" %>

<asp:Content ContentPlaceHolderID="TitleContent" runat="server">
	Industry: <%= Model.Name %>
</asp:Content>

<asp:Content ContentPlaceHolderID="HeadContent" runat="server">
  <link rel="stylesheet" type="text/css" media="screen" href="/Content/stylesheets/jquery-ui-1.7.2.custom.css" />
  <link rel="stylesheet" type="text/css" media="screen" href="/Content/stylesheets/ui.jqgrid.css" />

  <script src="/Content/javascripts/jquery-1.3.2.min.js" type="text/javascript"></script>
  <script src="/Content/javascripts/grid.locale-en.js" type="text/javascript"></script>
  <script src="/Content/javascripts/jquery.jqGrid.min.js" type="text/javascript"></script>
  <script type="text/javascript">
    var currentGrid, currentRow, currentCol;
    $(document).ready(function() {
      $("#submit").click(function() {
        $("#" + currentGrid).saveCell(currentRow, currentCol);
        $(this).attr("disabled", "true").attr("value", "Saving data...");

        var data = {};
        $(".industries table[id*=list]").each(function() {
          data[$(this).attr("id").replace("list_", "")] = $(this).getRowData();
        });

        $.post("/Industries/Edit/<%= Model.Id %>",
          Sys.Serialization.JavaScriptSerializer.serialize(data),
          function(msg) {
            $("#submit").attr("disabled", "").attr("value", "Save Changes");
          }
        );
      });
    });
  </script>
</asp:Content>

<asp:Content ContentPlaceHolderID="MainContent" runat="server">
<div class="industries">
  <input type="button" id="submit" value="Save Changes" />
  <% foreach (Demolition.Models.Table table in Model.Database.Tables) { %>
    <script type="text/javascript">
      $(document).ready(function(){ 
        $("#list_<%= table.Name %>").jqGrid({
          datatype: 'xmlstring',
          datastr: '<%= table %>',
          xmlReader: {
            root: "Table",
            row:  "Row",
            cell: "Column"
          },
          colNames: ['<%= String.Join("', '", table.ColumnNames().ToArray()) %>'],
          colModel: [ 
            <% foreach (var column in table.Schema()) { %>
              {name:'<%= column.Name %>', sortable: false, editable:true, formatter:'<%= column.Formatter %>'}, 
            <% } %>
          ],
          caption: '<%= table.Name %>',
          cellEdit: true,
          cellsubmit: 'clientArray',
          hidegrid: false,
          width: 850,
          toolbar: [true, "top"],
          beforeEditCell: function(rowid, cellname, value, iRow, iCol) {
            currentGrid = $(this).attr("id");
            currentRow  = iRow;
            currentCol  = iCol;
          }
        }); 
        
        $("#t_list_<%= table.Name %>").append("<input type='button' value='Add Row' style='height:20px;font-size:-3'/>")
        $("input", "#t_list_<%= table.Name %>").click(function(){
          $("#list_<%= table.Name %>").addRowData(0, {
            <% foreach (var column in table.ColumnNames()) { %>
              '<%= column %>':' ', 
            <% } %>
          });
        })
      }); 
    </script>
    <table id="list_<%= table.Name %>" cellpadding="0" cellspacing="0"></table> 
  <% } %>
</div>
</asp:Content>