﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace OM.Api.Models
{

    /// <summary>
    /// 设备项信息
    /// </summary>
    public abstract class DeviceItem
    {

        /// <summary>
        /// 中继的线路编号/分机的线路编号
        /// </summary>
        [XmlAttribute("lineid")]
        public string LineID { get; set; }

        /// <summary>
        /// 中继号/分机号
        /// </summary>
        [XmlAttribute("id")]
        public string ID { get; set; }
    }

    /// <summary>
    /// 设备信息：分机
    /// </summary>
    public class ExtDeviceItem : DeviceItem
    {
    }

    /// <summary>
    /// 设备信息：中继（外线）
    /// </summary>
    public class LineDeviceItem : DeviceItem
    {
    }
}