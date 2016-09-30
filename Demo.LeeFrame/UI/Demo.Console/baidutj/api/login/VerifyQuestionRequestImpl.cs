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
    public class VerifyQuestionRequestImpl : AbstractLoginRequest {

        private VerifyQuestionRequest request;

        /**
	 * @return the request
	 */
        public VerifyQuestionRequest getRequest() {
            return request;
        }

        /**
	 * @param request the request to set
	 */
        public void setRequest(VerifyQuestionRequest request) {
            this.request = request;
        }

        public void setUcid(long ucid) {
            initRequest();
            this.request.setUcid(ucid);
        }

        public void setSt(String st) {
            initRequest();
            this.request.setSt(st);
        }

        public void setQid(int qid) {
            initRequest();
            this.request.setQid(qid);
        }

        public void setAnswer(String answer) {
            initRequest();
            this.request.setAnswer(answer);
        }
	
        private void initRequest(){
            if(request == null){
                request = new VerifyQuestionRequest();
            }
        }
    }
}

