
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
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.WindowsFormsIsDOGSHIT = new ML2.UI.Application.ZoneTreePanel();
            this.ProjectTree = new System.Windows.Forms.TreeView();
            this.ThisShitIsAJOKE = new ML2.UI.Application.ConsolePanel();
            this.ConsoleBox = new System.Windows.Forms.RichTextBox();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            this.WindowsFormsIsDOGSHIT.SuspendLayout();
            this.ThisShitIsAJOKE.SuspendLayout();
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
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.ThisShitIsAJOKE);
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
            this.splitContainer2.Size = new System.Drawing.Size(1020, 340);
            this.splitContainer2.SplitterDistance = 237;
            this.splitContainer2.TabIndex = 0;
            // 
            // WindowsFormsIsDOGSHIT
            // 
            this.WindowsFormsIsDOGSHIT.Controls.Add(this.ProjectTree);
            this.WindowsFormsIsDOGSHIT.Dock = System.Windows.Forms.DockStyle.Fill;
            this.WindowsFormsIsDOGSHIT.Location = new System.Drawing.Point(0, 0);
            this.WindowsFormsIsDOGSHIT.Name = "WindowsFormsIsDOGSHIT";
            this.WindowsFormsIsDOGSHIT.Padding = new System.Windows.Forms.Padding(2);
            this.WindowsFormsIsDOGSHIT.Size = new System.Drawing.Size(237, 340);
            this.WindowsFormsIsDOGSHIT.TabIndex = 0;
            // 
            // ProjectTree
            // 
            this.ProjectTree.CheckBoxes = true;
            this.ProjectTree.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ProjectTree.Location = new System.Drawing.Point(2, 2);
            this.ProjectTree.Margin = new System.Windows.Forms.Padding(0);
            this.ProjectTree.Name = "ProjectTree";
            this.ProjectTree.Size = new System.Drawing.Size(233, 336);
            this.ProjectTree.TabIndex = 0;
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
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
            this.splitContainer2.ResumeLayout(false);
            this.WindowsFormsIsDOGSHIT.ResumeLayout(false);
            this.ThisShitIsAJOKE.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.RichTextBox ConsoleBox;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.TreeView ProjectTree;
        private ZoneTreePanel WindowsFormsIsDOGSHIT;
        private ConsolePanel ThisShitIsAJOKE;
    }
}
