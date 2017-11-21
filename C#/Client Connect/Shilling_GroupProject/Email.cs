using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Shilling_GroupProject
{
    class Email
    {
        public int generateVerificationCode()
        {
            //generates random verification code
            Random rand = new Random();
            return rand.Next(int.MaxValue);
        }

        public bool validateEmail(string email, object sender, EventArgs e)
        {
            //validates email
            string valEmail = "^([0-9a-zA-Z]([-\\.\\w]*[0-9a-zA-Z])*@([0-9a-zA-Z][-\\w]*[0-9a-zA-Z]\\.)+[a-zA-Z]{2,9})$";
            if (Regex.IsMatch(email, valEmail))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public int sendVerificationCodeToEmail(string email)
        {
            //generates and sets verification code
            var verificationCode = generateVerificationCode();
            //creates email
            MailMessage mail = new MailMessage();
            //sets smtp sever
            SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com");

            //sets email properties
            mail.From = new MailAddress("techtestmail77@gmail.com");
            mail.To.Add(email);
            mail.Subject ="Reset Password";
            mail.Body = "Your verification code is: " + verificationCode;

            //sets up Smtp properties
            SmtpServer.Port = 587;
            SmtpServer.Credentials = new System.Net.NetworkCredential("techtestmail77@gmail.com", "Bs0334058");
            SmtpServer.EnableSsl = true;
            SmtpServer.DeliveryMethod = SmtpDeliveryMethod.Network;

            //sends email
            SmtpServer.Send(mail);

            return verificationCode;
        }
    }
}
