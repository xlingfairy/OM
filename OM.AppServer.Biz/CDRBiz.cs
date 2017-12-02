using OM.AppServer.DB;
using OM.AppServer.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OM.AppServer.Biz
{
    public class CDRBiz
    {

        public async Task AddCDR(CDR item)
        {
            using (var db = new OMDbContext())
            {
                db.CDRs.Add(item);
                await db.SaveChangesAsync();
            }
        }

    }
}
