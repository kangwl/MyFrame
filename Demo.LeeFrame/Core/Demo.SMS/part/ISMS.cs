using Demo.SMS.part;

namespace Demo.SMS
{
    public interface ISMS
    {
        string MobileNO { get; set; }
        string VerifyCode { get; set; }
        SMSTypeEnum SmsType { get; set; }
        bool SendVerifyCode();
 
    }
}