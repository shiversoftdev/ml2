﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;

namespace ML2.Core
{
    internal class ML2Project
    {
        // NOTE: We will design this class to be stored as a zip encoded json because we do not want people to directly mess with the json.
        internal delegate void ProjectNameChangedEvent(ML2Project project, string oldVal, string newVal);
        internal delegate void ProjectZonesChangedEvent(ML2Project project, string[] removedZones, string[] addedZones);
        internal delegate void ProjectActiveConfigEvent(ML2Project project, int oldIndex, int newIndex, ML2BuildConfiguration config);
        internal delegate void ProjectConfigChangedEvent(ML2BuildConfiguration config);

        public const string PROJECT_FILE = "project.dat";
        private ML2ProjectConfig Data;
        private string PathOnDisk;
        private string PathToZone;
        public string ProjectDirectory { private set; get; }

        public ProjectNameChangedEvent OnNameUpdated;
        public ProjectZonesChangedEvent OnZonesUpdated;
        public ProjectActiveConfigEvent OnActiveConfigChanged;
        public ProjectConfigChangedEvent OnBuildConfigDeleted;
        public ProjectConfigChangedEvent OnBuildConfigAdded;

        private HashSet<string> __zoneslist;
        private List<ML2BuildConfiguration> BuildConfigurations;

        internal ML2BuildConfiguration ActiveConfig
        {
            get
            {
                // BUG: changing this without firing respective event may cause issues
                if (BuildConfigurations.Count < 1)
                {
                    throw new IndexOutOfRangeException("Cannot get active config of project without any build configurations!");
                }

                if (Data.ActiveBuildConfig < 0 || Data.ActiveBuildConfig >= BuildConfigurations.Count)
                {
                    Data.ActiveBuildConfig = 0;
                }

                return BuildConfigurations[Data.ActiveBuildConfig];
            }
        }

        internal int ActiveConfigIndex
        {
            get
            {
                return Data.ActiveBuildConfig;
            }
            set
            {
                if(value < 0 || value >= BuildConfigurations.Count)
                {
                    throw new IndexOutOfRangeException("Tried to change build config index, but the target index is out of range.");
                }
                int oldVal = Data.ActiveBuildConfig;
                Data.ActiveBuildConfig = value;
                OnActiveConfigChanged?.Invoke(this, oldVal, value, BuildConfigurations[value]);
            }
        }

        internal int ConfigCount => BuildConfigurations.Count;

        /// <summary>
        /// Public accessor for friendly name
        /// </summary>
        internal string FriendlyName
        {
            get
            {
                return Data?.FriendlyName ?? "Empty Project";
            }
            set
            {
                if(Data is null)
                {
                    throw new NullReferenceException("Tried to set friendly name of a non-existent project");
                }
                string target = (value == "") ? Data.InternalName : value;
                OnNameUpdated?.Invoke(this, Data.FriendlyName, target);
                Data.FriendlyName = target;
                SaveToDisk();
            }
        }
        internal bool IsMod => Data?.SimpleIsMod ?? false;

        private ML2Project() // prevent creation of the class from anywhere except inside the class
        {
            Data = new ML2ProjectConfig();
            __zoneslist = new HashSet<string>();
            BuildConfigurations = new List<ML2BuildConfiguration>();
        }


