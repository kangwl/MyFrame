using System;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MyFilms
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            CheckForIllegalCrossThreadCalls = false;
            webBrowser1.DocumentCompleted += WebBrowser1_DocumentCompleted;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //webBrowser1.DocumentText = "加载中...";
            Task.Factory.StartNew(() =>
            {
                //string url = FilmAnalyze.GetVipFilms();
              
              
            }); 
        }

        private void WebBrowser1_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            var links = webBrowser1.Document.Links;
            foreach (HtmlElement v in links)
            {
                string href = v.GetAttribute("href");
                if (href.IndexOf("http://www.iqiyi.com/v") != -1 || href.IndexOf("http://vip.iqiyi.com/") != -1 ||
                    href.IndexOf("http://v.qq.com/cover") != -1)
                {
                    v.SetAttribute("href", "http://api.aikantv.cc/?url=" + href);
                }
            }
        }


        private void btn_qq_Click(object sender, EventArgs e)
        {
            webBrowser1.Navigate("http://film.qq.com/vip/");
        }

        private void btn_qy_Click(object sender, EventArgs e)
        {
            webBrowser1.Navigate("http://www.iqiyi.com/dianying/vip.html");
        }
    }
}
