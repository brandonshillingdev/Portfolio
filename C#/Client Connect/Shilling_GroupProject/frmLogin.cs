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
    public partial class frmLogin : Form
    {
        public frmLogin()
        {
            InitializeComponent();
        }

        //login//
        private void btnLogin_Click(object sender, EventArgs e)
        {
            //changes cursor
            Cursor.Current = Cursors.WaitCursor;

            #region validation
            //validation//
            if (txtUsername.Text == "")
            {
                //if password is empty show error
                MessageBox.Show("Please Enter A Username", "Empty Login");
                //sets focus to username
                txtUsername.Focus();
                return;
            }

            if (txtPassword.Text == "")
            {
                //if password is empty show error
                MessageBox.Show("Please Enter A Password", "Empty Login");
                //sets focus to password
                txtPassword.Focus();
                return;
            }
            #endregion

            //create DbUnit object
            using (DbUnit unit = new DbUnit())
            {
                User user = unit.userfunction.Login(txtUsername.Text, txtPassword.Text);
                if (user != null)
                {
                    if(user.Status == 0)
                    {
                        //message
                        MessageBox.Show("Your account has been deactivated","Account");
                        //clears login
                        clearLogin();
                        return;
                    }
                    //sets user to current user in class
                    CurrentUser.user = user;
                    
                    //check what type of user
                    //navigates to the correct form
                    if(user.FirstTime == 1)
                    {
                        this.Hide();
                        frmFirstTime first = new frmFirstTime();
                        first.ShowDialog();

                        if (user.Type == "Photographer")
                        {
                            //hides form
                            this.Hide();
                            var nav = new frmPhotographerNavigation();
                            nav.ShowDialog();
                        }
                        if (user.Type == "Client")
                        {
                            //hides form
                            this.Hide();
                            frmManagePhotos manage = new frmManagePhotos();
                            manage.ShowDialog();
                        } 
                    }
                    if (user.Type == "Admin")
                    {
                        //hides form
                        this.Hide();
                        frmNavigation admin = new frmNavigation();
                        admin.ShowDialog();
                    }
                    if(user.Type == "Photographer")
                    {
                        //hides form
                        this.Hide();
                        var nav = new frmPhotographerNavigation();
                        nav.ShowDialog();
                    }
                    if (user.Type == "Client")
                    {
                        //hides form
                        this.Hide();
                        frmManagePhotos manage = new frmManagePhotos();
                        manage.ShowDialog();
                    }
                }
                else
                {
                    //error
                    MessageBox.Show("Wrong Username or Password", "Login");
                    //clears login
                    clearLogin();
                    return;
                }
                //closes application
                Application.Exit();
            }           
        }
        //Exit//
        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Exits the application
            Application.Exit();
        }
        //Register//

        private void lblRegister_MouseEnter(object sender, EventArgs e)
        {
            //changes lablel forecolor to appear to highlight it
            lblRegister.ForeColor = Color.LightSkyBlue;
        }

        private void lblRegister_MouseLeave(object sender, EventArgs e)
        {
            //returns the label color to unhighlight
            lblRegister.ForeColor = Color.Blue;
        }

        private void lblRegister_Click(object sender, EventArgs e)
        {
            //hides form
            this.Hide();
            //clears login
            clearLogin();
            //navigates to the register form
            frmRegister register = new frmRegister();
            register.ShowDialog();
            //shows login after register close
            this.Show();
        }
        //Clears Login//
        private void clearLogin()
        {
            //clears textboxes
            txtUsername.Text = "";
            txtPassword.Text = "";
            //sets focus to username textbox
            txtUsername.Focus();
        }

        private void lblForgot_Click(object sender, EventArgs e)
        {
            //navigates to forgot your password form
            this.Hide();
            var forgot = new frmForgot();
            forgot.ShowDialog();
            this.Show();
            txtUsername.Clear();
            txtPassword.Clear();
        }

        private void lblForgot_MouseEnter(object sender, EventArgs e)
        {
            //changes lablel forecolor to appear to highlight it
            lblForgot.ForeColor = Color.LightSkyBlue;
        }

        private void lblForgot_MouseLeave(object sender, EventArgs e)
        {
            //changes lablel forecolor back to blue
            lblForgot.ForeColor = Color.Blue;
        }
    }
}
