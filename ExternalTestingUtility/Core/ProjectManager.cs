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
        internal static ProjectChangedEvent OnProjectRemoved;

        private static readonly string TA_GAME_PATH, TA_TOOLS_PATH, TA_LOCAL_ASSET_CACHE;
        private static readonly Dictionary<string, ML2Project> DiscoveredProjects;

        static ProjectManager()
        {
#if DEBUG
            Environment.SetEnvironmentVariable("TA_GAME_PATH", @"C:\Program Files (x86)\Steam\steamapps\common\Call of Duty Black Ops III");
            Environment.SetEnvironmentVariable("TA_TOOLS_PATH", @"C:\Program Files (x86)\Steam\steamapps\common\Call of Duty Black Ops III");
            Environment.SetEnvironmentVariable("TA_LOCAL_ASSET_CACHE", @"C:\Program Files (x86)\Steam\steamapps\common\Call of Duty Black Ops III\share\assetconvert");
#endif
            TA_GAME_PATH = Environment.GetEnvironmentVariable("TA_GAME_PATH");
            TA_TOOLS_PATH = Environment.GetEnvironmentVariable("TA_TOOLS_PATH");
            TA_LOCAL_ASSET_CACHE = Environment.GetEnvironmentVariable("TA_LOCAL_ASSET_CACHE");

            if (TA_GAME_PATH is null)
            {
                throw new InvalidOperationException("TA_GAME_PATH not set. Please run this program through steam or configure TA_GAME_PATH in your system environment variables.");
            }

            if (TA_TOOLS_PATH is null)
            {
                throw new InvalidOperationException("TA_TOOLS_PATH not set. Please run this program through steam or configure TA_TOOLS_PATH in your system environment variables.");
            }

            if (TA_LOCAL_ASSET_CACHE is null)
            {
                throw new InvalidOperationException("TA_LOCAL_ASSET_CACHE not set. Please run this program through steam or configure TA_LOCAL_ASSET_CACHE in your system environment variables.");
            }

            DiscoveredProjects = new Dictionary<string, ML2Project>();

            DiscoverProjects();
        }

        public static void DiscoverProjects()
        {
            DiscoverProjectsInFolder(Path.Combine(TA_GAME_PATH, "usermaps"), false);
            DiscoverProjectsInFolder(Path.Combine(TA_GAME_PATH, "mods"), true);

            // TODO: clean stale projects that dont exist anymore, and verify that the zones of each file still match
        }

        private static void CheckProjectChanges(string projectFile)
        {
            // TODO: check for zone changes
        }

        private static void DiscoverProjectsInFolder(string path, bool isMod)
        {
            foreach(var folder in Directory.GetDirectories(path))
            {
                string zone_source = Path.Combine(folder, "zone_source");
                string project_file = Path.Combine(folder, ML2Project.PROJECT_FILE);

                if(DiscoveredProjects.ContainsKey(project_file))
                {
                    CheckProjectChanges(project_file);
                    continue;
                }

                if ((!Directory.Exists(zone_source) || Directory.GetFiles(zone_source, "*.zone").Length <= 0) && !File.Exists(project_file))
                {
                    continue;
                }

                var project = ML2Project.FromPath(folder, isMod);

                if(project is null)
                {
                    continue;
                }

                DiscoveredProjects[folder] = project;
                OnProjectAdded?.Invoke(project);
            }
        }
    }
}
