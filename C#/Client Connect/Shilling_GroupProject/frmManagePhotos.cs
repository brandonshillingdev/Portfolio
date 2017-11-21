using System;
using System.Drawing;
using System.Windows.Forms;
using DbOps2;
using System.Collections.Generic;

namespace Shilling_GroupProject
{
    public partial class frmManagePhotos : Form
    {
        public frmManagePhotos()
        {
            InitializeComponent();
        }
        //vars
        PictureConverting convert = new PictureConverting();
        private static int clientId = 0;
        private static int photographerId = 0;

        private void frmManagePhotos_Load(object sender, EventArgs e)
        {
            try
            {
                //wait cursor
                Cursor.Current = Cursors.WaitCursor;
                if (CurrentUser.user.Type == "Admin")
                {
                    //sets form up for admin
                    fillPhotographerComboBox();
                    //sets id to current selected photographer
                    photographerId = int.Parse(cmbPhotographers.SelectedValue.ToString());
                }
                else if (CurrentUser.user.Type == "Photographer")
                {
                    //sets form up for photographer
                    lblPhotographer.Visible = false;
                    cmbPhotographers.Visible = false;
                    //fills client combobox
                    fillClientsComboBox();
                }
                else if (CurrentUser.user.Type == "Client")
                {
                    //sets up form for client
                    btnRelease.Text = "View Bill";
                    lblClient.Visible = false;
                    cmbClient.Visible = false;
                    lblPhotographer.Visible = false;
                    cmbPhotographers.Visible = false;
                    btnSearch.Visible = false;
                    cmbStatus.Visible = false;
                    lblStatus.Visible = false;
                    btnAdd.Text = "Download Pictures";
                    btnBack.Text = "Logout";
                    //gets all released photos for client
                    //fills gallery
                    getClientReleasedPhotosClientForm();
                }
                else
                {
                    //error message
                    MessageBox.Show("There has been a issue, closing form.", "Credential Error");
                    this.Close();
                    return;
                }
                //clears comboboxes
                cmbClient.SelectedIndex = -1;
                cmbPhotographers.SelectedIndex = -1;
            }
            catch
            {
                //error message
                MessageBox.Show("Please make sure you have at least one client and one photographer", "Error");
                this.Close();
            }
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            if (CurrentUser.user.Type != "Client")
            {
                //closes form
                this.Close();
            }
            else
            {
                //Exits Application
                Application.Exit();
            }

        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                if (CurrentUser.user.Type == "Client")
                {
                    //downloads released photos
                    DownloadReleasedPhotos();
                }
                else
                {
                    this.Hide();
                    //clears comboboxes
                    if (CurrentUser.user.Type == "Admin")
                    {
                        //sets combobox to first selection
                        cmbPhotographers.SelectedIndex = -1;
                    }

                    //clears comboboxes
                    cmbClient.SelectedIndex = -1;
                    cmbStatus.SelectedIndex = -1;
                    //clears the flow panel
                    fpGallery.Controls.Clear();
                    //opens form
                    frmAddPhoto photo = new frmAddPhoto();
                    photo.ShowDialog();
                    this.Show();
                }
            }
            catch (Exception ex)
            {
                //try catch error message
                MessageBox.Show(ex.ToString(), "Error");
            }
        }

        private void FrmManagePhotos_Click(object sender, EventArgs e)
        {
            //gets selcted photo id
            var photo = (PictureBox)sender;
            var id = photo.Name;
            var fullScreen = new frmFullScreen();
            //sets PhotoId for fullscreen
            fullScreen.PhotoId = int.Parse(id);
            //changes cursor
            Cursor.Current = Cursors.WaitCursor;
            //hides current form
            this.Hide();
            fullScreen.ShowDialog();
            //clears the flow panel
            fpGallery.Controls.Clear();

            //if user is client load released photos
            if (CurrentUser.user.Type == "Client")
            {
                getClientReleasedPhotosClientForm();
            }
            //else load all of the pictures for a client
            else
            {
                //loads all clients photos
                loadAllClientPhotos();
            }
            //shows form
            this.Show();
        }

        private void cmbPhotographers_SelectionChangeCommitted(object sender, EventArgs e)
        {
            if (cmbPhotographers.SelectedIndex != -1)
            {
                //sets id to current selected photographer
                photographerId = int.Parse(cmbPhotographers.SelectedValue.ToString());
                //fills clients into combobox
                fillClientsComboBox();
                //sets combobox to empty
                cmbClient.SelectedIndex = -1;
                //clears the flow panel
                fpGallery.Controls.Clear();
            }
        }

