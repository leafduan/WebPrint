using System;

namespace WebPrint.Web.Core
{
    public class JsonHelper
    {
        public static string SerializeObject(Object value)
        {
            return Newtonsoft.Json.JsonConvert.SerializeObject(value);
        }
    }
}
