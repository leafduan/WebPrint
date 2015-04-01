using WebPrint.Framework;

namespace WebPrint.Web.Core
{
    public class PythonHelper
    {
        public static void PiPdfExport(string startFile, string args, out string standardOutput,
            out string standardError)
        {
            ProcessHelper.Process(startFile, args, 5*60*1000, out standardOutput, out standardError);
        }
    }
}
