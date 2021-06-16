using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace QuanLyBanHang.GUI
{
    public partial class Main : Form
    {
        public Main()
        {
            InitializeComponent();
        }

        private void btnlogout_Click(object sender, EventArgs e)
        {
            Visible = false;
            new Login().Visible = true;
        }

        private void btndevice_Click(object sender, EventArgs e)
        {
            Visible = false;
            new Device().Visible = true;
        }

        private void btncombo_Click(object sender, EventArgs e)
        {
            Visible = false;
            new Combo().Visible = true;
        }

        private void btnbilloflading_Click(object sender, EventArgs e)
        {
            Visible = false;
            new ListDelivery().Visible = true;
        }

        private void btnemployee_Click(object sender, EventArgs e)
        {
            Visible = false;
            new Employee().Visible = true;
        }

        private void btncustomer_Click(object sender, EventArgs e)
        {
            Visible = false;
            new Customer().Visible = true;
        }

        private void btninvoice_Click(object sender, EventArgs e)
        {
            Visible = false;
            new HoaDon().Visible = true;
        }
    }
}
