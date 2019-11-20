namespace VaultMangler.Controls {
    partial class OverallNodeControl {
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
            this.fNodeDetailPanel = new System.Windows.Forms.Panel();
            this.fBasicInfo = new VaultMangler.Controls.BasicNodeControl();
            this.fSaveButton = new System.Windows.Forms.Button();
            this.fResetButton = new System.Windows.Forms.Button();
            this.linkLabel1 = new System.Windows.Forms.LinkLabel();
            this.SuspendLayout();
            // 
            // fNodeDetailPanel
            // 
            this.fNodeDetailPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.fNodeDetailPanel.Location = new System.Drawing.Point(0, 109);
            this.fNodeDetailPanel.Name = "fNodeDetailPanel";
            this.fNodeDetailPanel.Size = new System.Drawing.Size(446, 259);
            this.fNodeDetailPanel.TabIndex = 11;
            // 
            // fBasicInfo
            // 
            this.fBasicInfo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.fBasicInfo.Location = new System.Drawing.Point(0, -2);
            this.fBasicInfo.Name = "fBasicInfo";
            this.fBasicInfo.Size = new System.Drawing.Size(443, 105);
            this.fBasicInfo.TabIndex = 10;
            // 
            // fSaveButton
            // 
            this.fSaveButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.fSaveButton.Location = new System.Drawing.Point(204, 372);
            this.fSaveButton.Name = "fSaveButton";
            this.fSaveButton.Size = new System.Drawing.Size(75, 23);
            this.fSaveButton.TabIndex = 12;
            this.fSaveButton.Text = "Save";
            this.fSaveButton.UseVisualStyleBackColor = true;
            this.fSaveButton.Click += new System.EventHandler(this.ISaveNode);
            // 
            // fResetButton
            // 
            this.fResetButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.fResetButton.Location = new System.Drawing.Point(285, 372);
            this.fResetButton.Name = "fResetButton";
            this.fResetButton.Size = new System.Drawing.Size(75, 23);
            this.fResetButton.TabIndex = 13;
            this.fResetButton.Text = "Reset";
            this.fResetButton.UseVisualStyleBackColor = true;
            this.fResetButton.Click += new System.EventHandler(this.IResetNode);
            // 
            // linkLabel1
            // 
            this.linkLabel1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.linkLabel1.AutoSize = true;
            this.linkLabel1.Location = new System.Drawing.Point(365, 377);
            this.linkLabel1.Name = "linkLabel1";
            this.linkLabel1.Size = new System.Drawing.Size(81, 13);
            this.linkLabel1.TabIndex = 14;
            this.linkLabel1.TabStop = true;
            this.linkLabel1.Text = "View Raw Data";
            this.linkLabel1.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.IViewNodeDetail);
            // 
            // OverallNodeControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.linkLabel1);
            this.Controls.Add(this.fResetButton);
            this.Controls.Add(this.fSaveButton);
            this.Controls.Add(this.fNodeDetailPanel);
            this.Controls.Add(this.fBasicInfo);
            this.Name = "OverallNodeControl";
            this.Size = new System.Drawing.Size(446, 398);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel fNodeDetailPanel;
        private BasicNodeControl fBasicInfo;
        private System.Windows.Forms.Button fSaveButton;
        private System.Windows.Forms.Button fResetButton;
        private System.Windows.Forms.LinkLabel linkLabel1;
    }
}
