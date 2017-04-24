using Serilog.Core;
using Serilog.Events;
using System;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;

class EventTypeEnricher : ILogEventEnricher
{
    public void Enrich(LogEvent logEvent, ILogEventPropertyFactory propertyFactory)
    {
        var st = propertyFactory.CreateProperty("StackTrace", Environment.StackTrace);

        logEvent.AddOrUpdateProperty(st);
    }


    [MethodImpl(MethodImplOptions.NoInlining)]
    public string GetCurrentMethod()
    {
        var st = new StackTrace();
        var sf = st.GetFrame(1);
        

        var param = sf.GetMethod().GetParameters()
            .Select(p => new { type = p.ParameterType.Name, name = p.Name });
        //.ToDictionary((a,b) => )
        return sf.GetMethod().Name;
    }
}