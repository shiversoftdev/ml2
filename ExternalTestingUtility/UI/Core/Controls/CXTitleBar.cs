using ML2.UI.Core.Interfaces;
using ML2.UI.Core.Singletons;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace ML2.UI.Core.Controls
{
    public partial class CXTitleBar : UserControl, IThemeableControl
    {
        public bool DisableDrag;
        public CXTitleBar()
        {
            InitializeComponent();
            MouseDown += MouseDown_Drag;
            UIThemeManager.RegisterCustomThemeHandler(typeof(CXTitleBar), ApplyThemeCustomType_Implementation);
            UIThemeManager.OnThemeChanged(this, ApplyThemeCustom_Implementation);
            TitleLabel.MouseDown += MouseDown_Drag;
        }

        private void ExitButton_Click(object sender, EventArgs e)
        {
            ParentForm?.Close();
        }

        public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int HT_CAPTION = 0x2;

        [System.Runtime.InteropServices.DllImport("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
        [System.Runtime.InteropServices.DllImport("user32.dll")]
        public static extern bool ReleaseCapture();

        private void MouseDown_Drag(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            if (ParentForm == null) return;
            if (DisableDrag) return;
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(ParentForm.Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
            }
        }

        public void SetExitButtonVisible(bool isVisible)
        {
            ExitButton.Visible = isVisible;
        }

        private void ApplyThemeCustomType_Implementation(UIThemeInfo themeData)
        {
            BackColor = themeData.TitleBarColor;
            ForeColor = themeData.TextColor;
        }

        private void ApplyThemeCustom_Implementation(UIThemeInfo themeData)
        {
            ExitButton.BackColor = themeData.TitleBarColor;
        }

        public IEnumerable<Control> GetThemedControls()
        {
            yield return ExitButton;
            yield return TitleLabel;
        }
    }
}
