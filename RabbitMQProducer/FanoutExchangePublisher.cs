using Newtonsoft.Json;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace RabbitMQProducer
{
    public static class FanoutExchangePublisher
    {
        public static void Publish(IModel channel)
        {
            var exchangeName = "demo-fanout-exchange";
            var routeKeyName = string.Empty;

            var ttl = new Dictionary<string, object>
            {
                { "x-message-ttl", 30000 }
            };
            channel.ExchangeDeclare(exchangeName, ExchangeType.Fanout, arguments: ttl);

            int count = 0;
            while (true)
            {
                var message = new { Name = "Producer", Message = $"Hello! Count: {count}" };
                var body = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(message));

                var properties = channel.CreateBasicProperties();
                properties.Headers = new Dictionary<string, object> { { "account", "new" } };
                Console.WriteLine(message.Message);
                channel.BasicPublish(exchangeName, routeKeyName, properties, body);
                count++;
                Thread.Sleep(5000);
            }
        }
    }
}