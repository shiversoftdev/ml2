﻿using ML2.Core;
using ML2.UI.Core.Interfaces;
using ML2.UI.Core.Singletons;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ML2.UI.Application
{
    public partial class CWorkPane : UserControl, IThemeableControl
    {
        private ML2DetailedProjectTreeNode Header;
        private Dictionary<string, ML2ProjectZoneNode> ZoneNodes;
        public CWorkPane()
        {
            InitializeComponent();
            ZoneNodes = new Dictionary<string, ML2ProjectZoneNode>();
            ProjectManager.OnActiveProjectChanged += OnActiveProjectChanged;
            Shared.Console.OnLogMessage += OnConsoleMessage;
            UIThemeManager.OnThemeChanged(this, ThemeUpdated);

            ProjectTree.NodeMouseDoubleClick += ProjectTree_NodeMouseDoubleClick;

            ProjectTree.AfterLabelEdit += ProjectTree_AfterLabelEdit;
            ProjectTree.BeforeCollapse += ProjectTree_BeforeCollapse;

            ProjectTree.KeyDown += ProjectTree_KeyDown;
            ProjectTree.AfterCheck += ProjectTree_AfterCheck;
        }

        private bool DontMatchChecksRightNow = false;
        private void ProjectTree_AfterCheck(object sender, TreeViewEventArgs e)
        {
            if(e.Node == null || DontMatchChecksRightNow)
            {
                return;
            }

            if (Header is null)
            {
                return;
            }

            if (e.Node == Header)
            {
                foreach (var node in ZoneNodes.Values)
                {
                    node.Checked = Header.Checked;
                }
                return;
            }
            else
            {
                if(Header.Checked != e.Node.Checked)
                {
                    bool consensus = e.Node.Checked;
                    foreach(var node in ZoneNodes.Values)
                    {
                        if(node == e.Node)
                        {
                            continue;
                        }
                        if(consensus != node.Checked)
                        {
                            if(Header.Checked)
                            {
                                DontMatchChecksRightNow = true;
                                Header.Checked = false;
                                DontMatchChecksRightNow = false;
                            }
                            return;
                        }
                    }
                    DontMatchChecksRightNow = true;
                    Header.Checked = true;
                    DontMatchChecksRightNow = false;
                }
            }
        }

        private void ProjectTree_KeyDown(object sender, KeyEventArgs e)
        {
            var tree = (sender as TreeView);
            if(tree is null)
            {
                return;
            }

            if(tree.SelectedNode is null)
            {
                return;
            }

            if(tree.SelectedNode == Header && e.KeyCode == Keys.F2)
            {
                tree.LabelEdit = true;
                Header.BeginEdit();
                return;
            }
        }

        internal void Opening()
        {
            Header?.ExpandAll();
        }

        private void ProjectTree_BeforeCollapse(object sender, TreeViewCancelEventArgs e)
        {
            if(e.Node == Header)
            {
                e.Cancel = true;
                return;
            }
        }

        private void ProjectTree_AfterLabelEdit(object sender, NodeLabelEditEventArgs e)
        {
            ProjectTree.LabelEdit = false;
            
            if(e.CancelEdit)
            {
                return;
            }

            if (e.Node != Header)
            {
                e.CancelEdit = true;
                return;
            }

            string target = null;
            if(e.Label == null || Header.Project.FriendlyName == (target = e.Label.Trim()))
            {
                e.CancelEdit = true;
                return;
            }

            Header.Project.FriendlyName = target;
            e.CancelEdit = true;
            e.Node.Text = Header.Project.FriendlyName;
        }

        private void ProjectTree_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            //if(!(Header is null) && e.Node == Header)
            //{
            //    ProjectTree.LabelEdit = true;
            //    Header.BeginEdit();
            //}
        }

        private ML2DetailedProjectTreeNode ProjectFactory(ML2Project project)
        {
            ML2DetailedProjectTreeNode node = new ML2DetailedProjectTreeNode(project);
            node.Text = project.FriendlyName;
            node.ExpandAll();
            return node;
        }

        private ML2ProjectZoneNode ZoneFactory(ML2Project project, string zone)
        {
            ML2ProjectZoneNode node = new ML2ProjectZoneNode(project, zone);
            node.Text = zone;
            ZoneNodes[zone] = node;
            return node;
        }

        private void ThemeUpdated(UIThemeInfo themeData)
        {
            ConsoleBox.BorderStyle = BorderStyle.None;
            ConsoleBox.ForeColor = Color.WhiteSmoke;
            ConsoleBox.BackColor = Color.Black;
            ProjectTree.BorderStyle = BorderStyle.None;
        }

        private void UpdateRenderedInfo()
        {
            if(Header is null)
            {
                return;
            }
            Header.Text = Header.Project.FriendlyName;
        }

        private void OnActiveProjectChanged(ML2Project project)
        {
            if (!(Header is null))
            {
                if(Header.Project == project)
                {
                    UpdateRenderedInfo();
                    return;
                }

                Header.Project.OnZonesUpdated -= OnZonesUpdated;

                Header.Nodes.Clear();
                ProjectTree.Nodes.Remove(Header);
                Header = null;
            }

            if (project is null)
            {
                return;
            }

            Header = ProjectFactory(project);
            ProjectTree.Nodes.Add(Header);
            Header.Project.OnZonesUpdated += OnZonesUpdated;

            foreach(var zone in project.GetZones())
            {
#if DEBUG
                Debug.Assert(Header.Project == project);
#endif
                Header.Nodes.Add(ZoneFactory(project, zone));
            }
        }

        private void OnZonesUpdated(ML2Project project, string[] removedZones, string[] addedZones)
        {
            if(removedZones.Length + addedZones.Length == 0)
            {
                return;
            }

            if(Header is null)
            {
                return;
            }

            foreach(var zone in removedZones)
            {
                if(!ZoneNodes.ContainsKey(zone))
                {
                    continue;
                }
                Header.Nodes.Remove(ZoneNodes[zone]);
                ZoneNodes.Remove(zone);
            }

            foreach (var zone in addedZones)
            {
#if DEBUG
                Debug.Assert(Header.Project == project);
#endif
                Header.Nodes.Add(ZoneFactory(project, zone));
            }
        }

        public IEnumerable<Control> GetThemedControls()
        {
            yield return ProjectTree;
            yield return WindowsFormsIsDOGSHIT;
            yield return ThisShitIsAJOKE;
        }

        Color previous = Color.White;
        private void HighlightText(ReadOnlySpan<char> text)
        {
            int index;
            Color c = previous;

            while ((index = text.IndexOf('^')) > -1)
            {
                var selection = text.Slice(0, index);
                int prevLen = ConsoleBox.Text.Length;
                ConsoleBox.AppendText(selection.ToString());
                ConsoleBox.SelectionStart = prevLen;
                ConsoleBox.SelectionLength = selection.Length;
                ConsoleBox.SelectionColor = c;

                if (text.Length > index + 1)
                {
                    switch (text[index + 1])
                    {
                        case '0':
                            c = Color.White;
                            break;
                        case '1':
                            c = Color.FromArgb(247, 10, 10);
                            break;
                        case '2':
                            c = Color.FromArgb(79, 235, 52);
                            break;
                        case '3':
                            c = Color.FromArgb(237, 222, 5);
                            break;
                        case '4':
                            c = Color.Blue;
                            break;
                        case '5':
                            c = Color.FromArgb(15, 171, 214);
                            break;
                        case '6':
                            c = Color.FromArgb(189, 52, 235);
                            break;
                        case '7':
                            c = Color.White;
                            break;
                        case '8':
                            c = Color.Gray;
                            break;
                        case '9':
                            c = Color.DarkOrange;
                            break;
                    }
                }

                if (index + 2 < text.Length)
                {
                    text = text.Slice(index + 2);
                }
                else
                {
                    text = "".AsSpan();
                }
            }
            {
                var selection = text;
                int prevLen = ConsoleBox.Text.Length;
                ConsoleBox.AppendText(text.ToString());
                ConsoleBox.SelectionStart = prevLen;
                ConsoleBox.SelectionLength = selection.Length;
                ConsoleBox.SelectionColor = c;
            }
            previous = c;
        }

        private void OnConsoleMessage(string message)
        {
            HighlightText(message.AsSpan());
            ConsoleBox.SelectionStart = ConsoleBox.Text.Length;
            ConsoleBox.ScrollToCaret();
        }

        private class ML2DetailedProjectTreeNode : TreeNode
        {
            internal readonly ML2Project Project;
            public ML2DetailedProjectTreeNode(ML2Project project) : base()
            {
                Project = project;
            }
        }

        private class ML2ProjectZoneNode : TreeNode
        {
            internal readonly ML2Project Project;
            internal readonly string Zone;
            public ML2ProjectZoneNode(ML2Project project, string zone) : base()
            {
                Project = project;
                Zone = zone;
            }
        }
    }

    internal class ZoneTreePanel : Panel
    {
        private const int BORDER_SIZE = 1;
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            ControlPaint.DrawBorder(e.Graphics, ClientRectangle,
                                  UIThemeManager.CurrentTheme.TitleBarColor, 0, ButtonBorderStyle.Solid,
                                  UIThemeManager.CurrentTheme.TitleBarColor, 0, ButtonBorderStyle.Solid,
                                  UIThemeManager.CurrentTheme.TitleBarColor, BORDER_SIZE, ButtonBorderStyle.Solid,
                                  UIThemeManager.CurrentTheme.TitleBarColor, 0, ButtonBorderStyle.Solid);
        }
    }

    internal class ConsolePanel : Panel
    {
        public ConsolePanel() : base()
        {
            UIThemeManager.OnThemeChanged(this, ThemeUpdated);
        }

        private void ThemeUpdated(UIThemeInfo themeData)
        {
            BackColor = Color.Black;
        }

        private const int BORDER_SIZE = 2;
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            ControlPaint.DrawBorder(e.Graphics, ClientRectangle,
                                  UIThemeManager.CurrentTheme.AccentColor, 0, ButtonBorderStyle.Solid,
                                  UIThemeManager.CurrentTheme.AccentColor, BORDER_SIZE, ButtonBorderStyle.Solid,
                                  UIThemeManager.CurrentTheme.AccentColor, 0, ButtonBorderStyle.Solid,
                                  UIThemeManager.CurrentTheme.AccentColor, 0, ButtonBorderStyle.Solid);
        }
    }
}
