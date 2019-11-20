namespace VaultMangler.Controls {
    partial class PlayerInfoControl {
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
            this.fAgeInstanceUuid = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.fAgeInstanceName = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.fOnline = new System.Windows.Forms.CheckBox();
            this.fPlayerId = new VaultMangler.Controls.NumericTextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.fPlayerName = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.fAgeInstanceUuid);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.fAgeInstanceName);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.fOnline);
            this.groupBox1.Controls.Add(this.fPlayerId);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.fPlayerName);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(4, 4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(303, 130);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Player Info Node";
            // 
            // fAgeInstanceUuid
            // 
            this.fAgeInstanceUuid.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.fAgeInstanceUuid.Location = new System.Drawing.Point(80, 95);
            this.fAgeInstanceUuid.Name = "fAgeInstanceUuid";
            this.fAgeInstanceUuid.Size = new System.Drawing.Size(217, 20);
            this.fAgeInstanceUuid.TabIndex = 10;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(8, 98);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(51, 13);
            this.label5.TabIndex = 9;
            this.label5.Text = "Age Uuid";
            // 
            // fAgeInstanceName
            // 
            this.fAgeInstanceName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.fAgeInstanceName.Location = new System.Drawing.Point(80, 70);
            this.fAgeInstanceName.Name = "fAgeInstanceName";
            this.fAgeInstanceName.Size = new System.Drawing.Size(217, 20);
            this.fAgeInstanceName.TabIndex = 8;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(8, 70);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(63, 13);
            this.label4.TabIndex = 7;
            this.label4.Text = "Current Age";
            // 
            // fOnline
            // 
            this.fOnline.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.fOnline.AutoSize = true;
            this.fOnline.Location = new System.Drawing.Point(241, 47);
            this.fOnline.Name = "fOnline";
            this.fOnline.Size = new System.Drawing.Size(56, 17);
            this.fOnline.TabIndex = 4;
            this.fOnline.Text = "Online";
            this.fOnline.UseVisualStyleBackColor = true;
            this.fOnline.CheckedChanged += new System.EventHandler(this.IOnlineChecked);
            // 
            // fPlayerId
            // 
            this.fPlayerId.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.fPlayerId.Location = new System.Drawing.Point(80, 44);
            this.fPlayerId.Name = "fPlayerId";
            this.fPlayerId.Size = new System.Drawing.Size(150, 20);
            this.fPlayerId.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(8, 44);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(50, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Player ID";
            // 
            // fPlayerName
            // 
            this.fPlayerName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.fPlayerName.Location = new System.Drawing.Point(80, 17);
            this.fPlayerName.Name = "fPlayerName";
            this.fPlayerName.Size = new System.Drawing.Size(217, 20);
            this.fPlayerName.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(7, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(67, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Player Name";
            // 
            // PlayerInfoControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupBox1);
            this.Name = "PlayerInfoControl";
            this.Size = new System.Drawing.Size(316, 143);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox fPlayerName;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox fAgeInstanceName;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.CheckBox fOnline;
        private NumericTextBox fPlayerId;
        private System.Windows.Forms.TextBox fAgeInstanceUuid;
        private System.Windows.Forms.Label label5;
    }
}
