using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;

namespace Demo.Common
{
    public class RSAHelper
    {
        /// <summary>
        /// 公钥循环加密消息体
        /// </summary>
        /// <param name="content"></param>
        /// <param name="publishKey"></param>
        /// <param name="encryptOneceLen"></param>
        /// <returns></returns>
        public static byte[] RSAEncryptMeessageBody(byte[] content, string publishKey, int encryptOneceLen = 117)
        {

            List<byte> bytes = new List<byte>();
            if (content.Length > encryptOneceLen)
            {
                while (content.Length > 0)
                {
                    byte[] dealBytes;
                    if (content.Length - encryptOneceLen > 0)
                    {
                        dealBytes = content.Take(encryptOneceLen).ToArray();
                        int nowLen = content.Length - encryptOneceLen;
                        content = content.Skip(encryptOneceLen).Take(nowLen).ToArray();
                    }
                    else
                    {
                        int nowLen = content.Length;
                        dealBytes = content;
                        content = new byte[] {};
                    }
                    byte[] enBytes = RSAEncrypt(dealBytes, publishKey);
                    bytes.AddRange(enBytes);
                }

            }
            else
            {
                bytes.AddRange(RSAEncrypt(content, publishKey));
            }
            var cipherbytes = bytes.ToArray();
            return cipherbytes;
        }

        /// <summary>
        /// 公钥加密
        /// </summary>
        /// <param name="content"></param>
        /// <param name="publishKey"></param>
        /// <returns></returns>
        public static byte[] RSAEncrypt(byte[] content,string publishKey)
        {
            RSACryptoServiceProvider rsa = new RSACryptoServiceProvider();
            rsa.FromXmlString(publishKey);

            var cipherbytes = rsa.Encrypt(content, false);
            return cipherbytes;
        }

    }
}
