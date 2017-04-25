using Serilog;
using Serilog.Core;
using Serilog.Events;
using System;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;

namespace SweetHelpers.Extensions
{
    public static class Extensions
    {
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

        public static bool IsNullOrEmpty(this string str)
        {
            return string.IsNullOrEmpty(str);
        }
        
        public static string FormatWith(this string str, params object[] strs)
        {
            return string.Format(str, strs);
        }

        public static bool Contains(this string source, string toCheck, StringComparison comp)
        {
            return source != null && toCheck != null && source.IndexOf(toCheck, comp) >= 0;
        }
        public static string Join(this string[] strings, string separator)
        {
            return string.Join(separator, strings);
        }

    }
}

namespace Extensions
{
    public static class Extensions
    {
        const string newNameSpace = nameof(SweetHelpers.Extensions);

        [Obsolete("Update your SweetHelpers namespace from Extensions to SweetHelpers.Extensions")]
        public static bool IsNullOrEmpty(this string str)
        {
            return string.IsNullOrEmpty(str);
        }

        [Obsolete("Update your SweetHelpers namespace from Extensions to SweetHelpers.Extensions")]
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
        [Obsolete("Update your SweetHelpers namespace from Extensions to SweetHelpers.Extensions")]
        public static string FormatWith(this string str, params object[] strs)
        {
            return string.Format(str, strs);
        }
        [Obsolete("Update your SweetHelpers namespace from Extensions to SweetHelpers.Extensions")]
        public static string formatWith(this string str, params object[] strs)
        {
            return string.Format(str, strs);
        }
        [Obsolete("Update your SweetHelpers namespace from Extensions to SweetHelpers.Extensions")]
        public static bool Contains(this string source, string toCheck, StringComparison comp)
        {
            return source != null && toCheck != null && source.IndexOf(toCheck, comp) >= 0;
        }
        [Obsolete("Update your SweetHelpers namespace from Extensions to SweetHelpers.Exceptions")]
        public static Exception GetBottomException(this Exception exception)
        {
            Exception ex = exception;
            while (ex.InnerException != null)
            {
                ex = ex.InnerException;
            }
            return ex;
        }
        [Obsolete("Update your SweetHelpers namespace from Extensions to SweetHelpers.Extensions")]

        public static string Join(this string[] strings,string separator)
        {
            return string.Join(separator, strings);
        }

        //public static Decimal TryParse(this decimal dec, string str)
        //{
        //    return 55;
        //}
        [Obsolete("Update your SweetHelpers namespace from Extensions to SweetHelpers.Serilog")]

        public static Exception LogWithSerilog(this Exception exception,LogEventLevel Level,params ILogEventEnricher[] enricher)
        {
            Log.Write(Level, exception, exception.GetBottomException().Message);
            return exception;
            //Log.Error(exception, exception.GetBottomException().Message);
        }
        [Obsolete("Update your SweetHelpers namespace from Extensions to SweetHelpers.Serilog")]
        public static Exception LogWithSerilog(this Exception exception)
        {
            return exception.LogWithSerilog(LogEventLevel.Error);
        }
        [Obsolete("Update your SweetHelpers namespace from Extensions to SweetHelpers.Serilog")]
        private static void LogWithSerilog(this Exception exception, bool throwError)
        {
            exception.LogWithSerilog();
            if (throwError)
                throw exception;
        }
        [Obsolete("Update your SweetHelpers namespace from Extensions to SweetHelpers.Serilog")]
        private static void LogWithSerilog(this Exception exception, ILogger log)
        {
            log.Error(exception, exception.GetBottomException().Message);
        }

        [Obsolete("Update your SweetHelpers namespace from Extensions to SweetHelpers.DataBinding")]
        public static void Fire(this PropertyChangedEventHandler notifier,
            object sender,
            [CallerMemberName] String propertyName = "") =>
            notifier?.Invoke(sender, new PropertyChangedEventArgs(propertyName));
        [Obsolete("Update your SweetHelpers namespace from Extensions to SweetHelpers.DataBinding")]
        public static void Fire<TEventArgs>(this EventHandler eventobj,object sender,TEventArgs args) where TEventArgs : EventArgs
        {
            eventobj?.Invoke(sender, args);
        }
        [Obsolete("Update your SweetHelpers namespace from Extensions to SweetHelpers.DataBinding")]
        public static void Fire(this EventHandler eventobj, object sender)
        {
            eventobj?.Invoke(sender,EventArgs.Empty);
        }
    }
}