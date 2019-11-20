namespace VaultMangler {
    partial class MainForm {
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
            this.fMainMenu = new System.Windows.Forms.MenuStrip();
            this.fFileMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.fLoginMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.fLogoutMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.fFileMenuSep1 = new System.Windows.Forms.ToolStripSeparator();
            this.fExitMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.fHelpMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.fStatusBar = new System.Windows.Forms.StatusStrip();
            this.fStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.fSplitPanel = new System.Windows.Forms.SplitContainer();
            this.fVaultTree = new VaultMangler.Controls.VaultTreeControl();
            this.fNodeControl = new VaultMangler.Controls.OverallNodeControl();
            this.fMainMenu.SuspendLayout();
            this.fStatusBar.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.fSplitPanel)).BeginInit();
            this.fSplitPanel.Panel1.SuspendLayout();
            this.fSplitPanel.Panel2.SuspendLayout();
            this.fSplitPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // fMainMenu
            // 
            this.fMainMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fFileMenu,
            this.fHelpMenu});
            this.fMainMenu.Location = new System.Drawing.Point(0, 0);
            this.fMainMenu.Name = "fMainMenu";
            this.fMainMenu.Size = new System.Drawing.Size(534, 24);
            this.fMainMenu.TabIndex = 0;
            this.fMainMenu.Text = "fMenuStrip";
            // 
            // fFileMenu
            // 
            this.fFileMenu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fLoginMenuItem,
            this.fLogoutMenuItem,
            this.fFileMenuSep1,
            this.fExitMenuItem});
            this.fFileMenu.Name = "fFileMenu";
            this.fFileMenu.Size = new System.Drawing.Size(37, 20);
            this.fFileMenu.Text = "&File";
            // 
            // fLoginMenuItem
            // 
            this.fLoginMenuItem.Name = "fLoginMenuItem";
            this.fLoginMenuItem.Size = new System.Drawing.Size(152, 22);
            this.fLoginMenuItem.Text = "Log&in";
            this.fLoginMenuItem.Click += new System.EventHandler(this.ILogin);
            // 
            // fLogoutMenuItem
            // 
            this.fLogoutMenuItem.Name = "fLogoutMenuItem";
            this.fLogoutMenuItem.Size = new System.Drawing.Size(152, 22);
            this.fLogoutMenuItem.Text = "Log&out";
            this.fLogoutMenuItem.Visible = false;
            this.fLogoutMenuItem.Click += new System.EventHandler(this.ILogout);
            // 
            // fFileMenuSep1
            // 
            this.fFileMenuSep1.Name = "fFileMenuSep1";
            this.fFileMenuSep1.Size = new System.Drawing.Size(149, 6);
            // 
            // fExitMenuItem
            // 
            this.fExitMenuItem.Name = "fExitMenuItem";
            this.fExitMenuItem.Size = new System.Drawing.Size(152, 22);
            this.fExitMenuItem.Text = "E&xit";
            this.fExitMenuItem.Click += new System.EventHandler(this.IQuit);
            // 
            // fHelpMenu
            // 
            this.fHelpMenu.Name = "fHelpMenu";
            this.fHelpMenu.Size = new System.Drawing.Size(44, 20);
            this.fHelpMenu.Text = "&Help";
            // 
            // fStatusBar
            // 
            this.fStatusBar.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fStatusLabel});
            this.fStatusBar.Location = new System.Drawing.Point(0, 539);
            this.fStatusBar.Name = "fStatusBar";
            this.fStatusBar.Size = new System.Drawing.Size(534, 22);
            this.fStatusBar.TabIndex = 1;
            this.fStatusBar.Text = "statusStrip1";
            // 
            // fStatusLabel
            // 
            this.fStatusLabel.Name = "fStatusLabel";
            this.fStatusLabel.Size = new System.Drawing.Size(0, 17);
            // 
            // fSplitPanel
            // 
            this.fSplitPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.fSplitPanel.Location = new System.Drawing.Point(0, 24);
            this.fSplitPanel.Name = "fSplitPanel";
            // 
            // fSplitPanel.Panel1
            // 
            this.fSplitPanel.Panel1.Controls.Add(this.fVaultTree);
            // 
            // fSplitPanel.Panel2
            // 
            this.fSplitPanel.Panel2.Controls.Add(this.fNodeControl);
            this.fSplitPanel.Size = new System.Drawing.Size(534, 515);
            this.fSplitPanel.SplitterDistance = 204;
            this.fSplitPanel.TabIndex = 2;
            // 
            // fVaultTree
            // 
            this.fVaultTree.Dock = System.Windows.Forms.DockStyle.Fill;
            this.fVaultTree.Location = new System.Drawing.Point(0, 0);
            this.fVaultTree.Name = "fVaultTree";
            this.fVaultTree.Size = new System.Drawing.Size(204, 515);
            this.fVaultTree.TabIndex = 0;
            // 
            // fNodeControl
            // 
            this.fNodeControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.fNodeControl.Location = new System.Drawing.Point(0, 0);
            this.fNodeControl.Name = "fNodeControl";
            this.fNodeControl.Size = new System.Drawing.Size(326, 515);
            this.fNodeControl.TabIndex = 0;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(534, 561);
            this.Controls.Add(this.fSplitPanel);
            this.Controls.Add(this.fStatusBar);
            this.Controls.Add(this.fMainMenu);
            this.DoubleBuffered = true;
            this.MainMenuStrip = this.fMainMenu;
            this.Name = "MainForm";
            this.Text = "Vault Mangler";
            this.fMainMenu.ResumeLayout(false);
            this.fMainMenu.PerformLayout();
            this.fStatusBar.ResumeLayout(false);
            this.fStatusBar.PerformLayout();
            this.fSplitPanel.Panel1.ResumeLayout(false);
            this.fSplitPanel.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.fSplitPanel)).EndInit();
            this.fSplitPanel.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip fMainMenu;
        private System.Windows.Forms.ToolStripMenuItem fFileMenu;
        private System.Windows.Forms.ToolStripMenuItem fHelpMenu;
        private System.Windows.Forms.StatusStrip fStatusBar;
        private System.Windows.Forms.ToolStripStatusLabel fStatusLabel;
        private System.Windows.Forms.ToolStripMenuItem fLoginMenuItem;
        private System.Windows.Forms.SplitContainer fSplitPanel;
        private Controls.VaultTreeControl fVaultTree;
        private Controls.OverallNodeControl fNodeControl;
        private System.Windows.Forms.ToolStripMenuItem fLogoutMenuItem;
        private System.Windows.Forms.ToolStripSeparator fFileMenuSep1;
        private System.Windows.Forms.ToolStripMenuItem fExitMenuItem;
    }
}

