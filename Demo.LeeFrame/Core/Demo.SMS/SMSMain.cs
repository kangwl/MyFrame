using System;
using Demo.Common;

namespace Demo.SMS
{
    public class SMSMain
    {
        /// <summary>
        /// 验证码过期时间（分钟）
        /// </summary>
        public static int ExpireMinutes = 15;

        /// <summary>
        /// 组装缓存的 key
        /// </summary>
        /// <param name="mobileNO"></param>
        /// <returns></returns>
        public static string GetVerifyKey(string mobileNO)
        {
            return $"m.verify.{mobileNO}";
        }
  
        /// <summary>
        /// 入口 发送短信验证码
        /// </summary> 
        /// <param name="smsObj"></param>
        /// <returns></returns>
        public static bool SendVerifyCode(ISMS smsObj)
        { 
            bool sendSuccess = smsObj.SendVerifyCode();
            if (sendSuccess)
            {
                SaveVeriyCode(smsObj.MobileNO, smsObj.VerifyCode);
                string codeGet = GetVerifyCode(smsObj.MobileNO);
                if (string.IsNullOrEmpty(codeGet)) return false;
            }
            return sendSuccess;
        }
        /// <summary>
        /// 检测 验证码
        /// </summary>
        /// <param name="mobileNO"></param>
        /// <param name="verifyCode"></param>
        /// <returns></returns>
        public static bool CheckVerifyCode(string mobileNO, string verifyCode)
        {
            if (string.IsNullOrEmpty(verifyCode)) return false;
            string codeGet = GetVerifyCode(mobileNO);
            if (string.IsNullOrEmpty(codeGet)) return false;
            //remove from redis
            RemoveVerifyCode(mobileNO);
            return codeGet == verifyCode;
        }

        private static void SaveVeriyCode(string mobileNO, string verifyCode)
        {
            string verifyKey = GetVerifyKey(mobileNO);
            bool success = Demo.Common.Redis.RedisHelper.StringSet(verifyKey, verifyCode, TimeSpan.FromMinutes(ExpireMinutes));
            if (!success)
            {
                CacheHelper.Insert(GetVerifyKey(mobileNO), verifyCode, DateTime.Now.AddMinutes(ExpireMinutes));
            }
        }

        private static string GetVerifyCode(string mobileNO)
        {
            string verifyKey = GetVerifyKey(mobileNO);
            string code= Demo.Common.Redis.RedisHelper.StringGet(verifyKey);
            if (string.IsNullOrEmpty(code))
            {
                code = CacheHelper.Get(verifyKey).ToStringEXT();
            }
            return code.Trim();
        }

        private static void RemoveVerifyCode(string mobileNO)
        {
            string verifyKey = GetVerifyKey(mobileNO);
            Demo.Common.Redis.RedisHelper.Remove(verifyKey);
            CacheHelper.Remove(verifyKey);
        }



    }
}
