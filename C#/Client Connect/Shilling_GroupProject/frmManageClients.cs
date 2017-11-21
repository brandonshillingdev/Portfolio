using System;
using System.ComponentModel;
using System.Windows.Forms;
using DbOps2;

namespace Shilling_GroupProject
{
    public partial class frmManageClients : Form
    {
        public frmManageClients()
        {
            InitializeComponent();
        }
        private void frmManageClients_Load(object sender, EventArgs e)
        {  
            //changes cursor
            Cursor.Current = Cursors.WaitCursor;
            //loads client data
            if (CurrentUser.user.Type == "Admin")
            {
                //hides visible button
                btnAdd.Visible = false;
                //loads admins clients
                loadDataAdmin();
            }
            if (CurrentUser.user.Type == "Photographer")
            {
                //loads photographers clients
                loadDataPhotographer();
            }
        }
        private void btnBack_Click(object sender, EventArgs e)
        {
            //closes form
            this.Close();
        }
        private void btnAdd_Click(object sender, EventArgs e)
        {
            //changes cursor
            Cursor.Current = Cursors.WaitCursor;
            this.Hide();
            //opens add form
            frmAddUser add = new frmAddUser(usertype.Client);
            add.ShowDialog();
            this.Show();
            //refreshes data
            if (CurrentUser.user.Type == "Admin")
            {
                loadDataAdmin();
            }
            if (CurrentUser.user.Type == "Photographer")
            {
                loadDataPhotographer();
            }   
        }
        private void loadDataPhotographer()
        {
            //clears datasource
            dgvClients.DataSource = null;
            //fills datagridview
            var list = new DbUnit().userfunction.GetClientsForPhotographer(CurrentUser.user.Id);
            //checks if list is empty
            if (list.Count == 0)
            {
                MessageBox.Show("No Clients Found!", "Empty Clients");
                return;
            }
            //converts list to binding list
            var bindingList = new BindingList<User>(list);
            //creates binding source
            var bindingSource = new BindingSource(bindingList, null);
            //sets datasourse
            dgvClients.DataSource = bindingSource;
            dgvClients.Columns["Photos"].Visible = false;
            dgvClients.Columns["Id"].Visible = false;
            dgvClients.Columns["Financials"].Visible = false;
            dgvClients.Columns["PhotographerId"].Visible = false;
            dgvClients.Columns["Password"].Visible = false;
            dgvClients.Columns["Type"].Visible = false;
            dgvClients.Columns["Status"].Visible = false;
            dgvClients.Columns["FirstTime"].Visible = false;
            dgvClients.Columns["AdminId"].Visible = false;
            dgvClients.Columns["FullName"].Visible = false;

        }
        private void loadDataAdmin()
        {
            //clears datasource
            dgvClients.DataSource = null;
            //fills datagridview
            var list = new DbUnit().userfunction.GetClientsForAdmin((int)CurrentUser.user.Id);
            //checks if list is empty
            if (list.Count == 0)
            {
                MessageBox.Show("No Clients Found!", "Empty Clients");
                return;
            }
            //converts list to binding list
            var bindingList = new BindingList<User>(list);
            //creates binding source
            var bindingSource = new BindingSource(bindingList, null);
            //sets datasourse
            dgvClients.DataSource = bindingSource;
            dgvClients.Columns["Photos"].Visible = false;
            dgvClients.Columns["Id"].Visible = false;
            dgvClients.Columns["Financials"].Visible = false;
            dgvClients.Columns["PhotographerId"].Visible = false;
            dgvClients.Columns["Password"].Visible = false;
            dgvClients.Columns["Type"].Visible = false;
            dgvClients.Columns["Status"].Visible = false;
            dgvClients.Columns["FirstTime"].Visible = false;
            dgvClients.Columns["AdminId"].Visible = false;
            dgvClients.Columns["FullName"].Visible = false;
        }
        private void dgvClients_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            this.Hide();
            //gets users id
            int id = int.Parse(dgvClients.CurrentRow.Cells["Id"].Value.ToString());
            //opens the edit form
            frmEditUser edit = new frmEditUser
            {
                //passes id to edit form
                UserId = id
            };
            //opens edit form
            edit.ShowDialog();
            //refreshes data
            if(CurrentUser.user.Type == "Admin")
            {
                loadDataAdmin();
            }
            if(CurrentUser.user.Type == "Photographer")
            {
                loadDataPhotographer();
            }
            this.Show();
        }
    }
}
