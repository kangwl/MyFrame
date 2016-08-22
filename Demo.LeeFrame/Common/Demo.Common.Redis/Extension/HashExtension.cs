using System.Collections.Generic;
using System.Linq; 
using StackExchange.Redis;

namespace Demo.Common.Redis
{
    /// <summary>
    ///     hash 操作扩展
    /// </summary>
    public static partial class RedisHelper
    {
        public static bool HashSetModelEXT<TModel>(string key, string hashField, TModel value) where TModel : class, new()
        { 
            return Db.HashSet(key, hashField, value.ToJson());
        }

        /// <summary>
        ///     存在会覆盖，不在会创建
        /// </summary>
        /// <param name="dic"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public static bool HashSetEXT(Dictionary<string, dynamic> dic, string key)
        {
            var count = dic.Count;
            Db.HashSet(key, dic.Select(pair => new HashEntry(pair.Key, pair.Value)).ToArray());
            return true;
        }

        public static TModel HashGetModelEXT<TModel>(string key, string hashField) where TModel : class
        {
            return Db.HashGet(key, hashField).ToModel<TModel>();
        }


        public static string[] HashGetMutiEXT(string key, params string[] fieldList)
        {
            var redisValues = Db.HashGet(key, fieldList.Select(one => (RedisValue) one).ToArray())
                .Select(one => one.ToStringEXT()).ToArray();
            return redisValues;
        }

        public static void HashSetModelsEXT<TModel>(string key, Dictionary<string, TModel> dicModels,
            CommandFlags flags = CommandFlags.None) where TModel : class, new()
        {
            var hashEntries =
                dicModels.Select(pair => new HashEntry(pair.Key, pair.Value.ToJson())).ToList();

            Db.HashSet(key, hashEntries.ToArray(), flags);
        }

        public static List<TModel> HashGetModelsEXT<TModel>(string key) where TModel : class
        {
            var hashEntries = Db.HashGetAll(key);

            var tModels =
                hashEntries.Select(hashEntry => hashEntry.Value)
                    .Select(redisValue => redisValue.ToModel<TModel>())
                    .ToList();
            return tModels;
        }
    }
}