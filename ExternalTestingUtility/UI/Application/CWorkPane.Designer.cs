
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
            this.ConsoleBox = new System.Windows.Forms.RichTextBox();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.ConsoleBox);
            this.splitContainer1.Size = new System.Drawing.Size(1020, 682);
            this.splitContainer1.SplitterDistance = 340;
            this.splitContainer1.TabIndex = 0;
            // 
            // ConsoleBox
            // 
            this.ConsoleBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ConsoleBox.Location = new System.Drawing.Point(0, 0);
            this.ConsoleBox.Margin = new System.Windows.Forms.Padding(4);
            this.ConsoleBox.Name = "ConsoleBox";
            this.ConsoleBox.ReadOnly = true;
            this.ConsoleBox.Size = new System.Drawing.Size(1020, 338);
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
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.RichTextBox ConsoleBox;
    }
}
