using ML2.Core;
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
    public partial class CProjectList : UserControl, IContentPanel, IThemeableControl
    {
        internal delegate void ProjectActionDelegate(ML2Project project);
        internal ProjectActionDelegate OnProjectSelected;
        private TreeNode Maps, Mods;
        private Dictionary<ML2Project, ML2ProjectTreeNode> ProjectTNCache;
        public CProjectList()
        {
            InitializeComponent();
            ProjectDiscoveryTimer.Tick += ProjectDiscoveryTimer_Tick;
            ProjectManager.OnProjectAdded += ProjectManager_OnProjectAdded;
            ProjectManager.OnProjectRemoved += ProjectManager_OnProjectRemoved;
            ProjectManager.OnProjectUpdated += ProjectManager_OnProjectUpdated;
            UIThemeManager.OnThemeChanged(this, ApplyThemeCustom_Implementation);

            Maps = HeaderFactory("Maps");
            Mods = HeaderFactory("Mods");

            ProjectTNCache = new Dictionary<ML2Project, ML2ProjectTreeNode>();

            foreach (var project in ProjectManager.GetProjects())
            {
                AddProject(project);
            }

            ProjectTree.NodeMouseDoubleClick += ProjectTree_NodeMouseDoubleClick;
        }

        private void ProjectTree_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            if (e.Node is ML2ProjectTreeNode node)
            {
                OnProjectSelected?.Invoke(node.Project);
            }
        }

        private TreeNode HeaderFactory(string name)
        {
            TreeNode node = new TreeNode();
            node.Text = name;
            ProjectTree.Nodes.Add(node);
            return node;
        }

        private ML2ProjectTreeNode ProjectFactory(ML2Project project)
        {
            ML2ProjectTreeNode node = new ML2ProjectTreeNode(project);
            ProjectTNCache[project] = node;
            UpdateProject(project);
            return node;
        }

        internal void ProjectSelected(ML2Project project)
        {
            OnProjectSelected?.Invoke(project);
        }

        internal void ApplyThemeCustom_Implementation(UIThemeInfo themeData)
        {

        }

        private void ProjectDiscoveryTimer_Tick(object sender, EventArgs e)
        {
            ProjectManager.DiscoverProjects();
        }

        private void ProjectManager_OnProjectAdded(ML2Project project)
        {
            AddProject(project);
        }

        private void ProjectManager_OnProjectRemoved(ML2Project project)
        {
            RemoveProject(project);
        }

        private void ProjectManager_OnProjectUpdated(ML2Project project)
        {
            UpdateProject(project);
        }

        #region IContentPanel
        public bool CanClosePanelNow()
        {
            return true;
        }

        public void OnContentClosing()
        {
            ProjectDiscoveryTimer.Enabled = false;
            ProjectDiscoveryTimer.Stop();
        }

        public void OnContentOpening()
        {
            ProjectDiscoveryTimer.Enabled = true;
            ProjectDiscoveryTimer.Start();
        }

        public IEnumerable<Control> GetThemedControls()
        {
            yield return ProjectTree;
        }

        private class ML2ProjectTreeNode : TreeNode
        {
            internal readonly ML2Project Project;
            public ML2ProjectTreeNode(ML2Project project) : base()
            {
                Project = project;
            }
        }

        internal void AddProject(ML2Project project)
        {
            if (ProjectTNCache.ContainsKey(project))
            {
                UpdateProject(project);
                return;
            }
            var header = project.Data.SimpleIsMod ? Mods : Maps;
            header.Nodes.Add(ProjectFactory(project));
        }

        internal void RemoveProject(ML2Project project)
        {
            if (!ProjectTNCache.ContainsKey(project))
            {
                return;
            }
            var header = project.Data.SimpleIsMod ? Mods : Maps;
            header.Nodes.Remove(ProjectTNCache[project]);
            ProjectTNCache.Remove(project);
        }

        /// <summary>
        /// Apply data bindings to the UI when necessary
        /// </summary>
        /// <param name="project"></param>
        internal void UpdateProject(ML2Project project)
        {
            if (!ProjectTNCache.ContainsKey(project))
            {
                return;
            }
            var tvi = ProjectTNCache[project];
            tvi.Text = project.Data.FriendlyName;
        }
        #endregion
    }
}
