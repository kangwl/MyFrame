using System;

namespace Demo.Common.Redis.Queue
{
    public class RedisQueueFactory
    {
        public RedisQueueFactory(string queueKey)
        {
            QueueKey = queueKey;
        }

        private string QueueKey { get; set; }
 
        public void Enqueue<T>(T t)
        {
            long ret = RedisHelper.ListLeftPushModelEXT(QueueKey, t);
        }

        public long QueueLeng {get { return RedisHelper.ListLength(QueueKey); } }

        public void Dequeue<T>(Action<T> action)
        {
            while (QueueLeng > 0)
            {
                T t = RedisHelper.ListRightPopModelEXT<T>(QueueKey);
                action(t);
            }
        }

        public bool ExistKey(string key)
        {
            return RedisHelper.ExistKey(key);
        }

    }
}
