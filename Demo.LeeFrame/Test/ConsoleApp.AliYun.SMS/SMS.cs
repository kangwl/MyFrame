using System;

namespace ConsoleApp.AliYun.SMS
{
    public class SMS
    {
        public void SendVerifyCode(string mobileNO, string verifyCode, Action<string, string> sendVerifyCodeAction)
        {
            Demo.Common.Redis.RedisHelper.StringSet(mobileNO, verifyCode, TimeSpan.FromMinutes(15));
            string codeGet = Demo.Common.Redis.RedisHelper.StringGet(mobileNO);
            sendVerifyCodeAction(mobileNO, codeGet);
        }
    }
}
