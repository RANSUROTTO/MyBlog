using System;
using Blog.Libraries.Data.Domain.Logging;
using Blog.Libraries.Data.Domain.Member;

namespace Blog.Libraries.Services.Logging
{
    public static class LogServiceExtensions
    {

        public static void Debug(this ILogService logService,
            string message,
            Exception exception = null,
            Customer customer = null)
        {
            FilteredLog(logService, LogLevel.Debug, message, exception, customer);
        }

        public static void Information(this ILogService logService,
            string message,
            Exception exception = null,
            Customer customer = null)
        {
            FilteredLog(logService, LogLevel.Information, message, exception, customer);
        }

        public static void Warning(this ILogService logService,
            string message,
            Exception exception = null,
            Customer customer = null)
        {
            FilteredLog(logService, LogLevel.Warning, message, exception, customer);
        }

        public static void Error(this ILogService logService,
            string message,
            Exception exception = null,
            Customer customer = null)
        {
            FilteredLog(logService, LogLevel.Error, message, exception, customer);
        }

        public static void Fatal(this ILogService logService,
            string message,
            Exception exception = null,
            Customer customer = null)
        {
            FilteredLog(logService, LogLevel.Fatal, message, exception, customer);
        }

        private static void FilteredLog(ILogService logService,
            LogLevel level,
            string message,
            Exception exception = null,
            Customer customer = null)
        {
            //排除手动停止线程异常记录
            if (exception is System.Threading.ThreadAbortException)
                return;

            var fullMessage = exception?.ToString() ?? string.Empty;
            logService.InsertLog(level, message, fullMessage, customer);
        }

    }
}
