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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace Admin
{
    public partial class ApproveReject : Form
    {
        dbHelper dbHelper = new dbHelper(); // db obj
        static DataTable dtLeaves = new DataTable();
        static DataTable dtEmployees = new DataTable();
        static string userName;
        public ApproveReject()
        {
            InitializeComponent();
            btnApprove.Enabled = false;
            btnReject.Enabled = false;
        }

        private void btnLookup_Click(object sender, EventArgs e)
        {
            dtLeaves.Rows.Clear();
            userName = txtUsername.Text;
            string query = "SELECT * FROM Employees WHERE Username = '" + userName + "'"; // select sql query
            dbHelper.readDatathroughAdapter(query, dtEmployees);  // executing the query using the dbHelper obj and store the result in dtEmployees
            dbHelper.closeConn(); // close the db connection
            query = "SELECT * FROM Leaves WHERE EmployeeID = '" + dtEmployees.Rows[0]["EmployeeID"].ToString() + "'"; // read sql query
            dbHelper.readDatathroughAdapter(query, dtLeaves); // executing the query using the dbHelper obj and store the result in dtLeaves
            dbHelper.closeConn(); // close the db connection

            dgvLeaves.DataSource = dtLeaves;
        }

        private void dgvLeaves_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                lblStatus.Text = dgvLeaves.SelectedRows[0].Cells["LeaveStatus"].Value.ToString();
                if (lblStatus.Text == "Pending"){
                    btnApprove.Enabled = true;
                    btnReject.Enabled = true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnApprove_Click(object sender, EventArgs e)
        {
            string status = "Approve";
            SqlCommand sqlCommand = new SqlCommand("UPDATE Leaves SET LeaveStatus = '" + status + "' WHERE LeaveID = '" + dgvLeaves.SelectedRows[0].Cells["LeaveID"].Value.ToString() + "'"); // update sql query
            int row = dbHelper.executeQuery(sqlCommand); // Executing the query

            if (row > 0)
            {
                string leaveType = dgvLeaves.SelectedRows[0].Cells["LeaveType"].Value.ToString();
                if (leaveType == "Annual")
                {
                    int annualLeaveBalance = Int32.Parse(dtEmployees.Rows[0]["AnnualLeaveBalance"].ToString());
                    sqlCommand = new SqlCommand("UPDATE Employees SET AnnualLeaveBalance = '" + --annualLeaveBalance + "' WHERE Username = '" + userName + "'"); // update sql query
                    row = dbHelper.executeQuery(sqlCommand); // Executing the query
                 }
               else if (leaveType == "Casual")
                {
                    int annualLeaveBalance = Int32.Parse(dtEmployees.Rows[0]["CasualLeaveBalance"].ToString());
                    sqlCommand = new SqlCommand("UPDATE Employees SET CasualLeaveBalance = '" + --annualLeaveBalance + "' WHERE Username = '" + userName + "'"); // update sql query
                    row = dbHelper.executeQuery(sqlCommand); // Executing the query
                }
                else if (leaveType == "Short")
                {
                    int annualLeaveBalance = Int32.Parse(dtEmployees.Rows[0]["ShortLeaveBalance"].ToString());
                    sqlCommand = new SqlCommand("UPDATE Employees SET ShortLeaveBalance = '" + --annualLeaveBalance + "' WHERE Username = '" + userName + "'"); // update sql query
                    row = dbHelper.executeQuery(sqlCommand); // Executing the query
                }
                MessageBox.Show("Leave Approved!");

                dtLeaves.Rows.Clear();
                userName = txtUsername.Text;
                string query = "SELECT * FROM Employees WHERE Username = '" + userName + "'"; // select sql query
                dbHelper.readDatathroughAdapter(query, dtEmployees);  // executing the query using the dbHelper obj and store the result in dtEmployees
                dbHelper.closeConn(); // close the db connection
                query = "SELECT * FROM Leaves WHERE EmployeeID = '" + dtEmployees.Rows[0]["EmployeeID"].ToString() + "'"; // read sql query
                dbHelper.readDatathroughAdapter(query, dtLeaves); // executing the query using the dbHelper obj and store the result in dtLeaves
                dbHelper.closeConn(); // close the db connection

                dgvLeaves.DataSource = dtLeaves;
            }
            else
           
            {
                MessageBox.Show ("Failed to Approve the Leave");
            }
        }

        private void btnReject_Click(object sender, EventArgs e)
        {
            string status = "Reject";
            SqlCommand sqlCommand = new SqlCommand("UPDATE Leaves SET LeaveStatus = '" + status + "' WHERE LeaveID = '" + dgvLeaves.SelectedRows[0].Cells["LeaveID"].Value.ToString() + "'"); // update sql query
            int row = dbHelper.executeQuery(sqlCommand); // Executing the query

            if (row > 0)
                MessageBox.Show("Not Approved!");

            dtLeaves.Rows.Clear();
            userName = txtUsername.Text;
            string query = "SELECT * FROM Employees WHERE Username = '" + userName + "'"; // select sql query
            dbHelper.readDatathroughAdapter(query, dtEmployees);  // executing the query using the dbHelper obj and store the result in dtEmployees
            dbHelper.closeConn(); // close the db connection
            query = "SELECT * FROM Leaves WHERE EmployeeID = '" + dtEmployees.Rows[0]["EmployeeID"].ToString() + "'"; // read sql query
            dbHelper.readDatathroughAdapter(query, dtLeaves); // executing the query using the dbHelper obj and store the result in dtLeaves
            dbHelper.closeConn(); // close the db connection

            dgvLeaves.DataSource = dtLeaves;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            Dashboard dashboard = new Dashboard();
            dashboard.Show();
        }
    }
}