        /// <summary>
        /// Creates a ML2 project from a path. If any errors are encountered, the project is returned null and an error is logged.
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static ML2Project FromPath(string folder, bool isMod)
        {
            string zone_source = Path.Combine(folder, "zone_source");
            string project_file = Path.Combine(folder, PROJECT_FILE);
            string project_internal_name = $"{(isMod ? "mods/" : "maps/")}{folder.Substring(folder.LastIndexOf(Path.DirectorySeparatorChar) + 1)}";

            string[] zones = Directory.Exists(zone_source) ? Directory.GetFiles(zone_source, "*.zone") : new string[0];

            if((!Directory.Exists(zone_source) || zones.Length <= 0) && !File.Exists(project_file))
            {
                Logger.LogOnce(Logger.LogWarning, $"Project '{folder}' cannot be parsed because it does not contain any valid project data.");
                return null;
            }

            ML2Project project = new ML2Project();
            // if the file exists, lets parse info about the project
            if (File.Exists(project_file))
            {
                try
                {
                    Shared.Decompress(File.ReadAllBytes(project_file));
                    object result = JsonSerializer.Deserialize(Shared.Decompress(File.ReadAllBytes(project_file)), typeof(ML2ProjectConfig), Shared.SerializeOptions);
                    if(result == null)
                    {
                        throw new Exception();
                    }

                    project.Data = (ML2ProjectConfig)result;
                }
                catch
                {
                    Logger.LogOnce(Logger.LogWarning, $"Project '{folder}' cannot be parsed because it contains an invalid project configuration. Send '{PROJECT_FILE}' to the developer of this application by joining the discord server in Help->Discord for a possible recovery of your project data.");
                    return null;
                }
            }
            else
            {
                project.Default();
            }

            // will always update since it shouldnt change
            project.Data.InternalName = project_internal_name;
            project.Data.FriendlyName = project.Data.FriendlyName ?? project.Data.InternalName;
            project.Data.SimpleIsMod = isMod;
            project.PathOnDisk = project_file;
            project.PathToZone = zone_source;
            project.ProjectDirectory = folder;

            foreach(var conf in project.Data.BuildConfigurations)
            {
                project.BuildConfigurations.Add(new ML2BuildConfiguration(conf));
            }

            if(project.BuildConfigurations.Count < 1)
            {
                var debug_default = ML2BuildConfigurationData.DebugDefault();
                project.Data.BuildConfigurations.Add(debug_default);
                project.BuildConfigurations.Add(new ML2BuildConfiguration(debug_default));
            }

            if(project.Data.ActiveBuildConfig < 0 || project.Data.ActiveBuildConfig >= project.BuildConfigurations.Count)
            {
                project.Data.ActiveBuildConfig = 0;
            }
            

            // pull zone info from disk
            project.UpdateZoneInfo();

            try
            {
                project.SaveToDisk();
            }
            catch
            {
                Logger.LogOnce(Logger.LogWarning, $"Project '{folder}' was unable to save to disk. This may be a permissions issue or another application may be accessing this file.");
                return null;
            }

            return project;
        }

        public void SaveToDisk()
        {
            Shared.Compress(Encoding.ASCII.GetBytes(JsonSerializer.Serialize(Data, Shared.SerializeOptions)), PathOnDisk);
            // File.WriteAllText(PathOnDisk + ".debug", JsonSerializer.Serialize(Data, Shared.SerializeOptions));
        }

        private void Default()
        {
            Data = new ML2ProjectConfig();
            // TODO: setup default configs (including default build configurations)
        }

        /// <summary>
        /// Pull zone list and other things from disk. Returns true if changes are made.
        /// </summary>
        /// <returns></returns>
        public void UpdateZoneInfo()
        {
            if(!Directory.Exists(PathToZone))
            {
                if(__zoneslist.Count > 0)
                {
                    OnZonesUpdated?.Invoke(this, __zoneslist.ToArray(), new string[0]);
                    __zoneslist.Clear();
                }
                return;
            }

            List<string> added = new List<string>();
            List<string> removed = new List<string>();
            HashSet<string> found = new HashSet<string>();
            foreach(var file in Directory.GetFiles(PathToZone, "*.zone"))
            {
                var zone = Path.GetFileNameWithoutExtension(file).ToLower();
                found.Add(zone);

                if (__zoneslist.Contains(zone))
                {
                    continue;
                }

                added.Add(zone);
            }

            foreach(var file in __zoneslist)
            {
                if(found.Contains(file))
                {
                    continue;
                }
                removed.Add(file);
            }

            __zoneslist = found;

            if(added.Count + removed.Count > 0)
            {
                OnZonesUpdated?.Invoke(this, removed.ToArray(), added.ToArray());
            }
        }

