using EntityCore.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Quantum
{
    public partial class frmHome : Form
    {
        public frmHome()
        {
            InitializeComponent();
            postInfo = new PostInfo();
        }
        //vars
        PostInfo postInfo;
        Friendship friendship;
        List<Image> imgList;
        bool updateProfilePicChanged = false;
        int skip = 0;
        const int take = 100;
        private int userid1;
        private int userid2;
        bool myposts = false;
        bool ProgramDropped = false;
        bool SourceDropped = false;


        private void frmMain_Load(object sender, EventArgs e)
        {
            //shows admin nav if current user is an admin
            if(CurrentUser.user.Admin == 1)
            {
                lblNavAdmin.Visible = true;
                btnNavUserReports.Visible = true;
                btnNavUserPostReport.Visible = true;
            }
            Cursor.Current = Cursors.WaitCursor;
            this.MaximumSize = Screen.FromControl(this).Bounds.Size;
            //allows pictureboxes to accept drag and drop
            #region PictureBoxes Allow Drop
            pctPicture1.AllowDrop = true;
            pctPicture2.AllowDrop = true;
            pctPicture3.AllowDrop = true;
            pctPicture4.AllowDrop = true;
            pctPicture5.AllowDrop = true;
            pctProgram.AllowDrop = true;
            pctSourceCode.AllowDrop = true;
            #endregion
            pnlSelected.Height = btnNavHome.Height;
            pnlSelected.Top = btnNavHome.Top;

            //sets profile picture
            if (CurrentUser.user.ProfilePicture != null)
            {
                pctProfilePic.Image = Convert.convertBlobToImage(CurrentUser.user.ProfilePicture);
            }
            else
            {
                pctProfilePic.Image = Properties.Resources.empty_profile;
            }

            //sets username label to the users username
            lblProfileUsername.Text = CurrentUser.user.Username.ToUpper();
            lblMyPosts.ForeColor = Color.Yellow;
            pnlPostWall.Anchor = (AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Bottom | AnchorStyles.Top);
            //clears any stale controls
            pnlPostWall.Controls.Clear();
            //loads current users posts
            LoadMyPosts();
            ShowLoadMore();
            pnlPosts.Visible = true;
        }

        #region Navigation
        private void btnNavUpload_Click(object sender, EventArgs e)
        {
            //vars
            postInfo = new PostInfo();
            pnlSelected.Top = btnNavUpload.Top;
            //shows upload panel
            HidePanels();
            pnlUpload.Visible = true;
        }

        private void btnNavFriends_Click(object sender, EventArgs e)
        {
            pnlSelected.Top = btnNavFriends.Top;
            //hides all panels
            HidePanels();
            pnlFriends.Visible = true;
            LoadFriendsPanel();
        }

        private void btnNavProfile_Click(object sender, EventArgs e)
        {
            updateProfilePicChanged = false;
            pnlSelected.Top = btnNavProfile.Top;
            //hides all panels
            HidePanels();
            pnlProfile.Visible = true;
            pctUpdateProfilePic.AllowDrop = true;
            //if the current user has a profile picture then it grabs the profile picture
            //and sets it to the picturebox
            if (CurrentUser.user.ProfilePicture != null)
            {
                Image img = Convert.convertBlobToImage(CurrentUser.user.ProfilePicture);
                pctUpdateProfilePic.Image = img;
            }

            //loads the current user information into the text boxes
            txtUpdateUsername.Text = CurrentUser.user.Username;
            txtUpdateEmail.Text = CurrentUser.user.Email;
        }

        private void btnNavHome_Click(object sender, EventArgs e)
        {
            skip = 0;
            pnlPosts.Visible = false;
            Cursor.Current = Cursors.WaitCursor;
            pnlSelected.Top = btnNavHome.Top;
            HidePanels();
            //sets labels back to default color
            ClearHomeSelectedLabels();
            //sets myposts to yellow
            lblMyPosts.ForeColor = Color.Yellow;
            //loads current users posts
            LoadMyPosts();
            pnlPosts.Visible = true;
        }
        private void btnLogout_Click(object sender, EventArgs e)
        {
            //logs out the current user and shows login form
            this.Close();
            CurrentUser.logout();
        }
        #endregion
        #region HomePanel
        private void GetPostForCurrentUser()
        {
            try
            {
                using (DbUnit unit = new DbUnit())
                {
                    //gets all the posts for the current user
                    var posts = unit.Posts.GetPostsByUserId(CurrentUser.user.UserId, skip, take);
                    //var posts = CurrentUser.user.Posts.ToList();
                    //gets post items for posts
                    //if there are posts to show
                    if (posts.Count != 0)
                    {
                        //gets post items for each posts
                        GetPostItems(posts);
                    }
                    //if there are no posts
                    else
                    {
                        //show no posts available
                        ShowNoPostAvailable();
                    }
                }
            }
            catch
            {
                MessageBox.Show("Please contact your system admin", "Post Error");
            }
        }
        private void GetFriendsPosts()
        {
            //clears any stale controls
            pnlPostWall.Controls.Clear();
            try
            {
                using (DbUnit unit = new DbUnit())
                {
                    var posts = unit.Posts.GetFriendsPosts(CurrentUser.user, skip, take);
                    //if there are posts to show
                    if (posts.Count != 0)
                    {
                        //gets post items for each posts
                        GetPostItems(posts);
                    }
                    //if there are no posts
                    else
                    {
                        //show no posts available
                        ShowNoPostAvailable();
                    }
                }
            }
            catch
            {
                MessageBox.Show("Please contact your system admin", "Post Error");
            }
        }
        private void LoadMyPosts()
        {
            //makes remove post label show
            myposts = true;
            //clears any stale controls
            pnlPostWall.Controls.Clear();
            //gets posts for current user
            GetPostForCurrentUser();
            //sets back to false
            myposts = false;
        }
        private void LoadCommunityPosts()
        {

            try
            {
                using (DbUnit unit = new DbUnit())
                {
                    //gets all the community posts
                    var posts = unit.Posts.GetCommunityPosts(skip, take);
                    //var posts = CurrentUser.user.Posts.ToList();
                    //gets post items for posts
                    //if there are posts to show
                    if (posts.Count != 0)
                    {
                        //gets post items for each posts
                        GetPostItems(posts);
                    }
                    //if there are no posts
                    else
                    {
                        //show no posts available
                        ShowNoPostAvailable();
                    }
                }
            }
            catch
            {
                MessageBox.Show("Please contact your system admin", "Post Error");
            }
        }
        private void LoadFriendsPosts()
        {
            //clears any stale controls
            pnlPostWall.Controls.Clear();
            GetFriendsPosts();
        }
        private void ShowPost(string Username, string Title, string Description, List<Image> Pictures, int postid, bool program, bool sourcecode, byte[] pic)
        {
            //create controls
            FlowLayoutPanel post = new FlowLayoutPanel();
            FlowLayoutPanel userinfo = new FlowLayoutPanel();
            PictureBox profilepic = new PictureBox();
            Label username = new Label();
            Label title = new Label();
            Label description = new Label();
            FlowLayoutPanel buttons = new FlowLayoutPanel();
            FlowLayoutPanel fppictures = new FlowLayoutPanel();
            Button downloadprogram = new Button();
            Button downloadsource = new Button();
            Label removepost = new Label();

            //post properties
            post.FlowDirection = FlowDirection.TopDown;
            post.Anchor = (AnchorStyles.Right | AnchorStyles.Left);
            post.Width = pnlPostWall.Width;
            post.Height = 500;
            post.Margin = new Padding(0, 0, 0, 10);
            post.Padding = new Padding(10, 10, 10, 10);
            post.BackColor = Color.FromArgb(64, 64, 64);

            //post properties
            userinfo.FlowDirection = FlowDirection.TopDown;
            userinfo.Width = post.Width;
            userinfo.Height = 70;
            userinfo.BackColor = Color.FromArgb(64, 64, 64);

            //Profile Pic
            if (pic != null)
            {
                profilepic.Image = Convert.convertBlobToImage(pic);
            }
            else
            {
                profilepic.Image = Properties.Resources.empty_profile;
            }
            profilepic.Height = 70;
            profilepic.Width = 75;
            profilepic.SizeMode = PictureBoxSizeMode.StretchImage;
            profilepic.BorderStyle = BorderStyle.FixedSingle;

            //username properties
            username.Text = Username.ToUpper();
            username.Font = new Font("Arial", 16);
            username.Height = 30;
            username.Margin = new Padding(10, 25, 10, 25);
            username.Width = pnlPostWall.Width - 50;
            //title properties
            title.Text = Title;
            title.Font = new Font("Arial", 20);
            title.Width = pnlPostWall.Width - 50;
            title.Height = 50;
            title.Padding = new Padding(40, 0, 0, 0);
            title.Margin = new Padding(150, 0, 0, 0);

            //description properties
            description.Text = Description;
            description.Enabled = false;
            description.ForeColor = Color.FromArgb(31, 31, 31);
            description.Font = new Font("Arial", 14);
            description.AutoSize = false;
            description.Height = 50;
            description.Width = pnlPostWall.Width - 50;
            description.Padding = new Padding(100, 0, 0, 0);
            description.BackColor = Color.FromArgb(64, 64, 64);
            description.BorderStyle = BorderStyle.None;
            description.MaximumSize = new Size(pnlPostWall.Width - 50, 50);
            description.Margin = new Padding(150, 0, 0, 0);

            //fpPicture properties
            fppictures.BorderStyle = BorderStyle.None;
            fppictures.BackColor = Color.FromArgb(64, 64, 64);
            fppictures.FlowDirection = FlowDirection.LeftToRight;
            fppictures.Margin = new Padding(50, 0, 0, 0);
            fppictures.Width = pnlPostWall.Width - 50;
            fppictures.Height = 140;
            fppictures.Margin = new Padding(200, 0, 0, 0);

            //pictue flow panel properties
            Pictures.ForEach(p =>
            {
                PictureBox box = new PictureBox();
                box.BorderStyle = BorderStyle.FixedSingle;
                box.Height = 120;
                box.Width = 120;
                box.Margin = new Padding(10);
                box.SizeMode = PictureBoxSizeMode.StretchImage;
                box.Image = p;
                fppictures.Controls.Add(box);
            });

            //download program button properties
            downloadprogram.Text = "Get Program";
            downloadprogram.Cursor = Cursors.Hand;
            downloadprogram.ForeColor = Color.Orange;
            downloadprogram.BackColor = Color.FromArgb(31, 31, 31);
            downloadprogram.FlatStyle = FlatStyle.Flat;
            downloadprogram.Margin = new Padding(10, 20, 0, 0);
            downloadprogram.Width = 150;
            downloadprogram.Height = 50;
            downloadprogram.Tag = postid;
            downloadprogram.Click += Downloadprogram_Click;

            //download source button properties
            downloadsource.Text = "Get Source Code";
            downloadsource.Cursor = Cursors.Hand;
            downloadsource.ForeColor = Color.Orange;
            downloadsource.BackColor = Color.FromArgb(31, 31, 31);
            downloadsource.FlatStyle = FlatStyle.Flat;
            downloadsource.Margin = new Padding(10, 20, 0, 0);
            downloadsource.Width = 150;
            downloadsource.Height = 50;
            downloadsource.Tag = postid;
            downloadsource.Click += Downloadsource_Click;

            //remove post label properties
            removepost.Text = "Remove Post";
            removepost.Font = new Font("Arial", 12);
            removepost.Height = 30;
            removepost.Width = 300;
            removepost.Margin = new Padding(15,35,15,35);
            removepost.Cursor = Cursors.Hand;
            removepost.ForeColor = Color.Orange;
            removepost.Tag = postid;
            removepost.Click += Removepost_Click;
            removepost.MouseEnter += Removepost_MouseEnter;
            removepost.MouseLeave += Removepost_MouseLeave;



            //button flow panel properties
            buttons.BorderStyle = BorderStyle.None;
            buttons.BackColor = Color.FromArgb(64, 64, 64);
            buttons.FlowDirection = FlowDirection.LeftToRight;
            buttons.Width = pnlPostWall.Width - 50;
            buttons.Margin = new Padding(200, 0, 0, 0);
            buttons.Height = 80;

            //add controls to post
            userinfo.Controls.Add(profilepic);
            userinfo.Controls.Add(username);
            post.Controls.Add(userinfo);
            post.Controls.Add(title);
            post.Controls.Add(description);
            post.Controls.Add(fppictures);
            if (program == true)
            {
                buttons.Controls.Add(downloadprogram);
            }
            if (sourcecode == true)
            {
                buttons.Controls.Add(downloadsource);
            }
            if (myposts == true)
            {
                buttons.Controls.Add(removepost);
            }
            post.Controls.Add(buttons);


            //add post to wall
            pnlPostWall.Controls.Add(post);
        }

        private void Removepost_MouseLeave(object sender, EventArgs e)
        {
            Label remove = (Label)sender;
            remove.ForeColor = Color.Orange;
        }

        private void Removepost_MouseEnter(object sender, EventArgs e)
        {
            Label remove = (Label)sender;
            remove.ForeColor = Color.Yellow;
        }

        private void Removepost_Click(object sender, EventArgs e)
        {
            //confirmation message
           var dr = MessageBox.Show("Confirm Delete", "Delete Post", MessageBoxButtons.YesNo);
            Cursor.Current = Cursors.WaitCursor;
            //if they selected yes
            if (dr == DialogResult.Yes)
            {
                //gets label from sender
                Label remove = (Label)sender;
                //gets post id
                int postid = (int)remove.Tag;
                try
                {
                    using (DbModel model = new DbModel())
                    {
                        //gets post
                        var post = model.Posts.Find(postid);
                        //changes posts status
                        post.Status = Status.Deleted.ToString();
                        //update
                        model.Posts.Update(post);
                        //saves changes to db
                        model.SaveChanges();
                    }
                }
                catch
                {
                    //error message
                    MessageBox.Show("Please contact your system administrator", "Remove Error");
                }
                //loads current users posts
                LoadMyPosts();
                ShowLoadMore();
            }
        }

        private void Downloadprogram_Click(object sender, EventArgs e)
        {
            Button download = (Button)sender;
            int postId = (int)download.Tag;

            try
            {
                using (DbUnit unit = new DbUnit())
                {
                    //gets the post to download by id
                    string title = unit.Posts.GetPostTitle(postId);
                    //gets sourcecode
                    var ProgramInstaller = unit.PostItem.GetProgramInstallerByPostId(postId);
                    SaveFileDialog sfd = new SaveFileDialog();
                    sfd.FileName = title;
                    var result = sfd.ShowDialog();
                    if (result == DialogResult.OK)
                    {
                        string filepath = sfd.FileName + "_program";
                        string zippath = filepath + ".zip";
                        Convert.convertByteToZip(ProgramInstaller, zippath);
                        Convert.unzipFolder(zippath, filepath);
                        File.Delete(zippath);
                    }
                }
            }
            catch
            {
                MessageBox.Show("Please contact your system admin", "Download Error");
            }
        }

        private void Downloadsource_Click(object sender, EventArgs e)
        {
            Button download = (Button)sender;
            int postId = (int)download.Tag;
            try
            {
                using (DbUnit unit = new DbUnit())
                {
                    //gets the post to download by id
                    string title = unit.Posts.GetPostTitle(postId);
                    //gets sourcecode
                    var sourcecode = unit.PostItem.GetSourceCodeByPostId(postId);
                    SaveFileDialog sfd = new SaveFileDialog();
                    sfd.FileName = title + "_source";
                    var result = sfd.ShowDialog();
                    if (result == DialogResult.OK)
                    {
                        string filepath = sfd.FileName;
                        string zippath = filepath + ".zip";
                        Convert.convertByteToZip(sourcecode, zippath);
                        Convert.unzipFolder(zippath, filepath);
                        File.Delete(zippath);
                    }
                }
            }
            catch
            {
                MessageBox.Show("Please contact your system admin", "Download Error");
            }
        }

        private void GetPostItems(List<Posts> posts)
        {
            try
            {
                using (DbUnit unit = new DbUnit())
                {
                    string username;
                    string title;
                    string description;
                    int postid;
                    bool program;
                    bool sourcecode;
                    byte[] profilepic;

                    //gets the post items for each post
                    posts.ForEach(p =>
                    {
                        imgList = new List<Image>();
                        username = p.User.Username;
                        title = p.Title;
                        description = p.Description;
                        postid = p.PostId;
                        if (p.User.ProfilePicture != null)
                        {
                            profilepic = p.User.ProfilePicture;
                        }
                        else
                        {
                            profilepic = null;
                        }

                    //checks if post has program and/or sourcecode
                    program = unit.Posts.DoesPostHaveProgram(postid);
                        sourcecode = unit.Posts.DoesPostHaveSourceCode(postid);
                    //gets all picture items for each post
                    var pictureItems = unit.PostItem.GetPostPicturesByPostId(p.PostId);

                        Parallel.ForEach(pictureItems, item =>
                         {
                             var Picture = Convert.convertBlobToImage(item.Item);
                             imgList.Add(Picture);
                         });
                    //shows the posts 
                    ShowPost(username, title, description, imgList, postid, program, sourcecode, profilepic);
                    });
                }
            }
            catch
            {
                MessageBox.Show("Please contact your system admin", "Post Items Error");
            }
        }

        private void ShowLoadMore()
        {
            //FlowLayoutPanel LoadMorePanel = new FlowLayoutPanel();
            //PictureBox LoadMore = new PictureBox();
            //LoadMore.Click += LoadMore_Click;

            //LoadMore.SizeMode = PictureBoxSizeMode.StretchImage;
            //LoadMore.Height = 25;
            //LoadMore.Width = 25;
            //LoadMore.Cursor = Cursors.Hand;
            //LoadMore.Margin = new Padding((pnlPostWall.Width) - 100, 0, 0, 0);
            //LoadMore.Anchor = (AnchorStyles.Left | AnchorStyles.Right);
            //LoadMore.Image = Properties.Resources.empty_profile;;
            //LoadMorePanel.Anchor = (AnchorStyles.Left | AnchorStyles.Right);
            //LoadMorePanel.Width = pnlPostWall.Width;
            //LoadMorePanel.Controls.Add(LoadMore);
            //pnlPostWall.Controls.Add(LoadMorePanel);
        }

        private void LoadMore_Click(object sender, EventArgs e)
        {
            skip += 10;
            if (lblMyPosts.ForeColor == Color.Yellow)
            {
                Cursor.Current = Cursors.WaitCursor;
                ClearHomeSelectedLabels();
                lblMyPosts.ForeColor = Color.Yellow;
                //loads current users posts
                LoadMyPosts();
                ShowLoadMore();
            }
            else if (lblFriendsPosts.ForeColor == Color.Yellow)
            {
                Cursor.Current = Cursors.WaitCursor;
                ClearHomeSelectedLabels();
                lblFriendsPosts.ForeColor = Color.Yellow;
                LoadFriendsPosts();
                ShowLoadMore();
            }
            else if (lblCommunityPosts.ForeColor == Color.Yellow)
            {
                ClearHomeSelectedLabels();
                lblCommunityPosts.ForeColor = Color.Yellow;
                LoadCommunityPosts();
                ShowLoadMore();
            }

        }

        private void ClearHomeSelectedLabels()
        {
            //clears selected colors for home labels
            lblMyPosts.ForeColor = Color.FromArgb(255, 128, 0);
            lblFriendsPosts.ForeColor = Color.FromArgb(255, 128, 0);
            lblCommunityPosts.ForeColor = Color.FromArgb(255, 128, 0);
        }
        private void ClearHomePanel()
        {
            //clears homewall
            pnlPostWall.Controls.Clear();
        }
        #endregion
        #region UploadPanel
        private void btnUpload_Click(object sender, EventArgs e)
        {
            //wait cursor
            Cursor.Current = Cursors.WaitCursor;
            #region validation
            if (txtTitle.ValidateEmpty("Please enter a title", "Upload")) return;
            if (rtbDescription.Text.Equals(string.Empty))
            {
                MessageBox.Show("Please enter a description", "Upload");
                rtbDescription.Focus();
                return;
            }
            if (rdoFriendsOnly.Checked == false && rdoPublic.Checked == false)
            {
                MessageBox.Show("Please select a privacy for your post", "Upload");
                return;
            }
            if (!ProgramDropped && !SourceDropped)
            {
                MessageBox.Show("You have to upload either a program installer or source code", "Upload");
                return;
            }
            if (pctPicture1.Image == null || pctPicture2.Image == null || pctPicture3.Image == null || pctPicture4.Image == null || pctPicture5.Image == null)
            {
                MessageBox.Show("You must fill all picture slots", "Upload");
                return;
            }
            #endregion
            #region UploadPost
            //gets privacy level
            if (rdoFriendsOnly.Checked == true)
            {
                postInfo.privacy = Privacy.Friends;
            }
            if (rdoPublic.Checked == true)
            {
                postInfo.privacy = Privacy.Public;
            }
            try
            {
                #region Add Post To DB [Upload Post]
                //creates new post
                Posts post = new Posts()
                {
                    UserId = CurrentUser.user.UserId,
                    Title = txtTitle.Text,
                    Description = rtbDescription.Text,
                    Privacy = postInfo.privacy.ToString(),
                    Status = Status.Active.ToString()
                };

                try
                {
                    using (var db = new DbModel())
                    {
                        //adds post and saves changes to db
                        post = db.Posts.Add(post).Entity;
                        db.SaveChanges();
                    }
                }
                catch
                {
                    MessageBox.Show("Please contact your system admin", "Add Post Error");
                }
                #endregion
                #region Add Program To DB [Upload Post]
                if (postInfo.Program != null)
                {
                    PostItems program = new PostItems()
                    {
                        PostId = post.PostId,
                        Item = postInfo.Program,
                        ItemType = ItemInfo.ItemType.Program.ToString()
                    };

                    try
                    {
                        using (var db = new DbModel())
                        {
                            //adds program and saves changes to db
                            db.PostItems.Add(program);
                            db.SaveChanges();
                        }
                    }
                    catch
                    {
                        MessageBox.Show("Please contact your system admin", "Add Program Error");
                    }
                }
                #endregion [Upload Post] 
                #region Add Source Code To DB [Upload Post]
                if (postInfo.SourceCode != null)
                {
                    PostItems sourceCode = new PostItems()
                    {
                        PostId = post.PostId,
                        Item = postInfo.SourceCode,
                        ItemType = ItemInfo.ItemType.SourceCode.ToString()
                    };
                    try
                    {
                        using (var db = new DbModel())
                        {
                            //adds sourcecode and saves changes to db
                            db.PostItems.Add(sourceCode);
                            db.SaveChanges();
                        }
                    }
                    catch
                    {
                        MessageBox.Show("Please contact your system admin", "Add Source Error");
                    }
                }

                #region Add Pictures To DB [Upload Post]
                try
                {
                    using (var unit = new DbUnit())
                    {
                        postInfo.Pictures.ForEach(p =>
                        {
                            PostItems picture = new PostItems()
                            {
                                PostId = post.PostId,
                                Item = Convert.convertImageToBlob(p),
                                ItemType = ItemInfo.ItemType.Picture.ToString()
                            };
                            using (DbModel db = new DbModel())
                            {
                            //adds sourcecode and saves changes to db
                            db.PostItems.Add(picture);
                                db.SaveChanges();
                            }

                        });
                    }
                }
                catch
                {
                    MessageBox.Show("Please contact your system admin", "Add Source Error");
                }

                #endregion
                #endregion [Upload Post]
                MessageBox.Show("Posted Successfully", "Upload");
                ClearUploadPanel();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                //MessageBox.Show("An error has occured, please contact your system admin", "Upload Error");
            }
            #endregion
        }
        private void btnClearUpload_Click(object sender, EventArgs e)
        {
            ClearUploadPanel();
        }
        private void ClearUploadPanel()
        {
            //clears postInfo
            postInfo = new PostInfo();
            //clears form
            txtTitle.Clear();
            rtbDescription.Clear();
            rdoFriendsOnly.Checked = false;
            rdoPublic.Checked = false;
           

            #region Clear Picture Boxes
            if (ProgramDropped)
            {
                pctProgram.Image.Dispose();
                pctProgram.Image = Properties.Resources.program;
            }
            if (SourceDropped)
            {
                pctSourceCode.Image.Dispose();
                pctSourceCode.Image = Properties.Resources.source;
            }
            if (pctPicture1.Image != null)
            {
                pctPicture1.Image.Dispose();
                pctPicture1.Image = null;
            }
            if (pctPicture2.Image != null)
            {
                pctPicture2.Image.Dispose();
                pctPicture2.Image = null;
            }
            if (pctPicture3.Image != null)
            {
                pctPicture3.Image.Dispose();
                pctPicture3.Image = null;
            }
            if (pctPicture4.Image != null)
            {
                pctPicture4.Image.Dispose();
                pctPicture4.Image = null;
            }
            if (pctPicture5.Image != null)
            {
                pctPicture5.Image.Dispose();
                pctPicture5.Image = null;
            }

            //returns bools to false
            ProgramDropped = false;
            SourceDropped = false;
            #endregion
            //focus to title
            txtTitle.Focus();
        }
        #region Drag & Drop

        #region Enter [Drag & Drop]
        private void pctProgram_DragEnter(object sender, DragEventArgs e)
        {
            EnterForDrag(e);
        }
        private void pctSourceCode_DragEnter(object sender, DragEventArgs e)
        {
            EnterForDrag(e);
        }
        private void pctPicture1_DragEnter(object sender, DragEventArgs e)
        {
            EnterForDragPic(sender, e, false);
        }
        private void pctPicture2_DragEnter(object sender, DragEventArgs e)
        {
            EnterForDragPic(sender, e, false);
        }

        private void pctPicture3_DragEnter(object sender, DragEventArgs e)
        {
            EnterForDragPic(sender, e, false);
        }

        private void pctPicture4_DragEnter(object sender, DragEventArgs e)
        {
            EnterForDragPic(sender, e, false);
        }

        private void pctPicture5_DragEnter(object sender, DragEventArgs e)
        {
            EnterForDragPic(sender, e, false);
        }

        #region Functions [Drag & Drop] [Enter] 
        private void EnterForDrag(DragEventArgs e)
        {
            //this shows copy effect for accepted files
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                e.Effect = DragDropEffects.Copy;
            }
            else
            {
                e.Effect = DragDropEffects.None;
            }
        }
        private void EnterForDragPic(object sender, DragEventArgs e, bool UpdateProfilePic)
        {
            var pictureBox = (PictureBox)sender;
            if (UpdateProfilePic = false && pictureBox.Image != null)
            {
                e.Effect = DragDropEffects.None;
                return;
            }
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                //gets filepath of selected image
                var filepath = (string[])e.Data.GetData(DataFormats.FileDrop, false);
                //checks for multiple dropped images
                if (filepath.Length != 1)
                {
                    e.Effect = DragDropEffects.None;
                    return;
                }

                //checks if image is supported
                string ext = Path.GetExtension(filepath[0]).ToLower();

                //this shows copy effect for accepted files
                if ((ext != ".jpg") && (ext != ".png") && (ext != ".bmp") && (ext != ".jpeg"))
                {
                    e.Effect = DragDropEffects.None;
                    return;
                }
                e.Effect = DragDropEffects.Copy;
            }
            else
            {
                e.Effect = DragDropEffects.None;
            }
        }
        #endregion

        #endregion

        #region Program , Source Code, and Pictures [Drag & Drop]
        private void pctProgram_DragDrop(object sender, DragEventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            //gets filepath
            var filepath = (string[])e.Data.GetData(DataFormats.FileDrop, false);
            if (!Directory.Exists(filepath[0]))
            {
                MessageBox.Show("Please upload a program folder, files are not accepted", "Upload");
                return;
            }
            if (filepath.Length != 1)
            {
                MessageBox.Show("Only one program is accepted", "Upload");
                return;
            }
            //changes pctPrograms picture to checkmark
            pctProgram.Image = Properties.Resources.check;
            //converts folder to zip
            Convert.zipFolder(filepath[0], filepath[0] + "Quantum" + ".zip");
            //converts zip to byte[]
            postInfo.Program = Convert.convertZipToByte(filepath[0] + "Quantum" + ".zip");
            //deletes zip file we made
            File.Delete(filepath[0] + "Quantum" + ".zip");
            ProgramDropped = true;
        }

        private void pctSourceCode_DragDrop(object sender, DragEventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            //gets filepath
            var filepath = (string[])e.Data.GetData(DataFormats.FileDrop, false);
            if (!Directory.Exists(filepath[0]))
            {
                MessageBox.Show("Please upload a program folder, files are not accepted", "Upload");
                return;
            }
            if (filepath.Length != 1)
            {
                MessageBox.Show("Only one source code is accepted", "Upload");
                return;
            }
            pctSourceCode.Image = Properties.Resources.check;
            //converts folder to zip
            Convert.zipFolder(filepath[0], filepath[0] + "Quantum" + ".zip");
            //converts zip to byte[]
            postInfo.SourceCode = Convert.convertZipToByte(filepath[0] + "Quantum" + ".zip");
            //deletes zip file we made
            File.Delete(filepath[0] + "Quantum" + ".zip");
            SourceDropped = true;
        }
        #endregion

        #region Pictures [Drag & Drop]
        private void pctPicture1_DragDrop(object sender, DragEventArgs e)
        {
            pictureDrag(pctPicture1, e, false);
        }

        private void pctPicture2_DragDrop(object sender, DragEventArgs e)
        {
            pictureDrag(pctPicture2, e, false);
        }

        private void pctPicture3_DragDrop(object sender, DragEventArgs e)
        {
            pictureDrag(pctPicture3, e, false);
        }

        private void pctPicture4_DragDrop(object sender, DragEventArgs e)
        {
            pictureDrag(pctPicture4, e, false);
        }
        private void pctPicture5_DragDrop(object sender, DragEventArgs e)
        {
            pictureDrag(pctPicture5, e, false);
        }
        #endregion

        #region Functions [Program , Source Code, and Pictures] [Drag & Drop]
        public void pictureDrag(PictureBox picturebox, DragEventArgs e, bool ProfileUpdate)
        {
            Cursor.Current = Cursors.WaitCursor;
            if (picturebox.Image == null || ProfileUpdate)
            {
                //gets filepath
                var filepath = (string[])e.Data.GetData(DataFormats.FileDrop, false);
                //gets image from file and sets to image img
                Image img = Image.FromFile(filepath[0].ToString());
                //sets img to picturebox's image
                picturebox.Image = img;
                postInfo.Pictures.Add(img);
            }
        }
        #endregion
        #endregion

        #endregion   
        #region FriendPanel
        private void LoadFriendsPanel()
        {
            ClearFriendsPanel();
            try
            {
                using (DbUnit unit = new DbUnit())
                {
                    //gets friends list for the current user
                    var FriendsList = unit.Friends.GetFriends(CurrentUser.user.UserId);
                    if (FriendsList.Count > 0)
                    {
                        lbFriendsList.Enabled = true;
                        //adds friends usernames to the friendslist listbox
                        FriendsList.ForEach(friend =>
                        {
                            using (DbModel db = new DbModel())
                            {
                            //adds correct name to friendslist from friendship
                            if (friend.UserId1 == CurrentUser.user.UserId)
                                {
                                    var username = db.Users.Find(friend.UserId2).Username;
                                    lbFriendsList.Items.Add(username.ToUpper());
                                }
                                else
                                {
                                    var username = db.Users.Find(friend.UserId1).Username;
                                    lbFriendsList.Items.Add(username.ToUpper());
                                }
                            }

                        });
                    }
                    else
                    {
                        lbFriendsList.Enabled = false;
                        lbFriendsList.Items.Add("No Friends Yet");
                    }
                    //gets friend requests for the current user
                    var FriendRequests = unit.Friends.GetPendingFriendRequests(CurrentUser.user.UserId);
                    if (FriendRequests.Count > 0)
                    {
                        lbFriendRequests.Enabled = true;
                        //adds friend requests to the friendrequest listbox
                        FriendRequests.ForEach(request =>
                    {
                        using (DbModel db = new DbModel())
                        {
                        //adds the friends username
                        if (request.UserId1 == CurrentUser.user.UserId)
                            {
                                var username = db.Users.Find(request.UserId2).Username;
                                lbFriendRequests.Items.Add(username.ToUpper());
                            }
                            else
                            {
                                var username = db.Users.Find(request.UserId1).Username;
                                lbFriendRequests.Items.Add(username.ToUpper());
                            }
                        }
                    });
                    }

                    else
                    {
                        lbFriendRequests.Enabled = false;
                        lbFriendRequests.Items.Add("No Requests At This Time");
                    }

                    //gets pending outgoing friend requests
                    var FriendRequestsOut = unit.Friends.GetPendingFriendRequestsOut(CurrentUser.user.UserId);
                    if (FriendRequestsOut.Count > 0)
                    {
                        lbPendingRequests.Enabled = true;
                        //adds the friend request out to the pending requests
                        FriendRequestsOut.ForEach(pending =>
                        {
                            using (DbModel db = new DbModel())
                            {
                            //adds the pending friends username
                            if (pending.UserId1 == CurrentUser.user.UserId)
                                {
                                    var username = db.Users.Find(pending.UserId2).Username;
                                    lbPendingRequests.Items.Add(username.ToUpper());

                                }
                                else
                                {
                                    var username = db.Users.Find(pending.UserId1).Username;
                                    lbPendingRequests.Items.Add(username.ToUpper());
                                }
                            }
                        });
                    }
                    else
                    {
                        lbPendingRequests.Enabled = false;
                        lbPendingRequests.Items.Add("No Pending Requests");
                    }

                }
            }
            catch
            {
                MessageBox.Show("Please contact your system admin", "Load Friends Error");
            }
        }
        private void btnAddFriend_Click(object sender, EventArgs e)
        {
            #region Validation
            if (txtAddFriends.ValidateEmpty("Please enter a username", "Add Friend")) return;
            if (txtAddFriends.Text.ToLower() == CurrentUser.user.Username)
            {
                MessageBox.Show("You cant send a friend request to yourself", "Add Friend");
                return;
            }
            #endregion
            SendFriendRequest(txtAddFriends.Text.ToLower());
            LoadFriendsPanel();
        }
        private void SendFriendRequest(string username)
        {
            try
            {
                using (DbUnit unit = new DbUnit())
                {
                    Cursor.Current = Cursors.WaitCursor;
                    //creates new instance of friendship
                    friendship = new Friendship();

                    if (!unit.Users.CheckUsername(username))
                    {
                        //if the username doesnt exist
                        //for security reasons we dont tell the user if the 
                        //username exists
                        MessageBox.Show("Friend request sent", "Add Friend");
                        txtAddFriends.Clear();
                        txtAddFriends.Focus();
                        return;
                    }
                    //gets friends id
                    int FriendId = unit.Users.GetUserIdByUsername(username);
                    //sets the smaller user id to userid1
                    //and sets the bigger one to userid2
                    if (CurrentUser.user.UserId < FriendId)
                    {
                        userid1 = CurrentUser.user.UserId;
                        userid2 = FriendId;
                    }
                    else if (FriendId < CurrentUser.user.UserId)
                    {
                        userid1 = FriendId;
                        userid2 = CurrentUser.user.UserId;
                    }
                    friendship = unit.Friends.GetFriendship(userid1, userid2);
                    if (friendship == null)
                    {
                        //creates new friendship
                        Friendship fr = new Friendship()
                        {
                            UserId1 = userid1,
                            UserId2 = userid2,
                            ActionUserId = CurrentUser.user.UserId,
                            Status = 0
                        };
                        //adds the new friendship
                        unit.Friends.Add(fr);
                        //commits changes to the db
                        unit.Commit();
                        MessageBox.Show("Friend request sent", "Add Friend");
                        txtAddFriends.Clear();
                        txtAddFriends.Focus();
                    }
                    else if (friendship.Status == 0)
                    {
                        //shows messagebox to let user know the friend request is still pending
                        MessageBox.Show("Friend request pending", "Add Friend");
                        txtAddFriends.Clear();
                        txtAddFriends.Focus();
                    }
                    else if (friendship.Status == 1)
                    {
                        //shows messagebox to let user know they are already friends
                        MessageBox.Show("Your already friends", "Add Friend");
                        txtAddFriends.Clear();
                        txtAddFriends.Focus();
                    }
                    else if (friendship.Status == 2)
                    {
                        //shows messagebox to let user know the friend request has been denied
                        MessageBox.Show("Friendship has been denied", "Add Friend");
                        txtAddFriends.Clear();
                        txtAddFriends.Focus();
                    }
                }
                //updates current user
                CurrentUser.update();
            }
            catch
            {
                MessageBox.Show("There has been an error please contact your system admin", "Add Friend Error");
                txtAddFriends.Clear();
                txtAddFriends.Focus();
            }
        }
        private void ClearFriendsPanel()
        {
            //clears the friends panel
            txtAddFriends.Clear();
            lbFriendsList.Items.Clear();
            lbFriendRequests.Items.Clear();
            lbPendingRequests.Items.Clear();
        }
        #endregion

        private void HidePanels()
        {
            //clears upload panel
            ClearUploadPanel();
            ClearHomePanel();
            ClearFriendsPanel();
            ClearProfilePanel();
            pnlProfile.Visible = false;
            pnlUpload.Visible = false;
            pnlPosts.Visible = false;
            pnlFriends.Visible = false;
        }
        private void lbFriendRequests_DoubleClick(object sender, EventArgs e)
        {
            if (lbFriendRequests.SelectedItem != null)
            {
                frmFriendRequest fr = new frmFriendRequest();
                fr.username = lbFriendRequests.SelectedItem.ToString().ToUpper();
                fr.ShowDialog();
                try
                {
                    using (DbUnit unit = new DbUnit())
                    {
                        Friendship FriendshipNew;
                        int FriendId = unit.Users.GetUserIdByUsername(lbFriendRequests.SelectedItem.ToString().ToLower());
                        //sets the smaller user id to userid1
                        //and sets the bigger one to userid2
                        if (CurrentUser.user.UserId < FriendId)
                        {
                            userid1 = CurrentUser.user.UserId;
                            userid2 = FriendId;
                        }
                        else if (FriendId < CurrentUser.user.UserId)
                        {
                            userid1 = FriendId;
                            userid2 = CurrentUser.user.UserId;
                        }
                        friendship = unit.Friends.GetFriendship(userid1, userid2);
                        FriendshipNew = unit.Friends.GetById(friendship.FriendshipId);

                        if (FriendRequestOps.answer == "accepted")
                        {
                            //changes friendship status to accepted
                            FriendshipNew.Status = 1;
                            MessageBox.Show("Accepted");
                        }
                        else if (FriendRequestOps.answer == "declined")
                        {
                            //changes friendship status to declined
                            FriendshipNew.Status = 2;
                            MessageBox.Show("Declined");
                        }
                        else
                        {
                            //if form is closed
                            //reloadsfriendspanel
                            LoadFriendsPanel();
                            //clears answer
                            FriendRequestOps.answer = string.Empty;
                            return;
                        }
                        //sets action user id to current userid
                        FriendshipNew.ActionUserId = CurrentUser.user.UserId;
                        unit.Friends.Update(FriendshipNew);
                        unit.Commit();
                        //updates current user
                        CurrentUser.update();
                        //reloadsfriendspanel
                        LoadFriendsPanel();
                        //clears answer
                        FriendRequestOps.answer = string.Empty;
                    }
                }
                catch
                {
                    MessageBox.Show("Please contact your system admin", "Friend Request Error");
                }
            }
            else
            {
                //this returns if they close the friendrequest form
                return;
            }
            ClearFriendsPanel();
            LoadFriendsPanel();
        }
        private void ShowNoPostAvailable()
        {
            FlowLayoutPanel post = new FlowLayoutPanel();
            Label noPosts = new Label();
            //NoPosts properties
            noPosts.Text = "No Posts Currently Available";
            noPosts.TextAlign = ContentAlignment.TopCenter;
            noPosts.Font = new Font("Arial", 20);
            noPosts.Width = pnlPostWall.Width - 50;
            noPosts.Height = 50;
            //post properties
            post.FlowDirection = FlowDirection.TopDown;
            post.Anchor = (AnchorStyles.Right | AnchorStyles.Left);
            post.Width = pnlPostWall.Width;
            post.Height = 400;
            post.Margin = new Padding(0, 0, 0, 10);
            post.Padding = new Padding(10, 10, 10, 10);
            post.BackColor = Color.FromArgb(64, 64, 64);
            //add the controls to correct panel
            pnlPostWall.Controls.Add(post);
            post.Controls.Add(noPosts);
        }
        private void lblMyPosts_Click(object sender, EventArgs e)
        {
            if (lblMyPosts.ForeColor != Color.Yellow)
            {
                skip = 0;
                Cursor.Current = Cursors.WaitCursor;
                ClearHomeSelectedLabels();
                lblMyPosts.ForeColor = Color.Yellow;
                //clears any stale controls
                pnlPostWall.Controls.Clear();
                //loads current users posts
                LoadMyPosts();
                ShowLoadMore();
            }
        }
        private void lblFriendsPosts_Click(object sender, EventArgs e)
        {
            if (lblFriendsPosts.ForeColor != Color.Yellow)
            {
                skip = 0;
                Cursor.Current = Cursors.WaitCursor;
                ClearHomeSelectedLabels();
                lblFriendsPosts.ForeColor = Color.Yellow;
                //clears any stale controls
                pnlPostWall.Controls.Clear();
                LoadFriendsPosts();
                ShowLoadMore();
            }
        }
        private void lblCommunityPosts_Click(object sender, EventArgs e)
        {
            if (lblCommunityPosts.ForeColor != Color.Yellow)
            {
                Cursor.Current = Cursors.WaitCursor;
                skip = 0;
                ClearHomeSelectedLabels();
                lblCommunityPosts.ForeColor = Color.Yellow;
                //clears any stale controls
                pnlPostWall.Controls.Clear();
                LoadCommunityPosts();
                ShowLoadMore();
            }
        }
        private void ClearProfilePanel()
        {
            //clears the picturebox
            pctUpdateProfilePic.Image = Properties.Resources.empty_profile;
            //clears the textboxes
            txtUpdateUsername.Clear();
            txtUpdateEmail.Clear();
        }

        private void btnProfileUpdate_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            //vars
            string username = CurrentUser.user.Username;
            string email = CurrentUser.user.Email;
            string newUsername = txtUpdateUsername.Text.ToLower();
            string newEmail = txtUpdateEmail.Text.ToLower();
            #region Validation
            //empty validation
            if (txtUpdateUsername.ValidateEmpty("Username cannot be empty", "Update Profile")) return;
            if (txtUpdateEmail.ValidateEmpty("Email cannot be empty", "Update Profile")) return;
            //checks email format
            if (!Email.validateEmail(txtUpdateEmail.Text))
            {
                MessageBox.Show("Please enter a valid email", "Update Profile");
                return;
            }
            //if user didnt change anything
            if (updateProfilePicChanged == false && txtUpdateUsername.Text == CurrentUser.user.Username && txtUpdateEmail.Text == CurrentUser.user.Email)
            {
                MessageBox.Show("You haven't changed anything", "Update Profile");
                return;
            }
            #endregion

            //gets current user
            var user = CurrentUser.user;
            //updates profile picture has been changed
            if (updateProfilePicChanged)
            {
                //converts new profile pic to byte{}
                var newImg = Convert.convertImageToBlob(pctUpdateProfilePic.Image);
                //changes the profile picture
                user.ProfilePicture = newImg;
            }
            //updates username if changed
            if (newUsername != username)
            {
                try
                {
                    using (DbUnit unit = new DbUnit())
                    {
                        //if username exists already
                        if (unit.Users.CheckUsername(newUsername))
                        {
                            MessageBox.Show("Username is not available", "Update Profile");
                            txtUpdateUsername.Focus();
                            return;
                        }
                        else
                        {
                            //sets username to desired username
                            user.Username = newUsername;
                        }
                    }
                }
                catch
                {
                    //error message
                    MessageBox.Show("Please contact your system administrator\nor please try again later", "Error");
                }
            }
            //updates email if changed
            if (newEmail != email)
            {
                try
                {
                    using (DbUnit unit = new DbUnit())
                    {
                        //if username exists already
                        if (unit.Users.CheckEmail(newEmail))
                        {
                            MessageBox.Show("Email is not available", "Update Profile");
                            txtUpdateEmail.Focus();
                            return;
                        }
                        else
                        {
                            //sets username to desired username
                            user.Email = newEmail;
                        }
                    }
                }
                catch
                {
                    MessageBox.Show("Please contact your system admin", "Update Email Error");
                }
            }
            try
            {
                //saves updates to the db
                using (DbModel db = new DbModel())
                {
                    //updates the user
                    db.Users.Update(user);
                    //saves changes to the db
                    db.SaveChanges();
                    //updates current user
                    CurrentUser.update();
                    MessageBox.Show("Update Successful", "Update Profile");
                    //updates username
                    lblProfileUsername.Text = CurrentUser.user.Username;
                    //update profile picture if it was changed
                    if (updateProfilePicChanged)
                    {
                        //updates profile pic
                        pctProfilePic.Image = Convert.convertBlobToImage(CurrentUser.user.ProfilePicture);
                    }
                }
            }
            catch
            {
                //error message
                MessageBox.Show("Please contact your system admin", "Update Profile Pic Error");
            }
            updateProfilePicChanged = false;
        }

        private void pctUpdateProfilePic_DragEnter(object sender, DragEventArgs e)
        {
            EnterForDragPic(sender, e, true);
        }

        private void pctUpdateProfilePic_DragDrop(object sender, DragEventArgs e)
        {
            pictureDrag(pctUpdateProfilePic, e, true);
            updateProfilePicChanged = true;
        }

        private void btnNavUserReports_Click(object sender, EventArgs e)
        {
            frmReports userreport = new frmReports();
            userreport.report = "user";
            userreport.ShowDialog();
           
        }

        private void btnNavHelp_Click(object sender, EventArgs e)
        {
            Help.ShowHelp(this, Application.StartupPath+"/Help/Help.chm");
        }

        private void btnNavUserPostReport_Click(object sender, EventArgs e)
        {
            frmReports userreport = new frmReports();
            userreport.report = "posts";
            userreport.ShowDialog();
        }

        private void btnNavAdminReport_Click(object sender, EventArgs e)
        {
            frmReports userreport = new frmReports();
            userreport.report = "admin";
            userreport.ShowDialog();
        }
    }
}
