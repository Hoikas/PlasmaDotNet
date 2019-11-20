namespace VaultMangler.Controls {
    partial class VaultTreeControl {
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
            this.components = new System.ComponentModel.Container();
            this.fTreeView = new System.Windows.Forms.TreeView();
            this.fNodeContextMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.fAddChildMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.fCreateChildMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.fCreateFolderNodeMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.fCreateImageNodeMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.fCreateTextNodeMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.fNodeContextMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // fTreeView
            // 
            this.fTreeView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.fTreeView.Location = new System.Drawing.Point(0, 0);
            this.fTreeView.Name = "fTreeView";
            this.fTreeView.Size = new System.Drawing.Size(273, 356);
            this.fTreeView.TabIndex = 0;
            this.fTreeView.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.IOnNodeSelect);
            // 
            // fNodeContextMenu
            // 
            this.fNodeContextMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fAddChildMenuItem,
            this.fCreateChildMenuItem});
            this.fNodeContextMenu.Name = "fNodeContextMenu";
            this.fNodeContextMenu.Size = new System.Drawing.Size(153, 70);
            // 
            // fAddChildMenuItem
            // 
            this.fAddChildMenuItem.Name = "fAddChildMenuItem";
            this.fAddChildMenuItem.Size = new System.Drawing.Size(152, 22);
            this.fAddChildMenuItem.Text = "Add Child";
            this.fAddChildMenuItem.Click += new System.EventHandler(this.IAddChildNode);
            // 
            // fCreateChildMenuItem
            // 
            this.fCreateChildMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fCreateFolderNodeMenuItem,
            this.fCreateImageNodeMenuItem,
            this.fCreateTextNodeMenuItem});
            this.fCreateChildMenuItem.Name = "fCreateChildMenuItem";
            this.fCreateChildMenuItem.Size = new System.Drawing.Size(152, 22);
            this.fCreateChildMenuItem.Text = "Create Child";
            // 
            // fCreateFolderNodeMenuItem
            // 
            this.fCreateFolderNodeMenuItem.Name = "fCreateFolderNodeMenuItem";
            this.fCreateFolderNodeMenuItem.Size = new System.Drawing.Size(139, 22);
            this.fCreateFolderNodeMenuItem.Text = "Folder Node";
            this.fCreateFolderNodeMenuItem.Click += new System.EventHandler(this.ICreateChildFolderNode);
            // 
            // fCreateImageNodeMenuItem
            // 
            this.fCreateImageNodeMenuItem.Name = "fCreateImageNodeMenuItem";
            this.fCreateImageNodeMenuItem.Size = new System.Drawing.Size(139, 22);
            this.fCreateImageNodeMenuItem.Text = "Image Node";
            this.fCreateImageNodeMenuItem.Click += new System.EventHandler(this.ICreateChildImageNode);
            // 
            // fCreateTextNodeMenuItem
            // 
            this.fCreateTextNodeMenuItem.Name = "fCreateTextNodeMenuItem";
            this.fCreateTextNodeMenuItem.Size = new System.Drawing.Size(139, 22);
            this.fCreateTextNodeMenuItem.Text = "Text Node";
            this.fCreateTextNodeMenuItem.Click += new System.EventHandler(this.ICreateChildTextNode);
            // 
            // VaultTreeControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.fTreeView);
            this.DoubleBuffered = true;
            this.Name = "VaultTreeControl";
            this.Size = new System.Drawing.Size(273, 356);
            this.fNodeContextMenu.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TreeView fTreeView;
        private System.Windows.Forms.ContextMenuStrip fNodeContextMenu;
        private System.Windows.Forms.ToolStripMenuItem fAddChildMenuItem;
        private System.Windows.Forms.ToolStripMenuItem fCreateChildMenuItem;
        private System.Windows.Forms.ToolStripMenuItem fCreateFolderNodeMenuItem;
        private System.Windows.Forms.ToolStripMenuItem fCreateImageNodeMenuItem;
        private System.Windows.Forms.ToolStripMenuItem fCreateTextNodeMenuItem;
    }
}
