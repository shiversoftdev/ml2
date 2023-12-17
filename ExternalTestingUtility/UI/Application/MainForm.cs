using ML2.Cheats;
using ML2.UI.Application;
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
using static ML2.Cheats.BlackOps3.ClassSetType;
using static ML2.Cheats.BlackOps3.loadoutClass_t;
using static ML2.ContentPanelIndex;

namespace ML2
{
    public partial class MainForm : Form, IThemeableControl
    {
        #region CONST
#if DEBUG
        private const ContentPanelIndex CONTENT_DEFAULT = CONTENT_PROJECT_LIST;
#else
        private const ContentPanelIndex CONTENT_DEFAULT = CONTENT_PROJECT_LIST;
#endif
        #endregion

        private Dictionary<ContentPanelIndex, Control> UIPanes;
        private ContentPanelIndex CurrentContent = CONTENT_NONE;
        public MainForm()
        {
            // Sets up stealth calls for native funcs to try to avoid api hooking
            NativeStealth.SetStealthMode(NativeStealthType.ManualInvoke);

            InitializeComponent();
            UIThemeManager.OnThemeChanged(this, OnThemeChanged_Implementation);
            this.SetThemeAware();
            UIThemeManager.ApplyTheme(Themes.Mikey);
            MaximizeBox = true;
            MinimizeBox = true;

            FormClosing += MainForm_FormClosing;

            UIPanes = new Dictionary<ContentPanelIndex, Control>();

            UIPanes[CONTENT_PROJECT_LIST] = new CProjectList();

            SetActiveContent(CONTENT_DEFAULT);
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (CheckExitAllowed())
            {
                return;
            }
            e.Cancel = true;
        }

        public IEnumerable<Control> GetThemedControls()
        {
            yield return InnerForm;
            yield return TopMenuBar;
            yield return toolStrip1;
            yield return ContentPanel;
        }

        private void OnThemeChanged_Implementation(UIThemeInfo currentTheme)
        {
        }

        private void openInRadiantToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private bool CheckExitAllowed()
        {
            if (AppEnv.ExitStatus == ExitProhibitedReason.ALLOWED)
            {
                return true;
            }
            // TODO (remember to edit notepad too when done)
            return false;
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!CheckExitAllowed())
            {
                return;
            }
            Application.Exit();
        }

        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void TopMenuBar_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void helpToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private bool SetActiveContent(ContentPanelIndex i)
        {
            if(CurrentContent != CONTENT_NONE)
            {
                if(ContentPanel is IContentPanel ip)
                {
                    if (!ip.CanClosePanelNow())
                    {
                        return false;
                    }
                    ip.OnContentClosing();
                }
                ContentPanel.SuspendLayout();
                ContentPanel.Controls.Remove(UIPanes[CurrentContent]);
                ContentPanel.ResumeLayout();
            }

            ContentPanelChanged?.Invoke(CurrentContent, i);
            CurrentContent = i;

            if (i == CONTENT_NONE)
            {
                return true;
            }

            var panel = UIPanes[CurrentContent];

            if(panel is IContentPanel content)
            {
                content.OnContentOpening();
            }

            ContentPanel.SuspendLayout();
            panel.Anchor = AnchorStyles.Left | AnchorStyles.Top;
            panel.Location = new Point(0, 0);
            panel.Dock = DockStyle.Fill;
            ContentPanel.Controls.Add(panel);
            ContentPanel.ResumeLayout();

            return true;
        }

        private delegate void ContentUpdatingDelegate(ContentPanelIndex from, ContentPanelIndex to);
        private ContentUpdatingDelegate ContentPanelChanged;
    }

    internal enum ContentPanelIndex
    {
        CONTENT_NONE,
        CONTENT_PROJECT_LIST
    }
}
