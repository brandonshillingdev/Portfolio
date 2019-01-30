using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Quantum
{
    class Email
    {
        public static bool validateEmail(string email)
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

        public static int verificationEmail(string toAddress)
        {
            //creates random verification code
            Random rand = new Random();
            int code = rand.Next(100000, 999999);

            try
            {
                //sets all the email settings
                MailMessage mail = new MailMessage();
                SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com");

                mail.From = new MailAddress("techtestmail7@gmail.com","Quantum");
                mail.To.Add(toAddress);
                mail.Subject = "Quantum Password Reset";
                mail.Body = "Here is your verification code " + code;

                SmtpServer.Port = 587;
                SmtpServer.Credentials = new System.Net.NetworkCredential("techtestmail7", "QuantumDev");
                SmtpServer.EnableSsl = true;
                //sends email
                SmtpServer.Send(mail);
            }
            catch
            {
                return 0;
            }
            //returns verification code
            return code;
        }
    }
}
