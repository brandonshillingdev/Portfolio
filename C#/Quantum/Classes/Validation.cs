using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Quantum
{
    public static class Validation
    {
        public static bool IsEmpty(this TextBox textbox)
        {
            //an extension method that checks if textbox text is empty
            return string.IsNullOrEmpty(textbox.Text);
        }

        public static bool ValidateEmpty(this TextBox txtbox, String EmptyMessage, String ErrorHeader)
        {
            //an extention method that displays a messagebox with a heading if the textbox text is empty
            if (txtbox.IsEmpty())
            {
                MessageBox.Show(EmptyMessage, ErrorHeader);
                txtbox.Focus();
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
