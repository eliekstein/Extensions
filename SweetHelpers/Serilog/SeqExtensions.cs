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
    [Obsolete("Update your SweetHelpers namespace from Serilog.Extensions to SweetHelpers.Serilog.Seq")]

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

      
    }


}

namespace SweetHelpers.Serilog.Seq
{
    public static class SeqExtensions
    {
        public static void Error(this ILogger logger, string messageTemplate, bool showError, params object[] propertyValues)
        {
            if (showError)
            {
                var msg = string.Format(messageTemplate, propertyValues);

                //MessageBox.Show();

            }
            logger.Error(messageTemplate, propertyValues);

        }


    }


}

