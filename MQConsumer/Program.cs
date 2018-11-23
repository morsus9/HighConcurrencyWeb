using HighConcurrencyWeb.Models;
using Microsoft.EntityFrameworkCore;
using MQConsumer.RabbitMQ;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace MQConsumer
{
    class Program
    {
        //test
        public static void Main()
        {
            int i = 0;
            var factory = new ConnectionFactory() { HostName = "localhost", UserName = "admin", Password = "123456", VirtualHost = "/" };
            factory.AutomaticRecoveryEnabled = true;
            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                channel.QueueDeclare(queue: "net",
                                     durable: false,
                                     exclusive: false,
                                     autoDelete: false,
                                     arguments: null);

                var consumer = new EventingBasicConsumer(channel);
                consumer.Received += (model, ea) =>
                {
                    try
                    {
                        i++;

                        var user = JsonConvert.DeserializeObject<User>(Encoding.UTF8.GetString(ea.Body));

                        //用户的第一次请求,为有效请求
                        //下面开始入库,这里使用List做为模拟

                        var _dbContext = Context.DBContext();

                        _dbContext.Persons.Add(new Person() { Id2 = user.Id.ToString(), Name = user.Name });
                        Task ts = _dbContext.SaveChangesAsync();
                        ts.Wait();
                        Console.WriteLine(user.Id + user.Name + "入库成功    " + i);


                        //用户的N次请求,为无效请求
                        channel.BasicAck(ea.DeliveryTag, false);
                    }
                    catch (Exception ex)
                    {
                        ExcuateWriteFile(ex.ToString());
                    }

                };
                channel.BasicConsume(queue: "net", autoAck: true,
                                     consumer: consumer);

                Console.WriteLine(" Press [enter] to exit.");
                Console.ReadLine();
            }
        }
        public static void ExcuateWriteFile(string i)
        {
            using (FileStream fs = new FileStream(@"d:\\error.txt", FileMode.Append))
            {
                using (StreamWriter sw = new StreamWriter(fs, Encoding.Unicode))
                {
                    sw.Write(i);
                }
            }
        }

    }
}
