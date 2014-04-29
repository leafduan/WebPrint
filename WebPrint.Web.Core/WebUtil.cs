using System.Web;

namespace WebPrint.Web.Core
{
    public static class WebUtil
    {
        public static string RelativePath(string absolutePath)
        {
            return absolutePath.Replace(HttpContext.Current.Request.ServerVariables["APPL_PHYSICAL_PATH"], "/").Replace(@"\", "/");
        }

        public static string GetClientIp(HttpRequest request = null)
        {
            if (request == null) request = HttpContext.Current.Request;

            var result = request.ServerVariables["HTTP_X_FORWARDED_FOR"];
            if (string.IsNullOrEmpty(result))
            {
                result = request.ServerVariables["REMOTE_ADDR"];
            }
            if (string.IsNullOrEmpty(result))
            {
                //  result=Request.ServerVariables.Get("Remote_Addr").ToString();
                result = request.UserHostAddress;
            }
            return result;
        }
    }
}
