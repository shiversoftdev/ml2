
namespace ML2
{
    partial class MainForm
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.InnerForm = new ML2.UI.Core.Controls.CXBorderedForm();
            this.ContentPanel = new System.Windows.Forms.Panel();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripNewFile = new System.Windows.Forms.ToolStripButton();
            this.FirstSeparator = new System.Windows.Forms.ToolStripSeparator();
            this.BuildConfigCombo = new System.Windows.Forms.ToolStripComboBox();
            this.BuildRunButton = new System.Windows.Forms.ToolStripSplitButton();
            this.buildToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.runToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.SecondSeparator = new System.Windows.Forms.ToolStripSeparator();
            this.PublishButton = new System.Windows.Forms.ToolStripButton();
            this.TopMenuBar = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.newToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.editToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.preferencesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.assetEditorToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.export2BinGUIToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.radiantToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.resourcesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.discordServerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.wikiToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.BuildBackgroundWorker = new System.ComponentModel.BackgroundWorker();
            this.InnerForm.ControlContents.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.TopMenuBar.SuspendLayout();
            this.SuspendLayout();
            // 
            // InnerForm
            // 
            this.InnerForm.BackColor = System.Drawing.Color.DodgerBlue;
            // 
            // InnerForm.ControlContents
            // 
            this.InnerForm.ControlContents.Controls.Add(this.ContentPanel);
            this.InnerForm.ControlContents.Controls.Add(this.toolStrip1);
            this.InnerForm.ControlContents.Controls.Add(this.TopMenuBar);
            this.InnerForm.ControlContents.Dock = System.Windows.Forms.DockStyle.Fill;
            this.InnerForm.ControlContents.Enabled = true;
            this.InnerForm.ControlContents.Location = new System.Drawing.Point(0, 32);
            this.InnerForm.ControlContents.Name = "ControlContents";
            this.InnerForm.ControlContents.Size = new System.Drawing.Size(1020, 732);
            this.InnerForm.ControlContents.TabIndex = 1;
            this.InnerForm.ControlContents.Visible = true;
            this.InnerForm.Dock = System.Windows.Forms.DockStyle.Fill;
            this.InnerForm.Location = new System.Drawing.Point(0, 0);
            this.InnerForm.Name = "InnerForm";
            this.InnerForm.Size = new System.Drawing.Size(1024, 768);
            this.InnerForm.TabIndex = 0;
            this.InnerForm.TitleBarTitle = "Black Ops III Mod Tools Launcher";
            this.InnerForm.UseTitleBar = true;
            // 
            // ContentPanel
            // 
            this.ContentPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ContentPanel.Location = new System.Drawing.Point(0, 50);
            this.ContentPanel.Name = "ContentPanel";
            this.ContentPanel.Size = new System.Drawing.Size(1020, 682);
            this.ContentPanel.TabIndex = 2;
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripNewFile,
            this.FirstSeparator,
            this.BuildConfigCombo,
            this.BuildRunButton,
            this.SecondSeparator,
            this.PublishButton});
            this.toolStrip1.Location = new System.Drawing.Point(0, 25);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Padding = new System.Windows.Forms.Padding(6, 0, 1, 0);
            this.toolStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional;
            this.toolStrip1.Size = new System.Drawing.Size(1020, 25);
            this.toolStrip1.TabIndex = 1;
            this.toolStrip1.Text = "TEST";
            // 
            // toolStripNewFile
            // 
            this.toolStripNewFile.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripNewFile.Image = global::ML2.Properties.Resources.i_new_file;
            this.toolStripNewFile.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripNewFile.Name = "toolStripNewFile";
            this.toolStripNewFile.Size = new System.Drawing.Size(23, 22);
            this.toolStripNewFile.Text = "New Project";
            // 
            // FirstSeparator
            // 
            this.FirstSeparator.Name = "FirstSeparator";
            this.FirstSeparator.Size = new System.Drawing.Size(6, 25);
            // 
            // BuildConfigCombo
            // 
            this.BuildConfigCombo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.BuildConfigCombo.DropDownWidth = 160;
            this.BuildConfigCombo.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.BuildConfigCombo.Items.AddRange(new object[] {
            "ERROR - I DONT EXIST",
            "PLEASE WARN THE DEVELOPER",
            "THIS IS NOT SUPPOSED TO HAPPEN",
            "Configuration Manager..."});
            this.BuildConfigCombo.Name = "BuildConfigCombo";
            this.BuildConfigCombo.Size = new System.Drawing.Size(100, 25);
            this.BuildConfigCombo.ToolTipText = "Build Configuration";
            // 
            // BuildRunButton
            // 
            this.BuildRunButton.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.buildToolStripMenuItem,
            this.runToolStripMenuItem});
            this.BuildRunButton.Image = global::ML2.Properties.Resources.i_go;
            this.BuildRunButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.BuildRunButton.Name = "BuildRunButton";
            this.BuildRunButton.Size = new System.Drawing.Size(66, 22);
            this.BuildRunButton.Text = "Build";
            this.BuildRunButton.ButtonClick += new System.EventHandler(this.BuildRunButton_ButtonClick);
            // 
            // buildToolStripMenuItem
            // 
            this.buildToolStripMenuItem.Image = global::ML2.Properties.Resources.i_go;
            this.buildToolStripMenuItem.Name = "buildToolStripMenuItem";
            this.buildToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift) 
            | System.Windows.Forms.Keys.B)));
            this.buildToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.buildToolStripMenuItem.Text = "Build";
            this.buildToolStripMenuItem.Click += new System.EventHandler(this.buildToolStripMenuItem_Click);
            // 
            // runToolStripMenuItem
            // 
            this.runToolStripMenuItem.Image = global::ML2.Properties.Resources.i_go;
            this.runToolStripMenuItem.Name = "runToolStripMenuItem";
            this.runToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift) 
            | System.Windows.Forms.Keys.R)));
            this.runToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.runToolStripMenuItem.Text = "Run";
            // 
            // SecondSeparator
            // 
            this.SecondSeparator.Name = "SecondSeparator";
            this.SecondSeparator.Size = new System.Drawing.Size(6, 25);
            // 
            // PublishButton
            // 
            this.PublishButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.PublishButton.Image = global::ML2.Properties.Resources.i_upload;
            this.PublishButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.PublishButton.Name = "PublishButton";
            this.PublishButton.Size = new System.Drawing.Size(23, 22);
            this.PublishButton.Text = "Publish to Workshop";
            // 
            // TopMenuBar
            // 
            this.TopMenuBar.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.editToolStripMenuItem,
            this.toolsToolStripMenuItem,
            this.helpToolStripMenuItem});
            this.TopMenuBar.Location = new System.Drawing.Point(0, 0);
            this.TopMenuBar.Name = "TopMenuBar";
            this.TopMenuBar.Padding = new System.Windows.Forms.Padding(6, 4, 0, 2);
            this.TopMenuBar.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional;
            this.TopMenuBar.Size = new System.Drawing.Size(1020, 25);
            this.TopMenuBar.TabIndex = 0;
            this.TopMenuBar.Text = "menuStrip1";
            this.TopMenuBar.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.TopMenuBar_ItemClicked);
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newToolStripMenuItem,
            this.toolStripSeparator1,
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 19);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // newToolStripMenuItem
            // 
            this.newToolStripMenuItem.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.newToolStripMenuItem.ForeColor = System.Drawing.SystemColors.Control;
            this.newToolStripMenuItem.Image = global::ML2.Properties.Resources.i_new_file;
            this.newToolStripMenuItem.Name = "newToolStripMenuItem";
            this.newToolStripMenuItem.ShortcutKeyDisplayString = "Ctrl+N";
            this.newToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.N)));
            this.newToolStripMenuItem.Size = new System.Drawing.Size(190, 22);
            this.newToolStripMenuItem.Text = "New Project...";
            this.newToolStripMenuItem.Click += new System.EventHandler(this.newToolStripMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.ForeColor = System.Drawing.SystemColors.ControlText;
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(187, 6);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.ShortcutKeyDisplayString = "Alt+F4";
            this.exitToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.F4)));
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(190, 22);
            this.exitToolStripMenuItem.Text = "Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // editToolStripMenuItem
            // 
            this.editToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.preferencesToolStripMenuItem});
            this.editToolStripMenuItem.Name = "editToolStripMenuItem";
            this.editToolStripMenuItem.Size = new System.Drawing.Size(39, 19);
            this.editToolStripMenuItem.Text = "Edit";
            this.editToolStripMenuItem.Click += new System.EventHandler(this.editToolStripMenuItem_Click);
            // 
            // preferencesToolStripMenuItem
            // 
            this.preferencesToolStripMenuItem.Name = "preferencesToolStripMenuItem";
            this.preferencesToolStripMenuItem.Size = new System.Drawing.Size(144, 22);
            this.preferencesToolStripMenuItem.Text = "Preferences...";
            // 
            // toolsToolStripMenuItem
            // 
            this.toolsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.assetEditorToolStripMenuItem,
            this.export2BinGUIToolStripMenuItem,
            this.radiantToolStripMenuItem});
            this.toolsToolStripMenuItem.Name = "toolsToolStripMenuItem";
            this.toolsToolStripMenuItem.Size = new System.Drawing.Size(46, 19);
            this.toolsToolStripMenuItem.Text = "Tools";
            // 
            // assetEditorToolStripMenuItem
            // 
            this.assetEditorToolStripMenuItem.Image = global::ML2.Properties.Resources.i_asset_editor;
            this.assetEditorToolStripMenuItem.Name = "assetEditorToolStripMenuItem";
            this.assetEditorToolStripMenuItem.ShortcutKeyDisplayString = "Ctrl+A";
            this.assetEditorToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.A)));
            this.assetEditorToolStripMenuItem.Size = new System.Drawing.Size(193, 22);
            this.assetEditorToolStripMenuItem.Text = "Asset Editor";
            this.assetEditorToolStripMenuItem.Click += new System.EventHandler(this.assetEditorToolStripMenuItem_Click);
            // 
            // export2BinGUIToolStripMenuItem
            // 
            this.export2BinGUIToolStripMenuItem.Image = global::ML2.Properties.Resources.i_export2bin;
            this.export2BinGUIToolStripMenuItem.Name = "export2BinGUIToolStripMenuItem";
            this.export2BinGUIToolStripMenuItem.ShortcutKeyDisplayString = "Ctrl+E";
            this.export2BinGUIToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.E)));
            this.export2BinGUIToolStripMenuItem.Size = new System.Drawing.Size(193, 22);
            this.export2BinGUIToolStripMenuItem.Text = "Export2Bin GUI";
            // 
            // radiantToolStripMenuItem
            // 
            this.radiantToolStripMenuItem.Image = global::ML2.Properties.Resources.i_radiant;
            this.radiantToolStripMenuItem.Name = "radiantToolStripMenuItem";
            this.radiantToolStripMenuItem.ShortcutKeyDisplayString = "Ctrl+R";
            this.radiantToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.R)));
            this.radiantToolStripMenuItem.Size = new System.Drawing.Size(193, 22);
            this.radiantToolStripMenuItem.Text = "Radiant";
            this.radiantToolStripMenuItem.Click += new System.EventHandler(this.radiantToolStripMenuItem_Click);
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.resourcesToolStripMenuItem,
            this.aboutToolStripMenuItem});
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(44, 19);
            this.helpToolStripMenuItem.Text = "Help";
            this.helpToolStripMenuItem.Click += new System.EventHandler(this.helpToolStripMenuItem_Click);
            // 
            // resourcesToolStripMenuItem
            // 
            this.resourcesToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.discordServerToolStripMenuItem,
            this.wikiToolStripMenuItem});
            this.resourcesToolStripMenuItem.Name = "resourcesToolStripMenuItem";
            this.resourcesToolStripMenuItem.Size = new System.Drawing.Size(127, 22);
            this.resourcesToolStripMenuItem.Text = "Resources";
            // 
            // discordServerToolStripMenuItem
            // 
            this.discordServerToolStripMenuItem.Image = global::ML2.Properties.Resources.i_discord;
            this.discordServerToolStripMenuItem.Name = "discordServerToolStripMenuItem";
            this.discordServerToolStripMenuItem.Size = new System.Drawing.Size(149, 22);
            this.discordServerToolStripMenuItem.Text = "Discord Server";
            this.discordServerToolStripMenuItem.Click += new System.EventHandler(this.discordServerToolStripMenuItem_Click);
            // 
            // wikiToolStripMenuItem
            // 
            this.wikiToolStripMenuItem.Enabled = false;
            this.wikiToolStripMenuItem.Name = "wikiToolStripMenuItem";
            this.wikiToolStripMenuItem.Size = new System.Drawing.Size(149, 22);
            this.wikiToolStripMenuItem.Text = "Wiki";
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(127, 22);
            this.aboutToolStripMenuItem.Text = "About";
            // 
            // BuildBackgroundWorker
            // 
            this.BuildBackgroundWorker.WorkerReportsProgress = true;
            this.BuildBackgroundWorker.WorkerSupportsCancellation = true;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1024, 768);
            this.Controls.Add(this.InnerForm);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "External Testing";
            this.InnerForm.ControlContents.ResumeLayout(false);
            this.InnerForm.ControlContents.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.TopMenuBar.ResumeLayout(false);
            this.TopMenuBar.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private ML2.UI.Core.Controls.CXBorderedForm InnerForm;
        private System.Windows.Forms.MenuStrip TopMenuBar;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem editToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem preferencesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem newToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem toolsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem assetEditorToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem export2BinGUIToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem resourcesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem discordServerToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem wikiToolStripMenuItem;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.Panel ContentPanel;
        private System.Windows.Forms.ToolStripButton toolStripNewFile;
        private System.Windows.Forms.ToolStripMenuItem radiantToolStripMenuItem;
        private System.Windows.Forms.ToolStripComboBox BuildConfigCombo;
        private System.Windows.Forms.ToolStripButton PublishButton;
        private System.Windows.Forms.ToolStripSeparator SecondSeparator;
        private System.Windows.Forms.ToolStripSeparator FirstSeparator;
        private System.Windows.Forms.ToolStripSplitButton BuildRunButton;
        private System.Windows.Forms.ToolStripMenuItem buildToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem runToolStripMenuItem;
        private System.ComponentModel.BackgroundWorker BuildBackgroundWorker;
    }
}

