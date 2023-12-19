using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ML2.Core
{
    internal static class ProjectManager
    {
        internal delegate void ProjectChangedEvent(ML2Project project);

        internal static ProjectChangedEvent OnProjectAdded;
        internal static ProjectChangedEvent OnProjectUpdated;
        internal static ProjectChangedEvent OnProjectRemoved;
        internal static ProjectChangedEvent OnActiveProjectChanged;

        private static ML2Project __activeproject;
        internal static ML2Project ActiveProject
        {
            get
            {
                return __activeproject;
            }
            set
            {
                __activeproject = value;
                OnActiveProjectChanged.Invoke(value);
            }
        }

        private static readonly Dictionary<string, ML2Project> DiscoveredProjects;

        static ProjectManager()
        {
            DiscoveredProjects = new Dictionary<string, ML2Project>();

            DiscoverProjects();
        }

        public static void DiscoverProjects()
        {
            DiscoverProjectsInFolder(Path.Combine(Shared.TA_GAME_PATH, "usermaps"), false);
            DiscoverProjectsInFolder(Path.Combine(Shared.TA_GAME_PATH, "mods"), true);

            // cleanup projects that dont exist on disk anymore
            List<string> cleanup = new List<string>();
            foreach(var project in DiscoveredProjects)
            {
                if(project.Value.Exists())
                {
                    continue;
                }
                cleanup.Add(project.Key);
            }

            foreach(string clean in cleanup)
            {
                OnProjectRemoved?.Invoke(DiscoveredProjects[clean]);
                DiscoveredProjects.Remove(clean);
            }
        }

        private static void DiscoverProjectsInFolder(string path, bool isMod)
        {
            foreach(var folder in Directory.GetDirectories(path))
            {
                string zone_source = Path.Combine(folder, "zone_source");
                string project_file = Path.Combine(folder, ML2Project.PROJECT_FILE);

                // update existing projects
                if(DiscoveredProjects.ContainsKey(folder))
                {
                    if(DiscoveredProjects[folder].UpdateZoneInfo())
                    {
                        OnProjectUpdated?.Invoke(DiscoveredProjects[folder]);
                        try
                        {
                            DiscoveredProjects[folder].SaveToDisk();
                        }
                        catch
                        {
                            Logger.LogWarning($"Project '{folder}' was unable to save to disk. This may be a permissions issue or another application may be accessing this file. (update save failed)");
                        }
                    }
                    continue;
                }

                if ((!Directory.Exists(zone_source) || Directory.GetFiles(zone_source, "*.zone").Length <= 0) && !File.Exists(project_file))
                {
                    continue;
                }

                // add a new project if possible
                var project = ML2Project.FromPath(folder, isMod);

                if(project is null)
                {
                    continue;
                }

                DiscoveredProjects[folder] = project;
                OnProjectAdded?.Invoke(project);
            }
        }

        public static IEnumerable<ML2Project> GetProjects()
        {
            foreach(var p in DiscoveredProjects)
            {
                yield return p.Value;
            }
        }
    }
}
