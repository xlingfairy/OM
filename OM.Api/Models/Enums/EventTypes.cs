using OM.Api.Attributes;
using OM.Api.Models.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace OM.Api.Models.Enums
{

    /// <summary>
    /// 
    /// </summary>
    public enum EventTypes
    {
        /// <summary>
        /// 系统启动
        /// </summary>
        [ForEvent(typeof(BootUp))]
        BOOTUP,

        /// <summary>
        /// 配置变化
        /// </summary>
        [ForEvent(typeof(ConfigChange))]
        CONFIG_CHANGE,

        /// <summary>
        /// 分机忙
        /// </summary>
        [ForEvent(typeof(Busy))]
        BUSY,

        /// <summary>
        /// 分机空闲
        /// </summary>
        [ForEvent(typeof(Idle))]
        IDLE,

        /// <summary>
        /// 分机上线
        /// </summary>
        [ForEvent(typeof(Online))]
        ONLINE,

        /// <summary>
        /// 分机离线
        /// </summary>
        [ForEvent(typeof(Offline))]
        OFFLINE,

        /// <summary>
        /// 振铃
        /// </summary>
        [ForEvent(typeof(Ring))]
        RING,

        /// <summary>
        /// 回铃
        /// </summary>
        [ForEvent(typeof(Alert))]
        ALERT,

        /// <summary>
        /// 呼叫应答
        /// </summary>
        [ForEvent(typeof(Answer))]
        ANSWER,

        /// <summary>
        /// 呼叫被应答
        /// </summary>
        [ForEvent(typeof(Answered))]
        ANSWERED,

        /// <summary>
        /// 通话结束
        /// </summary>
        [ForEvent(typeof(Bye))]
        BYE,

        /// <summary>
        /// 呼叫转移
        /// </summary>
        [ForEvent(typeof(Divert))]
        DIVERT,

        /// <summary>
        /// 呼叫临时事件
        /// </summary>
        [ForEvent(typeof(Transient))]
        TRANSIENT,

        /// <summary>
        /// 呼叫失败
        /// </summary>
        [ForEvent(typeof(Failed))]
        FAILED,

        /// <summary>
        /// 来电呼叫请求
        /// </summary>
        [ForEvent(typeof(Invite))]
        INVITE,

        /// <summary>
        /// 来电呼入
        /// </summary>
        [ForEvent(typeof(Incomming))]
        INCOMING,

        /// <summary>
        /// 按键信息事件
        /// </summary>
        [ForEvent(typeof(DTMF))]
        DTMF,

        /// <summary>
        /// 语音文件播放完毕
        /// </summary>
        [ForEvent(typeof(EndOfAnn))]
        EndOfAnn,

        /// <summary>
        /// 分机组队列事件
        /// </summary>
        [ForEvent(typeof(Queue))]
        QUEUE
    }
}
