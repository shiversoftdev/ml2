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

namespace ML2.UI.Core.Controls
{
    public partial class ActionForm : Form, IThemeableControl
    {
        // TODO: if anything changes *at all* we need to set DialogResult to OK
        public ActionForm(int actionIndex, bool isEdit)
        {
            InitializeComponent();
            UIThemeManager.OnThemeChanged(this, OnThemeChanged_Implementation);
            this.SetThemeAware();
            MaximizeBox = true;
            MinimizeBox = true;
            Text = InnerForm.TitleBarTitle = isEdit ? "Build Action Editor" : "Build Action Creator";
            DialogResult = DialogResult.Cancel;
        }

        private void OnThemeChanged_Implementation(UIThemeInfo themeData)
        {
            return;
        }

        public IEnumerable<Control> GetThemedControls()
        {
            yield return InnerForm;
        }

        private void AcceptButton_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        public static void Show(string title, string description)
        {
            new CErrorDialog(title, description).ShowDialog();
        }
    }
}
