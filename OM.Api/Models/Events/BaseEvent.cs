using OM.Api.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Linq;
using System.IO;
using CNB.Common;
using OM.Api.Attributes;

namespace OM.Api.Models.Events
{

    /// <summary>
    /// 
    /// </summary>
    [XmlInclude(typeof(Alert))]
    [XmlInclude(typeof(Answer))]
    [XmlInclude(typeof(Answered))]
    [XmlInclude(typeof(BootUp))]
    [XmlInclude(typeof(Busy))]
    [XmlInclude(typeof(Bye))]
    [XmlInclude(typeof(ConfigChange))]
    [XmlInclude(typeof(Divert))]
    [XmlInclude(typeof(Failed))]
    [XmlInclude(typeof(Idle))]
    [XmlInclude(typeof(Offline))]
    [XmlInclude(typeof(Online))]
    [XmlInclude(typeof(Ring))]
    [XmlInclude(typeof(Transient))]
    [XmlInclude(typeof(Invite))]
    [XmlInclude(typeof(Incomming))]
    [XmlInclude(typeof(DTMF))]
    [XmlInclude(typeof(EndOfAnn))]
    [XmlInclude(typeof(Queue))]
    [XmlRoot("Event")]
    public abstract class BaseEvent
    {

        /// <summary>
        /// 
        /// </summary>
        [XmlAttribute("attribute")]
        public abstract EventTypes Attribute { get; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="xml"></param>
        public static void Fix(string xml)
        {
            var x = XElement.Parse(xml);
            var attr = x.Attribute("attribute").Value;
            x.Add(XNamespace.Get("http://www.w3.org/2001/XMLSchema-instance"));
        }

        /// <summary>
        /// 事件报告的 XML 缺少反序列化需要的 xsi:type="xxx", 导致不能通过基类进行反序列化。
        /// 这里将 type 加上，使反序列能正常进行
        /// </summary>
        /// <param name="xml"></param>
        /// <returns></returns>
        public static BaseEvent Parse(string xml)
        {
            if (string.IsNullOrWhiteSpace(xml))
                throw new ArgumentNullException("xml");

            var ele = XElement.Parse(xml);

            var name = ele.Name;
            var attr = ele.Attribute("attribute")?.Value;

            var t = attr.ToEnum<EventTypes>();
            var forType = EnumHelper.GetAttribute<EventTypes, ForEventAttribute>(t);

            var ns = XNamespace.Get("http://www.w3.org/2001/XMLSchema-instance");
            //DTMF 事件用命名法转换，不合适
            //ele.Add(new XAttribute(ns + "type", attr.ToPascalCase()));
            ele.Add(new XAttribute(ns + "type", forType.EventType.Name));

            xml = ele.ToString();

            var bytes = Encoding.UTF8.GetBytes(xml);
            var ser = new XmlSerializer(typeof(BaseEvent));
            using (var msm = new MemoryStream(bytes))
            {
                return (BaseEvent)ser.Deserialize(msm);
            }
        }
    }
}
