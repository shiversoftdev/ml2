﻿using ML2.UI.Core.Interfaces;
using ML2.UI.Core.Singletons;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ML2.UI.Core.Controls
{
    public partial class CYesNoDialog : Form, IThemeableControl
    {
        public CYesNoDialog(string title, string description)
        {
            InitializeComponent();
            UIThemeManager.OnThemeChanged(this, OnThemeChanged_Implementation);
            this.SetThemeAware();
            MaximizeBox = true;
            MinimizeBox = true;
            Text = title;
            InnerForm.TitleBarTitle = title;
            ErrorRTB.Text = description;
            DialogResult = DialogResult.Cancel;
        }

        private void OnThemeChanged_Implementation(UIThemeInfo themeData)
        {
            return;
        }

        public IEnumerable<Control> GetThemedControls()
        {
            yield return InnerForm;
            yield return ErrorRTB;
            yield return NoButton;
            yield return YesButton;
        }

        private void AcceptButton_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.No;
            Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        public static DialogResult Show(string title, string description)
        {
            return new CYesNoDialog(title, description).ShowDialog();
        }

        private void YesButton_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Yes;
            Close();
        }
    }
}
