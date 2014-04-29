using System.IO;

namespace WebPrint.Framework
{
    public static class DirectoryHelper
    {
        public static bool Exists(string path)
        {
            return Directory.Exists(path);
        }

        public static void CreateDirectory(string path)
        {
            Directory.CreateDirectory(path);
        }
    }
}
