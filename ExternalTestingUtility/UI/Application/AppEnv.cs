using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ML2.UI.Application
{
    internal static class AppEnv
    {
        private static ExitProhibitedReason __exitstatus = ExitProhibitedReason.ALLOWED;
        public static ExitProhibitedReason ExitStatus
        {
            set
            {
                __exitstatus = value;
            }
            get
            {
                return __exitstatus;
            }
        }
    }

    internal enum ExitProhibitedReason
    {
        ALLOWED,
        UNSAVED_WORK,
        CRITICAL_WORK_IN_PROGRESS
    }
}
