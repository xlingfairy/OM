using AsNum.FluentXml;
using OM.Api.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OM.Api.Methods.Controls.Assign
{
    /// <summary>
    /// 配置语音菜单
    /// </summary>
    public class EditMenu : BaseMethod<MenuInfo>
    {

        /// <summary>
        /// 
        /// </summary>
        public override ActionCategories ActionCategory => ActionCategories.Control;

        /// <summary>
        /// 语音菜单的编号	有效值1~50
        /// </summary>
        [Range(1, 50)]
        public int ID { get; set; }

        /// <summary>
        /// 语音文件，当有呼叫被转接到该菜单时播放该语音文件。
        /// </summary>
        public string VoiceFile { get; set; }

        /// <summary>
        /// 语音文件的播放次数	有效值0~65535，0为循环播放，默认值为0
        /// </summary>
        [Range(0, 65535)]
        public int? RepeatCount { get; set; }


        /// <summary>
        /// 按键检查长度，当被转接到该菜单内的通话方输入按键的长度达到该长度时，OM会将已检测到的按键信息推送给应用服务器。	
        /// 合法值为1~255（一字节长度）默认值为1
        /// </summary>
        [Range(1, 255)]
        public int? InfoLength { get; set; }

        /// <summary>
        /// 按键检查结束符，当被转接到该菜单内的通话方按键输入该符号后，OM会立刻将已检测到的按键信息推送给应用服务器。	
        /// 合法值：1~9、A~D、*、#，默认为空
        /// </summary>
        [RegularExpression("[1-9A-D*#]")]
        public char? ExitChar { get; set; }

        internal override object GetRequestData(ApiClientOption opt)
        {
            return new
            {
                attribute = "Assign".AsAttribute(),
                menu = new
                {
                    id = this.ID.AsAttribute(),
                    voicefile = this.VoiceFile,
                    repeat = this.RepeatCount,
                    infolength = this.InfoLength,
                    exit = this.ExitChar
                }
            };
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="result"></param>
        /// <returns></returns>
        protected override string Fix(string result)
        {
            return result.Replace("<Status>", "").Replace("</Status>", "");
        }
    }
}
