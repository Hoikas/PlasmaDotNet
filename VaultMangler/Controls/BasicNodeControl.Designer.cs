namespace VaultMangler.Controls {
    partial class BasicNodeControl {
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
            this.fGroupBox = new System.Windows.Forms.GroupBox();
            this.fCreatorIdBox = new System.Windows.Forms.TextBox();
            this.fCreatorIdLabel = new System.Windows.Forms.Label();
            this.fModifyTimeLabel = new System.Windows.Forms.Label();
            this.fCreateTimeLabel = new System.Windows.Forms.Label();
            this.fNodeIdLabel = new System.Windows.Forms.Label();
            this.fNodeIdBox = new System.Windows.Forms.TextBox();
            this.fCreateTimePicker = new System.Windows.Forms.DateTimePicker();
            this.fModifyTimePicker = new System.Windows.Forms.DateTimePicker();
            this.fGroupBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // fGroupBox
            // 
            this.fGroupBox.Controls.Add(this.fCreatorIdBox);
            this.fGroupBox.Controls.Add(this.fCreatorIdLabel);
            this.fGroupBox.Controls.Add(this.fModifyTimeLabel);
            this.fGroupBox.Controls.Add(this.fCreateTimeLabel);
            this.fGroupBox.Controls.Add(this.fNodeIdLabel);
            this.fGroupBox.Controls.Add(this.fNodeIdBox);
            this.fGroupBox.Controls.Add(this.fCreateTimePicker);
            this.fGroupBox.Controls.Add(this.fModifyTimePicker);
            this.fGroupBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.fGroupBox.Location = new System.Drawing.Point(0, 0);
            this.fGroupBox.Name = "fGroupBox";
            this.fGroupBox.Size = new System.Drawing.Size(304, 110);
            this.fGroupBox.TabIndex = 0;
            this.fGroupBox.TabStop = false;
            this.fGroupBox.Text = "Node Information";
            // 
            // fCreatorIdBox
            // 
            this.fCreatorIdBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.fCreatorIdBox.Location = new System.Drawing.Point(220, 26);
            this.fCreatorIdBox.Name = "fCreatorIdBox";
            this.fCreatorIdBox.ReadOnly = true;
            this.fCreatorIdBox.Size = new System.Drawing.Size(74, 20);
            this.fCreatorIdBox.TabIndex = 8;
            // 
            // fCreatorIdLabel
            // 
            this.fCreatorIdLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.fCreatorIdLabel.AutoSize = true;
            this.fCreatorIdLabel.Location = new System.Drawing.Point(159, 29);
            this.fCreatorIdLabel.Name = "fCreatorIdLabel";
            this.fCreatorIdLabel.Size = new System.Drawing.Size(55, 13);
            this.fCreatorIdLabel.TabIndex = 7;
            this.fCreatorIdLabel.Text = "Creator ID";
            // 
            // fModifyTimeLabel
            // 
            this.fModifyTimeLabel.AutoSize = true;
            this.fModifyTimeLabel.Location = new System.Drawing.Point(6, 81);
            this.fModifyTimeLabel.Name = "fModifyTimeLabel";
            this.fModifyTimeLabel.Size = new System.Drawing.Size(64, 13);
            this.fModifyTimeLabel.TabIndex = 5;
            this.fModifyTimeLabel.Text = "Modify Time";
            // 
            // fCreateTimeLabel
            // 
            this.fCreateTimeLabel.AutoSize = true;
            this.fCreateTimeLabel.Location = new System.Drawing.Point(6, 55);
            this.fCreateTimeLabel.Name = "fCreateTimeLabel";
            this.fCreateTimeLabel.Size = new System.Drawing.Size(64, 13);
            this.fCreateTimeLabel.TabIndex = 4;
            this.fCreateTimeLabel.Text = "Create Time";
            // 
            // fNodeIdLabel
            // 
            this.fNodeIdLabel.AutoSize = true;
            this.fNodeIdLabel.Location = new System.Drawing.Point(23, 29);
            this.fNodeIdLabel.Name = "fNodeIdLabel";
            this.fNodeIdLabel.Size = new System.Drawing.Size(47, 13);
            this.fNodeIdLabel.TabIndex = 3;
            this.fNodeIdLabel.Text = "Node ID";
            // 
            // fNodeIdBox
            // 
            this.fNodeIdBox.Location = new System.Drawing.Point(76, 26);
            this.fNodeIdBox.Name = "fNodeIdBox";
            this.fNodeIdBox.ReadOnly = true;
            this.fNodeIdBox.Size = new System.Drawing.Size(74, 20);
            this.fNodeIdBox.TabIndex = 2;
            // 
            // fCreateTimePicker
            // 
            this.fCreateTimePicker.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.fCreateTimePicker.CustomFormat = "MMMM dd, yyyy @ HH:mm:ss ";
            this.fCreateTimePicker.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.fCreateTimePicker.Location = new System.Drawing.Point(76, 52);
            this.fCreateTimePicker.MinDate = new System.DateTime(1970, 1, 1, 0, 0, 0, 0);
            this.fCreateTimePicker.Name = "fCreateTimePicker";
            this.fCreateTimePicker.Size = new System.Drawing.Size(221, 20);
            this.fCreateTimePicker.TabIndex = 1;
            this.fCreateTimePicker.Value = new System.DateTime(1970, 1, 1, 0, 0, 0, 0);
            // 
            // fModifyTimePicker
            // 
            this.fModifyTimePicker.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.fModifyTimePicker.CustomFormat = "MMMM dd, yyyy @ HH:mm:ss ";
            this.fModifyTimePicker.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.fModifyTimePicker.Location = new System.Drawing.Point(76, 78);
            this.fModifyTimePicker.MinDate = new System.DateTime(1970, 1, 1, 0, 0, 0, 0);
            this.fModifyTimePicker.Name = "fModifyTimePicker";
            this.fModifyTimePicker.Size = new System.Drawing.Size(221, 20);
            this.fModifyTimePicker.TabIndex = 0;
            this.fModifyTimePicker.Value = new System.DateTime(1970, 1, 1, 0, 0, 0, 0);
            // 
            // BasicNodeControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.fGroupBox);
            this.Name = "BasicNodeControl";
            this.Size = new System.Drawing.Size(304, 110);
            this.fGroupBox.ResumeLayout(false);
            this.fGroupBox.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox fGroupBox;
        private System.Windows.Forms.DateTimePicker fModifyTimePicker;
        private System.Windows.Forms.Label fCreatorIdLabel;
        private System.Windows.Forms.Label fModifyTimeLabel;
        private System.Windows.Forms.Label fCreateTimeLabel;
        private System.Windows.Forms.Label fNodeIdLabel;
        private System.Windows.Forms.TextBox fNodeIdBox;
        private System.Windows.Forms.DateTimePicker fCreateTimePicker;
        private System.Windows.Forms.TextBox fCreatorIdBox;
    }
}
