using System;
using System.Windows.Forms;
using DbOps2;
using System.Collections.Generic;

namespace Shilling_GroupProject
{
    public partial class frmReport : Form
    {
        public frmReport()
        {
            InitializeComponent();
        }
        //vars
        CrystalReport1 cr1 = new CrystalReport1();
        public string action { get; set; }
        public int id = 0;

        private void frmReport_Load(object sender, EventArgs e)
        {
            //loads report according to which report is needed
            switch (action) {
                case "Photographer":
                    //loads report for photographer
                    loadPhotographerReport();
                    break;
                case "Invoice":
                    //loads report for invoice
                    loadInvoice();
                    break;
                case "Admin":
                    //loads report for admin
                    loadAdmin();
                    break;
            }
        }

        private void loadPhotographerReport()
        {
            using (DbUnit unit = new DbUnit())
            {
                //gets data in form of a list
                var data = unit.financialfunction.getAllFinancialForPhotographer(CurrentUser.user.Id);
                //sets datasource to list
                cr1.SetDataSource(data);
            }
            //sets report source
            crystalReportViewer1.ReportSource = cr1;
            //log into db
            cr1.SetDatabaseLogon("BShilling", "1354500");
            //refreshes report
            cr1.Refresh();
        }

        private void loadInvoice()
        {
            //creates new object of crystalreport3
            CrystalReport3 cr3 = new CrystalReport3();

            using (DbUnit unit = new DbUnit())
            {
                //gets data in form of a list
                var data = unit.financialfunction.getSingleFinancialUnpaid(id);
                //sets datasource to list
                cr3.SetDataSource(data);
            }
            //sets report source
            crystalReportViewer1.ReportSource = cr3;
            //log into db
            cr3.SetDatabaseLogon("BShilling", "1354500");
            //refreshes report
            cr3.Refresh();
        }

        private void loadAdmin()
        {
            CrystalReport2 cr = new CrystalReport2();
            using (DbUnit unit = new DbUnit())
            {
                //gets data in form of a list
                var data = unit.financialfunction.getAllFinancialForAdmin(CurrentUser.user.Id);
                //sets datasource to list
                cr.SetDataSource(data);
            }
            //sets report source
            crystalReportViewer1.ReportSource = cr;
            //log into db
            cr.SetDatabaseLogon("BShilling", "1354500");
            //refreshes report
            cr.Refresh();
        }
    }
}
