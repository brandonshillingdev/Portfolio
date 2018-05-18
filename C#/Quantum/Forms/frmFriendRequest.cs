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
    public partial class frmFriendRequest : Form
    {
        public frmFriendRequest()
        {
            InitializeComponent();
        }
        //vars
        public string username;
        public string answer;
        
        
        private void frmFriendRequest_Load(object sender, EventArgs e)
        {
            lblAdd.Text = "Would you like to add\n" + username + "\nas a friend?";
        }

        private void btnAccept_Click(object sender, EventArgs e)
        {
            //sets string answer to accepted
            FriendRequestOps.answer = "accepted";
            this.Close();
        }
        private void Decline_Click(object sender, EventArgs e)
        {
            //sets string answer to declined
            FriendRequestOps.answer = "declined";
            this.Close();
        }

        private void pctClose_Click(object sender, EventArgs e)
        {
            //closes form
            this.Close();
        }
    }
}
