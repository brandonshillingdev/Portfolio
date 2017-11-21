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
    public partial class frmAddFinancial : Form
    {
        public frmAddFinancial()
        {
            InitializeComponent();
        }

        //vars
        public string type = "";
        public int id;
        public int clientid;

        private void frmAddFinancial_Load(object sender, EventArgs e)
        {
            //sets minimum date to todays date
            dtpDue.MinDate = DateTime.Now.Date;
            //for editing
            if (type == "edit")
            {
                lblTitle.Text = "Edit Financial";
                btnAdd.Text = "Update";
                this.Text = "Edit Financial";
                btnDelete.Visible = true;
                btnInvoice.Visible = true;
                using (DbUnit unit = new DbUnit())
                {
                    //gets client
                    var client = unit.userfunction.GetById(clientid);
                    //sets client name in cmb
                    string fullname = (client.FName + " " + client.LName);
                    cmbClient.Items.Add(fullname);
                    cmbClient.Text = fullname;
                    //gets clients financial
                    var financial = unit.financialfunction.GetById(id);
                    //sets if paid and amount
                    cmbPaid.Text = financial.Paid;
                    numAmount.Value = financial.Amount;
                }
            }
            else
            {
                //for adding
                using (DbUnit unit = new DbUnit())
                {
                    var list = unit.userfunction.FillClientComboForPhotographer(CurrentUser.user.Id);
                    //add all clients to list
                    cmbClient.DisplayMember = "FullName";
                    cmbClient.ValueMember = "UserId";
                    cmbClient.DataSource = list;
                }
            }
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            //closes form
            this.Close();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            //empty validation and greater than 0 validation
            if (!(numAmount.Value > 0))
            {
                //error
                MessageBox.Show("Please enter an amount greater than 0", "Amount");
                return;
            }
            if (cmbPaid.SelectedIndex == -1)
            {
                //error
                MessageBox.Show("Please enter paid/unpaid", "Paid");
                return;
            }
            if (btnAdd.Text == "Update")
            {
                //updates
                using (DbUnit unit = new DbUnit())
                {
                    //gets current client financial
                    var financial = unit.financialfunction.GetById(id);
                    //updates financial
                    financial.Amount = numAmount.Value;
                    financial.Paid = cmbPaid.Text;
                    financial.DueDate = dtpDue.Value.Date;
                    //saves changes to db
                    unit.Commit();
                    //confimation message
                    MessageBox.Show("Update Successful", "Success");
                    //closes form
                    this.Close();
                }
            }
            else
            {
                //add
                using (DbUnit unit = new DbUnit())
                {
                    //creates new financial
                    var newFinancial = new Financial();
                    //sets financial
                    newFinancial.Amount = numAmount.Value;
                    newFinancial.Client = cmbClient.Text;
                    newFinancial.AdminId = (int)CurrentUser.user.AdminId;
                    newFinancial.ClientId = (int)cmbClient.SelectedValue;
                    newFinancial.DueDate = dtpDue.Value.Date;
                    newFinancial.Paid = cmbPaid.Text;
                    newFinancial.Company = CurrentUser.user.Company;
                    newFinancial.Photographer = CurrentUser.user.Username;
                    newFinancial.PhotographerId = CurrentUser.user.Id;
                    //adds financial
                    unit.financialfunction.Add(newFinancial);
                    //saves to db
                    unit.Commit();
                    MessageBox.Show("Add Successful", "Success");
                    //closes form
                    this.Close();
                }
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            //confirmation message to delete
            DialogResult dr = MessageBox.Show("Are you sure you want to delete?", "Confirm", MessageBoxButtons.YesNo);
            if (dr == DialogResult.Yes)
            {
                try
                {
                    using (DbUnit unit = new DbUnit())
                    {
                        //gets financial entity
                        var financial = unit.financialfunction.GetById(id);
                        //deletes entity from db
                        unit.financialfunction.Delete(financial);
                        //saves changes to db
                        unit.Commit();
                        //confimation message
                        MessageBox.Show("Delete Successful", "Success");
                        //closes form
                        this.Close();
                    }
                }
                catch
                {
                    //error
                    MessageBox.Show("If error continues please contact your system administrator", "Error");
                }
            }
        }

        private void clear()
        {
            //clear
            numAmount.Value = 0;
            cmbClient.SelectedIndex = 0;
            cmbPaid.SelectedIndex = -1;
        }

        private void btnInvoice_Click(object sender, EventArgs e)
        {
            //shows frmReport
            this.Hide();
            frmReport rpt = new frmReport();
            //sets rpt vars
            rpt.action = "Invoice";
            rpt.id = clientid;
            rpt.ShowDialog();
            this.Show();
        }
    }
}
