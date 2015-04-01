using System;
using System.IO;
using System.Linq;

namespace WebPrint.Framework
{
    public static class FileHelper
    {
        public static bool Exists(string fileFullPath)
        {
            return File.Exists(fileFullPath);
        }

        public static void DeleteFile(string fileFullPath)
        {
            if (File.Exists(fileFullPath)) File.Delete(fileFullPath);
        }
		
		public static string MapPath(string path)
        {
            return Path.Combine(AppDomain.CurrentDomain.BaseDirectory, path);
        }

        public static string MapPathExceptBin(string path)
        {
            var appPath = AppDomain
                .CurrentDomain
                .BaseDirectory
                .Split('\\')
                .Where(s => !s.EqualTo("bin", true) && !s.EqualTo("debug", true) && !s.EqualTo("release", true));

            return Path.Combine(string.Join(@"\", appPath), path);
        }
    }
}
