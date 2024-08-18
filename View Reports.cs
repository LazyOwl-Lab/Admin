using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Admin
{
    public partial class View_Reports : Form
    {
        dbHelper dbHelper = new dbHelper(); // db obj
        DataTable dtReport = new DataTable();

        public View_Reports()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string username = txtUsername.Text;
            string dateStart = dtpStart.Text;
            string dateEnd = dtpEnd.Text;
            string query;

            if (cbUNameFilter.Checked && cbDateFilter.Checked)
            {
                 query = "SELECT * FROM Leaves " +
        "INNER JOIN Employees ON Leaves.EmployeeID = Employees.EmployeeID " +
        "WHERE Employees.Username = '" + username + "' " +
        "AND (Leaves.StartDate >= '" + dateStart + "' AND Leaves.EndDate <= '" + dateEnd + "')"; // read sql query

            }
            else if (cbUNameFilter.Checked && !cbDateFilter.Checked)
            {
                 query = "SELECT * FROM Leaves " +
        "INNER JOIN Employees ON Leaves.EmployeeID = Employees.EmployeeID " +
        "WHERE Employees.Username = '" + username + "'"; // read sql query

            }
            else
            {
                 query = "SELECT * FROM Leaves"; // read sql query

            }
            dtReport.Rows.Clear();
            dbHelper.readDatathroughAdapter(query, dtReport); // executing the query using the dbHelper obj and store the result in dtReport
            dbHelper.closeConn(); // close the db connection

            dgvReport.DataSource = dtReport;
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            this.Hide();
            Dashboard dashboard = new Dashboard();
            dashboard.Show();
        }
    }
}
