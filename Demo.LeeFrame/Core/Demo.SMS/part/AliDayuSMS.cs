using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Demo.Common;
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
            SmsType = SMSTypeEnum.文本;
            Param = new ParamObj("尊敬的用户", verifyCode);
        }

        public string MobileNO { get; set; }

        public string VerifyCode { get; set; }
        public SMSTypeEnum SmsType { get; set; }

        public string UserID { get; set; }

        public string IdenParam { get; set; }

        /// <summary>
        /// 根据模板参数
        /// </summary>
        public class ParamObj
        {
            public ParamObj(string name, string verifyCode)
            {
                this.name = name;
                this.code = verifyCode;
            }
            public string name { get; set; }
            public string code { get; set; }
        }

        public ParamObj Param { get; set; }
        private const string serverUrl = "http://gw.api.taobao.com/router/rest";
        private const string appKey = "23478463";
        private const string appSecret = "d96bb6a448e1f2ef4b1f143d29e17390";

        public bool SendVerifyCode()
        {
            if (SmsType == SMSTypeEnum.文本)
            {
                return SendVerifyCodeText();
            }
            return SendVerifyCodeSingleCall();
        }

        public bool SendVerifyCodeText()
        {
            ITopClient client = new DefaultTopClient(serverUrl, appKey, appSecret);
            AlibabaAliqinFcSmsNumSendRequest req = new AlibabaAliqinFcSmsNumSendRequest();
            req.Extend = UserID;
            req.SmsType = "normal";
            req.SmsFreeSignName = "小康";//短信签名
            req.SmsParam = "{\"number\":\"" + VerifyCode + "\"}";
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
            string paramStr = JsonHelper<ParamObj>.Serialize2Object(Param, ifFormat: false);
            req.TtsParam = paramStr;
            req.CalledNum = MobileNO;
            req.CalledShowNum = "051482043260";
            req.TtsCode = "TTS_17900069";
            AlibabaAliqinFcTtsNumSinglecallResponse rsp = client.Execute(req);

            return rsp.Result.Success;
        }
    }
}
