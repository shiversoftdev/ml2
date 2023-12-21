
namespace ML2.UI.Application
{
    partial class CBuildConfigPane
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.BuildConfigDataGrid = new System.Windows.Forms.DataGridView();
            this.OrderCol = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.EnabledColumn = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.ActionCol = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DescriptionCol = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DataGridContextMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.deleteActionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.newBuildActionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.duplicateToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.editToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.BuildConfigDataGrid)).BeginInit();
            this.DataGridContextMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.BuildConfigDataGrid);
            this.splitContainer1.Size = new System.Drawing.Size(791, 340);
            this.splitContainer1.SplitterDistance = 500;
            this.splitContainer1.TabIndex = 0;
            // 
            // BuildConfigDataGrid
            // 
            this.BuildConfigDataGrid.AllowDrop = true;
            this.BuildConfigDataGrid.AllowUserToAddRows = false;
            this.BuildConfigDataGrid.AllowUserToDeleteRows = false;
            this.BuildConfigDataGrid.AllowUserToResizeRows = false;
            this.BuildConfigDataGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.BuildConfigDataGrid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.OrderCol,
            this.EnabledColumn,
            this.ActionCol,
            this.DescriptionCol});
            this.BuildConfigDataGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.BuildConfigDataGrid.Location = new System.Drawing.Point(0, 0);
            this.BuildConfigDataGrid.MultiSelect = false;
            this.BuildConfigDataGrid.Name = "BuildConfigDataGrid";
            this.BuildConfigDataGrid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.BuildConfigDataGrid.Size = new System.Drawing.Size(500, 340);
            this.BuildConfigDataGrid.TabIndex = 1;
            // 
            // OrderCol
            // 
            this.OrderCol.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.OrderCol.FillWeight = 5F;
            this.OrderCol.Frozen = true;
            this.OrderCol.HeaderText = "#";
            this.OrderCol.MinimumWidth = 40;
            this.OrderCol.Name = "OrderCol";
            this.OrderCol.ReadOnly = true;
            this.OrderCol.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.OrderCol.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.OrderCol.ToolTipText = "Build Order";
            this.OrderCol.Width = 40;
            // 
            // EnabledColumn
            // 
            this.EnabledColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.EnabledColumn.HeaderText = "Run";
            this.EnabledColumn.MinimumWidth = 40;
            this.EnabledColumn.Name = "EnabledColumn";
            this.EnabledColumn.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.EnabledColumn.ToolTipText = "Enable Step";
            this.EnabledColumn.Width = 40;
            // 
            // ActionCol
            // 
            this.ActionCol.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.ActionCol.FillWeight = 50F;
            this.ActionCol.HeaderText = "Action";
            this.ActionCol.MinimumWidth = 50;
            this.ActionCol.Name = "ActionCol";
            this.ActionCol.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.ActionCol.ToolTipText = "Action Name";
            // 
            // DescriptionCol
            // 
            this.DescriptionCol.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.DescriptionCol.FillWeight = 60F;
            this.DescriptionCol.HeaderText = "Description";
            this.DescriptionCol.MinimumWidth = 60;
            this.DescriptionCol.Name = "DescriptionCol";
            this.DescriptionCol.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.DescriptionCol.ToolTipText = "Action Description";
            // 
            // DataGridContextMenu
            // 
            this.DataGridContextMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newBuildActionToolStripMenuItem,
            this.editToolStripMenuItem,
            this.duplicateToolStripMenuItem,
            this.toolStripSeparator1,
            this.deleteActionToolStripMenuItem});
            this.DataGridContextMenu.Name = "DataGridContextMenu";
            this.DataGridContextMenu.Size = new System.Drawing.Size(181, 120);
            // 
            // deleteActionToolStripMenuItem
            // 
            this.deleteActionToolStripMenuItem.Name = "deleteActionToolStripMenuItem";
            this.deleteActionToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.deleteActionToolStripMenuItem.Text = "Delete";
            this.deleteActionToolStripMenuItem.Click += new System.EventHandler(this.deleteActionToolStripMenuItem_Click);
            // 
            // newBuildActionToolStripMenuItem
            // 
            this.newBuildActionToolStripMenuItem.Name = "newBuildActionToolStripMenuItem";
            this.newBuildActionToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.newBuildActionToolStripMenuItem.Text = "New Build Action...";
            this.newBuildActionToolStripMenuItem.Click += new System.EventHandler(this.newBuildActionToolStripMenuItem_Click);
            // 
            // duplicateToolStripMenuItem
            // 
            this.duplicateToolStripMenuItem.Name = "duplicateToolStripMenuItem";
            this.duplicateToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.duplicateToolStripMenuItem.Text = "Duplicate";
            this.duplicateToolStripMenuItem.Click += new System.EventHandler(this.duplicateToolStripMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(172, 6);
            // 
            // editToolStripMenuItem
            // 
            this.editToolStripMenuItem.Name = "editToolStripMenuItem";
            this.editToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.editToolStripMenuItem.Text = "Edit...";
            this.editToolStripMenuItem.Click += new System.EventHandler(this.editToolStripMenuItem_Click);
            // 
            // CBuildConfigPane
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(14)))), ((int)(((byte)(14)))), ((int)(((byte)(18)))));
            this.Controls.Add(this.splitContainer1);
            this.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.Name = "CBuildConfigPane";
            this.Size = new System.Drawing.Size(791, 340);
            this.splitContainer1.Panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.BuildConfigDataGrid)).EndInit();
            this.DataGridContextMenu.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.DataGridView BuildConfigDataGrid;
        private System.Windows.Forms.DataGridViewTextBoxColumn OrderCol;
        private System.Windows.Forms.DataGridViewCheckBoxColumn EnabledColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn ActionCol;
        private System.Windows.Forms.DataGridViewTextBoxColumn DescriptionCol;
        private System.Windows.Forms.ContextMenuStrip DataGridContextMenu;
        private System.Windows.Forms.ToolStripMenuItem deleteActionToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem newBuildActionToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem duplicateToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem editToolStripMenuItem;
    }
}
