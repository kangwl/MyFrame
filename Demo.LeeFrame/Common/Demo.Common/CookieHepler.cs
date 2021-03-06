﻿using System;
using System.Collections.Generic;
using System.Web;

namespace Demo.Common
{
    public class CookieHepler
    {
        public static string EncodeVal(string val)
        {
            if (string.IsNullOrEmpty(val)) return val;
            var enVal = EncryptHelper.Encrypt(val);
            return HttpUtility.UrlEncode(enVal);
        }

        public static string DecodeVal(string val)
        {
            if (string.IsNullOrEmpty(val)) return val;
            var deVal = HttpUtility.UrlDecode(val);
            var normalVal = "";
            EncryptHelper.Decrypt(deVal, out normalVal);
            return normalVal;
        }

        /// <summary>
        ///     添加一个单值cookie
        ///     如果已存在则覆盖相同键的值
        /// </summary>
        /// <param name="name"></param>
        /// <param name="value"></param>
        /// <param name="days"></param>
        public static void Add(string name, string value, int days)
        {
            var acookie = new HttpCookie(name);
            acookie.Value = EncodeVal(value);
            acookie.Expires = DateTime.Now.AddDays(days);
            HttpContext.Current.Response.Cookies.Add(acookie);
        }

        /// <summary>
        ///     添加一个多值cookie
        ///     如果已存在则覆盖相同键的值
        /// </summary>
        /// <param name="name"></param>
        /// <param name="dicItems"></param>
        /// <param name="days"></param>
        public static void AddRange(string name, Dictionary<string, string> dicItems, int days)
        {
            var cookie = new HttpCookie(name);
            foreach (var item in dicItems)
            {
                cookie.Values[item.Key] = EncodeVal(item.Value);
            }
            cookie.Expires = DateTime.Now.AddDays(days);
            HttpContext.Current.Response.Cookies.Add(cookie);
        }

        /// <summary>
        ///     获取一个单值cookie
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public static string Get(string name)
        {
            var cookieVal = string.Empty;
            if (HttpContext.Current.Request.Cookies[name] != null)
            {
                cookieVal = HttpContext.Current.Request.Cookies[name].Value;
            }
            return DecodeVal(cookieVal);
        }

        public static bool IsSet(string key)
        {
            object obj = HttpContext.Current.Request.Cookies[key];
            return null != obj;
        }

        /// <summary>
        ///     获取一个多值cookie
        /// </summary>
        /// <param name="name"></param>
        /// <param name="keys"></param>
        /// <returns></returns>
        public static Dictionary<string, string> GetRange(string name, List<string> keys)
        {
            if (HttpContext.Current.Request.Cookies[name] != null)
            {
                var dicItems = new Dictionary<string, string>();
                foreach (var key in keys)
                {
                    var valDecode = DecodeVal(HttpContext.Current.Request.Cookies[name][key]);
                    dicItems.Add(key, valDecode);
                }
                return dicItems;
            }
            return null;
        }

        /// <summary>
        ///     修改一个cookie
        /// </summary>
        /// <param name="name"></param>
        /// <param name="value"></param>
        public static void Update(string name, string value)
        {
            //获取客户端的Cookie对象
            var cok = HttpContext.Current.Request.Cookies[name];

            if (cok != null)
            {
                //修改Cookie的两种方法
                cok.Value = EncodeVal(value);

                HttpContext.Current.Response.AppendCookie(cok);
            }
        }

        /// <summary>
        ///     修改多个键值的 cookie
        /// </summary>
        /// <param name="name"></param>
        /// <param name="dicItems"></param>
        public static void UpdateRange(string name, Dictionary<string, string> dicItems)
        {
            var cok = HttpContext.Current.Request.Cookies[name];
            if (cok != null)
            {
                //修改Cookie的两种方法
                foreach (var item in dicItems)
                {
                    cok[item.Key] = EncodeVal(item.Value);
                }

                HttpContext.Current.Response.AppendCookie(cok);
            }
        }

        /// <summary>
        ///     删除cookie，即使之过期
        /// </summary>
        /// <param name="name"></param>
        public static void Delete(string name)
        {
            var cok = HttpContext.Current.Request.Cookies[name];
            if (cok != null)
            {
                var ts = new TimeSpan(-1, 0, 0, 0);
                cok.Expires = DateTime.Now.Add(ts); //删除整个Cookie，只要把过期时间设置为现在
                HttpContext.Current.Response.AppendCookie(cok);
            }
        }
    }
}