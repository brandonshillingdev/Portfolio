namespace Shilling_GroupProject
{
    partial class frmPhotographerNavigation
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmPhotographerNavigation));
            this.pnlAdmin = new System.Windows.Forms.Panel();
            this.btnLogout = new System.Windows.Forms.Button();
            this.pctFinancial = new System.Windows.Forms.PictureBox();
            this.lblFinancial = new System.Windows.Forms.Label();
            this.lblPhotos = new System.Windows.Forms.Label();
            this.lblClients = new System.Windows.Forms.Label();
            this.pctClients = new System.Windows.Forms.PictureBox();
            this.pctPhoto = new System.Windows.Forms.PictureBox();
            this.pnlTitle = new System.Windows.Forms.Panel();
            this.lblTitle = new System.Windows.Forms.Label();
            this.pnlAdmin.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pctFinancial)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pctClients)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pctPhoto)).BeginInit();
            this.pnlTitle.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlAdmin
            // 
            this.pnlAdmin.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlAdmin.BackColor = System.Drawing.SystemColors.ControlDark;
            this.pnlAdmin.Controls.Add(this.btnLogout);
            this.pnlAdmin.Controls.Add(this.pctFinancial);
            this.pnlAdmin.Controls.Add(this.lblFinancial);
            this.pnlAdmin.Controls.Add(this.lblPhotos);
            this.pnlAdmin.Controls.Add(this.lblClients);
            this.pnlAdmin.Controls.Add(this.pctClients);
            this.pnlAdmin.Controls.Add(this.pctPhoto);
            this.pnlAdmin.Controls.Add(this.pnlTitle);
            this.pnlAdmin.Location = new System.Drawing.Point(-2, -1);
            this.pnlAdmin.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.pnlAdmin.Name = "pnlAdmin";
            this.pnlAdmin.Size = new System.Drawing.Size(639, 308);
            this.pnlAdmin.TabIndex = 3;
            // 
            // btnLogout
            // 
            this.btnLogout.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.btnLogout.BackColor = System.Drawing.SystemColors.ControlDark;
            this.btnLogout.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnLogout.Font = new System.Drawing.Font("Constantia", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnLogout.Location = new System.Drawing.Point(265, 273);
            this.btnLogout.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btnLogout.Name = "btnLogout";
            this.btnLogout.Size = new System.Drawing.Size(116, 27);
            this.btnLogout.TabIndex = 54;
            this.btnLogout.Text = "Logout";
            this.btnLogout.UseVisualStyleBackColor = false;
            this.btnLogout.Click += new System.EventHandler(this.btnLogout_Click);
            // 
            // pctFinancial
            // 
            this.pctFinancial.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.pctFinancial.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pctFinancial.Image = ((System.Drawing.Image)(resources.GetObject("pctFinancial.Image")));
            this.pctFinancial.Location = new System.Drawing.Point(25, 71);
            this.pctFinancial.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.pctFinancial.Name = "pctFinancial";
            this.pctFinancial.Size = new System.Drawing.Size(176, 157);
            this.pctFinancial.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pctFinancial.TabIndex = 59;
            this.pctFinancial.TabStop = false;
            this.pctFinancial.Click += new System.EventHandler(this.pctFinancial_Click);
            // 
            // lblFinancial
            // 
            this.lblFinancial.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lblFinancial.Font = new System.Drawing.Font("Constantia", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFinancial.Location = new System.Drawing.Point(50, 230);
            this.lblFinancial.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblFinancial.Name = "lblFinancial";
            this.lblFinancial.Size = new System.Drawing.Size(124, 41);
            this.lblFinancial.TabIndex = 58;
            this.lblFinancial.Text = "Financial";
            this.lblFinancial.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblPhotos
            // 
            this.lblPhotos.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lblPhotos.Font = new System.Drawing.Font("Constantia", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPhotos.Location = new System.Drawing.Point(261, 230);
            this.lblPhotos.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblPhotos.Name = "lblPhotos";
            this.lblPhotos.Size = new System.Drawing.Size(124, 41);
            this.lblPhotos.TabIndex = 53;
            this.lblPhotos.Text = "Photos";
            this.lblPhotos.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblClients
            // 
            this.lblClients.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lblClients.Font = new System.Drawing.Font("Constantia", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblClients.Location = new System.Drawing.Point(458, 230);
            this.lblClients.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblClients.Name = "lblClients";
            this.lblClients.Size = new System.Drawing.Size(124, 41);
            this.lblClients.TabIndex = 55;
            this.lblClients.Text = "Clients";
            this.lblClients.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // pctClients
            // 
            this.pctClients.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.pctClients.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pctClients.Image = ((System.Drawing.Image)(resources.GetObject("pctClients.Image")));
            this.pctClients.Location = new System.Drawing.Point(437, 71);
            this.pctClients.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.pctClients.Name = "pctClients";
            this.pctClients.Size = new System.Drawing.Size(176, 157);
            this.pctClients.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pctClients.TabIndex = 57;
            this.pctClients.TabStop = false;
            this.pctClients.Click += new System.EventHandler(this.pctClients_Click);
            // 
            // pctPhoto
            // 
            this.pctPhoto.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.pctPhoto.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pctPhoto.Image = ((System.Drawing.Image)(resources.GetObject("pctPhoto.Image")));
            this.pctPhoto.Location = new System.Drawing.Point(231, 71);
            this.pctPhoto.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.pctPhoto.Name = "pctPhoto";
            this.pctPhoto.Size = new System.Drawing.Size(176, 157);
            this.pctPhoto.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pctPhoto.TabIndex = 56;
            this.pctPhoto.TabStop = false;
            this.pctPhoto.Click += new System.EventHandler(this.pctPhoto_Click);
            // 
            // pnlTitle
            // 
            this.pnlTitle.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlTitle.AutoSize = true;
            this.pnlTitle.BackColor = System.Drawing.SystemColors.MenuHighlight;
            this.pnlTitle.Controls.Add(this.lblTitle);
            this.pnlTitle.Location = new System.Drawing.Point(-3, 0);
            this.pnlTitle.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.pnlTitle.Name = "pnlTitle";
            this.pnlTitle.Size = new System.Drawing.Size(645, 67);
            this.pnlTitle.TabIndex = 38;
            // 
            // lblTitle
            // 
            this.lblTitle.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Constantia", 36F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitle.ForeColor = System.Drawing.Color.GreenYellow;
            this.lblTitle.Location = new System.Drawing.Point(195, 3);
            this.lblTitle.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(273, 59);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "Navigation";
            this.lblTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // frmPhotographerNavigation
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(635, 305);
            this.Controls.Add(this.pnlAdmin);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.Name = "frmPhotographerNavigation";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Navigation";
            this.pnlAdmin.ResumeLayout(false);
            this.pnlAdmin.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pctFinancial)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pctClients)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pctPhoto)).EndInit();
            this.pnlTitle.ResumeLayout(false);
            this.pnlTitle.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlAdmin;
        private System.Windows.Forms.Button btnLogout;
        private System.Windows.Forms.PictureBox pctFinancial;
        private System.Windows.Forms.Label lblFinancial;
        private System.Windows.Forms.Label lblPhotos;
        private System.Windows.Forms.Label lblClients;
        private System.Windows.Forms.PictureBox pctClients;
        private System.Windows.Forms.PictureBox pctPhoto;
        private System.Windows.Forms.Panel pnlTitle;
        private System.Windows.Forms.Label lblTitle;
    }
}