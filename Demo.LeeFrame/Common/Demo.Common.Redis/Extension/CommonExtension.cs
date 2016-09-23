using StackExchange.Redis;

namespace Demo.Common.Redis
{
    public static class CommonExtension
    {
        public static TModel ToModel<TModel>(this RedisValue redisValue) 
        {
            if (redisValue.HasValue)
            {
                return redisValue.ToString().ToModel<TModel>();
            }
            return default(TModel);
        }
    }
}