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
    public partial class frmAddUser : Form
    {
        public frmAddUser(usertype utype)
        {
            InitializeComponent();
            //set usertype
            type = utype;
        }
        //var
        private usertype type;
        Email mail = new Email();

        private void btnBack_Click(object sender, EventArgs e)
        {
            //closes form
            Close();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtUsername.Text == "")
                {
                    //error
                    MessageBox.Show("Please Enter A Username", "Empty Username");
                    txtUsername.Focus();
                    return;
                }
                if (txtEmail.Text == "")
                {
                    //error
                    MessageBox.Show("Please Enter An Email", "Empty Email");
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

                //validates 10 digit phone number
                if (txtPhone.Text.Length != 14)
                {
                    //error
                    MessageBox.Show("Please Enter A 10 Digit Phone Number", "Phone");
                    //clears text and focuses
                    txtPhone.Text = "";
                    txtPhone.Focus();
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

                using (DbUnit unit = new DbUnit())
                {
                    int adminId =0;
                    //sets admin id
                    if(CurrentUser.user.Type == "Admin")
                    {
                        adminId = CurrentUser.user.Id;
                    }
                    else
                    {
                        adminId = (int)CurrentUser.user.AdminId;
                    }
                    //creates new user
                    User user = new User
                    {
                        //sets user information
                        Username = txtUsername.Text,
                        FName = txtFName.Text,
                        LName = txtLName.Text,
                        Type = type.ToString(),
                        Email = txtEmail.Text,
                        Phone = txtPhone.Text,
                        PhotographerID = CurrentUser.user.Id,
                        Company = CurrentUser.user.Company,
                        AdminId = adminId,
                        Password = txtPassword.Text,
                        FirstTime = 1,
                        Status = 1
                    };
                    //adds user
                    unit.userfunction.Add(user);
                    //commits database
                    unit.Commit();


                }
            }
            catch
            {
                //error message
                MessageBox.Show("Please Try Again","Error");
                txtUsername.Text = "";
                txtFName.Text = "";
                txtLName.Text = "";
                txtEmail.Text = "";
                txtPhone.Text = "";
                txtPassword.Text = "";
                return;
            }
            //clears text
            txtUsername.Text = "";
            txtFName.Text = "";
            txtLName.Text = "";
            txtEmail.Text = "";
            txtPhone.Text = "";
            txtPassword.Text = "";
            //confirmation message
            MessageBox.Show("User Made Successfully", "User");
        }
    }
}
