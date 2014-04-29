using System;

namespace WebPrint.Logging
{
    public static class LoggerExtensions
    {
        public static void Debug(this ILogger logger, string message)
        {
            FilterLog(logger, LogLevel.Debug, message, null, null);
        }

        public static void Debug(this ILogger logger, string message, Exception exception)
        {
            FilterLog(logger, LogLevel.Debug, message, exception, null);
        }

        public static void Debug(this ILogger logger, string format, params object[] args)
        {
            FilterLog(logger, LogLevel.Debug, format, null, args);
        }

        public static void Error(this ILogger logger, string message)
        {
            FilterLog(logger, LogLevel.Error, message, null, null);
        }

        public static void Error(this ILogger logger, string message, Exception exception)
        {
            FilterLog(logger, LogLevel.Error, message, exception, null);
        }

        public static void Error(this ILogger logger, string format, params object[] args)
        {
            FilterLog(logger, LogLevel.Error, format, null, args);
        }

        public static void Fatal(this ILogger logger, string message)
        {
            FilterLog(logger, LogLevel.Fatal, message, null, null);
        }

        public static void Fatal(this ILogger logger, string message, Exception exception)
        {
            FilterLog(logger, LogLevel.Fatal, message, exception, null);
        }

        public static void Fatal(this ILogger logger, string format, params object[] args)
        {
            FilterLog(logger, LogLevel.Fatal, format, null, args);
        }

        public static void Info(this ILogger logger, string message)
        {
            FilterLog(logger, LogLevel.Info, message, null, null);
        }

        public static void Info(this ILogger logger, string message, Exception exception)
        {
            FilterLog(logger, LogLevel.Info, message, exception, null);
        }

        public static void Info(this ILogger logger, string format, params object[] args)
        {
            FilterLog(logger, LogLevel.Info, format, null, args);
        }

        public static void Warn(this ILogger logger, string message)
        {
            FilterLog(logger, LogLevel.Warn, message, null, null);
        }

        public static void Warn(this ILogger logger, string message, Exception exception)
        {
            FilterLog(logger, LogLevel.Warn, message, exception, null);
        }

        public static void Warn(this ILogger logger, string format, params object[] args)
        {
            FilterLog(logger, LogLevel.Warn, format, null, args);
        }

        private static void FilterLog(ILogger logger, LogLevel level, string format, Exception exception,
                                      object[] objects)
        {
            if (logger.IsEnabled(level))
            {
                logger.Log(level, format, exception, objects);
            }
        }
    }
}
