using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace ML2.Core
{
    internal static class Shared
    {
        internal const string VERSION = "2.0.0.0";
        internal static readonly string TA_GAME_PATH, TA_TOOLS_PATH, TA_LOCAL_ASSET_CACHE;
        static Shared()
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
        }
        internal class LowerNamePol : JsonNamingPolicy
        {
            public override string ConvertName(string name) =>
                name.ToLower();
        }

        public static JsonSerializerOptions SerializeOptions = new JsonSerializerOptions
        {
            PropertyNamingPolicy = new LowerNamePol(),
            PropertyNameCaseInsensitive = true,
            WriteIndented = true
        };

        internal static void Compress(byte[] data, string outfile)
        {
            MemoryStream ms = new MemoryStream(data);
            using (FileStream compressedFileStream = File.Create(outfile))
            {
                using (DeflateStream compressionStream = new DeflateStream(compressedFileStream, CompressionMode.Compress))
                {
                    ms.CopyTo(compressionStream);
                }
            }
            ms.Close();
        }

        internal static byte[] Decompress(byte[] data)
        {
            MemoryStream input = new MemoryStream();
            using (var compressStream = new MemoryStream(data))
            using (var compressor = new DeflateStream(compressStream, CompressionMode.Decompress))
            {
                compressor.CopyTo(input);
                compressor.Close();
                return input.ToArray();
            }
        }

        internal static class Console
        {
            internal delegate void ConsoleLogEvent(string message);
            internal static ConsoleLogEvent OnLogMessage;
            internal static void WriteLine(string message)
            {
                OnLogMessage?.Invoke(message + "^7\n");
            }
        }
    }
}
