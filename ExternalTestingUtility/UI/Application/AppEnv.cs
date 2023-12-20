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
                return ActivitiesStack.Count > 0 ? ActivitiesStack[ActivitiesStack.Count - 1] : "";
            }
        }

        private static List<string> ActivitiesStack = new List<string>();
        public static void PushActivity(string status)
        {
            ActivitiesStack.Add(status);
            OnActivityChanged?.Invoke();
        }

        public static void PopActivity(string status)
        {
#if DEBUG
            Debug.Assert(ActivitiesStack[ActivitiesStack.Count - 1] == status);
#endif
            ActivitiesStack.RemoveAt(ActivitiesStack.Count - 1);
            OnActivityChanged?.Invoke();
        }

        public static void ReplaceActivity(string oldVal, string newVal)
        {
            if(ActivityStatus == oldVal)
            {
                ActivitiesStack[ActivitiesStack.Count - 1] = newVal;
                OnActivityChanged?.Invoke();
            }
            for(int i = 0; i < ActivitiesStack.Count - 1; i++)
            {
                if(ActivitiesStack[i] == oldVal)
                {
                    ActivitiesStack[i] = newVal;
                }
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
