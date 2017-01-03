using Extensions.Settings;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Extensions.Data
{
    public class Context : DbContext
    {
        internal DbSet<UserApplicationSettings> userApplicationSettings { get; set; }
    }
}
