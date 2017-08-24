using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace OM.Api.Models.Enums
{
    public enum EventTypes
    {
        /// <summary>
        /// 系统启动
        /// </summary>
        BOOTUP,

        /// <summary>
        /// 配置变化
        /// </summary>
        CONFIG_CHANGE,

        /// <summary>
        /// 分机忙
        /// </summary>
        BUSY,

        /// <summary>
        /// 分机空闲
        /// </summary>
        IDLE,

        /// <summary>
        /// 分机上线
        /// </summary>
        ONLINE,

        /// <summary>
        /// 分机离线
        /// </summary>
        OFFLINE,

        /// <summary>
        /// 振铃
        /// </summary>
        RING,

        /// <summary>
        /// 回铃
        /// </summary>
        ALERT,

        /// <summary>
        /// 呼叫应答
        /// </summary>
        ANSWER,

        /// <summary>
        /// 呼叫被应答
        /// </summary>
        ANSWERED,

        /// <summary>
        /// 通话结束
        /// </summary>
        BYE,

        /// <summary>
        /// 呼叫转移
        /// </summary>
        DIVERT,

        /// <summary>
        /// 呼叫临时事件
        /// </summary>
        [XmlEnum("TRANSIENT")]
        呼叫临时事件,

        /// <summary>
        /// 呼叫失败
        /// </summary>
        FAILED,

        /// <summary>
        /// 来电呼叫请求
        /// </summary>
        INVITE,

        /// <summary>
        /// 来电呼入
        /// </summary>
        INCOMING,

        /// <summary>
        /// 按键信息事件
        /// </summary>
        DTMF,

        /// <summary>
        /// 语音文件播放完毕
        /// </summary>
        EndOfAnn,

        /// <summary>
        /// 分机组队列事件
        /// </summary>
        QUEUE
    }
}
