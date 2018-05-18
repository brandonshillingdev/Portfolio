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
    public partial class frmRegister : Form
    {
        public frmRegister()
        {
            InitializeComponent();
        }
        

        private void btnRegister_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
   
            #region Validation
            if (txtFirstName.ValidateEmpty("Please enter your first name", "Register")) return;
            if (txtLastName.ValidateEmpty("Please enter your last name", "Register")) return;
            if (txtUsername.ValidateEmpty("Please enter a username", "Register")) return;
            if (txtEmail.ValidateEmpty("Please enter an email", "Register")) return;
            if (txtPassword.ValidateEmpty("Please enter a password", "Register")) return;
            if (txtConfirmPassword.ValidateEmpty("Please confirm password", "Register")) return;
            if (txtPassword.Text.Length < 8)
            {
                MessageBox.Show("Password must be 8 characters long", "Reset Password");
                txtPassword.Clear();
                txtConfirmPassword.Clear();
                txtPassword.Focus();
                return;
            }
            if (txtPassword.Text != txtConfirmPassword.Text)
            {
                //shows error if confirm password is empty
                MessageBox.Show("Passwords Must Match", "Register");
                //clears password and confirm password
                txtPassword.Clear();
                txtConfirmPassword.Clear();
                //sets focus
                txtPassword.Focus();
                return;
            }

            //validates email
            if (!Email.validateEmail(txtEmail.Text))
            {
                MessageBox.Show("Please enter a valid email address", "Invalid Email");
                txtEmail.Clear();
                txtEmail.Focus();
                return;
            }
            //checks if user exists
            bool usernameExists = new DbUnit().Users.CheckUsername(txtUsername.Text.ToLower());
            //checks if email exists
            bool emailExists = new DbUnit().Users.CheckEmail(txtEmail.Text);
            //checks if username exists
            if (usernameExists)
            {
                //error, clears text, focus
                MessageBox.Show("Username already exists", "Username Error");
                txtUsername.Text = "";
                txtUsername.Focus();
                return;
            }
            //if email exsists
            if (emailExists)
            {
                //error, clears text, focus
                MessageBox.Show("Email already exists", "Email Error");
                txtEmail.Text = "";
                txtEmail.Focus();
                return;
            }
            #endregion

            try
            {
                //creates new unit
                using (var model = new DbUnit())
                {
                    //creates a new user object
                    Users user = new Users()
                    {
                        Username = txtUsername.Text.ToLower(),
                        Password = txtPassword.Text,
                        FirstName = txtFirstName.Text,
                        LastName = txtLastName.Text,
                        Email = txtEmail.Text.ToLower()
                    };
                    using(DbModel db = new DbModel())
                    {
                        //adds user to database
                        db.Users.Add(user);
                        //commits changes to the database
                        db.SaveChanges();
                    }
                }
                //shows confirmation messagebox 
                MessageBox.Show("Your all set, now its time to login", "Register");
                //closes form and returns to login
                this.Close();
            }
            catch (Exception ex)
            {
                //error message
                MessageBox.Show(ex.Message, "Error, please contact your system administrator");
            }
            Cursor.Current = Cursors.Default;
        }

        private void lblBack_Click(object sender, EventArgs e)
        {
            //returns to the login form
            this.Close();
        }

        private bool checkEmail()
        {
            //creates new unit
            using (DbUnit unit = new DbUnit())
            {
                //returns boolean
                return unit.Users.CheckEmail(txtEmail.Text);
            }
        }

        private void lblBack_MouseEnter(object sender, EventArgs e)
        {
            lblBack.ForeColor = Color.Yellow;
        }

        private void lblBack_MouseLeave(object sender, EventArgs e)
        {
            lblBack.ForeColor = Color.Orange;
        }
    }
}
