namespace Shilling_GroupProject
{
    partial class frmFullScreen
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmFullScreen));
            this.pctImage = new System.Windows.Forms.PictureBox();
            this.pctBack = new System.Windows.Forms.PictureBox();
            this.pctDelete = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pctImage)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pctBack)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pctDelete)).BeginInit();
            this.SuspendLayout();
            // 
            // pctImage
            // 
            this.pctImage.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pctImage.Location = new System.Drawing.Point(1, 2);
            this.pctImage.Name = "pctImage";
            this.pctImage.Size = new System.Drawing.Size(1558, 1098);
            this.pctImage.TabIndex = 0;
            this.pctImage.TabStop = false;
            // 
            // pctBack
            // 
            this.pctBack.BackColor = System.Drawing.Color.Transparent;
            this.pctBack.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pctBack.Image = ((System.Drawing.Image)(resources.GetObject("pctBack.Image")));
            this.pctBack.Location = new System.Drawing.Point(27, 21);
            this.pctBack.Name = "pctBack";
            this.pctBack.Size = new System.Drawing.Size(79, 85);
            this.pctBack.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pctBack.TabIndex = 1;
            this.pctBack.TabStop = false;
            this.pctBack.Click += new System.EventHandler(this.pctBack_Click);
            // 
            // pctDelete
            // 
            this.pctDelete.BackColor = System.Drawing.Color.Transparent;
            this.pctDelete.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pctDelete.Image = ((System.Drawing.Image)(resources.GetObject("pctDelete.Image")));
            this.pctDelete.Location = new System.Drawing.Point(27, 112);
            this.pctDelete.Name = "pctDelete";
            this.pctDelete.Size = new System.Drawing.Size(79, 85);
            this.pctDelete.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pctDelete.TabIndex = 2;
            this.pctDelete.TabStop = false;
            this.pctDelete.Click += new System.EventHandler(this.pctDelete_Click);
            // 
            // frmFullScreen
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.ClientSize = new System.Drawing.Size(1559, 1099);
            this.Controls.Add(this.pctDelete);
            this.Controls.Add(this.pctBack);
            this.Controls.Add(this.pctImage);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "frmFullScreen";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "frmFullScreen";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.frmFullScreen_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pctImage)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pctBack)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pctDelete)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox pctImage;
        private System.Windows.Forms.PictureBox pctBack;
        private System.Windows.Forms.PictureBox pctDelete;
    }
}