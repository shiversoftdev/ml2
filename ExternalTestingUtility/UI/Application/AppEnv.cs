using System;
using System.Collections.Generic;
using System.Diagnostics;
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

        internal static Action OnActivityChanged;
        public static string ActivityStatus
        {
            get
            {
                return ActivitiesStack.Count > 0 ? ActivitiesStack.Peek() : "";
            }
        }

        private static Stack<string> ActivitiesStack = new Stack<string>();
        public static void PushActivity(string status)
        {
            ActivitiesStack.Push(status);
            OnActivityChanged?.Invoke();
        }

        public static void PopActivity(string status)
        {
#if DEBUG
            Debug.Assert(ActivitiesStack.Peek() == status);
#endif
            ActivitiesStack.Pop();
            OnActivityChanged?.Invoke();
        }
    }

    internal enum ExitProhibitedReason
    {
        ALLOWED,
        UNSAVED_WORK,
        CRITICAL_WORK_IN_PROGRESS
    }
}
