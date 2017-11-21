using System;
using System.Windows.Forms;
using DbOps2;

namespace Shilling_GroupProject
{
    public partial class frmEditUser : Form
    {
        public frmEditUser()
        {
            InitializeComponent();
        }

        //getter and setter
        public int UserId { get; set; }
        //vars
        Email mail = new Email();

        private void btnBack_Click(object sender, EventArgs e)
        {
            //closes form
            this.Close();
        }

        private void btnStatus_Click(object sender, EventArgs e)
        {
            try
            {
                using (DbUnit unit = new DbUnit())
                {
                    //gets user
                    User user = unit.userfunction.GetById(UserId);
                    if (btnStatus.Text == "ACTIVATE")
                    {
                        //activates user
                        user.Status = 1;
                    }
                    if (btnStatus.Text == "DEACTIVATE")
                    {
                        //deactivates user
                        user.Status = 0;
                    }
                    //saves changes to db
                    unit.Commit();
                }
            }
            catch
            {
                //error
                MessageBox.Show("Update Unsuccessful", "Unsuccessful");
                return;
            }
            //confirmation message
            MessageBox.Show("User " + btnStatus.Text.ToLower() + "d", "Successful");
            //loads user info
            loadUserInfo();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
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
                MessageBox.Show("Please Enter A Valid Email Address", "Invalid Email");
                txtEmail.Clear();
                txtEmail.Focus();
                return;
            }

            try
            {
                using (DbUnit unit = new DbUnit())
                {
                    //gets user
                    User user = unit.userfunction.GetById(UserId);
                    //sets updated information
                    user.FName = txtFName.Text;
                    user.LName = txtLName.Text;
                    user.Phone = txtPhone.Text;
                    user.Username = txtUsername.Text;
                    user.Email = txtEmail.Text;
                    //commit to db
                    unit.Commit();
                }
            }
            catch(System.Exception except)
            {
                //error
                MessageBox.Show("Error " + except, "Unsuccessful");
                return;
            }
            //confirmation message
            MessageBox.Show("Updated Successful!", "Successful");
        }

        private void frmEditPhotographer_Load(object sender, EventArgs e)
        {
            //loads user info
            loadUserInfo();
        }

        private void loadUserInfo()
        {
            using (DbUnit unit = new DbUnit())
            {
                //gets user info
                User user = unit.userfunction.GetById(UserId);
                //checks if user is active/deactive
                //sets status button text
                if (user.Status == 0)
                {
                    btnStatus.Text = "ACTIVATE";
                }
                if(user.Status == 1)
                {
                    btnStatus.Text = "DEACTIVATE";
                }

                //sets user info into textboxes
                txtFName.Text = user.FName;
                txtLName.Text = user.LName;
                txtUsername.Text = user.Username;
                txtEmail.Text = user.Email;
                txtPhone.Text = user.Phone;
            }
        }
    }
}
