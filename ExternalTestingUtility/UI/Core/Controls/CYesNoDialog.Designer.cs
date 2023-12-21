
namespace ML2.UI.Core.Controls
{
    partial class CYesNoDialog
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
            this.YesButton = new System.Windows.Forms.Button();
            this.NoButton = new System.Windows.Forms.Button();
            this.ErrorRTB = new System.Windows.Forms.RichTextBox();
            this.InnerForm.ControlContents.SuspendLayout();
            this.SuspendLayout();
            // 
            // InnerForm
            // 
            this.InnerForm.BackColor = System.Drawing.Color.DodgerBlue;
            // 
            // InnerForm.ControlContents
            // 
            this.InnerForm.ControlContents.Controls.Add(this.YesButton);
            this.InnerForm.ControlContents.Controls.Add(this.NoButton);
            this.InnerForm.ControlContents.Controls.Add(this.ErrorRTB);
            this.InnerForm.ControlContents.Dock = System.Windows.Forms.DockStyle.Fill;
            this.InnerForm.ControlContents.Enabled = true;
            this.InnerForm.ControlContents.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.InnerForm.ControlContents.Location = new System.Drawing.Point(0, 32);
            this.InnerForm.ControlContents.Name = "ControlContents";
            this.InnerForm.ControlContents.Size = new System.Drawing.Size(396, 164);
            this.InnerForm.ControlContents.TabIndex = 1;
            this.InnerForm.ControlContents.Visible = true;
            this.InnerForm.Dock = System.Windows.Forms.DockStyle.Fill;
            this.InnerForm.Location = new System.Drawing.Point(0, 0);
            this.InnerForm.Name = "InnerForm";
            this.InnerForm.Size = new System.Drawing.Size(400, 200);
            this.InnerForm.TabIndex = 0;
            this.InnerForm.TitleBarTitle = "Error Dialog";
            this.InnerForm.UseTitleBar = true;
            // 
            // YesButton
            // 
            this.YesButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.YesButton.Location = new System.Drawing.Point(224, 123);
            this.YesButton.Name = "YesButton";
            this.YesButton.Size = new System.Drawing.Size(76, 32);
            this.YesButton.TabIndex = 2;
            this.YesButton.Text = "Yes";
            this.YesButton.UseVisualStyleBackColor = true;
            this.YesButton.Click += new System.EventHandler(this.YesButton_Click);
            // 
            // NoButton
            // 
            this.NoButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.NoButton.Location = new System.Drawing.Point(310, 123);
            this.NoButton.Name = "NoButton";
            this.NoButton.Size = new System.Drawing.Size(76, 32);
            this.NoButton.TabIndex = 1;
            this.NoButton.Text = "No";
            this.NoButton.UseVisualStyleBackColor = true;
            this.NoButton.Click += new System.EventHandler(this.AcceptButton_Click);
            // 
            // ErrorRTB
            // 
            this.ErrorRTB.DetectUrls = false;
            this.ErrorRTB.Location = new System.Drawing.Point(10, 4);
            this.ErrorRTB.Name = "ErrorRTB";
            this.ErrorRTB.ReadOnly = true;
            this.ErrorRTB.Size = new System.Drawing.Size(376, 106);
            this.ErrorRTB.TabIndex = 0;
            this.ErrorRTB.Text = "Generic error message! This is a generic error message, and you should be aware o" +
    "f that.";
            // 
            // CYesNoDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(400, 200);
            this.Controls.Add(this.InnerForm);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "CYesNoDialog";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Error Dialog";
            this.InnerForm.ControlContents.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private ML2.UI.Core.Controls.CXBorderedForm InnerForm;
        private System.Windows.Forms.RichTextBox ErrorRTB;
        private System.Windows.Forms.Button NoButton;
        private System.Windows.Forms.Button YesButton;
    }
}