namespace VaultMangler {
    partial class AvatarForm {
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            this.fAvatarBox = new System.Windows.Forms.ListBox();
            this.fOkButton = new System.Windows.Forms.Button();
            this.fCancelButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // fAvatarBox
            // 
            this.fAvatarBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.fAvatarBox.FormattingEnabled = true;
            this.fAvatarBox.Location = new System.Drawing.Point(12, 12);
            this.fAvatarBox.Name = "fAvatarBox";
            this.fAvatarBox.Size = new System.Drawing.Size(260, 147);
            this.fAvatarBox.Sorted = true;
            this.fAvatarBox.TabIndex = 0;
            // 
            // fOkButton
            // 
            this.fOkButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.fOkButton.Location = new System.Drawing.Point(68, 165);
            this.fOkButton.Name = "fOkButton";
            this.fOkButton.Size = new System.Drawing.Size(75, 23);
            this.fOkButton.TabIndex = 1;
            this.fOkButton.Text = "OK";
            this.fOkButton.UseVisualStyleBackColor = true;
            this.fOkButton.Click += new System.EventHandler(this.IOkayButtonClick);
            // 
            // fCancelButton
            // 
            this.fCancelButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.fCancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.fCancelButton.Location = new System.Drawing.Point(149, 165);
            this.fCancelButton.Name = "fCancelButton";
            this.fCancelButton.Size = new System.Drawing.Size(75, 23);
            this.fCancelButton.TabIndex = 2;
            this.fCancelButton.Text = "Cancel";
            this.fCancelButton.UseVisualStyleBackColor = true;
            // 
            // AvatarForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 196);
            this.Controls.Add(this.fCancelButton);
            this.Controls.Add(this.fOkButton);
            this.Controls.Add(this.fAvatarBox);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "AvatarForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.Text = "Select an Avatar";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListBox fAvatarBox;
        private System.Windows.Forms.Button fOkButton;
        private System.Windows.Forms.Button fCancelButton;
    }
}