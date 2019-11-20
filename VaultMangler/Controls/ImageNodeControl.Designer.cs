namespace VaultMangler.Controls {
    partial class ImageNodeControl {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.fExportLink = new System.Windows.Forms.LinkLabel();
            this.fReplaceLink = new System.Windows.Forms.LinkLabel();
            this.fDescription = new System.Windows.Forms.TextBox();
            this.fKiImage = new System.Windows.Forms.PictureBox();
            this.fIsPng = new System.Windows.Forms.CheckBox();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.fKiImage)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.fIsPng);
            this.groupBox1.Controls.Add(this.fExportLink);
            this.groupBox1.Controls.Add(this.fReplaceLink);
            this.groupBox1.Controls.Add(this.fDescription);
            this.groupBox1.Controls.Add(this.fKiImage);
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(481, 257);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Image Node";
            // 
            // fExportLink
            // 
            this.fExportLink.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.fExportLink.AutoSize = true;
            this.fExportLink.Location = new System.Drawing.Point(321, 235);
            this.fExportLink.Name = "fExportLink";
            this.fExportLink.Size = new System.Drawing.Size(69, 13);
            this.fExportLink.TabIndex = 7;
            this.fExportLink.TabStop = true;
            this.fExportLink.Text = "Export Image";
            this.fExportLink.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.IExportImage);
            // 
            // fReplaceLink
            // 
            this.fReplaceLink.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.fReplaceLink.AutoSize = true;
            this.fReplaceLink.Location = new System.Drawing.Point(396, 235);
            this.fReplaceLink.Name = "fReplaceLink";
            this.fReplaceLink.Size = new System.Drawing.Size(79, 13);
            this.fReplaceLink.TabIndex = 6;
            this.fReplaceLink.TabStop = true;
            this.fReplaceLink.Text = "Replace Image";
            this.fReplaceLink.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.IReplaceImage);
            // 
            // fDescription
            // 
            this.fDescription.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.fDescription.Location = new System.Drawing.Point(6, 210);
            this.fDescription.Name = "fDescription";
            this.fDescription.Size = new System.Drawing.Size(469, 20);
            this.fDescription.TabIndex = 5;
            // 
            // fKiImage
            // 
            this.fKiImage.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.fKiImage.Location = new System.Drawing.Point(6, 19);
            this.fKiImage.Name = "fKiImage";
            this.fKiImage.Size = new System.Drawing.Size(469, 184);
            this.fKiImage.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.fKiImage.TabIndex = 4;
            this.fKiImage.TabStop = false;
            // 
            // fIsPng
            // 
            this.fIsPng.AutoSize = true;
            this.fIsPng.Location = new System.Drawing.Point(7, 234);
            this.fIsPng.Name = "fIsPng";
            this.fIsPng.Size = new System.Drawing.Size(129, 17);
            this.fIsPng.TabIndex = 8;
            this.fIsPng.Text = "Lossless Compression";
            this.fIsPng.UseVisualStyleBackColor = true;
            // 
            // ImageNodeControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupBox1);
            this.Name = "ImageNodeControl";
            this.Size = new System.Drawing.Size(481, 260);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.fKiImage)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.LinkLabel fExportLink;
        private System.Windows.Forms.LinkLabel fReplaceLink;
        private System.Windows.Forms.TextBox fDescription;
        private System.Windows.Forms.PictureBox fKiImage;
        private System.Windows.Forms.CheckBox fIsPng;

    }
}
