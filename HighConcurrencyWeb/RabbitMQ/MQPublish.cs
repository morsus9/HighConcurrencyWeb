using HighConcurrencyWeb.Models;
using Newtonsoft.Json;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HighConcurrencyWeb.RabbitMQ
{
    public class MQPublish
    {

        public static void AddQueue(User user)
        {
            try
            {
                var factory = new ConnectionFactory() { HostName = "localhost", UserName = "admin", Password = "123456", VirtualHost = "/" };
                factory.AutomaticRecoveryEnabled = true;
                using (var connection = factory.CreateConnection())
                using (var channel = connection.CreateModel())
                {
                    channel.QueueDeclare(queue: "net", durable: false, exclusive: false, autoDelete: false, arguments: null);
                    var bytes = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(user));
                    channel.BasicPublish(exchange: "", routingKey: "net", basicProperties: null, body: bytes);

                }
            }
            catch
            {

            }
                //using (var channel = MQHelper.GetNewConnection().CreateModel())
                //{
                //    var bytes = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(user));
                //    channel.BasicPublish(String.Empty, QueueName, null, bytes);
                //}

        }

        public static void Write()
        {
            var factory = new ConnectionFactory() { HostName = "localhost", UserName = "admin", Password = "123456", };
            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                channel.QueueDeclare(queue: "net", durable: false, exclusive: false, autoDelete: false, arguments: null);
                string message = "12321";
                var body = Encoding.UTF8.GetBytes(message);

                channel.BasicPublish(exchange: "", routingKey: "net", basicProperties: null, body: body);

            }
        }

    }
}
