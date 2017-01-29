using System;
using Serilog.Core;
using Serilog.Events;
using Serilog;
using Serilog.Configuration;

namespace Extensions.Serilog
{
    public class ActionSink : ILogEventSink
    {
        private Action<LogEvent> EventAction;
        private Predicate<LogEventLevel> EventActionPredicate;

        private ActionSink()
        {

        }
        public ActionSink(Action<LogEvent> EventAction) : this()
        {
            this.EventAction = EventAction;
        }
        public ActionSink(Action<LogEvent> EventAction, Predicate<LogEventLevel> EventActionPredicate) : this(EventAction)
        {
            this.EventActionPredicate = EventActionPredicate;
        }
        public void Emit(LogEvent logEvent)
        {
            if (EventActionPredicate == null || EventActionPredicate.Invoke(logEvent.Level))
            {
                EventAction(logEvent);
            }
        }
    }

    public static class ActionSinkExtensions
    {
        public static LoggerConfiguration ActionSink(
                  this LoggerSinkConfiguration loggerConfiguration,
                  Action<LogEvent> EventAction)
        {
            return loggerConfiguration.Sink(new ActionSink(EventAction));
        }
    }
}
