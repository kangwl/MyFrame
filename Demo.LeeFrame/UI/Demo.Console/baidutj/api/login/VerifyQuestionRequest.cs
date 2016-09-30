/**
 * VerifyQuestionRequest.java
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
using System.Text;

namespace Demo.Console.baidutj.api.login
{
    public class VerifyQuestionRequest {
        // 用户ucid
        private long ucid;
        // 会话ID
        private String st;
        // 用户回答的安全问题ID
        private int qid;
        // 用户输入安全问题答案
        private String answer;

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

        public int getQid() {
            return qid;
        }

        public void setQid(int qid) {
            this.qid = qid;
        }

        public String getAnswer() {
            return answer;
        }

        public void setAnswer(String answer) {
            this.answer = answer;
        }
         
        public String toString() {
            StringBuilder builder = new StringBuilder();
            builder.Append("VerifyQuestionRequest [ucid=").Append(ucid).Append(", st=").Append(st).Append(", qid=")
                .Append(qid).Append(", answer=").Append(answer).Append("]");
            return builder.ToString();
        }

    }
}
