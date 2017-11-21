using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;


namespace AccountViewer
{
    public partial class frmMain : Form
    {
        public frmMain()
        {
            InitializeComponent();
        }

        private void mnuExit_Click(object sender, EventArgs e)
        {   
            //Exits the entire application
            Application.Exit();
        }

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            //Sets Open File Dialog Properties
            ofdBrowse.Filter = "CSV files (*.csv)|*.csv";
            ofdBrowse.Title = "Please select an account csv file";
            //if the open file dialog result = ok then retrieve the file path
            if (ofdBrowse.ShowDialog() == DialogResult.OK)
            {
                if(ofdBrowse.FileName != "")
                {
                    //sets csv object filepath and filename
                    CSV.filename = ofdBrowse.SafeFileName;
                    CSV.filepath = ofdBrowse.FileName;
                    //sets text box text to file path
                    txtFilePath.Text = ofdBrowse.FileName;
                    //enables next button
                    btnNext.Enabled = true;
                }
            }
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            //shows wait cursor
            Cursor = Cursors.WaitCursor;
            //hides current form
            this.Hide();
            //creates new frmSearchAccounts and shows form
            frmSearchAccounts search = new frmSearchAccounts();
            search.ShowDialog();
            //resets form
            txtFilePath.Text = "";
            btnNext.Enabled = false;
            Cursor = Cursors.Default;
            //shows current form
            this.Show();
        }
    }
}
