using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO.Compression;
using System.IO;
using System.Drawing;

namespace Quantum
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
        public static byte[] convertZipToByte(string filePath)
        {
            //convert zip to byte[]
            return File.ReadAllBytes(filePath);
        }
        public static void convertByteToZip(byte[] blob, string filePath)
        {
            //convert byte[] to zip
            File.WriteAllBytes(filePath, blob);
        }

        public static byte[] convertImageToBlob(Image img)
        {
            using (var ms = new MemoryStream())
            {
                img.Save(ms, img.RawFormat);
                return ms.ToArray();
            }
        }
        public static Image convertBlobToImage(byte[] byteImg)
        {
            //converts blob to image using memorystream
            MemoryStream memstream = new MemoryStream(byteImg);
            return Image.FromStream(memstream);
        }
    }
}
