using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DbOps2;


namespace Shilling_GroupProject
{
    public partial class frmNavigation : Form
    {
        public frmNavigation()
        {
            InitializeComponent();
        }

        private void btnLogout_Click(object sender, EventArgs e)
        {
            //exits application
            Application.Exit();
        }

        private void pctFinancial_Click(object sender, EventArgs e)
        {
            this.Hide();
            //opens financial page
            var financial = new frmFinancial();
            financial.ShowDialog();
            this.Show();
        }

        private void pctPhotographers_Click(object sender, EventArgs e)
        {
            //hides form
            this.Hide();
            //opens manage photographers page
            var photographers = new frmManagePhotographers();
            photographers.ShowDialog();
            //shows form
            this.Show();
        }

        private void pctClients_Click(object sender, EventArgs e)
        {
            //hides form
            this.Hide();
            //opens manage clients page
            var clients = new frmManageClients();
            clients.ShowDialog();
            //shows form
            this.Show();
        }

        private void pctPhoto_Click(object sender, EventArgs e)
        {
            //hides form
            this.Hide();
            //opens manage photos page
            var photos = new frmManagePhotos();
            photos.ShowDialog();
            //shows form
            this.Show();
        }
    }
}
