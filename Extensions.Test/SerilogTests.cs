using Extensions.Serilog;
using Serilog;
using Serilog.Events;
using System;
using Xunit;
using Xunit.Abstractions;

namespace Extensions.Test
{
    public class SerilogTests : IDisposable
    {
        private readonly ITestOutputHelper output;

        public SerilogTests(ITestOutputHelper output)
        {
            this.output = output;
        }

        public void Dispose()
        {
            Log.CloseAndFlush();
        }

        [Fact]
        public void ShouldExecuteAction()
        {
            LogEventLevel Level = 0;

            Log.Logger = new LoggerConfiguration()
                .WriteTo.ActionSink(l => 
                {
                    output.WriteLine(l.MessageTemplate.Text);
                    Level = l.Level;
                })
                .MinimumLevel.Verbose()
                .CreateLogger();

            //check value still as init
            Assert.Equal(Level , LogEventLevel.Verbose);


            output.WriteLine("testing output");
            //log something
            Log.Warning("hello");

            Assert.Equal(Level, LogEventLevel.Warning);
        }
        [Fact]
        public void ShouldCheckPredicate()
        {
            LogEventLevel Level = 0;

            Log.Logger = new LoggerConfiguration()
                .WriteTo.ActionSink(l =>
                {
                    output.WriteLine(l.MessageTemplate.Text);
                    Level = l.Level;
                },l => l.Level >= LogEventLevel.Warning)
                .MinimumLevel.Verbose()
                .CreateLogger();

            //check value still as init
            Assert.Equal(Level, LogEventLevel.Verbose);

            //log something
            Log.Verbose("hello verbose");
            //still default 
            Assert.Equal(Level, LogEventLevel.Verbose);
            //log again
            Log.Warning("hello verbose");
            //should be warning
            Assert.Equal(Level, LogEventLevel.Warning);
        }
    }
}
