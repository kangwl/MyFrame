﻿using System.Collections.Generic;
using System.Linq;
using StackExchange.Redis;

namespace Demo.Common.Redis
{
    public static partial class RedisHelper
    {
        public static bool SetAddModelEXT<TModel>(string key, TModel tModel,
            CommandFlags flags = CommandFlags.None) 
        {
            return Db.SetAdd(key, tModel.ToJson(), flags);
            //Db.SetCombine()
        }

        /// <summary>
        ///     返回添加的数量
        /// </summary>
        /// <typeparam name="TModel"></typeparam> 
        /// <param name="key"></param>
        /// <param name="tModels"></param>
        /// <param name="flags"></param>
        /// <returns></returns>
        public static long SetAddModelsEXT<TModel>(string key, List<TModel> tModels,
            CommandFlags flags = CommandFlags.None) where TModel : class
        {
            var redisValues = tModels.Select(t => (RedisValue) t.ToJson()).ToArray();
            return Db.SetAdd(key, redisValues, flags);
        }

        public static List<TModel> SetCombineModelsEXT<TModel>(SetOperation operation,
            string firstKey, string secondKey, CommandFlags flags = CommandFlags.None) where TModel : class
        {
            var redisValues = Db.SetCombine(operation, firstKey, secondKey, flags);
            return redisValues.Select(value => value.ToModel<TModel>()).ToList();
        }

        public static List<TModel> SetCombineModelsEXT<TModel>(SetOperation operation,
            List<string> keys, CommandFlags flags = CommandFlags.None) where TModel : class
        {
            var redisValues = Db.SetCombine(operation, keys.Select(s => (RedisKey) s).ToArray(), flags);
            return redisValues.Select(value => value.ToModel<TModel>()).ToList();
        }

        /// <summary>
        ///     判断set中是否存在此实体
        /// </summary>
        /// <typeparam name="TModel"></typeparam> 
        /// <param name="key"></param>
        /// <param name="tModel"></param>
        /// <param name="flags"></param>
        /// <returns></returns>
        public static bool SetContainsModelEXT<TModel>(string key, TModel tModel,
            CommandFlags flags = CommandFlags.None) where TModel : class
        {
            return Db.SetContains(key, tModel.ToJson(), flags);
        }

        public static List<TModel> SetMembersModelsEXT<TModel>(string key,
            CommandFlags flags = CommandFlags.None) where TModel : class
        {
            var redisValues = Db.SetMembers(key, flags);
            return redisValues.Select(value => value.ToModel<TModel>()).ToList();
        }

        public static bool SetMoveModelEXT<TModel>(string sourceKey, string destinationKey,
            TModel tModel,
            CommandFlags flags = CommandFlags.None) where TModel : class
        {
            return Db.SetMove(sourceKey, destinationKey, tModel.ToJson(), flags);
        }

        public static TModel SetPopModelEXT<TModel>(string key,
            CommandFlags flags = CommandFlags.None) 
        {
            var redisValue = Db.SetPop(key, flags);
            return redisValue.ToModel<TModel>();
        }

        public static TModel SetRandomMemberModelEXT<TModel>(string key,
            CommandFlags flags = CommandFlags.None) where TModel : class
        {
            var redisValue = Db.SetRandomMember(key, flags);
            return redisValue.ToModel<TModel>();
        }

        public static List<TModel> SetRandomMemberModelsEXT<TModel>(string key, long count,
            CommandFlags flags = CommandFlags.None) where TModel : class
        {
            var redisValues = Db.SetRandomMembers(key, count, flags);
            return redisValues.Select(value => value.ToModel<TModel>()).ToList();
        }

        public static bool SetRemoveModelEXT<TModel>(string key, TModel tModel,
            CommandFlags flags = CommandFlags.None) where TModel : class
        {
            return Db.SetRemove(key, tModel.ToJson(), flags);
        }

        public static long SetRemoveModelsEXT<TModel>(string key, List<TModel> tModels,
            CommandFlags flags = CommandFlags.None) where TModel : class
        {
            var redisValues = tModels.Select(t => (RedisValue) t.ToJson()).ToArray();

            return Db.SetRemove(key, redisValues, flags);
        }

        public static IEnumerable<TModel> SetScanModelsEXT<TModel>(string key, string pattern,
            int pageSize, CommandFlags flags) where TModel : class
        {
            var enumerable = Db.SetScan(key, pattern, pageSize, flags);
            var tModels = enumerable.Select(one => one.ToModel<TModel>());
            return tModels;
        }

        public static List<TModel> SetSortModelsEXT<TModel>(string key, long skip = 0, long take = -1,
            Order order = Order.Ascending, SortType sortType = SortType.Numeric, RedisValue by = default(RedisValue),
            List<TModel> get = null, CommandFlags flags = CommandFlags.None) where TModel : class
        {
            RedisValue[] redisValues = null;
            if (get != null)
            {
                redisValues = get.Select(one => (RedisValue) one.ToJson()).ToArray();
            }
            var sortValueses = Db.Sort(key, skip, take, order, sortType, @by, redisValues, flags);
            return sortValueses.Select(one => one.ToModel<TModel>()).ToList();
        }

        public static long SetSortAndStoreModelsEXT<TModel>(string destinationKey, string key,
            long skip = 0, long take = -1, Order order = Order.Ascending, SortType sortType = SortType.Numeric,
            RedisValue by = default(RedisValue), List<TModel> get = null, CommandFlags flags = CommandFlags.None)
            where TModel : class
        {
            RedisValue[] redisValues = null;
            if (get != null)
            {
                redisValues = get.Select(one => (RedisValue) one.ToJson()).ToArray();
            }
            return Db.SortAndStore(destinationKey, key, skip, take, order, sortType, by, redisValues, flags);
        }
    }
}