        public IEnumerable<string> GetZones()
        {
            foreach (var zone in __zoneslist)
            {
                yield return zone;
            }
        }

        /// <summary>
        /// Check if this project exists on disk
        /// </summary>
        /// <returns></returns>
        public bool Exists()
        {
            return Directory.Exists(ProjectDirectory);
        }

        internal IEnumerable<ML2BuildConfiguration> GetBuildConfigurations()
        {
            foreach(var config in BuildConfigurations)
            {
                yield return config;
            }
        }

        internal ML2BuildConfiguration GetConfiguration(int index)
        {
            if(index < 0 || index >= BuildConfigurations.Count)
            {
                throw new IndexOutOfRangeException($"Build configuration '{index}' is out of range");
            }
            return BuildConfigurations[index];
        }

        internal bool AnyConfigNamed(string name)
        {
            foreach(var config in BuildConfigurations)
            {
                if(config.Name == name)
                {
                    return true;
                }
            }
            return false;
        }

        internal void AddDebugConfig()
        {
            var debug_default = ML2BuildConfigurationData.DebugDefault();
            Data.BuildConfigurations.Add(debug_default);
            var conf = new ML2BuildConfiguration(debug_default);

            int i = 0;
            string name = conf.Name;
            while(AnyConfigNamed(name))
            {
                i++;
                name = $"{conf.Name} ({i})";
            }

            conf.Name = name;

            BuildConfigurations.Add(conf);
            OnBuildConfigAdded?.Invoke(conf);
        }

        internal void DeleteConfiguration(int index)
        {
            if(BuildConfigurations.Count < 2)
            {
                throw new InvalidOperationException("Cannot delete a configuration because the project only has 1 configuration available");
            }

            // to keep things the simplest, we need to select a new index, change it, delete the old thing, and refresh all UIs
            
            // cache for later
            var conf = BuildConfigurations[index];

            int newIndex = 0;
            if(index == newIndex)
            {
                newIndex++;
            }

            ActiveConfigIndex = newIndex; // unbind all active events

            // if the deleted config is below the new active config, active config now points to the wrong index
            if(newIndex > index)
            {
                Data.ActiveBuildConfig--;
            }

            Data.BuildConfigurations.RemoveAt(index);
            BuildConfigurations.RemoveAt(index);
            OnBuildConfigDeleted?.Invoke(conf);
        }

        internal void CloneConfig(int index)
        {
            if(index < 0 || index >= BuildConfigurations.Count)
            {
                throw new InvalidOperationException($"Cannot clone index '{index}' because it is out of range.");
            }
            var config = BuildConfigurations[index].CopyData();
            var conf = new ML2BuildConfiguration(config);

            int i = 0;
            string name = conf.Name;
            while (AnyConfigNamed(name))
            {
                i++;
                name = $"{conf.Name} ({i})";
            }

            conf.Name = name;

            BuildConfigurations.Add(conf);
            Data.BuildConfigurations.Add(config);
            OnBuildConfigAdded?.Invoke(conf);
        }

        public int FindConfigIndex(ML2BuildConfiguration conf)
        {
            for(int i = 0; i < BuildConfigurations.Count; i++)
            {
                if(BuildConfigurations[i] == conf)
                {
                    return i;
                }
            }
            return -1;
        }
    }

    public sealed class ML2ProjectConfig
    { 
        public string InternalName { get; set; }
        public string FriendlyName { get; set; }
        public bool SimpleIsMod { get; set; }
        public int ActiveBuildConfig { get; set; }
        public List<ML2BuildConfigurationData> BuildConfigurations { get; set; }
        public ML2ProjectConfig()
        {
            BuildConfigurations = new List<ML2BuildConfigurationData>();
            ActiveBuildConfig = -1;
        }
    }
}
