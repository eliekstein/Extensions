using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Serilog;
using Serilog.Core;
using Serilog.Events;
using System.Windows.Forms;

namespace Serilog.Extensions
{
    public static class SeqExtensions
    {
        public static void Error(this ILogger logger, string messageTemplate,bool showError, params object[] propertyValues)
        {
            if (showError)
            {
                var msg = string.Format(messageTemplate, propertyValues);
                
                //MessageBox.Show();

            }
                logger.Error(messageTemplate, propertyValues);

        }

        public static void WriteLog(string level,string message,params string[] propertyValues)
        {
            var log = new LoggerConfiguration()
                .WriteTo.Seq("http://srv3:5341", apiKey: "zw6Xu5aY4fFXuQbFBnC")
                .CreateLogger();

            var logLevel = LogEventLevel.Verbose;
            Enum.TryParse(level, true,out logLevel);

            log.Write(logLevel, message, propertyValues);

        }
    }


}
