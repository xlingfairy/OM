using CNB.Common;
using OM.Api.Attributes;
using OM.Api.Models.Enums;
using OM.Api.Models.Events;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace OM.Api.Parser
{

    /// <summary>
    /// 
    /// </summary>
    internal class EventParser : IInputParser
    {

        /// <summary>
        /// 事件报告的 XML 缺少反序列化需要的 xsi:type="xxx", 导致不能通过基类进行反序列化。
        /// 这里将 type 加上，使反序列能正常进行
        /// </summary>
        /// <param name="xml"></param>
        /// <returns></returns>
        public IInput Parse(string xml)
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
