using System.Web;

namespace WebPrint.Web.Core
{
    public class HttpRequestState
    {
        public static object Get(object key)
        {
            HttpContext context = HttpContext.Current;

            return context.Items[key];
        }

        public static void Store(object key, object value)
        {
            HttpContext context = HttpContext.Current;

            context.Items[key] = value;
        }

        public static void Remove(object key)
        {
            HttpContext context = HttpContext.Current;

            context.Items.Remove(key);
        }
    }
}
