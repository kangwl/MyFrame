/**
 * LoginTest.java
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
using System.Security.AccessControl;
using Demo.Console.baidutj.api;
using Demo.Console.baidutj.api.login;
using Demo.Console.baidutj.api.seclogin;

namespace Demo.Console.baidutj.seclogin_test
{
    public class SecLoginTest
    {

        public static string IMAGE_PATH = "D:\\";

        /**
     * @param args
     * @throws IOException
     */

        public static void TJLoginTest()
        {
            //测试用户userName,tokenStr
            SecLoginService login = new SecLoginProxy("kangwenli", "f4285c3d7462bb946e5910ef412bf2eb");

            PreLoginResponse res = pre(login);

            DoLoginResponse dol = doLoginWithInput(login, res);

            if (dol.getQuestions() != null && dol.getQuestions().Count > 0)
                veri(login, dol);

            SendSmsResponse sendRes = sendSms(login, dol);

            if (sendRes != null && sendRes.getRetcode() != 0)
            {
                System.Console.WriteLine(sendRes.getRetcode() + "," + sendRes.getRetmsg());
            }

            System.Console.WriteLine("send success");

            VerifySmsResponse verifyRes = verifySms(login, dol);

            if (verifyRes != null && verifyRes.getRetcode() != 0)
            {
                System.Console.WriteLine(verifyRes.getRetcode() + "," + verifyRes.getRetmsg());
            }
            System.Console.WriteLine("verify success");

            doLogout(login, dol);

        }


        /**
     * @param login
     * @param dol
     */

        private static void doLogout(SecLoginService login, DoLoginResponse dol)
        {
            DoLogoutRequestImpl req = new DoLogoutRequestImpl();
            req.setSt(dol.getSt());
            req.setUcid(dol.getUcid());
            DoLogoutResponse r = login.doLogout(req);
            System.Console.WriteLine(r);
        }

        /**
     * @param login
     * @param dol
     * @return
     */

        private static VerifyQuestionResponse veri(SecLoginService login,
            DoLoginResponse dol)
        {
            VerifyQuestionRequestImpl req = new VerifyQuestionRequestImpl();
            req.setAnswer("1234567890");
            req.setQid(dol.getQuestions()[0].getQid());
            req.setSt(dol.getSt());
            req.setUcid(dol.getUcid());
            VerifyQuestionResponse r = login.verifyQuestion(req);
            // System.out.println(r);
            return r;
        }

        private static PreLoginResponse pre(SecLoginService login)
        {
            PreLoginRequest loginRequest=new PreLoginRequest();
            loginRequest.clientVersion = "1.0";
            loginRequest.deviceType = "cellphone";
            loginRequest.osVersion = "Android";
            PreLoginRequestImpl req = new PreLoginRequestImpl();
            req.setRequest(loginRequest);
            req.request = loginRequest;
            //req.setClientVersion("1.=0");
            //req.setDeviceType("cellphone");
            //req.setOsVersion("Android");
           
            PreLoginResponse res = login.preLogin(req);
            // System.out.println(res);
            // 保存验证码图片到本地
            if (res.getAuthCode() != null)
            {
                saveAuthImage(res);
            }
            return res;
        }

        private static void saveAuthImage(PreLoginResponse res)
        {
            if (res != null)
            {
                try
                {
                    byte[] buff = Base64.decode(res.getAuthCode().getImgdata()); 
                    string file = IMAGE_PATH + "out." + res.getAuthCode().getImgtype();
                    FileStream fileStream = File.Create(file, buff.Length);
                    using (fileStream)
                    {
                        fileStream.Write(buff, 0, buff.Length);
                        fileStream.Flush();
                    } 
                }
                catch (IOException e)
                {
                    throw e;
                }

            }
        }

        private static DoLoginResponse doLoginWithInput(SecLoginService login,
            PreLoginResponse res)
        {
            DoLoginRequestImpl req = new DoLoginRequestImpl();
            // req.setImageCode("1111");
            if (res.getAuthCode() != null)
            {
                //Scanner in =
                //new Scanner(System.in);
                //String code = in.
                //nextLine();
                //req.setImageCode(code);
                //req.setImageSsid(res.getAuthCode().getImgssid());
            }
            //线上白名单用户
            //req.setPassword("Colin128");
            //线上对照组用户
            //req.setPassword("Aa123456");
            //线下测试用户
            req.setPassword("kangwenli123");
            DoLoginResponse dol = login.doLogin(req);
    
            return dol;
        }

        private static SendSmsResponse sendSms(SecLoginService login, DoLoginResponse dlr)
        {
            SendSmsRequestImpl req = new SendSmsRequestImpl();
            req.setRequest(new SendSmsRequest());
            req.functionName=("sendSms");
            req.setUcid(430);
            req.token=("12345678");
            req.username=("86flower");
            req.setStrMobile("12345678900");
            req.uuid=("dr-api-test-123456");
            req.setSt(dlr.getSt());

            return login.sendSms(req);
        }

        private static VerifySmsResponse verifySms(SecLoginService login, DoLoginResponse dlr)
        {
            VerifySmsRequestImpl req = new VerifySmsRequestImpl();
            req.setRequest(new VerifySmsRequest());
            req.setCode("123456");
            req.functionName=("verifySms");
            req.setSt(dlr.getSt());
            req.token=("12345678");
            req.setUcid(430);
            req.username=("86flower");
            req.uuid=("dr-api-test-123456");

            return login.verifySms(req);
        }
    }
}
