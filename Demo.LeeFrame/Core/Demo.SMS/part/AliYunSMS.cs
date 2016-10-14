using System.Collections.Generic;
using Aliyun.Acs.Core;
using Aliyun.Acs.Core.Exceptions;
using Aliyun.Acs.Core.Profile;
using Aliyun.Acs.Sms.Model.V20160927;
using Demo.Common;
using Demo.SMS.part;

namespace Demo.SMS
{
    public class AliYunSMS : ISMS
    {
        public AliYunSMS(string mobileNO, string verifyCode)
        {
            MobileNO = mobileNO;
            VerifyCode = verifyCode;
            TemplateCode = "SMS_17820059";
            SignName = "草芝能";
            ParamDic.Add("code", verifyCode);
        }
        /// <summary>
        /// 手机号
        /// </summary>
        public string MobileNO { get; set; }
        /// <summary>
        /// 验证码
        /// </summary>
        public string VerifyCode { get; set; }

        public SMSTypeEnum SmsType { get; set; }

        /// <summary>
        /// 短信签名
        /// </summary>
        public string SignName { get; set; }
        /// <summary>
        /// 短信模板code
        /// </summary>
        public string TemplateCode { get; set; }

        public Dictionary<string, string> ParamDic = new Dictionary<string, string>();

        #region base config
        private const string regionId = "cn-hangzhou";
        private const string accessKeyId = "bsc0Brx3rkgjXjsO";
        private const string accessSecret = "oT4yJqswpWApsfH66EqnsJ85TPCrRc";
        #endregion

        public bool SendVerifyCode()
        {
            IClientProfile profile = DefaultProfile.GetProfile(regionId, accessKeyId, accessSecret);
            IAcsClient client = new DefaultAcsClient(profile);
            SingleSendSmsRequest request = new SingleSendSmsRequest();
            try
            {
                request.SignName = SignName;
                request.TemplateCode = TemplateCode;//SMS_17820059,SMS_17520003
                request.RecNum = MobileNO; //接收号码，多个号码可以逗号分隔 
                //request.ParamString = "{\"company\":\"\",\"code\":\" " + VerifyCode + " \"}";
                string paramStr = JsonHelper<Dictionary<string, string>>.Serialize2Object(ParamDic, ifFormat: false);
                request.ParamString = paramStr;
                SingleSendSmsResponse httpResponse = client.GetAcsResponse(request);
                string reqID = httpResponse.RequestId;

                return true;
            }
            catch (ServerException e)
            {
                Log log = new Log();
                log.WriteLog("AliyunSMS.SendVerifyCode:" + e.ErrorCode + e.ErrorMessage);

                return false;
            }
            catch (ClientException e)
            {
                Log log = new Log();
                log.WriteLog("AliyunSMS.SendVerifyCode:" + e.ErrorCode + e.ErrorMessage);
                return false;
            }
        }
    }
}
