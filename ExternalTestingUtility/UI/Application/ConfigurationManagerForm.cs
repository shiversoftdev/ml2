using ML2.Core;
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
    public partial class ConfigurationManagerForm : Form, IThemeableControl
    {
        private TreeNode Templates, Project;
        private Dictionary<int, ProjectConfigurationTreeNode> ConfigNodeMap;
        public ConfigurationManagerForm()
        {
            InitializeComponent();
            ConfigNodeMap = new Dictionary<int, ProjectConfigurationTreeNode>();

            UIThemeManager.OnThemeChanged(this, OnThemeChanged_Implementation);
            this.SetThemeAware();
            MaximizeBox = true;
            MinimizeBox = true;
            Text = "Configuration Manager";

            ConfigTreeView.Nodes.Add(Templates = HeaderFactory("Templates"));
            ConfigTreeView.Nodes.Add(Project = HeaderFactory(ProjectManager.ActiveProject.FriendlyName));

            ConfigTreeView.NodeMouseClick += ConfigTreeView_NodeMouseClick;
            ConfigTreeView.KeyDown += ConfigTreeView_KeyDown;
            ConfigTreeView.BeforeLabelEdit += ConfigTreeView_BeforeLabelEdit;
            ConfigTreeView.AfterLabelEdit += ConfigTreeView_AfterLabelEdit;

            Templates.Collapse();
            Project.ExpandAll();

            UpdateProjectConfigs();
        }

        private void ConfigTreeView_AfterLabelEdit(object sender, NodeLabelEditEventArgs e)
        {
            if(e.Label == null || e.Label.Length < 1)
            {
                e.CancelEdit = true;
                return;
            }

            if (e.Node is ProjectConfigurationTreeNode node)
            {
                if(ProjectManager.ActiveProject.GetConfiguration(node.ConfigIndex).Name != e.Label)
                {
                    for(int i = 0; i < ProjectManager.ActiveProject.ConfigCount; i++)
                    {
                        if(i == node.ConfigIndex)
                        {
                            continue;
                        }
                        if(e.Label == ProjectManager.ActiveProject.GetConfiguration(i).Name)
                        {
                            CErrorDialog.Show("Rename Failed", "There is already a build configuration with this name.");
                            e.CancelEdit = true;
                            return;
                        }
                    }
                    ProjectManager.ActiveProject.GetConfiguration(node.ConfigIndex).Name = e.Label;
                    ProjectManager.ActiveProject.SaveToDisk();
                }
            }
            ConfigTreeView.LabelEdit = false;
        }

        private void ConfigTreeView_BeforeLabelEdit(object sender, NodeLabelEditEventArgs e)
        {
            if (e.Node == Project || e.Node == Templates)
            {
                ConfigTreeView.LabelEdit = false;
                e.CancelEdit = true;
                return;
            }
        }

        private void ConfigTreeView_KeyDown(object sender, KeyEventArgs e)
        {
            if(ConfigTreeView.SelectedNode is null)
            {
                return;
            }

            if(e.KeyCode == Keys.F2)
            {
                if(ConfigTreeView.SelectedNode == Project || ConfigTreeView.SelectedNode == Templates)
                {
                    return;
                }
                ConfigTreeView.LabelEdit = true;
                ConfigTreeView.SelectedNode.BeginEdit();
            }
        }

        private bool UpdateContextItems(TreeNodeMouseClickEventArgs e)
        {
            if(e.Node == Templates)
            {
                return false;
            }

            NodeContextMenu.Items.Clear();

            if (e.Node == Project)
            {
                NodeContextMenu.Items.Add(newToolStripMenuItem);
                return true;
            }

            NodeContextMenu.Items.Add(saveAsTemplateToolStripMenuItem);
            NodeContextMenu.Items.Add(duplicateToolStripMenuItem);
            NodeContextMenu.Items.Add(toolStripSeparator1);
            NodeContextMenu.Items.Add(renameToolStripMenuItem);
            NodeContextMenu.Items.Add(toolStripSeparator2);
            NodeContextMenu.Items.Add(deleteToolStripMenuItem);

            return true;
        }

        private void ConfigTreeView_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            if(e.Button == MouseButtons.Right)
            {
                ConfigTreeView.SelectedNode = e.Node;
                if(!UpdateContextItems(e))
                {
                    return;
                }
                NodeContextMenu.Show(ConfigTreeView, e.Location);
                return;
            }
        }

        private void UpdateProjectConfigs()
        {
            Project.Nodes.Clear();
            ConfigNodeMap.Clear();

            for(int i = 0; i < ProjectManager.ActiveProject.ConfigCount; i++)
            {
                Project.Nodes.Add(ProjectNodeFactory(i));
            }

            Project.ExpandAll();
        }

        private TreeNode HeaderFactory(string text)
        {
            TreeNode node = new TreeNode();
            node.Text = text;
            return node;
        }

        private ProjectConfigurationTreeNode ProjectNodeFactory(int index)
        {
            ProjectConfigurationTreeNode node = new ProjectConfigurationTreeNode(index);
            node.Text = ProjectManager.ActiveProject.GetConfiguration(index).ToString();
            ConfigNodeMap[index] = node;
            return node;
        }

        private void OnThemeChanged_Implementation(UIThemeInfo themeData)
        {
            return;
        }

        public IEnumerable<Control> GetThemedControls()
        {
            yield return InnerForm;
            yield return ConfigTreeView;
            yield return NodeContextMenu;
        }

        private void AcceptButton_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void renameToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (ConfigTreeView.SelectedNode == Project || ConfigTreeView.SelectedNode == Templates)
            {
                return;
            }
            ConfigTreeView.LabelEdit = true;
            ConfigTreeView.SelectedNode.BeginEdit();
        }

        private void duplicateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (ConfigTreeView.SelectedNode == Project || ConfigTreeView.SelectedNode == Templates)
            {
                return;
            }

            if (ConfigTreeView.SelectedNode is ProjectConfigurationTreeNode node)
            {
                ProjectManager.ActiveProject.CloneConfig(node.Index);
                ProjectManager.ActiveProject.SaveToDisk();
                UpdateProjectConfigs();
                ConfigTreeView.SelectedNode = ConfigNodeMap[ProjectManager.ActiveProject.ConfigCount - 1];
                return;
            }
        }

        private void debugToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ProjectManager.ActiveProject.AddDebugConfig();
            ProjectManager.ActiveProject.SaveToDisk();
            UpdateProjectConfigs();
            ConfigTreeView.SelectedNode = ConfigNodeMap[ProjectManager.ActiveProject.ConfigCount - 1];
        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (ConfigTreeView.SelectedNode == Project || ConfigTreeView.SelectedNode == Templates)
            {
                return;
            }

            if (ConfigTreeView.SelectedNode is ProjectConfigurationTreeNode node)
            {
                if (ProjectManager.ActiveProject.ConfigCount < 2)
                {
                    CErrorDialog.Show("Failed to Delete", "Projects must have at least one build configuration.");
                    return;
                }

                var dialog = new CYesNoDialog("Delete Configuration", $"Are you sure you want to delete configuration '{ProjectManager.ActiveProject.GetConfiguration(node.ConfigIndex)}'? You cannot undo this action.");
                if(dialog.ShowDialog() != DialogResult.Yes)
                {
                    return;
                }

                ProjectManager.ActiveProject.DeleteConfiguration(node.ConfigIndex);
                ProjectManager.ActiveProject.SaveToDisk();
                ConfigTreeView.SelectedNode = null;
                UpdateProjectConfigs();

                return;
            }
        }

        private class ProjectConfigurationTreeNode : TreeNode
        {
            public int ConfigIndex { get; private set; }
            public ProjectConfigurationTreeNode(int configIndex) : base()
            {
                ConfigIndex = configIndex;
            }
        }
    }
}
