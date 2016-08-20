using StackExchange.Redis;

namespace Demo.Common.Redis.Extension
{
    public static class CommonExtension
    {
        public static TModel ToModel<TModel>(this RedisValue redisValue) where TModel : class
        {
            if (redisValue.HasValue)
            {
                return redisValue.ToString().ToModel<TModel>();
            }
            return default(TModel);
        }
    }
}