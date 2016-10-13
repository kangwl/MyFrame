using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Aliyun.Acs.Core;
using Aliyun.Acs.Core.Exceptions;
using Aliyun.Acs.Core.Profile;
using Aliyun.Acs.Sms.Model.V20160927;
using Demo.Common;
using Demo.SMS;

namespace ConsoleApp.AliYun.SMS
{
    class Program
    {
        static void Main(string[] args)
        {
            AliyunSMS.SendVerifyCode("15869165949", RandomHelper.CreateNumberStr(4));
            Console.Read();

           // bool ok = SMSMain.SendVerifyCode("15869165949", RandomHelper.CreateNumberStr(4), Demo.SMS.AliYunSMS.SendVerifyCode);

        }
    }

    
    public class AliyunSMS
    {
        public static void SendVerifyCode(string mobileNO,string verifyCode)
        {
            IClientProfile profile = DefaultProfile.GetProfile("cn-hangzhou", "bsc0Brx3rkgjXjsO", "oT4yJqswpWApsfH66EqnsJ85TPCrRc");
            IAcsClient client = new DefaultAcsClient(profile);
            SingleSendSmsRequest request = new SingleSendSmsRequest();
            try
            {
                request.SignName = "草芝能";
                request.TemplateCode = "SMS_17520003";
                request.RecNum = mobileNO;//接收号码，多个号码可以逗号分隔 
                request.ParamString = "{\"company\":\"伊冠生物\",\"code\":\" " + verifyCode + " \"}";
                SingleSendSmsResponse httpResponse = client.GetAcsResponse(request);
                string reqID = httpResponse.RequestId;
            }
            catch (ServerException e)
            {
                Log log = new Log();
                log.WriteLog("AliyunSMS.SendVerifyCode:" + e.Message + e.StackTrace);
            }
            catch (ClientException e)
            {
                Log log = new Log();
                log.WriteLog("AliyunSMS.SendVerifyCode:" + e.Message + e.StackTrace);
            }
        }

        class AliYunSMSParams
        {
            public AliYunSMSParams(string company, string code)
            {
                this.company = company;
                this.code = code;
            }
            public string company { get; set; }
            public string code { get; set; }
        }
    }
}
