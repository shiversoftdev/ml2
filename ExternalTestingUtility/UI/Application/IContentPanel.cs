using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ML2.UI.Application
{
    interface IContentPanel
    {
        bool CanClosePanelNow();
        void OnContentClosing();
        void OnContentOpening();
    }
}
