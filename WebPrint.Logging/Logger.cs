using System;
using log4net;

namespace WebPrint.Logging
{
    public class Logger : ILogger
    {
        private readonly ILog log;

        public Logger(string name)
        {
            log = LogManager.GetLogger(name);
        }

        public Logger(Type type)
        {
            // ILog最终实现类log4net.Core.LogImpl 
            // 统一调用方法 Log(Type callerStackBoundaryDeclaringType, Level level, object message, Exception exception);
            // format和args先格式化成message (SystemStringFormat)
            log = LogManager.GetLogger(type);
        }

        public bool IsEnabled(LogLevel level)
        {
            switch (level)
            {
                case LogLevel.Debug:
                    return log.IsDebugEnabled;
                case LogLevel.Info:
                    return log.IsInfoEnabled;
                case LogLevel.Warn:
                    return log.IsWarnEnabled;
                case LogLevel.Error:
                    return log.IsErrorEnabled;
                case LogLevel.Fatal:
                    return log.IsFatalEnabled;
            }

            return false;
        }

        public void Log(LogLevel level, string format, Exception exception, params object[] args)
        {
            if (args == null)
            {
                switch (level)
                {
                    case LogLevel.Debug:
                        log.Debug(format, exception);
                        break;
                    case LogLevel.Info:
                        log.Info(format, exception);
                        break;
                    case LogLevel.Warn:
                        log.Warn(format, exception);
                        break;
                    case LogLevel.Error:
                        log.Error(format, exception);
                        break;
                    case LogLevel.Fatal:
                        log.Fatal(format, exception);
                        break;
                }
            }
            else
            {
                switch (level)
                {
                    case LogLevel.Debug:
                        log.DebugFormat(format, args);
                        break;
                    case LogLevel.Info:
                        log.InfoFormat(format, args);
                        break;
                    case LogLevel.Warn:
                        log.WarnFormat(format, args);
                        break;
                    case LogLevel.Error:
                        log.ErrorFormat(format, args);
                        break;
                    case LogLevel.Fatal:
                        log.FatalFormat(format, args);
                        break;
                }
            }
        }
    }
}
