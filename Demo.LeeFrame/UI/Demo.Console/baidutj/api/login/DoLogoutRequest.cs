/**
 * DoLogoutRequest.java
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

using System;

namespace Demo.Console.baidutj.api.login
{
    public class DoLogoutRequest {
        // 用户ucid
        private long ucid;
        // 会话ID
        private String st;

        public long getUcid() {
            return ucid;
        }

        public void setUcid(long ucid) {
            this.ucid = ucid;
        }

        public String getSt() {
            return st;
        }

        public void setSt(String st) {
            this.st = st;
        }
         
        public String toString() {
            return "[ucid=" + ucid + ", st=" + st + "]";
        }

    }
}
