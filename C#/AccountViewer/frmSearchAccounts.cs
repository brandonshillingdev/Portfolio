using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.VisualBasic.FileIO;

namespace AccountViewer
{
    public partial class frmSearchAccounts : Form
    {
        public frmSearchAccounts()
        {
            InitializeComponent();
        }

        private void mnuClose_Click(object sender, EventArgs e)
        {
            //closes current form
            this.Close();
        }

        public List<Account> readcsv()
        {
            try
            {
                //create new textfieldparser object
                TextFieldParser parser = new TextFieldParser(CSV.filepath);
                //sets textfieldparser properties
                parser.SetDelimiters(",");
                parser.HasFieldsEnclosedInQuotes = true;


                //create new list<account> object
                List<Account> allAccounts = new List<Account>();
                //get columns and saves to variable
                var columns = parser.ReadFields();
                //while loop until endoffile
                while (!parser.EndOfData)
                {
                    //create new account object
                    Account account = new Account();
                    //sets current row to data
                    var data = parser.ReadFields();
                    //sets data to correct account information
                    account.Number = int.Parse(data[0]);
                    account.Name = data[1];
                    account.Balance = double.Parse(data[2]);
                    //adds information to allAccounts list
                    allAccounts.Add(account);
                }
                //return account acct
                return allAccounts;
            }
            catch
            {
                return null;
            }
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            if (txtSearch.Text == "")
            {
                //if search bar is empty return
                return;
            }

            dgvSearchResults.DataSource = null;
            // uses linq to set a list of search results to variable list
            var list = readcsv().Where(a => a.Name.ToLower().Contains(txtSearch.Text.ToLower())).ToList();
            //checks if list is not null
            if (list != null && list.Count() > 0)
            {
                //sets datagridview datasource equal to list
                dgvSearchResults.DataSource = list;
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            //clears searchbar and datagridview and focus on searchbar
            txtSearch.Clear();
            dgvSearchResults.DataSource = null;
            txtSearch.Focus();
        }
    }
}
