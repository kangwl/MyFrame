/**
 * LoginProxy.java
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
 * @version @version@, $Date: 2012-4-10$
 */

using System;
using System.IO;
using System.Net;
using Demo.Common;
using Demo.Common.ExtensionMethods;
using Demo.Common.web;
using Demo.Console.baidutj.api;
using Demo.Console.baidutj.api.login;
using Demo.Console.baidutj.api.seclogin;

namespace Demo.Console.baidutj.seclogin_test
{
    public class SecLoginProxy : SecLoginService
    {

        //上线机器
        public static string ADDRESS = "https://api.baidu.com/sem/common/HolmesLoginService";
        //        public static final String ADDRESS = "http://fctest.baidu.com:8080/gateway/sem/common/SecLoginService";
        public static string KEY =
            @"<RSAKeyValue><Modulus>3TY7mJK3DWt/kFo6i/vgqJQ13Kc8mb7wEB7FKad40DFqHtxlXRcZup/B5U3C5kUvLvD2/36vCIt8aH8GwslQFOsmflN42/mKv0LpdIltW0nDI60BuNSDpGmMZtRk4k+bJvN+9+056AZduu3BCRJxgk3qn8xsdeWbp+um42j4uLs=</Modulus><Exponent>AQAB</Exponent></RSAKeyValue>";

        public static string UUID = "767474055kwl";
        private string username;
        private string token;

        /**
     * @param username
     * @param token
     */

        public SecLoginProxy(string username, string token)
        {
            this.username = username;
            this.token = token;
        }

        private LoginConnection makeConnection()
        {
            LoginConnection l;
            try
            {
               // Key publicKey = RSAUtil.getPublicKey(KEY);
                l = new LoginConnection(ADDRESS);
                l.setPublicKey(KEY);
                return l;
            }
            catch (Exception e)
            {
                throw;
            }
        }

        /* (non-Javadoc)
    * @see com.baidu.api.client.core.login.LoginService#preLogin(com.baidu.api.client.core.login.PreLoginRequest)
    */

        public PreLoginResponse preLogin(PreLoginRequestImpl req)
        {
            LoginConnection l = makeConnection();

            req.username = (username);
            req.uuid = UUID;
            req.token = (token);
            req.functionName = ("preLogin");
            l.sendRequest(req);
            return l.readSecClearResponse<PreLoginResponse, PreLoginRequestImpl>(req);
            // string json = Newtonsoft.Json.JsonConvert.SerializeObject(req);
            //// string ret = Common.web.HttpHelper.PostData(ADDRESS, json);
            // //
            // return new PreLoginResponse();
            //var paramD =new {username = "kangwenli", token = "f4285c3d7462bb946e5910ef412bf2eb", functionName= "preLogin", uuid= "767474055kwl", request=new PreLoginRequest() {clientVersion="1.0",deviceType= "cellphone",osVersion= "Android" } };
            //string json = Newtonsoft.Json.JsonConvert.SerializeObject(paramD);

            //try
            //{
            //    HttpWebRequest webRequest = null;
            //    StreamWriter requestWriter = null;

            //    var postString = json;
            //    var responseData = "";
            //    webRequest = WebRequest.Create(ADDRESS) as HttpWebRequest;
            //    //
            //    webRequest.Headers.Add("UUID:767474055kwl");//,Content-Type:data/gzencode and rsa public encrypt;charset=UTF-8
            //    webRequest.Headers.Add("ACCOUNT_TYPE:1 ");
            //    webRequest.ContentType = "data/gzencode and rsa public encrypt;data/json;charset=UTF-8";
            //    //text/json; charset=utf-8

            //    webRequest.Method = "POST";
            //    webRequest.ServicePoint.Expect100Continue = false;
            //    webRequest.Timeout = 1000 * 60;
            //    // webRequest.ContentType = "application/x-www-form-urlencoded";
            //    //POST the data. 
            //    requestWriter = new StreamWriter(webRequest.GetRequestStream());
            //    try
            //    {
            //        byte[] zip = ZipHelper.Compress(json.ToByteArray());
            //        zip = RSAUtil.encryptByPublicKey(zip, KEY);
            //        requestWriter.Write(zip);
            //    }
            //    catch (Exception ex2)
            //    {
            //        //return "连接错误";
            //    }
            //    finally
            //    {
            //        requestWriter.Close();
            //    }
            //    responseData = HttpHelper.WebResponseGet(webRequest);
            //   // return responseData;
            //}
            //catch
            //{
            //    //return "未知错误";
            //} 
            //return new PreLoginResponse();
        }

        /* (non-Javadoc)
     * @see com.baidu.api.client.core.login.LoginService#doLogin(com.baidu.api.client.core.login.DoLoginRequest)
     */

        public DoLoginResponse doLogin(DoLoginRequestImpl req)
        {
            LoginConnection l = makeConnection();
            req.username=(username);
            req.uuid=(UUID);
            req.token=(token);
            req.functionName=("doLogin");
            l.sendRequest(req);
            return l.readSecClearResponse<DoLoginResponse, DoLoginRequestImpl>(req);
        }

/* (non-Javadoc)
 * @see com.baidu.api.client.core.login.LoginService#verifyQuestion(com.baidu.api.client.core.login.VerifyQuestionRequest)
 */

        public VerifyQuestionResponse verifyQuestion(VerifyQuestionRequestImpl req)
        {
            LoginConnection l = makeConnection();
            req.username=(username);
            req.uuid=(UUID);
            req.token=(token);
            req.functionName=("verifyQuestion");
            l.sendRequest(req);
            return l.readSecClearResponse<VerifyQuestionResponse, VerifyQuestionRequestImpl>(req);
        }

        /* (non-Javadoc)
     * @see com.baidu.api.client.core.login.LoginService#doLogout(com.baidu.api.client.core.login.DoLogoutRequest)
     */

        public DoLogoutResponse doLogout(DoLogoutRequestImpl req)
        {
            LoginConnection l = makeConnection();
            req.username=(username);
            req.uuid=(UUID);
            req.token=(token);
            req.functionName=("doLogout");
            l.sendRequest(req);
            return l.readSecClearResponse<DoLogoutResponse, DoLogoutRequestImpl>(req);
        }

        /* (non-Javadoc)
     * @see com.baidu.api.client.core.seclogin.SecLoginService#verifySms(com.baidu.drapi.iauth.pubsec.VerifySmsRequestImpl)
     */

        public VerifySmsResponse verifySms(VerifySmsRequestImpl req)
        {
            LoginConnection l = makeConnection();
            req.username=(username);
            req.uuid=(UUID);
            req.token=(token);
            req.functionName=("verifySms");
            l.sendRequest(req);
            return l.readSecClearResponse<VerifySmsResponse, VerifySmsRequestImpl>(req);
        }

        /* (non-Javadoc)
     * @see com.baidu.api.client.core.seclogin.SecLoginService#sendSms(com.baidu.drapi.iauth.pubsec.SendSmsRequestImpl)
     */

        public SendSmsResponse sendSms(SendSmsRequestImpl req)
        {
            LoginConnection l = makeConnection();
            req.username=(username);
            req.uuid=(UUID);
            req.token=(token);
            req.functionName=("sendSms");
            l.sendRequest(req);
            return l.readSecClearResponse<SendSmsResponse, SendSmsRequestImpl>(req);
        }
    }
}
