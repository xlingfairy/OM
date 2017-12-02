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
    public class DebtInfo
    {

        public long ID { get; set; }

        /// <summary>
        /// 债务人，欠款人
        /// </summary>
        public string DebtorName { get; set; }

        /// <summary>
        /// 债务人，欠款人电话
        /// </summary>
        public string DebtorPhone { get; set; }

        /// <summary>
        /// 电话
        /// </summary>
        public string DebtorAddress { get; set; }

        /// <summary>
        /// 债权人
        /// </summary>
        public string Creditor { get; set; }


        /// <summary>
        /// 欠款金额
        /// </summary>
        public decimal Amount { get; set; }

        /// <summary>
        /// 欠款时间
        /// </summary>
        public DateTime DebitTime { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string ShortDesc { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Desc { get; set; }
    }
}
