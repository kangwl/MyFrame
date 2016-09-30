/**
 * RSAUtil.java
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
 * RSAUtil是一个运用RSA加密算法进行签名和加密、解密的工具类
 * 
 * @used 暂无项目使用
 * @category veyron code -> 公共库 -> 编码加密
 * @author baidu
 * @version 1.0.0
 */

using System;
using System.IO;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace Demo.Console.baidutj.api
{
    public class RSAUtil {
        public static  String KEY_ALGORITHM = "RSA";

        /**
     * 用公钥解密通过私钥加密的数据
     * 
     * @param data 通过私钥加密的数据
     * @param key 用来解密的公钥
     * @return 解密后的数据
     * 
     * @throws NoSuchAlgorithmException 假如用户的JDK不支持RSA
     * @throws InvalidKeySpecException 假如根据privateKey生成密钥失败
     * @throws InvalidKeyException 假如输入的RSA私钥不合法
     * @throws SignatureException 假如根据privateKey生成密钥失败
     * @throws UnsupportedEncodingException 假如privateKey不是使用UTF-8进行编码
     * @throws NoSuchPaddingException 假如产生的密钥对有问题
     * @throws BadPaddingException 假如输入的加密的数据填充数据错误
     * @throws IllegalBlockSizeException 假如输入的加密的数据字节数不是BlockSize的整数倍
     * @throws ShortBufferException 
     */
        public static byte[] decryptByPublicKey(byte[] data, string publicKey) {
                // 取得公钥
                //KeyFactory keyFactory = KeyFactory.getInstance(KEY_ALGORITHM);

                //// 对数据解密
                //Cipher cipher = Cipher.getInstance(keyFactory.getAlgorithm());
                //cipher.init(Cipher.DECRYPT_MODE, publicKey);

                //return CipherUtil.process(cipher, 128, data);

            RSACryptoServiceProvider rsa = new RSACryptoServiceProvider();
            byte[] cipherbytes;
            rsa.FromXmlString(publicKey);
            cipherbytes = rsa.Decrypt(data, false);
            return cipherbytes;
        }

        /**
     * 用公钥加密数据
     * 
     * @param data 等待加密的原始数据
     * @param key 用来加密的公钥
     * @return 加密后的数据
     * 
     * @throws NoSuchAlgorithmException 假如用户的JDK不支持RSA
     * @throws InvalidKeySpecException 假如根据privateKey生成密钥失败
     * @throws InvalidKeyException 假如输入的RSA私钥不合法
     * @throws SignatureException 假如根据privateKey生成密钥失败
     * @throws UnsupportedEncodingException 假如privateKey不是使用UTF-8进行编码
     * @throws NoSuchPaddingException 假如产生的密钥对有问题
     * @throws BadPaddingException 假如输入的加密的数据填充数据错误
     * @throws IllegalBlockSizeException 假如输入的加密的数据字节数不是BlockSize的整数倍
     * @throws ShortBufferException 
     */

        public static byte[] encryptByPublicKey(byte[] data, string publicKey)
        {
            // 取得公钥
            //KeyFactory keyFactory = KeyFactory.getInstance(KEY_ALGORITHM);

            //// 对数据加密
            //Cipher cipher = Cipher.getInstance(keyFactory.getAlgorithm());
            //cipher.init(Cipher.ENCRYPT_MODE, publicKey);

            //return CipherUtil.process(cipher, 117, data);
            RSACryptoServiceProvider rsa = new RSACryptoServiceProvider(); 
            int MaxBlockSize = rsa.KeySize / 8 - 11;    //加密块最大长度限制

            if (data.Length <= MaxBlockSize)
                return rsa.Encrypt(data, false);

            using (MemoryStream PlaiStream = new MemoryStream(data))
            using (MemoryStream CrypStream = new MemoryStream())
            {
                Byte[] Buffer = new Byte[MaxBlockSize];
                int BlockSize = PlaiStream.Read(Buffer, 0, MaxBlockSize);

                while (BlockSize > 0)
                {
                    Byte[] ToEncrypt = new Byte[BlockSize];
                    Array.Copy(Buffer, 0, ToEncrypt, 0, BlockSize);

                    Byte[] Cryptograph = rsa.Encrypt(ToEncrypt, false);
                    CrypStream.Write(Cryptograph, 0, Cryptograph.Length);

                    BlockSize = PlaiStream.Read(Buffer, 0, MaxBlockSize);
                }

                return CrypStream.ToArray();
            }
            //byte[] cipherbytes;
            //rsa.FromXmlString(publicKey);
            //cipherbytes = rsa.Encrypt(data, false);
            //return cipherbytes;
        }

        /**
     * 取得公钥
     * 
     * @param keyMap 密钥对Map
     * @return 公钥
     * @throws UnsupportedEncodingException 
     * @throws InvalidKeySpecException 
     * @throws NoSuchAlgorithmException 
     */
   //     public static Key getPublicKey(string key) {
   //         // 解密由base64编码的公钥
   //         byte[] keyBytes = Base64.decode(key);
			//System.Security.Cryptography.X509Certificates.X509Certificate keyCertificate=new X509Certificate(keyBytes);
   //         // 构造X509EncodedKeySpec对象
   //        // X509EncodedKeySpec keySpec = new X509EncodedKeySpec(keyBytes);

   //         // KEY_ALGORITHM 指定的加密算法
   //         //KeyFactory keyFactory = KeyFactory.getInstance(KEY_ALGORITHM);

   //         RSA rsa = new RSACryptoServiceProvider();
   //         keyCertificate.GetPublicKey();
   //         System.Security.Cryptography.RijndaelManaged rm = new System.Security.Cryptography.RijndaelManaged
   //         {
   //             Key = Encoding.UTF8.GetBytes(key),
   //             Mode = System.Security.Cryptography.CipherMode.ECB,
   //             Padding = System.Security.Cryptography.PaddingMode.PKCS7
   //         };
   //         // 取公钥匙对象
   //         return keyFactory.generatePublic(keySpec);
   //     }
    
    }
}
