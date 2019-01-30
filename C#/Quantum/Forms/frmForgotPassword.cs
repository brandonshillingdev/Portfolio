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
    public partial class frmForgotPassword : Form
    {
        public frmForgotPassword()
        {
            InitializeComponent();
        }
        //vars
        private int verificationCode;
        private string email;

        private void lblBack_Click(object sender, EventArgs e)
        {
            //if form is set up to enter email then do enter email action
            if (lblEmail.Text == "Email")
            {
                this.Close();
            }
            else
            {
                //changes email label to say verification code
                lblEmail.Text = "Email";
            }
        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            //if form is set up to enter email then do enter email action
           if(lblEmail.Text == "Email")
            {
                enterEmailAction();
            }

           //if form is set up to enter verification code then do enter verification action
            else
            {
                enterVerificationAction();
            }
        }
       
        private void enterEmailAction()
        {
            //wait cursor
            Cursor.Current = Cursors.WaitCursor;
            //checks email format
            if (Email.validateEmail(txtEmail.Text.ToLower()))
            {
                //sets email text to variable email
                email = txtEmail.Text.ToLower();
                //checks to see if user email exists, if so sends verification email
                sendVerificationEmail();
                MessageBox.Show("An email has been sent \nPlease enter verification code", "Forgot Password");
                //changes email label to say verification code
                lblEmail.Text = "Verification Code";
            }
            else
            {
                //if email is not in correct format show error
                MessageBox.Show("Please enter a valid email", "Forgot Password");
            }

            //clears and sets focus to email textbox
            txtEmail.Text = string.Empty;
            txtEmail.Focus();
        }

        private void enterVerificationAction()
        {
            //validation
            if(txtEmail.Text == "")
            {
                MessageBox.Show("Please enter a verification code", "Forgot Passowrd");
                txtEmail.Focus();
            }
            if(txtEmail.Text == verificationCode.ToString())
            {
                this.Hide();
                //navigates to reset password form
                frmResetPassword resetPassword = new frmResetPassword();
                resetPassword.email = this.email;
                resetPassword.ShowDialog();
                this.Close();
            }
            else
            {
                //error message
                MessageBox.Show("The verification code doesn't match");
                txtEmail.Clear();
                txtEmail.Focus();
            }
        }

        private void sendVerificationEmail()
        {

            try
            {
                //create DbUnit object
                using (DbUnit unit = new DbUnit())
                {
                    //checks to see if email exists
                    bool emailExists = unit.Users.CheckEmail(email);
                    if (emailExists == true)
                    {
                        //if email exists then send verification email
                        //returns verification code
                        verificationCode = Email.verificationEmail(email);
                    }
                }
            }
            catch
            {

                MessageBox.Show("Please contact your system admin","Email Error");
            }
        }
    }
}
