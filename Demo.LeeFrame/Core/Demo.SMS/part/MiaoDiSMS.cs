using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Demo.Common;
using Demo.Common.web;

namespace Demo.SMS.part
{
    public class MiaoDiSMS : ISMS
    {
        public MiaoDiSMS(string mobileNO, string verifyCode)
        {
            MobileNO = mobileNO;
            VerifyCode = verifyCode;
        }
        public string MobileNO { get; set; }
        public string VerifyCode { get; set; }

        private const string accountSid = "275c36f4549b4e439776a9b1e96b4f78";
        private const string authToken = "aaf12a72aa7b4042bb7a0a731b0f27cd";
        public bool SendVerifyCode()
        {
            string smsUrl = "https://api.miaodiyun.com/20150822/industrySMS/sendSMS";
            Dictionary<string, string> dic = new Dictionary<string, string>();
            dic.Add("accountSid", accountSid);
            string smsContent = $"【草芝能】尊敬的用户你好，你的验证码为：{VerifyCode}，请于 {15} 分钟内正确输入此验证码。";
            dic.Add("smsContent", smsContent);
            dic.Add("to", MobileNO);
            //dic.Add("portNumber","");
            string timestamp = DateTime.Now.ToString("yyyyMMddHHmmss");
            string sig = EncryptHelper.MD5(accountSid + authToken + timestamp);
            dic.Add("timestamp", timestamp);
            dic.Add("sig", sig);
            dic.Add("respDataType", "JSON");
            var response = HttpHelper.CreatePostHttpResponse(smsUrl, dic, null, null, Encoding.UTF8, null);
            string jsonStr = HttpHelper.WebResponse2Str(response, Encoding.UTF8);

            SMSBack smsBack = Common.JsonHelper<SMSBack>.DeserializeFromStr(jsonStr);

            return smsBack.failCount.ToInt(-1) == 0;
        }


        public class SMSBack
        {
            public string respCode { get; set; }
            public string failCount { get; set; }
            public Faillist[] failList { get; set; }
            public string smsId { get; set; }
        }

        public class Faillist
        {
            public string phone { get; set; }
            public string respCode { get; set; }
        }

        public class DevInfo
        {
            public string respCode { get; set; }
            public string developerId { get; set; }
            public string developerName { get; set; }
            public string createDate { get; set; }
            public string updateDate { get; set; }
            public string email { get; set; }
            public string mobile { get; set; }
            public string activationStatus { get; set; }
            public string status { get; set; }
            public string balance { get; set; }
            public string giftBlance { get; set; }
        }

        public DevInfo GetDevelopInfo()
        {
            string url = "https://api.miaodiyun.com/20150822/query/accountInfo";
            Dictionary<string, string> dic = new Dictionary<string, string>();
            dic.Add("accountSid", accountSid);
            string timestamp = DateTime.Now.ToString("yyyyMMddHHmmss");
            string sig = EncryptHelper.MD5(accountSid + authToken + timestamp);
            dic.Add("timestamp", timestamp);
            dic.Add("sig", sig);
            dic.Add("respDataType", "JSON");
            var response = HttpHelper.CreatePostHttpResponse(url, dic, null, null, Encoding.UTF8, null);
            string jsonStr = HttpHelper.WebResponse2Str(response, Encoding.UTF8);

            DevInfo devInfo = Common.JsonHelper<DevInfo>.DeserializeFromStr(jsonStr);

            return devInfo;
        }


    }
}

