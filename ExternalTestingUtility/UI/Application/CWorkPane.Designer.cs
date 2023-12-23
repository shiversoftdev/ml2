
namespace ML2.UI.Application
{
    partial class CWorkPane
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.WindowsFormsIsDOGSHIT = new ML2.UI.Application.ZoneTreePanel();
            this.ProjectTree = new System.Windows.Forms.TreeView();
            this.BuildConfigPane = new ML2.UI.Application.CBuildConfigPane();
            this.ThisShitIsAJOKE = new ML2.UI.Application.ConsolePanel();
            this.ConsoleBox = new System.Windows.Forms.RichTextBox();
            this.ZoneTreeRightClick = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.openFolderToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.RenameProjectButton = new System.Windows.Forms.ToolStripMenuItem();
            this.ConsoleContextMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.selectAllToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.copyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.clearConsoleToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.addZoneToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            this.WindowsFormsIsDOGSHIT.SuspendLayout();
            this.ThisShitIsAJOKE.SuspendLayout();
            this.ZoneTreeRightClick.SuspendLayout();
            this.ConsoleContextMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.splitContainer2);
            this.splitContainer1.Panel1MinSize = 200;
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.ThisShitIsAJOKE);
            this.splitContainer1.Panel2MinSize = 100;
            this.splitContainer1.Size = new System.Drawing.Size(1020, 682);
            this.splitContainer1.SplitterDistance = 340;
            this.splitContainer1.TabIndex = 0;
            // 
            // splitContainer2
            // 
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.Location = new System.Drawing.Point(0, 0);
            this.splitContainer2.Name = "splitContainer2";
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.WindowsFormsIsDOGSHIT);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.BuildConfigPane);
            this.splitContainer2.Panel2.Padding = new System.Windows.Forms.Padding(0, 4, 4, 0);
            this.splitContainer2.Size = new System.Drawing.Size(1020, 340);
            this.splitContainer2.SplitterDistance = 225;
            this.splitContainer2.TabIndex = 0;
            // 
            // WindowsFormsIsDOGSHIT
            // 
            this.WindowsFormsIsDOGSHIT.Controls.Add(this.ProjectTree);
            this.WindowsFormsIsDOGSHIT.Dock = System.Windows.Forms.DockStyle.Fill;
            this.WindowsFormsIsDOGSHIT.Location = new System.Drawing.Point(0, 0);
            this.WindowsFormsIsDOGSHIT.Name = "WindowsFormsIsDOGSHIT";
            this.WindowsFormsIsDOGSHIT.Padding = new System.Windows.Forms.Padding(2, 2, 2, 0);
            this.WindowsFormsIsDOGSHIT.Size = new System.Drawing.Size(225, 340);
            this.WindowsFormsIsDOGSHIT.TabIndex = 0;
            // 
            // ProjectTree
            // 
            this.ProjectTree.CheckBoxes = true;
            this.ProjectTree.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ProjectTree.ItemHeight = 20;
            this.ProjectTree.Location = new System.Drawing.Point(2, 2);
            this.ProjectTree.Margin = new System.Windows.Forms.Padding(0);
            this.ProjectTree.Name = "ProjectTree";
            this.ProjectTree.Size = new System.Drawing.Size(221, 338);
            this.ProjectTree.TabIndex = 0;
            // 
            // BuildConfigPane
            // 
            this.BuildConfigPane.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(14)))), ((int)(((byte)(14)))), ((int)(((byte)(18)))));
            this.BuildConfigPane.Dock = System.Windows.Forms.DockStyle.Fill;
            this.BuildConfigPane.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BuildConfigPane.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.BuildConfigPane.Location = new System.Drawing.Point(0, 4);
            this.BuildConfigPane.Name = "BuildConfigPane";
            this.BuildConfigPane.Size = new System.Drawing.Size(787, 336);
            this.BuildConfigPane.TabIndex = 0;
            // 
            // ThisShitIsAJOKE
            // 
            this.ThisShitIsAJOKE.Controls.Add(this.ConsoleBox);
            this.ThisShitIsAJOKE.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ThisShitIsAJOKE.Location = new System.Drawing.Point(0, 0);
            this.ThisShitIsAJOKE.Name = "ThisShitIsAJOKE";
            this.ThisShitIsAJOKE.Padding = new System.Windows.Forms.Padding(4, 6, 4, 4);
            this.ThisShitIsAJOKE.Size = new System.Drawing.Size(1020, 338);
            this.ThisShitIsAJOKE.TabIndex = 0;
            // 
            // ConsoleBox
            // 
            this.ConsoleBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.ConsoleBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ConsoleBox.Location = new System.Drawing.Point(4, 6);
            this.ConsoleBox.Margin = new System.Windows.Forms.Padding(4);
            this.ConsoleBox.Name = "ConsoleBox";
            this.ConsoleBox.ReadOnly = true;
            this.ConsoleBox.Size = new System.Drawing.Size(1012, 328);
            this.ConsoleBox.TabIndex = 0;
            this.ConsoleBox.Text = "";
            // 
            // ZoneTreeRightClick
            // 
            this.ZoneTreeRightClick.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.addZoneToolStripMenuItem,
            this.toolStripSeparator2,
            this.openFolderToolStripMenuItem,
            this.toolStripSeparator3,
            this.RenameProjectButton});
            this.ZoneTreeRightClick.Name = "ZoneTreeRightClick";
            this.ZoneTreeRightClick.Size = new System.Drawing.Size(181, 104);
            // 
            // openFolderToolStripMenuItem
            // 
            this.openFolderToolStripMenuItem.Name = "openFolderToolStripMenuItem";
            this.openFolderToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.openFolderToolStripMenuItem.Text = "Open Project Folder";
            this.openFolderToolStripMenuItem.Click += new System.EventHandler(this.openFolderToolStripMenuItem_Click);
            // 
            // RenameProjectButton
            // 
            this.RenameProjectButton.Name = "RenameProjectButton";
            this.RenameProjectButton.Size = new System.Drawing.Size(180, 22);
            this.RenameProjectButton.Text = "Rename";
            this.RenameProjectButton.Click += new System.EventHandler(this.RenameProjectButton_Click);
            // 
            // ConsoleContextMenu
            // 
            this.ConsoleContextMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.selectAllToolStripMenuItem,
            this.copyToolStripMenuItem,
            this.toolStripSeparator1,
            this.clearConsoleToolStripMenuItem});
            this.ConsoleContextMenu.Name = "ConsoleContextMenu";
            this.ConsoleContextMenu.Size = new System.Drawing.Size(148, 76);
            // 
            // selectAllToolStripMenuItem
            // 
            this.selectAllToolStripMenuItem.Name = "selectAllToolStripMenuItem";
            this.selectAllToolStripMenuItem.Size = new System.Drawing.Size(147, 22);
            this.selectAllToolStripMenuItem.Text = "Select All";
            this.selectAllToolStripMenuItem.Click += new System.EventHandler(this.selectAllToolStripMenuItem_Click);
            // 
            // copyToolStripMenuItem
            // 
            this.copyToolStripMenuItem.Name = "copyToolStripMenuItem";
            this.copyToolStripMenuItem.Size = new System.Drawing.Size(147, 22);
            this.copyToolStripMenuItem.Text = "Copy Text";
            this.copyToolStripMenuItem.Click += new System.EventHandler(this.copyToolStripMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(144, 6);
            // 
            // clearConsoleToolStripMenuItem
            // 
            this.clearConsoleToolStripMenuItem.Name = "clearConsoleToolStripMenuItem";
            this.clearConsoleToolStripMenuItem.Size = new System.Drawing.Size(147, 22);
            this.clearConsoleToolStripMenuItem.Text = "Clear Console";
            this.clearConsoleToolStripMenuItem.Click += new System.EventHandler(this.clearConsoleToolStripMenuItem_Click);
            // 
            // addZoneToolStripMenuItem
            // 
            this.addZoneToolStripMenuItem.Name = "addZoneToolStripMenuItem";
            this.addZoneToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.addZoneToolStripMenuItem.Text = "New Zone...";
            this.addZoneToolStripMenuItem.Click += new System.EventHandler(this.addZoneToolStripMenuItem_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(177, 6);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(177, 6);
            // 
            // CWorkPane
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(14)))), ((int)(((byte)(14)))), ((int)(((byte)(18)))));
            this.Controls.Add(this.splitContainer1);
            this.ForeColor = System.Drawing.Color.White;
            this.Name = "CWorkPane";
            this.Size = new System.Drawing.Size(1020, 682);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
            this.splitContainer2.ResumeLayout(false);
            this.WindowsFormsIsDOGSHIT.ResumeLayout(false);
            this.ThisShitIsAJOKE.ResumeLayout(false);
            this.ZoneTreeRightClick.ResumeLayout(false);
            this.ConsoleContextMenu.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.RichTextBox ConsoleBox;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.TreeView ProjectTree;
        private ZoneTreePanel WindowsFormsIsDOGSHIT;
        private ConsolePanel ThisShitIsAJOKE;
        private System.Windows.Forms.ContextMenuStrip ZoneTreeRightClick;
        private System.Windows.Forms.ToolStripMenuItem RenameProjectButton;
        private System.Windows.Forms.ContextMenuStrip ConsoleContextMenu;
        private System.Windows.Forms.ToolStripMenuItem clearConsoleToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem copyToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem selectAllToolStripMenuItem;
        private CBuildConfigPane BuildConfigPane;
        private System.Windows.Forms.ToolStripMenuItem openFolderToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem addZoneToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
    }
}
