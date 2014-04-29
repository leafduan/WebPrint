using System;
using System.Web;
using System.Configuration;
using WebPrint.BarTender;
using WebPrint.Framework;

namespace WebPrint.Web.Core
{
    public sealed class BarTenderHelper
    {
        /// <summary>
        /// Application_Start 时候调用,保证整个应用程序只有一个TaskManager对象
        /// </summary>
        public static void Start()
        {
            try
            {
                BarTenderBootStrapper.Start();
            }
            catch (Exception)
            {
                BarTenderBootStrapper.Stop();
                throw;
            }
        }

        /// <summary>
        /// Shut down all BarTender print engines and the task manager.
        /// </summary>
        public static void Stop()
        {
            BarTenderBootStrapper.Stop();
        }

        /// <summary>
        /// absolute path that preview image generated in print srceen saved in
        /// </summary>
        public static string PreviewFileFullPath
        {
            get
            {
                var path = HttpContext.Current.Server.MapPath(@"\FormatPreview\Image");
                if (!DirectoryHelper.Exists(path))
                    DirectoryHelper.CreateDirectory(path);

                return path;
            }
        }

        /// <summary>
        /// absolute path that print file(*.prn) generated in print srceen saved in
        /// </summary>
        public static string PrintFileFullPath
        {
            get
            {
                var path = HttpContext.Current.Server.MapPath(@"\FormatPreview\PrintFile");
                if (!DirectoryHelper.Exists(path))
                    DirectoryHelper.CreateDirectory(path);

                return path;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public static int TaskPrintTimeout
        {
            get { return ConfigurationManager.AppSettings["TaskPrintTimeout"].AsInt(); }
        }

        /// <summary>
        /// in milliseconds
        /// </summary>
        public static int TaskWaitForCompletionTimeout
        {
            get { return ConfigurationManager.AppSettings["TaskWaitForCompletionTimeout"].AsInt(99000); }
        }

        /// <summary>
        /// delete all the preview file that created when user preview it
        /// Delete all print preview images starting with Session.SessionID.
        /// </summary>
        /// <param name="sessionId"></param>
        public static void ClearPreviewFile(string sessionId)
        {
            try
            {
                var sFileNames = System.IO.Directory.GetFiles(PreviewFileFullPath, sessionId + ".*.png",
                                                              System.IO.SearchOption.TopDirectoryOnly);

                foreach (var fileName in sFileNames)
                {
                    try
                    {
                        System.IO.File.Delete(fileName);
                    }
                    catch (Exception)
                    {
                        // Ignore if not found.
                    }
                }
            }
            catch (Exception)
            {
            }
        }
    }
}
