using Serilog;
using Serilog.Core;
using Serilog.Events;
using System;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;

namespace Extensions
{
    public static class Extensions
    {
        public static bool IsNullOrEmpty(this string str)
        {
            return string.IsNullOrEmpty(str);
        }

        public static DateTime AddDays(this DateTime date, double value, params DayOfWeek[] exceptWeekdays)
        {
            if (exceptWeekdays.Count() > 0)
            {
                for (int i = 0; i < value; i++)
                {
                    if (exceptWeekdays.Any(d => d == date.AddDays((double)i).DayOfWeek))
                        value += 1;
                }
            }
            return date.AddDays(value);
        }

        public static string FormatWith(this string str, params object[] strs)
        {
            return string.Format(str, strs);
        }
        [Obsolete]
        public static string formatWith(this string str, params object[] strs)
        {
            return string.Format(str, strs);
        }

        public static bool Contains(this string source, string toCheck, StringComparison comp)
        {
            return source != null && toCheck != null && source.IndexOf(toCheck, comp) >= 0;
        }
        public static Exception GetBottomException(this Exception exception)
        {
            Exception ex = exception;
            while (ex.InnerException != null)
            {
                ex = ex.InnerException;
            }
            return ex;
        }

        public static string Join(this string[] strings,string separator)
        {
            return string.Join(separator, strings);
        }

        //public static Decimal TryParse(this decimal dec, string str)
        //{
        //    return 55;
        //}

        public static Exception LogWithSerilog(this Exception exception,LogEventLevel Level,params ILogEventEnricher[] enricher)
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


        public static void Fire(this PropertyChangedEventHandler notifier,
            object sender,
            [CallerMemberName] String propertyName = "") =>
            notifier?.Invoke(sender, new PropertyChangedEventArgs(propertyName));
    }
}
