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
    public partial class frmFirstTime : Form
    {
        public frmFirstTime()
        {
            InitializeComponent();
        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            #region validation
            if(txtUsername.Text == "")
            {
                MessageBox.Show("Please enter a username.", "Empty");
                txtUsername.Focus();
                return;
              
            }
            if(txtPassword.Text == "")
            {
                MessageBox.Show("Please enter a password.", "Empty");
                txtPassword.Focus();
                return;
            }
            if(txtPassword.Text.Length < 8)
            {
                MessageBox.Show("Password must be at least 8 characters.", "Password");
                txtPassword.Clear();
                txtConfirmPassword.Clear();
                txtPassword.Focus();
                return;
            }
            if (txtConfirmPassword.Text != txtPassword.Text)
            {
                MessageBox.Show("Passwords must match.", "Password");
                txtPassword.Clear();
                txtConfirmPassword.Clear();
                txtPassword.Focus();
                return;
            }
            #endregion

            using (DbUnit unit = new DbUnit())
            {
                //gets current user
               var user = unit.userfunction.GetById(CurrentUser.user.Id);
                //changes current user username and password to new username and password
                user.Username = txtUsername.Text;
                user.Password = txtPassword.Text;
                //changes first time to 0 (or false);
                user.FirstTime = 0;
                //saves changes to db
                unit.Commit();
            }
            //confirmation message
            MessageBox.Show("Updated Successfully","Updated");
            //closes form
            this.Close();
        }

        private void clear()
        {
            //clears textboxes
            txtUsername.Clear();
        }
    }
}
