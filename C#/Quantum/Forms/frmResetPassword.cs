using EntityCore.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Quantum
{
    public partial class frmResetPassword : Form
    {
        public frmResetPassword()
        {
            InitializeComponent();
        }
        //vars
        public string email;

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            //validation
            if (txtPassword.ValidateEmpty("Please enter a password", "Reset Password")) return;
            if (txtConfirmPassword.ValidateEmpty("Please confirm password", "Reset Password")) return;
            if(txtPassword.Text.Length < 8)
            {
                MessageBox.Show("Password must be 8 characters long", "Reset Password");
                txtPassword.Clear();
                txtConfirmPassword.Clear();
                txtPassword.Focus();
                return;
            }
            if (txtPassword.Text != txtConfirmPassword.Text) {
                MessageBox.Show("Passwords don't match", "Reset Password");
                return;
            }
            else
            {

                try
                {
                    using (DbUnit unit = new DbUnit())
                    {
                        //gets the user with the email that was provided and sets it to current user
                        //then changes the password and saves the changes to the database
                        CurrentUser.user = unit.Users.getUserByEmail(email);
                        //checks if password is same as current password
                        if (CurrentUser.user.Password == txtPassword.Text)
                        {
                            //error message
                            MessageBox.Show("You can't change the password to the current password", "Reset Password");
                            txtPassword.Clear();
                            txtConfirmPassword.Clear();
                            txtPassword.Focus();
                            return;
                        }

                        CurrentUser.user.Password = txtPassword.Text;
                        using (DbModel db = new DbModel())
                        {
                            //updates user information in database
                            db.Users.Update(CurrentUser.user);
                            //saves changes in database
                            db.SaveChanges();
                        }
                    }
                }
                catch
                {
                    MessageBox.Show("Please contact your system admin", "Reset Password Error");
                }
            }
            this.Hide();
            frmLogin login = new frmLogin();
            login.ShowDialog();
            this.Close();

        }
    }
}
