using ML2.Core;
using ML2.UI.Core.Controls;
using ML2.UI.Core.Interfaces;
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
    public partial class CBuildConfigPane : UserControl, IThemeableControl, IContentPanel
    {
        private bool SuspendConfigEvents = false;
        public CBuildConfigPane()
        {
            InitializeComponent();

            BuildConfigDataGrid.Rows.Add(new object[] {"*", false, ML2BuildAction.None.ToString(), "No build steps found." });
            BuildConfigDataGrid.ReadOnly = true;

            ProjectManager.OnActiveProjectChanged += OnActiveProjectChanged;
            ProjectManager.BeforeActiveProjectChanged += BeforeActiveProjectChanged;

            BuildConfigDataGrid.CellBeginEdit += BuildConfigDataGrid_CellBeginEdit;
            BuildConfigDataGrid.CellEndEdit += BuildConfigDataGrid_CellEndEdit;

            BuildConfigDataGrid.MouseMove += BuildConfigDataGrid_MouseMove;
            BuildConfigDataGrid.MouseDown += BuildConfigDataGrid_MouseDown;
            BuildConfigDataGrid.DragOver += BuildConfigDataGrid_DragOver;
            BuildConfigDataGrid.DragDrop += BuildConfigDataGrid_DragDrop;

            BuildConfigDataGrid.CellMouseClick += BuildConfigDataGrid_CellMouseClick;
        }

        private DataGridViewCellMouseEventArgs ContextMenuSelectedNode;
        private void BuildConfigDataGrid_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if(e.RowIndex < 0)
            {
                return;
            }
            if(e.Button == MouseButtons.Right)
            {
                if((CachedProject?.ActiveConfig.ActionsCount ?? 0) <= 0)
                {
                    DataGridContextMenu.Items.Remove(duplicateToolStripMenuItem);
                    DataGridContextMenu.Items.Remove(toolStripSeparator1);
                    DataGridContextMenu.Items.Remove(deleteActionToolStripMenuItem);
                    DataGridContextMenu.Items.Remove(editToolStripMenuItem);
                }
                else
                {
                    
                    if (!DataGridContextMenu.Items.Contains(editToolStripMenuItem))
                    {
                        DataGridContextMenu.Items.Add(editToolStripMenuItem);
                    }
                    if (!DataGridContextMenu.Items.Contains(duplicateToolStripMenuItem))
                    {
                        DataGridContextMenu.Items.Add(duplicateToolStripMenuItem);
                    }
                    if (!DataGridContextMenu.Items.Contains(toolStripSeparator1))
                    {
                        DataGridContextMenu.Items.Add(toolStripSeparator1);
                    }
                    if (!DataGridContextMenu.Items.Contains(deleteActionToolStripMenuItem))
                    {
                        DataGridContextMenu.Items.Add(deleteActionToolStripMenuItem);
                    }
                }
                ContextMenuSelectedNode = e;
                BuildConfigDataGrid.Rows[e.RowIndex].Cells[e.ColumnIndex].Selected = true;
                var origin = BuildConfigDataGrid.GetCellDisplayRectangle(e.ColumnIndex, e.RowIndex, false).Location;
                DataGridContextMenu.Show(BuildConfigDataGrid, new Point(origin.X + e.Location.X, origin.Y + e.Location.Y));
            }
        }

        private void BuildConfigDataGrid_DragDrop(object sender, DragEventArgs e)
        {
            // The mouse locations are relative to the screen, so they must be 
            // converted to client coordinates.
            Point clientPoint = BuildConfigDataGrid.PointToClient(new Point(e.X, e.Y));

            // Get the row index of the item the mouse is below. 
            RowIndexOfItemUnderMouseToDrop = BuildConfigDataGrid.HitTest(clientPoint.X, clientPoint.Y).RowIndex;

            // If the drag operation was a move then remove and insert the row.
            if (e.Effect == DragDropEffects.Move)
            {
                DataGridViewRow rowToMove = e.Data.GetData(typeof(DataGridViewRow)) as DataGridViewRow;
                BuildConfigDataGrid.Rows.RemoveAt(RowIndexFromMouseDown);
                BuildConfigDataGrid.Rows.Insert(RowIndexOfItemUnderMouseToDrop, rowToMove);
                ProjectManager.ActiveProject.ActiveConfig.ReorderBuild(RowIndexFromMouseDown, RowIndexOfItemUnderMouseToDrop);
                ProjectManager.ActiveProject.SaveToDisk();
                RefreshConfigTable();
                BuildConfigDataGrid.Rows[RowIndexOfItemUnderMouseToDrop].Cells[0].Selected = true;
            }
        }

        private void BuildConfigDataGrid_DragOver(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.Move;
        }

        private void BuildConfigDataGrid_MouseDown(object sender, MouseEventArgs e)
        {
            // Get the index of the item the mouse is below.
            RowIndexFromMouseDown = BuildConfigDataGrid.HitTest(e.X, e.Y).RowIndex;

            if (RowIndexFromMouseDown != -1)
            {
                // Remember the point where the mouse down occurred. 
                // The DragSize indicates the size that the mouse can move 
                // before a drag event should be started.                
                Size dragSize = SystemInformation.DragSize;

                // Create a rectangle using the DragSize, with the mouse position being
                // at the center of the rectangle.
                DragBoxFromMouseDown = new Rectangle(
                          new Point(
                            e.X - (dragSize.Width / 2),
                            e.Y - (dragSize.Height / 2)),
                      dragSize);
            }
            else
            {
                // Reset the rectangle if the mouse is not over an item in the ListBox.
                DragBoxFromMouseDown = Rectangle.Empty;
            }
        }

        private Rectangle DragBoxFromMouseDown;
        private int RowIndexFromMouseDown;
        private int RowIndexOfItemUnderMouseToDrop;
        private void BuildConfigDataGrid_MouseMove(object sender, MouseEventArgs e)
        {
            if ((e.Button & MouseButtons.Left) == MouseButtons.Left)
            {
                // If the mouse moves outside the rectangle, start the drag.
                if (DragBoxFromMouseDown != Rectangle.Empty &&
                !DragBoxFromMouseDown.Contains(e.X, e.Y))
                {
                    // Proceed with the drag and drop, passing in the list item.                    
                    DragDropEffects dropEffect = BuildConfigDataGrid.DoDragDrop(
                          BuildConfigDataGrid.Rows[RowIndexFromMouseDown],
                          DragDropEffects.Move);
                }
            }
        }

        private void BuildConfigDataGrid_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if(ProjectManager.ActiveProject is null)
            {
                return;
            }

            int configIndex = (int)BuildConfigDataGrid.Rows[e.RowIndex].Cells[0].Value;
            switch(e.ColumnIndex)
            {
                case 0:
                    throw new InvalidOperationException("Cannot edit the numeric order column for a cell!");
                case 1: // Enabled
                    ProjectManager.ActiveProject.ActiveConfig.GetAction(configIndex).Enabled = (bool)BuildConfigDataGrid.Rows[e.RowIndex].Cells[e.ColumnIndex].Value;
                    ProjectManager.ActiveProject.SaveToDisk();
                    break;
                case 2: // Name
                    ProjectManager.ActiveProject.ActiveConfig.GetAction(configIndex).Name = BuildConfigDataGrid.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString();
                    ProjectManager.ActiveProject.SaveToDisk();
                    break;
                case 3: // Description
                    ProjectManager.ActiveProject.ActiveConfig.GetAction(configIndex).Description = BuildConfigDataGrid.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString();
                    ProjectManager.ActiveProject.SaveToDisk();
                    break;
            }
        }

        private void BuildConfigDataGrid_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            if(e.ColumnIndex == 0)
            {
                e.Cancel = true;
                return;
            }
        }

        private ML2Project CachedProject;
        private void OnActiveProjectChanged(ML2Project project)
        {
            if(CachedProject != project)
            {
                CachedProject = project;
                RefreshConfigTable();
            }
        }

        private void BeforeActiveProjectChanged(ML2Project project)
        {
            if (CachedProject == project)
            {
                // needs to happen BEFORE we change project
                if (BuildConfigDataGrid.IsCurrentCellDirty)
                {
                    BuildConfigDataGrid.EndEdit();
                }
            }
        }

        private void RefreshConfigTable()
        {
            if (CachedProject is null)
            {
                SuspendConfigEvents = true;
                BuildConfigDataGrid.Rows.Clear();
                BuildConfigDataGrid.Rows.Add(new object[] { "*", false, ML2BuildAction.None.ToString(), "No build steps found." });
                BuildConfigDataGrid.ReadOnly = true;
                SuspendConfigEvents = false;
                return;
            }

            SuspendConfigEvents = true;
            BuildConfigDataGrid.Rows.Clear();

            if (CachedProject.ActiveConfig.ActionsCount <= 0)
            {
                BuildConfigDataGrid.Rows.Add(new object[] { "*", false, ML2BuildAction.None.ToString(), "No build steps found." });
                BuildConfigDataGrid.ReadOnly = true;
            }
            else
            {
                BuildConfigDataGrid.ReadOnly = false;
                int i = 0;
                foreach (var action in CachedProject.ActiveConfig.GetActions())
                {
                    BuildConfigDataGrid.Rows.Add(new object[] { i, action.Enabled, action.Name, action.Description });
                    i++;
                }
            }

            SuspendConfigEvents = false;
        }

        public IEnumerable<Control> GetThemedControls()
        {
            yield return BuildConfigDataGrid;
            yield return DataGridContextMenu;
        }

        private void deleteActionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int configIndex = (int)BuildConfigDataGrid.Rows[ContextMenuSelectedNode.RowIndex].Cells[0].Value;
            var result = CYesNoDialog.Show("Delete Build Action", $"Are you sure you want to delete the action '{ProjectManager.ActiveProject.ActiveConfig.GetAction(configIndex).Name}'?");
            if(result != DialogResult.Yes)
            {
                return;
            }
            ProjectManager.ActiveProject.ActiveConfig.DeleteAction(configIndex);
            ProjectManager.ActiveProject.SaveToDisk();
            RefreshConfigTable();
        }

        private void duplicateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int configIndex = (int)BuildConfigDataGrid.Rows[ContextMenuSelectedNode.RowIndex].Cells[0].Value;
            ProjectManager.ActiveProject.ActiveConfig.Duplicate(configIndex);
            ProjectManager.ActiveProject.SaveToDisk();
            RefreshConfigTable();
        }

        public bool CanClosePanelNow()
        {
            return true;
        }

        public void OnContentClosing()
        {
        }

        public void OnContentOpening()
        {
            
        }

        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int configIndex = (int)BuildConfigDataGrid.Rows[ContextMenuSelectedNode.RowIndex].Cells[0].Value;
            if(new ActionForm(configIndex, true).ShowDialog() == DialogResult.OK)
            {
                RefreshConfigTable();
                BuildConfigDataGrid.Rows[ContextMenuSelectedNode.RowIndex].Cells[ContextMenuSelectedNode.ColumnIndex].Selected = true;
            }
        }

        private void newBuildActionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // TODO: create a new 'none' type action as last index, then send the index to the action form
        }
    }
}
