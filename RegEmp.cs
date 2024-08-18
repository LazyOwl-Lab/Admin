using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Admin
{
    public partial class RegEmp : Form
    {
        dbHelper dbHelper = new dbHelper(); // db obj

        public RegEmp()
        {
            InitializeComponent();
        }

        private void btnInsert_Click(object sender, EventArgs e)
        {
            string firstName = txtFName.Text;
            string lastName = txtLName.Text;
            string userName = txtUserName.Text;
            string password = txtPassword.Text;
            string role = "";
            if (cmbRole.SelectedIndex == 0)
            {
                role = "Employee";
            }
            else if (cmbRole.SelectedIndex == 1){ 
                role = "Admin";
            }
            else { MessageBox.Show("Please select a role"); }
            int annualLeave = 0;
            int casualLeave = 0;
            int shortLeave = 0;
            try
            {
                annualLeave = Int32.Parse(txtAnnualLeave.Text); //converting from text to int
                casualLeave = Int32.Parse(txtCasualLeave.Text);
                shortLeave = Int32.Parse(txtShortlLeave.Text);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            string dateofjoin = dtpJoin.Text;
            string start = dtpStart.Text;
            string end = dtpEnd.Text;

            if (firstName == "")
            {
                MessageBox.Show("First name cannot be Empty");
            }
            else if (lastName == "")
            {
                MessageBox.Show("Lase name cannot be Empty");
            }
            else if (userName == "")
            {
                MessageBox.Show("Username cannot be Empty");
            }
            else if (password == "")
            {
                MessageBox.Show("Password cannot be Empty");
            }
            else if (dateofjoin == "")
            {
                MessageBox.Show("Date of join cannot be Empty");
            }
            else if (start == "")
            {
                MessageBox.Show("Start time cannot be Empty");
            }
            else if (end == "")
            {
                MessageBox.Show("End time cannot be Empty");
            }
            else
            {
                SqlCommand sqlCommand = new SqlCommand("INSERT INTO Employees (FirstName, LastName, Username, PasswordHash, Role, AnnualLeaveBalance, CasualLeaveBalance, ShortLeaveBalance, DateOfJoining, RosterStartTime, RosterEndTime) " +
                   "VALUES ('" + firstName + "', '" + lastName + "', '" + userName + "', '" + password + "', '" + role + "', " +
                   annualLeave + ", " + casualLeave + ", " + shortLeave + ", '" + dateofjoin + "', '" + start + "', '" + end + "')"); // Insert sql qurey
                int row = dbHelper.executeQuery(sqlCommand); // Executing the query

                if (row > 0)
                {
                    MessageBox.Show("Employee registered successfully");
                }
                else
                {
                    MessageBox.Show("Failled to register the employee");
                }
            }
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            this.Hide();
            Dashboard dashboard = new Dashboard();
            dashboard.Show();
        }
    }
}
