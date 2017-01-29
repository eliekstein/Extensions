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
        private Predicate<LogEvent> EventActionPredicate;

        private ActionSink()
        {

        }
        public ActionSink(Action<LogEvent> EventAction) : this()
        {
            this.EventAction = EventAction;
        }
        public ActionSink(Action<LogEvent> EventAction, Predicate<LogEvent> EventActionPredicate) : this(EventAction)
        {
            this.EventActionPredicate = EventActionPredicate;
        }
        public void Emit(LogEvent logEvent)
        {
            if (EventActionPredicate == null || EventActionPredicate.Invoke(logEvent))
            {
                EventAction(logEvent);
            }
        }
    }

    public static class ActionSinkExtensions
    {
        public static LoggerConfiguration ActionSink(
                  this LoggerSinkConfiguration loggerConfiguration,
                  Action<LogEvent> EventAction,
                  Predicate<LogEvent> EventActionPredicate = null)
        {
            if (EventActionPredicate == null)
                return loggerConfiguration.Sink(new ActionSink(EventAction));
            else
                return loggerConfiguration.Sink(new ActionSink(EventAction, EventActionPredicate));
        }

    }
}
