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
    public partial class frmResetPassword : Form
    {
        public frmResetPassword()
        {
            InitializeComponent();
        }
        public string email { get; set; }

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            #region verification
            if(txtPassword.Text.Length < 8)
            {
                MessageBox.Show("Please enter a password longer than 8 characters","Password");
                txtPassword.Clear();
                txtConfirm.Clear();
                return;
            }
            if(txtConfirm.Text != txtPassword.Text)
            {
                MessageBox.Show("Passwords do not match.", "Password");
                txtPassword.Clear();
                txtConfirm.Clear();
                return;
            }
            #endregion

            using (DbUnit unit = new DbUnit())
            {
                //creates new user obj and sets to user in db
                var user = unit.userfunction.GetUserByEmail(email);
                //sets password
                user.Password = txtConfirm.Text;
                //saves to db
                unit.Commit();
            }
            //confirmation message
            MessageBox.Show("Password Updated Successfully", "Updated");
            this.Close();
        }
    }
}
