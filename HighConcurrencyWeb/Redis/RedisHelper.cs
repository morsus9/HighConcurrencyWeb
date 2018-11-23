using CSRedis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HighConcurrencyWeb.Redis
{
    public class RedisHelper
    {
        public static CSRedisClient GetCSRedisClient()
        {
            return new CSRedis.CSRedisClient("127.0.0.1:6379,password=,defaultDatabase=1,poolsize=50,ssl=false,writeBuffer=10240");
        }
            
    }
}
