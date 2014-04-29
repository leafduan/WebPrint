namespace WebPrint.Web.Core
{
    public class PythonHelper
    {
        public static void PiPdfExport(string startFile, string args, out string standardOutput, out string standardError)
        {
            ProcessHelper.Render(startFile, args, out standardOutput, out standardError);
        }
    }
}
