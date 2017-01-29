using System;
using Serilog.Core;
using Serilog.Events;

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
            if (EventActionPredicate.Invoke(logEvent.Level))
            {
                EventAction(logEvent);
            }
        }
    }
}
