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

            BuildConfigCombo.SelectedIndex = 0;
            SetActiveContent(CONTENT_DEFAULT);
            RebuildToolStripItems(null);
            Shared.Console.WriteLine($"[{DateTime.Now.ToLongTimeString()}^7] ^5Modtools Launcher {Shared.VERSION}^7, by ^2Serious");

            FormClosing += MainForm_FormClosing1;
        }

        private void MainForm_FormClosing1(object sender, FormClosingEventArgs e)
        {
            if(!(ProjectManager.ActiveProject is null))
            {
                ProjectManager.ActiveProject = null;
            }
            SetActiveContent(CONTENT_NONE); // invoke closing events on everything
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
            ContentPanelChanged += (ContentPanelIndex from, ContentPanelIndex to) =>
            {
                pane.Opening();
            };
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
                if(UIPanes[CurrentContent] is IContentPanel ip)
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
            AppEnv.PushActivity(ProjectManager.ActiveProject.FriendlyName);
            SetActiveContent(CONTENT_WORK_PANE);
            Shared.Console.WriteLine($"[{DateTime.Now.ToLongTimeString()}] Active project changed: ^3{ProjectManager.ActiveProject.FriendlyName}");
        }

        internal void CloseActiveProject()
        {
            if (ProjectManager.ActiveProject is null)
            {
                SetActiveContent(CONTENT_PROJECT_LIST);
                return;
            }

            // TODO: if changes are pending, ask to save to disk

            AppEnv.PopActivity(ProjectManager.ActiveProject.FriendlyName);
            ProjectManager.ActiveProject = null;
            SetActiveContent(CONTENT_PROJECT_LIST);
        }

        private bool SuspendBuildConfigEvents = false;
        private void RebuildToolStripItems(ML2Project project)
        {
            toolStrip1.Items.Clear();

            toolStrip1.Items.Add(toolStripNewFile);
            if (!(project is null))
            {
                toolStrip1.Items.Add(toolStripNewFile);
                toolStrip1.Items.Add(FirstSeparator);
                toolStrip1.Items.Add(BuildConfigCombo);
                toolStrip1.Items.Add(BuildRunButton);
                toolStrip1.Items.Add(SecondSeparator);
                toolStrip1.Items.Add(PublishButton);

                SuspendBuildConfigEvents = true;
                BuildConfigCombo.ComboBox.Items.Clear();

                foreach(var conf in project.GetBuildConfigurations())
                {
                    BuildConfigCombo.ComboBox.Items.Add(conf);
                }

                BuildConfigCombo.ComboBox.Items.Add("Configuration Manager...");
                SuspendBuildConfigEvents = false;
                BuildConfigCombo.SelectedIndex = project.ActiveConfigIndex;
            }
            else
            {
                SuspendBuildConfigEvents = true;
                BuildConfigCombo.ComboBox.Items.Clear();
                BuildConfigCombo.ComboBox.Items.Add("ERROR - I DONT EXIST");
                BuildConfigCombo.ComboBox.Items.Add("PLEASE REPORT THIS TO THE DEVELOPER");
                BuildConfigCombo.ComboBox.Items.Add("YOU SHOULD NOT SEE ME");
                BuildConfigCombo.ComboBox.Items.Add("Configuration Manager...");
                BuildConfigCombo.SelectedIndex = 0;
                SuspendBuildConfigEvents = false;
            }
        }

        private ML2Project CachedProject = null;
        internal void OnActiveProjectChanged(ML2Project project)
        {
            if(CachedProject != project)
            {
                newToolStripMenuItem.Text = project is null ? "New Project..." : "Close Project";
                newToolStripMenuItem.Image = project is null ? ML2.Properties.Resources.i_new_file : null;
                newToolStripMenuItem.ShortcutKeyDisplayString = project is null ? "Ctrl+N" : "Ctrl+F4";
                newToolStripMenuItem.ShortcutKeys = project is null ? (Keys.Control | Keys.N) : (Keys.Control | Keys.F4);

                if(CachedProject != null)
                {
                    CachedProject.OnNameUpdated -= OnNameUpdated;
                }

                CachedProject = project;

                if (CachedProject != null)
                {
                    CachedProject.OnNameUpdated += OnNameUpdated;
                }

                RebuildToolStripItems(project);
            }
            
            if(CachedProject is null)
            {
                return;
            }
        }

        private void OnNameUpdated(ML2Project project, string oldVal, string newVal)
        {
            AppEnv.ReplaceActivity(oldVal, newVal);
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

        private void radiantToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // TODO: check if target map is selected
            var path_to_rad = Path.Combine(Shared.TA_TOOLS_PATH, "bin", "radiant_modtools.exe");
            if (!File.Exists(path_to_rad))
            {
                CErrorDialog.Show("Error", "The level editor program, Radiant, could not be located. Please ensure that you have installed the original BO3 modtools.");
                return;
            }
            Process.Start(path_to_rad);
        }

        private void runToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }
    }

    internal enum ContentPanelIndex
    {
        CONTENT_NONE,
        CONTENT_PROJECT_LIST,
        CONTENT_WORK_PANE
    }
}
