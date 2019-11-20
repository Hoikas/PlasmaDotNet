namespace VaultMangler {
    partial class LoginForm {
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
            this.fAccountBox = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.fPasswordBox = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.fServerComboBox = new System.Windows.Forms.ComboBox();
            this.fLoginButton = new System.Windows.Forms.Button();
            this.fCancelButton = new System.Windows.Forms.Button();
            this.fRemember = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 18);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(47, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Account";
            // 
            // fAccountBox
            // 
            this.fAccountBox.Location = new System.Drawing.Point(69, 15);
            this.fAccountBox.Name = "fAccountBox";
            this.fAccountBox.Size = new System.Drawing.Size(167, 20);
            this.fAccountBox.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 44);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Password";
            // 
            // fPasswordBox
            // 
            this.fPasswordBox.Location = new System.Drawing.Point(69, 41);
            this.fPasswordBox.Name = "fPasswordBox";
            this.fPasswordBox.Size = new System.Drawing.Size(167, 20);
            this.fPasswordBox.TabIndex = 3;
            this.fPasswordBox.UseSystemPasswordChar = true;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(25, 70);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(38, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "Server";
            // 
            // fServerComboBox
            // 
            this.fServerComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.fServerComboBox.FormattingEnabled = true;
            this.fServerComboBox.Location = new System.Drawing.Point(69, 67);
            this.fServerComboBox.Name = "fServerComboBox";
            this.fServerComboBox.Size = new System.Drawing.Size(167, 21);
            this.fServerComboBox.TabIndex = 5;
            // 
            // fLoginButton
            // 
            this.fLoginButton.Location = new System.Drawing.Point(69, 117);
            this.fLoginButton.Name = "fLoginButton";
            this.fLoginButton.Size = new System.Drawing.Size(75, 23);
            this.fLoginButton.TabIndex = 7;
            this.fLoginButton.Text = "Login";
            this.fLoginButton.UseVisualStyleBackColor = true;
            this.fLoginButton.Click += new System.EventHandler(this.ILogin);
            // 
            // fCancelButton
            // 
            this.fCancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.fCancelButton.Location = new System.Drawing.Point(161, 117);
            this.fCancelButton.Name = "fCancelButton";
            this.fCancelButton.Size = new System.Drawing.Size(75, 23);
            this.fCancelButton.TabIndex = 8;
            this.fCancelButton.Text = "Cancel";
            this.fCancelButton.UseVisualStyleBackColor = true;
            // 
            // fRemember
            // 
            this.fRemember.AutoSize = true;
            this.fRemember.Location = new System.Drawing.Point(69, 94);
            this.fRemember.Name = "fRemember";
            this.fRemember.Size = new System.Drawing.Size(147, 17);
            this.fRemember.TabIndex = 9;
            this.fRemember.Text = "Rember Login Information";
            this.fRemember.UseVisualStyleBackColor = true;
            // 
            // LoginForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(248, 151);
            this.Controls.Add(this.fRemember);
            this.Controls.Add(this.fCancelButton);
            this.Controls.Add(this.fLoginButton);
            this.Controls.Add(this.fServerComboBox);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.fPasswordBox);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.fAccountBox);
            this.Controls.Add(this.label1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "LoginForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.Text = "Login";
            this.Shown += new System.EventHandler(this.IOnFormShown);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox fAccountBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox fPasswordBox;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox fServerComboBox;
        private System.Windows.Forms.Button fLoginButton;
        private System.Windows.Forms.Button fCancelButton;
        private System.Windows.Forms.CheckBox fRemember;
    }
}