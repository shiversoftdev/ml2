using System;
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


        public const string PROJECT_FILE = "project.dat";
        public ML2ProjectConfig Data;
        private string PathOnDisk;
        private ML2Project() // prevent creation of the class from anywhere except inside the class
        {
            Data = new ML2ProjectConfig();
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
                    Logger.LogOnce(Logger.LogWarning, $"Project '{folder}' cannot be parsed because it contains an invalid project configuration. Send the '{PROJECT_FILE}' to the developer of this application by joining the discord server in Help->Discord for a possible recovery of your project data.");
                    return null;
                }
            }
            else
            {
                project.Data = new ML2ProjectConfig();
            }

            // will always update since it shouldnt change
            project.Data.InternalName = project_internal_name;
            project.Data.FriendlyName = project.Data.FriendlyName ?? project.Data.InternalName;
            project.Data.SimpleIsMod = isMod;
            project.PathOnDisk = project_file;

            // TODO: update information stored in the data in case things changed since we parsed it

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
        }
    }

    public sealed class ML2ProjectConfig
    { 
        public string InternalName { get; set; }
        public string FriendlyName { get; set; }
        public bool SimpleIsMod { get; set; }
        public ML2ProjectConfig()
        {

        }
    }
}
