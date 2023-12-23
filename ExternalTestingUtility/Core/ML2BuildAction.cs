using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Reflection;
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

        public ML2BuildActionType Action
        {
            get
            {
                return Data.ActionTypeEnum;
            }
            set
            {
                Data.ActionTypeEnum = value;
            }
        }

        public string this[string index]
        {
            get
            {
                return Data.Options.ContainsKey(index.ToLower()) ? Data.Options[index.ToLower()].ToLower() : null;
            }
            set
            {
                Data.Options[index.ToLower()] = value;
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

        public ML2BuildActionEditor Editor()
        {
            switch(Data.ActionTypeEnum)
            {
                case ML2BuildActionType.Compile:
                    return new ML2ActionEditorCompile(this);
                case ML2BuildActionType.Light:
                    return new ML2ActionEditorLight(this);
                // TODO: linker command line options
                case ML2BuildActionType.CustomTool:
                    return new ML2ActionEditorCustom(this);
            }
            return new ML2BuildActionEditor(this);
        }

        public string DefaultName()
        {
            switch (Data.ActionTypeEnum)
            {
                case ML2BuildActionType.Compile:
                    return "Compile";
                case ML2BuildActionType.CustomTool:
                    return "Custom";
                case ML2BuildActionType.Light:
                    return "Light";
                case ML2BuildActionType.Link:
                    return "Link";
                case ML2BuildActionType.CleanXPaks:
                    return "Clean";
            }
            return "Build Step";
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
                if(Enum.TryParse(ActionType, true, out ML2BuildActionType actionType))
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
            data.Options["type"] = "full";
            // TODO default arguments
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
            // TODO default arguments
            data.Enabled = true;
            return data;
        }

        public static ML2BuildActionData DefaultLink()
        {
            ML2BuildActionData data = new ML2BuildActionData();
            data.Description = "Link zone and build fast file";
            data.FriendlyName = "Link";
            data.ActionTypeEnum = ML2BuildActionType.Link;
            // TODO default arguments
            data.Enabled = true;
            return data;
        }
    }

    public class ML2BuildActionEditor
    {
        [Category("(General)")]
        [Description("Determines if this build step is executed or skipped.")]
        public bool Enabled
        {
            get
            {
                return ActionObject.Enabled;
            }
            set
            {
                bool matched = ActionObject.Enabled == value;
                ActionObject.Enabled = value;
                if(!matched)
                {
                    OnAnyPropertyChanged?.Invoke();
                }
            }
        }

        [Category("(General)")]
        [Description("Friendly description of the build step.")]
        public string Description
        {
            get
            {
                return ActionObject.Description;
            }
            set
            {
                bool matched = ActionObject.Description == value;
                ActionObject.Description = value;
                if (!matched)
                {
                    OnAnyPropertyChanged?.Invoke();
                }
            }
        }

        [Category("(General)")]
        [DisplayName("(Name)")]
        [Description("Friendly name of the build step.")]
        public string Name
        {
            get
            {
                return ActionObject.Name;
            }
            set
            {
                bool matched = ActionObject.Name == value;
                ActionObject.Name = value;
                if (!matched)
                {
                    OnAnyPropertyChanged?.Invoke();
                }
            }
        }

        public Action OnActionTypeChanged;
        public Action OnAnyPropertyChanged;

        [DisplayName("Action Type")]
        [Category("(General)")]
        [Description("Changes the type of build step (will reset options).")]
        [TypeConverter(typeof(ActionTypeConverter))]
        public ML2BuildActionEditorType Action
        {
            get
            {
                return Enum.TryParse(ActionObject.Action.ToString(), true, out ML2BuildActionEditorType res) ? res : ML2BuildActionEditorType.CustomTool;
            }
            set
            {
                var newAction = Enum.TryParse(value.ToString(), true, out ML2BuildActionType res) ? res : ML2BuildActionType.Unknown;
                bool matched = ActionObject.Action == newAction;

                ActionObject.Action = newAction;
                OnActionTypeChanged?.Invoke();
                if (!matched)
                {
                    OnAnyPropertyChanged?.Invoke();
                }
            }
        }

        private protected ML2BuildAction ActionObject;
        internal ML2BuildActionEditor(ML2BuildAction action)
        {
            ActionObject = action;
        }

        public enum ML2BuildActionEditorType
        {
            [Description("Custom")]
            CustomTool,
            [Description("Compile")]
            Compile,
            [Description("Light")]
            Light,
            [Description("Link")]
            Link,
            [Description("Clean X-Paks")]
            CleanXPaks
        }
    }

    public class ML2ActionEditorCompile : ML2BuildActionEditor
    {
        [Category("Compile Options")]
        [Description("Allows quick, map-ents only compilation, if desired.")]
        [DisplayName("Type")]
        public ML2CompileType CompileType
        {
            get
            {
                return Enum.TryParse((ActionObject["type"] ?? (ActionObject["type"] = "full")), true, out ML2CompileType res) ? res : ML2CompileType.Full;
            }
            set
            {
                bool matched = value.ToString().ToLower() == ActionObject["type"]?.ToLower();
                ActionObject["type"] = value.ToString().ToLower();
                if (!matched)
                {
                    OnAnyPropertyChanged?.Invoke();
                }
            }
        }

        internal ML2ActionEditorCompile(ML2BuildAction action) : base(action)
        {
            Debug.Assert(action.Action == ML2BuildActionType.Compile);
        }
    }

    public class ML2ActionEditorLight : ML2BuildActionEditor
    {
        [Category("Light Options")]
        [Description("Changes fidelity of build lighting. Higher quality takes more time.")]
        [DisplayName("Quality")]
        public ML2LightQuality Quality
        {
            get
            {
                return Enum.TryParse((ActionObject["quality"] ?? (ActionObject["quality"] = "medium")), true, out ML2LightQuality res) ? res : ML2LightQuality.Medium;
            }
            set
            {
                bool matched = value.ToString().ToLower() == ActionObject["quality"]?.ToLower();
                ActionObject["quality"] = value.ToString().ToLower();
                if (!matched)
                {
                    OnAnyPropertyChanged?.Invoke();
                }
            }
        }

        internal ML2ActionEditorLight(ML2BuildAction action) : base(action)
        {
            Debug.Assert(action.Action == ML2BuildActionType.Light);
        }
    }

    public class ML2ActionEditorCustom : ML2BuildActionEditor
    {
        [Category("Tool Options")]
        [Description("Command to send to the default shell")]
        [DisplayName("Command")]
        public string ExecutablePath
        {
            get
            {
                return ActionObject["program"] ?? (ActionObject["program"] = "");
            }
            set
            {
                bool matched = ActionObject["program"] == value;
                ActionObject["program"] = value;
                if (!matched)
                {
                    OnAnyPropertyChanged?.Invoke();
                }
            }
        }

        internal ML2ActionEditorCustom(ML2BuildAction action) : base(action)
        {
            Debug.Assert(action.Action == ML2BuildActionType.CustomTool);
        }
    }

    public enum ML2CompileType
    {
        Full,
        Ents
    }

    public enum ML2LightQuality
    {
        Low,
        Medium,
        High
    }

    public enum ML2BuildActionType
    {
        None,
        Unknown,
        CustomTool,
        Compile,
        Light,
        Link,
        CleanXPaks
    }

    class ActionTypeConverter : EnumConverter
    {
        private Type _enumType;
        /// <summary />Initializing instance</summary />
        /// <param name=""type"" />type Enum</param />
        /// this is only one function, that you must
        /// change. All another functions for enums
        /// you can use by Ctrl+C/Ctrl+V
        public ActionTypeConverter(Type type)
            : base(type)
        {
            _enumType = type;
        }

        public override bool CanConvertTo(ITypeDescriptorContext context,
            Type destType)
        {
            return destType == typeof(string);
        }

        public override object ConvertTo(ITypeDescriptorContext context,
            CultureInfo culture,
            object value, Type destType)
        {
            if(value == null)
            {
                return "(null)";
            }

            FieldInfo fi = _enumType.GetField(Enum.GetName(_enumType, value));
            DescriptionAttribute dna =
                (DescriptionAttribute)Attribute.GetCustomAttribute(
                fi, typeof(DescriptionAttribute));

            if (dna != null)
                return dna.Description;
            else
                return value.ToString();
        }

        public override bool CanConvertFrom(ITypeDescriptorContext context,
            Type srcType)
        {
            return srcType == typeof(string);
        }

        public override object ConvertFrom(ITypeDescriptorContext context,
            CultureInfo culture,
            object value)
        {
            foreach (FieldInfo fi in _enumType.GetFields())
            {
                DescriptionAttribute dna =
                (DescriptionAttribute)Attribute.GetCustomAttribute(
                fi, typeof(DescriptionAttribute));

                if ((dna != null) && ((string)value == dna.Description))
                    return Enum.Parse(_enumType, fi.Name);
            }
            return Enum.Parse(_enumType, (string)value);
        }
    }
}
