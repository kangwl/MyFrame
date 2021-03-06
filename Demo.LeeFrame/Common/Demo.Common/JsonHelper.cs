﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Xml;
using System.Xml.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Bson;
using Newtonsoft.Json.Linq;
using Formatting = Newtonsoft.Json.Formatting;

namespace Demo.Common
{
    public class JsonFac
    {
        public static string Serialize2Json(object obj)
        {
            var json = JsonConvert.SerializeObject(obj);
            return json;
        }

        /// <summary>
        ///     获取JObject
        ///     取值EXP： string zhangjia = jo["items"]["264648"].ToString();
        /// </summary>
        /// <param name="json"></param>
        /// <returns></returns>
        public static JObject GetJObject(string json)
        {
            var jo = (JObject) JsonConvert.DeserializeObject(json);
            return jo;
        }
    }

    public class JsonHelper<T>
    {
        /// <summary>
        ///     一些序列化设置
        /// </summary>
        /// <returns></returns>
        private static JsonSerializerSettings CreateSettings(string dateFormat)
        {
            var settings = new JsonSerializerSettings();
            //格式化时间
            settings.DateFormatString = dateFormat;
            return settings;
        }

        /// <summary>
        ///     对象序列化成json
        /// </summary>
        /// <param name="t"></param>
        /// <param name="ifFormat"></param>
        /// <param name="isDate"></param>
        /// <returns></returns>
        public static string Serialize2Object(T t, bool ifFormat = true, bool isDate = false)
        {
            var dateFormat = "yyyy-MM-dd HH:mm:ss";
            if (isDate)
            {
                dateFormat = "yyyy-MM-dd";
            }

            string json;
            try
            {
                json = JsonConvert.SerializeObject(t, ifFormat ? Formatting.Indented : Formatting.None,
                    CreateSettings(dateFormat));
            }
            catch (Exception ex)
            {
                json = string.Empty;
            }
            return json;
        }

        /// <summary>
        ///     保存 json 到文件
        /// </summary>
        /// <param name="t"></param>
        /// <param name="file">@"c:\movie.json"</param>
        /// <returns></returns>
        public static void Serialize2File(T t, string file)
        {
            // serialize JSON directly to a file


            using (var streamWriter = File.CreateText(file))
            {
                var serializer = new JsonSerializer();
                serializer.Serialize(streamWriter, t);
            }
        }

        /// <summary>
        ///     反序列化成对象
        /// </summary>
        /// <param name="json"></param>
        /// <returns></returns>
        public static T DeserializeFromStr(string json)
        {
            T t;
            try
            {
                t = JsonConvert.DeserializeObject<T>(json);
            }
            catch (Exception)
            {
                t = default(T);
            }
            return t;
        }

        /// <summary>
        ///     解析（反序列化）文件
        /// </summary>
        /// <param name="filePath">@"c:\movie.json"</param>
        /// <returns></returns>
        public static T DeserializeFromFile(string filePath)
        {
            // deserialize JSON directly from a file
            T t;
            try
            {
                using (var file = File.OpenText(filePath))
                {
                    var serializer = new JsonSerializer();
                    t = (T) serializer.Deserialize(file, typeof (T));
                }
            }
            catch (Exception)
            {
                t = default(T);
            }
            return t;
        }

        /// <summary>
        ///     创建json ，对象列表
        /// </summary>
        /// <param name="jsonName"></param>
        /// <param name="ts"></param>
        /// <returns></returns>
        public static string CreateJson(string jsonName, IEnumerable<T> ts)
        {
            string json;
            try
            {
                var array = new JArray();
                foreach (var t in ts)
                {
                    array.Add(t);
                }
                var o = new JObject();
                o[jsonName] = array;
                json = 0.ToString(CultureInfo.InvariantCulture);
            }
            catch (Exception)
            {
                json = string.Empty;
            }
            return json;
        }

        /// <summary>
        ///     用动态队形穿件json
        /// </summary>
        /// <param name="dynamicObj"></param>
        /// <returns></returns>
        public static string CreateJsonDynamic(dynamic dynamicObj)
        {
            return dynamicObj.ToString();
        }

        /// <summary>
        ///     json 转换成 xml
        /// </summary>
        /// <param name="json"></param>
        /// <param name="root"></param>
        /// <returns></returns>
        public static string Json2Xml(string json, string root = "Root")
        {
            XNode node = JsonConvert.DeserializeXNode(json, root);
            return node.ToString();
        }

        /// <summary>
        ///     xml转换成json
        /// </summary>
        /// <param name="xml"></param>
        /// <returns></returns>
        public static string Xml2Json(string xml)
        {
            var doc = new XmlDocument();
            doc.LoadXml(xml);
            var json = JsonConvert.SerializeXmlNode(doc);
            return json;
        }

        /// <summary>
        ///     序列化成 base64string
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        public static string Serialize2Bson(T t)
        {
            var ms = new MemoryStream();
            using (var writer = new BsonWriter(ms))
            {
                var serializer = new JsonSerializer();
                serializer.Serialize(writer, t);
            }
            var data = Convert.ToBase64String(ms.ToArray());
            return data;
        }

        /// <summary>
        ///     解析 base64string
        /// </summary>
        /// <param name="bson"></param>
        /// <returns></returns>
        public static T DeserializeBson(string bson)
        {
            T t;
            var data = Convert.FromBase64String(bson);
            var ms = new MemoryStream(data);
            using (var reader = new BsonReader(ms))
            {
                var serializer = new JsonSerializer();
                t = serializer.Deserialize<T>(reader);
            }
            return t;
        }

        /// <summary>
        ///     解析 base64string 到集合
        /// </summary>
        /// <param name="bsons">MQAAAAMwACkAAAACTmFtZQAHAAAARWFzdGVyAAlTdGFydERhdGUAgDf0uj0BAAAAAA==</param>
        /// <returns></returns>
        public static IEnumerable<T> DeserializeBsonList(string bsons)
        {
            var data = Convert.FromBase64String(bsons);
            var ms = new MemoryStream(data);
            using (var reader = new BsonReader(ms))
            {
                reader.ReadRootValueAsArray = true;
                var serializer = new JsonSerializer();
                var ts = serializer.Deserialize<IEnumerable<T>>(reader);
                return ts;
            }
        }

        #region jobject

        //         1JObject o = new JObject
        // 2  {
        // 3    {"Cpu", "Intel"},
        // 4    {"Memory", 32},
        // 5    {
        // 6      "Drives", new JArray
        // 7        {
        // 8          "DVD",
        // 9          "SSD"
        //10        }
        //11    }
        //12  };
        //13
        //14Console.WriteLine(o.ToString());
        //15// {
        //16//   "Cpu": "Intel",
        //17//   "Memory": 32,
        //18//   "Drives": [
        //19//     "DVD",
        //20//     "SSD"
        //21//   ]
        //22// } 

        #endregion
    }
}