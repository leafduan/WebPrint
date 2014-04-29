using System.IO;

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
    }
}
