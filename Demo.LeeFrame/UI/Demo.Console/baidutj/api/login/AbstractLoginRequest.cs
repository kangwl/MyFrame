/**
 * 
 */



/**
 * @author baidu
 * @date 2012-10-16
 * @version V1.0
 */

namespace Demo.Console.baidutj.api.login
{
    public abstract class AbstractLoginRequest : LoginRequest {
        public string username { get; set; }
        public string token { get; set; }
        public string functionName { get; set; }
        public string uuid { get; set; }
        public object request { get; set; }
    }
}

