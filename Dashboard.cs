using System;
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
    public partial class Dashboard : Form
    {
        public Dashboard()
        {
            InitializeComponent();
        }

        private void btnAddEmp_Click(object sender, EventArgs e)
        {
            this.Hide();
            RegEmp regEmp = new RegEmp();
            regEmp.Show(); 
        }

        private void btnManage_Click(object sender, EventArgs e)
        {
            this.Hide();
            ManageEmployees manageEmployees = new ManageEmployees();
            manageEmployees.Show();
        }

        private void btnLeaveReq_Click(object sender, EventArgs e)
        {
            this.Hide();
            ApproveReject approveReject = new ApproveReject();
            approveReject.Show();
        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void btnView_Click(object sender, EventArgs e)
        {
            this.Hide();
            View_Reports view_Reports = new View_Reports();
            view_Reports.Show();
        }
    }
}
