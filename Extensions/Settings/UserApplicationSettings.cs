using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Extensions.Settings
{
    internal class UserApplicationSettings
    {
        public int id { get; set; }
        public Guid applicationGuid { get; set; }
        public string userName { get; set; }
        public string applicationSettings { get; set; }
        public DateTime dateUpdated { get; set; }

        public UserApplicationSettings()
        {
            userName = Environment.UserName;
            dateUpdated = DateTime.Now;
        }
    }
}
