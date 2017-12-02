using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OM.AppServer.Entity
{
    /// <summary>
    /// 
    /// </summary>
    public class DebtNote
    {

        public long ID { get; set; }

        public long DebtID { get; set; }

        public string Msg { get; set; }

        public DateTime CreateOn { get; set; }

        public string CreateBy { get; set; }
    }
}
