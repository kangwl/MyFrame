using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using ConsoleApplication1.Model;
using Demo.Common;
using Demo.Common.ExtensionMethods;
using Demo.Console.baidutj.api;

namespace ConsoleApplication1
{
    class Program
    {
        //https://api.baidu.com/sem/common/HolmesLoginService
        //https://api.baidu.com/sem/common/SecLoginService
        public static string ReqUrl = "https://api.baidu.com/sem/common/SecLoginService";
        public static string Uuid = "2C56DC4991F6";
        public static string Token = "f4285c3d7462bb946e5910ef412bf2eb";
        private static int MAX_MSG_SIZE = 4096;
        public static string publishKey =
            @"<RSAKeyValue><Modulus>3TY7mJK3DWt/kFo6i/vgqJQ13Kc8mb7wEB7FKad40DFqHtxlXRcZup/B5U3C5kUvLvD2/36vCIt8aH8GwslQFOsmflN42/mKv0LpdIltW0nDI60BuNSDpGmMZtRk4k+bJvN+9+056AZduu3BCRJxgk3qn8xsdeWbp+um42j4uLs=</Modulus><Exponent>AQAB</Exponent></RSAKeyValue>";

        private static HttpClient http = new HttpClient();
        static void Main(string[] args)
        {



            #region 1
            //string jsonReq = Demo.Common.JsonFac.Serialize2Json(loginRequestImpl);
            //byte[] bytes = CGZipUtil.GZipCompressBytes(jsonReq);
            //byte[] jmBytes = RSAEncryptMeessageBody(bytes);
            //HttpContent content = new ByteArrayContent(jmBytes);
            //Task<HttpResponseMessage> taskResponse = http.PostAsync(ReqUrl, content);
            //var res = taskResponse.Result;
            ////var n1 = res.RequestMessage;
            //var stream = res.Content.ReadAsStreamAsync().Result; 
            #endregion
            #region 2
            //byte[] b = new byte[8];
            //if (stream.Read(b, 0, b.Length) != 8)
            //{
            //    throw new Exception("Server response is invalid.");
            //}
            //if (b[1] != 0)
            //{
            //    throw new Exception("Server returned an error code: " + b[1]);
            //}
            //int total = 0, k = 0;
            //b = new byte[MAX_MSG_SIZE];
            //while (total < MAX_MSG_SIZE)
            //{
            //    k = stream.Read(b, total, MAX_MSG_SIZE - total);
            //    if (k <= 0)
            //        break;
            //    total += k;
            //}
            //if (total == MAX_MSG_SIZE)
            //{
            //    //throw new ClientInternalException("Server returned message too large.");
            //}
            //byte[] zip = ArrayUtils.GetNew(total, (a) => b[a]);
            //zip = GZipUtil.Decompress(zip);

            //string str = Encoding.UTF8.GetString(zip);

            //PreLoginResponse preLoginResponse = JsonHelper<PreLoginResponse>.DeserializeFromStr(str); 
            #endregion

            PreLoginResponse preLoginResponse = PreLogin();
            DoLoginWithInput(preLoginResponse);
        }

        public static PreLoginResponse PreLogin()
        {
            http.DefaultRequestHeaders.Add("UUID", Uuid);
            http.DefaultRequestHeaders.Add("account_type", "1");

            PreLoginRequestImpl loginRequestImpl = new PreLoginRequestImpl()
            {
                username = "kangwenli",
                token = Token,
                functionName = "preLogin",
                uuid = Uuid,
                request = new PreLoginRequest()
            };
            Stream stream = SendRequest(loginRequestImpl);

            PreLoginResponse preLoginResponse = ReadResponse<PreLoginResponse>(stream);
            if (preLoginResponse.needAuthCode)
            {
                if (!string.IsNullOrEmpty(preLoginResponse.authCode.imgdata))
                {
                    string imgData = preLoginResponse.authCode.imgdata;
                    string file = $"D:\\baidutj." + preLoginResponse.authCode.imgtype;
                    byte[] byteArr = Convert.FromBase64String(imgData);
                    FileStream fileStream = File.Create(file, byteArr.Length);
                    using (fileStream)
                    {
                        fileStream.Write(byteArr, 0, byteArr.Length);
                        fileStream.Flush();
                    }
                }
            }
            return preLoginResponse;
        }

