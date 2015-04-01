using System.IO;
using System.Xml;
using System.Xml.Linq;
using log4net.Config;

namespace WebPrint.Logging
{
    public static class LoggerConfigurator
    {
        /// <summary>
        /// base on the application configuration settings
        /// </summary>
        public static void Configure()
        {
            XmlConfigurator.Configure();
        }

        /// <summary>
        /// using special log4net configure file
        /// </summary>
        public static void Configure(string configFileName)
        {
            XmlConfigurator.ConfigureAndWatch(new FileInfo(configFileName));
        }
		
		public static void Configure(XElement element)
        {
            var doc = new XmlDocument();
            doc.Load(element.CreateReader());
            XmlConfigurator.Configure(doc.DocumentElement);
        }
    }
}
