using DbOps2;
using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace Shilling_GroupProject
{
    public partial class frmAddPhoto : Form
    {
        public frmAddPhoto()
        {
            InitializeComponent();
        }
        //vars
        PictureConverting convert = new PictureConverting();
        int photographerId = 0;
        int clientID = 0;

        private void btnUpload_Click(object sender, EventArgs e)
        {
            if(CurrentUser.user.Type == "Admin" && cmbPhotographer.SelectedIndex == -1)
            {
                //if current user is an admin and photographer combobox is empty show error
                MessageBox.Show("Please Select A Photographer \n Then Select A Client");
                return;
            }
            
            if(cmbClient.SelectedIndex == -1)
            {
                //if client combobox is empty show error
                MessageBox.Show("Please Select A Client");
                return;
            }

           if(pctPhoto.Image != null)
            {
                //sets returned byte array to img
              var img = convert.converImageToBlob(pctPhoto.Image);
                try
                {
                    using (DbUnit unit = new DbUnit())
                    {
                        //get current selected client
                        var client = (UserShort)cmbClient.SelectedItem;
                        //create new photo obj
                        Photo photo = new Photo();
                        //sets photo information
                        photo.PhotographerId = photographerId;
                        photo.Photo1 = img;
                        photo.Client = client.UserId;
                        //adds photo to database
                        unit.photofunction.Add(photo);
                        //commits to database
                        unit.Commit();
                        //confirmation message
                        MessageBox.Show("Image Uploaded Successfully", "Image Uploaded");
                        //clears picturebox
                        pctPhoto.Image = null;
                        //clears the comboboxes
                        cmbPhotographer.SelectedIndex = -1;
                        cmbClient.SelectedIndex = -1;
                    }
                }
                catch
                {
                    //error message
                    MessageBox.Show("Error Uploading Photo", "Error");
                }
            }
            else
            {
                //error message
                MessageBox.Show("No Image To Upload", "Empty Image");
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            //get current selected object
            var item = (UserShort)cmbClient.SelectedItem;
            //sets filter on what files can be selected
            fdImage.Filter = "Images Only. |*.jpg;.*jpeg;*.png;*.gif";
            //shows dialog
            fdImage.ShowDialog();
            try
            {
                //gets image from path and sets it in picturebox
                pctPhoto.Image = Image.FromFile(fdImage.FileName);
            }
            catch
            {
                //error message
                MessageBox.Show("Please Select A Valid Picture");
            }
        }

        private void frmAddPhoto_Load(object sender, EventArgs e)
        {
            
            if (CurrentUser.user.Type == "Admin")
            {
                using (DbUnit unit = new DbUnit())
                {
                    //fills list of users under admin
                    var photographerList = unit.userfunction.FillPhotographerComboForAdmin(CurrentUser.user.Id);
                    //add all clients to list
                    cmbPhotographer.DisplayMember = "FullName";
                    cmbPhotographer.ValueMember = "UserId";
                    cmbPhotographer.DataSource = photographerList;

                    if (cmbPhotographer.SelectedIndex != -1)
                    {
                        //sets id to current selected photographer
                        photographerId = int.Parse(cmbPhotographer.SelectedValue.ToString());
                        //fills clients into combobox
                        fillClientsComboBox();
                        //sets combobox to empty
                        cmbClient.SelectedIndex = -1;
                    }
                }
            }

            else if (CurrentUser.user.Type == "Photographer")
            {
                //hides photographer combobox
                cmbPhotographer.Visible = false;
                lblPhotographer.Visible = false;
                //sets photographer id
                photographerId = CurrentUser.user.Id;

                using (DbUnit unit = new DbUnit())
                {
                    var clientList = unit.userfunction.FillClientComboForPhotographer(CurrentUser.user.Id);
                    //add all clients to list
                    cmbClient.DisplayMember = "FullName";
                    cmbClient.ValueMember = "UserId";
                    cmbClient.DataSource = clientList;
                }
            }

            else
            {
                MessageBox.Show("There has been a issue, closing form.", "Credential Error");
                this.Close();
                return;
            }
            }

        private void btnBack_Click(object sender, EventArgs e)
        {
            //closes form
            this.Close();
        }

        private void fillClientsComboBox()
        {
            using (DbUnit unit = new DbUnit())
            {
                if(CurrentUser.user.Type == "Admin")
                {
                    //sets photographer id to selectede photographer 
                    photographerId = int.Parse(cmbPhotographer.SelectedValue.ToString());
                }

                else if (CurrentUser.user.Type == "Photographer")
                {
                    //if current user is a photographer
                    //set the current user id to var photographer id
                    photographerId = CurrentUser.user.Id;
                }

                //adds clients to combobox
                var list = unit.userfunction.FillClientComboForPhotographer(photographerId);
                cmbClient.DisplayMember = "FullName";
                cmbClient.ValueMember = "UserId";
                cmbClient.DataSource = list;
            }
        }

        private void cmbClient_SelectionChangeCommitted(object sender, EventArgs e)
        {
            //sets selected client id
            clientID = int.Parse(cmbClient.SelectedValue.ToString());
        }

        private void cmbPhotographer_SelectionChangeCommitted(object sender, EventArgs e)
        {
            //fills combobox
            fillClientsComboBox();
            //sets combobox to empty
            cmbClient.SelectedIndex = -1;
        }
    }
}
