using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Text;

namespace MQConsumer.RabbitMQ
{
    public class MQHelper
    {
        private static IConnection _connection;

        public static IConnection GetConnection()
        {
            if (_connection != null) return _connection;
            _connection = GetNewConnection();
            return _connection;
        }

        public static IConnection GetNewConnection()
        {
            var factory = new ConnectionFactory()
            {
                HostName = "localhost",
                UserName = "admin",
                Password = "123456",
                VirtualHost = "/"
            };

            _connection = factory.CreateConnection();
            return _connection;
        }
    }
}
