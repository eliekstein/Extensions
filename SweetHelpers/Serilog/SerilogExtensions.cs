using Serilog;
using Serilog.Core;
using Serilog.Events;
using SweetHelpers.Exceptions;
using System;

namespace SweetHelpers.Serilog
{
    public static class SerilogExtensions
    {
        public static Exception LogWithSerilog(this Exception exception, LogEventLevel Level, params ILogEventEnricher[] enricher)
        {
            Log.Write(Level, exception, exception.GetBottomException().Message);
            return exception;
            //Log.Error(exception, exception.GetBottomException().Message);
        }
        public static Exception LogWithSerilog(this Exception exception)
        {
            return exception.LogWithSerilog(LogEventLevel.Error);
        }
        private static void LogWithSerilog(this Exception exception, bool throwError)
        {
            exception.LogWithSerilog();
            if (throwError)
                throw exception;
        }

        private static void LogWithSerilog(this Exception exception, ILogger log)
        {
            log.Error(exception, exception.GetBottomException().Message);
        }
    }
}
