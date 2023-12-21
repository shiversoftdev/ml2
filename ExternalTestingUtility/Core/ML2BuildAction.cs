using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ML2.Core
{
    internal sealed class ML2BuildAction
    {
        private ML2BuildActionData Data;

        public bool Enabled
        {
            get
            {
                return Data.Enabled;
            }
            set
            {
                Data.Enabled = value;
            }
        }

        public string Description
        {
            get
            {
                return Data.Description;
            }
            set
            {
                Data.Description = value;
            }
        }

        public string Name
        {
            get
            {
                return Data.FriendlyName;
            }
            set
            {
                Data.FriendlyName = value;
            }
        }

        public ML2BuildAction(ML2BuildActionData data)
        {
            Data = data;
        }

        internal static readonly ML2BuildAction None;
        static ML2BuildAction()
        {
            ML2BuildActionData data = new ML2BuildActionData();
            data.Enabled = false;
            data.ActionTypeEnum = ML2BuildActionType.None;
            data.FriendlyName = "None";
            data.Description = "No build steps found.";
            None = new ML2BuildAction(data);
        }

        public override string ToString()
        {
            return Data.FriendlyName ?? (Data.ActionTypeEnum.ToString());
        }
    }

    public sealed class ML2BuildActionData
    {
        public bool Enabled { get; set; }
        public string ActionType { get; set; }

        public string FriendlyName { get; set; }
        public string Description { get; set; }
        public Dictionary<string, string> Options { get; set; }

        internal ML2BuildActionType ActionTypeEnum
        {
            get
            {
                if(Enum.TryParse(ActionType, out ML2BuildActionType actionType))
                {
                    return actionType;
                }
                return ML2BuildActionType.Unknown;
            }
            set
            {
                ActionType = value.ToString().ToLower();
            }
        }

        public ML2BuildActionData()
        {
            Enabled = false;
            ActionType = ML2BuildActionType.Unknown.ToString().ToLower();
            Options = new Dictionary<string, string>();
        }

        public static ML2BuildActionData DefaultCompile()
        {
            ML2BuildActionData data = new ML2BuildActionData();
            data.Description = "Compile map data";
            data.FriendlyName = "Compile (Full)";
            data.ActionTypeEnum = ML2BuildActionType.Compile;
            // TODO default arguments data.Options[""]
            data.Enabled = true;
            return data;
        }

        public static ML2BuildActionData DefaultLight()
        {
            ML2BuildActionData data = new ML2BuildActionData();
            data.Description = "Compile map lighting data";
            data.FriendlyName = "Light (Medium)";
            data.ActionTypeEnum = ML2BuildActionType.Light;
            data.Options["quality"] = "medium";
            // TODO default arguments data.Options[""]
            data.Enabled = true;
            return data;
        }

        public static ML2BuildActionData DefaultLink()
        {
            ML2BuildActionData data = new ML2BuildActionData();
            data.Description = "Link zone and build fast file";
            data.FriendlyName = "Link";
            data.ActionTypeEnum = ML2BuildActionType.Link;
            // TODO default arguments data.Options[""]
            data.Enabled = true;
            return data;
        }
    }

    public enum ML2BuildActionType
    {
        None,
        Unknown,
        CustomTool,
        Compile,
        Light,
        Link
    }
}
