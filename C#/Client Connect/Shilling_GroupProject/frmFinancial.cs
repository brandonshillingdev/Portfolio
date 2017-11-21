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
    public partial class frmFinancial : Form
    {
        public frmFinancial()
        {
            InitializeComponent();
        }

        private void frmFinancial_Load(object sender, EventArgs e)
        {
            if (CurrentUser.user.Type == "Admin")
            {
                //fills dgv for admin
                fillDgvAdmin();
                btnAdd.Visible = false;
            }
            else
            {
                //fills dgv for photographer
                fillDgv();
            }
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            //closes form
            this.Close();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (btnAdd.Text == "Report")
            {
                this.Hide();
                //opens the report page
                frmReport report = new frmReport();
                report.ShowDialog();
                this.Show();
            }
            else
            {
                //opens addfinancial form
                var add = new frmAddFinancial();
                this.Hide();
                add.ShowDialog();
                this.Show();
                //refreshes dgv
                dgvClients.DataSource = null;
                fillDgv();
            }

        }

        private void dgvClients_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            frmAddFinancial edit = new frmAddFinancial();
            this.Hide();
            //sets to type of form to edit
            edit.type = "edit";
            //sets id to selected client id
            edit.id = int.Parse(dgvClients.CurrentRow.Cells["Id"].Value.ToString());
            edit.clientid = int.Parse(dgvClients.CurrentRow.Cells["ClientId"].Value.ToString());
            edit.ShowDialog();
            this.Show();
            //refreshes dgv
            dgvClients.DataSource = null;
            if (CurrentUser.user.Type == "Admin")
            {
                //fills dgv for admin
                fillDgvAdmin();
                btnAdd.Text = "Report";
            }
            else
            {
                //fills dgv for photographer
                fillDgv();
            }
        }

        public void fillDgvAdmin()
        {
            using (DbUnit unit = new DbUnit())
            {
                //clears datasource
                dgvClients.DataSource = null;
                //makes list of financial for current photographer
                var list = unit.financialfunction.getAllFinancialForAdmin((int)CurrentUser.user.Id);
                //checks if list is empty
                if (list.Count == 0)
                {
                    MessageBox.Show("No Financials Found!", "Empty Clients");
                    return;
                }
                //converts list to binding list
                var bindingList = new BindingList<Financial>(list);
                //creates binding source
                var bindingSource = new BindingSource(bindingList, null);
                //fills datagrid view
                dgvClients.DataSource = bindingSource;
                dgvClients.Columns["User"].Visible = false;
                dgvClients.Columns["PhotographerId"].Visible = false;
                dgvClients.Columns["ClientId"].Visible = false;
                dgvClients.Columns["AdminId"].Visible = false;
            }
        }

        public void fillDgv()
        {
            using (DbUnit unit = new DbUnit())
            {
                //clears datasource
                dgvClients.DataSource = null;
                //makes list of financial for current photographer
                var list = unit.financialfunction.getAllFinancialForPhotographer((int)CurrentUser.user.Id);
                //checks if list is empty
                if (list.Count == 0)
                {
                    MessageBox.Show("No Clients Found!", "Empty Clients");
                    return;
                }
                //converts list to binding list
                var bindingList = new BindingList<Financial>(list);
                //creates binding source
                var bindingSource = new BindingSource(bindingList, null);
                //fills datagrid view
                dgvClients.DataSource = bindingSource;
                dgvClients.Columns["User"].Visible = false;
                dgvClients.Columns["PhotographerId"].Visible = false;
                dgvClients.Columns["ClientId"].Visible = false;
                dgvClients.Columns["AdminId"].Visible = false;
            }
        }

        private void btnReport_Click(object sender, EventArgs e)
        {
            //shows report form
            this.Hide();
            frmReport rpt = new frmReport();
            //sets action to current user type
            rpt.action = CurrentUser.user.Type;
            rpt.ShowDialog();
            this.Show();
        }
    }
}
