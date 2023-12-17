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
    }
}
