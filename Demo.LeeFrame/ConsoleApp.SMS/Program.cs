using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Demo.Common;
using Demo.SMS;
using Demo.SMS.part;

namespace ConsoleApp.SMS
{
    class Program
    {
        static void Main(string[] args)
        {
            ////短信发送调用
            string mobileNO = "15869165949";//15869165949
            string verifyCode = RandomHelper.CreateNumberStr(6);
            //bool ok = Demo.SMS.SMSMain.SendVerifyCode(new JuHeSMS(mobileNO, verifyCode));

            //Console.WriteLine(ok);
            //if (ok)
            //{
            //    //用户输入验证码后进行比对
            //    bool checkOK = Demo.SMS.SMSMain.CheckVerifyCode(mobileNO, verifyCode);
            //    Console.WriteLine("check:" + checkOK);
            //}

            //Console.Read();


            //MiaoDiSMS miaoDiSms = new MiaoDiSMS(mobileNO, verifyCode);
            // miaoDiSms.GetDevelopInfo();
            bool ok = SMSMain.SendVerifyCode(new AliDayuSMS(mobileNO, verifyCode));

            Console.WriteLine(ok);

     
            Console.Read();

        }
    }
}
