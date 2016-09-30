/**
 * Base64.java
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
 * @version @version@, $Date: 2012-3-21$
 * 
 */

using System;
using System.Text;
using Demo.Common.ExtensionMethods;

namespace Demo.Console.baidutj.api
{
    public class Base64
    {
        private static char[] base64EncodeChars = new char[]
        {
            'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L',
            'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z', 'a', 'b', 'c', 'd', 'e', 'f', 'g',
            'h', 'i', 'j', 'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z', '0', '1',
            '2', '3', '4', '5', '6', '7', '8', '9', '+', '/'
        };

        private static int[] base64DecodeChars = new int[]
        {
            -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1,
            -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1,
            -1, 62, -1, -1, -1, 63, 52, 53, 54, 55, 56, 57, 58, 59, 60, 61, -1, -1, -1, -1, -1, -1, -1, 0, 1, 2, 3, 4,
            5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23, 24, 25, -1, -1, -1, -1, -1, -1, 26,
            27, 28, 29, 30, 31, 32, 33, 34, 35, 36, 37, 38, 39, 40, 41, 42, 43, 44, 45, 46, 47, 48, 49, 50, 51, -1, -1,
            -1, -1, -1
        };

        public static string encode(byte[] data)
        {
            return data.Base64Encode();
            StringBuilder sb = new StringBuilder();
            int len = data.Length;
            int i = 0;
            int b1, b2, b3;
            while (i < len)
            {
                b1 = data[i++] & 0xff;
                if (i == len)
                {
                    sb.Append(base64EncodeChars[b1 >> 2]);
                    sb.Append(base64EncodeChars[(b1 & 0x3) << 4]);
                    sb.Append("==");
                    break;
                }
                b2 = data[i++] & 0xff;
                if (i == len)
                {
                    sb.Append(base64EncodeChars[b1 >> 2]);
                    sb.Append(base64EncodeChars[((b1 & 0x03) << 4) | ((b2 & 0xf0) >> 4)]);
                    sb.Append(base64EncodeChars[(b2 & 0x0f) << 2]);
                    sb.Append("=");
                    break;
                }
                b3 = data[i++] & 0xff;
                sb.Append(base64EncodeChars[b1 >> 2]);
                sb.Append(base64EncodeChars[((b1 & 0x03) << 4) | ((b2 & 0xf0) >> 4)]);
                sb.Append(base64EncodeChars[((b2 & 0x0f) << 2) | ((b3 & 0xc0) >> 6)]);
                sb.Append(base64EncodeChars[b3 & 0x3f]);
            }
            return sb.ToString();
        }

        public static byte[] decode(String str)
        {
            return str.Base64Decode().ToByteArray();

            StringBuilder sb = new StringBuilder();
            byte[] data = str.ToByteArray();
            int len = data.Length;
            int i = 0;
            int b1, b2, b3, b4;
            while (i < len)
            {

                do
                {
                    b1 = base64DecodeChars[data[i++]];
                } while (i < len && b1 == -1);
                if (b1 == -1)
                    break;

                do
                {
                    b2 = base64DecodeChars[data[i++]];
                } while (i < len && b2 == -1);
                if (b2 == -1)
                    break;
                sb.Append((char) ((b1 << 2) | ((b2 & 0x30) >> 4)));

                do
                {
                    b3 = data[i++];
                    if (b3 == 61)
                        return sb.ToString().ToByteArray();
                    b3 = base64DecodeChars[b3];
                } while (i < len && b3 == -1);
                if (b3 == -1)
                    break;
                sb.Append((char) (((b2 & 0x0f) << 4) | ((b3 & 0x3c) >> 2)));

                do
                {
                    b4 = data[i++];
                    if (b4 == 61)
                        return sb.ToString().ToByteArray();
                    b4 = base64DecodeChars[b4];
                } while (i < len && b4 == -1);
                if (b4 == -1)
                    break;
                sb.Append((char) (((b3 & 0x03) << 6) | b4));
            }
            return sb.ToString().ToByteArray();
        }

    }
}
