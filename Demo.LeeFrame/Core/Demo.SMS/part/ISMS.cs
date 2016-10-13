namespace Demo.SMS
{
    public interface ISMS
    {
        string MobileNO { get; set; }
        string VerifyCode { get; set; }
        bool SendVerifyCode();
    }
}