namespace VaultMangler.Controls {
    partial class MarkerNodeControl {
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
            this.fImportLink = new System.Windows.Forms.LinkLabel();
            this.fExportLink = new System.Windows.Forms.LinkLabel();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.fGameRewardTextBox = new System.Windows.Forms.TextBox();
            this.fGameNameTextBox = new System.Windows.Forms.TextBox();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.fImportLink);
            this.groupBox1.Controls.Add(this.fExportLink);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.fGameRewardTextBox);
            this.groupBox1.Controls.Add(this.fGameNameTextBox);
            this.groupBox1.Location = new System.Drawing.Point(4, 4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(355, 85);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Marker Game";
            // 
            // fImportLink
            // 
            this.fImportLink.AutoSize = true;
            this.fImportLink.Enabled = false;
            this.fImportLink.Location = new System.Drawing.Point(157, 67);
            this.fImportLink.Name = "fImportLink";
            this.fImportLink.Size = new System.Drawing.Size(67, 13);
            this.fImportLink.TabIndex = 6;
            this.fImportLink.TabStop = true;
            this.fImportLink.Text = "Import Game";
            // 
            // fExportLink
            // 
            this.fExportLink.AutoSize = true;
            this.fExportLink.Location = new System.Drawing.Point(83, 67);
            this.fExportLink.Name = "fExportLink";
            this.fExportLink.Size = new System.Drawing.Size(68, 13);
            this.fExportLink.TabIndex = 5;
            this.fExportLink.TabStop = true;
            this.fExportLink.Text = "Export Game";
            this.fExportLink.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.IExportGameAsYAML);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(7, 47);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(75, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Game Reward";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(7, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(66, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Game Name";
            // 
            // fGameRewardTextBox
            // 
            this.fGameRewardTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.fGameRewardTextBox.Location = new System.Drawing.Point(86, 44);
            this.fGameRewardTextBox.Name = "fGameRewardTextBox";
            this.fGameRewardTextBox.Size = new System.Drawing.Size(263, 20);
            this.fGameRewardTextBox.TabIndex = 2;
            // 
            // fGameNameTextBox
            // 
            this.fGameNameTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.fGameNameTextBox.Location = new System.Drawing.Point(86, 17);
            this.fGameNameTextBox.Name = "fGameNameTextBox";
            this.fGameNameTextBox.Size = new System.Drawing.Size(263, 20);
            this.fGameNameTextBox.TabIndex = 1;
            // 
            // MarkerNodeControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupBox1);
            this.Name = "MarkerNodeControl";
            this.Size = new System.Drawing.Size(362, 92);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.LinkLabel fImportLink;
        private System.Windows.Forms.LinkLabel fExportLink;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox fGameRewardTextBox;
        private System.Windows.Forms.TextBox fGameNameTextBox;
    }
}
