using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;

namespace PreviewPayroll {
	public partial class UserDataEntry: System.Web.UI.Page {
		protected void Page_Load( object sender, EventArgs e ) {

		}

		protected void button1_Click( object sender, EventArgs e ) {
			var sc =
	new SqlCommand(
		"INSERT INTO PayrollInfo (LastName,FirstName,GrossPay,Deductions,Taxes) VALUES ('" + lastName.Text + "','" + firstName.Text + "','" + grossPay.Text + "','" +
		deductions.Text + "','"+ taxes.Text+"')", new SqlConnection( "Data Source=ROB-DEV;Initial Catalog=SentinelPrime2;Integrated Security=True" ) );
			sc.Connection.Open();
			sc.ExecuteNonQuery();
		}
		protected void button2_Click( object sender, EventArgs e ) {
			var sc =
new SqlCommand( "SELECT * FROM UserData WHERE FirstName = '" + firstName.Text + "' AND LastName='" + lastName.Text + "'", new SqlConnection( "Data Source=ROB-DEV;Initial Catalog=SentinelPrime;Integrated Security=True" ) );
			sc.Connection.Open();
			var sqlReader = sc.ExecuteReader();
			sqlReader.Read();
			zipcode.Text = sqlReader[ "Zipcode" ].ToString();
			hireDate.Text = sqlReader[ "HireDate" ].ToString();
		}
	}
}
