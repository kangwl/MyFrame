using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using Demo.Common.web;
using Supremes.Nodes;

namespace Demo.Common
{
    public class KuaiDiAPI
    {
        /*
            success	返回状态：true 成功 ，false 失败
            status	运单的当前状态：0物流单号暂无结果，3在途，4 揽件，5 疑难，6签收，7退签，8 派件，9 退回
            reason	如果请求失败，失败原因
            data	数据集合
            time	每条数据的时间
            context	每条数据的内容 
        */
        /// <summary>
        /// 返回字符串
        /// </summary>
        /// <param name="expressCode">快递公司代码</param>
        /// <param name="trackingNo">快递单号</param>
        /// <returns></returns>
        public static string Search(string expressCode, string trackingNo)
        {
            try
            {
                string searchUrl =
                    string.Format(
                        "http://api.kuaidi.com/openapi.html?id=208b2f83c750959fbfb81351c704f7eb&com={0}&nu={1}&show=0&muti=0&order=desc",
                        expressCode, trackingNo);

                HttpWebHelper httpWeb = new HttpWebHelper(searchUrl);
                string resStr = httpWeb.GetResponseStr();

                return resStr;
            }
            catch (Exception)
            {
                return "";
            }
        }

        /// <summary>
        /// 返回快递信息、时间
        /// 列表
        /// </summary>
        /// <param name="trackingNo">快递单号</param>
        /// <returns></returns>
        public static List<ExpressInfo> SearchExpressInfoList(string trackingNo)
        {
            List<ExpressInfo> list = new List<ExpressInfo>();
            Elements elements = GetExpressInfoEles(trackingNo);
            if (elements == null) return new List<ExpressInfo>();
            foreach (Element element in elements)
            {
                ExpressInfo expressInfo = new ExpressInfo();
                var time_s = element.GetElementsByClass("time_s");
                if (time_s.HasText)
                {
                    expressInfo.Time = time_s.Text;
                }
                var kd_con = element.GetElementsByClass("kd_con");
                if (kd_con.HasText)
                {
                    expressInfo.Info = kd_con.Text;
                }
                list.Add(expressInfo);
            }
            return list;
        }

        /// <summary>
        /// 返回ul html
        /// </summary>
        /// <param name="trackingNo"></param>
        /// <returns></returns>
        public static string SearchDirect(string trackingNo)
        {
            string strContent = "";

            Elements elements = GetExpressInfoEles(trackingNo);
            if (elements == null)
            {
                return strContent;
            }
            Element notUse = elements.First.GetElementsByClass("color_blue").First;
            notUse.Remove();
            strContent = elements.OuterHtml;

            return strContent;
        }

        /// <summary>
        /// 返回对象
        /// </summary>
        /// <param name="expressCode">快递公司代码</param>
        /// <param name="trackingNo">快递单号</param>
        /// <returns></returns>
        public static Rootobject SearchObject(string expressCode, string trackingNo)
        {
            Rootobject rootobject = new Rootobject();
            try
            {
                string searchUrl =
                    string.Format(
                        "http://api.kuaidi.com/openapi.html?id=208b2f83c750959fbfb81351c704f7eb&com={0}&nu={1}&show=0&muti=0&order=desc",
                        expressCode, trackingNo);

                HttpWebHelper httpWeb = new HttpWebHelper(searchUrl);
                string resStr = httpWeb.GetResponseStr();
                rootobject = JsonHelper<Rootobject>.DeserializeFromStr(resStr);
            }
            catch (Exception ex)
            {
                rootobject.success = false;
            }
            return rootobject;
        }

        /// <summary>
        /// 分析网页查询
        /// </summary>
        /// <param name="trackingNo"></param>
        /// <returns></returns>
        private static Elements GetExpressInfoEles(string trackingNo)
        {
            string strContent = "";
            string reqUrl = "http://m.kuaidi.com/queryresults.html";
            Dictionary<string, string> dic = new Dictionary<string, string>();
            dic.Add("ordernumber", trackingNo);
            HttpWebResponse res = HttpHelper.CreatePostHttpResponse(reqUrl, dic, null, null, Encoding.UTF8, null);
            var stream = res.GetResponseStream();
            if (stream != null)
            {
                using (stream)
                {
                    using (var streamReader = new StreamReader(stream))
                    {
                        strContent = streamReader.ReadToEnd();
                    }
                }
            }

            if (strContent.Length > 0)
            {
                Document doc = Supremes.Dcsoup.Parse(strContent);
                Elements elements = doc.GetElementById("main_section").GetElementsByClass("resutl_box");
                Element element = elements.First;
                Element ul = element.GetElementsByTag("ul").First;
                Element notUse = ul.GetElementsByClass("color_blue").First;
                notUse.Remove();
                return ul.Children;
            }
            return null;
        }
        public class ExpressInfo
        {
            public ExpressInfo()
            {
                Time = "";
                Info = "";
            }
            public string Time { get; set; }
            public string Info { get; set; }
        }

        public class Rootobject
        {
            public bool success { get; set; }
            public string reason { get; set; }
            public Datum[] data { get; set; }
            public int status { get; set; }
        }

        public class Datum
        {
            public string time { get; set; }
            public string context { get; set; }
        }
    }
}
