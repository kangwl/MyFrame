using System;
using StackExchange.Redis;

namespace Demo.Common.Redis
{
    public static partial class RedisHelper
    {
        public static bool StringSetModelEXT<TModel>(string key, TModel tModel,
            TimeSpan? expiry = default(TimeSpan?), When when = When.Always, CommandFlags flags = CommandFlags.None)
            where TModel : class, new()
        {
            var json = tModel.ToJson();
            return Db.StringSet(key, json, expiry, when, flags);
        }

        public static TModel StringGetModelEXT<TModel>(string key,
            CommandFlags flags = CommandFlags.None) where TModel : class
        {
            var redisValue = Db.StringGet(key, flags);

            return redisValue.ToModel<TModel>();
        }
    }
}