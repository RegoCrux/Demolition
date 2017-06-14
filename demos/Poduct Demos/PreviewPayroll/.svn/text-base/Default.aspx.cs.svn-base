using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;

namespace PreviewPayroll {
	public partial class UserDataEntry: System.Web.UI.Page {
		protected void Page_Load( object sender, EventArgs e ) {

		}
		protected void Page_PreRender( object sender, EventArgs e ) {
			var sc = new SqlCommand( "SELECT * FROM Employees", new SqlConnection( ConfigurationManager.ConnectionStrings[ "SentinelPrimeConnectionString" ].ConnectionString ) );
			sc.Connection.Open();
			SqlDataAdapter da = new SqlDataAdapter( sc );
			DataSet ds = new DataSet();

			da.Fill( ds, "Employees" );
			ViewState[ "industrySpecificField" ] = ds.Tables[ 0 ].Columns[ 6 ].ColumnName;
			industrySpecficLabel.Text = Regex.Replace( ds.Tables[ 0 ].Columns[ 6 ].ColumnName, "(?!^)([A-Z])", " $1" );
		}

		protected void button1_Click( object sender, EventArgs e ) {

				var sc =
				new SqlCommand(
			"INSERT INTO Employees (LastName,FirstName,Wage,Deductions,JobTitle,Hours," + ViewState[ "industrySpecificField" ] + ") VALUES ('" + lastName.Text + "','" + firstName.Text + "','" + wage.Text + "','" +
			deductions.Text + "','" + jobTitle.Text + "','"+hours.Text+"','"+industrySpecificTextBox.Text+"')", new SqlConnection( ConfigurationManager.ConnectionStrings[ "SentinelPrimeConnectionString" ].ConnectionString ) );
				sc.Connection.Open();
				sc.ExecuteNonQuery();
		}
		protected void button2_Click( object sender, EventArgs e ) {
			try {
				var sc =
	new SqlCommand( "SELECT * FROM Employees WHERE FirstName = '" + firstName.Text + "' AND LastName='" + lastName.Text + "'", new SqlConnection( ConfigurationManager.ConnectionStrings[ "SentinelPrimeConnectionString" ].ConnectionString ) );
				sc.Connection.Open();
				var sqlReader = sc.ExecuteReader();
				sqlReader.Read();
				wage.Text = sqlReader[ "Wage" ].ToString();
				jobTitle.Text = sqlReader[ "JobTitle" ].ToString();
				deductions.Text = sqlReader[ "Deductions" ].ToString();
				hours.Text = sqlReader[ "Hours" ].ToString();
				industrySpecificTextBox.Text = sqlReader[ ViewState[ "industrySpecificField" ].ToString() ].ToString();
			}
			catch( Exception ex ) {
				Response.Write( "That user is not in the system." );
			}
		}
	}
}
