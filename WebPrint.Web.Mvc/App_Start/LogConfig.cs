using System.Configuration;
using System.Web;
using WebPrint.Logging;

namespace WebPrint.Web.Mvc
{
    public sealed class LogConfig
    {
        public static void Register()
        {
            var server = HttpContext.Current.Server;
            var config = server.MapPath(ConfigurationManager.AppSettings["log4net.config"]);
            Configurator.Configure(config);
        }
    }
}