        public static Stream SendRequest(object request)
        {
            string jsonReq = Demo.Common.JsonFac.Serialize2Json(request);
            byte[] bytes = CGZipUtil.GZipCompressBytes(jsonReq);
            byte[] jmBytes = RSAEncryptMeessageBody(bytes);
            HttpContent content = new ByteArrayContent(jmBytes);
            Task<HttpResponseMessage> taskResponse = http.PostAsync(ReqUrl, content);
            var res = taskResponse.Result;
            var stream = res.Content.ReadAsStreamAsync().Result;

            return stream;
        }

        public static T ReadResponse<T>(Stream stream)
        {
            byte[] b = new byte[8];
            if (stream.Read(b, 0, b.Length) != 8)
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
                if (k <= 0)
                    break;
                total += k;
            }
            if (total == MAX_MSG_SIZE)
            {
                //throw new ClientInternalException("Server returned message too large.");
            }
            byte[] zip = ArrayUtils.GetNew(total, a => b[a]);
            zip = GZipUtil.Decompress(zip);

            string str = Encoding.UTF8.GetString(zip);

            T preLoginResponse = JsonHelper<T>.DeserializeFromStr(str);

            return preLoginResponse;
        }

        public static void DoLoginWithInput(PreLoginResponse loginResponse)
        {
            //http = new HttpClient(); 
            string imgCode = "";
            if (loginResponse.needAuthCode)
            {
                Console.WriteLine("请输入验证码：");
                imgCode = Console.ReadLine();
            }

            DoLoginRequestImpl doLoginRequestImpl = new DoLoginRequestImpl()
            {
                username = "kangwenli",
                token = Token,
                functionName = "doLogin",
                uuid = Uuid,
                request =
                    new DoLoginRequest()
                    {
                        imageCode = imgCode,
                        imageSsid = loginResponse.authCode.imgssid,
                        password = "kangwenli123"
                    }
            };
           // http.BaseAddress=new Uri("");
            Stream stream = SendRequest(doLoginRequestImpl);

            string res = ReadResponse<string>(stream);
        }



        public static void DataApi(HttpClient http)
        {
            string url = "https://api.baidu.com/json/tongji/v1/ProductService/api";

        }


        public static void MessageBody()
        {
            RSACryptoServiceProvider rsa = new RSACryptoServiceProvider();
            rsa.FromXmlString(publishKey);
        }


        public static byte[] RSAEncryptMeessageBody(byte[] content)
        {

            List<byte> bytes = new List<byte>();
            if (content.Length > 117)
            {
                while (content.Length > 0)
                {
                    byte[] dealBytes;
                    if (content.Length - 117 > 0)
                    {
                        dealBytes = content.Take(117).ToArray();
                        int nowLen = content.Length - 117;
                        content = content.Skip(117).Take(nowLen).ToArray();
                    }
                    else
                    {
                        int nowLen = content.Length;
                        dealBytes = content;
                        content = new byte[] { };
                    }
                    byte[] enBytes = RSAEncrypt(dealBytes);
                    bytes.AddRange(enBytes);
                }

            }
            else
            {
                bytes.AddRange(RSAEncrypt(content));
            }
            var cipherbytes = bytes.ToArray();
            return cipherbytes;
        }

        public static byte[] RSAEncrypt(byte[] content)
        {
            RSACryptoServiceProvider rsa = new RSACryptoServiceProvider();
            rsa.FromXmlString(publishKey);

            var cipherbytes = rsa.Encrypt(content, false);
            return cipherbytes;
        }
    }
 



}
