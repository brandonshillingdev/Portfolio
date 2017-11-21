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
    public partial class frmManagePhotographers : Form
    {
        public frmManagePhotographers()
        {
            InitializeComponent();
        }

        private void frmManagePhotographers_Load(object sender, EventArgs e)
        {
            //changes cursor
            Cursor.Current = Cursors.WaitCursor;
            LoadData(); //loads photographers
        }

        private void LoadData()
        {
            //clear datagridview
            dgvPhotographers.DataSource = null;
            //fills datagridview
            var list = new DbUnit().userfunction.FillPhotographer((int)CurrentUser.user.Id);
            //checks if list is empty
            if (list.Count == 0)
            {
                MessageBox.Show("No Photographers Found!", "Empty Photographers");
                return;
            }
            //converts list to binding list
            var bindingList = new BindingList<User>(list);
            //creates binding source
            var bindingSource = new BindingSource(bindingList, null);
            //sets datasourse
            dgvPhotographers.DataSource = bindingSource;
            dgvPhotographers.Columns["Photos"].Visible = false;
            dgvPhotographers.Columns["PhotographerId"].Visible = false;
            dgvPhotographers.Columns["Financials"].Visible = false;
            dgvPhotographers.Columns["Id"].Visible = false;
            dgvPhotographers.Columns["Password"].Visible = false;
            dgvPhotographers.Columns["Type"].Visible = false;
            dgvPhotographers.Columns["Status"].Visible = false;
            dgvPhotographers.Columns["FirstTime"].Visible = false;
            dgvPhotographers.Columns["AdminId"].Visible = false;
            dgvPhotographers.Columns["FullName"].Visible = false;
        }

        private void dgvPhotographers_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            this.Hide();
            //gets users id
            int id = int.Parse(dgvPhotographers.CurrentRow.Cells["Id"].Value.ToString());
            //opens the edit form
            frmEditUser edit = new frmEditUser
            {
                //passes id to edit form
                UserId = id
            };
            //opens edit
            edit.ShowDialog();
            //loads photographers
            LoadData();
            this.Show();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            this.Hide();
            //opens the add photographer form
            frmAddUser add = new frmAddUser(usertype.Photographer);
            add.ShowDialog();
            //refreshes data
            LoadData();
            this.Show();
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            //closes form
            this.Close();
        }
    }
}
