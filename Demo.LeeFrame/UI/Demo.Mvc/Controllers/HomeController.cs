using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Demo.Common.DB;
using Demo.Common.IOC;
using Demo.Model;
using Exceptionless;
using Demo.Repository.IRepository;
using System.Net.Http;

namespace Demo.Mvc.Controllers
{
    public class HomeController : Controller
    {
        //public IUserServ<User> UserServ { get; set; }
        public IUserRepository UserRepository { get; set; }
        // GET: Home
        public ActionResult Index()
        {
         
            try
            {
                // int.Parse("sdd");
                //var user = UserServ.GetOne(Guid.Parse("ff2e2a7d-e94a-44a6-ae99-82605b4393cf"));
                //Model.User user = new User()
                //{
                //    ID = Guid.NewGuid(),
                //    Name = "kwl",
                //    Age = 13,
                //    QQ = "767474055",
                //    CreateTime = DateTime.Now,
                //    UpdateTime = DateTime.Now
                //};
                
                //bool ok = UserRepository.Insert(user);

                //Model.User user = new User();
                //List<WhereItem> whereItems = new List<WhereItem>()
                //{

                //};
                //var users = UserRepository.GetPaged(1, 10, new List<string>(),
                //    new OrderByItem(nameof(user.CreateTime), false),
                //    whereItems, user);




            }
            catch (System.Exception ex)
            {

                
               // ex.ToExceptionless().AddTags("HomeController-Index").Submit();
            }
          
            return View();
        }


        #region 断点续传
        [HttpGet]
        public HttpResponseMessage GetResumFile()
        {
            //用于获取当前文件是否是续传。和续传的字节数开始点。
            var md5str = Request.QueryString["md5str"];
            var saveFilePath = Server.MapPath("~/Images/") + md5str;
            if (System.IO.File.Exists(saveFilePath))
            {
                var fs = System.IO.File.OpenWrite(saveFilePath);
                var fslength = fs.Length.ToString();
                fs.Close();
                return new HttpResponseMessage { Content = new StringContent(fslength, System.Text.Encoding.UTF8, "text/plain") };
            }
            return new HttpResponseMessage(HttpStatusCode.OK);
        }
        [HttpPost]
        public HttpResponseMessage Rsume()
        {

            var file = Request.InputStream;
            var filename = Request.QueryString["filename"];

            this.SaveAs(Server.MapPath("~/Images/") + filename, file);


            Response.StatusCode = 200;

            // For compatibility with IE's "done" event we need to return a result as well as setting the context.response
            return new HttpResponseMessage(HttpStatusCode.OK);
        }


        private void SaveAs(string saveFilePath, System.IO.Stream stream)
        {
            long lStartPos = 0;
            int startPosition = 0;
            int endPosition = 0;
            var contentRange = Request.Headers["Content-Range"];
            //bytes 10000-19999/1157632
            if (!string.IsNullOrEmpty(contentRange))
            {
                contentRange = contentRange.Replace("bytes", "").Trim();
                contentRange = contentRange.Substring(0, contentRange.IndexOf("/"));
                string[] ranges = contentRange.Split('-');
                startPosition = int.Parse(ranges[0]);
                endPosition = int.Parse(ranges[1]);
            }
            System.IO.FileStream fs;
            if (System.IO.File.Exists(saveFilePath))
            {
                fs = System.IO.File.OpenWrite(saveFilePath);
                lStartPos = fs.Length;

            }
            else
            {
                fs = new System.IO.FileStream(saveFilePath, System.IO.FileMode.Create);
                lStartPos = 0;
            }
            if (lStartPos > endPosition)
            {
                fs.Close();
                return;
            }
            else if (lStartPos < startPosition)
            {
                lStartPos = startPosition;
            }
            else if (lStartPos > startPosition && lStartPos < endPosition)
            {
                lStartPos = startPosition;
            }
            fs.Seek(lStartPos, System.IO.SeekOrigin.Current);
            byte[] nbytes = new byte[512];
            int nReadSize = 0;
            nReadSize = stream.Read(nbytes, 0, 512);
            while (nReadSize > 0)
            {
                fs.Write(nbytes, 0, nReadSize);
                nReadSize = stream.Read(nbytes, 0, 512);
            }
            fs.Close();
        } 
        #endregion
    }
}