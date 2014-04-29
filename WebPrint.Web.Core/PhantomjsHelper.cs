namespace WebPrint.Web.Core
{
    public class PhantomjsHelper
    {
        public static void WebPageToImge(string startFile, string args, out string standardOutput, out string standardError)
        {
            ProcessHelper.Render(startFile, args, out standardOutput, out standardError);
        }

        public static void WebPageToPdf(string startFile, string args, out string standardOutput, out string standardError)
        {
            ProcessHelper.Render(startFile, args, out standardOutput, out standardError);
        }
    }
}
