using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO.Compression;
using System.IO;

namespace Zippy
{
    public static class Convert
    {
        public static void zipFolder(string startPath, string zipPath)
        {
            //zips folders
            ZipFile.CreateFromDirectory(startPath, zipPath);
        }
        public static void unzipFolder(string zipPath, string extractPath)
        {
            //unzips folders
            ZipFile.ExtractToDirectory(zipPath, extractPath);
        }
        public static byte[] convertToByte(string filePath)
        {
            //convert zip to byte[]
            return File.ReadAllBytes(filePath);
        }
        public static void convertByteTo(byte[] blob, string filePath)
        {
            //convert byte[] to zip
            File.WriteAllBytes(filePath, blob);
        }
    }
}
