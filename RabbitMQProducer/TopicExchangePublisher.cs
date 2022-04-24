using Newtonsoft.Json;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace RabbitMQProducer
{
    public static class TopicExchangePublisher
    {
        public static void Publish(IModel channel)
        {
            var exchangeName = "demo-topic-exchange";
            var routeKeyName = "account.update";

            var ttl = new Dictionary<string, object>
            {
                { "x-message-ttl", 30000 }
            };
            channel.ExchangeDeclare(exchangeName, ExchangeType.Topic, arguments: ttl);

            int count = 0;
            while (true)
            {
                var message = new { Name = "Producer", Message = $"Hello! Count: {count}" };
                var body = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(message));
                Console.WriteLine(message.Message);
                channel.BasicPublish(exchangeName, routeKeyName, null, body);
                count++;
                Thread.Sleep(5000);
            }
        }
    }
}