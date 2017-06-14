using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace HRO {
	public partial class _Default: System.Web.UI.Page {
		protected void Page_PreRender( object sender, EventArgs e ) {
			var sc = new SqlCommand( "SELECT * FROM Employees", new SqlConnection( ConfigurationManager.ConnectionStrings[ "SentinelPrimeConnectionString" ].ConnectionString ) );
			sc.Connection.Open();
			SqlDataAdapter da = new SqlDataAdapter( sc );
			DataSet ds = new DataSet();

			da.Fill( ds, "Employees" );
			ViewState[ "industrySpecificField" ] = ds.Tables[ 0 ].Columns[ 6 ].ColumnName;
			industrySpecifcLabel.Text = Regex.Replace( ds.Tables[ 0 ].Columns[ 6 ].ColumnName, "(?!^)([A-Z])", " $1" );
		}

		protected void payrollButton_Click( object sender, EventArgs e ) {
			var sc =
	new SqlCommand("SELECT * FROM Employees WHERE FirstName = '"+firstName.Text+"' AND LastName='"+lastName.Text+"'", new SqlConnection(ConfigurationManager.ConnectionStrings["SentinelPrimeConnectionString"].ConnectionString) );
			sc.Connection.Open();
			var sqlReader = sc.ExecuteReader();
			sqlReader.Read();
			wage.Text = sqlReader[ "Wage" ].ToString();
			deductions.Text = sqlReader[ "Deductions" ].ToString();
			jobTitle.Text = sqlReader[ "JobTitle" ].ToString();
			industrySpecifictextBox.Text = sqlReader[ ViewState[ "industrySpecificField" ].ToString() ].ToString();
		}

		protected void updateButton_Click( object sender, EventArgs e ) {
			var sc =
	new SqlCommand( "UPDATE Employees SET Wage='" + wage.Text + "', Deductions='" + deductions.Text + "', JobTitle='" + jobTitle.Text + "', " + ViewState[ "industrySpecificField" ] + "='" + industrySpecifictextBox.Text + "' WHERE FirstName = '" + firstName.Text + "' AND LastName='" + lastName.Text + "'", new SqlConnection( ConfigurationManager.ConnectionStrings[ "SentinelPrimeConnectionString" ].ConnectionString ) );
			sc.Connection.Open();
			sc.ExecuteNonQuery();
		}
	}
}
