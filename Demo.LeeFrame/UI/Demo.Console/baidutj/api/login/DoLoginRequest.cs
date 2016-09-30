/**
 * DoLoginRequest.java
 *
 * Copyright 2012 Baidu, Inc.
 *
 * Baidu licenses this file to you under the Apache License, version 2.0
 * (the "License"); you may not use this file except in compliance with the
 * License.  You may obtain a copy of the License at:
 *
 *    http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS, WITHOUT
 * WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.  See the
 * License for the specific language governing permissions and limitations
 * under the License.
 */ 

/**
 * @author @author@ (@author-email@)
 * 
 * @version @version@, $Date: 2012-3-22$
 * 
 */
namespace Demo.Console.baidutj.api.login
{
    public class DoLoginRequest {
        // 用户输入密码
        private string password;
        // 验证码
        private string imageCode;
        // 验证码会话id
        private string imageSsid;

        public string getPassword() {
            return password;
        }

        public void setPassword(string password) {
            this.password = password;
        }

        public string getImageCode() {
            return imageCode;
        }

        public void setImageCode(string imageCode) {
            this.imageCode = imageCode;
        }

        public string getImageSsid() {
            return imageSsid;
        }

        public void setImageSsid(string imageSsid) {
            this.imageSsid = imageSsid;
        }
         
        public string toString() {
            return "DoLoginRequest [imageCode=" + imageCode + ", imageSsid=" + imageSsid + "]";
        }

    }
}
