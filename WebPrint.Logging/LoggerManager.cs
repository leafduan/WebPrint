using System;
using System.Collections.Concurrent;

namespace WebPrint.Logging
{
    public static class LoggerManager
    {
        // .NET 4.0并行库中thread-safe集合
        private static readonly ConcurrentDictionary<string, ILogger> loggerCache;

        static LoggerManager()
        {
            loggerCache = new ConcurrentDictionary<string, ILogger>();
        }

        public static ILogger GetLogger(string name)
        {
            return loggerCache.GetOrAdd(name, key => new Logger(name));
        }

        public static ILogger GetLogger(Type type)
        {
            return loggerCache.GetOrAdd(type.ToString(), key => new Logger(type));
        }
    }
}