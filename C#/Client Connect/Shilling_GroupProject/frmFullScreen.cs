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
    public partial class frmFullScreen : Form
    {
        public frmFullScreen()
        {
            InitializeComponent();
        }
        //vars
        PictureConverting pc = new PictureConverting();
        public int PhotoId { get; set; }

        private void frmFullScreen_Load(object sender, EventArgs e)
        {
            //changes cursor
            Cursor.Current = Cursors.WaitCursor;

            //if user is client
            if(CurrentUser.user.Type == "Client")
            {
                //disables delete button
                pctDelete.Visible = false;
            }
            //gets photo as byte array
            using (DbUnit unit = new DbUnit())
            {
                //gets byte array and convets into image
                var imageByte = unit.photofunction.GetSinglePhoto(PhotoId);
                var image = pc.convertBlobToImage(imageByte);
                //sets picturebox image and height and width
                //to original image height and width
                pctImage.SizeMode = PictureBoxSizeMode.Zoom;
                pctImage.Image = image;
                pctImage.Height = this.Height;
                pctImage.Width = this.Width;
            }
            //changes cursor
            Cursor.Current = Cursors.Arrow;
        }

        private void pctBack_Click(object sender, EventArgs e)
        {
            //changes cursor
            Cursor.Current = Cursors.WaitCursor;
            //closes form
            this.Close();
        }

        private void pctDelete_Click(object sender, EventArgs e)
        {
            using(DbUnit unit = new DbUnit())
            {
                DialogResult dialogResult = MessageBox.Show("Are you sure you want to delete this photo?", "Delete?", MessageBoxButtons.YesNo);
                if(dialogResult == DialogResult.Yes)
                {
                    //deletes photo
                    unit.photofunction.Delete(PhotoId);
                    //commits database
                    unit.Commit();
                    //messagebox
                    MessageBox.Show("Photo Deleted Successfully!", "Successful");
                    this.Close();
                }                
            }
        }
    }
}
