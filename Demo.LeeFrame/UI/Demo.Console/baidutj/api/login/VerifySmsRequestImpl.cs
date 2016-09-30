/**
 * Baidu.com Inc.
 * Copyright (c) 2000-2013 All Rights Reserved.
 */ 

/**
 * @title VerifySmsRequestImpl
 * @description TODO 
 * @author lyb1985031
 * @date 2013-4-12
 * @version 1.0
 */

using System;

namespace Demo.Console.baidutj.api.login
{
    public class VerifySmsRequestImpl : AbstractLoginRequest{

        private VerifySmsRequest request;
    
    
    
        public void setRequest(VerifySmsRequest request) {
            this.request = request;
        }
         
        public new VerifySmsRequest getRequest() {
            return request;
        }

        public void setUcid(long ucid) {
            initRequest();
            this.request.setUcid(ucid);
        }

        public void setSt(String st) {
            initRequest();
            this.request.setSt(st);
        }
    
        public void setCode(String code)
        {
            initRequest();
            this.request.setCode(code);
        }
    
        private void initRequest()
        {
            if(request==null)
                request = new VerifySmsRequest();
        }
    }
}
