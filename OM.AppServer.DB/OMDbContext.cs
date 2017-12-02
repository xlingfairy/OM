using OM.AppServer.Entity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OM.AppServer.DB
{
    public class OMDbContext : DbContext
    {

        public OMDbContext()
            : base() {

            this.Configuration.LazyLoadingEnabled = false;
            this.Configuration.ProxyCreationEnabled = false;

        }

        public DbSet<CDR> CDRs { get; set; }

    }
}
