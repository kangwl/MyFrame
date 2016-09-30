/**
 * LoginConnection.java
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
 * @version @version@, $Date: 2012-3-21$
 */

using System;
using System.IO;
using System.IO.Compression;
using Demo.Common;
using Demo.Common.ExtensionMethods;

namespace Demo.Console.baidutj.api
{
   public static class ArrayUtils
    {

        public static T[] GetNew<T>(int length, Func<int, T> init)
       {

           T[] arr = new T[length];

           for (int i = 0; i < length; i++) arr[i] = init(i);

           return arr;

       }

       public static R[] Select<T, R>(this T[] arr, Func<T, R> map)
       {

           R[]  res = new R[arr.Length];

           for (int i = 0; i < arr.Length; i++) res[i] = map(arr[i]);

           return res;

       }

    }
    public class LoginConnection : JsonConnection {
        private static  int MAX_MSG_SIZE = 4096;
        private string publicKey;

        /**
     * @param url
     * @throws MalformedURLException
     * @throws IOException
     */
        public LoginConnection(String url) : base(url)
        { 
        }

        /**
     * 向服务器发送信息
     *
     * @param body 向服务器发送的信封对象
     */

        public void sendRequest(object request)
        {
            Stream outStream = sendRequest();
            try
            {
                string json = Newtonsoft.Json.JsonConvert.SerializeObject(request);
                //System.out.println("json:" + json); 

                byte[] zip = ZipHelper.Compress(json.ToByteArray());
                zip = RSAUtil.encryptByPublicKey(zip, publicKey);
                //System.out.println("zip:" + zip.length);
                outStream.Write(zip, 0, zip.Length);
            }
            catch (Exception e)
            {
                throw;
            }
            finally
            {
                if (outStream != null)
                {
                    try
                    {
                        outStream.Close();
                    }
                    catch (IOException e)
                    {
                        throw;
                    }
                }
            }
        }

        public T1 readSecClearResponse<T1, T>(T t)
        {
            Stream stream =  readResponse();
            try
            {
                byte[] b = new byte[8];
                if (stream.Read(b,0,b.Length) != 8)
                {
                    throw new Exception("Server response is invalid.");
                }
                if (b[1] != 0)
                {
                    throw new Exception("Server returned an error code: " + b[1]);
                }
                int total = 0, k = 0;
                b = new byte[MAX_MSG_SIZE];
                while (total < MAX_MSG_SIZE)
                {
                    k = stream.Read(b, total, MAX_MSG_SIZE - total);
                    if (k < 0)
                        break;
                    total += k;
                }
                if (total == MAX_MSG_SIZE)
                {
                    throw new Exception("Server returned message too large.");
                }
                byte[] zip = ArrayUtils.GetNew(total, (a) => b[a]);
                zip = GZipUtil.Decompress(zip);
                return Common.JsonHelper<T1>.DeserializeFromStr(zip.ToEncodingString());
            }
            catch (IOException e)
            {
                throw  e;
            }
            finally
            {
                if (stream !=  null)
                {
                    try
                    {
                        stream.Close();
                    }
                    catch (IOException e)
                    {
                        throw e;
                    }
                }
            }
        }

        public void setPublicKey(string publicKey) {
            this.publicKey = publicKey;
        }

    }
}
