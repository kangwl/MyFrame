/**
 * 
 */ 


/**
 * @author baidu
 * @date 2013-1-8
 * @version V1.0
 */

using System;

namespace Demo.Console.baidutj.api.login
{
    public class SendSmsResponse {

        // 0: success, 190: session invalid, 197: wrong mobile number, 502: parameter error
        private int retcode;
        // error message
        private String retmsg;
        /**
	 * @return the retcode
	 */
        public int getRetcode() {
            return retcode;
        }
        /**
	 * @param retcode the retcode to set
	 */
        public void setRetcode(int retcode) {
            this.retcode = retcode;
        }
        /**
	 * @return the retmsg
	 */
        public String getRetmsg() {
            return retmsg;
        }
        /**
	 * @param retmsg the retmsg to set
	 */
        public void setRetmsg(String retmsg) {
            this.retmsg = retmsg;
        }
        /* (non-Javadoc)
	 * @see java.lang.Object#toString()
	 */ 
        public String toString() {
            return "SendSmsResponse [retcode=" + retcode + ", retmsg=" + retmsg
                   + "]";
        }	
    }
}

