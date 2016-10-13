using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Top.Api;
using Top.Api.Request;
using Top.Api.Response;

namespace Demo.SMS.part
{
    public class AliDayuSMS : ISMS
    {
        public AliDayuSMS(string mobileNO, string verifyCode)
        {
            MobileNO = mobileNO;
            VerifyCode = verifyCode;
            UserID = "kangwl";
            IdenParam = "草芝能";
        }

        public string MobileNO { get; set; }

        public string VerifyCode { get; set; }

        public string UserID { get; set; }

        public string IdenParam { get; set; }

        private const string serverUrl = "http://gw.api.taobao.com/router/rest";
        private const string appKey = "23478463";
        private const string appSecret = "d96bb6a448e1f2ef4b1f143d29e17390";

        public bool SendVerifyCode()
        {

            ITopClient client = new DefaultTopClient(serverUrl, appKey, appSecret);
            AlibabaAliqinFcSmsNumSendRequest req = new AlibabaAliqinFcSmsNumSendRequest();
            req.Extend = UserID;
            req.SmsType = "normal";
            req.SmsFreeSignName = "小康";//短信签名
            req.SmsParam = "{\"company\":\"" + IdenParam + "\",\"number\":\"" + VerifyCode + "\"}";
            req.RecNum = MobileNO;
            req.SmsTemplateCode = "SMS_17910073";//SMS_17910073,SMS_17435106
            AlibabaAliqinFcSmsNumSendResponse rsp = client.Execute(req);
            //Console.WriteLine(rsp.Body);

            return rsp.Result.Success;
        }
        //语音验证码
        public bool SendVerifyCodeSingleCall()
        {
            ITopClient client = new DefaultTopClient(serverUrl, appKey, appSecret);
            AlibabaAliqinFcTtsNumSinglecallRequest req = new AlibabaAliqinFcTtsNumSinglecallRequest();
            req.Extend = UserID;
            req.TtsParam = "{\"AckNum\":\"123456\"}";
            req.CalledNum = MobileNO;
            req.CalledShowNum = "051482043261";
            req.TtsCode = "TTS_10001";
            AlibabaAliqinFcTtsNumSinglecallResponse rsp = client.Execute(req);
            //Console.WriteLine(rsp.Body);

            return rsp.Result.Success;
        }
    }
}
