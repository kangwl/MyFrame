using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks; 
using Demo.Common.ExtensionMethods;
using Demo.Common.web;
using Supremes;
using Supremes.Nodes;

namespace MyFilms.Core
{
    public class FilmAnalyze
    {
        //http://www.iqiyi.com/dianying/vip.html
        public static string BaseUrl = "http://api.aikantv.cc/?url=";
        //http://jiexi888.duapp.com/?url=http://www.iqiyi.com/v_19rr9igq6k.html
        public static string GetVipFilms()
        {
            string url = "http://www.iqiyi.com/dianying/vip.html";
            string html = HttpHelper.GetPage(url, Encoding.UTF8);
            Document document = Dcsoup.ParseBodyFragment(html);
            Elements elements = document.GetElementsByClass("wrapper-cols");

            List<FilmModel> filmModels = new List<FilmModel>();
            foreach (Element element in elements)
            {
                Elements linkEles = element.GetElementsByClass("site-piclist_pic_link");
                foreach (Element linkEle in linkEles)
                {
                    var href = linkEle.Attr("href");
                    var title = linkEle.Attr("title");
                    Element imgEle = linkEle.GetElementsByTag("img")[0];
                    var imgSrc = imgEle.Attr("src");

                    bool exist = filmModels.Any(one => one.ImgSrc.Trim() == imgSrc.Trim());
                    if (!exist)
                    {
                        filmModels.Add(new FilmModel() {Href = BaseUrl + href, Title = title, ImgSrc = imgSrc});
                    }
                }
            }
            return CreateHtml(filmModels);
        }

        public static void GetQQFilms()
        {
            string url = "http://film.qq.com/vip/";
            string html = HttpHelper.GetPage(url, Encoding.UTF8);
            Document document = Dcsoup.ParseBodyFragment(html);
        }

        public static string CreateHtml(List<FilmModel> filmModels)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("<html>");
            sb.Append("<head>");
            sb.Append("<meta http-equiv=\"Content-Type\" content=\"text/html; charset=utf-8\"/>");
            sb.Append(@"<style>
                        ul li{float:left;margin:10px;list-style:none}
                        ul li div{margin-top:5px;font-size:15px}
                        </style>");
            sb.Append("</head>");
            sb.Append("<body>");
            sb.Append("<ul>");
            filmModels.ForEach(one =>
            {
                string li =
                    $"<li><a href={one.Href} title={one.Title}><img src={one.ImgSrc}/></a><div>{one.Title}</div></li>";
                sb.Append(li);
            });
        
            sb.Append("</ul>");
            sb.Append("</body>");

            string page = sb.ToString();
            byte[] bytes = page.ToByteArray();
            string file = AppDomain.CurrentDomain.BaseDirectory + "film.html";
            FileStream fileStream = File.Create(file);
            using (fileStream)
            {
                fileStream.Write(bytes, 0, bytes.Length);
                fileStream.Flush();
            }
            return file;
        }
        public class FilmModel
        {
            public string Href { get; set; }
            public string Title { get; set; }
            public string ImgSrc { get; set; }
        }
    }
}
