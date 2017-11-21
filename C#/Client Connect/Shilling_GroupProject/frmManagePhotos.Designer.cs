namespace Shilling_GroupProject
{
    partial class frmManagePhotos
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmManagePhotos));
            this.btnBack = new System.Windows.Forms.Button();
            this.btnAdd = new System.Windows.Forms.Button();
            this.fpGallery = new System.Windows.Forms.FlowLayoutPanel();
            this.pnlTitle = new System.Windows.Forms.Panel();
            this.lblTitle = new System.Windows.Forms.Label();
            this.cmbPhotographers = new System.Windows.Forms.ComboBox();
            this.cmbClient = new System.Windows.Forms.ComboBox();
            this.lblPhotographer = new System.Windows.Forms.Label();
            this.lblClient = new System.Windows.Forms.Label();
            this.btnRelease = new System.Windows.Forms.Button();
            this.lblStatus = new System.Windows.Forms.Label();
            this.cmbStatus = new System.Windows.Forms.ComboBox();
            this.btnSearch = new System.Windows.Forms.Button();
            this.pnlTitle.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnBack
            // 
            this.btnBack.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.btnBack.BackColor = System.Drawing.SystemColors.MenuHighlight;
            this.btnBack.Font = new System.Drawing.Font("Constantia", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnBack.ForeColor = System.Drawing.Color.GreenYellow;
            this.btnBack.Location = new System.Drawing.Point(394, 908);
            this.btnBack.Name = "btnBack";
            this.btnBack.Size = new System.Drawing.Size(174, 42);
            this.btnBack.TabIndex = 32;
            this.btnBack.Text = "BACK";
            this.btnBack.UseVisualStyleBackColor = false;
            this.btnBack.Click += new System.EventHandler(this.btnBack_Click);
            // 
            // btnAdd
            // 
            this.btnAdd.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.btnAdd.BackColor = System.Drawing.SystemColors.MenuHighlight;
            this.btnAdd.Font = new System.Drawing.Font("Constantia", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAdd.ForeColor = System.Drawing.Color.GreenYellow;
            this.btnAdd.Location = new System.Drawing.Point(603, 908);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(174, 42);
            this.btnAdd.TabIndex = 33;
            this.btnAdd.Text = "ADD";
            this.btnAdd.UseVisualStyleBackColor = false;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // fpGallery
            // 
            this.fpGallery.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.fpGallery.AutoScroll = true;
            this.fpGallery.BackColor = System.Drawing.Color.LightGray;
            this.fpGallery.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.fpGallery.Location = new System.Drawing.Point(44, 220);
            this.fpGallery.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.fpGallery.Name = "fpGallery";
            this.fpGallery.Size = new System.Drawing.Size(1278, 660);
            this.fpGallery.TabIndex = 34;
            // 
            // pnlTitle
            // 
            this.pnlTitle.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlTitle.AutoSize = true;
            this.pnlTitle.BackColor = System.Drawing.SystemColors.MenuHighlight;
            this.pnlTitle.Controls.Add(this.lblTitle);
            this.pnlTitle.Location = new System.Drawing.Point(-9, 0);
            this.pnlTitle.Name = "pnlTitle";
            this.pnlTitle.Size = new System.Drawing.Size(1442, 103);
            this.pnlTitle.TabIndex = 35;
            // 
            // lblTitle
            // 
            this.lblTitle.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Constantia", 25.875F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitle.ForeColor = System.Drawing.Color.GreenYellow;
            this.lblTitle.Location = new System.Drawing.Point(564, 25);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(208, 64);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "Gallery";
            this.lblTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // cmbPhotographers
            // 
            this.cmbPhotographers.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.cmbPhotographers.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbPhotographers.Font = new System.Drawing.Font("Constantia", 12F);
            this.cmbPhotographers.FormattingEnabled = true;
            this.cmbPhotographers.Location = new System.Drawing.Point(68, 157);
            this.cmbPhotographers.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.cmbPhotographers.Name = "cmbPhotographers";
            this.cmbPhotographers.Size = new System.Drawing.Size(380, 37);
            this.cmbPhotographers.TabIndex = 36;
            this.cmbPhotographers.SelectionChangeCommitted += new System.EventHandler(this.cmbPhotographers_SelectionChangeCommitted);
            // 
            // cmbClient
            // 
            this.cmbClient.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.cmbClient.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbClient.Font = new System.Drawing.Font("Constantia", 12F);
            this.cmbClient.FormattingEnabled = true;
            this.cmbClient.Location = new System.Drawing.Point(459, 157);
            this.cmbClient.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.cmbClient.Name = "cmbClient";
            this.cmbClient.Size = new System.Drawing.Size(319, 37);
            this.cmbClient.TabIndex = 37;
            // 
            // lblPhotographer
            // 
            this.lblPhotographer.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.lblPhotographer.AutoSize = true;
            this.lblPhotographer.Font = new System.Drawing.Font("Constantia", 12F);
            this.lblPhotographer.Location = new System.Drawing.Point(62, 123);
            this.lblPhotographer.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblPhotographer.Name = "lblPhotographer";
            this.lblPhotographer.Size = new System.Drawing.Size(156, 29);
            this.lblPhotographer.TabIndex = 38;
            this.lblPhotographer.Text = "Photographer";
            // 
            // lblClient
            // 
            this.lblClient.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.lblClient.AutoSize = true;
            this.lblClient.Font = new System.Drawing.Font("Constantia", 12F);
            this.lblClient.Location = new System.Drawing.Point(453, 123);
            this.lblClient.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblClient.Name = "lblClient";
            this.lblClient.Size = new System.Drawing.Size(77, 29);
            this.lblClient.TabIndex = 39;
            this.lblClient.Text = "Client";
            // 
            // btnRelease
            // 
            this.btnRelease.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.btnRelease.BackColor = System.Drawing.SystemColors.MenuHighlight;
            this.btnRelease.Font = new System.Drawing.Font("Constantia", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRelease.ForeColor = System.Drawing.Color.GreenYellow;
            this.btnRelease.Location = new System.Drawing.Point(798, 908);
            this.btnRelease.Name = "btnRelease";
            this.btnRelease.Size = new System.Drawing.Size(258, 42);
            this.btnRelease.TabIndex = 40;
            this.btnRelease.Text = "Release Photos";
            this.btnRelease.UseVisualStyleBackColor = false;
            this.btnRelease.Click += new System.EventHandler(this.btnRelease_Click);
            // 
            // lblStatus
            // 
            this.lblStatus.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.lblStatus.AutoSize = true;
            this.lblStatus.Font = new System.Drawing.Font("Constantia", 12F);
            this.lblStatus.Location = new System.Drawing.Point(792, 123);
            this.lblStatus.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(144, 29);
            this.lblStatus.TabIndex = 42;
            this.lblStatus.Text = "Photo Status";
            // 
            // cmbStatus
            // 
            this.cmbStatus.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.cmbStatus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbStatus.Font = new System.Drawing.Font("Constantia", 12F);
            this.cmbStatus.FormattingEnabled = true;
            this.cmbStatus.Items.AddRange(new object[] {
            "All",
            "Released",
            "Unreleased"});
            this.cmbStatus.Location = new System.Drawing.Point(789, 157);
            this.cmbStatus.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.cmbStatus.Name = "cmbStatus";
            this.cmbStatus.Size = new System.Drawing.Size(328, 37);
            this.cmbStatus.TabIndex = 41;
            // 
            // btnSearch
            // 
            this.btnSearch.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.btnSearch.BackColor = System.Drawing.SystemColors.MenuHighlight;
            this.btnSearch.Font = new System.Drawing.Font("Constantia", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSearch.ForeColor = System.Drawing.Color.GreenYellow;
            this.btnSearch.Location = new System.Drawing.Point(1126, 154);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(220, 43);
            this.btnSearch.TabIndex = 43;
            this.btnSearch.Text = "Search";
            this.btnSearch.UseVisualStyleBackColor = false;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // frmManagePhotos
            // 
            this.AcceptButton = this.btnAdd;
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlDark;
            this.ClientSize = new System.Drawing.Size(1365, 975);
            this.Controls.Add(this.btnSearch);
            this.Controls.Add(this.cmbStatus);
            this.Controls.Add(this.lblStatus);
            this.Controls.Add(this.btnRelease);
            this.Controls.Add(this.lblClient);
            this.Controls.Add(this.lblPhotographer);
            this.Controls.Add(this.cmbClient);
            this.Controls.Add(this.cmbPhotographers);
            this.Controls.Add(this.pnlTitle);
            this.Controls.Add(this.fpGallery);
            this.Controls.Add(this.btnAdd);
            this.Controls.Add(this.btnBack);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "frmManagePhotos";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Manage Photos";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.frmManagePhotos_Load);
            this.pnlTitle.ResumeLayout(false);
            this.pnlTitle.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button btnBack;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.FlowLayoutPanel fpGallery;
        private System.Windows.Forms.Panel pnlTitle;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.ComboBox cmbPhotographers;
        private System.Windows.Forms.ComboBox cmbClient;
        private System.Windows.Forms.Label lblPhotographer;
        private System.Windows.Forms.Label lblClient;
        private System.Windows.Forms.Button btnRelease;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.ComboBox cmbStatus;
        private System.Windows.Forms.Button btnSearch;
    }
}