        private void btnRelease_Click(object sender, EventArgs e)
        {
            if(CurrentUser.user.Type == "Client")
            {
                //shows frmReport
                this.Hide();
                frmReport rpt = new frmReport();
                //sets rpt vars
                rpt.action = "Invoice";
                rpt.id = CurrentUser.user.Id;
                rpt.ShowDialog();
                this.Show();
            }
            if(fpGallery.Controls.Count == 0)
            {
                //error message if no photos are in flow panel
                MessageBox.Show("There are no photos to release/unrealease.", "No Photos");
                return;
            }
            if (cmbClient.SelectedIndex != -1)
            {
                //Prompt
                DialogResult result = MessageBox.Show("This will release all photos for this client.\nDo you wish to continue?", "Release All", MessageBoxButtons.YesNo);
                if (result == DialogResult.Yes)
                {
                    using (DbUnit unit = new DbUnit())
                    {
                        //returns list of selected client photos
                        var list = unit.photofunction.GetSingleClientPhotos(int.Parse(cmbClient.SelectedValue.ToString()));
                        //ternary operator sets action to integer
                        int action = (btnRelease.Text == "Release Photos") ? 1 : 0;
                        //for loop to run through all pictures in list
                        for (int i = 0; i < list.Count; i++)
                        {
                            //changes released to correct int for all photos
                            list[i].Released = action;
                        }
                        //saves changes to db
                        Cursor.Current = Cursors.WaitCursor;
                        unit.Commit();
                        //confirmation message
                        MessageBox.Show("Photos sucessfully released", "Release Successful");
                    }
                }
                //refreshes the flow panel
                fpGallery.Controls.Clear();
            }
            else if(CurrentUser.user.Type == "Client")
            {
                return;
            }
            else
            {
                //shows error message
                MessageBox.Show("Please choose a client first.", "No Client Selected");
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {

            #region validation
            if (cmbStatus.SelectedIndex == -1)
            {
                //error message
                MessageBox.Show("Please select a photo status.", "Empty Field");
                return;
            }

            if (cmbPhotographers.SelectedIndex == -1 && CurrentUser.user.Type == "Admin")
            {
                //error message
                MessageBox.Show("Please select a photographer.", "Empty Field");
                return;
            }

            if (cmbClient.SelectedIndex == -1)
            {
                //error message
                MessageBox.Show("Please select a client.", "Empty Field");
                return;
            }
            #endregion

            //changes cursor
            Cursor.Current = Cursors.WaitCursor;
            //clears controls
            fpGallery.Controls.Clear();
            //switch to get search by status
            switch (cmbStatus.Text)
            {
                case "All":
                    //loads all clients photos
                    loadAllClientPhotos();
                    //sets Action Button To Release
                    btnRelease.Text = "Release Photos";
                    break;
                case "Released":
                    //gets clients released photos
                    getClientReleasedPhotosManagePhotosForm();
                    //sets Action Button To Release
                    btnRelease.Text = "Unrelease Photos";
                    break;
                case "Unreleased":
                    //gets clients unreleased photos
                    getClientUnreleasedPhotos();
                    //sets Action Button To Release
                    btnRelease.Text = "Release Photos";
                    break;
                default:
                    //error message
                    MessageBox.Show("A photo status error has occurred", "Error");
                    break;
            }
        }

        //functions
        private void getClientReleasedPhotosClientForm()
        {
            using (DbUnit unit = new DbUnit())
            {
                //gets all released photos for the current user
                var list = unit.photofunction.GetReleasedPhotos(CurrentUser.user.Id);

                //checks if list is empty
                if (list.Count == 0)
                {
                    MessageBox.Show("No Photos Found!", "No Photos");
                    return;
                }

                //creates picturebox array
                var pctArray = new PictureBox[list.Count];

                for (int t = 0; t < list.Count; t++)
                {
                    //creates new instance of pctArray
                    //sets picturebox properties
                    pctArray[t] = new PictureBox();
                    pctArray[t].Name = list[t].Id.ToString();
                    pctArray[t].Size = new Size(150, 150);
                    pctArray[t].SizeMode = PictureBoxSizeMode.StretchImage;
                    pctArray[t].BorderStyle = BorderStyle.FixedSingle;
                    pctArray[t].Cursor = Cursors.Hand;
                    pctArray[t].Click += FrmManagePhotos_Click;
                    //converts and gets the pictures from database
                    pctArray[t].Image = convert.convertBlobToImage(list[t].Photo1);

                    //add picturesboxes to flow panel
                    fpGallery.Controls.Add(pctArray[t]);
                }
            }
        }

        private void getClientReleasedPhotosManagePhotosForm()
        {
            using (DbUnit unit = new DbUnit())
            {
                //gets all released photos for the selected client
                var list = unit.photofunction.GetReleasedPhotos((int)cmbClient.SelectedValue);

                //checks if list is empty
                if (list.Count == 0)
                {
                    MessageBox.Show("No Photos Found!", "No Photos");
                    return;
                }

                //creates picturebox array
                var pctArray = new PictureBox[list.Count];

                for (int t = 0; t < list.Count; t++)
                {
                    //creates new instance of pctArray
                    //sets picturebox properties
                    pctArray[t] = new PictureBox();
                    pctArray[t].Name = list[t].Id.ToString();
                    pctArray[t].Size = new Size(150, 150);
                    pctArray[t].SizeMode = PictureBoxSizeMode.StretchImage;
                    pctArray[t].BorderStyle = BorderStyle.FixedSingle;
                    pctArray[t].Cursor = Cursors.Hand;
                    pctArray[t].Click += FrmManagePhotos_Click;
                    //converts and gets the pictures from database
                    pctArray[t].Image = convert.convertBlobToImage(list[t].Photo1);

                    //add picturesboxes to flow panel
                    fpGallery.Controls.Add(pctArray[t]);
                }
            }
        }

        private void getClientUnreleasedPhotos()
        {
            using (DbUnit unit = new DbUnit())
            {
                //gets all released photos for the selected client
                var list = unit.photofunction.GetUnreleasedPhotos((int)cmbClient.SelectedValue);

                //checks if list is empty
                if (list.Count == 0)
                {
                    MessageBox.Show("No Photos Found!", "No Photos");
                    return;
                }

                //creates picturebox array
                var pctArray = new PictureBox[list.Count];

                for (int t = 0; t < list.Count; t++)
                {
                    //creates new instance of pctArray
                    //sets picturebox properties
                    pctArray[t] = new PictureBox();
                    pctArray[t].Name = list[t].Id.ToString();
                    pctArray[t].Size = new Size(150, 150);
                    pctArray[t].SizeMode = PictureBoxSizeMode.StretchImage;
                    pctArray[t].BorderStyle = BorderStyle.FixedSingle;
                    pctArray[t].Cursor = Cursors.Hand;
                    pctArray[t].Click += FrmManagePhotos_Click;
                    //converts and gets the pictures from database
                    pctArray[t].Image = convert.convertBlobToImage(list[t].Photo1);

                    //add picturesboxes to flow panel
                    fpGallery.Controls.Add(pctArray[t]);
                }
            }
        }

        private void DownloadReleasedPhotos()
        {
            try
            {
                using (DbUnit unit = new DbUnit())
                {
                    //gets list of photos
                    var list = unit.photofunction.GetReleasedPhotos(CurrentUser.user.Id);
                    //gets size of list
                    var listCount = list.Count;
                    //creates image array
                    Image[] imgArray = new Image[listCount];
                    //creates new ojb of filehandler class
                    FileHandler fh = new FileHandler();

                    //savefiledialog obj
                    SaveFileDialog save = new SaveFileDialog();
                    //set savefiledialog properties
                    save.AddExtension = true;
                    save.FileName = "Pictures";
                    save.Filter = "";

                    //message to download to computer
                    DialogResult dr = MessageBox.Show("This will download your photos in a folder to your computer.\nDownload Now?", "Download Photos", MessageBoxButtons.YesNo);
                    if (dr == DialogResult.Yes)
                    {
                        if (save.ShowDialog() == DialogResult.OK)
                        {
                            //creates folder
                            fh.createFolder(save.FileName);

                            for (int i = 0; i < listCount; i++)
                            {
                                string savefilepath = save.FileName;
                                //converts to image and saves image into array
                                imgArray[i] = convert.convertBlobToImage(list[i].Photo1);
                                //saves image to tempfolder
                                imgArray[i].Save(savefilepath + "/" + i + ".jpeg");
                            }
                            //confirmation message
                            MessageBox.Show("Your Photos Have Been Downloaded Successfully", "Download");
                        }
                    }
                }
            }
            catch
            {
                //error message
                MessageBox.Show("There has been an error please try again.\nIf error continues please contact system administrator.", "Error");
            }
        }

        private void loadAllClientPhotos()
        {
            //sets id to current selected photographer
            clientId = int.Parse(cmbClient.SelectedValue.ToString());
            //clears the flow panel
            fpGallery.Controls.Clear();
            //fills list
            var list = new DbUnit().photofunction.GetSingleClientPhotos(clientId);

            //checks if list is empty
            if (list.Count == 0)
            {
                MessageBox.Show("No Photos Found!", "No Photos");
                return;
            }

            //creates picturebox array
            var pctArray = new PictureBox[list.Count];
            for (int t = 0; t < list.Count; t++)
            {
                //creates new instance of pctArray
                //sets picturebox properties
                pctArray[t] = new PictureBox();
                pctArray[t].Name = list[t].Id.ToString();
                pctArray[t].Size = new Size(150, 150);
                pctArray[t].SizeMode = PictureBoxSizeMode.StretchImage;
                pctArray[t].BorderStyle = BorderStyle.FixedSingle;
                pctArray[t].Cursor = Cursors.Hand;
                pctArray[t].Click += FrmManagePhotos_Click;
                //converts and gets the pictures from database
                pctArray[t].Image = convert.convertBlobToImage(list[t].Photo1);

                //add picturesboxes to flow panel
                fpGallery.Controls.Add(pctArray[t]);
            }
        }

        private void fillPhotographerComboBox()
        {
            using (DbUnit unit = new DbUnit())
            {
                //adds photographers to combobox
                var list = unit.userfunction.FillPhotographerComboForAdmin(CurrentUser.user.Id);
                cmbPhotographers.DisplayMember = "FullName";
                cmbPhotographers.ValueMember = "UserId";
                cmbPhotographers.DataSource = list;
            }
        }

        private void fillClientsComboBox()
        {
            using (DbUnit unit = new DbUnit())
            {
                if (CurrentUser.user.Type == "Photographer")
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
    }
}
