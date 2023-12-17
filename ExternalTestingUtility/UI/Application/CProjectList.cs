using ML2.Core;
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
    public partial class CProjectList : UserControl, IContentPanel
    {
        public CProjectList()
        {
            InitializeComponent();
            ProjectDiscoveryTimer.Tick += ProjectDiscoveryTimer_Tick;
            ProjectManager.OnProjectAdded += ProjectManager_OnProjectAdded;
            ProjectManager.OnProjectRemoved += ProjectManager_OnProjectRemoved;
        }

        private void ProjectDiscoveryTimer_Tick(object sender, EventArgs e)
        {
            ProjectManager.DiscoverProjects();
        }

        private void ProjectManager_OnProjectAdded(ML2Project project)
        {

        }

        private void ProjectManager_OnProjectRemoved(ML2Project project)
        {

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
        #endregion
    }
}
