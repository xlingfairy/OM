﻿using OM.Moq.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace OM.Moq.AppServer.Controllers
{

    [RoutePrefix("api/Debts")]
    public class DebtsController : BaseController
    {

        /// <summary>
        /// 
        /// </summary>
        /// <param name="cond"></param>
        /// <returns></returns>
        [HttpPost, Route("Search")]
        public ListResult<DebtInfo> Post(DebtCondition cond)
        {
            if (string.IsNullOrWhiteSpace(cond?.Name) && string.IsNullOrWhiteSpace(cond?.Phone))
            {
                var datas = Enumerable.Range((cond?.Page * cond?.PageSize) ?? 0, cond?.PageSize ?? 20)
                    .Select(i => new DebtInfo()
                    {
                        Creditor = "虚拟银行",
                        DebtorAddress = $"汉东市罗海区人民路{i}号{i}室",
                        DebtorName = $"张{i + 1}",
                        DebtorPhone = (15986627851 + i).ToString(),
                        ShortDesc = "xxx",
                        Amount = 20000 + i,
                        DebitTime = DateTime.Now.AddYears(-3).AddDays(-i),
                        Desc = @"南湖晚报9月8日消息，麻雀，曾是一些人舌尖上的美味佳肴。殊不知，麻雀虽小，随意捕捉也犯法。9月5日，一男子因捕捉44只麻雀，被桐乡市公安局依法采取取保候审强制措施。
不久前的一天，王某经过桐乡市屠甸镇荣星村时，见一处地里成群的麻雀飞来飞去。王某停下脚步，想捉几只来尝尝鲜，但两手空空，无法捕捉这些大自然的“小精灵”。于是，次日上午，他特意去买来竹竿、尼龙网等捕鸟工具，再次来到这里。看着这些叽叽喳喳、嬉戏觅食的麻雀，王某笑逐颜开，张网以待。不一会儿，众多麻雀先后“中招”落入网中。本想着晚上可以大餐一顿，但还没等王某清点“战果”，就被巡逻至此的屠甸派出所民警发现并抓获。经现场清点，民警缴获了被王某网住的44只麻雀和捕鸟工具。
后经国家林业局森林公安司法鉴定中心鉴定，这44只鸟类均属麻雀。根据《中华人民共和国野生动物保护法》第二十四条和《最高人民法院关于审理破坏野生动物资源刑事案件具体应用法律若干问题的解释》（法释（2000）37号）第六条之规定，王某的行为已涉嫌刑事犯罪。9月5日，桐乡市公安局依法对王某采取取保候审强制措施。

办案民警提醒，麻雀是雀属的鸟类，是国家“三有”保护动物。任何捕杀、出售、食用麻雀的行为，都是违法的。我国刑法规定，私自捕捉20只以上野生麻雀，即构成犯罪，50只以上属于重大刑事案件，100只以上属于特大刑事案件。如果看见他人捕杀麻雀，群众应当及时向公安机关报案。",
                        ID = i + 1
                    }).Where(i => i.ID <= 187);

                return BaseResult.CreateListResult(datas, total: 187);
            }
            else
            {
                var datas = new List<DebtInfo>() {
                    new DebtInfo()
                    {
                        Creditor = "虚拟银行",
                        DebtorAddress = $"汉东市罗海区人民路1号1室",
                        DebtorName = cond?.Name ?? "张三",
                        DebtorPhone = cond?.Phone ?? "123456789",
                        ShortDesc = "xxx",
                        Amount = 20000,
                        DebitTime = DateTime.Now.AddYears(-3),
                        Desc = @"南湖晚报9月8日消息，麻雀，曾是一些人舌尖上的美味佳肴。殊不知，麻雀虽小，随意捕捉也犯法。9月5日，一男子因捕捉44只麻雀，被桐乡市公安局依法采取取保候审强制措施。
不久前的一天，王某经过桐乡市屠甸镇荣星村时，见一处地里成群的麻雀飞来飞去。王某停下脚步，想捉几只来尝尝鲜，但两手空空，无法捕捉这些大自然的“小精灵”。于是，次日上午，他特意去买来竹竿、尼龙网等捕鸟工具，再次来到这里。看着这些叽叽喳喳、嬉戏觅食的麻雀，王某笑逐颜开，张网以待。不一会儿，众多麻雀先后“中招”落入网中。本想着晚上可以大餐一顿，但还没等王某清点“战果”，就被巡逻至此的屠甸派出所民警发现并抓获。经现场清点，民警缴获了被王某网住的44只麻雀和捕鸟工具。
后经国家林业局森林公安司法鉴定中心鉴定，这44只鸟类均属麻雀。根据《中华人民共和国野生动物保护法》第二十四条和《最高人民法院关于审理破坏野生动物资源刑事案件具体应用法律若干问题的解释》（法释（2000）37号）第六条之规定，王某的行为已涉嫌刑事犯罪。9月5日，桐乡市公安局依法对王某采取取保候审强制措施。

办案民警提醒，麻雀是雀属的鸟类，是国家“三有”保护动物。任何捕杀、出售、食用麻雀的行为，都是违法的。我国刑法规定，私自捕捉20只以上野生麻雀，即构成犯罪，50只以上属于重大刑事案件，100只以上属于特大刑事案件。如果看见他人捕杀麻雀，群众应当及时向公安机关报案。",
                        ID = 1000000
                    }
                };

                return BaseResult.CreateListResult(datas, total: 1);
            }
        }



        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet, Route("Notes")]
        public ListResult<DebtNote> GetDebtNotes(long id)
        {
            var datas = Enumerable.Range(0, 5)
                .Select(i => new DebtNote
                {
                    ID = i,
                    CreateOn = DateTime.Now.AddDays(-i),
                    CreateBy = "6678",
                    DebtID = id,
                    Msg = "这人不好说话"
                });

            return BaseResult.CreateListResult(datas);
        }
    }
}
