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
    public partial class frmRegister : Form
    {
        public frmRegister()
        {
            InitializeComponent();
        }
        //vars
        Email mail = new Email();

        private void btnRegister_Click(object sender, EventArgs e)
        {
            #region validation
            if (txtUsername.Text == "")
            {
                //shows error if username is empty
                MessageBox.Show("Please Enter A Username", "Register");
                //sets focus
                txtUsername.Focus();
                return;
            }
            if (txtFName.Text == "")
            {
                //shows error if first name is empty
                MessageBox.Show("Please Enter Your First Name", "Register");
                //sets focus
                txtFName.Focus();
                return;
            }
            if (txtLName.Text == "")
            {
                //shows error if last name is empty
                MessageBox.Show("Please Enter Your Last Name", "Register");
                //sets focus
                txtLName.Focus();
                return;
            }
            if (txtEmail.Text == "")
            {
                //shows error if email is empty
                MessageBox.Show("Please Enter An Email", "Register");
                //sets focus
                txtEmail.Focus();
                return;
            }

            if (txtPassword.Text == "")
            {
                //shows error if password is empty
                MessageBox.Show("Please Enter A Password", "Register");
                //sets focus
                txtPassword.Focus();
                return;
            }
            if (txtConfirmPassword.Text == "")
            {
                //shows error if confirm password is empty
                MessageBox.Show("Please Confirm Password", "Register");
                //sets focus
                txtConfirmPassword.Focus();
                return;
            }
            if(txtPassword.Text != txtConfirmPassword.Text)
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
            if (txtConfirmPassword.Text.Length < 8)
            {
                //shows error if confirm password is empty
                MessageBox.Show("Password Must Be At Least 8 Characters", "Register");
                //clears password and confirm password
                txtPassword.Clear();
                txtConfirmPassword.Clear();
                //sets focus
                txtPassword.Focus();
                return;
            }
            if (checkEmail() == true)
            {
                //if email is already in use
                MessageBox.Show("Email already in use");
                //clear textbox
                txtEmail.Clear();
                //sets focus to txtemail
                txtEmail.Focus();
                return;
            }
            if (txtPhone.Text.Length != 14)
            {
                //error
                MessageBox.Show("Please Enter A 10 Digit Phone Number", "Phone");
                //clears text and focuses
                txtPhone.Text = "";
                txtPhone.Focus();
                return;
            }
            if(txtCompany.Text == "")
            {
                //error
                MessageBox.Show("Please Enter A Company", "Company");
                txtCompany.Focus();
                return;
            }
            //validates email
            if (!mail.validateEmail(txtEmail.Text, sender, e))
            {
                MessageBox.Show("Please Enter A Valid Email Address","Invalid Email");
                txtEmail.Clear();
                txtEmail.Focus();
                return;
            }

            //checks if user exists
            bool usernameExists = new DbUnit().userfunction.CheckUsername(txtUsername.Text);
            //checks if email exists
            bool emailExists = new DbUnit().userfunction.CheckEmail(txtEmail.Text);

            if (usernameExists)
            {
                //error, clears text, focus
                MessageBox.Show("Username Already Exists!", "Username Error");
                txtUsername.Text = "";
                txtUsername.Focus();
                return;
            }

            //if email exsists
            if (emailExists)
            {
                //error, clears text, focus
                MessageBox.Show("Email Already Exists!", "Email Error");
                txtEmail.Text = "";
                txtEmail.Focus();
                return;
            }
            #endregion
            try
            {
                //creates new unit
                using (DbUnit unit = new DbOps2.DbUnit())
                {
                    //creates a new user object
                    User user = new User();
                    //adds the text boxtext to user data
                    user.Username = txtUsername.Text;
                    user.Password = txtPassword.Text;
                    user.FName = txtFName.Text;
                    user.LName = txtLName.Text;
                    user.Phone = txtPhone.Text;
                    user.Email = txtEmail.Text;
                    user.Company = txtCompany.Text;
                    user.Type = "Admin";
                    user.Status = 1;
                    user.FirstTime = 0;
                    //adds user to database
                    unit.userfunction.Add(user);
                    //commits database
                    unit.Commit();
                }
                //shows confirmation messagebox 
                MessageBox.Show("Your Account Has Successfully Been Made!", "Register");
                //closes form and returns to login
                this.Close();
            }catch(Exception ex)
            {
                //error message
                MessageBox.Show(ex.Message, "Error");
            }    
        }

        private void mnuExit_Click(object sender, EventArgs e)
        {
            //closes form
            this.Close();
        }

        private bool checkEmail()
        {
            //creates new unit
            using (DbUnit unit = new DbUnit())
            {
                //returns boolean
                return unit.userfunction.CheckEmail(txtEmail.Text);
            }
        }
    }
}
