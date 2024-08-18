using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace Admin
{
    public partial class ManageEmployees : Form
    {
        dbHelper dbHelper = new dbHelper(); // db obj
        DataTable dtEmployees = new DataTable();

         string username;
         string firstName;
         string lastName;
         string joinDate;
         int annualLeave;
         int casualLeave;
         int shortLeave;
         string startTime;
         string endTime;

        public ManageEmployees()
        {
            InitializeComponent();
        }

        private void btnLookup_Click(object sender, EventArgs e)
        {
            username = txtUsername.Text;

            if (username == "")
            {
                MessageBox.Show("Username cannot be empty");
            }
            else
            {
                dtEmployees.Rows.Clear();
                string query = "SELECT * FROM Employees WHERE Username = '" + username + "'"; // select sql query
                dbHelper.readDatathroughAdapter(query, dtEmployees);  // executing the query using the dbHelper obj and store the result in dtEmployees
                dbHelper.closeConn(); // close the db connection

                if (dtEmployees.Rows.Count == 1)
                {
                    firstName = dtEmployees.Rows[0]["FirstName"].ToString();
                    lastName = dtEmployees.Rows[0]["LastName"].ToString();
                    joinDate = dtEmployees.Rows[0]["DateOfJoining"].ToString();
                    annualLeave = Int32.Parse(dtEmployees.Rows[0]["AnnualLeaveBalance"].ToString());
                    casualLeave = Int32.Parse(dtEmployees.Rows[0]["CasualLeaveBalance"].ToString());
                    shortLeave = Int32.Parse(dtEmployees.Rows[0]["ShortLeaveBalance"].ToString());
                    startTime = dtEmployees.Rows[0]["RosterStartTime"].ToString();
                    endTime = dtEmployees.Rows[0]["RosterEndTime"].ToString();

                    lblFName.Text = firstName;
                    lblLName.Text = lastName;
                    lblDoJ.Text = joinDate;
                    txtAnnual.Text = annualLeave.ToString();
                    txtCasual.Text = casualLeave.ToString();
                    txtShort.Text = shortLeave.ToString();
                    dtpStart.Text = startTime.ToString();
                    dtpEnd.Text = endTime.ToString();
                }
                else
                {
                    MessageBox.Show("Invalid Username");
                }
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            annualLeave = Int32.Parse(txtAnnual.Text);
            casualLeave = Int32.Parse(txtCasual.Text);
            shortLeave = Int32.Parse(txtShort.Text);
            startTime = dtpStart.Text; 
            endTime = dtpEnd.Text;
            username = txtUsername.Text;
   
            SqlCommand sqlCommand = new SqlCommand("UPDATE Employees SET AnnualLeaveBalance = '" + annualLeave + "', CasualLeaveBalance = '" + casualLeave + "', ShortLeaveBalance = '" + shortLeave + "', RosterStartTime = '" + startTime + "', RosterEndTime = '" + endTime + "' WHERE Username = '" + username + "'"); // update sql query
            int row = dbHelper.executeQuery(sqlCommand); // Executing the query

            if (row > 0)
            {
                MessageBox.Show("Employee updated successfully");
            }
            else
            {
                MessageBox.Show("Failled to update the employee");
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
