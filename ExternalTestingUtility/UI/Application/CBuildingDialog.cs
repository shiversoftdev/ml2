using ML2.UI.Core.Controls;
using ML2.UI.Core.Interfaces;
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

namespace ML2.UI.Application
{
    public partial class CBuildingDialog : Form, IThemeableControl
    {
        private bool AllowClose = false;
        public EventHandler OperationComplete { get; private set; }
        public EventHandler CancelRequested { get; set; }
        public CBuildingDialog(string title, string description)
        {
            InitializeComponent();
            UIThemeManager.OnThemeChanged(this, OnThemeChanged_Implementation);
            this.SetThemeAware();
            MaximizeBox = true;
            MinimizeBox = true;
            Text = title;
            InnerForm.TitleBarTitle = title;
            ErrorRTB.Text = description;
            FormClosing += CPleaseWaitDialog_FormClosing;
            InnerForm.SetExitHidden(false);
            InnerForm.SetDraggable(false);
            OperationComplete = CloseFormInternal;
            vsIsStupidAsf = Close;
        }

        private void CPleaseWaitDialog_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!AllowClose)
            {
                if(CYesNoDialog.Show("Cancel Build", "Are you sure you want to cancel the build process?") == DialogResult.Yes)
                {
                    CancelRequested?.Invoke(sender, e);
                    return;
                }
                e.Cancel = true;
                return;
            }
        }

        private Action vsIsStupidAsf;
        private void CloseFormInternal(object sender, EventArgs e)
        {
            AllowClose = true;
            this.Invoke(vsIsStupidAsf);
        }

        private void OnThemeChanged_Implementation(UIThemeInfo themeData)
        {
            return;
        }

        public IEnumerable<Control> GetThemedControls()
        {
            yield return InnerForm;
            yield return ErrorRTB;
        }

        private void AcceptButton_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }
    }
}
