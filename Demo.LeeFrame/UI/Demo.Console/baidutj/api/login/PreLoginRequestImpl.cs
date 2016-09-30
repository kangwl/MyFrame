/**
 *
 */


/**
 * @author baidu
 * @version V1.0
 * @date 2012-10-16
 */

using System;

namespace Demo.Console.baidutj.api.login
{
    public class PreLoginRequestImpl : AbstractLoginRequest {

        private PreLoginRequest request;

        /**
         * @return the request
         */
        public PreLoginRequest getRequest()
        {
            return request;
        }

        /**
         * @param request the request to set
         */
        public void setRequest(PreLoginRequest request)
        {
            this.request = request;
        }

        public void setOsVersion(String osVersion)
        {
            initRequest();
            this.request.osVersion=(osVersion);
        }

        public void setDeviceType(String deviceType)
        {
            initRequest();
            this.request.deviceType=(deviceType);
        }

        public void setClientVersion(String clientVersion)
        {
            initRequest();
            this.request.clientVersion=(clientVersion);
        }

        private void initRequest()
        {
            if (request == null)
            {
                request = new PreLoginRequest();
            }
        }
    }
}

