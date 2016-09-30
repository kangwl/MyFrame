/**
 * 
 */ 


/**
 * @author baidu
 * @date 2012-10-15
 * @version V1.0
 */

using System;

namespace Demo.Console.baidutj.api.login
{
    public interface LoginRequest {
        string username { get; set; }

        string token { get; set; }
        string functionName { get; set; }
        string uuid { get; set; }

       
        object request { get; set; }
    }
}

