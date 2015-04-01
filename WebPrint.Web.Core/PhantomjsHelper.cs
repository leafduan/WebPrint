using WebPrint.Framework;

namespace WebPrint.Web.Core
{
    public class PhantomjsHelper
    {
        // 默认 5 分钟
        private const int Timeout = 5*60*1000;

        public static void WebPageToImge(string startFile, string args, out string standardOutput,
            out string standardError)
        {
            ProcessHelper.Process(startFile, args, Timeout, out standardOutput, out standardError);
        }

        public static void WebPageToPdf(string startFile, string args, out string standardOutput,
            out string standardError)
        {
            ProcessHelper.Process(startFile, args, Timeout, out standardOutput, out standardError);
        }
    }
}
