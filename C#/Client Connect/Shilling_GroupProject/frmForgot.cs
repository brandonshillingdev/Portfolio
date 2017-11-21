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
    public partial class frmForgot : Form
    {
        public frmForgot()
        {
            InitializeComponent();
        }

        //vars
        Email mail = new Email();
        private string email;
        private int verificationCode;

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            #region verification
            if (txtEmail.Text == "" && lblEmail.Text == "Email")
            {
                //error message
                MessageBox.Show("Please enter an email.", "Empty");
                txtEmail.Focus();
                return;
            }
            if(txtEmail.Text == "" && lblEmail.Text == "Verification Code")
            {
                //error message
                MessageBox.Show("Please enter verification code.", "Empty");
                txtEmail.Focus();
                return;
            }
            //validates email
            if (!mail.validateEmail(txtEmail.Text, sender, e) && lblEmail.Text == "Email")
            {
                MessageBox.Show("Please Enter A Valid Email Address", "Invalid Email");
                txtEmail.Clear();
                txtEmail.Focus();
                return;
            }

            using (DbUnit unit = new DbUnit())
            {
                //checks to see if email is in db
                if (unit.userfunction.GetUserByEmail(txtEmail.Text) == null && lblEmail.Text == "Email")
                {
                    //error message
                    MessageBox.Show("The email provided does not match our records.\nPlease enter a valid email address.", "Invalid Email");
                    txtEmail.Clear();
                    txtEmail.Focus();
                    return;
                }
            }
            #endregion
            
            if (lblEmail.Text == "Email")
            {
                //changes form for verification code
                lblEmail.Text = "Verification Code";
                lblInstructions.Text = "Check your email for a verification code.\nEnter the code below.";
                lblResend.Visible = true;
                //sets cursor to wait cursor
                this.Cursor = Cursors.WaitCursor;
                try
                {
                    //sends verification code to email address
                    verificationCode = mail.sendVerificationCodeToEmail(txtEmail.Text);
                }
                catch
                {
                    //Error message
                    MessageBox.Show("There has been a problem sending the verification code.\nPlease try again.", "Error");
                }

                //sets cursor back
                this.Cursor = Cursors.Default;
                //sets email to textbox email
                email = txtEmail.Text;
                //clears text in textbox
                txtEmail.Clear();
            }
            else if (lblEmail.Text == "Verification Code")
            {
                //checks if verification code matches user input
                if(verificationCode.ToString() == txtEmail.Text)
                {
                    //goes to reset password form
                    this.Hide();
                    var psw = new frmResetPassword();
                    psw.email = email;
                    psw.ShowDialog();
                    this.Close();
                }
                else
                {
                    //Error message
                    MessageBox.Show("The verification code does not match.","Wrong Code");
                    //changes for to enter email
                    txtEmail.Clear();
                    txtEmail.Focus();
                }
            }
        }

        private void lblResend_Click(object sender, EventArgs e)
        {
            //changes for to enter email
            lblEmail.Text = "Email";
            txtEmail.Clear();
            lblInstructions.Text = "Please enter your email address";
            lblResend.Visible = true;
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            //closes current form
            this.Close();
        }

        private void lblResend_MouseEnter(object sender, EventArgs e)
        {
            //changes lablel forecolor to appear to highlight it
            lblResend.ForeColor = Color.LightSkyBlue;
        }

        private void lblResend_MouseLeave(object sender, EventArgs e)
        {
            //changes lablel forecolor back to blue
            lblResend.ForeColor = Color.Blue;
        }
    }
}
