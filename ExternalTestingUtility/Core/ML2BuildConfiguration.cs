using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace ML2.Core
{
    internal sealed class ML2BuildConfiguration
    {
        private ML2BuildConfigurationData Data;
        internal delegate void UpdatedConfigPropEvent(ML2BuildConfiguration e);
        internal UpdatedConfigPropEvent OnConfigNameUpdated;

        private List<ML2BuildAction> BuildActions;

        public int ActionsCount => BuildActions.Count;

        public string Name
        {
            get
            {
                return Data.Name;
            }
            set
            {
                Data.Name = value;
                OnConfigNameUpdated?.Invoke(this);
            }
        }


        public ML2BuildConfiguration(ML2BuildConfigurationData data)
        {
            Data = data;
            BuildActions = new List<ML2BuildAction>();
            foreach(var action in Data.BuildActionQueue)
            {
                BuildActions.Add(new ML2BuildAction(action));
            }
        }

        public override string ToString()
        {
            return Data.Name;
        }

        public IEnumerable<ML2BuildAction> GetActions()
        {
            foreach(var action in BuildActions)
            {
                yield return action;
            }
        }

        public ML2BuildAction GetAction(int index)
        {
            if(index < 0 || index >= BuildActions.Count)
            {
                throw new IndexOutOfRangeException($"Tried to get action index '{index}' which is out of range");
            }
            return BuildActions[index];
        }

        public void ReorderBuild(int from, int to)
        {
            if(from == to)
            {
                return;
            }
            if(from < 0 || to < 0 || from >= BuildActions.Count || to >= BuildActions.Count)
            {
                throw new IndexOutOfRangeException("Invalid from/to for build action reordering");
            }
            var actionToMove = BuildActions[from];
            BuildActions.RemoveAt(from);
            BuildActions.Insert(to, actionToMove);
            Data.ReorderBuild(from, to);
        }

        public void DeleteAction(int index)
        {
            if(index < 0 || index > BuildActions.Count)
            {
                throw new IndexOutOfRangeException("Invalid index for build action deletion");
            }
            Data.DeleteAction(index);
            BuildActions.RemoveAt(index);
        }

        public void Duplicate(int index)
        {
            if (index < 0 || index > BuildActions.Count)
            {
                throw new IndexOutOfRangeException("Invalid index for build action duplication");
            }
            var cloned = (JsonSerializer.Deserialize(JsonSerializer.Serialize(Data.BuildActionQueue[index], Shared.SerializeOptions), typeof(ML2BuildActionData), Shared.SerializeOptions) as ML2BuildActionData);
            Data.BuildActionQueue.Insert(index + 1, cloned);
            BuildActions.Insert(index + 1, new ML2BuildAction(cloned));
        }

        public int New()
        {
            var action = ML2BuildActionData.DefaultCompile();
            Data.BuildActionQueue.Add(action);
            BuildActions.Add(new ML2BuildAction(action));
            return BuildActions.Count - 1;
        }

        public ML2BuildConfigurationData CopyData()
        {
            return (ML2BuildConfigurationData)JsonSerializer.Deserialize(JsonSerializer.Serialize(Data, Shared.SerializeOptions), typeof(ML2BuildConfigurationData), Shared.SerializeOptions);
        }
    }

    public sealed class ML2BuildConfigurationData
    {
        public List<ML2BuildActionData> BuildActionQueue { get; set; }
        public string Name { get; set; }
        
        public ML2BuildConfigurationData()
        {
            BuildActionQueue = new List<ML2BuildActionData>();
            Name = "New Configuration";
        }

        internal static ML2BuildConfigurationData DebugDefault()
        {
            ML2BuildConfigurationData data = new ML2BuildConfigurationData();
            data.Name = "Debug";
            var compile_action = ML2BuildActionData.DefaultCompile();

            var light_action = ML2BuildActionData.DefaultLight();

            var link_action = ML2BuildActionData.DefaultLink();

            data.BuildActionQueue.Add(compile_action);
            data.BuildActionQueue.Add(light_action);
            data.BuildActionQueue.Add(link_action);

            return data;
        }

        internal void ReorderBuild(int from, int to)
        {
            if (from == to)
            {
                return;
            }
            if (from < 0 || to < 0 || from >= BuildActionQueue.Count || to >= BuildActionQueue.Count)
            {
                throw new IndexOutOfRangeException("Invalid from/to for build action queue reordering");
            }
            var actionToMove = BuildActionQueue[from];
            BuildActionQueue.RemoveAt(from);
            BuildActionQueue.Insert(to, actionToMove);
        }

        internal void DeleteAction(int index)
        {
            if (index < 0 || index > BuildActionQueue.Count)
            {
                throw new IndexOutOfRangeException("Invalid index for build action queue deletion");
            }
            BuildActionQueue.RemoveAt(index);
        }
    }
}
