namespace VaultMangler {
    partial class AddChildNodeForm {
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
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.fParentID = new VaultMangler.Controls.NumericTextBox();
            this.fChildID = new VaultMangler.Controls.NumericTextBox();
            this.fSaverID = new VaultMangler.Controls.NumericTextBox();
            this.fSaveButton = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(52, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Parent ID";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(17, 40);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(44, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Child ID";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 66);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(49, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "Saver ID";
            // 
            // fParentID
            // 
            this.fParentID.Location = new System.Drawing.Point(71, 10);
            this.fParentID.Name = "fParentID";
            this.fParentID.Size = new System.Drawing.Size(86, 20);
            this.fParentID.TabIndex = 3;
            this.fParentID.Text = "0";
            // 
            // fChildID
            // 
            this.fChildID.Location = new System.Drawing.Point(71, 37);
            this.fChildID.Name = "fChildID";
            this.fChildID.Size = new System.Drawing.Size(86, 20);
            this.fChildID.TabIndex = 4;
            this.fChildID.Text = "0";
            // 
            // fSaverID
            // 
            this.fSaverID.Location = new System.Drawing.Point(71, 63);
            this.fSaverID.Name = "fSaverID";
            this.fSaverID.Size = new System.Drawing.Size(86, 20);
            this.fSaverID.TabIndex = 5;
            this.fSaverID.Text = "0";
            // 
            // fSaveButton
            // 
            this.fSaveButton.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.fSaveButton.Location = new System.Drawing.Point(12, 95);
            this.fSaveButton.Name = "fSaveButton";
            this.fSaveButton.Size = new System.Drawing.Size(75, 23);
            this.fSaveButton.TabIndex = 6;
            this.fSaveButton.Text = "Add Child";
            this.fSaveButton.UseVisualStyleBackColor = true;
            // 
            // button1
            // 
            this.button1.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.button1.Location = new System.Drawing.Point(90, 95);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 7;
            this.button1.Text = "Cancel";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // AddChildNodeForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(177, 130);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.fSaveButton);
            this.Controls.Add(this.fSaverID);
            this.Controls.Add(this.fChildID);
            this.Controls.Add(this.fParentID);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "AddChildNodeForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.Text = "Add Child";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private Controls.NumericTextBox fParentID;
        private Controls.NumericTextBox fChildID;
        private Controls.NumericTextBox fSaverID;
        private System.Windows.Forms.Button fSaveButton;
        private System.Windows.Forms.Button button1;
    }
}