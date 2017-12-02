using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OM.AppServer.Entity
{
    public class CDR
    {

        /// <summary>
        /// 话单id
        /// </summary>
        [Key]
        [StringLength(50), Required(AllowEmptyStrings = false)]
        public string ID { get; set; }

        /// <summary>
        /// 通话的相对唯一标识符
        /// </summary>
        public int CallID { get; set; }


        /// <summary>
        /// 
        /// </summary>
        public int VisitorID { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int OuterID { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public byte Type { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public byte Route { get; set; }

        /// <summary>
        /// 呼叫起始时间，即发送或收到呼叫请求的时间
        /// </summary>
        public DateTime? TimeStar { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DateTime? TimeEnd { get; set; }

        /// <summary>
        /// 主叫号码
        /// </summary>
        [StringLength(20)]
        public string From { get; set; }

        /// <summary>
        /// 被叫号码
        /// </summary>
        [StringLength(20)]
        public string To { get; set; }

        /// <summary>
        /// 通话时长，值为0说明未接通。
        /// </summary>
        public int Duration { get; set; }

        /// <summary>
        /// 该路通话所经过的中继号码
        /// </summary>
        [StringLength(20)]
        public string TrunkNumber { get; set; }

        /// <summary>
        /// 录音文件的相对保存路径
        /// </summary>
        [StringLength(500)]
        public string Recording { get; set; }

        /// <summary>
        /// 编码方式，决定录音文件格式，值为：G729、G711(PCMA、PCMU)。 
        /// </summary>
        [StringLength(10)]
        public string RecCodec { get; set; }

        /// <summary>
        /// 记录时间
        /// </summary>
        public DateTime RecoredOn { get; set; }
    }
}
