/**
 * 
 */ 

/**
 * @author baidu
 * @date 2012-10-16
 * @version V1.0
 */

using System;

namespace Demo.Console.baidutj.api.login
{
    public class DoLoginRequestImpl : AbstractLoginRequest {

        private DoLoginRequest request;

        /**
	 * @return the request
	 */
        public DoLoginRequest getRequest() {
            return request;
        }

        /**
	 * @param request the request to set
	 */
        public void setRequest(DoLoginRequest request) {
            this.request = request;
        }
	
        private void initRequest(){
            if(request == null){
                this.request = new DoLoginRequest();
            }
        }
	
        public void setPassword(String password) {
            initRequest();
            this.request.setPassword(password);
        }

        public void setImageCode(String imageCode) {
            initRequest();
            this.request.setImageCode(imageCode);
        }

        public void setImageSsid(String imageSsid) {
            initRequest();
            this.request.setImageSsid(imageSsid);
        }
    }
}

