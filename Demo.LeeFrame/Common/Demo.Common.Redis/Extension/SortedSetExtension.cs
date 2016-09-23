using System.Collections.Generic;
using System.Linq;
using StackExchange.Redis;

namespace Demo.Common.Redis
{
    public static partial class RedisHelper
    {
        public static bool SortedSetAddModelEXT<TModel>(string key, TModel tModel, double score)
        {
            return Db.SortedSetAdd(key, tModel.ToJson(), score);
        }


        public static long SortedSetAddModelsEXT<TModel>(string key, Dictionary<TModel, double> dic,
            CommandFlags flags = CommandFlags.None) where TModel : class
        {
            var entries = dic.Select(pair => new SortedSetEntry(pair.Key.ToJson(), pair.Value)).ToArray();

            return Db.SortedSetAdd(key, entries, flags);
        }

        //RedisKey key, RedisValue member, double value, CommandFlags flags = CommandFlags.None
        public static double SortedSetDecrementModelEXT<TModel>(string key, TModel member,
            double value,
            CommandFlags flags = CommandFlags.None) where TModel : class
        {
            return Db.SortedSetDecrement(key, member.ToJson(), value, flags);
        }

        public static double SortedSetIncrementModelEXT<TModel>(string key, TModel member,
            double value,
            CommandFlags flags = CommandFlags.None) where TModel : class
        {
            return Db.SortedSetIncrement(key, member.ToJson(), value, flags);
        }

        public static Dictionary<TModel, double> SortedSetRangeByRankWithScoresModelsEXT<TModel>(this IDatabase Db,
            string key, long start = 0, long stop = -1, Order order = Order.Ascending,
            CommandFlags flags = CommandFlags.None) where TModel : class
        {
            var sortedSetEntries = Db.SortedSetRangeByRankWithScores(key, start, stop, order, flags);
            var dic = sortedSetEntries.ToDictionary(entry => entry.Element.ToModel<TModel>(),
                entry => entry.Score);
            return dic;
        }

        public static List<TModel> SortedSetRangeByRankModelsEXT<TModel>(string key, long start = 0,
            long stop = -1, Order order = Order.Ascending, CommandFlags flags = CommandFlags.None) where TModel : class
        {
            var redisValues = Db.SortedSetRangeByRank(key, start, stop, order, flags);
            return redisValues.Select(one => one.ToModel<TModel>()).ToList();
        }

        public static List<TModel> SortedSetRangeByScoreModelsEXT<TModel>(string key,
            double start = double.NegativeInfinity, double stop = double.PositiveInfinity,
            Exclude exclude = Exclude.None, Order order = Order.Ascending, long skip = 0, long take = -1,
            CommandFlags flags = CommandFlags.None) where TModel : class
        {
            var redisValues = Db.SortedSetRangeByScore(key, start, stop, exclude, order, skip, take, flags);

            return redisValues.Select(one => one.ToModel<TModel>()).ToList();
        }

        public static Dictionary<TModel, double> SortedSetRangeByScoreWithScoresModelsEXT<TModel>(this IDatabase Db,
            string key, double start = double.NegativeInfinity, double stop = double.PositiveInfinity,
            Exclude exclude = Exclude.None, Order order = Order.Ascending, long skip = 0, long take = -1,
            CommandFlags flags = CommandFlags.None) where TModel : class
        {
            var sortedSetEntries = Db.SortedSetRangeByScoreWithScores(key, start, stop, exclude, order,
                skip, take, flags);
            var dic = sortedSetEntries.ToDictionary(entry => entry.Element.ToModel<TModel>(),
                entry => entry.Score);
            return dic;
        }

        public static long? SortedSetRankModelEXT<TModel>(string key, TModel member,
            Order order = Order.Ascending, CommandFlags flags = CommandFlags.None) where TModel : class
        {
            return Db.SortedSetRank(key, member.ToJson(), order, flags);
        }

        public static bool SortedSetRemoveModelEXT<TModel>(string key, TModel member,
            CommandFlags flags = CommandFlags.None) where TModel : class
        {
            return Db.SortedSetRemove(key, member.ToJson(), flags);
        }

        public static long SortedSetRemoveModelsEXT<TModel>(string key, List<TModel> members,
            CommandFlags flags = CommandFlags.None) where TModel : class
        {
            return Db.SortedSetRemove(key, members.Select(one => (RedisValue) one.ToJson()).ToArray(), flags);
        }
    }
}