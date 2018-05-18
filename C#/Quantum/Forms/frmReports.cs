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
    public partial class frmReports : Form
    {
        public frmReports()
        {
            InitializeComponent();
        }
        //vars
        public string report;
        private void frmReports_Load(object sender, EventArgs e)
        {
            if (report == "user")
            {
                //shows user report
                UserReport rpt = new UserReport();
                rpt.SetDatabaseLogon("brandonshillingdev", "Bs012293", "gaming.tstc.edu", "Quantum", false);
                crystalReportViewer1.ReportSource = rpt;
            }
            if (report == "posts")
            {
                //shows user report
                PostByUser rpt = new PostByUser();
                rpt.SetDatabaseLogon("brandonshillingdev", "Bs012293", "gaming.tstc.edu", "Quantum", false);
                crystalReportViewer1.ReportSource = rpt;
            }
        }
    }
}
