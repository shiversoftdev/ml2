using ML2.Cheats;
using ML2.Core;
using ML2.UI.Application;
using ML2.UI.Core.Controls;
using ML2.UI.Core.Interfaces;
using ML2.UI.Core.Singletons;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
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

            toolStrip1.Renderer = new OverrideStripSystemRenderer();

            // NEED TO DO THIS BEFORE THEMING!!
            UIPanes = new Dictionary<ContentPanelIndex, Control>();
            UIPanes[CONTENT_PROJECT_LIST] = ProjectListPaneFactory();
            UIPanes[CONTENT_WORK_PANE] = WorkPaneFactory();

            ProjectManager.OnActiveProjectChanged += OnActiveProjectChanged;
            AppEnv.OnActivityChanged += OnActivityChanged;
            OnActivityChanged();

            UIThemeManager.OnThemeChanged(this, OnThemeChanged_Implementation);
            this.SetThemeAware();
            UIThemeManager.ApplyTheme(Themes.Mikey);


            MaximizeBox = true;
            MinimizeBox = true;
            FormClosing += MainForm_FormClosing;

            SetActiveContent(CONTENT_DEFAULT);

            Shared.Console.WriteLine($"[{DateTime.Now.ToLongTimeString()}^7] ^5Modtools Launcher {Shared.VERSION}^7, by ^2Serious");
        }

        private Control ProjectListPaneFactory()
        {
            var pane = new CProjectList();
            pane.OnProjectSelected += ProjectSelected;

            return pane;
        }

        private Control WorkPaneFactory()
        {
            var pane = new CWorkPane();

            return pane;
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

            foreach(var pane in UIPanes)
            {
                yield return pane.Value;
            }
        }

        private void OnThemeChanged_Implementation(UIThemeInfo currentTheme)
        {
        }

        private void openInRadiantToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if(ProjectManager.ActiveProject is null)
            {
                // TODO
            }
            else
            {
                CloseActiveProject();
            }
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

        internal void ProjectSelected(ML2Project project)
        {
            if(ProjectManager.ActiveProject != null)
            {
                CErrorDialog.Show("Error", "Cannot set the active project because there is already an active project. Please report this issue to the developer of this application. You should restart the program to fix this problem.");
                return;
            }

            ProjectManager.ActiveProject = project;
            AppEnv.PushActivity(ProjectManager.ActiveProject.Data.FriendlyName);
            SetActiveContent(CONTENT_WORK_PANE);
            ConsumeFocus();
            Shared.Console.WriteLine($"[{DateTime.Now.ToLongTimeString()}] Active project changed: ^3{ProjectManager.ActiveProject.Data.FriendlyName}");
        }

        internal void ConsumeFocus()
        {
            Task.Delay(100).ContinueWith(_ =>
            {
                Invoke(new Action(() =>
                {
                    Focus();
                    Select();
                }));
            });
        }

        internal void CloseActiveProject()
        {
            if (ProjectManager.ActiveProject is null)
            {
                SetActiveContent(CONTENT_PROJECT_LIST);
                return;
            }

            // TODO: if changes are pending, ask to save to disk

            AppEnv.PopActivity(ProjectManager.ActiveProject.Data.FriendlyName);
            ProjectManager.ActiveProject = null;
            SetActiveContent(CONTENT_PROJECT_LIST);
        }

        internal void OnActiveProjectChanged(ML2Project project)
        {
            newToolStripMenuItem.Text = ProjectManager.ActiveProject is null ? "New Project..." : "Close Project";
            newToolStripMenuItem.Image = ProjectManager.ActiveProject is null ? ML2.Properties.Resources.i_new_file : null;
            newToolStripMenuItem.ShortcutKeyDisplayString = ProjectManager.ActiveProject is null ? "Ctrl+N" : "Ctrl+F4";
            newToolStripMenuItem.ShortcutKeys = ProjectManager.ActiveProject is null ? (Keys.Control | Keys.N) : (Keys.Control | Keys.F4);

            toolStrip1.Items.Remove(toolStripNewFile);

            if(ProjectManager.ActiveProject is null)
            {
                toolStrip1.Items.Insert(0, toolStripNewFile);
            }
        }

        public class OverrideStripSystemRenderer : ToolStripSystemRenderer
        {
            public OverrideStripSystemRenderer() { }

            protected override void OnRenderToolStripBorder(ToolStripRenderEventArgs e)
            {
                //base.OnRenderToolStripBorder(e);
            }
        }

        private void assetEditorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var path_to_ape = Path.Combine(Shared.TA_TOOLS_PATH, "bin", "AssetEditor_modtools.exe");
            if(!File.Exists(path_to_ape))
            {
                CErrorDialog.Show("Error", "The asset editor program could not be located. Please ensure that you have installed the original BO3 modtools.");
                return;
            }
            Process.Start(path_to_ape);
        }

        private void OnActivityChanged()
        {
            string status_text = "Black Ops III Mod Tools Launcher";

            if(AppEnv.ActivityStatus != null && AppEnv.ActivityStatus != "")
            {
                status_text += $" - {AppEnv.ActivityStatus}";
            }

            Text = status_text;
            InnerForm.TitleBarTitle = Text;
        }

        private void discordServerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Process.Start("https://gsc.dev/s/discord");
        }
    }

    internal enum ContentPanelIndex
    {
        CONTENT_NONE,
        CONTENT_PROJECT_LIST,
        CONTENT_WORK_PANE
    }
}
