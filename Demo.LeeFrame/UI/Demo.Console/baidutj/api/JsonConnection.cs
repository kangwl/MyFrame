/**
 * JsonConnection.java
 *
 * Copyright 2011 Baidu, Inc.
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
 * @version @version@, $Date: 2011-5-10$
 */

using System;
using System.IO;
using System.Net;
using Demo.Common.web;

namespace Demo.Console.baidutj.api
{
    public class JsonConnection {

        private HttpWebRequest connection;
        private int connectTimeout = -1; // 默认连接超时30秒
        private int readTimeout = -1; // 默认连接超时60秒

        public int getConnectTimeout() {
            return connectTimeout;
        }

        public void setConnectTimeout(int connectTimeout) {
            this.connectTimeout = connectTimeout;
        }

        public int getReadTimeout() {
            return readTimeout;
        }

        public void setReadTimeout(int readTimeout) {
            this.readTimeout = readTimeout;
        }

        /**
     * @param url The request URL
     * @throws IOException
     * @throws MalformedURLException
     */
        public JsonConnection(string url)
        { 
            connection = WebRequest.CreateHttp(url);
        }

        /**
     * 向服务器发送信息
     *
     * @throws ClientInternalException
     */

        protected Stream sendRequest() {

 

            Stream outsStream = null;
            try {
                if (connectTimeout > 0)
                {
                    connection.ReadWriteTimeout = connectTimeout;
                }
                if (readTimeout > 0)
                {
                    connection.ReadWriteTimeout = readTimeout; 
                }
                connection.Method = "POST";
                connection.ContentType = "text/json; charset=utf-8";
                connection.Headers.Add("UUID:767474055kwl");//,Content-Type:data/gzencode and rsa public encrypt;charset=UTF-8
                connection.Headers.Add("account_type:1 ");
                //connection.Headers.Add("Content-Type", "text/json; charset=utf-8");
                // connection.Headers.Add("UUID", "KANGEFG-1234567");

                outsStream = connection.GetRequestStream();
                return outsStream;
            } catch (Exception e)
            {
                throw;
            }
        }

        /**
     * 读取服务器返回的信息
     *
     * @return 读取到的数据
     */

        protected Stream readResponse()
        {
            Stream insStream = null;
            try
            {
                insStream = connection.GetResponse().GetResponseStream();
                StreamReader reader = new StreamReader(insStream);
                string r = reader.ReadToEnd();
                return insStream;
            }
            catch (IOException e)
            {
                throw;
            }
        }

    }
}
