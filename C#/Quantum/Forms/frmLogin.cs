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
    public partial class frmLogin : Form
    {
        public frmLogin()
        {
            InitializeComponent();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            //validation
            if (txtUsername.ValidateEmpty("Please Enter A Username", "Login")) return;
            if (txtPassword.ValidateEmpty("Please Enter A Password", "Login")) return;

            using (DbUnit unit = new DbUnit())
            {
                try
                {
                    Users user = unit.Users.Login(txtUsername.Text.ToLower(), txtPassword.Text);
                    if (user != null)
                    {
                        //sets user to current user in class
                        CurrentUser.user = user;
                        this.Hide();
                        //navigates to the correct form
                        frmHome main = new frmHome();
                        main.ShowDialog();
                        this.Show();
                        txtUsername.Focus();
                    }
                    else
                    {
                        //error
                        MessageBox.Show("Wrong Username or Password", "Login");
                        txtUsername.Focus();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                   // MessageBox.Show("Login Failed. Please contact your system admin", "Login");
                }
                //clears login textboxes
                clearLogin();
                Cursor.Current = Cursors.Default;
            }
        }
        //changes colors and cursor when mouse enters and leaves button and labels
        private void lblForgotPassword_MouseLeave(object sender, EventArgs e)
        {
            lblForgotPassword.ForeColor = Color.Orange;
        }

        private void lblForgotPassword_MouseEnter(object sender, EventArgs e)
        {
            lblForgotPassword.ForeColor = Color.Yellow;
        }

        private void lblCreateAccount_MouseEnter(object sender, EventArgs e)
        {
            lblCreateAccount.ForeColor = Color.Yellow;
        }

        private void lblCreateAccount_MouseLeave(object sender, EventArgs e)
        {
            lblCreateAccount.ForeColor = Color.Orange;
        }
        
        public void clearLogin()
        {
            txtUsername.Clear();
            txtPassword.Clear();
        }

        private void pctExit_Click(object sender, EventArgs e)
        {
            //closes application
            Application.Exit();
        }

        private void lblCreateAccount_Click(object sender, EventArgs e)
        {
            //navigates to register form
            this.Hide();
            frmRegister register = new frmRegister();
            register.ShowDialog();
            this.Show();
        }

        private void lblForgotPassword_Click(object sender, EventArgs e)
        {
            //navigates to register form
            this.Hide();
            frmForgotPassword forgot = new frmForgotPassword();
            forgot.ShowDialog();
            this.Show();
        }
    }
}
