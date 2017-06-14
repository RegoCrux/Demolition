<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="HRO._Default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title></title>
</head>

<body>
    <form id="form1" runat="server">
		<table style="width: 100%;">
		<tr>
		<td><asp:Label runat="server" Text="First Name"></asp:Label>  </td>
		<td><asp:TextBox runat="server" ID="firstName"></asp:TextBox> </td>
		</tr>
		<tr>
		<td><asp:Label runat="server" Text="Last Name"></asp:Label></td>
		<td><asp:TextBox runat="server" id="lastName"></asp:TextBox></td>
		</tr>
			<tr>
	<td></td>
		<td><asp:Button runat="server" Text="Get Payroll Information" ID="payrollButton" onclick="payrollButton_Click"/></td>
	</tr>
	<tr>
		<td> Gross Pay</td>
		<td> <asp:TextBox runat="server" ID="wage"></asp:TextBox> </td>
	</tr>
	<tr>
		<td><asp:Label runat="server" Text="Deductions"></asp:Label></td>
		<td><asp:TextBox runat="server" ID="deductions"></asp:TextBox> </td>
	</tr>
	<tr>
		<td>Job Title</td>
		<td><asp:TextBox runat="server" ID="jobTitle"></asp:TextBox></td>
	</tr>
	<tr>
		<td><asp:Label runat="server" ID="industrySpecifcLabel"></asp:Label></td>
		<td><asp:TextBox runat="server" ID="industrySpecifictextBox"></asp:TextBox></td>
	</tr>
<tr>
<td>
</td>
<td>
<asp:Button runat="server" ID="updateButton" Text="Update HR Info" onclick="updateButton_Click" />
</td>
</tr>
</table>
    </form>
</body>
</html>
