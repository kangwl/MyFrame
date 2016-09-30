

/**
 * @author @author@ (@author-email@)
 * @version @version@, $Date: 2012-4-10$
 */

using Demo.Console.baidutj.api.login;

namespace Demo.Console.baidutj.api.seclogin
{
    public interface SecLoginService
    {
        PreLoginResponse preLogin(PreLoginRequestImpl req);

        DoLoginResponse doLogin(DoLoginRequestImpl req);

        VerifyQuestionResponse verifyQuestion(VerifyQuestionRequestImpl req);

        DoLogoutResponse doLogout(DoLogoutRequestImpl req);

        VerifySmsResponse verifySms(VerifySmsRequestImpl req);

        SendSmsResponse sendSms(SendSmsRequestImpl req);
    }
}
