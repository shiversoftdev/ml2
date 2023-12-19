
namespace ML2.UI.Application
{
    partial class CProjectList
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
            this.ProjectDiscoveryTimer = new System.Windows.Forms.Timer(this.components);
            this.ProjectTree = new System.Windows.Forms.TreeView();
            this.SuspendLayout();
            // 
            // ProjectDiscoveryTimer
            // 
            this.ProjectDiscoveryTimer.Interval = 5000;
            // 
            // ProjectTree
            // 
            this.ProjectTree.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(14)))), ((int)(((byte)(14)))), ((int)(((byte)(18)))));
            this.ProjectTree.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.ProjectTree.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ProjectTree.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.ProjectTree.Location = new System.Drawing.Point(4, 4);
            this.ProjectTree.Name = "ProjectTree";
            this.ProjectTree.Size = new System.Drawing.Size(1012, 674);
            this.ProjectTree.TabIndex = 0;
            // 
            // CProjectList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(14)))), ((int)(((byte)(14)))), ((int)(((byte)(18)))));
            this.Controls.Add(this.ProjectTree);
            this.ForeColor = System.Drawing.Color.White;
            this.Name = "CProjectList";
            this.Padding = new System.Windows.Forms.Padding(4);
            this.Size = new System.Drawing.Size(1020, 682);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Timer ProjectDiscoveryTimer;
        private System.Windows.Forms.TreeView ProjectTree;
    }
}
